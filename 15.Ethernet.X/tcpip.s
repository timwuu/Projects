;.equ __30F2011, 1

.include "xc.inc"

.global _tcpip_checksum

.section sec_tcpip, code ;.text

; W0: RESULT, W1:START ADDRESS, W2:BYTE COUNT
; notes: checksum column should set to ZERO before calling
_tcpip_checksum:    

    clr w0    ;clear w0
    push w2
    lsr w2,#01,w2   ;div w2 by 2
    dec w2, w2

    bclr SR, #C ;clear carry flag
        
    repeat w2
    addc w0,[w1++],w0
    addc #0x00, w0
1:  pop w2   
    ;check odd/even
    btss w2, #0x00 ;if even, branch 1f
    bra 1f   
;if w2 is odd
    clr w2
    mov.b [w1],w2
    add w0,w2,w0
    addc #0x00, w0
1:  com w0,w0    
    
return

.end
