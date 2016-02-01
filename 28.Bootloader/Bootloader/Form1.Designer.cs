namespace BT_HC04
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnBLVer = new System.Windows.Forms.Button();
            this.btnRDFlash = new System.Windows.Forms.Button();
            this.btnERASE = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importHEXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportHexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStFilename = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.cOM1ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOM5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnWTFlash = new System.Windows.Forms.Button();
            this.btnVerify = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbEraseBeforeWrite = new System.Windows.Forms.CheckBox();
            this.lbConfiguraion = new System.Windows.Forms.Label();
            this.lbChecksum = new System.Windows.Forms.Label();
            this.lbUserIDs = new System.Windows.Forms.Label();
            this.lbDevice = new System.Windows.Forms.Label();
            this.bkgWritePM = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 38400;
            this.serialPort1.PortName = "COM4";
            this.serialPort1.ReadTimeout = 1000;
            this.serialPort1.WriteTimeout = 1000;
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 25;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 215);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(514, 141);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // btnBLVer
            // 
            this.btnBLVer.Location = new System.Drawing.Point(351, 166);
            this.btnBLVer.Name = "btnBLVer";
            this.btnBLVer.Size = new System.Drawing.Size(70, 28);
            this.btnBLVer.TabIndex = 8;
            this.btnBLVer.Text = "BL Version";
            this.btnBLVer.UseVisualStyleBackColor = true;
            this.btnBLVer.Click += new System.EventHandler(this.btnBLVer_Click);
            // 
            // btnRDFlash
            // 
            this.btnRDFlash.Enabled = false;
            this.btnRDFlash.Location = new System.Drawing.Point(27, 166);
            this.btnRDFlash.Name = "btnRDFlash";
            this.btnRDFlash.Size = new System.Drawing.Size(70, 28);
            this.btnRDFlash.TabIndex = 9;
            this.btnRDFlash.Text = "Read";
            this.btnRDFlash.UseVisualStyleBackColor = true;
            this.btnRDFlash.Click += new System.EventHandler(this.btnRDFlash_Click);
            // 
            // btnERASE
            // 
            this.btnERASE.Enabled = false;
            this.btnERASE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnERASE.ForeColor = System.Drawing.Color.Red;
            this.btnERASE.Location = new System.Drawing.Point(270, 166);
            this.btnERASE.Name = "btnERASE";
            this.btnERASE.Size = new System.Drawing.Size(70, 28);
            this.btnERASE.TabIndex = 10;
            this.btnERASE.Text = "Erase";
            this.btnERASE.UseVisualStyleBackColor = true;
            this.btnERASE.Click += new System.EventHandler(this.btnERASE_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(432, 166);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(70, 28);
            this.btnReset.TabIndex = 11;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(538, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importHEXToolStripMenuItem,
            this.exportHexToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // importHEXToolStripMenuItem
            // 
            this.importHEXToolStripMenuItem.Name = "importHEXToolStripMenuItem";
            this.importHEXToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.importHEXToolStripMenuItem.Text = "&Import Hex";
            this.importHEXToolStripMenuItem.Click += new System.EventHandler(this.importHEXToolStripMenuItem_Click);
            // 
            // exportHexToolStripMenuItem
            // 
            this.exportHexToolStripMenuItem.Name = "exportHexToolStripMenuItem";
            this.exportHexToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exportHexToolStripMenuItem.Text = "&Export Hex";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(130, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem4.Text = "1.";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem5.Text = "2.";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem6.Text = "3.";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(133, 22);
            this.toolStripMenuItem7.Text = "4.";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStFilename,
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripDropDownButton1,
            this.toolStripDropDownButton2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 359);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(538, 22);
            this.statusStrip1.TabIndex = 13;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStFilename
            // 
            this.toolStripStFilename.Name = "toolStripStFilename";
            this.toolStripStFilename.Size = new System.Drawing.Size(0, 17);
            this.toolStripStFilename.ToolTipText = "Hex file";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(217, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Hex File";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 16);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem2});
            this.toolStripDropDownButton1.Enabled = false;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(50, 20);
            this.toolStripDropDownButton1.Text = "19200";
            this.toolStripDropDownButton1.ToolTipText = "Baud Rate";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "9600";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "19200";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cOM1ToolStripMenuItem2,
            this.cOM2ToolStripMenuItem,
            this.cOM3ToolStripMenuItem,
            this.cOM4ToolStripMenuItem,
            this.cOM5ToolStripMenuItem});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(54, 20);
            this.toolStripDropDownButton2.Text = "COM4";
            this.toolStripDropDownButton2.ToolTipText = "Serial Port";
            this.toolStripDropDownButton2.Click += new System.EventHandler(this.toolStripDropDownButton2_Click);
            // 
            // cOM1ToolStripMenuItem2
            // 
            this.cOM1ToolStripMenuItem2.Name = "cOM1ToolStripMenuItem2";
            this.cOM1ToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.cOM1ToolStripMenuItem2.Text = "COM1";
            this.cOM1ToolStripMenuItem2.Click += new System.EventHandler(this.cOM1ToolStripMenuItem2_Click);
            // 
            // cOM2ToolStripMenuItem
            // 
            this.cOM2ToolStripMenuItem.Name = "cOM2ToolStripMenuItem";
            this.cOM2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cOM2ToolStripMenuItem.Text = "COM2";
            this.cOM2ToolStripMenuItem.Click += new System.EventHandler(this.cOM2ToolStripMenuItem_Click);
            // 
            // cOM3ToolStripMenuItem
            // 
            this.cOM3ToolStripMenuItem.Name = "cOM3ToolStripMenuItem";
            this.cOM3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cOM3ToolStripMenuItem.Text = "COM3";
            this.cOM3ToolStripMenuItem.Click += new System.EventHandler(this.cOM3ToolStripMenuItem_Click);
            // 
            // cOM4ToolStripMenuItem
            // 
            this.cOM4ToolStripMenuItem.Name = "cOM4ToolStripMenuItem";
            this.cOM4ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cOM4ToolStripMenuItem.Text = "COM4";
            this.cOM4ToolStripMenuItem.Click += new System.EventHandler(this.cOM4ToolStripMenuItem_Click);
            // 
            // cOM5ToolStripMenuItem
            // 
            this.cOM5ToolStripMenuItem.Name = "cOM5ToolStripMenuItem";
            this.cOM5ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cOM5ToolStripMenuItem.Text = "COM5";
            this.cOM5ToolStripMenuItem.Click += new System.EventHandler(this.cOM5ToolStripMenuItem_Click);
            // 
            // btnWTFlash
            // 
            this.btnWTFlash.Enabled = false;
            this.btnWTFlash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnWTFlash.Location = new System.Drawing.Point(108, 166);
            this.btnWTFlash.Name = "btnWTFlash";
            this.btnWTFlash.Size = new System.Drawing.Size(70, 28);
            this.btnWTFlash.TabIndex = 14;
            this.btnWTFlash.Text = "Write";
            this.btnWTFlash.UseVisualStyleBackColor = true;
            this.btnWTFlash.Click += new System.EventHandler(this.btnWTFlash_Click);
            // 
            // btnVerify
            // 
            this.btnVerify.Enabled = false;
            this.btnVerify.Location = new System.Drawing.Point(189, 166);
            this.btnVerify.Name = "btnVerify";
            this.btnVerify.Size = new System.Drawing.Size(70, 28);
            this.btnVerify.TabIndex = 15;
            this.btnVerify.Text = "Verify";
            this.btnVerify.UseVisualStyleBackColor = true;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cbEraseBeforeWrite);
            this.groupBox1.Controls.Add(this.lbConfiguraion);
            this.groupBox1.Controls.Add(this.lbChecksum);
            this.groupBox1.Controls.Add(this.lbUserIDs);
            this.groupBox1.Controls.Add(this.lbDevice);
            this.groupBox1.Location = new System.Drawing.Point(28, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(475, 113);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Device Configuration";
            // 
            // cbEraseBeforeWrite
            // 
            this.cbEraseBeforeWrite.AutoSize = true;
            this.cbEraseBeforeWrite.Location = new System.Drawing.Point(232, 74);
            this.cbEraseBeforeWrite.Name = "cbEraseBeforeWrite";
            this.cbEraseBeforeWrite.Size = new System.Drawing.Size(111, 17);
            this.cbEraseBeforeWrite.TabIndex = 4;
            this.cbEraseBeforeWrite.Text = "Erase before write";
            this.cbEraseBeforeWrite.UseVisualStyleBackColor = true;
            // 
            // lbConfiguraion
            // 
            this.lbConfiguraion.Location = new System.Drawing.Point(229, 32);
            this.lbConfiguraion.Name = "lbConfiguraion";
            this.lbConfiguraion.Size = new System.Drawing.Size(191, 16);
            this.lbConfiguraion.TabIndex = 3;
            this.lbConfiguraion.Text = "Configuration:";
            // 
            // lbChecksum
            // 
            this.lbChecksum.Location = new System.Drawing.Point(25, 84);
            this.lbChecksum.Name = "lbChecksum";
            this.lbChecksum.Size = new System.Drawing.Size(191, 16);
            this.lbChecksum.TabIndex = 2;
            this.lbChecksum.Text = "Checksum:";
            // 
            // lbUserIDs
            // 
            this.lbUserIDs.Location = new System.Drawing.Point(25, 58);
            this.lbUserIDs.Name = "lbUserIDs";
            this.lbUserIDs.Size = new System.Drawing.Size(191, 16);
            this.lbUserIDs.TabIndex = 1;
            this.lbUserIDs.Text = "User IDs:";
            // 
            // lbDevice
            // 
            this.lbDevice.Location = new System.Drawing.Point(25, 32);
            this.lbDevice.Name = "lbDevice";
            this.lbDevice.Size = new System.Drawing.Size(191, 16);
            this.lbDevice.TabIndex = 0;
            this.lbDevice.Text = "Device:";
            // 
            // bkgWritePM
            // 
            this.bkgWritePM.WorkerReportsProgress = true;
            this.bkgWritePM.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgWritePM_DoWork);
            this.bkgWritePM.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkgWritePM_ProgressChanged);
            this.bkgWritePM.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgWritePM_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 381);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnWTFlash);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnERASE);
            this.Controls.Add(this.btnRDFlash);
            this.Controls.Add(this.btnBLVer);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "PIC24FJ Bootloader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnBLVer;
        private System.Windows.Forms.Button btnRDFlash;
        private System.Windows.Forms.Button btnERASE;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importHEXToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStFilename;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem cOM1ToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cOM2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOM3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOM4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOM5ToolStripMenuItem;
        private System.Windows.Forms.Button btnWTFlash;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.ToolStripMenuItem exportHexToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbConfiguraion;
        private System.Windows.Forms.Label lbChecksum;
        private System.Windows.Forms.Label lbUserIDs;
        private System.Windows.Forms.Label lbDevice;
        private System.Windows.Forms.CheckBox cbEraseBeforeWrite;
        private System.ComponentModel.BackgroundWorker bkgWritePM;
    }
}

