using System.Reflection.Emit;
using System.Security.Cryptography;
using ISALib;
using static ISALib.Instruc.Thing;
using static ISALib.OpCodes;

namespace ISA.Assembler
{
    public class Assembler
    {
        //done!
        const string COMMENT = "#"; // comment string. 
        const string LABEL = ":";
        const string COMMENT2 = ";";
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

        public static byte[] Assemble(string File)
        {
            //first pass
            string[] file = ParseStringsFromFile(File);

            Dictionary<string, byte> labels = new();

            int passednonlabels = 0; 
            for(int l = 0; l < file.Length; l++) 
            {
                string line = file[l];
                int commentIndex = line.IndexOf(COMMENT);
                if (commentIndex >= 0) line = line.Substring(0, commentIndex);
                commentIndex = line.IndexOf(COMMENT2);
                if (commentIndex >= 0) line = line.Substring(0, commentIndex);

                string[] parts = line.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) 
                {
                    continue;
                }//splits each line into separate parts. 
                //if (parts[0] == COMMENT2) continue; // Handled above
                if (parts[0] == LABEL) 
                {
                    labels.Add(parts[1], (byte)(passednonlabels)); //note that the indexes start at 1. If i want to point 2 lines in the future, have to do +2
                }
                else 
                {
                    passednonlabels++;
                }
            }
            //iterate through and get all labels. 

            int currentByte = 0;
            byte[] machineCode = new byte[file.Length * INSTRUCTION_LENGTH];



            foreach (var rawLine in file)
            {
                string line = rawLine;
                int commentIndex = line.IndexOf(COMMENT);
                if (commentIndex >= 0) line = line.Substring(0, commentIndex);
                commentIndex = line.IndexOf(COMMENT2);
                if (commentIndex >= 0) line = line.Substring(0, commentIndex);

                if(line == "") 
                {
                    continue;
                }
                string[] parts = line.ToUpper().Split(' ', StringSplitOptions.RemoveEmptyEntries); //splits each line into separate parts. 
                if (parts.Length == 0) continue;

                //if (parts[0] == COMMENT2) continue;

                if (parts[0] == LABEL) 
                {
                    continue;
                }

                byte[] bytes = Codes[parts[0]].Assemble(parts, labels);
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
