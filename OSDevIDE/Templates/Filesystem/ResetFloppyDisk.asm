.Reset:
	mov		ah, 0					; reset floppy disk function
	mov		dl, 0					; drive 0 is floppy drive
	int		0x13					; call BIOS
	jc		.Reset					; If Carry Flag (CF) is set, there was an error. Try resetting again
 
	mov		ax, 0x1000				; we are going to read sector to into address 0x1000:0
	mov		es, ax
	xor		bx, bx
 
	mov		ah, 0x02				; read floppy sector function
	mov		al, 1					; read 1 sector
	mov		ch, 1					; we are reading the second sector past us, so its still on track 1
	mov		cl, 2					; sector to read (The second sector)
	mov		dh, 0					; head number
	mov		dl, 0					; drive number. Remember Drive 0 is floppy drive.
	int		0x13					; call BIOS - Read the sector
	
 
	jmp		0x1000:0x0				; jump to execute the sector!