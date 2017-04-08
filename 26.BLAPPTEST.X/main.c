/*
 * File:   newmainXC16.c
 * Author: Tim
 *
 * Created on April 18, 2015, 10:01 AM
 */

#include <xc.h>
//#include "p24FJ64GB002.h"

/* Definitions *****************************************************/
#define STOP_TIMER_IN_IDLE_MODE     0x2000
#define TIMER_SOURCE_INTERNAL       0x0000
#define TIMER_SOURCE_EXTERNAL       0x0002
#define TIMER_ON                    0x8000
#define GATED_TIME_DISABLED         0x0000
#define TIMER_16BIT_MODE            0x0000

#define TIMER_PRESCALER_1           0x0000
#define TIMER_PRESCALER_8           0x0010
#define TIMER_PRESCALER_64          0x0020
#define TIMER_PRESCALER_256         0x0030
#define TIMER_INTERRUPT_PRIORITY    0x0001
#define TIMER_INTERRUPT_PRIORITY_4  0x0004

//int j;

void setup()
{
    
  //  j=1;
    
    AD1PCFG = 0xFFFF;

    TRISA = 0xFFFF;

    TRISB = 0xFFFF;
    
    TRISBbits.TRISB2=0;
    TRISBbits.TRISB3=0;
    
    LATBbits.LATB2=0;
    
    
    T2CONbits.T32 = 0;  //timwuu because bootloader is using T32=1...
    T3CON = 0;
    IPC2bits.T3IP = TIMER_INTERRUPT_PRIORITY;
    IFS0bits.T3IF = 0;

    TMR3 = 0;

    PR3 = 31249;
    T3CON = TIMER_ON |
            STOP_TIMER_IN_IDLE_MODE |
            TIMER_SOURCE_INTERNAL |
            GATED_TIME_DISABLED |
            TIMER_16BIT_MODE |
            TIMER_PRESCALER_256;
            
    IEC0bits.T3IE = 1 ;

    
}

void loop()
{
    int i=0;

    while(1)
    {
        if(i==0) LATBbits.LATB3 ^= 1;
        //if(IFS0bits.T3IF) { LATBbits.LATB2 ^= 1; IFS0bits.T3IF=0;}
        
        i++;
    }

}


int main(void) {
    setup();
    loop();

    return 0;
}

void __attribute__ ( ( __interrupt__ , auto_psv ) ) _T3Interrupt ( void )
{
    LATBbits.LATB2 ^= 1;
    IFS0bits.T3IF = 0 ;

}

void __attribute__ ( ( __interrupt__ , auto_psv ) ) _USB1Interrupt ( void )
{

}

void __attribute__ ( ( __interrupt__ , auto_psv ) ) _U1RXInterrupt ( void )
{

}
