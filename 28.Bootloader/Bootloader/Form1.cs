using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace BT_HC04
{
    public partial class Form1 : Form
    {

        //4:<STX>*2+<CHKSUM>+<ETX>
        //261:<COM>+<LEN>+<ADDRL>+<ADDRH>+<ADDRU>+<DAT>*256
        //261*2:<DLE><COM>+<DEL><LEN>+<DLE><ADDRx>*3+{<DLE><DAT>}*256
        public const int BUFFSIZE = 4 + 261 * 2;   //<STX><STX>[<DATA>...]<CHKSUM><0xETX>

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


        Byte[] wrBuff = new Byte[BUFFSIZE];
        Byte[] cmdBuff = new Byte[BUFFSIZE];

        string hexfilename;

        public Form1()
        {
            InitializeComponent();
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //  if (!PICkit2V2.Pk2BootLoader.chkMsgRecieved) PICkit2V2.Pk2BootLoader.chkMsgRecieved = true;

            //   toolStripProgressBar1.Value = (int)PICkit2V2.Pk2BootLoader.tkProgress;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int c;

            if (serialPort1.BytesToRead > 0)
            {
                timer1.Enabled = false;

                while (serialPort1.BytesToRead > 0)
                {
                    c = serialPort1.ReadByte();

                    richTextBox1.AppendText(c.ToString("X2"));
                    richTextBox1.AppendText(" ");


                    if (PICkit2V2.Pk2BootLoader.chkMsgRecievedCnt > 0) PICkit2V2.Pk2BootLoader.chkMsgRecievedCnt--;

                }

                timer1.Enabled = true;
            }
        }


        /// <summary>
        /// packet format <STX><STX>[<DATA><DATA>...]<CHKSUM><ETX>
        /// </summary>
        int processCmdBuff(int length)
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
            wrBuff[j++] = (Byte)((~checkSum + 1) & 0xFF);
            wrBuff[j++] = ETX;

            return j;
        }

        /// <summary>
        /// [<0x00><0x02>]
        /// </summary>
        void readVersion()
        {
            int cnt = 0;

            cmdBuff[0] = cmdRD_VER;
            cmdBuff[1] = 0x02;

            cnt = processCmdBuff(2);

            serialPort1.Write(wrBuff, 0, cnt);

        }

        /// <summary>
        /// [<0x03><LEN><ADDRL><ADDRH><ADDRU>]
        /// LEN: number of PAGES to be erased
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnERASE_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Continue?", "Erase Flash", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                return;

            eraseFlash(0x00, 0xC0, 0x00, 0x01);
        }

        //AddrX: Address of Word (not Bytes)
        private void eraseFlash(Byte AddrL, Byte AddrH, Byte AddrU, Byte BlockLength)
        {
            int cnt = 0;

            cmdBuff[0] = cmdER_FLASH;
            cmdBuff[1] = BlockLength;
            cmdBuff[2] = AddrL;
            cmdBuff[3] = AddrH;
            cmdBuff[4] = AddrU;

            cnt = processCmdBuff(5);

            serialPort1.Write(wrBuff, 0, cnt);
        }

        /// <summary>
        /// [<0x01><LEN><ADDRL><ADDRH><ADDRU>]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRDFlash_Click(object sender, EventArgs e)
        {
            checkComPort();

            readFlash();
        }

        private void readFlash()
        {
            int cnt = 0;

            cmdBuff[0] = cmdRD_FLASH;
            cmdBuff[1] = 0x20;  // number of instructions
            cmdBuff[2] = 0x00;  // source address
            cmdBuff[3] = 0x00;
            cmdBuff[4] = 0x00;

            cnt = processCmdBuff(5);

            serialPort1.Write(wrBuff, 0, cnt);

        }

        private void reset()
        {
            int cnt = 0;

            cmdBuff[0] = cmdRESET;
            cmdBuff[1] = 0x00;

            cnt = processCmdBuff(2);

            serialPort1.Write(wrBuff, 0, cnt);
        }


        private void btnBLVer_Click(object sender, EventArgs e)
        {
            checkComPort();

            readVersion();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            checkComPort();

            reset();
        }

        private void checkComPort()
        {
            if (!serialPort1.IsOpen) serialPort1.Open();

            if (!timer1.Enabled)
            {
                timer1.Enabled = true; timer1.Start();
            }

        }

        private void importHEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();

            x.Filter = "HEX files (*.hex)|*.hex";

            DialogResult r = x.ShowDialog();

            if (r != DialogResult.OK) return;

            hexfilename = x.FileName;

            toolStripStatusLabel1.Text = x.SafeFileName;
            toolStripStatusLabel1.ToolTipText = hexfilename;

            Properties.Settings s = Properties.Settings.Default;

            if (s.HexFilename1 != hexfilename)
            {
                s.HexFilename4 = s.HexFilename3;
                s.HexFilename3 = s.HexFilename2;
                s.HexFilename2 = s.HexFilename1;
                s.HexFilename1 = hexfilename;

                toolStripMenuItem4.Text = s.HexFilename1;
                toolStripMenuItem5.Text = s.HexFilename2;
                toolStripMenuItem6.Text = s.HexFilename3;
                toolStripMenuItem7.Text = s.HexFilename4;

            }

            s.Save();

            btnWTFlash.Enabled = true;
            btnERASE.Enabled = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            setCOMPortBaudRate(toolStripMenuItem2.Text);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            hexfilename = null;

            serialPort1.PortName = Properties.Settings.Default.COMx;

            toolStripDropDownButton2.Text = Properties.Settings.Default.COMx;
            toolStripDropDownButton1.Text = Properties.Settings.Default.BaudRatex.ToString();

            toolStripMenuItem4.Text = Properties.Settings.Default.HexFilename1;
            toolStripMenuItem5.Text = Properties.Settings.Default.HexFilename2;
            toolStripMenuItem6.Text = Properties.Settings.Default.HexFilename3;
            toolStripMenuItem7.Text = Properties.Settings.Default.HexFilename4;

            toolStripMenuItem4.ToolTipText = Properties.Settings.Default.HexFilename1;
            toolStripMenuItem5.ToolTipText = Properties.Settings.Default.HexFilename2;
            toolStripMenuItem6.ToolTipText = Properties.Settings.Default.HexFilename3;
            toolStripMenuItem7.ToolTipText = Properties.Settings.Default.HexFilename4;

            cbEraseBeforeWrite.Checked = Properties.Settings.Default.EraseBeforeWrite;

        }

        private void cOM1ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            setCOMPortName(cOM1ToolStripMenuItem2.Text);
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void cOM2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCOMPortName( cOM2ToolStripMenuItem.Text);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            setCOMPortBaudRate(toolStripMenuItem3.Text);
        }

        private void btnWTFlash_Click(object sender, EventArgs e)
        {
            //checkComPort();

            toolStripProgressBar1.Value = 0;

            //call backgroundworker
            bkgWritePM.RunWorkerAsync(cbEraseBeforeWrite.Checked);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string fn = Properties.Settings.Default.HexFilename1;
            FileInfo hexFile = new FileInfo(fn);

            if (hexFile.Exists)
            {
                hexfilename = fn;
                toolStripStatusLabel1.Text = hexFile.Name;
                btnWTFlash.Enabled = true;
            }
            else
            {
                MessageBox.Show("File does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                hexfilename = null;

                toolStripStatusLabel1.Text = "Hex File";

                btnWTFlash.Enabled = false;
            }

            btnERASE.Enabled = btnWTFlash.Enabled;


        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string fn = Properties.Settings.Default.HexFilename2;
            FileInfo hexFile = new FileInfo(fn);

            if (hexFile.Exists)
            {
                hexfilename = fn;
                toolStripStatusLabel1.Text = hexFile.Name;
                btnWTFlash.Enabled = true;
            }
            else
            {
                MessageBox.Show("File does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                hexfilename = null;

                toolStripStatusLabel1.Text = "Hex File";

                btnWTFlash.Enabled = false;
            }

            btnERASE.Enabled = btnWTFlash.Enabled;

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string fn = Properties.Settings.Default.HexFilename3;
            FileInfo hexFile = new FileInfo(fn);

            if (hexFile.Exists)
            {
                hexfilename = fn;
                toolStripStatusLabel1.Text = hexFile.Name;
                btnWTFlash.Enabled = true;
            }
            else
            {
                MessageBox.Show("File does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                hexfilename = null;

                toolStripStatusLabel1.Text = "Hex File";

                btnWTFlash.Enabled = false;
            }

            btnERASE.Enabled = btnWTFlash.Enabled;

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string fn = Properties.Settings.Default.HexFilename4;
            FileInfo hexFile = new FileInfo(fn);

            if (hexFile.Exists)
            {
                hexfilename = fn;
                toolStripStatusLabel1.Text = hexFile.Name;
                btnWTFlash.Enabled = true;
            }
            else
            {
                MessageBox.Show("File does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                hexfilename = null;

                toolStripStatusLabel1.Text = "Hex File";

                btnWTFlash.Enabled = false;
            }

            btnERASE.Enabled = btnWTFlash.Enabled;

        }

        private void bkgWritePM_DoWork(object sender, DoWorkEventArgs e)
        {
            bool chkErase = bool.Parse(e.Argument.ToString());

            if (chkErase)
            {
                byte addrU = 0, addrH = 0, addrL = 0, blockLen = 0;

                PICkit2V2.Pk2BootLoader.calculatePageUsage(hexfilename, ref addrL, ref addrH, ref addrU, ref blockLen);

                eraseFlash(addrL, addrH, addrU, blockLen);

                PICkit2V2.Pk2BootLoader.chkMsgRecievedCnt = 5;

                while (PICkit2V2.Pk2BootLoader.chkMsgRecievedCnt > 0)
                {
                    Thread.Sleep(100);
                }
            }

            PICkit2V2.Pk2BootLoader.ReadHexAndDownload(bkgWritePM, hexfilename);

        }

        private void bkgWritePM_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            serialPort1.Write(PICkit2V2.Pk2BootLoader.wrBuff, 0, PICkit2V2.Pk2BootLoader.wrBuffCnt);

            toolStripProgressBar1.Value = e.ProgressPercentage;

        }

        private void bkgWritePM_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Complete!", "Write Program Memory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void cOM4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCOMPortName( cOM4ToolStripMenuItem.Text);
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {

        }

        private void cOM3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCOMPortName(cOM3ToolStripMenuItem.Text);
        }

        private void cOM5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCOMPortName( cOM5ToolStripMenuItem.Text);
            //toolStripDropDownButton2.Text = cOM5ToolStripMenuItem.Text;
            //Properties.Settings.Default.COMx = toolStripDropDownButton2.Text;
            //timer1.Enabled = false;
            //if (serialPort1.IsOpen) serialPort1.Close();
            //serialPort1.PortName = Properties.Settings.Default.COMx;
            //Properties.Settings.Default.Save(); ;
        }

        private void setCOMPortName( string name)
        {
            toolStripDropDownButton2.Text = name;
            Properties.Settings.Default.COMx = name;
            timer1.Enabled = false;
            if (serialPort1.IsOpen) serialPort1.Close();
            serialPort1.PortName = name;
            Properties.Settings.Default.Save(); ;
        }

        private void setCOMPortBaudRate( string name)
        {
            toolStripDropDownButton1.Text = name;
            serialPort1.BaudRate = Convert.ToInt32(name);
        }

        private void cOM6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCOMPortName(cOM6ToolStripMenuItem.Text);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            setCOMPortBaudRate(toolStripMenuItem1.Text);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            setCOMPortBaudRate(toolStripMenuItem9.Text);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            setCOMPortBaudRate(toolStripMenuItem10.Text);
        }
    }
}
