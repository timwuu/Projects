
/**
  MCCP1 Generated Driver File 

  @Company
    Microchip Technology Inc.

  @File Name
    mccp1.c

  @Summary
    This is the generated driver implementation file for the MCCP1 driver using MPLAB(c) Code Configurator

  @Description
    This header file provides implementations for driver APIs for MCCP1. 
    Generation Information : 
        Product Revision  :  MPLAB(c) Code Configurator - pic24-dspic-pic32mm : v1.25
        Device            :  PIC32MM0064GPL028
        Driver Version    :  0.5
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

/**
  Section: Included Files
*/

#include <xc.h> 
#include "mccp1_tmr.h"

/**
  Section: Data Type Definitions
*/

/**
  MCCP1 Driver Hardware Instance Object

  @Summary
    Defines the object required for the maintenance of the hardware instance.

  @Description
    This defines the object required for the maintenance of the hardware
    instance. This object exists once per hardware instance of the peripheral.

*/
typedef struct _MCCP1_TMR_OBJ_STRUCT
{

    /* Timer Elapsed */
    bool                                                    primaryTimer16Elapsed;
    bool                                                    secondaryTimer16Elapsed;
    bool                                                    Timer32Elapsed;
} MCCP1_TMR_OBJ;

static MCCP1_TMR_OBJ mccp1_timer_obj;
void MCCP1_TMR_Initialize(void)
{
  //ON enabled; MOD 16-Bit/32-Bit Timer; ALTSYNC disabled; SIDL disabled; OPS Each Time Base Period Match; CCPSLP disabled; TMRSYNC disabled; RTRGEN disabled; CCSEL disabled; ONESHOT disabled; TRIGEN disabled; T32 32 Bit; SYNC None; OPSSRC Timer Interrupt Event; TMRPS 1:64; CLKSEL REFO; 
    CCP1CON1 = 0x81E0;
  //OCCEN disabled; OCDEN disabled; ASDGM disabled; OCEEN disabled; ICGSM Level-Sensitive mode; OCFEN disabled; ICS ICM1; SSDG disabled; AUXOUT Disabled; ASDG None; OCAEN disabled; OCBEN disabled; OENSYNC disabled; PWMRSEN disabled; 
    CCP1CON2 = 0x0;
  //DT 0; OETRIG disabled; OSCNT None; POLACE disabled; POLBDF disabled; PSSBDF Tri-state; OUTM Steerable single output; PSSACE Tri-state; 
    CCP1CON3 = 0x0;
  //SCEVT disabled; TRSET disabled; ICOV disabled; ASEVT disabled; ICGARM disabled; RBWIP disabled; TRCLR disabled; RAWIP disabled; TMRHWIP disabled; TMRLWIP disabled; PRLWIP disabled; 
    CCP1STAT = 0x0;
  //TMRL 0; TMRH 0; 
    CCP1TMR = 0x0;
  //PRH 0; PRL 57600; 
    CCP1PR = 0xE100;
  //CMPA 0; 
    CCP1RA = 0x0;
  //CMPB 0; 
    CCP1RB = 0x0;
  //BUFL 0; BUFH 0; 
    CCP1BUF = 0x0;

    IFS0CLR= 1 << _IFS0_CCP1IF_POSITION;

    IFS0CLR= 1 << _IFS0_CCT1IF_POSITION;
      
    // Enabling MCCP1 interrupt.
    IEC0bits.CCP1IE = 1;

    // Enabling MCCP1 interrupt.
    IEC0bits.CCT1IE = 1;

}

void MCCP1_TMR_Start( void )
{
    /* Reset the status information */
    mccp1_timer_obj.primaryTimer16Elapsed = false;
    mccp1_timer_obj.secondaryTimer16Elapsed = false;
    mccp1_timer_obj.Timer32Elapsed = false;

    /* Start the Timer */
    CCP1CON1SET = (1 << _CCP1CON1_ON_POSITION);
}
void MCCP1_TMR_Stop( void )
{
    /* Stop the Timer */
    CCP1CON1CLR = (1 << _CCP1CON1_ON_POSITION);
}

void __attribute__ ( ( vector ( _CCT1_VECTOR ), interrupt ( IPL1SOFT ))) CCT1_ISR (void)
{
    /* Check if the Timer Interrupt/Status is set */
    if(IFS0bits.CCT1IF)
    {         
        mccp1_timer_obj.Timer32Elapsed = true;
        IFS0CLR= 1 << _IFS0_CCT1IF_POSITION;
    }
}



void MCCP1_TMR_Period32BitSet( uint32_t value )
{
    /* Update the period values */
    CCP1PR  = value;

    /* Reset the status information */
    mccp1_timer_obj.Timer32Elapsed = false;
}
uint32_t MCCP1_TMR_Period32BitGet( void )
{
    uint32_t periodVal = 0xFFFFFFFF;
    
    /* get the timer period value and return it */
    periodVal = (uint32_t)CCP1PR;

    return(periodVal);
}
void MCCP1_TMR_Counter32BitSet ( uint32_t value )
{
    /* Update the counter values */
    CCP1TMR  = value;
    /* Reset the status information */
    mccp1_timer_obj.Timer32Elapsed = false;
}
uint32_t MCCP1_TMR_Counter32BitGet( void )
{
    uint32_t counterVal = 0xFFFFFFFF;

    /* get the timer period value and return it */
    counterVal = CCP1TMR;

    return(counterVal);
}

bool MCCP1_TMR_Timer32ElapsedThenClear(void)
{
    bool status;
    
    status = mccp1_timer_obj.Timer32Elapsed ;
    
    if(status == true)
    {
        mccp1_timer_obj.Timer32Elapsed = false;
    }
    return status;
}
/**
 End of File
*/