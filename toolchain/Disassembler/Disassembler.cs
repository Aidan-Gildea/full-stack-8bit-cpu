using System.Collections.Generic;
using System.Text;
using static ISALib.OpCodes;

namespace ISA.Disassembler
{
    internal class Disassembler
    {

        // Defaults are relative to the executable's directory (TestData is copied next to the build output).
        static readonly string DefaultBinaryFileName = Path.Combine(AppContext.BaseDirectory, "TestData", "TestInfiniteCounter.bin");
        static readonly string DefaultDisassemblyFileName = Path.Combine(AppContext.BaseDirectory, "TestData", "InfiniteCounter.dasm");

        static void Main(string[] args)
        {
            // Usage: Disassembler [inputBinFile] [outputDasmFile]
            string binaryFileName = args.Length > 0 ? args[0] : DefaultBinaryFileName;
            string disassemblyFileName = args.Length > 1 ? args[1] : DefaultDisassemblyFileName;

            string disassembly = Disassemble(args, binaryFileName);

            Directory.CreateDirectory(Path.GetDirectoryName(disassemblyFileName));
            File.WriteAllText(disassemblyFileName, disassembly);

            Console.WriteLine($"Disassembled '{binaryFileName}' -> '{disassemblyFileName}'.");
        }

        static string Disassemble(string[] args, string FileName)
        {
            byte[] machineCode = File.ReadAllBytes(FileName);

            StringBuilder disassembly = new();

            for (int i = 0; i < machineCode.Length; i += 4) // will iterate through  
            {
                byte opCode = machineCode[i];
                // couldn't I just convert to string for this -_-
                var key = Codes.FirstOrDefault(x => x.Value.OpCode.Value == opCode).Key; // iterate through each keyvaluepair, check the instruc.opcode.value and see if it is equal to opcode, then get the key to get opcode. 
                
                
                //get the bytes to translate. 
                byte[] bytes = new byte[4];
                for (int j = 0; j < 4; j++)
                {
                    bytes[j] = machineCode[i + j];
                }
                disassembly.Append(Codes[key].Disassemble(bytes)); //shorten the passed byte array to the current 4 bytes / total instruction, and then fill out instruction command. 

            }
            return disassembly.ToString();
            
        }
    }
}
