/* 
 * File:   main.c
 * Author: Timijk
 *
 * Created on February 12, 2017, 12:29 PM
 */

#include <xc.h>
#include <stdint.h>
/*
 * 
 */
void main(void) {

    uint32_t cnt=0;
    
    ANSELA = 0x0000; //Disable ANALOG mode
    TRISACLR = _LATA_LATA0_MASK | _LATA_LATA1_MASK;
    
    LATASET = _LATA_LATA0_MASK | _LATA_LATA1_MASK;
    
    while(1)
    {
        for( cnt=0; cnt<0x4000000; cnt++)
        {
            if( (cnt & 0xFFFFFF) ==0) LATAINV= _LATA_LATA1_MASK;  //LED4

        }
        
        LATAINV= _LATA_LATA0_MASK;  //LED3
        
    }
    
    return;
}

