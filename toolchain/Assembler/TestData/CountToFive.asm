# Counts from 0 up to 5 in R1, then stops.
# This is the same program the logisim CPU runs in the README gif
# (logisim/example bytecode/count_to_five), except the hardware
# version jumps back to the start so it loops forever.

SET R0 5         # Target value
SET R1 0         # Counter
SET R2 1         # Increment amount

: LOOP
ADD R1 R1 R2     # Counter += 1
JMPEQ DONE R1 R0 # Stop once the counter hits the target
JMP LOOP

: DONE
