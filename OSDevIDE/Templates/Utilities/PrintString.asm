; String Printing Routine
; Version $PrintStringVersion
; Added By OSIDE $OSIDEVersion
 
Print:
			lodsb					; load next byte of string from SI to AL
			or			al, al		; IS AL = 0?
			jz			PrintDone	; Jump if Zero (null terminator) to PrintDone
			mov			ah,	0eh		; Continue
			int			10h			; Display Character
			jmp			Print		; Repeat until null terminator found
PrintDone:
			ret						; Return