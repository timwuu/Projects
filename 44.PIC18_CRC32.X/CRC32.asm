#include <xc.inc>

; When assembly code is placed in a psect, it can be manipulated as a
; whole by the linker and placed in memory.  
;
; In this example, barfunc is the program section (psect) name, 'local' means
; that the section will not be combined with other sections even if they have
; the same name.  class=CODE means the barfunc must go in the CODE container.
; PIC18's should have a delta (addressible unit size) of 1 (default) since they
; are byte addressible.  PIC10/12/16's have a delta of 2 since they are word
; addressible.  PIC18's should have a reloc (alignment) flag of 2 for any
; psect which contains executable code.  PIC10/12/16's can use the default
; reloc value of 1.  Use one of the psects below for the device you use:

;Poly3 equ 0x04 ; 04C11DB7
;Poly2 equ 0xC1 
;Poly1 equ 0x1D 
;Poly0 equ 0xB7
 
Poly3 equ 0xED ; EDB88320
Poly2 equ 0xB8 
Poly1 equ 0x83 
Poly0 equ 0x20
 

global _CRCi
global _CRC0
global _CRC1
global _CRC2
global _CRC3 
 
 


 

    
;psect   barfunc,local,class=CODE,delta=2 ; PIC10/12/16
psect   barfunc,local,class=CODE,reloc=2 ; PIC18

global _Calc_CRC32 ; extern of bar function goes in the C source file 
 
_Calc_CRC32:
    
    call CRCstart

    movlw '1'
    call CRCupdate
    movlw '2'
    call CRCupdate
    movlw '3'
    call CRCupdate
    movlw '4'
    call CRCupdate
    movlw '5'
    call CRCupdate
    movlw '6'
    call CRCupdate
    movlw '7'
    call CRCupdate
    movlw '8'
    call CRCupdate
    movlw '9'
    call CRCupdate

;    movlw 0x00 ; Flush 32 0 bits
;    call CRCupdate
;    movlw 0x00
;    call CRCupdate
;    movlw 0x00
;    call CRCupdate
;    movlw 0x00
;    call CRCupdate
    call CRCfinish
    return
    
    
CRCupdate: 
    xorwf _CRC0, f   ; CRC0= CRC0^W
    ;movwf CRCdataIn  
    movlw 8
    movwf _CRCi
CRCloop:
    BCF STATUS,0  ;clear carry flag
    rrcf _CRC3
    rrcf _CRC2
    rrcf _CRC1
    rrcf _CRC0
    bnc CRCnotSet
    movlw Poly0 ; Carry bit is set
    xorwf _CRC0, f
    movlw Poly1
    xorwf _CRC1, f
    movlw Poly2
    xorwf _CRC2, f
    movlw Poly3
    xorwf _CRC3, f
CRCnotSet: 
    decfsz _CRCi, f
    bra CRCloop
    return
;-------------------------------------------------------------------------------
CRCfinish:
    comf _CRC0, f
    comf _CRC1, f
    comf _CRC2, f
    comf _CRC3, f
    return
;-------------------------------------------------------------------------------
CRCstart:
    movlw 0xFF ; init CRC
    movwf _CRC0
    movwf _CRC1
    movwf _CRC2
    movwf _CRC3
    return    
