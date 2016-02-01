; SUPPORT:
;  OP:2 REPLY Dec 12, 2007
;
;
;        .equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "memory.inc"
.include "enc.inc"
.include "tcpip.inc"

.extern _lcd_wr_data
.extern _display_byte
.extern _display_word

.extern _lcd_line1
.extern _lcd_line2
.extern _lcd_clear_display

.global _arp_process
.global _arp_request

.global ARP_HEADER

.section sec_arpbss, bss, near

ARP_HEADER: .space 0
ARP_HD_TYPE:    .space 2
ARP_PROTOCAL:   .space 2
ARP_ADDRESS_LEN:    .space 2
ARP_OPCODE: .space 2           ;1:Request, 2:Reply
ARP_SDR_MAC: .space 6
ARP_SDR_IP4: .space 4
ARP_TGT_MAC: .space 6
ARP_TGT_IP4: .space 4
ARP_HEADER_END: .space 0

ARP_BUFFER:  .space 4

.section sec_ARP, code ;.text

;_arp_request:
;   mov #0x0100, w0
;   mov w0, ARP_HD_TYPE
;   mov #0x0008, w0
;   mov w0, ARP_PROTOCAL
;   mov #0x0406, w0
;   mov w0, ARP_ADDRESS_LEN
;   mov #0x0100, w0
;   mov w0, ARP_OPCODE
;   
;   mov #ARP_SDR_MAC, w0
;   call _memcpmac
;
;   mov #0xFEA9, w0
;   mov w0, ARP_SDR_IP4
;   mov #0x6929, w0
;   mov w0, ARP_SDR_IP4+2
;
;    clr ARP_TGT_MAC
;    clr ARP_TGT_MAC+2
;    clr ARP_TGT_MAC+4
;
;   mov #0xFEA9, w0
;   mov w0, ARP_TGT_IP4
;   mov #0x6829, w0
;   mov w0, ARP_TGT_IP4+2
;
;    mov #ARP_HEADER, w0
;    mov #0x1C, w1
;    mov #0x0608, w2
;    call _tx_packet
;
;return

_arp_process:

    mov.b #'A', w0
    rCALL _lcd_wr_data

    mov #ARP_HEADER, w0        
    mov #(ARP_HEADER_END-ARP_HEADER), w1    
    call _enc_getarr
    
;---- if this is the last protocol then release current rx buffer
    ;call _enc_end_rx_session    
        
    mov #0x0100, w0
    cp ARP_OPCODE             ;request for MAC with IP
    bra NZ, 1f
    
    ;filtering: compare target IP address
    mov #IP_LOW, w0
    cp ARP_TGT_IP4
    bra NZ, 1f
    
    mov #IP_HIGH, w0
    cp ARP_TGT_IP4+2
    bra NZ, 1f
        
    call _arp_response    

1:   
return


_arp_response:

; prepare packet
    mov #0x0200, w0
    mov w0, ARP_OPCODE             ;reply for MAC with IP

   ;set MAC Address
    mov #ARP_SDR_MAC, w0
    mov #ARP_TGT_MAC, w1
    
    repeat #0x02
    mov [w0++],[w1++]

    mov #ARP_SDR_MAC, w0
    call _memcpmac
    
    ;set IP address
    
    mov #ARP_TGT_IP4, w0
    mov #ARP_SDR_IP4, w1
    call _memswip4    

; call parent protocol to request start_tx_session

    mov #ETHERTYPE_ARP, w0
    call _eth_tx_packet

    push w0

; buffering data

    mov #ARP_HEADER, w0        
    mov #(ARP_HEADER_END - ARP_HEADER), w1
    call _enc_putarr

    pop w0
    
    add #(ARP_HEADER_END - ARP_HEADER), w0
    
    ;if this is the last protocol then flush data
        
    call _enc_end_tx_session
    
return

.end
