### Logisim Folder
This folder contains three subfolders dedicated to the following:
1. **logisim_full**: A full 8-bit cpu with 24 implemented instructions. Includes various components including a Control Unit, ALU, FLOW Circuit, registers and RAM. 
2. **logisim_small**: A significantly reduced version of the logisim_full alu which can perform basic arithmetic (ADD / SUB) by reading from ROM and performing operations using a multiplexer, full adder and XOR IC. 
3. **example bytecode**: A folder dedicated to example programs pre-assembled to bytecode in my ISA. 

In order to execute the programs in the example_bytecode, do the following: 
- Select ROM component in main circuit
- Click on contents in properties tab
- In the popup bytecode editor, select open
- select one of the example programs in this folder


Program definitions: 
count_to_five - a program which utilizes labels, JMP and manipulation of the IP to count to five iteratively. 

