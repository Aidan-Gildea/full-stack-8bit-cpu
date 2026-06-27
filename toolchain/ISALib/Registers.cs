using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISALib
{
    public class Registers
    {
        public static Dictionary<string, byte> reg = new()
        {
            ["ZERO"] = 0x00,
            ["ONE"] = 0x01,
            ["CHAR"] = 0x02,
            ["FLAGS"] = 0x03,
            ["RANDOM"] = 0x04,
            ["USER"] = 0x05,
            ["IP"] = 0x06,
            ["FREE_17"] = 0x07,
            ["R0"] = 0x08,
            ["R1"] = 0x09,
            ["R2"] = 0x0A,
            ["R3"] = 0x0B,
            ["R4"] = 0x0C,
            ["R5"] = 0x0D,
            ["R6"] = 0x0E,
            ["R7"] = 0x0F,
            ["R8"] = 0x10,
            ["R9"] = 0x11,
            ["R10"] = 0x12,
            ["R11"] = 0x13,
            ["R12"] = 0x14,
            ["R13"] = 0x15,
            ["R14"] = 0x16,
            ["R15"] = 0x17,
            ["FREE_18"] = 0x18,
            ["FREE_19"] = 0x19,
            ["FREE_1A"] = 0x1A,
            ["FREE_1B"] = 0x1B,
            ["FREE_1C"] = 0x1C,
            ["FREE_1D"] = 0x1D,
            ["FREE_1E"] = 0x1E,
            ["FREE_1F"] = 0x1F
        };
    }
}
