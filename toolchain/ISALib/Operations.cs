using System;
using System.Collections.Generic;
using static ISALib.Registers;

namespace ISALib
{
    public class Operations
    {
        public static void ADD(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] + registers[R2]);
        }

        public static void SUB(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] - registers[R2]);
        }

        public static void MULT(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] * registers[R2]);
        }

        public static void DIV(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            if (registers[R2] != 0)
                registers[outR] = (ushort)(registers[R1] / registers[R2]);
            else
                registers[outR] = 0; // handle divide by zero
        }

        public static void EQ(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (registers[R1] == registers[R2]) ? (ushort)1 : (ushort)0;
        }

        public static void GTHAN(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (registers[R1] > registers[R2]) ? (ushort)1 : (ushort)0;
        }

        public static void LTHAN(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (registers[R1] < registers[R2]) ? (ushort)1 : (ushort)0;
        }

        public static void SETBIT(ushort[] registers, byte outR, byte val, byte pad, int EXTRA)
        {
            registers[outR] |= (ushort)(1 << val);
        }

        public static void CLRBIT(ushort[] registers, byte outR, byte val, byte pad, int EXTRA)
        {
            ushort mask = 0;
            for (int i = 0; i < 16; i++)
            {
                if (i != val)
                    mask |= (ushort)(1 << i);
            }
            registers[outR] &= mask;
        }

        public static void SET(ushort[] registers, byte R1, byte val, byte pad, int EXTRA)
        {
            registers[R1] = (ushort)val;
        }

        public static void MOV(ushort[] registers, byte outR, byte R1, byte pad, int EXTRA)
        {
            registers[outR] = registers[R1];
        }

        public static void PRNT(ushort[] registers, byte valR, byte pad1, byte pad2, int EXTRA)
        {
            if ((registers[reg["FLAGS"]] & 1) == 1)
            {
                CLRBIT(registers, reg["FLAGS"], 0, 0, EXTRA);
                Console.Write((char)registers[reg["CHAR"]]);
            }
        }

        public static void READ(ushort[] registers,byte b, byte pad1, byte pad2, int EXTRA)
        {
            //Console.WriteLine("ello");
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                registers[reg["CHAR"]] = (ushort)keyInfo.KeyChar;
                SETBIT(registers, reg["FLAGS"], (byte)FlagIndex["READ"], 0, 0);
            }
            //CLRBIT(registers, reg["FLAGS"], (byte)FlagIndex["READ"], 0, 0);
        }

        public static void RNDM(ushort[] registers, byte MinV, byte MaxV, byte pad, int EXTRA)
        {
            if (((registers[reg["FLAGS"]] & (1 << FlagIndex["RNDM"])) >> FlagIndex["RNDM"]) == 1)
            {
                CLRBIT(registers, reg["FLAGS"], (byte)FlagIndex["RNDM"], 0, EXTRA);
                Random rnd = new Random();
                registers[reg["RNDM"]] = (ushort)rnd.Next(MinV, MaxV);
            }
        }

        public static void LOAD(ushort[] registers, byte Mem1, byte Mem2, byte outR, int EXTRA)
        {
            throw new NotImplementedException();
        }

        public static void STR(ushort[] registers, byte Mem1, byte Mem2, byte inR, int EXTRA)
        {
            throw new NotImplementedException();
        }

        public static void INC(ushort[] registers, byte R1, byte pad1, byte pad2, int EXTRA)
        {
            registers[R1]++;
        }

        public static void DEC(ushort[] registers, byte R1, byte pad1, byte pad2, int EXTRA)
        {
            registers[R1]--;
        }

        public static void NOT(ushort[] registers, byte outR, byte R1, byte pad, int EXTRA)
        {
            registers[outR] = (ushort)(~registers[R1]);
        }

        public static void AND(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] & registers[R2]);
        }

        public static void OR(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] | registers[R2]);
        }

        public static void NOR(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(~(registers[R1] | registers[R2]));
        }

        public static void NAND(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(~(registers[R1] & registers[R2]));
        }

        public static void XOR(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] ^ registers[R2]);
        }

        public static void LSHF(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] << registers[R2]);
        }

        public static void RSHF(ushort[] registers, byte outR, byte R1, byte R2, int EXTRA)
        {
            registers[outR] = (ushort)(registers[R1] >> registers[R2]);
        }

        public static void NONE(ushort[] registers, byte pad1, byte pad2, byte pad3, int EXTRA)
        {
            return;
        }
        public static void JMP(ushort[] registers, byte label, byte pad1, byte pad2, int EXTRA) //note that the label is limited to a byte, only 255 lines of code. 
        {
            
            registers[reg["IP"]] = label;
        }

        public static void JMPZ(ushort[] registers, byte label, byte R1, byte pad, int EXTRA)
        {
            if (registers[R1] != 0) 
            {
                return;
            }
            registers[reg["IP"]] = label;
        }
    }
}
