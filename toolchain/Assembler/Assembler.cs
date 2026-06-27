using System.Reflection.Emit;
using System.Security.Cryptography;
using ISALib;
using static ISALib.Instruc.Thing;
using static ISALib.OpCodes;

namespace ISA.Assembler
{
    internal class Assembler
    {
        const string COMMENT = "#"; // comment string. 
        const string LABEL = ":";
        const byte INSTRUCTION_LENGTH = 4; // 4 bytes / instruction

        // Defaults are relative to the executable's directory (TestData is copied next to the build output).
        static readonly string DefaultAssemblyCodeFile = Path.Combine(AppContext.BaseDirectory, "TestData", "InfiniteCouter.asm");
        static readonly string DefaultBinaryOutputFile = Path.Combine(AppContext.BaseDirectory, "TestData", "TestInfiniteCounter.bin");

        static void Main(string[] args)
        {
            // Usage: Assembler [inputAsmFile] [outputBinFile]
            string assemblyCodeFile = args.Length > 0 ? args[0] : DefaultAssemblyCodeFile;
            string binaryOutputFile = args.Length > 1 ? args[1] : DefaultBinaryOutputFile;

            byte[] machineCode = Assemble(assemblyCodeFile);

            Directory.CreateDirectory(Path.GetDirectoryName(binaryOutputFile));
            File.WriteAllBytes(binaryOutputFile, machineCode);

            Console.WriteLine($"Assembled '{assemblyCodeFile}' -> '{binaryOutputFile}' ({machineCode.Length} bytes).");
        }

        static string[] ParseStringsFromFile(string path) 
        {
            return File.ReadAllLines(path);
        }

        static byte[] Assemble(string File) 
        {
            Dictionary<string, byte> labels;
            //first pass
            string[] file = ParseStringsFromFile(File);
            foreach(var l in file) 
            {
                string[] parts = l.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in parts) 
                {
                    if(word == LABEL) 
                    {
                        //add label to dictoinary
                    }
                }
            }

            int currentByte = 0;
            byte[] machineCode = new byte[file.Length * INSTRUCTION_LENGTH];



            foreach (var line in file)
            {
                string[] parts = line.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries); //splits each line into separate parts. 

                byte[] bytes = Codes[parts[0]].Assemble(parts);
                foreach (byte val in bytes)
                {
                    machineCode[currentByte] = val;
                    currentByte++;
                }
            }
            return machineCode;

        }
    }
}
