using ISALib;
using Xunit;
using AsmTool = ISA.Assembler.Assembler;
using DisasmTool = ISA.Disassembler.Disassembler;

namespace ISA.Tests
{
    public class ToolchainTests
    {
        static string Data(string name) => Path.Combine(AppContext.BaseDirectory, "GoldenData", name);

        public static IEnumerable<object[]> Programs =>
        [
            ["CountToFive"],
            ["Fibonacci"],
            ["InfiniteCouter"],
            ["MemorySwap"],
            ["PrintDigits"],
            ["RockPaperScissors"],
        ];

        // ---- Golden binaries: the assembler must keep producing byte-identical output ----

        [Theory]
        [MemberData(nameof(Programs))]
        public void Assembling_matches_the_committed_golden_binary(string program)
        {
            byte[] assembled = AsmTool.Assemble(Data(program + ".asm"));
            byte[] golden = File.ReadAllBytes(Data(program + ".bin"));

            Assert.Equal(golden, assembled);
        }

        // ---- The round trip: assemble(disassemble(bin)) == bin for every program ----

        [Theory]
        [MemberData(nameof(Programs))]
        public void Reassembling_a_disassembly_reproduces_the_binary_exactly(string program)
        {
            string binPath = Data(program + ".bin");
            string disassembly = DisasmTool.Disassemble([], binPath);

            string tempAsm = Path.Combine(Path.GetTempPath(), $"roundtrip_{program}_{Guid.NewGuid():N}.asm");
            try
            {
                File.WriteAllText(tempAsm, disassembly);
                byte[] reassembled = AsmTool.Assemble(tempAsm);

                Assert.Equal(File.ReadAllBytes(binPath), reassembled);
            }
            finally
            {
                File.Delete(tempAsm);
            }
        }

        // ---- Every opcode survives encode -> decode -> encode on its own ----

        [Fact]
        public void Every_opcode_survives_an_assemble_disassemble_assemble_round_trip()
        {
            var labels = new Dictionary<string, byte> { ["LOOP"] = 3 };

            foreach ((string mnemonic, Instruc instruc) in OpCodes.Codes)
            {
                string[] parts = CanonicalParts(mnemonic, instruc);
                byte[] first = instruc.Assemble(parts, labels);

                string text = instruc.Disassemble(first);
                string[] reparsed = text.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                byte[] second = instruc.Assemble(reparsed, new Dictionary<string, byte>());

                Assert.True(first.SequenceEqual(second),
                    $"{mnemonic}: '{text.Trim()}' reassembled to [{string.Join(" ", second.Select(b => b.ToString("X2")))}] " +
                    $"instead of [{string.Join(" ", first.Select(b => b.ToString("X2")))}]");
            }
        }

        static string[] CanonicalParts(string mnemonic, Instruc instruc)
        {
            string[] parts = new string[4];
            for (int i = 0; i < 4; i++)
            {
                parts[i] = instruc.Order[i] switch
                {
                    Instruc.Thing.OPCODE => mnemonic,
                    Instruc.Thing.REGISTER => "R1",
                    Instruc.Thing.VALUE => "7",
                    Instruc.Thing.LABEL => "LOOP",
                    _ => "0",
                };
            }
            return parts;
        }

        // ---- Spot checks against the ISA spec, so encodings can never silently drift ----

        [Fact]
        public void Add_encodes_as_opcode_then_three_registers()
        {
            byte[] bytes = OpCodes.Codes["ADD"].Assemble(["ADD", "R1", "R2", "R3"], []);
            Assert.Equal(new byte[] { 0x10, 0x09, 0x0A, 0x0B }, bytes);
        }

        [Fact]
        public void Set_encodes_register_then_immediate_then_pad()
        {
            byte[] bytes = OpCodes.Codes["SET"].Assemble(["SET", "R0", "5", "0"], []);
            Assert.Equal(new byte[] { 0x40, 0x08, 0x05, 0x00 }, bytes);
        }

        [Fact]
        public void Labels_resolve_to_the_index_of_the_next_real_instruction()
        {
            string tempAsm = Path.Combine(Path.GetTempPath(), $"labels_{Guid.NewGuid():N}.asm");
            try
            {
                // Label sits after one real instruction, so TOP must resolve to 1.
                File.WriteAllText(tempAsm, "SET R0 5\n: TOP\nJMP TOP\n");
                byte[] bytes = AsmTool.Assemble(tempAsm);

                Assert.Equal(new byte[] { 0x30, 0x01, 0x00, 0x00 }, bytes[4..8]);
            }
            finally
            {
                File.Delete(tempAsm);
            }
        }

        [Fact]
        public void Disassembly_puts_each_instruction_on_its_own_line()
        {
            string disassembly = DisasmTool.Disassemble([], Data("CountToFive.bin"));
            string[] lines = disassembly.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            long slots = new FileInfo(Data("CountToFive.bin")).Length / 4;
            Assert.Equal(slots, lines.Length);
            Assert.Equal("SET R0 5", lines[0].Trim());
        }
    }
}
