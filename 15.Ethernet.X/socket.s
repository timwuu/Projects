        .equ __30F2011, 1

.include "p30f2011.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "memory.inc"
.include "tcpip.inc"
.include "lcd.inc"

.global _sck_register
.global _tcpscSIZEOF
.global _tcpscSTATE
.global TCP_scHEADER
.global TCP_scSTATE

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

_sck_register:

    do #(socket_size-1), 0f
    
    
    nop
    nop
    nop
    
0:  nop

    clr w0
    
return


.end
