/*
; ?2005  Microchip Technology Inc.
;
; Microchip Technology Inc. (Microchip? licenses this software to you
; solely for use with Microchip dsPIC?digital signal controller
; products.  The software is owned by Microchip and is protected under
; applicable copyright laws.  All rights reserved.
;
; SOFTWARE IS PROVIDED AS IS.? MICROCHIP EXPRESSLY DISCLAIMS ANY
; WARRANTY OF ANY KIND, WHETHER EXPRESS OR IMPLIED, INCLUDING BUT NOT
; LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
; PARTICULAR PURPOSE, OR NON-INFRINGEMENT. IN NO EVENT SHALL MICROCHIP
; BE LIABLE FOR ANY INCIDENTAL, SPECIAL, INDIRECT OR CONSEQUENTIAL
; DAMAGES, LOST PROFITS OR LOST DATA, HARM TO YOUR EQUIPMENT, COST OF
; PROCUREMENT OF SUBSTITUTE GOODS, TECHNOLOGY OR SERVICES, ANY CLAIMS
; BY THIRD PARTIES (INCLUDING BUT NOT LIMITED TO ANY DEFENSE THEREOF),
; ANY CLAIMS FOR INDEMNITY OR CONTRIBUTION, OR OTHER SIMILAR COSTS.
;
;REVISION HISTORY:
;  $Log: lcd.s,v $
;
*/
.include "xc.inc"

.global	_delayms
.global _delayus
.global _lcd_init
.global _lcd_wr_cmd_nibble
.global _lcd_wr_data_nibble
.global _lcd_wr_cmd
.global _lcd_wr_data
.global __AddressError
.global	__StackError

.global _lcd_clear_display
.global _lcd_line1
.global _lcd_line2

.global _display_word
.global _display_byte
.global _lcd_itoa
.global _lcd_itoa_d
 
    .equiv LCD_RS, RC13        
    .equiv LCD_E, RC14
    .equiv LCD_RW, RC15

.section sec_lcdbss, bss, near
LCD_STR: .space 4

.section sec_lcd, code

	
_lcd_init:
	push	w0
	
	setm 	ADPCFG

	mov.b	TRISB, WREG
	and.b	#0xF0, w0
	mov.b	WREG, TRISB
	nop
	mov.b	TRISCH, WREG
	and.b	#0x1F, w0
	mov.b	WREG, TRISCH
	nop	
		
	bclr	PORTB, #RB0
	nop
	bclr	PORTB, #RB1
	nop
	bclr	PORTB, #RB2
	nop
	bclr	PORTB, #RB3
	nop
	bclr	PORTC, #LCD_RW		;RW
	nop
	bclr	PORTC, #LCD_RS		;RS
	nop
	bclr	PORTC, #LCD_E		;E
	nop
	
	mov	#200, w0
	rcall	_delayms
	
	;------------- STC2E16DRG.Power On Initialization ------------
	mov	#0x3, w0
	rcall	_lcd_wr_cmd_nibble
	mov	#5, w0
	rcall	_delayms   ;more than 4.1ms required
		
	mov	#0x3, w0
	rcall	_lcd_wr_cmd_nibble
	mov	#100, w0
	rcall	_delayus   ;more than 100us required
	
	mov	#0x3, w0
	rcall	_lcd_wr_cmd_nibble
	mov	#100, w0
	rcall	_delayus   ;more than 100us required
	;--------------------------------------	
	
	mov	#0x2, w0
	rcall	_lcd_wr_cmd_nibble
	mov	#40, w0
	rcall	_delayus    ;4 bit mode
	
	mov	#0x28, w0
	rcall	_lcd_wr_cmd
	mov	#40, w0
	rcall	_delayus    ;4 bit, 2 lines, 5x7 dots

	mov	#0x08, w0
	rcall	_lcd_wr_cmd	
	mov	#40, w0
	rcall	_delayus    ;display off
	
	mov	#0x01, w0
	rcall	_lcd_wr_cmd	
	mov	#2, w0
	rcall	_delayms    ;clear display (1.64ms)

	mov	#0x06, w0
	rcall	_lcd_wr_cmd		
	mov	#40, w0
	rcall	_delayus    ;incremental, normal operation

	rcall	_lcd_line1  ;goto Line1

	mov	#0x0F, w0
	rcall	_lcd_wr_cmd	
	mov	#40, w0
	rcall	_delayus	;display on, cursor on, blink cursor
	
	pop	w0
	return

_lcd_clear_display:
	mov	#0x01, w0
	rcall	_lcd_wr_cmd	
	mov	#2, w0
	rcall	_delayms    ;clear display (1.64ms)
    return

_lcd_line1:

	mov	#0x80, w0
	rcall	_lcd_wr_cmd		
	mov	#100, w0
	rcall	_delayus    ;display address -> 0x00

    return
	
_lcd_line2:

	mov	#0xC0, w0
	rcall	_lcd_wr_cmd		
	mov	#100, w0
	rcall	_delayus    ;display address -> 0x00

    return


_lcd_wr_cmd_nibble:
	push	w1
	push.d	w2
	push	SR
	bclr	PORTC, #LCD_RS	
	nop
	bclr	PORTC, #LCD_RW
	bclr	SR, #C
	mov	#PORTB, w2
	mov	#0, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	mov	#1, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	mov	#2, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	mov	#3, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	nop	
	bset	PORTC, #LCD_E
	mov	#1, w0
	rcall	_delayus
	bclr	PORTC, #LCD_E

	pop	SR
	pop.d	w2
	pop	w1
	return

_lcd_wr_data_nibble:
	push	w1
	push.d	w2
	push	SR

	bset	PORTC, #LCD_RS
	nop	
	bclr	PORTC, #LCD_RW
	bclr	SR, #C
	mov	#PORTB, w2
	mov	#0, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	mov	#1, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	mov	#2, w3
	btst.c	w0, w3
	bsw.c	[w2], w3
	mov	#3, w3
	btst.c	w0, w3
	bsw.c	[w2], w3	
	nop
	bset	PORTC, #LCD_E
	mov	#1, w0
	rcall	_delayus
	bclr	PORTC, #LCD_E
	pop	SR
	pop.d	w2
	pop	w1
	return	

_lcd_wr_data:
	push	w1
	and	w0, #0xF, w1
	lsr	w0, #4, w0
	rcall	_lcd_wr_data_nibble
	mov	w1, w0
	rcall	_lcd_wr_data_nibble
	mov	#40, w0
	rcall	_delayus
	pop	w1
	return

_lcd_wr_cmd:
	push	w1
	and	w0, #0xF, w1
	lsr	w0, #4, w0
	rcall	_lcd_wr_cmd_nibble
	mov	w1, w0
	rcall	_lcd_wr_cmd_nibble
	mov	#40, w0
	rcall	_delayus
	pop	w1
	return	

_lcdisBusy:
	push	w0
	bclr	PORTC, #LCD_RS
	nop
	bset	PORTC, #LCD_RW
	nop
 	bset	TRISB, #TRISB3           ;make TRISB3 input
 	nop
	bset	PORTC, #LCD_E
	nop
	btsc	PORTB, #RB3
	bra	$-2
	mov	#1, w0
	rcall	_delayus
	bclr	PORTC, #LCD_E
	nop
	bclr	TRISB, #TRISB3      ;make TRISB3 output
	nop
	pop	w0
	return

	
__AddressError:
	nop
	bra	$-2
	retfie

__StackError:
	nop
	bra	$-2
	retfie



_delayms:
	push	w1
	clr	T1CON
	clr	PR1
	clr	TMR1
	bclr	IFS0, #T1IF
;	mov	#32000, w1
	mov	#32000, w1
	mov	w1, PR1
	bset	T1CON, #TON
	btss	IFS0, #T1IF
	bra	$-2
	dec	w0, w0
	bra	nz, _delayms+2
	bclr	T1CON, #TON
	bclr	IFS0, #T1IF
	pop	w1
	return

_delayus:
	push	w1
	clr	T1CON
	clr	PR1
	clr	TMR1
	bclr	IFS0, #T1IF
;	mov	#10, w1
	mov	#10, w1
	mov	w1, PR1
	bset	T1CON, #TON
	btss	IFS0, #T1IF
	bra	$-2
	dec	w0, w0
	bra	nz, _delayus+2
	bclr	T1CON, #TON
	bclr	IFS0, #T1IF
	pop	w1
	return


_display_byte:

        push w0
        and #0x00F0,w0
        lsr w0,#0x04,w0
        cp  w0,#0x09
        bra LE, 1f
        add #('A'-'9'-1), w0      
1:      add #'0', w0     
        rCALL _lcd_wr_data          
        pop w0

        and #0x000F,w0
        cp  w0,#0x09
        bra LE, 1f
        add #('A'-'9'-1), w0      
1:      add #'0', w0     
        rCALL _lcd_wr_data
                
        RETURN
        
_display_word:
    push w2

    mov #LCD_STR, w2    
    call _lcd_itoa

    mov #LCD_STR, w2    
    do #0x03, 0f
    mov.b [w2++], w0
    call _lcd_wr_data
0:  nop    

    pop w2
        
    RETURN

; w0: integer
; w2: address
_lcd_itoa:

        push w1
        
        add w2, #0x03, w2
  
        do #0x03, 0f     
        mov w0, w1
        and #0x0F, w1
        cp w1, #0x09
        bra LE, 1f
        add #('A'-'9'-1), w1
1:      add #'0', w1
        mov.b w1, [w2--]
0:      lsr w0,#0x04,w0

        pop w1
                
        RETURN

; w0: integer ( less then 10,000 (0x2710))
; w2: address
_lcd_itoa_d:

    push w2

    mov #0x0A, w2   ;w2=10
    do #0x02, 1f
    repeat #17
    div.u w0, w2    ;w0:quotient, w1:remainder
1:  push w1    
    push w0
    
    mov [w15-#0x0A],w1  ; w2
    
    do #0x03, 1f
    pop w0
    add #'0', w0
1:  mov.b w0, [w1++]

    pop w2

return


.end
