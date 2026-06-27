# Rock Paper Scissors Game
# Registers:
# R0: User Input
# R1: Computer Choice
# R2: Game State / Temp
# R3: Comparison Result
# R4: Constants (Rock=1, Paper=2, Scissors=3)

# Initialize
SET R0 0
SET R1 0
SET R2 0

# Main Loop
: START
    # Request Random Number for Computer Choice (1-3)
    # Set RNDM flag to request new random number
    SETBIT FLAGS 2 0
    # Wait for random number to be ready (simulated by just reading it after setting flag)
    # RNDM opcode: RNDM Min Max
    RNDM 1 4 0
    MOV R1 RNDM 0

    # Print Prompt "Rock (r), Paper (p), Scissors (s)?"
    # Since we can only print char by char from CHAR register, we'll skip complex strings for now
    # and just wait for input.
    
    # Wait for User Input
    # READ opcode checks Console.KeyAvailable. If true, puts char in CHAR and sets READ flag.
: WAIT_INPUT
    READ 0 0 0
    # Check if READ flag (bit 1) is set
    # We need to check bit 1 of FLAGS register.
    # AND FLAGS with 2 (binary 10). If result is 2, then bit is set.
    SET R2 2
    AND R3 FLAGS R2
    # If R3 is 0, jump back to WAIT_INPUT
    # We need a JMPZ that jumps if ZERO. But JMPZ jumps if NOT ZERO?
    # Let's check JMPZ implementation:
    # if (registers[R1] != 0) return; registers[reg["IP"]] = label;
    # So JMPZ jumps if R1 IS ZERO.
    
    # If R3 (result of AND) is 0, it means flag was NOT set. So we should jump back.
    JMPZ WAIT_INPUT R3 0
    
    # Input received in CHAR register.
    MOV R0 CHAR 0
    
    # Clear READ flag
    CLRBIT FLAGS 1 0
    
    # Echo input
    SETBIT FLAGS 0 0 # Set PRNT flag
    PRNT 0 0 0
    
    # Convert input to 1, 2, 3
    # r = 114, p = 112, s = 115
    
    SET R2 114 # 'r'
    EQ R3 R0 R2
    # If R3 is 1 (Equal), Jump to SET_ROCK
    # JMPZ jumps if ZERO. So we need to jump if NOT ZERO.
    # We don't have JMPNZ.
    # We can use EQ to compare R3 with 0.
    # If R3 == 0 (Not Equal), then EQ returns 1. JMPZ won't jump.
    # If R3 == 1 (Equal), then EQ returns 0. JMPZ will jump.
    # Wait, JMPZ jumps if R1 == 0.
    # So if R3 (Equality result) is 1 (They are equal), we want to jump.
    # But JMPZ jumps on 0.
    # So we need to invert R3? Or compare R3 with 1?
    # EQ R3 R3 ONE (Compare R3 with 1).
    # If R3 was 1, result is 1. JMPZ won't jump.
    # If R3 was 0, result is 0. JMPZ will jump.
    # This logic is getting complicated.
    
    # Let's look at JMPZ again.
    # JMPZ label R1 -> if (R1 == 0) GOTO label.
    
    # We want: IF (R0 == 'r') GOTO SET_ROCK
    # EQ R3 R0 'r' -> R3 is 1 if equal, 0 if not.
    # We want to jump if R3 is 1.
    # JMPZ jumps if R3 is 0.
    # So JMPZ skips the jump if R3 is 1.
    # This is the opposite of what we want.
    # We want to jump if condition is met.
    
    # Workaround:
    # EQ R3 R0 'r'
    # JMPZ NOT_ROCK R3  (If R3 is 0/Not Equal, jump to NOT_ROCK)
    # JMP SET_ROCK      (Else, fall through to unconditional jump)
    
    SET R2 114 # 'r'
    EQ R3 R0 R2
    JMPZ CHECK_PAPER R3 0
    SET R0 1
    JMP GAME_LOGIC 0 0
    
: CHECK_PAPER
    SET R2 112 # 'p'
    EQ R3 R0 R2
    JMPZ CHECK_SCISSORS R3 0
    SET R0 2
    JMP GAME_LOGIC 0 0

: CHECK_SCISSORS
    SET R2 115 # 's'
    EQ R3 R0 R2
    JMPZ INVALID_INPUT R3 0
    SET R0 3
    JMP GAME_LOGIC 0 0

: INVALID_INPUT
    # Handle invalid input (maybe loop back)
    JMP WAIT_INPUT 0 0

: GAME_LOGIC
    # R0: User (1,2,3)
    # R1: Computer (1,2,3)
    
    # Print Computer Choice
    # (Optional: Convert 1,2,3 back to char and print)
    
    # Check Draw
    EQ R3 R0 R1
    # If R3 is 1 (Equal), it's a draw.
    # JMPZ SKIP_DRAW R3 (If R3 is 0/Not Equal, skip draw logic)
    # But we want to jump TO draw logic if equal.
    # So: JMPZ CHECK_WIN R3
    # If R3 is 0 (Not Equal), jump to CHECK_WIN.
    # If R3 is 1 (Equal), continue to Draw.
    JMPZ CHECK_WIN R3 0
    
    # Draw Logic
    # Print 'D'
    SET CHAR 84
    SETBIT FLAGS 0 0
    PRNT 0 0 0
    JMP START 0 0

: CHECK_WIN
    # Rock (1) beats Scissors (3)
    # Paper (2) beats Rock (1)
    # Scissors (3) beats Paper (2)
    
    # Logic:
    # If (User == 1 AND Comp == 3) -> Win
    # If (User == 2 AND Comp == 1) -> Win
    # If (User == 3 AND Comp == 2) -> Win
    
    # Case 1: User Rock
    SET R2 1
    EQ R3 R0 R2
    JMPZ TRY_CASE_2 R3 0 # If not Rock, try next
    
    # User is Rock. Check if Comp is Scissors (3)
    SET R2 3
    EQ R3 R1 R2
    JMPZ LOSE R3 0 # If Comp not Scissors, then Comp is Paper (since Draw checked), so Lose.
    JMP WIN 0 0

: TRY_CASE_2
    # Case 2: User Paper
    SET R2 2
    EQ R3 R0 R2
    JMPZ TRY_CASE_3 R3 0
    
    # User is Paper. Check if Comp is Rock (1)
    SET R2 1
    EQ R3 R1 R2
    JMPZ LOSE R3 0
    JMP WIN 0 0

: TRY_CASE_3
    # Case 3: User Scissors
    # We know User is Scissors because 1 and 2 checked.
    # Check if Comp is Paper (2)
    SET R2 2
    EQ R3 R1 R2
    JMPZ LOSE R3 0
    JMP WIN 0 0

: WIN
    # Print 'W'
    SET CHAR 87
    SETBIT FLAGS 0 0
    PRNT 0 0 0
    JMP START 0 0

: LOSE
    # Print 'L'
    SET CHAR 76
    SETBIT FLAGS 0 0
    PRNT 0 0 0
    JMP START 0 0
