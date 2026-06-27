using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ISALib
{
    public class Operations
    {
        public static void ADD(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] + registers[PARAM3]);
        }

        public static void SUB(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] - registers[PARAM3]);
        }

        public static void MULT(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] * registers[PARAM3]);
        }

        public static void DIV(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            if (registers[PARAM3] != 0)
                registers[PARAM1] = (ushort)(registers[PARAM2] / registers[PARAM3]);
            else
                registers[PARAM1] = 0; // handle divide by zero as zero output
        }

        public static void EQ(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (registers[PARAM2] == registers[PARAM3]) ? (ushort)1 : (ushort)0;
        }

        public static void GTHAN(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (registers[PARAM2] > registers[PARAM3]) ? (ushort)1 : (ushort)0;
        }

        public static void LTHAN(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (registers[PARAM2] < registers[PARAM3]) ? (ushort)1 : (ushort)0;
        }

        public static void NOT(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(~registers[PARAM2]);
        }

        public static void AND(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] & registers[PARAM3]);
        }

        public static void OR(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] | registers[PARAM3]);
        }

        public static void NOR(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(~(registers[PARAM2] | registers[PARAM3]));
        }

        public static void NAND(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(~(registers[PARAM2] & registers[PARAM3]));
        }

        public static void XOR(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] ^ registers[PARAM3]);
        }

        public static void LSHF(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] << registers[PARAM3]);
        }

        public static void RSHF(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)(registers[PARAM2] >> registers[PARAM3]);
        }

        public static void NONE(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            return;
        }

        public static void SET(ushort[] registers, byte PARAM1, byte PARAM2, byte PARAM3)
        {
            registers[PARAM1] = (ushort)PARAM2;
        }

    }
}
