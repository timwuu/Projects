       ; .equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "enc.inc"
.include "memory.inc"
.include "tcpip.inc"

.global _eth_process
.global _eth_tx_packet
.global ETHER_HEADER

.section sec_ethernetbss, bss, near

ETHER_HEADER: .space 0
ETHER_DestMacAddr: .space 6
ETHER_SrcMacAddr: .space 6
ETHER_TYPE:   .space 2
ETHER_HEADER_END: .space 0


.section sec_ETHERNET, code ;.text


_eth_process:

    ;read ETHER Header
    mov #ETHER_HEADER, w0
    mov #(ETHER_HEADER_END-ETHER_HEADER), w1
    call _enc_getarr
    
    rcall _lcd_line2

;display ETHER_TYPE
;    mov ETHER_TYPE, w0 
;    rCALL _display_word
;    
;Process Ethernet Type    
    mov #ETHERTYPE_ARP, w0
    cp ETHER_TYPE
    bra NZ, 1f
    call _arp_process    
    bra 0f
        
1:  mov #ETHERTYPE_IP, w0
    cp ETHER_TYPE
    bra NZ, 1f
    call _ip_process    
    bra 0f

1: 
 ;call _enc_end_tx_session
0:
    return

; w0: ETHER_TYPE
_eth_tx_packet:

;prepare data
    mov w0, ETHER_TYPE
    
    mov #ETHER_SrcMacAddr, W0
    mov #ETHER_DestMacAddr, W1

    repeat #0x02    ;3 words
    mov [w0++],[w1++]
    
    ;set Src Mac Address
    mov #ETHER_SrcMacAddr, w0
    call _memcpmac
    
    call _enc_start_tx_session
    
    push w0  ;the accumulated count
 
;    mov #ETHER_HEADER, w0
;    call _memdump16

    ;Ethernet Layer
    mov #ETHER_HEADER, w0
    mov #(ETHER_HEADER_END-ETHER_HEADER), w1
    call _enc_putarr

    pop w0
    
    add #(ETHER_HEADER_END-ETHER_HEADER), w0
    
    ;RETURN to CHILD PROTOCOL
       ;packet length w0: does not include the Per Packet Control Byte(PPCP)
       ;USE _enc_putarr to WRITE TX buffer, accumulate byte count w0
       ;The bottom PROTOCOL will call _enc_end_tx_session to flush the data
 
return  

.end
