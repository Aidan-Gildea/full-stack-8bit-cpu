# SimpleISA

A custom instruction set with an assembler, disassembler, emulator, and a Logisim CPU that runs it.

## What's here

- **ISALib** (`toolchain/ISALib`): a .NET class library containing all the definitions from the ISA spec implemented in code, shared by the assembler, disassembler, emulator, and other tools.
- **Assembler** (`toolchain/Assembler`): two-pass assembler with label support, turns assembly files (`.asm`) files into machine code.
- **Disassembler** (`toolchain/Disassembler`): turns machine code back into readable assembly for debugging.
- **Emulator** (`toolchain/Emulator`): runs the machine code in software. Written in C#, with registers and basic I/O.
- **Logisim CPU** (`logisim/logisim/SimpleCPU.circ`): a gate-level CPU that executes the assembled bytecode in hardware.
- **Spec** (`spec/`): the full instruction and register definitions as CSV.

## The ISA

- 32-bit fixed-length instructions: `[OPCODE | PARAM1 | PARAM2 | PARAM3]`, one byte each.
- 32 registers, including special-purpose ones for the instruction pointer, flags, char I/O, and random number generation.
- ~30 instructions covering math, logic, memory, control flow, and I/O. Full list in `spec/isa-instructions.csv`.

## Demo: Rock Paper Scissors

A full Rock Paper Scissors game written in SimpleISA assembly (`toolchain/Assembler/TestData/RockPaperScissors.asm`). It reads user input with `READ`, branches with `JMPZ`, and prints results with `PRNT`.

## Media

The full CPU built in Logisim:

![SimpleISA CPU in Logisim](media/cpu-layout.png)

## Getting started

Requires the .NET 8.0 SDK.

```bash
dotnet build toolchain/ISA.sln
```

The Logisim CPU opens in [Logisim Evolution](https://github.com/logisim-evolution/logisim-evolution).

