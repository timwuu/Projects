.macro BITVALUE op1, op2, op3, op4, op5, op6, op7, op8
     .equ __btv, (0x01<<(\op1))
    .ifndef NULL&op2&
        .equ __btv, __btv|(0x01<<(\op2))    
    .ifndef NULL&op3&
        .equ __btv, __btv|(0x01<<(\op3))
    .ifndef NULL&op4&
        .equ __btv, __btv|(0x01<<(\op4))
    .ifndef NULL&op5&
        .equ __btv, __btv|(0x01<<(\op5))
    .ifndef NULL&op6&
        .equ __btv, __btv|(0x01<<(\op6))
    .ifndef NULL&op7&
        .equ __btv, __btv|(0x01<<(\op7))
    .ifndef NULL&op8&
        .equ __btv, __btv|(0x01<<(\op8))
    .endif
    .endif
    .endif
    .endif
    .endif
    .endif
    .endif
.endm

.macro BANKSEL address
    mov #(\address>>8), w0
    call _banksel
.endm

.macro ENC_RCR_ETH address
    mov #\address, w0
    call _enc_rcr_eth
.endm

.macro ENC_RCR_MAC address
    mov #\address, w0
    call _enc_rcr_mac
.endm

.macro ENC_RCR_PHY address
    mov #\address, w0
    call _enc_rcr_phy
.endm

.macro ENC_SRC
    call _sendSystemReset
.endm

.macro _ENC_WRITE cmdaddr, value
    .ifdef NULL&value&
        mov #__btv, w1
    .else
        mov #\value, w1
    .endif
    mov #\cmdaddr, w0
    call _write_reg 
.endm

.macro ENC_BFC address, value
    _ENC_WRITE (BFC|\address&0xFF), \value
.endm

.macro ENC_BFS address, value
    _ENC_WRITE (BFS|\address&0xFF), \value
.endm

.macro ENC_BFS_W address, register
    mov \register, w1
    mov #(BFS|\address&0xFF), w0
    call _write_reg 
.endm

.macro ENC_WCR address, value
    _ENC_WRITE (WCR|\address&0xFF), \value
.endm

.macro ENC_WCR_W address, register
    mov \register, w1
    mov #(WCR|\address&0xFF), w0
    call _write_reg 
.endm

.macro ENC_WCRL address, value
    ENC_WCR \address, \value
.endm

.macro ENC_WCRH address, value
    mov #(WCR|\address&0xFF), w0
    .ifdef NULL&value&
        mov #(__btv>>8), w1
    .else
        mov #(\value>>8), w1
    .endif    
    call _write_reg 
.endm

.macro ENC_WCR_PHY cmdaddr, value
    .ifdef NULL&value&
        mov #__btv, w1
    .else
        mov #\value, w1
    .endif
    mov #\cmdaddr, w0
    call _enc_wcr_phy
.endm

.macro MACPUT value
    mov #\value, w0
    call _mac_put 
.endm
