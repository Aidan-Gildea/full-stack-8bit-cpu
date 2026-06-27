# AG-Core Assembler, Emulator, and Disassembler
A custom 32-bit Instruction Set Architecture (ISA) with a complete software stack, including a two-pass assembler, emulator, and disassembler.

## Overview
The **AG-Core** is a custom ISA designed to explore low-level systems and the hardware-software interface. This repository contains the full toolchain required to write, compile, and execute programs on a virtual CPU.

### Technical Specifications
* **Instruction Length:** 32-bit (fixed-length, 4 bytes).
* **Instruction Format:** `[OPCODE (8-bit) | PARAM1 (8-bit) | PARAM2 (8-bit) | PARAM3 (8-bit)]`.
* **Registers:** 32 registers, including special-purpose registers for Instruction Pointer (`IP`), Flags, and Random Number Generation.
* **Execution Model:** Simulated fetch-decode-execute cycle within the Emulator.

### 🛠️ The Software Stack
* **ISALib:** The core library defining opcodes, register mappings, and the binary specification used by all the other tools.
* **Two-Pass Assembler:** Supports labels and symbols. It first maps label addresses and then generates machine code.
* **Emulator:** A virtual environment that simulates CPU with registers and basic I/O (Read/Print).
* **Disassembler:** A program to reverse the binary machine code produced by the two-pass assembler back into human-readable assembly. The central purpose of the disassembler is for debugging. 

## Project Highlight: Rock-Paper-Scissors
To validate the architecture, I developed a functional "Rock Paper Scissors" game written entirely in AG-Core assembly. The program handles user input via the `READ` opcode, implements game logic using conditional jumps (`JMPZ`), and outputs results using the `PRNT` opcode.

## ISA Specification
The full bit-layout, opcodes, and register definitions are documented in this [ISA Specification Sheet](https://docs.google.com/spreadsheets/d/1EImqmfBQeWMdciui3smtlShUqmLWG_6YjswOCMwuLK0/edit?usp=sharing).

## Getting Started
### Prerequisites
* .NET 8.0 SDK

### Building the Project
```bash
dotnet build ISA.sln
```
