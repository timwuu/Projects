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
        Product Revision  :  MPLAB(c) Code Configurator - pic24-dspic-pic32mm : v1.25
        Device            :  PIC32MM0064GPL028
        Driver Version    :  2.00
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

#include "mcc_generated_files/mcc.h"

void UART_PutUint32( uint32_t data);


#define MTAP_IDCODE  0x01
#define MTAP_SW_MTAP 0x04
#define MTAP_SW_ETAP 0x05
#define MTAP_COMMAND 0x07

//#define DEVICE_ID 0x06B0E053

//#define 2W4P_BLANK 0xCCCCCCCC

//from <Run Test/Idle>
//Send Command and goto <Run Test/Idle>
void sendCommand5b( uint8_t cmd) 
{

    //Cmd.LSB first
    // TDO:0000.cccc.c000.0000
    // TMS:1100.0000.1100.0000
    uint32_t data1= 0x44000000; //JTAG Goto <Run Test/Idle> State
    uint32_t data2= 0x44000000; //JTAG Goto <Run Test/Idle> State
    
    
    switch(cmd)
    {
        case MTAP_IDCODE:
            data1 |= 0x00008000;
            break;
        case MTAP_SW_MTAP:
            data1 |= 0x00000080;
            break;
        case MTAP_SW_ETAP:
            data1 |= 0x00008080;
            break;
        case MTAP_COMMAND:
            data1 |= 0x00008880;
            break;            
        default:
            return;
    }

    SPI1CONbits.ON = 0;
    SPI1CONbits.MODE32 = 1;
    //SPI1CONbits.MODE16 = 0;

    SPI1CONbits.CKP = 0;
    SPI1CONbits.CKE = 0;
    SPI1CONbits.SMP = 0;
     
    SDO1_SetLow();
    SCK1_SetLow();    
    SPI1CONbits.ON = 1;
    
    SPI1_Exchange( (uint8_t*)&data1, (uint8_t*)&data1 );
    SPI1_Exchange( (uint8_t*)&data2, (uint8_t*)&data2 );
    
    SPI1CONbits.ON = 0;
    
}

//from <Run Test/Idle>
//Xfer Data and goto <Run Test/Idle>
uint32_t xferData32b( uint32_t data32) 
{

    uint32_t data[6];
    
    //Cmd.LSB first
    // TDO:0000.cccc.cccc.cccc:cccc.cccc.cccc.cccc:cccc.0000.0000.0000
    // TMS:0100.0000.0000.0000.0000.0000.0000.0000:0001.1000.0000.0000
    data[0]= 0x04000000; //JTAG Goto <Run Test/Idle> State
    data[1]= 0x00000000; //JTAG Goto <Run Test/Idle> State
    data[2]= 0x00000000; //JTAG Goto <Run Test/Idle> State
    data[3]= 0x00000000; //JTAG Goto <Run Test/Idle> State
    data[4]= 0x00044000; //JTAG Goto <Run Test/Idle> State
    data[5]= 0x00000000; //JTAG Goto <Run Test/Idle> State

    uint32_t result;
    uint32_t _bit;
    uint32_t _mask;
    uint32_t i,j;
    
    SPI1CONbits.ON = 0;
    SPI1CONbits.MODE32 = 1;
    //SPI1CONbits.MODE16 = 0;

    SPI1CONbits.CKP = 0;
    SPI1CONbits.CKE = 0;
    SPI1CONbits.SMP = 0;
     
    SDO1_SetLow();
    SCK1_SetLow();    
    SPI1CONbits.ON = 1;
    
    SPI1_Exchange( (uint8_t*)&data[0], (uint8_t*)&data[0] );
    SPI1_Exchange( (uint8_t*)&data[1], (uint8_t*)&data[1] );
    SPI1_Exchange( (uint8_t*)&data[2], (uint8_t*)&data[2] );
    SPI1_Exchange( (uint8_t*)&data[3], (uint8_t*)&data[3] );
    SPI1_Exchange( (uint8_t*)&data[4], (uint8_t*)&data[4] );
    SPI1_Exchange( (uint8_t*)&data[5], (uint8_t*)&data[5] );
    
    SPI1CONbits.ON = 0;
    
    result=0x00000000;
    _bit  =0x00000001;
    _mask =0x00030000;

    for( i=0,j=0; i<32; i++)
    {
        if( data[j] & _mask) result |= _bit;
        
        _mask = _mask >> 4;
        _bit  = _bit << 1;
        if(_mask==0x0)
        {
            _mask=0x30000000;
            j++;
        }
    }
    
//    if( data[0] & 0x00030000) result |= 0x01;   
//    if( data[0] & 0x00003000) result |= 0x02;
//    if( data[0] & 0x00000300) result |= 0x04;
//    if( data[0] & 0x00000030) result |= 0x08;
//    if( data[0] & 0x00000003) result |= 0x10;
//    if( data[1] & 0x30000000) result |= 0x20;
//    if( data[1] & 0x03000000) result |= 0x40;
//    if( data[1] & 0x00300000) result |= 0x80;
//    if( data[1] & 0x00030000) result |= 0x100;
//    if( data[1] & 0x00003000) result |= 0x200;
//    if( data[1] & 0x00000300) result |= 0x400;
//    if( data[1] & 0x00000030) result |= 0x800;
//    if( data[1] & 0x00000003) result |= 0x1000;
//    if( data[2] & 0x30000000) result |= 0x2000;
//    if( data[2] & 0x03000000) result |= 0x4000;
//    if( data[2] & 0x00300000) result |= 0x8000;
//    if( data[2] & 0x00030000) result |= 0x10000;
//    if( data[2] & 0x00003000) result |= 0x20000;
//    if( data[2] & 0x00000300) result |= 0x40000;
//    if( data[2] & 0x00000030) result |= 0x80000;
//    if( data[2] & 0x00000003) result |= 0x100000;
//    
//    if( data[3] & 0x30000000) result |= 0x200000;
//    if( data[3] & 0x03000000) result |= 0x400000;
//    if( data[3] & 0x00300000) result |= 0x800000;
//    if( data[3] & 0x00030000) result |= 0x1000000;
//    if( data[3] & 0x00003000) result |= 0x2000000;
//    if( data[3] & 0x00000300) result |= 0x4000000;
//    if( data[3] & 0x00000030) result |= 0x8000000;
//    if( data[3] & 0x00000003) result |= 0x1000000;
//
//    if( data[4] & 0x30000000) result |= 0x20000000;
//    if( data[4] & 0x03000000) result |= 0x40000000;
//    if( data[4] & 0x00300000) result |= 0x80000000;
    
    return result;
    
}

void setTestLogicResetMode()
{

    uint32_t data= 0x44444444; //JTAG Goto <Test Logic Reset> State

    SPI1CONbits.ON = 0;
    SPI1CONbits.MODE32 = 1;
    //SPI1CONbits.MODE16 = 0;

    SPI1CONbits.CKP = 0;
    SPI1CONbits.CKE = 0;
    SPI1CONbits.SMP = 0;
     
    SDO1_SetLow();
    SCK1_SetLow();    
    SPI1CONbits.ON = 1;
    
    SPI1_Exchange( (uint8_t*)&data, (uint8_t*)&data );
    
    SPI1CONbits.ON = 0;
    
}

void setRunTestIdleMode()
{

    uint32_t data= 0x44444000; //JTAG Goto <Run Test/Idle> State

    SPI1CONbits.ON = 0;
    SPI1CONbits.MODE32 = 1;
    //SPI1CONbits.MODE16 = 0;

    SPI1CONbits.CKP = 0;
    SPI1CONbits.CKE = 0;
    SPI1CONbits.SMP = 0;
     
    SDO1_SetLow();
    SCK1_SetLow();    
    SPI1CONbits.ON = 1;
    
    SPI1_Exchange( (uint8_t*)&data, (uint8_t*)&data );

    SPI1CONbits.ON = 0;    
}


void enterICSP()
{
    uint32_t data= 0x4D434850; //ICSP Entry Code

    SPI1CONbits.ON = 0;
    SPI1CONbits.MODE32 = 1;

    SPI1CONbits.CKP = 0;
    SPI1CONbits.CKE = 1;
    
    
    SDO1_SetLow();
    SCK1_SetLow();    
    SPI1CONbits.ON = 1;
   
    TG_MCLR_SetLow();     //MCLR_LOW P6(100ns)
    TG_MCLR_SetLow();
    TG_MCLR_SetLow();    
    TG_MCLR_SetHigh();    //MCLR_HIGH < P14+P20 (501us)
    TG_MCLR_SetLow();     //MCLR_LOW P18(40ns)
    
    SPI1_Exchange( (uint8_t*)&data, (uint8_t*)&data );
        
    //Delay P19(40ns)
    TG_MCLR_SetHigh();    //MCLR_HIGH P7(500ns)
    
    SPI1CONbits.ON = 0;
    SDO1_SetLow();
    SCK1_SetLow();

}

void exitICSP()
{

    uint32_t data= 0x44444444; //JTAG Goto <Test Logic Reset> State

    SPI1CONbits.ON = 0;
    SPI1CONbits.MODE32 = 1;
    //SPI1CONbits.MODE16 = 0;

    SPI1CONbits.CKP = 0;
    SPI1CONbits.CKE = 0;
    SPI1CONbits.SMP = 0;
     
    SDO1_SetLow();
    SCK1_SetLow();    
    SPI1CONbits.ON = 1;
    
    SPI1_Exchange( (uint8_t*)&data, (uint8_t*)&data );
    
    //TG_MCLR_SetHigh();   //MCLR_HIGH P16(0ns)
    TG_MCLR_SetLow();    // < MCLR_LOW P17(100ns)
    TG_MCLR_SetLow();    //

    SPI1CONbits.ON = 0;
    SDO1_SetLow();
    SCK1_SetLow();

    //Send a pulse SCK1.H
    SCK1_SetHigh();
    SCK1_SetHigh();
    SCK1_SetLow();
    
}

void processCmd( uint8_t cmd)
{

    
               switch(cmd)
           {
               case 'D':
                   enterICSP();
                   
                   setRunTestIdleMode();
                   
                   sendCommand5b( MTAP_IDCODE);
                   UART_PutUint32(xferData32b(0x00));
                   
                   exitICSP();
                   break;
               
               case 'I':
                   enterICSP();
                   break;

               case 'E':
                   exitICSP();
                   break;

               case 'H':
                   TG_MCLR_SetHigh();
                   break;
                   
               case 'L':
                   TG_MCLR_SetLow();
                   break;
                   
               default:
                   break;              
           }

}


/*
                         Main application
 */
void main(void)
{
    uint32_t cnt;
    uint8_t cmd;
    
    // initialize the device
    SYSTEM_Initialize();

    // When using interrupts, you need to set the Global Interrupt Enable bits
    // Use the following macros to:

    // Enable the Global Interrupts
    INTERRUPT_GlobalEnable();

    // Disable the Global Interrupts
    //INTERRUPT_GlobalDisable();

    LATBINV = _LATB_LATB5_MASK;
    
    TG_MCLR_SetHigh();  //Start up Target PIC
                   
    while (1)
    {
        // Add your application code
        
        //for( cnt=0; cnt<10000000; cnt++);

        //LATBINV = _LATB_LATB5_MASK;

        //UART2_Write( '-');           

        
        //loopback
        if( !UART2_ReceiveBufferIsEmpty())
        {
           cmd = UART2_Read();
            
           while( UART2_TransmitBufferIsFull());
                   
           LATBINV = _LATB_LATB5_MASK;
           
           UART2_Write( cmd);

           processCmd( cmd);
           
        }
            
    }
}


void UART_PutUint32( uint32_t data)
{
    int32_t i;
    uint32_t tmp;
    uint8_t c, str[8];   
    
    tmp=data;
    
    for( i=7; i>=0; i--)
    {
        c= (uint8_t)(tmp & 0x0F);
        
        if( c<0x0A) c+= '0';
        else c+='A'-10;
        
        str[i]=c;
        
        tmp = tmp>>4;
    }
    
    for( i=0; i<=7; i++)
    {
        while( UART2_TransmitBufferIsFull());
        UART2_Write(str[i]);
    }
    //UART2_WriteBuffer( &str[0],8);
    
}
/**
 End of File
*/