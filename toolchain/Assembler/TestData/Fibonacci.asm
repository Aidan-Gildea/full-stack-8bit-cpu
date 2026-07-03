# Computes Fibonacci numbers until they pass 200:
# 1 1 2 3 5 8 13 21 34 55 89 144 233
# R0 = previous number, R1 = current number

SET R0 0
SET R1 1
SET R4 200      # Limit

: LOOP
ADD R2 R0 R1    # next = previous + current
MOV R0 R1
MOV R1 R2
GTHAN R3 R1 R4  # Done when current > limit
JMPZ LOOP R3
