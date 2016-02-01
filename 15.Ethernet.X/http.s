; SUPPORT:
;  OP:GET Dec 18, 2007 (working)
;
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

.extern _tcp_tx_packet
.global _http_process

.global HTTP_HEADER

.data

HTTP_HTML:       .space 0
HTTP_StatusLine: .ascii "HTTP/1.1 200 OK\r\n"
HTTP_EntLine1:   .ascii "Content-Type: text/html\r\n"
HTTP_EntLine2:   .ascii "Content-Length: "
HTTP_BODYLEN:    .ascii "0000\r\n"
HTTP_BlankLine:  .ascii "\r\n"
HTTP_BODY:       .ascii "<html><head><title>dsPicWEB - Tim Wuu</title> </head><body>HELLO! - "
HTTP_BODY2:      .ascii "0000"
HTTP_BODY3:      .ascii "</body></html>\r\n"
HTTP_HTML_END:   .space 0

.section sec_httpbss, bss, near

HTTP_HEADER: .space 0
HTTP_DATA:  .space 0x80     ;512bytes
HTTP_HEADER_END: .space 0
HTTP_LENGTH:   .space 2
HTTP_CHECKSUM: .space 2

HTTP_VISITSEQ:   .space 2

.section sec_HTTP, code ;.text

;input w0: data size
_http_process:

    mov w0, HTTP_LENGTH

    mov #(HTTP_HEADER), w0        
    ;mov HTTP_LENGTH, w1
    mov #0x04, w1
    call _enc_getarr
    

    mov #0x4547, w0    ;"GE"
    cp HTTP_HEADER
    bra NZ, 1f
    call _http_get     
    bra 0f
1:
    return    
    call _http_payload
    
0:

return


_http_payload:
       
;prepare data
    clr HTTP_LENGTH
    clr HTTP_CHECKSUM         
    
    mov #TCP_HTTP, w0
       
    push w0
    push HTTP_LENGTH 
    push HTTP_CHECKSUM        
    
    call _tcp_tx_packet
    
    sub #0x06, w15  ;pop * 3 times    

    push w0
    
;HTTP Layer
;    mov #HTTP_HTML, w0
;    mov HTTP_LENGTH, w1
;    call _enc_putarr    
;    
    pop w0

    ;add HTTP_LENGTH, wreg
    
    ;if this is the last protocol then flush data
        
    call _enc_end_tx_session
    
return


_http_get:
       
;prepare data

    mov #(HTTP_HTML_END-HTTP_BODY), w0
    mov #HTTP_BODYLEN, w2
    call _lcd_itoa_d
    
    inc HTTP_VISITSEQ
    mov HTTP_VISITSEQ, w0
    mov #HTTP_BODY2, w2
    call _lcd_itoa
    
    mov #(HTTP_HTML_END-HTTP_HTML), w0
    mov wreg, HTTP_LENGTH
    
    mov #HTTP_HTML, w1
    mov HTTP_LENGTH, w2    
    call _tcpip_checksum
    
    mov wreg, HTTP_CHECKSUM         
    
    mov #TCP_HTTP, w0
       
    push w0
    push HTTP_LENGTH 
    push HTTP_CHECKSUM        
    
    call _tcp_tx_packet
    
    sub #0x06, w15  ;pop * 3 times
    
    push w0
    
;HTTP Layer
    mov #HTTP_HTML, w0
    mov HTTP_LENGTH, w1
    call _enc_putarr    
    
    pop w0

    add HTTP_LENGTH, wreg
    
    ;if this is the last protocol then flush data
        
    call _enc_end_tx_session
    
return
.end
