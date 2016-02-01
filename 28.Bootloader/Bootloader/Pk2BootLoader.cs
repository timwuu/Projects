using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
//using System.Threading;
//using Pk2 = PICkit2V2.PICkitFunctions;
//using KONST = PICkit2V2.Constants;

namespace PICkit2V2
{
    class Pk2BootLoader
    {
        //4:<STX>*2+<CHKSUM>+<ETX>
        //5:<STX>*2+<DLE><CHKSUM>+<ETX>
        //261:<COM>+<LEN>+<ADDRL>+<ADDRH>+<ADDRU>+<DAT>*256
        //261*2:<DLE><COM>+<DEL><LEN>+<DLE><ADDRx>*3+{<DLE><DAT>}*256
        public const int BUFFSIZE = 5 + 261 * 2;   //<STX><STX>[<DATA>...]<CHKSUM><0xETX>

        const Byte STX = 0x55;
        const Byte ETX = 0x04;
        const Byte DLE = 0x05;

        const Byte cmdRESET = 0xFF; //<STX><STX><0xFF><0x00>
        const Byte cmdRD_VER = 0x00;
        const Byte cmdRD_FLASH = 0x01;
        const Byte cmdWT_FLASH = 0x02;
        const Byte cmdER_FLASH = 0x03;
        const Byte cmdRD_EEDATA = 0x04;
        const Byte cmdWT_EEDATA = 0x05;
        const Byte cmdRD_CONFIG = 0x06;
        const Byte cmdWT_CONFIG = 0x07;
        const Byte cmdVERIFY_OK = 0x08;
        const Byte cmdREPEAT = 0xFF;   //<STX><STX><CHKSUM><ETX>
        const Byte cmdREPLICATE = 0x02;  //<STX><STX><0x02><LEN><ADDRL><ADDRH><ADDRU><CHKSUM><ETX>


        public static Byte[] wrBuff = new Byte[BUFFSIZE];
        public static int wrBuffCnt;

        public static Byte[] Pk2_Usb_read_array = new Byte[BT_HC04.Form1.BUFFSIZE];

        static BackgroundWorker bkgWk;

        //timwuu 2015.04.19
        public const int PM_ROW_SIZE = 0x100;
        public const UInt32 PM_ROW_ADDRESS_MASK = 0xFFFFFF00;
        //public const int PM_ROW_SIZE = 0x20;  //PIC18F2550
        //public const UInt16 PM_ROW_ADDRESS_MASK = 0xFFE0;   //PIC18F2550

        public const int PM_PAGE_SIZE = 2048;

        //PIC18F2550
        //const UInt16 USER_ADDR_LOW = 0x2000;
        //const UInt16 USER_ADDR_HI = 0x7FE0;
        const UInt16 USER_ADDR_LOW = 0xC00;  // >= USER_ADDR_LOW
        const UInt16 USER_ADDR_HI = 0xABF8;  // < USER_ADDR_HI


        //const UInt16 BYTES_PER_ADDR = 1;   //PIC18F2550
        const UInt16 BYTES_PER_ADDR = 2;

        public static volatile byte chkMsgRecievedCnt = 0;  //using to wait for msg from bootloader

        public static int tkProgress;

        static int userAppRowLen;

        static int processCmdBuff(byte[] cmdBuff, int length)
        {
            int i, j = 0;

            Byte checkSum = 0;

            // two start signal <STX>
            wrBuff[j++] = STX;
            wrBuff[j++] = STX;

            for (i = 0; i < length; i++, j++)
            {
                switch (cmdBuff[i])
                {
                    case STX:
                    case ETX:
                    case DLE:
                        wrBuff[j++] = DLE;
                        break;

                    default:
                        break;

                }

                checkSum += cmdBuff[i];
                wrBuff[j] = cmdBuff[i];
            }

            //<checksum><ETX>
            Byte cksum = (Byte)((~checkSum + 1) & 0xFF);

            switch (cksum)
            {
                case STX:
                case ETX:
                case DLE:
                    wrBuff[j++] = DLE;
                    break;

                default:
                    break;
            }

            wrBuff[j++] = cksum;
            wrBuff[j++] = ETX;

            return j;
        }

        private static void Pk2_BL_WriteFlash(byte[] data)
        {
            int dataAddr;

            if (BYTES_PER_ADDR != 1)
            {
                dataAddr = data[2] | data[3] << 8 | data[4] << 16;

                dataAddr /= BYTES_PER_ADDR;

                data[2] = (byte)(dataAddr & 0xFF);
                data[3] = (byte)((dataAddr >> 8) & 0xFF);
                data[4] = (byte)((dataAddr >> 16) & 0xFF);

            }

            //check configuration bits

            if (dataAddr == (int)(USER_ADDR_HI & PM_ROW_ADDRESS_MASK))
            {
                return;
            }

            wrBuffCnt = processCmdBuff(data, data.Length);

            tkProgress++;

            bkgWk.ReportProgress((int)(tkProgress * 100 / userAppRowLen));

            int retry = 20;

            //<STX><STX><02><CHKSUM><ETX>
            chkMsgRecievedCnt = 5;

            while (chkMsgRecievedCnt != 0)
            {
                Thread.Sleep(20);
                if (retry-- == 0)
                {
                    wrBuffCnt = processCmdBuff(data, data.Length);
                    retry = 20;
                    chkMsgRecievedCnt = 5;

                }

            }


        }

        private static void Pk2_BL_ReadFlash16(int addr)

        { }


        public static bool ReadHexAndDownload(BackgroundWorker wrk, string fileName)
        {
            bkgWk = wrk;

            tkProgress = 0;

            try
            {
                FileInfo hexFile = new FileInfo(fileName);
                TextReader hexRead = hexFile.OpenText();

                // <cmd><len> 3 address bytes plus 32 data bytes.
                byte[] flashWriteData = new byte[5 + PM_ROW_SIZE];

                flashWriteData[0] = cmdWT_FLASH;
                flashWriteData[1] = 1;   // One Row of PM_ROM_SIZE

                string fileLine = hexRead.ReadLine();

                for (int x = 2; x < flashWriteData.Length; x++)
                { // clear array for skipped bytes in hex file
                    flashWriteData[x] = 0xFF;
                }

                UInt32 rowAddress = 0;
                UInt32 prvRowAddress = 0;
                UInt32 segmentAddress = 0;


                while (fileLine != null)
                {
                    if ((fileLine[0] == ':') && (fileLine.Length >= 11))
                    { // skip line if not hex line entry,or not minimum length ":BBAAAATTCC"
                        int byteCount = Int32.Parse(fileLine.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                        UInt32 fileAddress = UInt32.Parse(fileLine.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
                        int recordType = Int32.Parse(fileLine.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);

                        if (recordType == 2 || recordType == 4)
                        {
                            // extended address format
                            segmentAddress = UInt32.Parse(fileLine.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);

                            if (recordType == 2)
                            {
                                segmentAddress <<= 4;
                            }
                            else
                            {
                                segmentAddress <<= 16;
                                if(segmentAddress!=00)
                                {

                                    segmentAddress = segmentAddress;
                                }
                            }

                            fileLine = hexRead.ReadLine();
                            continue;

                            //MessageBox.Show("Extended Address > 0", "Read Hex and Download", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }


                        fileAddress = segmentAddress + fileAddress;

                        //rowAddress = fileAddress & 0xFFE0; timwuu 2015.04.19
                        rowAddress = fileAddress & PM_ROW_ADDRESS_MASK;

                        if (prvRowAddress != 0 && prvRowAddress != rowAddress)
                        {
                            Pk2_BL_WriteFlash(flashWriteData);
                            for (int x = 0; x < flashWriteData.Length; x++)
                            { // clear array for skipped bytes in hex file
                                flashWriteData[x] = 0xFF;
                            }
                            prvRowAddress = 0;
                        }

                        if (recordType == 0)
                        { // Data Record}
                            if ((fileAddress >= USER_ADDR_LOW * BYTES_PER_ADDR) && (fileAddress < USER_ADDR_HI * BYTES_PER_ADDR))
                            { // don't program 5555 key at last address until after verification.
                                if (prvRowAddress != rowAddress)
                                {
                                    flashWriteData[2] = (byte)(rowAddress & 0xFF);
                                    flashWriteData[3] = (byte)((rowAddress >> 8) & 0xFF);
                                    flashWriteData[4] = (byte)((rowAddress >> 16) & 0xFF);  // address upper
                                    prvRowAddress = rowAddress;
                                }

                                if (fileLine.Length >= (11 + (2 * byteCount)))
                                { // skip if line isn't long enough for bytecount.                    
                                    int addrIdx = (int)( fileAddress & (PM_ROW_SIZE - 1));

                                    int offset = 5;

                                    for (int j = 0; j < byteCount; j++)
                                    {
                                        uint wordByte = UInt32.Parse(fileLine.Substring((9 + (2 * j)), 2), System.Globalization.NumberStyles.HexNumber);
                                        flashWriteData[offset + addrIdx] = (byte)(wordByte & 0xFF);

                                        addrIdx++;

                                        if (addrIdx == PM_ROW_SIZE)
                                        {
                                            Pk2_BL_WriteFlash(flashWriteData);
                                            for (int x = 2; x < flashWriteData.Length; x++)
                                            { // clear array for skipped bytes in hex file
                                                flashWriteData[x] = 0xFF;
                                            }
                                            prvRowAddress = 0;

                                            if (j < byteCount - 1)
                                            {
                                                rowAddress = (fileAddress + PM_ROW_SIZE) & PM_ROW_ADDRESS_MASK;
                                                flashWriteData[2] = (byte)(rowAddress & 0xFF);
                                                flashWriteData[3] = (byte)((rowAddress >> 8) & 0xFF);
                                                flashWriteData[4] = (byte)((rowAddress >> 16) & 0xFF);  // address upper
                                                prvRowAddress = rowAddress;
                                                addrIdx = 0;

                                            }
                                        }

                                    }

                                }
                            }

                        } // end if (recordType == 0)  



                        if (recordType == 1)
                        { // end of record
                            break;
                        }
                    }

                    fileLine = hexRead.ReadLine();

                } //while
                if (prvRowAddress != 0) Pk2_BL_WriteFlash(flashWriteData); // write last row
                hexRead.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Read Hex & Downloading", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public static bool ReadHexAndVerify(string fileName)
        {
            try
            {
                FileInfo hexFile = new FileInfo(fileName);
                TextReader hexRead = hexFile.OpenText();
                string fileLine = hexRead.ReadLine();
                bool verified = true;
                int lastAddress = 0;
                int usbRdAddr = 0;
                while (fileLine != null)
                {
                    if ((fileLine[0] == ':') && (fileLine.Length >= 11))
                    { // skip line if not hex line entry,or not minimum length ":BBAAAATTCC"
                        int byteCount = Int32.Parse(fileLine.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                        int fileAddress = Int32.Parse(fileLine.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
                        int recordType = Int32.Parse(fileLine.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);

                        if (recordType == 0)
                        { // Data Record}
                            if ((fileAddress >= 0x2000) && (fileAddress < 0x7FE0))
                            { // don't check bootloader stuff.
                                int startByte = fileAddress & 0x000F; // read 16 bytes at a time.
                                int firstAddress = fileAddress & 0xFFF0;
                                if (lastAddress != firstAddress)
                                { // only read if next line in different 16-byte block
                                    Pk2_BL_ReadFlash16(firstAddress);
                                }
                                if (fileLine.Length >= (11 + (2 * byteCount)))
                                { // skip if line isn't long enough for bytecount.

                                    usbRdAddr = startByte;

                                    for (int lineByte = 0; lineByte < byteCount; lineByte++)
                                    {
                                        // get the byte value from hex file
                                        uint wordByte = UInt32.Parse(fileLine.Substring((9 + (2 * lineByte)), 2), System.Globalization.NumberStyles.HexNumber);

                                        if (usbRdAddr == 0x10)
                                        {
                                            firstAddress += 0x10;
                                            Pk2_BL_ReadFlash16(firstAddress);
                                            usbRdAddr = 0;
                                        }

                                        if (Pk2_Usb_read_array[6 + usbRdAddr++] != (byte)(wordByte & 0xFF))
                                        {
                                            verified = false;
                                            recordType = 1;
                                            break;
                                        }
                                    }
                                }
                                lastAddress = firstAddress;
                            }
                        } // end if (recordType == 0)  



                        if (recordType == 1)
                        { // end of record
                            break;
                        }
                    }
                    fileLine = hexRead.ReadLine();

                }
                hexRead.Close();
                return verified;
            }
            catch
            {
                return false;
            }

        }

        public static void calculatePageUsage(string filename, ref byte addrL, ref byte addrH, ref byte addrU, ref byte blockLen)
        {
            UInt32 minPrgAddress = 0xFFFFFFFF;
            UInt32 maxPrgAddress = 0;
            UInt32 segmentAddress = 0;

            UInt32 tmpAddr = 0;

            FileInfo hexFile = new FileInfo(filename);
            TextReader hexRead = hexFile.OpenText();

            string fileLine = hexRead.ReadLine();


            while (fileLine != null)
            {
                if ((fileLine[0] == ':') && (fileLine.Length >= 11))
                { // skip line if not hex line entry,or not minimum length ":BBAAAATTCC"
                    int byteCount = Int32.Parse(fileLine.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
                    int fileAddress = Int32.Parse(fileLine.Substring(3, 4), System.Globalization.NumberStyles.HexNumber);
                    int recordType = Int32.Parse(fileLine.Substring(7, 2), System.Globalization.NumberStyles.HexNumber);

                    tmpAddr = segmentAddress + (UInt32)fileAddress;

                    if (recordType == 0)
                    { // Data Record

                        //check for boundary

                        if ((tmpAddr >= USER_ADDR_LOW * BYTES_PER_ADDR) && (tmpAddr < USER_ADDR_HI * BYTES_PER_ADDR))
                        {
                            if (tmpAddr < minPrgAddress) minPrgAddress = tmpAddr;
                            if (tmpAddr + byteCount > maxPrgAddress) maxPrgAddress = tmpAddr;
                        }
                    }

                    if (recordType == 2 || recordType == 4)
                    { // Extended Address
                        segmentAddress = UInt32.Parse(fileLine.Substring(9, 4), System.Globalization.NumberStyles.HexNumber);

                        if (recordType == 2)
                        {
                            segmentAddress <<= 4;
                        }
                        else
                        {
                            segmentAddress <<= 16;
                        }

                        if (segmentAddress != 00)
                        {

                            segmentAddress = segmentAddress;
                        }
                    }

                    if (recordType == 1)
                    { // End of Record
                        break;
                    }
                }//end of if

                fileLine = hexRead.ReadLine();

            }//end of while


            //align PrgAddress to page boundaries for erase operation

            minPrgAddress = (UInt32)(minPrgAddress & (~(PM_PAGE_SIZE - 1)));

            blockLen = (byte)((maxPrgAddress - minPrgAddress) / PM_PAGE_SIZE + 1);

            userAppRowLen = (int)((maxPrgAddress - minPrgAddress) / PM_ROW_SIZE + 1);

            minPrgAddress /= BYTES_PER_ADDR;

            addrL = (byte)(minPrgAddress & 0xFF);
            addrH = (byte)(minPrgAddress >> 8 & 0xFF);
            addrU = (byte)(minPrgAddress >> 16 & 0xFF);

        }//end of function


    }
}
