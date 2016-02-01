;        .equ __30F2011, 1
;
;.include "p30f2011.inc"
;.include "config.inc"
;.include "_enc28j60reg.inc"
;.include "memory.inc"
;.include "tcpip.inc"
;.include "lcd.inc"
;
;.global _sck_register
;.global _tcpscSIZEOF
;.global _tcpscSTATE
;.global TCP_scHEADER
;.global TCP_scSTATE

.global _sck_init

.equiv socket_size, 0x08

.section sec_socketbss, bss, near
;TCP_SOCKET info
TCP_scBUFFER:   .space 0
TCP_scHEADER:   .space 0
TCP_scREMOTE_IP:.space 4
TCP_scSTATE:    .space 2
TCP_scSRC_PORT: .space 2
TCP_scDST_PORT: .space 2
TCP_scSEQNUM:   .space 4
TCP_scACKNUM:   .space 4
TCP_scTIMEOUT:  .space 2
TCP_scHEADER_END:   .space 0
TCP_scBUFFERex:   .space (TCP_scHEADER_END-TCP_scHEADER)*(socket_size-1) ;8 buffers total

.equiv _tcpscREMOTE_IP, TCP_scREMOTE_IP-TCP_scHEADER
.equiv _tcpscSTATE, TCP_scSTATE-TCP_scHEADER
.equiv _tcpscSRC_PORT, TCP_scSRC_PORT-TCP_scHEADER
.equiv _tcpscDST_PORT, TCP_scDST_PORT-TCP_scHEADER
.equiv _tcpscSEQNUM, TCP_scSEQNUM-TCP_scHEADER
.equiv _tcpscACKNUM, TCP_scACKNUM-TCP_scHEADER
.equiv _tcpscTIMEOUT, TCP_scTIMEOUT-TCP_scHEADER

.equiv _tcpscSIZEOF, TCP_scHEADER_END - TCP_scHEADER

;
;    BYTE RetryCount;
;    TICK startTick;
;    TICK TimeOut;
;

.section sec_socket, code ;.text

;return the socket number
;create a socket if it is not binded
;remote IP is the key

_sck_init:

    mov #(TCP_scBUFFER+_tcpscREMOTE_IP), w0
    do #(socket_size-1), 0f
    clr [w0]
0:  add w0, #_tcpscSIZEOF, w0

    mov #(TCP_scBUFFER+_tcpscTIMEOUT), w0
    do #(socket_size-1), 0f
    clr [w0]
0:  add w0, #_tcpscSIZEOF, w0

return;

;Check Timeout Flag first
;User Remote_IP, SrcPort as the Key (need improved)
_sck_register:

    clr w1
    mov #TCP_scBUFFER, w2

    do #(socket_size-1), 0f

    mov [w2+#_tcpscTIMEOUT],w0    ;if timeout, then fail
    cp  w0, #0x00
    bra Z, 2f
    mov [w2+#_tcpscREMOTE_IP],w0
    cp  IP_SRC_ADDR
    bra NZ, 2f
    mov [w2+#_tcpscDST_PORT],w0
    cp  TCP_SRC_PORT
    bra Z, 1f
2:  add w2, #_tcpscSIZEOF, w2
0:  inc w1,w1

    ;no match, find an empty one
    clr w1
    mov #TCP_scBUFFER, w2

    do #(socket_size-1), 0f
    
    mov [w2+#_tcpscTIMEOUT],w0
    cp  w0, #0x00
    bra Z, 2f
    add w2, #_tcpscSIZEOF, w2
0:  inc w1,w1

    ;an empty socket available
2:
    mov #TCP_sCLOSED, w0
    mov w0, [w2+#_tcpscSTATE]
    mov IP_SRC_ADDR, w0
    mov w0, [w2+#_tcpscREMOTE_IP]
    mov TCP_DST_PORT, w0
    mov w0, [w2+#_tcpscSRC_PORT]
    mov TCP_SRC_PORT, w0
    mov w0, [w2+#_tcpscDST_PORT]
    ;mov TCP_SEQNUM, w0
    mov 0x100, w0
    mov w0, [w2+#_tcpscSEQNUM]
    mov 0x00, w0
    mov w0, [w2+#_tcpscSEQNUM+2]

    mov TCP_SEQNUM, w0
    mov w0, [w2+#_tcpscACKNUM]
    mov TCP_SEQNUM+2, w0
    mov w0, [w2+#_tcpscACKNUM+2]
    
    mov #0xFFFF, w0
    mov w0, [w2+#_tcpscTIMEOUT]

1:  mov w1, w0  
return
