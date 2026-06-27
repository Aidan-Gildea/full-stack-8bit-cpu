using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ISALib
{

    public class Instruc
    {
        public enum Thing
        {
            OPCODE,
            REGISTER,
            VALUE,
            PAD,
            LABEL
        };

        public Thing[] Order;

        public KeyValuePair<string, byte> OpCode;

        public Action<ushort[], byte, byte, byte, int> operation; //action is when void, func is when function is not void. 
        

        public Instruc(Thing[] ORDER, KeyValuePair<string, byte> OPCODE, Action<ushort[], byte, byte, byte, int> action = null)
        {
            Order = ORDER;
            OpCode = OPCODE;
            this.operation = action;
        }

        public byte[] Assemble(string[] perameters, Dictionary<string, byte> labels) //includes opcode, so offset ny 1
        {
            byte[] bytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                switch (Order[i])
                {
                    case Thing.OPCODE:
                        bytes[i] = OpCode.Value;
                        break;

                    case Thing.REGISTER:
                        bytes[i] = Registers.reg[perameters[i]];
                        break;

                    case Thing.VALUE:
                        bytes[i] = byte.Parse(perameters[i]);
                        break;

                    case Thing.PAD:
                        bytes[i] = 0x00;
                        break;

                    case Thing.LABEL:
                        bytes[i] = labels[perameters[i]];
                        break;
                }
            }

            return bytes;
        }

        public string Disassemble(byte[] args) 
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < args.Length; i++) 
            {
                switch (Order[i]) 
                {
                    case Thing.OPCODE:
                        sb.Append($"{OpCode.Key} ");
                        break;
                    case Thing.REGISTER: //turns the register into a string, using the dictionary in Registers.cs
                        sb.Append($"{Registers.reg.FirstOrDefault(x => x.Value == args[i]).Key} ");
                        break;
                    case Thing.VALUE: //turns the value into a string
                        sb.Append($"{args[i]} ");
                        break;
                    case Thing.PAD://does nothing, just a placeholder for the instruction
                        break;
                    case Thing.LABEL:
                        sb.Append($"{args[i]} ");
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
