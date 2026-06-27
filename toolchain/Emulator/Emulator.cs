using System.Diagnostics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using static ISALib.OpCodes;
using static ISALib.Registers;
using static ISALib.Operations;
using System.Reflection.Emit;

namespace ISA.Emulator
{
    internal class Emulator
    {

        const int NUM_OF_REGISTERS = 32;

        static ushort[] Registers = new ushort[NUM_OF_REGISTERS]; // checks off :)

        static int InstructionRegister = 0;

        static int IP => Registers[reg["IP"]];

        const string machineFileName = "C:\\Users\\Aidan.Gildea\\source\\repos\\ISA\\Assembler\\bin\\Debug\\net8.0\\TestData\\TestInfiniteCounter.bin";

        //Dictionary<> Is there a way to make a function dictionary??
        //make gotos
        //implement special registers
        //get proper registers working
        //make it as close to an actual computer as possible? 

        //For Print, wait until flag is marked by emulator, i do the magic, then Print whatever is in char register and set flag to 0. 

        static void Main(string[] args)
        {
            int[] instructions = ConvertBinToInstructions(machineFileName);

            Registers[reg["IP"]] = 0;

            while (IP < instructions.Length) 
            {
                InstructionRegister = instructions[IP]; // set instruction to index of instructions based off of instruction pointer
                Registers[reg["IP"]]++; // move instruction pointer up 1

                //Now, you have the current instruction. Next, you want to complete the operations. //xxxxxxxx xxxxxxxx xxxxxxxx xxxxxxxx
                byte OPCODE = (byte)(InstructionRegister >> 24);
                byte PARAM1 = (byte)((InstructionRegister << 8) >> 24);
                byte PARAM2 = (byte)((InstructionRegister << 16) >> 24);
                byte PARAM3 = (byte)((InstructionRegister << 24) >> 24);

                //possible idea: pass in the operation function as a perameter to the dictionar in opcodes, instead of using this switch. 
                string key = Codes.FirstOrDefault(x => x.Value.OpCode.Value == OPCODE).Key;
                Codes[key].operation(Registers, PARAM1, PARAM2, PARAM3);

            }


        }

        static int[] ConvertBinToInstructions(string Address) // ts so ahh 💀
        {
            //will die on stack, its ok
            //just bundling up each byte into integers? 
            byte[] machineCode = File.ReadAllBytes(Address);
            int[] instructions = new int[machineCode.Length / 4];
            int counter = 0;
            int currI = 0;
            for(int i = 0; i < machineCode.Length; i++) 
            {
                instructions[currI] |= machineCode[i] << ((3-counter) * 8); //opcode, then params. 
                counter++;
                if(counter > 3) 
                {
                    currI++;
                    counter = 0;
                }
            }

            return instructions;

        }
    }
}
