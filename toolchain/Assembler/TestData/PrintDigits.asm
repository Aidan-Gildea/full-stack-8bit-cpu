# Prints the characters 1 through 5 to the console using the
# CHAR register and the PRNT flag. (Emulator only -- PRNT, INC,
# and SETBIT are not implemented in the logisim CPU.)

SET R0 49       # ASCII '1'
SET R1 54       # ASCII '6', the stop value

: LOOP
MOV CHAR R0     # Put the character into the CHAR register
SETBIT FLAGS 0  # Set the PRNT flag
PRNT            # Prints CHAR and clears the flag
INC R0
JMPEQ DONE R0 R1
JMP LOOP

: DONE
