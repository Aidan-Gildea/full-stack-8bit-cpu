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
            ["NONE"] = new([OPCODE, PAD, PAD, PAD], new("NONE", 0x00), NONE),
            ["ADD"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("ADD", 0x10), ADD),
            ["SUB"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("SUB", 0x11), SUB),
            ["MULT"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("MULT", 0x12), MULT),
            ["DIV"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("DIV", 0x13), DIV),
            ["LSHF"] = new([OPCODE, REGISTER, REGISTER, PAD], new("LSHF", 0x14), LSHF),   //shifts left by a single bit
            ["RSHF"] = new([OPCODE, REGISTER, REGISTER, PAD], new("RSHF", 0x15), RSHF),   //shifts right by a single bit
            ["GTHAN"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("GTHAN", 0x16), GTHAN),
            ["EQ"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("EQ", 0x17), EQ),
            ["LTHAN"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("LTHAN", 0x18), LTHAN),
            ["SETBIT"] = new([OPCODE, REGISTER, VALUE, PAD], new("SETBIT", 0x19), SETBIT),
            ["CLRBIT"] = new([OPCODE, REGISTER, VALUE, PAD], new("CLRBIT", 0x1A), CLRBIT),
            ["NOT"] = new([OPCODE, REGISTER, REGISTER, PAD], new("NOT", 0x20), NOT),
            ["AND"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("AND", 0x21), AND),
            ["OR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("OR", 0x22), OR),
            ["NOR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("NOR", 0x23), NOR),
            ["NAND"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("NAND", 0x24), NAND),
            ["XOR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("XOR", 0x25), XOR),
            ["RSHFVAR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("RSHFVAR", 0x26), RSHFVAR), //shifts right by the value in R2
            ["JMP"] = new([OPCODE, LABEL, PAD, PAD], new("JMP", 0x30), JMP),
            ["JMPZ"] = new([OPCODE, LABEL, REGISTER, PAD], new("JMPZ", 0x31), JMPZ),
            ["JMPEQ"] = new([OPCODE, LABEL, REGISTER, REGISTER], new("JMPEQ", 0x32), JMPEQ),
            ["SET"] = new([OPCODE, REGISTER, VALUE, PAD], new("SET", 0x40), SET),
            ["MOV"] = new([OPCODE, REGISTER, REGISTER, PAD], new("MOV", 0x41), MOV),
            ["LOAD"] = new([OPCODE, REGISTER, VALUE, PAD], new("LOAD", 0x42), LOAD),  //LOAD outR Addr
            ["STR"] = new([OPCODE, REGISTER, VALUE, PAD], new("STR", 0x43), STR),     //STR inR Addr
            ["PRNT"] = new([OPCODE, PAD, PAD, PAD], new("PRNT", 0x46), PRNT),
            ["READ"] = new([OPCODE, PAD, PAD, PAD], new("READ", 0x47), READ),
            ["INC"] = new([OPCODE, REGISTER, PAD, PAD], new("INC", 0x48), INC),
            ["DEC"] = new([OPCODE, REGISTER, PAD, PAD], new("DEC", 0x49), DEC),
            ["RNDM"] = new([OPCODE, VALUE, VALUE, PAD], new("RNDM", 0x4A), RNDM),
        };

    }
}
