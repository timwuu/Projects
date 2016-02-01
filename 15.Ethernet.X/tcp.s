; SUPPORT:
;  OP:0 TCP_SYN/ACK Dec 12, 2007 (working)
;  OP:8 TCP_SYN Dec 12, 2007 (working)
;
;
        ;.equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "memory.inc"
.include "tcpip.inc"
.include "lcd.inc"
.include "tcp.inc"

.include "socket.inc"

.extern _ip_tx_packet
.global _tcp_process
.global _tcp_tx_packet

.global TCP_HEADER
.global TCP_SRC_PORT

.section sec_tcpbss, bss, near

psTCP_HEADER:  .space 0
psTCP_SRC_IP:  .space 4
psTCP_DST_IP:  .space 4
psTCP_PADDING: .space 1
psTCP_PROTOCOL:.space 1
psTCP_DATLEN:  .space 2
TCP_HEADER: .space 0
TCP_SRC_PORT:  .space 2
TCP_DST_PORT:  .space 2
TCP_SEQNUM:    .space 4
TCP_ACKNUM:    .space 4
TCP_OFFSET:    .space 1
TCP_FLAG:      .space 1
TCP_WINDOW:    .space 2
TCP_CHECKSUM:  .space 2
TCP_URGENT_PT: .space 2
TCP_HEADER_END: .space 0
TCP_OPTIONS:  .space 0x28       ;40 bytes

TCP_LENGTH:  .space 2
TCP_TCB:     .space 2

.section sec_TCP, code ;.text

;input w0: data size
_tcp_process:

    mov w0, TCP_LENGTH     
    ;call _display_word

    mov #(TCP_HEADER), w0        
    mov #(TCP_HEADER_END-TCP_HEADER), w1
    call _enc_getarr
    
    mov.b TCP_OFFSET, wreg
    and #0x00F0,w0
    lsr w0,#0x02,w1
    
    ;read remaining header
    sub w1, #(TCP_HEADER_END-TCP_HEADER), w1
    bra Z, 1f
    mov #(TCP_OPTIONS), w0        
    call _enc_getarr

1:
    call _sck_register
    
    call _tcp_transit
     
    bra 0f
1:

    
0:

return

;input: w0: socket number
_tcp_transit:

    mul.uu w0, #_tcpscSIZEOF, w2   ;
    mov #TCP_scBUFFER,w0
    add w2,w0,w2
    
    mov w2, TCP_TCB     ;store the TCB buffer address

    mov [w2+#(_tcpscSTATE)], w0

    cp w0, #TCP_sCLOSED
    bra NZ, 1f
    call _tcp_trn_closed  
    bra 0f
1:  cp w0, #TCP_sLISTEN
    bra NZ, 1f  
    call _tcp_trn_listen    
    bra 0f
1:  cp w0, #TCP_sSYN_RCVD
    bra NZ, 1f  
    call _tcp_trn_syn_rcvd   
    bra 0f
1:  cp w0, #TCP_sEST
    bra NZ, 1f  
    call _tcp_trn_est   
    bra 0f
1:    
0:
return

_tcp_trn_closed:
_tcp_trn_listen:
;{
    mov w2, w0
    call _display_word
    
    btss TCP_FLAG, #TCP_fSYN
    bra 0f
    mov #TCP_sSYN_RCVD, w0
    mov w0, [w2+#(_tcpscSTATE)]   ;LISTEN - [SYN/SYN+ACK] - SYN_RCVD
       
    add w2, #_tcpscACKNUM, w4
    mov.D [w4], w0
    swap w1
    inc w1, w1
    swap w1
    mov.D w0,[w4]
 
    
    mov #0xFFFF, w0
    mov w0, [w2+#_tcpscTIMEOUT]

    ;prepare data  -- pseudo header --
    
    mov IP_TGT_ADDR, w0
    mov w0, psTCP_SRC_IP
    mov IP_TGT_ADDR+2, w0
    mov w0, psTCP_SRC_IP+2

    mov IP_SRC_ADDR, w0
    mov w0, psTCP_DST_IP
    mov IP_SRC_ADDR+2, w0
    mov w0, psTCP_DST_IP+2
    
    clr.b psTCP_PADDING
    
    mov.b #IP_TCP, w0
    mov.b wreg, psTCP_PROTOCOL
    
    mov TCP_LENGTH, w0
    swap w0
    mov w0, psTCP_DATLEN

    ;-----------------------------
    
    mov [w2+#(_tcpscSRC_PORT)], w0
    mov w0, TCP_SRC_PORT

    mov [w2+#(_tcpscDST_PORT)], w0
    mov w0, TCP_DST_PORT

    add w2, #_tcpscSEQNUM, w4
    mov.D [w4], w0
    mov #0x100, w0
    
    mov #0x100, w0
    clr w1
    mov w0, TCP_SEQNUM
    mov w1, TCP_SEQNUM+2

    add w2, #_tcpscACKNUM, w4
    mov.D [w4], w0
    mov w0, TCP_ACKNUM
    mov w1, TCP_ACKNUM+2

    ;mov #0x50, w0           ; 5*4 = 20 bytes
    ;mov.b wreg, TCP_OFFSET

    ; SYN+ACK
    mov.b #((1<<TCP_fSYN)|(1<<TCP_fACK)), w0
    mov.b wreg, TCP_FLAG

    mov #TCP_WINDOW_SIZE, w0
    mov w0, TCP_WINDOW

    clr TCP_CHECKSUM
    
    clr TCP_URGENT_PT
        
    mov #psTCP_HEADER, w1
;    ;mov #(TCP_HEADER_END-psTCP_HEADER), w2
    mov TCP_LENGTH, w2
    add w2,#0x0C,w2
    call _tcpip_checksum
    ;call _tcp_checksum

    mov w0, TCP_CHECKSUM
    
    mov #IP_TCP, w0     ;IP Protocal
    call _ip_tx_packet

    push w0
    
;IP Layer
    mov #TCP_HEADER, w0
    mov TCP_LENGTH, w1
    call _enc_putarr    
    
    pop w0

    ;add #0x14, w0
    add TCP_LENGTH, wreg
    
    ;if this is the last protocol then flush data
        
    call _enc_end_tx_session
           
0:        
return
;}
_tcp_trn_syn_rcvd:
    btss TCP_FLAG, #TCP_fACK
    bra 0f
    mov #TCP_sEST, w0
    mov w0, [w2+#(_tcpscSTATE)]   ;SYN_RCVD - [ACK] - ESTABLISHED
    
    ;prepare data  -- pseudo header --           
0:        
return


_tcp_trn_est:

;----------- RST ---------------
    btss TCP_FLAG, #TCP_fRST
    bra 1f

    clr w0
    mov w0, [w2+#_tcpscTIMEOUT]  ; release socket
    bra 0f

;----------- FIN --------------
1:  btss TCP_FLAG, #TCP_fFIN
    bra 1f
    
    call _tcp_tx_fin_ack_packet
        
    call _enc_end_tx_session
    
    mov #TCP_sLAST_ACK, w0
    mov w0, [w2+#(_tcpscSTATE)]   ;ESTABLISHED - [FIN+ACK] - LAST_ACK    
    
    clr w0
    mov w0, [w2+#_tcpscTIMEOUT]  ; release socket

    bra 0f
    
;----------- ACK --------------    
1:  btss TCP_FLAG, #TCP_fACK
    bra 1f
    
    ;calc TCP header length
    mov.b TCP_OFFSET, wreg
    and #0x00F0,w0
    lsr w0,#0x02,w0
    
    sub TCP_LENGTH, wreg   ; w0= TCP_LENGTH-w0
        
    push w5    
    mov w0, w5    
    add w2, #_tcpscACKNUM, w4
    mov.D [w4], w0
    swap w1
    add w1, w5, w1
    swap w1
    mov.D w0,[w4]
        
    mov w5,w0    
    pop w5
    
    mov TCP_ACKNUM, w1
    mov w1, [w2+#_tcpscSEQNUM]
    mov TCP_ACKNUM+2, w1
    mov w1, [w2+#_tcpscSEQNUM+2]
        
    call _http_process    
    bra 0f
    
    
    
1:
0:
return

;[w15-#0x0A]: child protocol
;[w15-#0x08]: data length
;[w15-#0x06]: data checksum
_tcp_tx_packet:
    
    mov IP_TGT_ADDR, w0
    mov w0, psTCP_SRC_IP
    mov IP_TGT_ADDR+2, w0
    mov w0, psTCP_SRC_IP+2

    mov IP_SRC_ADDR, w0
    mov w0, psTCP_DST_IP
    mov IP_SRC_ADDR+2, w0
    mov w0, psTCP_DST_IP+2
    
    clr.b psTCP_PADDING
    
    mov.b #IP_TCP, w0
    mov.b wreg, psTCP_PROTOCOL
    
    mov [w15-#0x08], w1                         ;data length
    add w1, #(TCP_HEADER_END-TCP_HEADER), w1    ;header length
    add w1, #0x14, w0
    swap w1
    mov w1, psTCP_DATLEN
    swap w0
    mov wreg, IP_LENGTH


    ;-----------------------------

    mov TCP_TCB, w2    
    
    mov [w2+#(_tcpscSRC_PORT)], w0
    mov w0, TCP_SRC_PORT

    mov [w2+#(_tcpscDST_PORT)], w0
    mov w0, TCP_DST_PORT

    add w2, #_tcpscSEQNUM, w4
    mov.D [w4], w0
    mov w0, TCP_SEQNUM
    mov w1, TCP_SEQNUM+2

    add w2, #_tcpscACKNUM, w4
    mov.D [w4], w0
    mov w0, TCP_ACKNUM
    mov w1, TCP_ACKNUM+2

    mov #0x50, w0           ; 5*4 = 20 bytes, no options data
    mov.b wreg, TCP_OFFSET

    ; ACK
    mov.b #(1<<TCP_fACK), w0
    mov.b wreg, TCP_FLAG

    mov #TCP_WINDOW_SIZE, w0
    mov w0, TCP_WINDOW

    clr TCP_CHECKSUM
    
    clr TCP_URGENT_PT
        
    mov #psTCP_HEADER, w1
    mov #(TCP_HEADER_END-psTCP_HEADER), w2
    call _tcpip_checksum

    com w0,w0
    mov [w15-#0x06],w1
    com w1,w1
    
    add w0,w1,w0
    addc w0,#0x00,w0
    com w0,w0

    mov w0, TCP_CHECKSUM
    
    mov #IP_TCP, w0     ;IP Protocal
    call _ip_tx_packet

    push w0
    
;IP Layer
    mov #TCP_HEADER, w0
    mov #(TCP_HEADER_END-TCP_HEADER), w1
    call _enc_putarr    
    
    pop w0

    ;add #0x14, w0
    add #0x14, w0
    
    ;if this is the last protocol then flush data
        
    ;call _enc_end_tx_session
           
0:        
return


_tcp_tx_fin_ack_packet:
    
    mov IP_TGT_ADDR, w0
    mov w0, psTCP_SRC_IP
    mov IP_TGT_ADDR+2, w0
    mov w0, psTCP_SRC_IP+2

    mov IP_SRC_ADDR, w0
    mov w0, psTCP_DST_IP
    mov IP_SRC_ADDR+2, w0
    mov w0, psTCP_DST_IP+2
    
    clr.b psTCP_PADDING
    
    mov.b #IP_TCP, w0
    mov.b wreg, psTCP_PROTOCOL
    
    ;mov [w15-#0x08], w1                         ;data length
    clr w1
    add w1, #(TCP_HEADER_END-TCP_HEADER), w1    ;header length
    add w1, #0x14, w0
    swap w1
    mov w1, psTCP_DATLEN
    swap w0
    mov wreg, IP_LENGTH


    ;-----------------------------

    mov TCP_TCB, w2    
    
    mov [w2+#(_tcpscSRC_PORT)], w0
    mov w0, TCP_SRC_PORT

    mov [w2+#(_tcpscDST_PORT)], w0
    mov w0, TCP_DST_PORT

    add w2, #_tcpscSEQNUM, w4
    mov.D [w4], w0
    mov w0, TCP_SEQNUM
    mov w1, TCP_SEQNUM+2

    add w2, #_tcpscACKNUM, w4
    mov.D [w4], w0
    mov w0, TCP_ACKNUM
    mov w1, TCP_ACKNUM+2

    mov #0x50, w0           ; 5*4 = 20 bytes, no options data
    mov.b wreg, TCP_OFFSET

    ; ACK
    mov.b #(1<<TCP_fFIN|1<<TCP_fACK), w0
    mov.b wreg, TCP_FLAG

    mov #TCP_WINDOW_SIZE, w0
    mov w0, TCP_WINDOW

    clr TCP_CHECKSUM
    
    clr TCP_URGENT_PT
        
    mov #psTCP_HEADER, w1
    mov #(TCP_HEADER_END-psTCP_HEADER), w2
    call _tcpip_checksum

    mov w0, TCP_CHECKSUM
    
    mov #IP_TCP, w0     ;IP Protocal
    call _ip_tx_packet

    push w0
    
;IP Layer
    mov #TCP_HEADER, w0
    mov #(TCP_HEADER_END-TCP_HEADER), w1
    call _enc_putarr    
    
    pop w0

    ;add #0x14, w0
    add #0x14, w0
    
    ;if this is the last protocol then flush data
        
    ;call _enc_end_tx_session
           
0:        
return

.end
