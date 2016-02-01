; Bank 0 registers --------
.equ ERDPTL,		0x00
.equ ERDPTH,		0x01
.equ EWRPTL,		0x02
.equ EWRPTH,		0x03
.equ ETXSTL,		0x04
.equ ETXSTH,		0x05
.equ ETXNDL,		0x06
.equ ETXNDH,		0x07
.equ ERXSTL,		0x08
.equ ERXSTH,		0x09
.equ ERXNDL,		0x0A
.equ ERXNDH,		0x0B
.equ ERXRDPTL,	0x0C
.equ ERXRDPTH,	0x0D
.equ ERXWRPTL,	0x0E
.equ ERXWRPTH,	0x0F
.equ EDMASTL,		0x10
.equ EDMASTH,		0x11
.equ EDMANDL,		0x12
.equ EDMANDH,		0x13
.equ EDMADSTL,	0x14
.equ EDMADSTH,	0x15
.equ EDMACSL,		0x16
.equ EDMACSH,		0x17
;.equ			0x18
;.equ			0x19
;.equ r			0x1A
.equ EIE,			0x1B
.equ EIR,			0x1C
.equ ESTAT,		0x1D
.equ ECON2,		0x1E
.equ ECON1,		0x1F

; Bank 1 registers -----
.equ EHT0,		0x100
.equ EHT1,		0x101
.equ EHT2,		0x102
.equ EHT3,		0x103
.equ EHT4,		0x104
.equ EHT5,		0x105
.equ EHT6,		0x106
.equ EHT7,		0x107
.equ EPMM0,		0x108
.equ EPMM1,		0x109
.equ EPMM2,		0x10A
.equ EPMM3,		0x10B
.equ EPMM4,		0x10C
.equ EPMM5,		0x10D
.equ EPMM6,		0x10E
.equ EPMM7,		0x10F
.equ EPMCSL,		0x110
.equ EPMCSH,	0x111
;.equ			0x112
;.equ			0x113
.equ EPMOL,		0x114
.equ EPMOH,		0x115
;.equ r			0x116
;.equ r			0x117
.equ ERXFCON,		0x118
.equ EPKTCNT,		0x119
;.equ r			0x11A
;.equ EIE		0x11B
;.equ EIR		0x11C
;.equ ESTAT		0x11D
;.equ ECON2		0x11E
;.equ ECON1		0x11F

; Bank 2 registers -----
.equ MACON1,		0x200
;.equ r			0x201
.equ MACON3,		0x202
.equ MACON4,		0x203
.equ MABBIPG,		0x204
;.equ			0x205
.equ MAIPGL,		0x206
.equ MAIPGH,		0x207
.equ MACLCON1,	0x208
.equ MACLCON2,	0x209
.equ MAMXFLL,		0x20A
.equ MAMXFLH,		0x20B
;.equ r			0x20C
;.equ r			0x20D
;.equ r			0x20E
;.equ			0x20F
;.equ r			0x210
;.equ r			0x211
.equ MICMD,		0x212
;.equ r			0x213
.equ MIREGADR,	0x214
;.equ r			0x215
.equ MIWRL,		0x216
.equ MIWRH,		0x217
.equ MIRDL,		0x218
.equ MIRDH,		0x219
;.equ r			0x21A
;.equ EIE		0x21B
;.equ EIR		0x21C
;.equ ESTAT		0x21D
;.equ ECON2		0x21E
;.equ ECON1		0x21F

; Bank 3 registers -----
.equ MAADR5,		0x300
.equ MAADR6,	0x301
.equ MAADR3,		0x302
.equ MAADR4,		0x303
.equ MAADR1,		0x304
.equ MAADR2,		0x305
.equ EBSTSD,		0x306
.equ EBSTCON,		0x307
.equ EBSTCSL,		0x308
.equ EBSTCSH,		0x309
.equ MISTAT,		0x30A
;.equ			0x30B
;.equ			0x30C
;.equ			0x30D
;.equ			0x30E
;.equ			0x30F
;.equ			0x310
;.equ			0x311
.equ EREVID,		0x312
;.equ			0x313
;.equ			0x314
.equ ECOCON,		0x315
;.equ 			0x316
.equ EFLOCON,		0x317
.equ EPAUSL,		0x318
.equ EPAUSH,	0x319
;.equ r			0x31A
;.equ EIE		0x31B
;.equ EIR		0x31C
;.equ ESTAT		0x31D
;.equ ECON2		0x31E
;.equ ECON1		0x31F



;******************************************************************************
;* PH Register Locations
;******************************************************************************/
.equ PHCON1,	0x00
.equ PHSTAT1,	0x01
.equ PHID1,	0x02
.equ PHID2,	0x03
.equ PHCON2,	0x10
.equ PHSTAT2,	0x11
.equ PHIE,	0x12
.equ PHIR,	0x13
.equ PHLCON,	0x14


	

	
	
/******************************************************************************
* Individual Register Bits
******************************************************************************/
; ETH/MAC/MII bits

; EIE bits ----------
.equ	EIE_INTIE,		(7)
.equ	EIE_PKTIE,		(6)
.equ	EIE_DMAIE,		(5)
.equ	EIE_LINKIE,		(4)
.equ	EIE_TXIE,		(3)
.equ	EIE_TXERIE,		(1)
.equ	EIE_RXERIE,		(0)

; EIR bits ----------
.equ	EIR_PKTIF,		(6)
.equ	EIR_DMAIF,		(5)
.equ	EIR_LINKIF,		(4)
.equ	EIR_TXIF,		(3)
.equ	EIR_TXERIF,		(1)
.equ	EIR_RXERIF,		(0)
	
; ESTAT bits ---------
.equ	ESTAT_INT,		(7)
.equ ESTAT_BUFER,		(6)
.equ	ESTAT_LATECOL,	(4)
.equ	ESTAT_RXBUSY,	(2)
.equ	ESTAT_TXABRT,	(1)
.equ	ESTAT_CLKRDY,	(0)
	
; ECON2 bits --------
.equ	ECON2_AUTOINC,	(7)
.equ	ECON2_PKTDEC,	(6)
.equ	ECON2_PWRSV	,	(5)
.equ	ECON2_VRPS	,	(3)
	
; ECON1 bits --------
.equ	ECON1_TXRST,		(7)
.equ	ECON1_RXRST	,	(6)
.equ	ECON1_DMAST	,	(5)
.equ	ECON1_CSUMEN,	(4)
.equ	ECON1_TXRTS	,	(3)
.equ	ECON1_RXEN	,	(2)
.equ	ECON1_BSEL1	,	(1)
.equ	ECON1_BSEL0	,	(0)
	
; ERXFCON bits ------
.equ	ERXFCON_UCEN,	(7)
.equ	ERXFCON_ANDOR,	(6)
.equ	ERXFCON_CRCEN,	(5)
.equ	ERXFCON_PMEN,	(4)
.equ	ERXFCON_MPEN,	(3)
.equ	ERXFCON_HTEN,	(2)
.equ	ERXFCON_MCEN,	(1)
.equ	ERXFCON_BCEN,	(0)
	
; MACON1 bits --------
.equ	MACON1_TXPAUS,	(3)
.equ	MACON1_RXPAUS,	(2)
.equ	MACON1_PASSALL,	(1)
.equ	MACON1_MARXEN,	(0)
	
; MACON3 bits --------
.equ	MACON3_PADCFG2,	(7)
.equ	MACON3_PADCFG1,	(6)
.equ	MACON3_PADCFG0,	(5)
.equ	MACON3_TXCRCEN,	(4)
.equ	MACON3_PHDREN,	(3)
.equ	MACON3_HFRMEN,	(2)
.equ	MACON3_FRMLNEN,	(1)
.equ	MACON3_FULDPX,	(0)
	
; MACON4 bits --------
.equ	MACON4_DEFER,	(6)
.equ	MACON4_BPEN	,	(5)
.equ	MACON4_NOBKOFF,	(4)
	
; MICMD bits ---------
.equ	MICMD_MIISCAN,	(1)
.equ	MICMD_MIIRD	,	(0)

; EBSTCON bits -----
.equ	EBSTCON_PSV2,	(7)
.equ	EBSTCON_PSV1,	(6)
.equ	EBSTCON_PSV0,	(5)
.equ	EBSTCON_PSEL,	(4)
.equ	EBSTCON_TMSEL1,	(3)
.equ	EBSTCON_TMSEL0,	(2)
.equ	EBSTCON_TME,		(1)
.equ	EBSTCON_BISTST,	(0)

; MISTAT bits --------
.equ	MISTAT_NVALID,	(2)
.equ	MISTAT_SCAN	,	(1)
.equ	MISTAT_BUSY	,	(0)
	
; ECOCON bits -------
.equ	ECOCON_COCON2,	(2)
.equ	ECOCON_COCON1,	(1)
.equ	ECOCON_COCON0,	(0)
	
; EFLOCON bits -----
.equ	EFLOCON_FULDPXS,	(2)
.equ	EFLOCON_FCEN1	,(1)
.equ	EFLOCON_FCEN0,	(0)



; PHY bits

; PHCON1 bits ----------
.equ	PHCON1_PRST,		(15)
.equ	PHCON1_PLOOPBK,	(14)
.equ	PHCON1_PPWRSV,	(11)
.equ	PHCON1_PDPXMD,	(8)

; PHSTAT1 bits --------
.equ	PHSTAT1_PFDPX,	(12)
.equ	PHSTAT1_PHDPX,	(11)
.equ	PHSTAT1_LLSTAT,	(2)
.equ	PHSTAT1_JBSTAT,	(1)

; PHID2 bits --------
.equ	PHID2_PID24	,	(15)
.equ	PHID2_PID23	,	(14)
.equ	PHID2_PID22	,	(13)
.equ	PHID2_PID21	,	(12)
.equ	PHID2_PID20	,	(11)
.equ	PHID2_PID19	,	(10)
.equ	PHID2_PPN5	,	(9)
.equ	PHID2_PPN4	,	(8)
.equ	PHID2_PPN3	,	(7)
.equ	PHID2_PPN2	,	(6)
.equ	PHID2_PPN1	,	(5)
.equ	PHID2_PPN0	,	(4)
.equ	PHID2_PREV3	,	(3)
.equ	PHID2_PREV2	,	(2)
.equ	PHID2_PREV1	,	(1)
.equ	PHID2_PREV0	,	(0)

; PHCON2 bits ----------
.equ	PHCON2_FRCLNK,	(14)
.equ	PHCON2_TXDIS,	(13)
.equ	PHCON2_JABBER,	(10)
.equ	PHCON2_HDLDIS,	(8)

; PHSTAT2 bits --------
.equ	PHSTAT2_TXSTAT,	(13)
.equ	PHSTAT2_RXSTAT,	(12)
.equ	PHSTAT2_COLSTAT,	(11)
.equ	PHSTAT2_LSTAT	,(10)
.equ	PHSTAT2_DPXSTAT,	(9)
.equ	PHSTAT2_PLRITY	,(5)

; PHIE bits -----------
.equ	PHIE_PLNKIE,		(4)
.equ	PHIE_PGEIE	,	(1)

; PHIR bits -----------
.equ	PHIR_PLNKIF	,	(4)
.equ	PHIR_PGIF	,	(2)

; PHLCON bits -------
.equ	PHLCON_LACFG3,	(11)
.equ	PHLCON_LACFG2,	(10)
.equ	PHLCON_LACFG1,	(9)
.equ	PHLCON_LACFG0,	(8)
.equ	PHLCON_LBCFG3,	(7)
.equ	PHLCON_LBCFG2,	(6)
.equ	PHLCON_LBCFG1,	(5)
.equ	PHLCON_LBCFG0,	(4)
.equ	PHLCON_LFRQ1,	(3)
.equ	PHLCON_LFRQ0,	(2)
.equ	PHLCON_STRCH,	(1)
