; SUPPORT:
;  OP:1 IP_ICMP Dec 12, 2007 (working)
;
;
       ;.equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "enc.inc"
.include "memory.inc"
.include "tcpip.inc"
.include "ethernet.inc"

.LIST

.extern _lcd_wr_data
.extern _display_byte
.extern _display_word

.extern _lcd_line1
.extern _lcd_line2
.extern _lcd_clear_display

.extern _tx_packet
.extern _icmp_process

.global _ip_process
.global _ip_tx_packet

.global IP_HEADER
.global IP_SRC_ADDR
.global IP_TGT_ADDR
.global IP_LENGTH

.section sec_ipbss, bss, near

IP_HEADER: .space 0
IP_VER_HLEN:  .space 1
IP_TYPE_CODE: .space 1
IP_LENGTH:   .space 2
IP_ID:    .space 2
IP_F_OFFSET: .space 2
IP_TTLIVE:   .space 1
IP_NXT_PRTC: .space 1
IP_CHECKSUM: .space 2
IP_SRC_ADDR: .space 4
IP_TGT_ADDR: .space 4
IP_HEADER_END: .space 0

IP_OPTIONS:  .space 0x28       ;40 bytes

.section sec_IP, code ;.text

;store Header Size in W4
_ip_process:

    mov.b #'I', w0
    rCALL _lcd_wr_data

    push w4
    
    call _enc_getchar
    mov.b wreg, IP_VER_HLEN
    and #0x0F, w0
    sl w0, #0x02, w1

    mov w1, w4
    dec w1, w1
    ;mov #0x14, w4
    mov #(IP_HEADER+1), w0
    call _enc_getarr
    
   ; mov #IP_HEADER, w0
   ; call _memdump16

    ; w1: Next Protocol Type
    mov.b IP_NXT_PRTC, wreg ; packet:[TTL|PRTC], w0:[H:PRTC|L:TTL]
    mov w0, w1

   ; w0: Next Protocol Data Length
    mov IP_LENGTH, w0
    swap w0
    sub w0, w4, w0
    
    cp.b w1, #IP_ICMP
    bra NZ, 0f
    call _icmp_process    
    bra 1f
    
0:  cp.b w1, #IP_TCP
    bra NZ, 0f
    call _tcp_process
    bra 1f
    
    
0:
1:
    pop w4
return

; W0: IP PROTOCAL OPCODE
_ip_tx_packet:

;prepare data

    ;mov.b WREG, IP_TTLIVE_PRTC+1  ; packet:[TTL|PRTC], w0:[PRTC]

    mov #IP_SRC_ADDR, w0
    mov #IP_TGT_ADDR, w1
    call _memswip4    
    
;calc checksum    
    clr IP_CHECKSUM
    mov #IP_HEADER, w1
    mov #0x14, w2
    call _tcpip_checksum
    mov w0, IP_CHECKSUM

    mov #ETHERTYPE_IP, w0
    call _eth_tx_packet

    push w0

;    mov #IP_HEADER, w0
;    call _memdump16
    
;IP Layer
    mov #IP_HEADER, w0
    mov #(IP_HEADER_END-IP_HEADER), w1
    call _enc_putarr    
    
    pop w0

    add #(IP_HEADER_END-IP_HEADER), w0

return

.end
