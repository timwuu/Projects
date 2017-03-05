/**
  Generated Main Source File

  Company:
    Microchip Technology Inc.

  File Name:
    main.c

  Summary:
    This is the main file generated using MPLAB(c) Code Configurator

  Description:
    This header file provides implementations for driver APIs for all modules selected in the GUI.
    Generation Information :
        Product Revision  :  MPLAB(c) Code Configurator - 4.15
        Device            :  PIC32MX250F128B
        Driver Version    :  2.00
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

#include "mcc_generated_files/mcc.h"


uint8_t data;

/*
                         Main application
 */

inline uint8_t SPI2_RXTX_EBUF_8bit(uint8_t data){
    while(SPI2STATbits.SPITBF);
    SPI2BUF = data;
    return SPI2BUF;
}

int main(void)
{
    uint32_t dummy;
    
    data = 0xAA;
    
    // initialize the device
    SYSTEM_Initialize();

    while (1)
    {
        // Add your application code
        if(TMR2_GetElapsedThenClear())
        {
            LATBINV= _LATB_LATB5_MASK;
            
            //set slave SPI
            //SPI2BUF = 0xAA;  //01010101
            SPI2_RXTX_EBUF_8bit(0xAA);
            
            SPI1BUF = 0xAA;
            
            //read back data
            // while ( SPI1STATbits.SPIRBE == true);
            // while ( SPI2STATbits.SPIRBE == true);
            //dummy = SPI2BUF;
            //dummy = SPI1BUF;

            if( TMR2_Period16BitGet()!=0x30D4)
            {            
                if( SPI1STATbits.SPIROV)
                {
                    TMR2_Stop();

                    TMR2_Period16BitSet(0x30D4);

                    TMR2_Start();
                }
            }
        }
    }

    return -1;
}
/**
 End of File
*/