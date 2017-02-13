


/**
  @Generated MPLAB(c) Code Configurator Source File

  @Company:
    Microchip Technology Inc.

  @File Name:
    mcc.c

  @Summary:
    This is the mcc.c file generated using MPLAB(c) Code Configurator

  @Description:
    This header file provides implementations for driver APIs for all modules selected in the GUI.
    Generation Information :
        Product Revision  :  MPLAB(c) Code Configurator - pic24-dspic-pic32mm : v1.25
        Device            :  PIC32MM0064GPL028
        Driver Version    :  1.02
    The generated drivers are tested against the following:
        Compiler          :  XC32 1.42
        MPLAB             :  MPLAB X 3.45
*/

/*
    (c) 2016 Microchip Technology Inc. and its subsidiaries. You may use this
    software and any derivatives exclusively with Microchip products.

    THIS SOFTWARE IS SUPPLIED BY MICROCHIP "AS IS". NO WARRANTIES, WHETHER
    EXPRESS, IMPLIED OR STATUTORY, APPLY TO THIS SOFTWARE, INCLUDING ANY IMPLIED
    WARRANTIES OF NON-INFRINGEMENT, MERCHANTABILITY, AND FITNESS FOR A
    PARTICULAR PURPOSE, OR ITS INTERACTION WITH MICROCHIP PRODUCTS, COMBINATION
    WITH ANY OTHER PRODUCTS, OR USE IN ANY APPLICATION.

    IN NO EVENT WILL MICROCHIP BE LIABLE FOR ANY INDIRECT, SPECIAL, PUNITIVE,
    INCIDENTAL OR CONSEQUENTIAL LOSS, DAMAGE, COST OR EXPENSE OF ANY KIND
    WHATSOEVER RELATED TO THE SOFTWARE, HOWEVER CAUSED, EVEN IF MICROCHIP HAS
    BEEN ADVISED OF THE POSSIBILITY OR THE DAMAGES ARE FORESEEABLE. TO THE
    FULLEST EXTENT ALLOWED BY LAW, MICROCHIP'S TOTAL LIABILITY ON ALL CLAIMS IN
    ANY WAY RELATED TO THIS SOFTWARE WILL NOT EXCEED THE AMOUNT OF FEES, IF ANY,
    THAT YOU HAVE PAID DIRECTLY TO MICROCHIP FOR THIS SOFTWARE.

    MICROCHIP PROVIDES THIS SOFTWARE CONDITIONALLY UPON YOUR ACCEPTANCE OF THESE
    TERMS.
*/


// Configuration bits: selected in the GUI

// FDEVOPT
#pragma config SOSCHP = OFF    // Secondary Oscillator High Power Enable bit->SOSC oprerates in normal power mode.

// FICD
#pragma config JTAGEN = OFF    // JTAG Enable bit->JTAG is disabled
#pragma config ICS = PGx1    // ICE/ICD Communication Channel Selection bits->Communicate on PGEC1/PGED1

// FPOR
#pragma config BOREN = BOR3    // Brown-out Reset Enable bits->Brown-out Reset enabled in hardware; SBOREN bit disabled
#pragma config RETVR = OFF    // Retention Voltage Regulator Enable bit->Retention regulator is disabled
#pragma config LPBOREN = ON    // Low Power Brown-out Enable bit->Low power BOR is enabled, when main BOR is disabled

// FWDT
#pragma config SWDTPS = PS1048576    // Sleep Mode Watchdog Timer Postscale Selection bits->1:1048576
#pragma config FWDTWINSZ = PS25_0    // Watchdog Timer Window Size bits->Watchdog timer window size is 25%
#pragma config WINDIS = OFF    // Windowed Watchdog Timer Disable bit->Watchdog timer is in non-window mode
#pragma config RWDTPS = PS1048576    // Run Mode Watchdog Timer Postscale Selection bits->1:1048576
#pragma config RCLKSEL = LPRC    // Run Mode Watchdog Timer Clock Source Selection bits->Clock source is LPRC (same as for sleep mode)
#pragma config FWDTEN = OFF    // Watchdog Timer Enable bit->WDT is disabled

// FOSCSEL
#pragma config FNOSC = PRI    // Oscillator Selection bits->Primary oscillator (XT, HS, EC)
#pragma config PLLSRC = PRI    // System PLL Input Clock Selection bit->Primary oscillator is selected as PLL reference input on device reset
#pragma config SOSCEN = OFF    // Secondary Oscillator Enable bit->Secondary oscillator (SOSC) is disabled
#pragma config IESO = ON    // Two Speed Startup Enable bit->Two speed startup is enabled
#pragma config POSCMOD = HS    // Primary Oscillator Selection bit->HS oscillator mode is selected
#pragma config OSCIOFNC = OFF    // System Clock on CLKO Pin Enable bit->OSCO pin operates as a normal I/O
#pragma config SOSCSEL = ON    // Secondary Oscillator External Clock Enable bit->External clock is connected to SOSCO pin (RA4 and RB4 are controlled by I/O port registers)
#pragma config FCKSM = CSDCMD    // Clock Switching and Fail-Safe Clock Monitor Enable bits->Clock switching is disabled; Fail-safe clock monitor is disabled

// FSEC
#pragma config CP = OFF    // Code Protection Enable bit->Code protection is disabled

#include "mcc.h"

void SYSTEM_Initialize(void)
{
    PIN_MANAGER_Initialize();
    INTERRUPT_Initialize();
    OSCILLATOR_Initialize();
    UART2_Initialize();
    SPI1_Initialize();
    INTERRUPT_GlobalEnable();
}


void OSCILLATOR_Initialize(void)
{
    SYSTEM_RegUnlock();
    // TUN Center frequency; 
    OSCTUN = 0x0;
    // PLLODIV 1:1; PLLMULT 2x; PLLICLK POSC; 
    SPLLCON = 0x0;
    // SWRST disabled; 
    RSWRST = 0x0;
    // WDTO disabled; GNMI disabled; CF disabled; WDTS disabled; NMICNT 0; LVD disabled; SWNMI disabled; 
    RNMICON = 0x0;
    // SBOREN disabled; VREGS disabled; RETEN disabled; 
    PWRCON = 0x0;
    // CF No Clock Failure; FRCDIV FRC/1; SLPEN Device will enter Idle mode when a WAIT instruction is issued; NOSC PRI; SOSCEN disabled; CLKLOCK Clock and PLL selections are not locked and may be modified; OSWEN Complete; 
    OSCCON = 0x200;
    SYSTEM_RegLock();
    // WDTO disabled; EXTR disabled; POR disabled; SLEEP disabled; BOR disabled; PORIO disabled; IDLE disabled; PORCORE disabled; BCFGERR disabled; CMR disabled; BCFGFAIL disabled; SWR disabled; 
    RCON = 0x0;
    // ON enabled; DIVSWEN enabled; RSLP disabled; ROSEL SYSCLK; OE disabled; SIDL disabled; RODIV 1; 
    REFO1CON = 0x18200;
    // ROTRIM 182; 
    REFO1TRIM = 0x5B000000;
    // SPDIVRDY disabled; 
    CLKSTAT = 0x0;
}

/**
 End of File
*/