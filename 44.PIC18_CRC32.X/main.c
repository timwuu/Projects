/* 
 * File:   main.c
 * Author: Tim
 *
 * Created on March 5, 2017, 1:58 PM
 */

#include <xc.h>

void Calc_CRC32();

unsigned char CRCi;
unsigned char CRC0;
unsigned char CRC1;
unsigned char CRC2;
unsigned char CRC3;

/*
 * 
 */
void main(void) {

    Calc_CRC32();
    
    Calc_CRC32();
    
    return;
}

