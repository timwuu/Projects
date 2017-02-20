/**
  @Generated Pin Manager Header File

  @Company:
    Microchip Technology Inc.

  @File Name:
    pin_manager.h

  @Summary:
    This is the Pin Manager file generated using MPLAB(c) Code Configurator

  @Description:
    This header file provides implementations for pin APIs for all pins selected in the GUI.
    Generation Information :
        Product Revision  :  MPLAB(c) Code Configurator - 4.15
        Device            :  PIC16F18855
        Version           :  1.01
    The generated drivers are tested against the following:
        Compiler          :  XC8 1.35
        MPLAB             :  MPLAB X 3.40

    Copyright (c) 2013 - 2015 released Microchip Technology Inc.  All rights reserved.

    Microchip licenses to you the right to use, modify, copy and distribute
    Software only when embedded on a Microchip microcontroller or digital signal
    controller that is integrated into your product or third party product
    (pursuant to the sublicense terms in the accompanying license agreement).

    You should refer to the license agreement accompanying this Software for
    additional information regarding your rights and obligations.

    SOFTWARE AND DOCUMENTATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
    EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION, ANY WARRANTY OF
    MERCHANTABILITY, TITLE, NON-INFRINGEMENT AND FITNESS FOR A PARTICULAR PURPOSE.
    IN NO EVENT SHALL MICROCHIP OR ITS LICENSORS BE LIABLE OR OBLIGATED UNDER
    CONTRACT, NEGLIGENCE, STRICT LIABILITY, CONTRIBUTION, BREACH OF WARRANTY, OR
    OTHER LEGAL EQUITABLE THEORY ANY DIRECT OR INDIRECT DAMAGES OR EXPENSES
    INCLUDING BUT NOT LIMITED TO ANY INCIDENTAL, SPECIAL, INDIRECT, PUNITIVE OR
    CONSEQUENTIAL DAMAGES, LOST PROFITS OR LOST DATA, COST OF PROCUREMENT OF
    SUBSTITUTE GOODS, TECHNOLOGY, SERVICES, OR ANY CLAIMS BY THIRD PARTIES
    (INCLUDING BUT NOT LIMITED TO ANY DEFENSE THEREOF), OR OTHER SIMILAR COSTS.

*/


#ifndef PIN_MANAGER_H
#define PIN_MANAGER_H

#define INPUT   1
#define OUTPUT  0

#define HIGH    1
#define LOW     0

#define ANALOG      1
#define DIGITAL     0

#define PULL_UP_ENABLED      1
#define PULL_UP_DISABLED     0

// get/set LED_D2 aliases
#define LED_D2_TRIS               TRISAbits.TRISA0
#define LED_D2_LAT                LATAbits.LATA0
#define LED_D2_PORT               PORTAbits.RA0
#define LED_D2_WPU                WPUAbits.WPUA0
#define LED_D2_OD                ODCONAbits.ODCA0
#define LED_D2_ANS                ANSELAbits.ANSA0
#define LED_D2_SetHigh()            do { LATAbits.LATA0 = 1; } while(0)
#define LED_D2_SetLow()             do { LATAbits.LATA0 = 0; } while(0)
#define LED_D2_Toggle()             do { LATAbits.LATA0 = ~LATAbits.LATA0; } while(0)
#define LED_D2_GetValue()           PORTAbits.RA0
#define LED_D2_SetDigitalInput()    do { TRISAbits.TRISA0 = 1; } while(0)
#define LED_D2_SetDigitalOutput()   do { TRISAbits.TRISA0 = 0; } while(0)
#define LED_D2_SetPullup()      do { WPUAbits.WPUA0 = 1; } while(0)
#define LED_D2_ResetPullup()    do { WPUAbits.WPUA0 = 0; } while(0)
#define LED_D2_SetPushPull()    do { ODCONAbits.ODCA0 = 1; } while(0)
#define LED_D2_SetOpenDrain()   do { ODCONAbits.ODCA0 = 0; } while(0)
#define LED_D2_SetAnalogMode()  do { ANSELAbits.ANSA0 = 1; } while(0)
#define LED_D2_SetDigitalMode() do { ANSELAbits.ANSA0 = 0; } while(0)

// get/set LED_D3 aliases
#define LED_D3_TRIS               TRISAbits.TRISA1
#define LED_D3_LAT                LATAbits.LATA1
#define LED_D3_PORT               PORTAbits.RA1
#define LED_D3_WPU                WPUAbits.WPUA1
#define LED_D3_OD                ODCONAbits.ODCA1
#define LED_D3_ANS                ANSELAbits.ANSA1
#define LED_D3_SetHigh()            do { LATAbits.LATA1 = 1; } while(0)
#define LED_D3_SetLow()             do { LATAbits.LATA1 = 0; } while(0)
#define LED_D3_Toggle()             do { LATAbits.LATA1 = ~LATAbits.LATA1; } while(0)
#define LED_D3_GetValue()           PORTAbits.RA1
#define LED_D3_SetDigitalInput()    do { TRISAbits.TRISA1 = 1; } while(0)
#define LED_D3_SetDigitalOutput()   do { TRISAbits.TRISA1 = 0; } while(0)
#define LED_D3_SetPullup()      do { WPUAbits.WPUA1 = 1; } while(0)
#define LED_D3_ResetPullup()    do { WPUAbits.WPUA1 = 0; } while(0)
#define LED_D3_SetPushPull()    do { ODCONAbits.ODCA1 = 1; } while(0)
#define LED_D3_SetOpenDrain()   do { ODCONAbits.ODCA1 = 0; } while(0)
#define LED_D3_SetAnalogMode()  do { ANSELAbits.ANSA1 = 1; } while(0)
#define LED_D3_SetDigitalMode() do { ANSELAbits.ANSA1 = 0; } while(0)

// get/set LED_D4 aliases
#define LED_D4_TRIS               TRISAbits.TRISA2
#define LED_D4_LAT                LATAbits.LATA2
#define LED_D4_PORT               PORTAbits.RA2
#define LED_D4_WPU                WPUAbits.WPUA2
#define LED_D4_OD                ODCONAbits.ODCA2
#define LED_D4_ANS                ANSELAbits.ANSA2
#define LED_D4_SetHigh()            do { LATAbits.LATA2 = 1; } while(0)
#define LED_D4_SetLow()             do { LATAbits.LATA2 = 0; } while(0)
#define LED_D4_Toggle()             do { LATAbits.LATA2 = ~LATAbits.LATA2; } while(0)
#define LED_D4_GetValue()           PORTAbits.RA2
#define LED_D4_SetDigitalInput()    do { TRISAbits.TRISA2 = 1; } while(0)
#define LED_D4_SetDigitalOutput()   do { TRISAbits.TRISA2 = 0; } while(0)
#define LED_D4_SetPullup()      do { WPUAbits.WPUA2 = 1; } while(0)
#define LED_D4_ResetPullup()    do { WPUAbits.WPUA2 = 0; } while(0)
#define LED_D4_SetPushPull()    do { ODCONAbits.ODCA2 = 1; } while(0)
#define LED_D4_SetOpenDrain()   do { ODCONAbits.ODCA2 = 0; } while(0)
#define LED_D4_SetAnalogMode()  do { ANSELAbits.ANSA2 = 1; } while(0)
#define LED_D4_SetDigitalMode() do { ANSELAbits.ANSA2 = 0; } while(0)

// get/set LED_D5 aliases
#define LED_D5_TRIS               TRISAbits.TRISA3
#define LED_D5_LAT                LATAbits.LATA3
#define LED_D5_PORT               PORTAbits.RA3
#define LED_D5_WPU                WPUAbits.WPUA3
#define LED_D5_OD                ODCONAbits.ODCA3
#define LED_D5_ANS                ANSELAbits.ANSA3
#define LED_D5_SetHigh()            do { LATAbits.LATA3 = 1; } while(0)
#define LED_D5_SetLow()             do { LATAbits.LATA3 = 0; } while(0)
#define LED_D5_Toggle()             do { LATAbits.LATA3 = ~LATAbits.LATA3; } while(0)
#define LED_D5_GetValue()           PORTAbits.RA3
#define LED_D5_SetDigitalInput()    do { TRISAbits.TRISA3 = 1; } while(0)
#define LED_D5_SetDigitalOutput()   do { TRISAbits.TRISA3 = 0; } while(0)
#define LED_D5_SetPullup()      do { WPUAbits.WPUA3 = 1; } while(0)
#define LED_D5_ResetPullup()    do { WPUAbits.WPUA3 = 0; } while(0)
#define LED_D5_SetPushPull()    do { ODCONAbits.ODCA3 = 1; } while(0)
#define LED_D5_SetOpenDrain()   do { ODCONAbits.ODCA3 = 0; } while(0)
#define LED_D5_SetAnalogMode()  do { ANSELAbits.ANSA3 = 1; } while(0)
#define LED_D5_SetDigitalMode() do { ANSELAbits.ANSA3 = 0; } while(0)

// get/set POT_ANA4 aliases
#define POT_ANA4_TRIS               TRISAbits.TRISA4
#define POT_ANA4_LAT                LATAbits.LATA4
#define POT_ANA4_PORT               PORTAbits.RA4
#define POT_ANA4_WPU                WPUAbits.WPUA4
#define POT_ANA4_OD                ODCONAbits.ODCA4
#define POT_ANA4_ANS                ANSELAbits.ANSA4
#define POT_ANA4_SetHigh()            do { LATAbits.LATA4 = 1; } while(0)
#define POT_ANA4_SetLow()             do { LATAbits.LATA4 = 0; } while(0)
#define POT_ANA4_Toggle()             do { LATAbits.LATA4 = ~LATAbits.LATA4; } while(0)
#define POT_ANA4_GetValue()           PORTAbits.RA4
#define POT_ANA4_SetDigitalInput()    do { TRISAbits.TRISA4 = 1; } while(0)
#define POT_ANA4_SetDigitalOutput()   do { TRISAbits.TRISA4 = 0; } while(0)
#define POT_ANA4_SetPullup()      do { WPUAbits.WPUA4 = 1; } while(0)
#define POT_ANA4_ResetPullup()    do { WPUAbits.WPUA4 = 0; } while(0)
#define POT_ANA4_SetPushPull()    do { ODCONAbits.ODCA4 = 1; } while(0)
#define POT_ANA4_SetOpenDrain()   do { ODCONAbits.ODCA4 = 0; } while(0)
#define POT_ANA4_SetAnalogMode()  do { ANSELAbits.ANSA4 = 1; } while(0)
#define POT_ANA4_SetDigitalMode() do { ANSELAbits.ANSA4 = 0; } while(0)

// get/set SW_S2 aliases
#define SW_S2_TRIS               TRISAbits.TRISA5
#define SW_S2_LAT                LATAbits.LATA5
#define SW_S2_PORT               PORTAbits.RA5
#define SW_S2_WPU                WPUAbits.WPUA5
#define SW_S2_OD                ODCONAbits.ODCA5
#define SW_S2_ANS                ANSELAbits.ANSA5
#define SW_S2_SetHigh()            do { LATAbits.LATA5 = 1; } while(0)
#define SW_S2_SetLow()             do { LATAbits.LATA5 = 0; } while(0)
#define SW_S2_Toggle()             do { LATAbits.LATA5 = ~LATAbits.LATA5; } while(0)
#define SW_S2_GetValue()           PORTAbits.RA5
#define SW_S2_SetDigitalInput()    do { TRISAbits.TRISA5 = 1; } while(0)
#define SW_S2_SetDigitalOutput()   do { TRISAbits.TRISA5 = 0; } while(0)
#define SW_S2_SetPullup()      do { WPUAbits.WPUA5 = 1; } while(0)
#define SW_S2_ResetPullup()    do { WPUAbits.WPUA5 = 0; } while(0)
#define SW_S2_SetPushPull()    do { ODCONAbits.ODCA5 = 1; } while(0)
#define SW_S2_SetOpenDrain()   do { ODCONAbits.ODCA5 = 0; } while(0)
#define SW_S2_SetAnalogMode()  do { ANSELAbits.ANSA5 = 1; } while(0)
#define SW_S2_SetDigitalMode() do { ANSELAbits.ANSA5 = 0; } while(0)

// get/set ALARM2 aliases
#define ALARM2_TRIS               TRISAbits.TRISA6
#define ALARM2_LAT                LATAbits.LATA6
#define ALARM2_PORT               PORTAbits.RA6
#define ALARM2_WPU                WPUAbits.WPUA6
#define ALARM2_OD                ODCONAbits.ODCA6
#define ALARM2_ANS                ANSELAbits.ANSA6
#define ALARM2_SetHigh()            do { LATAbits.LATA6 = 1; } while(0)
#define ALARM2_SetLow()             do { LATAbits.LATA6 = 0; } while(0)
#define ALARM2_Toggle()             do { LATAbits.LATA6 = ~LATAbits.LATA6; } while(0)
#define ALARM2_GetValue()           PORTAbits.RA6
#define ALARM2_SetDigitalInput()    do { TRISAbits.TRISA6 = 1; } while(0)
#define ALARM2_SetDigitalOutput()   do { TRISAbits.TRISA6 = 0; } while(0)
#define ALARM2_SetPullup()      do { WPUAbits.WPUA6 = 1; } while(0)
#define ALARM2_ResetPullup()    do { WPUAbits.WPUA6 = 0; } while(0)
#define ALARM2_SetPushPull()    do { ODCONAbits.ODCA6 = 1; } while(0)
#define ALARM2_SetOpenDrain()   do { ODCONAbits.ODCA6 = 0; } while(0)
#define ALARM2_SetAnalogMode()  do { ANSELAbits.ANSA6 = 1; } while(0)
#define ALARM2_SetDigitalMode() do { ANSELAbits.ANSA6 = 0; } while(0)

// get/set ALARM1 aliases
#define ALARM1_TRIS               TRISAbits.TRISA7
#define ALARM1_LAT                LATAbits.LATA7
#define ALARM1_PORT               PORTAbits.RA7
#define ALARM1_WPU                WPUAbits.WPUA7
#define ALARM1_OD                ODCONAbits.ODCA7
#define ALARM1_ANS                ANSELAbits.ANSA7
#define ALARM1_SetHigh()            do { LATAbits.LATA7 = 1; } while(0)
#define ALARM1_SetLow()             do { LATAbits.LATA7 = 0; } while(0)
#define ALARM1_Toggle()             do { LATAbits.LATA7 = ~LATAbits.LATA7; } while(0)
#define ALARM1_GetValue()           PORTAbits.RA7
#define ALARM1_SetDigitalInput()    do { TRISAbits.TRISA7 = 1; } while(0)
#define ALARM1_SetDigitalOutput()   do { TRISAbits.TRISA7 = 0; } while(0)
#define ALARM1_SetPullup()      do { WPUAbits.WPUA7 = 1; } while(0)
#define ALARM1_ResetPullup()    do { WPUAbits.WPUA7 = 0; } while(0)
#define ALARM1_SetPushPull()    do { ODCONAbits.ODCA7 = 1; } while(0)
#define ALARM1_SetOpenDrain()   do { ODCONAbits.ODCA7 = 0; } while(0)
#define ALARM1_SetAnalogMode()  do { ANSELAbits.ANSA7 = 1; } while(0)
#define ALARM1_SetDigitalMode() do { ANSELAbits.ANSA7 = 0; } while(0)

// get/set F188ANA1 aliases
#define F188ANA1_TRIS               TRISBbits.TRISB0
#define F188ANA1_LAT                LATBbits.LATB0
#define F188ANA1_PORT               PORTBbits.RB0
#define F188ANA1_WPU                WPUBbits.WPUB0
#define F188ANA1_OD                ODCONBbits.ODCB0
#define F188ANA1_ANS                ANSELBbits.ANSB0
#define F188ANA1_SetHigh()            do { LATBbits.LATB0 = 1; } while(0)
#define F188ANA1_SetLow()             do { LATBbits.LATB0 = 0; } while(0)
#define F188ANA1_Toggle()             do { LATBbits.LATB0 = ~LATBbits.LATB0; } while(0)
#define F188ANA1_GetValue()           PORTBbits.RB0
#define F188ANA1_SetDigitalInput()    do { TRISBbits.TRISB0 = 1; } while(0)
#define F188ANA1_SetDigitalOutput()   do { TRISBbits.TRISB0 = 0; } while(0)
#define F188ANA1_SetPullup()      do { WPUBbits.WPUB0 = 1; } while(0)
#define F188ANA1_ResetPullup()    do { WPUBbits.WPUB0 = 0; } while(0)
#define F188ANA1_SetPushPull()    do { ODCONBbits.ODCB0 = 1; } while(0)
#define F188ANA1_SetOpenDrain()   do { ODCONBbits.ODCB0 = 0; } while(0)
#define F188ANA1_SetAnalogMode()  do { ANSELBbits.ANSB0 = 1; } while(0)
#define F188ANA1_SetDigitalMode() do { ANSELBbits.ANSB0 = 0; } while(0)

// get/set F188RST aliases
#define F188RST_TRIS               TRISBbits.TRISB1
#define F188RST_LAT                LATBbits.LATB1
#define F188RST_PORT               PORTBbits.RB1
#define F188RST_WPU                WPUBbits.WPUB1
#define F188RST_OD                ODCONBbits.ODCB1
#define F188RST_ANS                ANSELBbits.ANSB1
#define F188RST_SetHigh()            do { LATBbits.LATB1 = 1; } while(0)
#define F188RST_SetLow()             do { LATBbits.LATB1 = 0; } while(0)
#define F188RST_Toggle()             do { LATBbits.LATB1 = ~LATBbits.LATB1; } while(0)
#define F188RST_GetValue()           PORTBbits.RB1
#define F188RST_SetDigitalInput()    do { TRISBbits.TRISB1 = 1; } while(0)
#define F188RST_SetDigitalOutput()   do { TRISBbits.TRISB1 = 0; } while(0)
#define F188RST_SetPullup()      do { WPUBbits.WPUB1 = 1; } while(0)
#define F188RST_ResetPullup()    do { WPUBbits.WPUB1 = 0; } while(0)
#define F188RST_SetPushPull()    do { ODCONBbits.ODCB1 = 1; } while(0)
#define F188RST_SetOpenDrain()   do { ODCONBbits.ODCB1 = 0; } while(0)
#define F188RST_SetAnalogMode()  do { ANSELBbits.ANSB1 = 1; } while(0)
#define F188RST_SetDigitalMode() do { ANSELBbits.ANSB1 = 0; } while(0)

// get/set F188CS aliases
#define F188CS_TRIS               TRISBbits.TRISB2
#define F188CS_LAT                LATBbits.LATB2
#define F188CS_PORT               PORTBbits.RB2
#define F188CS_WPU                WPUBbits.WPUB2
#define F188CS_OD                ODCONBbits.ODCB2
#define F188CS_ANS                ANSELBbits.ANSB2
#define F188CS_SetHigh()            do { LATBbits.LATB2 = 1; } while(0)
#define F188CS_SetLow()             do { LATBbits.LATB2 = 0; } while(0)
#define F188CS_Toggle()             do { LATBbits.LATB2 = ~LATBbits.LATB2; } while(0)
#define F188CS_GetValue()           PORTBbits.RB2
#define F188CS_SetDigitalInput()    do { TRISBbits.TRISB2 = 1; } while(0)
#define F188CS_SetDigitalOutput()   do { TRISBbits.TRISB2 = 0; } while(0)
#define F188CS_SetPullup()      do { WPUBbits.WPUB2 = 1; } while(0)
#define F188CS_ResetPullup()    do { WPUBbits.WPUB2 = 0; } while(0)
#define F188CS_SetPushPull()    do { ODCONBbits.ODCB2 = 1; } while(0)
#define F188CS_SetOpenDrain()   do { ODCONBbits.ODCB2 = 0; } while(0)
#define F188CS_SetAnalogMode()  do { ANSELBbits.ANSB2 = 1; } while(0)
#define F188CS_SetDigitalMode() do { ANSELBbits.ANSB2 = 0; } while(0)

// get/set RB3 procedures
#define RB3_SetHigh()    do { LATBbits.LATB3 = 1; } while(0)
#define RB3_SetLow()   do { LATBbits.LATB3 = 0; } while(0)
#define RB3_Toggle()   do { LATBbits.LATB3 = ~LATBbits.LATB3; } while(0)
#define RB3_GetValue()         PORTBbits.RB3
#define RB3_SetDigitalInput()   do { TRISBbits.TRISB3 = 1; } while(0)
#define RB3_SetDigitalOutput()  do { TRISBbits.TRISB3 = 0; } while(0)
#define RB3_SetPullup()     do { WPUBbits.WPUB3 = 1; } while(0)
#define RB3_ResetPullup()   do { WPUBbits.WPUB3 = 0; } while(0)
#define RB3_SetAnalogMode() do { ANSELBbits.ANSB3 = 1; } while(0)
#define RB3_SetDigitalMode()do { ANSELBbits.ANSB3 = 0; } while(0)

// get/set RB4 procedures
#define RB4_SetHigh()    do { LATBbits.LATB4 = 1; } while(0)
#define RB4_SetLow()   do { LATBbits.LATB4 = 0; } while(0)
#define RB4_Toggle()   do { LATBbits.LATB4 = ~LATBbits.LATB4; } while(0)
#define RB4_GetValue()         PORTBbits.RB4
#define RB4_SetDigitalInput()   do { TRISBbits.TRISB4 = 1; } while(0)
#define RB4_SetDigitalOutput()  do { TRISBbits.TRISB4 = 0; } while(0)
#define RB4_SetPullup()     do { WPUBbits.WPUB4 = 1; } while(0)
#define RB4_ResetPullup()   do { WPUBbits.WPUB4 = 0; } while(0)
#define RB4_SetAnalogMode() do { ANSELBbits.ANSB4 = 1; } while(0)
#define RB4_SetDigitalMode()do { ANSELBbits.ANSB4 = 0; } while(0)

// get/set RB5 procedures
#define RB5_SetHigh()    do { LATBbits.LATB5 = 1; } while(0)
#define RB5_SetLow()   do { LATBbits.LATB5 = 0; } while(0)
#define RB5_Toggle()   do { LATBbits.LATB5 = ~LATBbits.LATB5; } while(0)
#define RB5_GetValue()         PORTBbits.RB5
#define RB5_SetDigitalInput()   do { TRISBbits.TRISB5 = 1; } while(0)
#define RB5_SetDigitalOutput()  do { TRISBbits.TRISB5 = 0; } while(0)
#define RB5_SetPullup()     do { WPUBbits.WPUB5 = 1; } while(0)
#define RB5_ResetPullup()   do { WPUBbits.WPUB5 = 0; } while(0)
#define RB5_SetAnalogMode() do { ANSELBbits.ANSB5 = 1; } while(0)
#define RB5_SetDigitalMode()do { ANSELBbits.ANSB5 = 0; } while(0)

// get/set RC0 procedures
#define RC0_SetHigh()    do { LATCbits.LATC0 = 1; } while(0)
#define RC0_SetLow()   do { LATCbits.LATC0 = 0; } while(0)
#define RC0_Toggle()   do { LATCbits.LATC0 = ~LATCbits.LATC0; } while(0)
#define RC0_GetValue()         PORTCbits.RC0
#define RC0_SetDigitalInput()   do { TRISCbits.TRISC0 = 1; } while(0)
#define RC0_SetDigitalOutput()  do { TRISCbits.TRISC0 = 0; } while(0)
#define RC0_SetPullup()     do { WPUCbits.WPUC0 = 1; } while(0)
#define RC0_ResetPullup()   do { WPUCbits.WPUC0 = 0; } while(0)
#define RC0_SetAnalogMode() do { ANSELCbits.ANSC0 = 1; } while(0)
#define RC0_SetDigitalMode()do { ANSELCbits.ANSC0 = 0; } while(0)

// get/set RC1 procedures
#define RC1_SetHigh()    do { LATCbits.LATC1 = 1; } while(0)
#define RC1_SetLow()   do { LATCbits.LATC1 = 0; } while(0)
#define RC1_Toggle()   do { LATCbits.LATC1 = ~LATCbits.LATC1; } while(0)
#define RC1_GetValue()         PORTCbits.RC1
#define RC1_SetDigitalInput()   do { TRISCbits.TRISC1 = 1; } while(0)
#define RC1_SetDigitalOutput()  do { TRISCbits.TRISC1 = 0; } while(0)
#define RC1_SetPullup()     do { WPUCbits.WPUC1 = 1; } while(0)
#define RC1_ResetPullup()   do { WPUCbits.WPUC1 = 0; } while(0)
#define RC1_SetAnalogMode() do { ANSELCbits.ANSC1 = 1; } while(0)
#define RC1_SetDigitalMode()do { ANSELCbits.ANSC1 = 0; } while(0)

// get/set F188INT aliases
#define F188INT_TRIS               TRISCbits.TRISC2
#define F188INT_LAT                LATCbits.LATC2
#define F188INT_PORT               PORTCbits.RC2
#define F188INT_WPU                WPUCbits.WPUC2
#define F188INT_OD                ODCONCbits.ODCC2
#define F188INT_ANS                ANSELCbits.ANSC2
#define F188INT_SetHigh()            do { LATCbits.LATC2 = 1; } while(0)
#define F188INT_SetLow()             do { LATCbits.LATC2 = 0; } while(0)
#define F188INT_Toggle()             do { LATCbits.LATC2 = ~LATCbits.LATC2; } while(0)
#define F188INT_GetValue()           PORTCbits.RC2
#define F188INT_SetDigitalInput()    do { TRISCbits.TRISC2 = 1; } while(0)
#define F188INT_SetDigitalOutput()   do { TRISCbits.TRISC2 = 0; } while(0)
#define F188INT_SetPullup()      do { WPUCbits.WPUC2 = 1; } while(0)
#define F188INT_ResetPullup()    do { WPUCbits.WPUC2 = 0; } while(0)
#define F188INT_SetPushPull()    do { ODCONCbits.ODCC2 = 1; } while(0)
#define F188INT_SetOpenDrain()   do { ODCONCbits.ODCC2 = 0; } while(0)
#define F188INT_SetAnalogMode()  do { ANSELCbits.ANSC2 = 1; } while(0)
#define F188INT_SetDigitalMode() do { ANSELCbits.ANSC2 = 0; } while(0)

// get/set F188RXM aliases
#define F188RXM_TRIS               TRISCbits.TRISC5
#define F188RXM_LAT                LATCbits.LATC5
#define F188RXM_PORT               PORTCbits.RC5
#define F188RXM_WPU                WPUCbits.WPUC5
#define F188RXM_OD                ODCONCbits.ODCC5
#define F188RXM_ANS                ANSELCbits.ANSC5
#define F188RXM_SetHigh()            do { LATCbits.LATC5 = 1; } while(0)
#define F188RXM_SetLow()             do { LATCbits.LATC5 = 0; } while(0)
#define F188RXM_Toggle()             do { LATCbits.LATC5 = ~LATCbits.LATC5; } while(0)
#define F188RXM_GetValue()           PORTCbits.RC5
#define F188RXM_SetDigitalInput()    do { TRISCbits.TRISC5 = 1; } while(0)
#define F188RXM_SetDigitalOutput()   do { TRISCbits.TRISC5 = 0; } while(0)
#define F188RXM_SetPullup()      do { WPUCbits.WPUC5 = 1; } while(0)
#define F188RXM_ResetPullup()    do { WPUCbits.WPUC5 = 0; } while(0)
#define F188RXM_SetPushPull()    do { ODCONCbits.ODCC5 = 1; } while(0)
#define F188RXM_SetOpenDrain()   do { ODCONCbits.ODCC5 = 0; } while(0)
#define F188RXM_SetAnalogMode()  do { ANSELCbits.ANSC5 = 1; } while(0)
#define F188RXM_SetDigitalMode() do { ANSELCbits.ANSC5 = 0; } while(0)

// get/set F188TXM aliases
#define F188TXM_TRIS               TRISCbits.TRISC6
#define F188TXM_LAT                LATCbits.LATC6
#define F188TXM_PORT               PORTCbits.RC6
#define F188TXM_WPU                WPUCbits.WPUC6
#define F188TXM_OD                ODCONCbits.ODCC6
#define F188TXM_ANS                ANSELCbits.ANSC6
#define F188TXM_SetHigh()            do { LATCbits.LATC6 = 1; } while(0)
#define F188TXM_SetLow()             do { LATCbits.LATC6 = 0; } while(0)
#define F188TXM_Toggle()             do { LATCbits.LATC6 = ~LATCbits.LATC6; } while(0)
#define F188TXM_GetValue()           PORTCbits.RC6
#define F188TXM_SetDigitalInput()    do { TRISCbits.TRISC6 = 1; } while(0)
#define F188TXM_SetDigitalOutput()   do { TRISCbits.TRISC6 = 0; } while(0)
#define F188TXM_SetPullup()      do { WPUCbits.WPUC6 = 1; } while(0)
#define F188TXM_ResetPullup()    do { WPUCbits.WPUC6 = 0; } while(0)
#define F188TXM_SetPushPull()    do { ODCONCbits.ODCC6 = 1; } while(0)
#define F188TXM_SetOpenDrain()   do { ODCONCbits.ODCC6 = 0; } while(0)
#define F188TXM_SetAnalogMode()  do { ANSELCbits.ANSC6 = 1; } while(0)
#define F188TXM_SetDigitalMode() do { ANSELCbits.ANSC6 = 0; } while(0)

// get/set F188PWM aliases
#define F188PWM_TRIS               TRISCbits.TRISC7
#define F188PWM_LAT                LATCbits.LATC7
#define F188PWM_PORT               PORTCbits.RC7
#define F188PWM_WPU                WPUCbits.WPUC7
#define F188PWM_OD                ODCONCbits.ODCC7
#define F188PWM_ANS                ANSELCbits.ANSC7
#define F188PWM_SetHigh()            do { LATCbits.LATC7 = 1; } while(0)
#define F188PWM_SetLow()             do { LATCbits.LATC7 = 0; } while(0)
#define F188PWM_Toggle()             do { LATCbits.LATC7 = ~LATCbits.LATC7; } while(0)
#define F188PWM_GetValue()           PORTCbits.RC7
#define F188PWM_SetDigitalInput()    do { TRISCbits.TRISC7 = 1; } while(0)
#define F188PWM_SetDigitalOutput()   do { TRISCbits.TRISC7 = 0; } while(0)
#define F188PWM_SetPullup()      do { WPUCbits.WPUC7 = 1; } while(0)
#define F188PWM_ResetPullup()    do { WPUCbits.WPUC7 = 0; } while(0)
#define F188PWM_SetPushPull()    do { ODCONCbits.ODCC7 = 1; } while(0)
#define F188PWM_SetOpenDrain()   do { ODCONCbits.ODCC7 = 0; } while(0)
#define F188PWM_SetAnalogMode()  do { ANSELCbits.ANSC7 = 1; } while(0)
#define F188PWM_SetDigitalMode() do { ANSELCbits.ANSC7 = 0; } while(0)

/**
   @Param
    none
   @Returns
    none
   @Description
    GPIO and peripheral I/O initialization
   @Example
    PIN_MANAGER_Initialize();
 */
void PIN_MANAGER_Initialize (void);

/**
 * @Param
    none
 * @Returns
    none
 * @Description
    Interrupt on Change Handling routine
 * @Example
    PIN_MANAGER_IOC();
 */
void PIN_MANAGER_IOC(void);



#endif // PIN_MANAGER_H
/**
 End of File
*/