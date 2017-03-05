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
        Product Revision  :  MPLAB(c) Code Configurator - 4.15
        Device            :  PIC32MX250F128B
        Driver Version    :  1.02
    The generated drivers are tested against the following:
        Compiler          :  XC32 1.40
        MPLAB             :  MPLAB X 3.40
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

// DEVCFG3
#pragma config PMDL1WAY = ON    // Peripheral Module Disable Configuration->Allow only one reconfiguration
#pragma config IOL1WAY = ON    // Peripheral Pin Select Configuration->Allow only one reconfiguration
#pragma config FUSBIDIO = OFF    // USB USID Selection->Controlled by Port Function
#pragma config FVBUSONIO = OFF    // USB VBUS ON Selection->Controlled by Port Function

// DEVCFG2
#pragma config FPLLIDIV = DIV_5    // PLL Input Divider->5x Divider
#pragma config FPLLMUL = MUL_16    // PLL Multiplier->16x Multiplier
#pragma config UPLLIDIV = DIV_12    // USB PLL Input Divider->12x Divider
#pragma config UPLLEN = OFF    // USB PLL Enable->Disabled and Bypassed
#pragma config FPLLODIV = DIV_8    // System PLL Output Clock Divider->PLL Divide by 8

// DEVCFG1
#pragma config FNOSC = FRC    // Oscillator Selection Bits->Fast RC Osc (FRC)
#pragma config FSOSCEN = OFF    // Secondary Oscillator Enable->Disabled
#pragma config IESO = ON    // Internal/External Switch Over->Enabled
#pragma config POSCMOD = OFF    // Primary Oscillator Configuration->Primary osc disabled
#pragma config OSCIOFNC = OFF    // CLKO Output Signal Active on the OSCO Pin->Disabled
#pragma config FPBDIV = DIV_1    // Peripheral Clock Divisor->Pb_Clk is Sys_Clk/1
#pragma config FCKSM = CSDCMD    // Clock Switching and Monitor Selection->Clock Switch Disable, FSCM Disabled
#pragma config WDTPS = PS1048576    // Watchdog Timer Postscaler->1:1048576
#pragma config WINDIS = OFF    // Watchdog Timer Window Enable->Watchdog Timer is in Non-Window Mode
#pragma config FWDTEN = OFF    // Watchdog Timer Enable->WDT Disabled (SWDTEN Bit Controls)
#pragma config FWDTWINSZ = WINSZ_25    // Watchdog Timer Window Size->Window Size is 25%

// DEVCFG0
#pragma config DEBUG = OFF    // Background Debugger Enable->Debugger is Disabled
#pragma config JTAGEN = OFF    // JTAG Enable->JTAG Disabled
#pragma config ICESEL = ICS_PGx1    // ICE/ICD Comm Channel Select->Communicate on PGEC1/PGED1
#pragma config PWP = OFF    // Program Flash Write Protect->Disable
#pragma config BWP = OFF    // Boot Flash Write Protect bit->Protection Disabled
#pragma config CP = OFF    // Code Protect->Protection Disabled

#include "mcc.h"

/**
  @Summary
    Indicates the exception cause. 

  @Description
    This array identifies the cause for exception.
 */

static char *cause[] = 
{
    "Interrupt", "Undefined", "Undefined", "Undefined",
    "Load/fetch address error", "Store address error",
    "Instruction bus error", "Data bus error", "Syscall", 
    "Breakpoint", "Reserved instruction", "Coprocessor unusable", 
    "Arithmetic overflow", "Trap", "Reserved", "Reserved", 
    "Reserved", "Reserved", "Reserved"
};

void SYSTEM_Initialize(void)
{
    PIN_MANAGER_Initialize();
    OSCILLATOR_Initialize();
    SPI1_Initialize();
    SPI2_Initialize();
    TMR2_Initialize();
    DMA_Initialize();
    INTERRUPT_Initialize();

}

void SYSTEM_RegUnlock(void)
{
    SYSKEY = 0x00000000;
    SYSKEY = 0xAA996655;
    SYSKEY = 0x556699AA;
}

void SYSTEM_RegLock(void)
{
    SYSKEY = 0x00000000; 
}

void OSCILLATOR_Initialize(void)
{
    SYSTEM_RegUnlock(); 
    // CF no clock failure; COSC FRC; PLLODIV DIV_8; PBDIVRDY disabled; SLOCK out of lock; FRCDIV FRC/1; SLPEN Idle on WAIT instruction; NOSC FRC; PLLMULT MUL_16; SOSCEN disabled; PBDIV DIV_1; CLKLOCK unlocked; OSWEN Switch is Complete; SOSCRDY disabled; 
    OSCCON = 0x18010000;
    SYSTEM_RegLock();
    // TUN Center Frequency; 
    OSCTUN = 0x0;
    // DIVSWEN disabled; RSLP disabled; ACTIVE Active; ROSEL SYSCLK; OE Not Driven out on REFCLKO pin; SIDL disabled; RODIV 0; ON disabled; 
    REFOCON = 0x100;
    // ROTRIM 0; 
    REFOTRIM = 0x0;
}

void _general_exception_handler ()
{
    /* Mask off the ExcCode Field from the Cause Register
    Refer to the MIPs Software User's manual */
    uint8_t _excep_code;
    uint8_t _excep_addr;
    uint8_t *_cause_str;
    _excep_code = (_CP0_GET_CAUSE() & 0x0000007C) >> 2;
    _excep_addr = _CP0_GET_EPC();
    _cause_str  = cause[_excep_code];

    while(1)
    {
      ;
    }
}

/**
 End of File
*/