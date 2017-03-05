#include "stdio.h"

#define CRC32_POLYNOMIAL_R 0xEDB88320

char _message[] = "123456789";

unsigned long CRC32Value(unsigned long i)
{
    int j;
    unsigned long ulCRC;
    ulCRC = i;
    for (j = 8; j > 0; j--)
    {
        if (ulCRC & 1)
            ulCRC = (ulCRC >> 1) ^ CRC32_POLYNOMIAL_R;
        else
            ulCRC >>= 1;
    //printf("-- %X\n", ulCRC);
    }
    
    //printf("%X %X\n",i, ulCRC);

    return ulCRC;
}

unsigned long CalculateBlockCRC32(
    unsigned long ulCount,
    unsigned char *ucBuffer)
{
    unsigned long ulTemp1;
    unsigned long ulTemp2;
    unsigned long ulCRC = 0xFFFFFFFF;
    while (ulCount-- != 0)
    {
        //ulTemp1 = (ulCRC >> 8) & 0x00FFFFFFL;
        //ulTemp2 = CRC32Value((ulCRC ^ *ucBuffer++) & 0xff);
        //ulCRC = ulTemp1 ^ ulTemp2;
        ulCRC = CRC32Value(ulCRC ^ *ucBuffer++);
        //printf( "%X - %X\n", ulTemp1, ulCRC);
    }
    return (~ulCRC);
}

unsigned int crc32b(unsigned char *message)
{
    int i, j;
    unsigned int byte, crc, mask;

    i = 0;
    crc = 0xFFFFFFFF;
    while (message[i] != 0)
    {
        byte = message[i]; // Get next byte.
        crc = crc ^ byte;
        for (j = 7; j >= 0; j--)
        { // Do eight times.
            //printf("-- %X\n", crc);

            mask = -(crc & 1);
            crc = (crc >> 1) ^ (CRC32_POLYNOMIAL_R & mask);

        }
        i = i + 1;
        //printf("-- %X --\n", crc);
        //printf("-------\n");

    }
    return ~crc;
}

//--------------
#define CRC32_POLYNOMIAL_N 0x04C11DB7

unsigned reverse(unsigned x) {
   x = ((x & 0x55555555) <<  1) | ((x >>  1) & 0x55555555);
   x = ((x & 0x33333333) <<  2) | ((x >>  2) & 0x33333333);
   x = ((x & 0x0F0F0F0F) <<  4) | ((x >>  4) & 0x0F0F0F0F);
   x = (x << 24) | ((x & 0xFF00) << 8) |
       ((x >> 8) & 0xFF00) | (x >> 24);
   return x;
}

unsigned long CRC32Value_N(unsigned long i)
{
    int j;
    unsigned long ulCRC;
    ulCRC = i;
    for (j = 8; j > 0; j--)
    {
        if (ulCRC & 0x80000000)
            ulCRC = (ulCRC << 1) ^ CRC32_POLYNOMIAL_N;
        else
            ulCRC <<= 1;
    }

    //printf("%X %X\n",i, reverse(ulCRC));

    return ulCRC;
}

unsigned long CalculateBlockCRC32_N(
    unsigned long ulCount,
    unsigned char *ucBuffer)
{
    unsigned long ulTemp1;
    unsigned long ulTemp2;
    unsigned long ulCRC = 0xFFFFFFFF;

    while (ulCount-- != 0)
    {
        //ulTemp1 = (ulCRC << 8) & 0xFFFFFF00L;
        //ulTemp2 = CRC32Value_N((ulCRC ^ reverse(*ucBuffer++)) & 0xff000000);
        //ulCRC = ulTemp1 ^ ulTemp2;
        ulCRC = CRC32Value_N(ulCRC ^ reverse(*ucBuffer++));
    }
    
    return reverse(~ulCRC);
}

//--------------

int main()
{
    unsigned int result;

    result = crc32b(_message);

    printf("%X\n", result);

    result = CalculateBlockCRC32(9,_message);

    printf("%X\n", result);

    result = CalculateBlockCRC32_N(9,_message);

    printf("%X", result);

    return 1;
}

//
//0xCBF43926
//0xD202D277