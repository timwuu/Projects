/**
  MCCP1 Generated Driver API Header File 

  @Company
    Microchip Technology Inc.

  @File Name
    mccp1.h

  @Summary
    This is the generated header file for the MCCP1 driver using MPLAB(c) Code Configurator

  @Description
    This header file provides APIs for driver for MCCP1. 
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

#ifndef _MCCP1_TMR_H
#define _MCCP1_TMR_H
/**
  Section: Included Files
*/
#include <stdlib.h>
#include <stdint.h>
#include <stdbool.h>

#ifdef __cplusplus  // Provide C++ Compatibility

    extern "C" {

#endif
/**
  Section: Interface Routines
*/
/**
  @Summary
    Initializes hardware and data for the given instance of the TMR module

  @Description
    This routine initializes hardware for the instance of the TMR module,
    using the hardware initialization given data.  It also initializes all
    necessary internal data.

  @Param
    None.

  @Returns
    None

  @Comment
    
 
  @Example
    <code>
    bool statusTimer1;
    uint16_t period;
    uint16_t value;

    period = 0x20;

    MCCP1_TMR_Initializer();

    MCCP1_TMR_Period16BitSet(period);

    if((value = MCCP1_TMR_Period16BitGet())== period)
    {
        MCCP1_TMR_Start();
    }

    while(1)
    {
        MCCP1_TMR_Tasks();
        if( (statusTimer1 = MCCP1_TMR_IsElapsed()) == true)
        {
            MCCP1_TMR_Stop();
        }
    }
    </code>
*/
void MCCP1_TMR_Initialize (void);

/**
  @Summary
    Starts the TMR

  @Description
    This routine starts the TMR

  @Param
    None.

  @Returns
    None

  @Example
    Refer to the example of MCCP1_Initializer();
*/

void MCCP1_TMR_Start( void );
/**
  @Summary
    Stops the TMR

  @Description
    This routine stops the TMR

  @Param
    None.

  @Returns
    None

  @Example
    Refer to the example of MCCP1_Initializer();
*/

void MCCP1_TMR_Stop( void );

void __attribute__ ( ( interrupt, no_auto_psv ) ) _CCT1 ( void );
/**
  @Summary
    Updates 16-bit mccp value

  @Description
    This routine updates 16-bit mccp value

  @Param
    None.

  @Returns
    None

  @Example
    Refer to the example of MCCP1_TMR_Initializer();
*/
void MCCP1_TMR_Period32BitSet( uint32_t value );
/**
  @Summary
    Provides the mccp 16-bit period value

  @Description
    This routine provides the mccp 16-bit period value

  @Param
    None.

  @Returns
    Timer 16-bit period value

  @Example
    Refer to the example of MCCP1_TMR_Initializer();
*/
uint32_t MCCP1_TMR_Period32BitGet( void );
/**
  @Summary
    Updates the mccp's 16-bit value

  @Description
    This routine updates the mccp's 16-bit value

  @Param
    None.

  @Returns
    None  @Example
    <code>
    uint16_t value=0xF0F0;

    MCCP1_TMR_Counter16BitSet(value));

    while(1)
    {
        MCCP1_TMR_Tasks();
        if( (value == MCCP1_TMR_Counter16BitGet()))
        {
            MCCP1_TMR_Stop();
        }
    }
    </code>
*/

void MCCP1_TMR_Counter32BitSet( uint32_t value );
/**
  @Summary
    Provides 16-bit current counter value

  @Description
    This routine provides 16-bit current counter value

  @Param
    None.

  @Returns
    16-bit current counter value

  @Example
    Refer to the example of MCCP1_TMR_Counter16BitSet();
*/
uint32_t MCCP1_TMR_Counter32BitGet( void );
/**
  @Summary
    Returns the elapsed status of the mccp

  @Description
    This routine returns the elapsed status of the mccp

  @Param
    None.

  @Returns
    bool - Elapsed status of the mccp.

  @Example
    Refer to the example of MCCP1_TMR_Initializer();
*/
bool MCCP1_TMR_Timer32ElapsedThenClear(void);

#ifdef __cplusplus  // Provide C++ Compatibility

    }

#endif

#endif //_MCCP1_H

/**
 End of File
*/
