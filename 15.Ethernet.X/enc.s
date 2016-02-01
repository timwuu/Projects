/*
;REVISION HISTORY:
;  $Log: lcd.s,v $
;
*/
        ;.equ __30F2011, 1

.include "xc.inc"
.include "config.inc"
.include "_enc28j60reg.inc"
.include "lcd.inc"
.include "tcpip.inc"
.include "ethernet.inc"
.include "memory.inc"

.include "enc_macro.inc"

.extern _arp_request
.extern _arp_process

.global	_enc_init
.global _enc_getarr
.global _enc_getchar
.global _enc_putarr
.global _enc_putchar

.global _enc_start_tx_session
.global _enc_end_tx_session
.global _enc_end_rx_session

.global _enc_show_tx_buffer
.global _enc_show_rx_buffer

.global _monitor_rx
.global _tx_packet

.LIST

;.equiv FULL_DUPLEX, 0x01
   
          .section sec_encbss, bss, near
ENCRevID:   .space 2
CurrPacketLocation:  .space 2
NextPacketLocation:  .space 2

ENC_HEADER: .space 0
ENC_NextPacketPointer:  .space 2
ENC_Status: .space 4
ENC_HEADER_END: .space 0

    .equiv NULL, 1

    .equiv LCD_RS, RC13        
    .equiv LCD_E, RC14
    .equiv ENC_SSL, RD0
    
    .equiv	WCR,  0x40	;Write Control Register command
    .equiv  BFS,  0x80 	;Bit Field Set command
    .equiv	BFC,  0xA0	;Bit Field Clear command
    .equiv	RCR,  0x00	;Read Control Register command
    .equiv  RBM,  0x3A	;Read Buffer Memory command
    .equiv	WBM,  0x7A  ;Write Buffer Memory command
    .equiv	SRC,  0xFF	;System Reset command does not use an address.  

    .equiv RAMSIZE, 0x2000
    .equiv RXSTART, 0x0000    ;Errata
    .equiv RXSTOP,  0x16FF
    .equiv TXSTART, 0x1700
    .equiv TXSTOP,  0x1FFF

.section sec_enc, code ;.text

	
_enc_init:
;{
    ;CKP=0, CKE=1, SMP=0
    mov #0x00, w0
    mov w0, SPI1STAT ;clear SPI
    mov #0x1F, w0
    ;1:1 Primary Prescale,1:1(0x1F)15MIPS - 1:2(0x1B) - 1:3(0x17)30MIPS
    mov w0, SPI1CON 
    bset SPI1CON, #CKE
    bset SPI1CON, #MSTEN
    bset SPI1STAT, #SPIEN   ;;Enable SPI
    
1:  ENC_RCR_ETH ESTAT
    btsc w0,#0x03    ;if <3>:0 , continue, (<3>:1 RESET related)
    bra  1b
    btss w0,#ESTAT_CLKRDY
    bra  1b
    
    mov w0, w1
    
    ENC_SRC

;Set NextPacketLocation
    mov #RXSTART, w0
    mov w0, NextPacketLocation

;set receive buffer start(ERXST)
    ENC_WCRL ERXSTL, RXSTART
    ENC_WCRH ERXSTH, RXSTART

;set receive buffer end(ERXND)
    ENC_WCRL ERXNDL, RXSTOP
    ENC_WCRH ERXNDH, RXSTOP
    
;set receive buffer read pointer(ERXRDPT) (L first)
    ENC_WCRL ERXRDPTL, RXSTOP
    ENC_WCRH ERXRDPTH, RXSTOP

;set transmit buffer start(ETXST)
    ENC_WCRL ETXSTL, TXSTART
    ENC_WCRH ETXSTH, TXSTART

;set transmit buffer stop(ETXND)
    ENC_WCRL ETXNDL, TXSTOP
    ENC_WCRH ETXNDH, TXSTOP

;set buffer write pointer(EWRPT)
    ENC_WCRL EWRPTL, TXSTART
    ENC_WCRH EWRPTH, TXSTART

;write a permanant per packet control byte of 0x00
    MACPUT 0x00
    
;enter bank2 and configure the MAC
    BANKSEL MACON1
    
    BITVALUE MACON1_TXPAUS, MACON1_RXPAUS, MACON1_MARXEN
    ENC_WCR MACON1

.ifdef FULL_DUPLEX
    BITVALUE MACON3_PADCFG0,MACON3_PADCFG1,MACON3_TXCRCEN,MACON3_FRMLEN,MACON3_FULDPX
    ENC_WCR MACON3

    ENC_WCR MABBIPG, 0x15
.else    
    BITVALUE MACON3_PADCFG0,MACON3_PADCFG1,MACON3_TXCRCEN,MACON3_FRMLEN  ;64B Padding
    ENC_WCR MACON3

    ENC_WCR MABBIPG, 0x12
.endif    

;Allow infinite deferals if the medium is continuously busy 
;(do not time out a transmission if the half duplex medium is 
;completely saturated with other people's data)    
    BITVALUE MACON4_DEFER
    ENC_WCR MACON4
    
    
; 	// Late collisions occur beyond 63+8 bytes (8 bytes for preamble/start of frame delimiter)
;	// 55 is all that is needed for IEEE 802.3, but ENC28J60 B5 errata for improper link pulse 
;	// collisions will occur less often with a larger number.
    ENC_WCR MACLCON2, 63
	
;	// Set non-back-to-back inter-packet gap to 9.6us.  The back-to-back 
;	// inter-packet gap (MABBIPG) is set by MACSetDuplex() which is called 
;	// later.
	ENC_WCR MAIPGL, 0x12
	ENC_WCR MAIPGH, 0x0C

;	// Set the maximum packet size which the controller will accept
;   0x5EE = (6+6+2+1500+4) = 1518 is the IEEE 802.3 specified limit
	ENC_WCRL MAMXFLL, 0x5EE;
	ENC_WCRH MAMXFLH, 0x5EE; 
   
    
;enter bank3 and configure the MAC address
    BANKSEL MAADR1
    
    ENC_WCR MAADR1, mMAC_Addr0
    ENC_WCR MAADR2, mMAC_Addr1
    ENC_WCR MAADR3, mMAC_Addr2
    ENC_WCR MAADR4, mMAC_Addr3
    ENC_WCR MAADR5, mMAC_Addr4
    ENC_WCR MAADR6, mMAC_Addr5
    
    ENC_RCR_MAC MAADR5     
    rCALL _display_byte
    
    mov.b #':', w0
    rCALL _lcd_wr_data

    ENC_RCR_MAC MAADR6    
    rCALL _display_byte
    
    ;disable CLKOUT output (EMI issue)
    ENC_WCR ECOCON, 0x00
    

    mov.b #'v', w0
    rCALL _lcd_wr_data

    ;get Ethernet Revision ID
    BANKSEL EREVID
    ENC_RCR_ETH EREVID

    mov w0, ENCRevID            
    rCALL _display_byte
          
    ;disable loopback
    BITVALUE PHCON2_HDLDIS
    ENC_WCR_PHY PHCON2    
    
    ;configure LEDA to display LINK status, LEDB to display TX/RX activity
    ENC_WCR_PHY PHLCON, 0x3472
        
.ifdef FULL_DUPLEX
    BITVALUE PHCON1_PDPXMD
	ENC_WCR_PHY PHCON1
.else
 .ifdef HALF_DUPLEX
    ENC_WCR_PHY PHCON1, 0x0000
 .endif
.endif 
    
    BANKSEL ERXFCON 
    BITVALUE ERXFCON_UCEN, ERXFCON_BCEN
    ENC_WCR ERXFCON
    
;clean RX buffer    
    BANKSEL EWRPTL
    
    ENC_WCRL EWRPTL, RXSTART
    ENC_WCRH EWRPTH, RXSTART

    ;Per Packet Control Byte
    
    do #0x1000, 1f
    clr w0;
    call _enc_putchar    
1:  NOP    
    
;return to bank0
    BANKSEL ERDPTL
        
;enable packet reception
    BITVALUE ECON1_RXEN
    ENC_BFS ECON1

    mov  #0x200, w0
    rCALL _delayms

    return
;}  
    
_monitor_rx:
;{    
    mov #0x2, w0
    call _delayms
    rcall _lcd_line1
        
    ;Get Packet Count    
    BANKSEL EPKTCNT
    ENC_RCR_ETH EPKTCNT
    push w0
    rCALL _display_byte    
    pop w0
    
    cp w0, #0x01
    bra LT, _monitor_rx     ;no packet

;    call _enc_show_rx_buffer

    call _enc_start_rx_session

    call _enc_end_rx_session

    bra _monitor_rx 

return
;}

_enc_start_rx_session:
    
    BANKSEL ERDPTL
    mov NextPacketLocation, w0
    mov w0, CurrPacketLocation
    ENC_WCR_W ERDPTL, w0    
    mov CurrPacketLocation, w0
    swap w0
    ENC_WCR_W ERDPTH, w0    
    
        
    ;ENC_GETARR ENC_NextPacketPointer, 0x14 
 
    ;read ENC Header
    mov #ENC_HEADER, w0
    mov #(ENC_HEADER_END-ENC_HEADER), w1
    call _enc_getarr

    mov ENC_NextPacketPointer, w0
    mov w0, NextPacketLocation

;    cp w0, #0x00
;    bra NZ, 1f
;    
    
    ;mov #ENC_HEADER, w0
    ;call _memdump16

1:
    ;Show Current Packet Location
    mov.b #':', w0
    rCALL _lcd_wr_data    
    mov CurrPacketLocation, w0
    rCALL _display_word
    
    ;Show Status Vector
    mov.b #':', w0
    rCALL _lcd_wr_data    
    mov ENC_Status, w0
    rCALL _display_word
    mov ENC_Status+2, w0
    rCALL _display_word

    call _eth_process
    
return

_enc_end_rx_session:
    ;After everything is done, Release current RX buffer
    ;Set ERXRDPT (L first)    
    mov ENC_NextPacketPointer, w0
    ENC_WCR_W ERXRDPTL, w0
    mov ENC_NextPacketPointer, w0
    swap w0
    ENC_WCR_W ERXRDPTH, w0

    ;Set ECON2.PKTDEC
    BITVALUE ECON2_PKTDEC
    ENC_BFS ECON2
return

_enc_show_rx_buffer:

    BANKSEL ERDPTL
    ENC_WCRL ERDPTL RXSTART
    ENC_WCRH ERDPTH RXSTART

    mov #_mBUFFER, w0
    mov #0x60, w1
    call _enc_getarr    

    mov #_mBUFFER, w0
    call _memdump64
     
return
    
_enc_show_tx_buffer:

    BANKSEL ERDPTL
    ENC_WCRL ERDPTL TXSTART
    ENC_WCRH ERDPTH TXSTART

    mov #_mBUFFER, w0
    mov #0x60, w1
    call _enc_getarr    

    mov #_mBUFFER, w0
    call _memdump64
     
return


;w0: dest_address, w1: count
_enc_putarr:

    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF

    mov #WBM, w2
    mov w2, SPI1BUF    ;send command
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w2
    bclr IFS0, #SPI1IF

2:  dec w1,w1
    bra N, 1f

    mov.b [w0++], w2
    mov w2, SPI1BUF    ;send data byte
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w2
    bclr IFS0, #SPI1IF
    
    bra 2b
  
1:  bset PORTD, #ENC_SSL

    return
    
;return w0: char    
_enc_getchar:

    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF

    mov #RBM, w0
    mov w0, SPI1BUF    ;send command
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w0
    bclr IFS0, #SPI1IF

    mov w0, SPI1BUF    ;send dummy byte
0:
    btss IFS0, #SPI1IF
    goto 0b
    bset PORTD, #ENC_SSL

    mov SPI1BUF, w0
    bclr IFS0, #SPI1IF
      
    ;bset PORTD, #ENC_SSL

return    
    
;w0: dest_address, w1: count
_enc_getarr:

    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF

    mov #RBM, w2
    mov w2, SPI1BUF    ;send command
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w2
    bclr IFS0, #SPI1IF

2:  dec w1,w1
    bra N, 1f

    mov w0, SPI1BUF    ;send dummy byte
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w2
    mov.b w2, [w0++]
    bclr IFS0, #SPI1IF
    
    bra 2b
  
1:  bset PORTD, #ENC_SSL

    return
    
_banksel:
    push w0
    BITVALUE ECON1_BSEL1,ECON1_BSEL0
    ENC_BFC ECON1
    pop w0
    ENC_BFS_W ECON1, w0
    return
    
;w0: data byte
_enc_putchar:
_mac_put:
    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF

    mov #WBM, w1
    mov w1, SPI1BUF    ;send address
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w1    
    bclr IFS0, #SPI1IF

    mov w0, SPI1BUF     ;send data
1:
    btss IFS0, #SPI1IF
    goto 1b
    mov SPI1BUF, w0
    bclr IFS0, #SPI1IF
       
    bset PORTD, #ENC_SSL

    return    
    

;_enc_rcr_eth & _write_reg share same codes    
;input w0: reg address {, w1: data}
;ouput {w0: reg data}
_enc_rcr_eth:
_write_reg:
    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF

    mov w0, SPI1BUF    ;wait
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w0    
    bclr IFS0, #SPI1IF

    mov w1, SPI1BUF     ;send dummy
1:
    btss IFS0, #SPI1IF
    goto 1b
    mov SPI1BUF, w0
    bclr IFS0, #SPI1IF
       
    bset PORTD, #ENC_SSL
    
    return 
    
_enc_rcr_mac:
    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF

    mov w0, SPI1BUF    ;wait
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w0    
    bclr IFS0, #SPI1IF

    mov w0, SPI1BUF     ;send dummy
1:
    btss IFS0, #SPI1IF
    goto 1b
    mov SPI1BUF, w0     ;read dummy
    bclr IFS0, #SPI1IF
       
    mov w0, SPI1BUF     ;send dummy
1:
    btss IFS0, #SPI1IF
    goto 1b
    mov SPI1BUF, w0
    bclr IFS0, #SPI1IF
    bset PORTD, #ENC_SSL
    
    return 
    
_enc_rcr_phy:

    push w0
    BANKSEL MIREGADR
    pop w0
    
    ENC_WCR_W MIREGADR w0
    
    BITVALUE MICMD_MIIRD
    ENC_WCR MICMD
    
    BANKSEL MISTAT
1:  ENC_RCR_MAC MISTAT
    btsc w0, #MISTAT_BUSY
    bra 1b    
    
    BANKSEL MIREGADR
    
    ENC_WCR MICMD 0x00

    ENC_RCR_MAC MIRDL
    push w0
    ENC_RCR_MAC MIRDH
    pop w1
    sl w0,#8,w0
    ior w0, w1, w2

    BANKSEL ERDPTL

    mov w2, w0
    
    return
    
_enc_wcr_phy:

    push w1
    push w1
    push w0
    BANKSEL MIREGADR
    pop w0
    
    ENC_WCR_W MIREGADR w0
        
    pop w0
    
    ENC_WCR_W MIWRL w0
    
    pop w0
    swap w0
    
    ENC_WCR_W MIWRH w0
    
    BANKSEL MISTAT
1:  ENC_RCR_MAC MISTAT
    btsc w0, #MISTAT_BUSY
    bra 1b 

    BANKSEL ERDPTL
    
    return
    
       
_sendSystemReset:
;{
    bclr PORTD, #ENC_SSL
    bclr IFS0, #SPI1IF
    
    mov #SRC, w0
    mov w0, SPI1BUF    ;wait
0:
    btss IFS0, #SPI1IF
    goto 0b
    mov SPI1BUF, w0    
    bclr IFS0, #SPI1IF
       
    bset PORTD, #ENC_SSL
    
   	mov	#2, w0
	rcall	_delayms

    return
;}

_enc_start_tx_session:
;{
    ;Set WritePointer
    BANKSEL EWRPTL
    
    ENC_WCRL EWRPTL, TXSTART
    ENC_WCRH EWRPTH, TXSTART

    ;Per Packet Control Byte
    clr w0;
    call _enc_putchar      
    
    clr w0  ;start the count  

return
;}

; input w0: total byte
_enc_end_tx_session:
;{
    push w4

    mov #TXSTART, w4
    add w4, w0, w4     ; w4 = #TXSTART + w0

    BANKSEL ETXSTL
    
    ;set transmit buffer start(ETXST)
    ENC_WCRL ETXSTL, TXSTART
    ENC_WCRH ETXSTH, TXSTART
    
    ;set transmit buffer stop(ETXND)
    ENC_WCR_W ETXNDL, w4    
    swap w4    
    ENC_WCR_W ETXNDH, w4
        
    ;ERRATA

    Do #0xFF, 0f
    BITVALUE ECON1_TXRST
    ENC_BFS ECON1
    ENC_BFC ECON1    
    
    ;Set ECON1.TXRTS
    BITVALUE EIR_TXIF, EIR_TXERIF
    ENC_BFC EIR

    BITVALUE ECON1_TXRTS
    ENC_BFS ECON1
    
1:  ENC_RCR_ETH ECON1
    btsc w0,#ECON1_TXRTS
    bra  1b     
 
    ENC_RCR_ETH EIR
    btss w0, #EIR_TXERIF
    bra 1f
    BITVALUE EIR_TXERIF
    ENC_BFC EIR
    
    ENC_RCR_ETH ESTAT
    btss w0, #ESTAT_LATECOL
    bra 1f

    BITVALUE ESTAT_LATECOL    
    ENC_BFC ESTAT

0:  NOP    
1:  pop w4
 
return
;}
.end
