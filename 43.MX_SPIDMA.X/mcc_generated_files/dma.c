/*******************************************************************************
  DMA Generated Driver File

  Company:
    Microchip Technology Inc.

  File Name:
    dma.c

  Summary:
    This is the generated driver implementation file for the DMA driver using MPLAB(c) Code Configurator

  Description:
    This source file provides implementations for driver APIs for DMA.
    Generation Information :
        Product Revision  :  MPLAB(c) Code Configurator - 4.15
        Device            :  PIC32MX250F128B
        Version           :  1.0
    The generated drivers are tested against the following:
        Compiler          :  XC32 1.40
        MPLAB             :  MPLAB X 3.40
 *******************************************************************************/

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

#include <xc.h>
#include "dma.h"
/**
  Prototype:        void DMA_Initialize(void)
  Input:            none
  Output:           none
  Description:      DMA_Initialize is an
                    initialization routine that takes inputs from the GUI.
  Comment:          
  Usage:            DMA_Initialize();
 */
void DMA_Initialize(void) 
{ 
    // DMABUSY disabled; SUSPEND Normal DMA operation; ON enabled; 
    DMACON = 0x8000;

}


void DMA_TransferCountSet(DMA_CHANNEL channel, uint16_t count)
{
    switch(channel) {
        default: break;
    }
}

void DMA_SoftwareTriggerEnable(DMA_CHANNEL channel )
{
    switch(channel) {
        default: break;
    }
}

void DMA_SourceAddressSet(DMA_CHANNEL  channel, uint32_t address) {
    switch(channel) {
        default: break;
    }    
}

void DMA_DestinationAddressSet(DMA_CHANNEL  channel, uint32_t address) {
    switch(channel) {
        default: break;
    }    
}

void DMA_SourceSizeSet(DMA_CHANNEL  channel, uint16_t sourceSize) {
    switch(channel) {
        default: break;
    }    
}

void DMA_DestinationSizeSet(DMA_CHANNEL  channel, uint16_t dstSize) {
    switch(channel) {
        default: break;
    }    
}

