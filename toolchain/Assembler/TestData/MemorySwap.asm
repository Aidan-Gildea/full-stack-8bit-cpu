# Swaps the values in R0 and R1 through RAM, using STR and LOAD.

SET R0 11
SET R1 22

STR R0 0        # RAM[0] = R0
STR R1 1        # RAM[1] = R1

LOAD R0 1       # R0 = RAM[1]
LOAD R1 0       # R1 = RAM[0]
