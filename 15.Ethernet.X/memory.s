
;.equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "lcd.inc"

.global _memswap
.global _memcopy
.global _memcpmac
.global _memcpip4
.global _memswip4
.global _memdump16
.global _memdump64

.global _mBUFFER

.section sec_buffer, bss, near

_mMACADDR: .space 6
_mIP4ADDR: .space 4
_mBUFFER: .space 0x40  ;64 bytes

.section sec_memory, code

; w0: dest address
_memcpmac:    
    mov #(mMAC_Addr0|mMAC_Addr1<<8),w1
    mov w1,[w0++]
    mov #(mMAC_Addr2|mMAC_Addr3<<8),w1
    mov w1,[w0++]
    mov #(mMAC_Addr4|mMAC_Addr5<<8),w1
    mov w1,[w0]
return

_memcpip4:
    mov _mIP4ADDR, w1
    repeat #0x01
    mov [w1++],[w0++]    
return

; swap [w0],[w1], by (w2+1) words
;
_memswap:
    push w3
    mov #_mBUFFER, w3

    push w0
    repeat w2
    mov [w0++],[w3++]    
    pop w0
    
    repeat w2
    mov [w1++],[w0++]
    
    repeat w2
    mov [--w3],[--w1]
    
    pop w3

return

; swap [w0],[w1], by 2 words
;
_memswip4:
    mov [w0], w2
    mov [w1],[w0++]
    mov w2,[w1++]
        
    mov [w0], w2
    mov [w1],[w0]
    mov w2,[w1]
return

; copy [w0] to [w1], by (w2+1) words
_memcopy:

    repeat w2
    mov [w0++],[w1++]

return

;64 bytes dump [w0] - [w0+63]
_memdump64:   
    push w5
    mov #0x04, w5
    
0:  call _memdump16

    dec w5, w5
    bra NZ, 0b

    pop w5
        
return    

;16 bytes dump [w0] - [w0+15]
_memdump16:   

    .equ _mem_display_delay, 0x800

    push w4

    mov w0, w4
        
0:  mov  #_mem_display_delay, w0
    rCALL _delayms

    rcall _lcd_clear_display
    rcall _lcd_line1
    
    do #0x03, _nextline
    mov [w4++], w0
    swap w0
    call _display_word

_nextline: nop
    rcall _lcd_line2

    do #0x03, _nextpage
    mov [w4++], w0
    swap w0
    call _display_word

_nextpage: nop

    mov  #_mem_display_delay, w0
    rCALL _delayms
    
    mov w4, w0
    
    pop w4
        
return    

.end
