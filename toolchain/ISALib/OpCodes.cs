using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static ISALib.Instruc.Thing;
using static ISALib.Operations;

namespace ISALib
{
    public static class OpCodes
    {
        public static Dictionary<string, Instruc> Codes = new()
        {

            // MATH
            ["NONE"] = new([OPCODE, PAD, PAD, PAD], new("NONE", 0x00), NONE),
            ["SET"] = new([OPCODE, REGISTER, VALUE, PAD], new("SET", 0x41), SET),
            ["ADD"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("ADD", 0x10), ADD), //if you want to pass in function wthout calling, don't do ()
            ["SUB"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("SUB", 0x11), SUB),
            ["MULT"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("MULT", 0x12), MULT),
            ["DIV"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("DIV", 0x13), DIV),
            ["EQ"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("EQ", 0x14), EQ),
            ["GTHAN"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("GTHAN", 0x15), GTHAN),
            ["LTHAN"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("LTHAN", 0x16), LTHAN),

            // 

        };

        
    }
}
