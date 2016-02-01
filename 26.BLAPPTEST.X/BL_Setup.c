#include <xc.h>

void _reset();
void __attribute__((interrupt,auto_psv)) _U1RXInterrupt();
void __attribute__((interrupt,auto_psv)) _USB1Interrupt();
void __attribute__((interrupt,auto_psv)) _T3Interrupt();

 void __attribute__ ((address(0xC00))) ISRTable(){
	   asm("reset"); //reset instruction to prevent runaway code
           asm("goto %0"::"i"(&_reset));           //0XC02
	   asm("goto %0"::"i"(&_U1RXInterrupt));   //0XC06
	   asm("goto %0"::"i"(&_USB1Interrupt));   //0XC0A
	   asm("goto %0"::"i"(&_T3Interrupt));     //0XC0E
        }

