using System.Collections.Generic;
using System.Text;
using static ISALib.OpCodes;

namespace ISA.Disassembler
{
    internal class Disassembler
    {

        const string binaryFileName = "C:\\Users\\Aidan.Gildea\\source\\repos\\ISA\\Assembler\\bin\\Debug\\net8.0\\TestData\\TestInfiniteCounter.bin";
        const string disassemblyFileName = "C:\\Users\\Aidan.Gildea\\source\\repos\\ISA\\Assembler\\bin\\Debug\\net8.0\\TestData\\InfiniteCounter.dasm";
        static void Main(string[] args)
        {
            string disassembly = Disassemble(args, binaryFileName);
            File.WriteAllText(disassemblyFileName, disassembly);
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
