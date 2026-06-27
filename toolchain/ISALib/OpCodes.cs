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
            ["SET"] = new([OPCODE, REGISTER, VALUE, PAD], new("SET", 0x41), SET),
            ["ADD"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("ADD", 0x10), ADD),
            ["SUB"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("SUB", 0x11), SUB),
            ["MULT"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("MULT", 0x12), MULT),
            ["DIV"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("DIV", 0x13), DIV),
            ["EQ"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("EQ", 0x14), EQ),
            ["GTHAN"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("GTHAN", 0x15), GTHAN),
            ["LTHAN"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("LTHAN", 0x16), LTHAN),
            ["SETBIT"] = new([OPCODE, REGISTER, VALUE, PAD], new("SETBIT", 0x17), SETBIT),
            ["CLRBIT"] = new([OPCODE, REGISTER, VALUE, PAD], new("CLRBIT", 0x18), CLRBIT),
            ["MOV"] = new([OPCODE, REGISTER, REGISTER, PAD], new("MOV", 0x40), MOV),
            ["PRNT"] = new([OPCODE, PAD, PAD, PAD], new("PRNT", 0x42), PRNT),
            ["READ"] = new([OPCODE, PAD, PAD, PAD], new("READ", 0x44), READ),
            ["RNDM"] = new([OPCODE, VALUE, VALUE, PAD], new("RNDM", 0x43), RNDM),
            ["LOAD"] = new([OPCODE, VALUE, VALUE, REGISTER], new("LOAD", 0x46), LOAD),  //note that the two values are actually two parts of the mem address. 
            ["STR"] = new([OPCODE, VALUE, VALUE, REGISTER], new("STR", 0x47), STR),     //note that the two values are actually two parts of the mem address. 
            ["INC"] = new([OPCODE, REGISTER, PAD, PAD], new("INC", 0x48), INC),
            ["DEC"] = new([OPCODE, REGISTER, PAD, PAD], new("DEC", 0x4B), DEC),
            ["NOT"] = new([OPCODE, REGISTER, REGISTER, PAD], new("NOT", 0x20), NOT),
            ["AND"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("AND", 0x21), AND),
            ["OR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("OR", 0x22), OR),
            ["NOR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("NOR", 0x23), NOR),
            ["NAND"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("NAND", 0x24), NAND),
            ["XOR"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("XOR", 0x25), XOR),
            ["LSHF"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("LSHF", 0x26), LSHF),
            ["RSHF"] = new([OPCODE, REGISTER, REGISTER, REGISTER], new("RSHF", 0x27), RSHF),
            ["JMP"] = new([OPCODE, LABEL, PAD, PAD], new("JMP", 0x30), JMP),
            ["JMPZ"] = new([OPCODE, LABEL, REGISTER, PAD], new("JMPZ", 0x31), JMPZ),
        };

    }
}
