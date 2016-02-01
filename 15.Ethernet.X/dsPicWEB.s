;******************************************************************************
;   How to use this file:                                                     *
;   ======================                                                    *
;                                                                             *
;   This file is a basic template for creating Assembly code for a dsPIC30F   *
;   device.  Copy this file into your project directory and modify or         *
;   add to it as needed.                                                      *
;                                                                             *
;   Add the device linker script (e.g., p30f2011.gld) and the device          *
;   include file (e.g. p30f2011.inc). If you are using this file for          *
;   an application that uses a device other than dsPIC 30F6014 which is       *
;   identified in the list of supported devices, add the linker script and    *
;   include file specific to that device into your project.                   *
;                                                                             *
;   If you plan to use initialized data sections in the application and       *
;   would like to use the start-up module in the run-time library to          *
;   initialize the data sections then use the template file in this folder    *
;   named, "tmp6014_srt.s". Refer to the document "dsPIC 30F Assembler,       *
;   Linker and Utilities User's Guide" for further information on the start-up*
;   module in the run-time library.                                           *
;                                                                             *
;   If interrupts are not used, all code presented for that interrupt         *
;   can be removed or commented out using comment symbols (semi-colons).      *
;                                                                             *
;   For additional information about dsPIC 30F  architecture and language     *
;   tools, refer to the following documents:                                  *
;                                                                             *
;   MPLAB C30 Compiler User's Guide                       : DS51284           *
;   dsPIC 30F Assembler, Linker and Utilities User's Guide: DS51317           *
;   dsPIC 30F 16-bit MCU Family Reference Manual          : DS70046           *
;   dsPIC 30F Power and Motion Control Family Data Sheet  : DS70082           *
;   dsPIC 30F Programmer's Reference Manual               : DS70030           *
;                                                                             *
;******************************************************************************
;                                                                             *
;    Author              :                                                    *
;    Company             :                                                    *
;    Filename            :  dsBlink.s                                         *
;    Date                :  12/04/2007                                        *
;    File Version        :  0.00                                              *
;                                                                             *
;    Other Files Required: p30F2011.gld, p30f2011.inc                         *
;    Tools Used:MPLAB GL : 7.60                                               *
;               Compiler : 1.30                                               *
;               Assembler: 1.30                                               *
;               Linker   : 1.30                                               *
;                                                                             *
;    Devices Supported by this file:                                          *
;                        dsPIC 30F2011                                        *
;                        dsPIC 30F3012                                        *
;                        dsPIC 30F2012                                        *
;                        dsPIC 30F3013                                        *
;                        dsPIC 30F5011                                        *
;                        dsPIC 30F5012                                        *
;                        dsPIC 30F6011                                        *
;                        dsPIC 30F6012                                        *
;                        dsPIC 30F5013                                        *
;                        dsPIC 30F6013                                        *
;                        dsPIC 30F6014                                        *
;                                                                             *
;******************************************************************************
;    Notes:                                                                   *
;    ======                                                                   *
;                                                                             *
;1. The device include file defines bit names and their location within SFR   *
;   words. Replace the path to the include file, if necessary.                *
;   For e.g., to use the p30f2011 device modify the .include directive to     *
;   .include "Yourpath\p30F2011.inc"                                          *
;                                                                             *
;2. The "config" macro is present in the device specific '.inc' file and can  *
;   be used to set configuration bits for the dsPIC 30F device.               *
;   The '.inc' files also contain examples on using the "config" macro.       *
;   Some examples have been provided in this file. The user application may   *
;   require a different setting for the configuration bits.                   *
;   The dsPIC 30F 16-bit MCU Family Refernce Manual(DS70046) explains the     *
;   function of the configuration bits.                                       *
;                                                                             *
;3. The "__reset" label is the first label in the code section and must be    *
;   declared global. The Stack Pointer (W15) and the Stack Pointer Limit      *
;   register must be initialized to values that are past the run-time heap    *
;   space, so as to avoid corrupting user data. Initializing these registers  *
;   at the "__reset" label ensures that user data is not corrupted            *
;   accidentally. The __SP_init and __SPLIM_init literals are defined by the  *
;   linker. The linker automatically addresses above the run-time heap to     *
;   these symbols. Users can change the __SP_init and __SPLIM_init literals to*
;   suit their application.                                                   *
;                                                                             *
;4. A ".section <section_name>, <attributes>" directive declares a new section*
;   named "<section_name>" with an attribute list that specifies if the       *
;   section is located in Program Space(code), Uninitialized(bss) or          *
;   Initialized Data Space(data). Refer to the document "dsPIC 30F Assembler, *
;   Linker and Utilities User's Guide" for further details.                   *
;                                                                             *
;5. Initialize W registers: Using uninitialized W registers as Effective      *
;   Addresses(pointers) will cause an "Uninitialized W Register Trap" !       *
;                                                                             *
;6. The label "__T1Interrupt" is defined in the device linker script. The     *
;   label defines the starting location in program space of the Timer1        *
;   interrupt service routine (ISR).                                          *
;   Similarly, the linker script defines labels for all ISRs. Notice, that    *
;   the ISR label names are preceded by two underscore characters.            *
;   When users needs to write ISR code, they must use these labels after      *
;   providing them global scope. The linker will then place the ISR address in*
;   the respective interrupt vector table location in program space.          *
;   Context Save/Restore in ISRs can be performed using the double-word PUSH  *
;   instruction,PUSH.D. User can also make use of MOV.D, PUSH and PUSH.S      *
;   instructions. Refer dsPIC 30F 16-bit MCU Family Refernce Manual(DS70046)  *
;   for further details.                                                      *
;                                                                             *
;******************************************************************************
;                                                                             *
;    Additional Notes:                                                        *
;                                                                             *
;                                                                             *
;                                                                             *
;                                                                             *
;******************************************************************************
        ;.equ __30F2011, 1
        .include "xc.inc"
        .include "_ENC28J60Reg.inc"
        .include "lcd.inc"
        
.extern _enc_init
.extern _monitor_rx
.extern _socket_init        
;..............................................................................
;Configuration bits:
;..............................................................................
    config __FOSC, CSW_FSCM_OFF & FRC_PLL8   ;Turn off clock switching and      
                                        ;fail-safe clock monitoring and      
                                        ;use the Internal fast RC Osc as the      
                                        ;system clock, 7.37MHz * 8       
    config __FWDT, WDT_OFF              ;Turn off Watchdog Timer        
    config __FBORPOR, PBOR_OFF & BORV_27 & PWRT_OFF & MCLR_DIS      
                                        ;Set Brown-out Disabled and     
                                        ;and set Power-up Timer OFF       
    config __FGS, CODE_PROT_OFF         ;Set Code Protection Off for the       
                                        ;General Segment
                                        
;..............................................................................
;Program Specific Constants (literals used in code)
;..............................................................................
                 
    .equ SAMPLES, 64         ;Number of samples
    .equiv LCD_RS, RC13        
    .equiv LCD_E, RC14
    .equiv LCD_RW, RC15

;..............................................................................
;Global Declarations:
;..............................................................................
        .global _wreg_init       ;Provide global scope to _wreg_init routine    
                                ;In order to call this routine from a C file,   
                                ;place "wreg_init" in an "extern" declaration   
                                ;in the C file.        
        .global __reset          ;The label for the first line of code.         
        .global __T1Interrupt    ;Declare Timer 1 ISR name global
        
        .global _main
        
;..............................................................................
;Constants stored in Program space
;..............................................................................

    .section .myconstbuffer, code
    .palign 2   ;Align next word stored in Program space to an              
                ;address that is a multiple of 2
                
ps_coeff:        .hword   0x0002, 0x0003, 0x0005, 0x000A
;..............................................................................
;Uninitialized variables in X-space in data memory
;..............................................................................

         .section .xbss, bss, xmemory
         
;x_input: .space 2*SAMPLES        ;Allocating space (in bytes) to variable.

;..............................................................................
;Uninitialized variables in Y-space in data memory
;..............................................................................

          .section .ybss, bss, ymemory
          
;y_input:  .space 2*SAMPLES

;..............................................................................
;Uninitialized variables in Near data memory (Lower 8Kb of RAM)
;..............................................................................

          .section .nbss, bss, near
          
var1:     .space 2  ;Example of allocating 1 word of space for                    
                    ;variable "var1"
                    
delayH:   .space 2
delayL:   .space 2

;..............................................................................
;Code Section in Program Memory
;..............................................................................



;RD0:OUTPUT, LED0
;RC13: OUTPUT, LCD_RS
;RC14: OUTPUT, LCD_E
;RC15: OUTPUT, LCD_RW

;RB<0:3>: OUTPUT, LCD_DB<4:7>

.text                             ;Start of Code section
__reset:        
        MOV #__SP_init, W15       ;Initalize the Stack Pointer        
        MOV #__SPLIM_init, W0     ;Initialize the Stack Pointer Limit Register
        MOV W0, SPLIM
        NOP                     ;Add NOP to follow SPLIM initialization
        CALL _wreg_init         ;Call _wreg_init subroutine
                                ;Optionally use RCALL instead of CALL

        call __data_init
        ;<<insert more user code here>>        

;Initialize Port I/O
                
        MOV #0x00, W0       ;Initialize PortB, PortC, PortD
        MOV W0, PORTB
        MOV W0, PORTC
        MOV W0, PORTD
        
        MOV #0xFFF0, W0     ;Set PortB<0:3> for output
        MOV W0, TRISB
        
        MOV #0x1FFF, W0     ;Set PortC<13:15> for output
        MOV W0, TRISC
        
        MOV #0xFFFE, W0     ;Set PortD<0> for output
        MOV W0, TRISD
        
        bset PORTD, #RD0
        
;Initialize LCD
        rCALL _lcd_init
        
        mov.b #'@', W0
        rCALL _lcd_wr_data
        
;Initialize ENC
        rCALL _enc_init 

        rCALL _sck_init

        mov  #0x0200, w0   ;delay 2sec
        rCALL _delayms

        rcall _lcd_clear_display

        rCALL _monitor_rx        
blink:        

done:        
        BRA     blink ;Place holder for last line of executed code
                
        RETURN


        
;..............................................................................        
;Subroutine: Initialization of W registers to 0x0000
;..............................................................................
_wreg_init:        
        CLR W0        
        MOV W0, W14        
        REPEAT #12        
        MOV W0, [++W14]        
        CLR W14        
        RETURN
;..............................................................................
;Timer 1 Interrupt Service Routine
;Example context save/restore in the ISR performed using PUSH.D/POP.D
;instruction. The instruction pushes two words W4 and W5 on to the stack on
;entry into ISR and pops the two words back into W4 and W5 on exit from the ISR
;..............................................................................

__T1Interrupt:
        PUSH.D W4     ;Save context using double-word PUSH        

    ;<<insert more user code here>>       
        BCLR IFS0, #T1IF      ;Clear the Timer1 Interrupt flag Status
                              ;bit.        
        POP.D W4      ;Retrieve context POP-ping from Stack
        RETFIE        ;Return from Interrupt Service routine



        .weak __data_init
__data_init:
;; 
;; Process data init template
;;
;; The template is null-terminated, with records
;; in the following format:
;;
;; struct data_record {
;;  char *dst;        /* destination address  */
;;  int  len;         /* length in bytes      */
;;  int  format;      /* format code          */
;;  char dat[0];      /* variable length data */
;; };
;; 
;; Registers used:  w0 w1 w2 w3 w4 w5
;; 
;; Inputs (defined by linker):
;;  __dinit_tbloffset
;;  __dinit_tblpage
;; 
;; Outputs:
;;  (none)
;;
;; Calls:
;;  __memcpypd3
;; 
        .equiv   FMT_CLEAR,0    ;  format codes
        .equiv   FMT_COPY2,1    ; 
        .equiv   FMT_COPY3,2    ; 

        mov      #__dinit_tbloffset,w0 ; w0,w1 = template
        mov      #__dinit_tblpage,w1   ;
        bra      4f                    ; br to continue

1:      add      w0,#2,w0       ; template+=2
        addc     w1,#0,w1       ; 
        mov      w1,_TBLPAG     ; TBLPAG = tblpage(template)

        tblrdl.w [w0],w3        ; w3 = len 
        add      w0,#2,w0       ; template+=2
        addc     w1,#0,w1       ; 
        mov      w1,_TBLPAG     ; TBLPAG = tblpage(template)

        tblrdl.w [w0],w5        ; w5 = format
        add      w0,#2,w0       ; template+=2
        addc     w1,#0,w1       ; 
        clr      w4             ; upper = FALSE (default)

        cp       w5,#FMT_CLEAR  ; test format
        bra      nz,2f          ; br if not FMT_CLEAR

        ;; FMT_CLEAR - clear destination memory
        dec      w3,w3          ; decrement & test len
        bra      n,4f           ; br if negative

        repeat   w3             ; 
        clr.b    [w2++]         ; clear memory      
        bra      4f             ; br to continue

        ;; FMT_COPY2, FMT_COPY3 - copy bytes
2:      cp       w5,#FMT_COPY2  ; test format
        bra      z,3f           ; br if FMT_COPY2

        setm     w4             ; upper = TRUE

3:      rcall    __memcpypd3    ; copy 2 or 3 bytes

4:      mov      w1,_TBLPAG     ; TBLPAG = tblpage(template)
        tblrdl.w [w0],w2        ; w2 = next dst      
        cp0      w2             ; 
        bra      nz,1b          ; loop on non-zero dst

        retlw    #0,w0          ; exit (clears ARGC also)

        
__memcpypd3:
;; 
;; Copy data from program memory to data memory
;; 
;; Registers used:  w0 w1 w2 w3 w4 w5
;; 
;; Inputs:
;;  w0,w1 = source address   (24 bits)
;;  w2 = destination address (16 bits)
;;  w3 = number of bytes (even or odd)
;;  w4 = upper byte flag   (0 = false)
;; 
;; Outputs:
;;  w0,w1 = next source address (24 bits)
;; 

1:      mov      w1,_TBLPAG     ; TBLPAG = tblpage(src)
        mov      w0,w5          ; w5 =   tbloffset(src)
        add      w0,#2,w0       ; src+=2
        addc     w1,#0,w1       ;

        tblrdl.b [w5++],[w2++]  ; dst++ = lo byte
        dec      w3,w3          ; num--
        bra      z,2f           ; br if done

        tblrdl.b [w5--],[w2++]  ; dst++ = hi byte
        dec      w3,w3          ; num--
        bra      z,2f           ; br if done

        cp0      w4             ; test upper flag
        bra      z,1b           ; br if false

        tblrdh.b [w5],[w2++]    ; dst++ = upper byte
        dec      w3,w3          ; num--
        bra      nz,1b          ; br if not done

2:      return                  ; exit

;--------End of All Code Sections ---------------------------------------------

.end                               ;End of program code in this file
