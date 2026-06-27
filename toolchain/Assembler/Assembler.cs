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

        const string AssemblyCodeFile = "C:\\Users\\Aidan.Gildea\\source\\repos\\ISA\\Assembler\\bin\\Debug\\net8.0\\TestData\\InfiniteCouter.asm";
        const string BinaryOutputFile = "TestData\\TestInfiniteCounter.bin";

        static void Main(string[] args)
        {
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string projectPath = Path.Combine(docPath, "source", ) for dynamic file paths


            byte[] machineCode = Assemble(AssemblyCodeFile);
            File.WriteAllBytes(BinaryOutputFile, machineCode);


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
