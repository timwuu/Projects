; SUPPORT:
;  OP:0 ICMP_ECHOREPLY Dec 12, 2007 (working)
;  OP:8 ICMP_ECHO Dec 12, 2007 (working)
;
;
        ;.equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "memory.inc"
.include "tcpip.inc"
.include "lcd.inc"

.extern _ip_tx_packet

.global _icmp_process

.global ICMP_HEADER

.equiv ICMP_ECHOREPLY,  0x00
.equiv ICMP_ECHO,       0x08

.section sec_icmpbss, bss, near

ICMP_HEADER: .space 0
ICMP_TYPE_CODE:    .space 2
ICMP_CHECKSUM:   .space 2
ICMP_ID:    .space 2
ICMP_SEQNUM: .space 2           ;1:Request, 2:Reply
ICMP_HEADER_END: .space 0
ICMP_BUFFER:  .space 0x40       ;64 bytes

ICMP_LENGTH:  .space 2

.section sec_ICMP, code ;.text

;input w0: data size
_icmp_process:

;    clr ICMP_BUFFER
;    clr ICMP_BUFFER+2
;    clr ICMP_BUFFER+4
;    clr ICMP_BUFFER+6
;    clr ICMP_BUFFER+8
;    clr ICMP_BUFFER+10
;    clr ICMP_BUFFER+12
;    clr ICMP_BUFFER+14


    mov w0, ICMP_LENGTH     
    mov w0, w1
    mov #(ICMP_HEADER), w0        
    call _enc_getarr
    
;---- if this is the last protocol then release current rx buffer
    ;call _enc_end_rx_session    
             
    mov ICMP_TYPE_CODE, w0  ;PCKT[TYPE|CODE], W0:[H:CODE|L:TYPE]
    cp.b w0, #ICMP_ECHO
    bra NZ, 0f
    
    call _icmp_echoreply

0:

return


_icmp_echoreply:

    
    mov #ICMP_TYPE_CODE, w0
    mov #ICMP_ECHOREPLY, w1
    mov.b w1, [w0]

    clr ICMP_CHECKSUM
    mov #ICMP_HEADER, w1
    mov ICMP_LENGTH, w2
    ;call _icmp_checksum
    call _tcpip_checksum
    ;swap w0
    mov wreg, ICMP_CHECKSUM
       
    mov #IP_ICMP, w0     ;IP Protocal
    call _ip_tx_packet

    push w0

;    mov #ICMP_HEADER, w0
;    call _memdump16
    
;IP Layer
    mov #ICMP_HEADER, w0
    mov ICMP_LENGTH, w1
    call _enc_putarr    
    
    pop w0

    add ICMP_LENGTH, wreg
    
    ;if this is the last protocol then flush data
        
    call _enc_end_tx_session
    
return

.end
