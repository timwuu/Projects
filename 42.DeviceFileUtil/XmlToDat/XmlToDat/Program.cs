using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KONST = XmlToDat.Constants;
using System.IO;
using File = System.IO.File;
using System.Data;
using DTBL = System.Data.DataTable;

namespace XmlToDat
{
    class Program
    {

        public class PIC32MM
        {
            public string PartName;
            public uint DeviceID;
            public uint ProgramMem;

            public PIC32MM(string pname, uint devid, uint progmem )
            {
                PartName = pname;
                DeviceID = devid;
                ProgramMem = progmem;
            }
        }

        public static PIC32MM[] PIC32MM_List;


        public static String FirmwareVersion = "NA";
        public static String DeviceFileVersion = "NA";
        public static DeviceFile DevFile = new DeviceFile();
        private static int[] familySearchTable; // index is search priority, value is family array index.


        static void Main(string[] args)
        {
            InitPIC32MMList();

            //ReadDeviceFile("PK2DeviceFile.dat");
            ReadDeviceFileXML("PK2DeviceFile.dat");
            WriteDeviceFileDAT("PK2DeviceFileNew.dat");
        }

        public static void InitPIC32MMList()
        {
            PIC32MM_List = new PIC32MM[9];

            PIC32MM_List[0] = new PIC32MM("PIC32MM0016GPL020", 0x6B04053, 0x15C0);
            PIC32MM_List[1] = new PIC32MM("PIC32MM0032GPL020", 0x6B0C053, 0x25C0);
            PIC32MM_List[2] = new PIC32MM("PIC32MM0064GPL020", 0x6B14053, 0x45C0);
            PIC32MM_List[3] = new PIC32MM("PIC32MM0016GPL028", 0x6B02053, 0x15C0);
            PIC32MM_List[4] = new PIC32MM("PIC32MM0032GPL028", 0x6B0A053, 0x25C0);
            PIC32MM_List[5] = new PIC32MM("PIC32MM0064GPL028", 0x6B12053, 0x45C0);
            PIC32MM_List[6] = new PIC32MM("PIC32MM0016GPL036", 0x6B06053, 0x15C0);
            PIC32MM_List[7] = new PIC32MM("PIC32MM0032GPL036", 0x6B0B053, 0x25C0);
            PIC32MM_List[8] = new PIC32MM("PIC32MM0064GPL036", 0x6B16053, 0x45C0);

        }


        public static bool ReadDeviceFile(string DeviceFileName)
        {
            string DeviceFileNameXML = DeviceFileName.Replace(".dat", ".xml");
            System.Data.DataSet dsDeviceFile;
            DTBL tblFamilies = new DTBL("Families");
            DTBL tblPartsList = new DTBL("PartsList");
            DTBL tblScripts = new DTBL("Scripts");

            if (File.Exists(DeviceFileNameXML))
            {
                try
                {
                    dsDeviceFile = new System.Data.DataSet("DeviceFile");
                    dsDeviceFile.ReadXml(DeviceFileNameXML, System.Data.XmlReadMode.ReadSchema);
                    tblFamilies = dsDeviceFile.Tables["Families"];
                    tblPartsList = dsDeviceFile.Tables["PartsList"];
                    tblScripts = dsDeviceFile.Tables["Scripts"];
                }
                catch
                {
                    return false;
                }
            }


            bool fileExists = File.Exists(DeviceFileName);
            //DTBL tblInfo = new DTBL("Info");
            //DTBL tblFamilies = new DTBL("Families");
            //DTBL tblPartsList = new DTBL("PartsList");
            //DTBL tblScripts = new DTBL("Scripts");

            //System.Data.DataSet dsDeviceFile = new System.Data.DataSet("DeviceFile");

            //dsDeviceFile.Tables.Add(tblInfo);
            //dsDeviceFile.Tables.Add(tblFamilies);
            //dsDeviceFile.Tables.Add(tblPartsList);
            //dsDeviceFile.Tables.Add(tblScripts);

            //setupDTBLs(tblInfo, tblFamilies, tblPartsList, tblScripts);

            if (fileExists)
            {
                try
                {
                    //FileStream fsDevFile = File.Open(DeviceFileName, FileMode.Open);
                    FileStream fsDevFile = File.OpenRead(DeviceFileName);
                    using (BinaryReader binRead = new BinaryReader(fsDevFile))
                    {
                        //
                        DevFile.Info.VersionMajor = binRead.ReadInt32();
                        DevFile.Info.VersionMinor = binRead.ReadInt32();
                        DevFile.Info.VersionDot = binRead.ReadInt32();
                        DevFile.Info.VersionNotes = binRead.ReadString();
                        DevFile.Info.NumberFamilies = binRead.ReadInt32();
                        DevFile.Info.NumberParts = binRead.ReadInt32() + tblPartsList.Rows.Count;
                        DevFile.Info.NumberScripts = binRead.ReadInt32() + tblScripts.Rows.Count;
                        DevFile.Info.Compatibility = binRead.ReadByte();
                        DevFile.Info.UNUSED1A = binRead.ReadByte();
                        DevFile.Info.UNUSED1B = binRead.ReadUInt16();
                        DevFile.Info.UNUSED2 = binRead.ReadUInt32();

                        //addTblInfo(tblInfo, DevFile.Info);

                        // create a version string
                        DeviceFileVersion = string.Format("{0:D1}.{1:D2}.{2:D2}", DevFile.Info.VersionMajor,
                                         DevFile.Info.VersionMinor, DevFile.Info.VersionDot);
                        //
                        // Declare arrays
                        //
                        DevFile.Families = new DeviceFile.DeviceFamilyParams[DevFile.Info.NumberFamilies];
                        DevFile.PartsList = new DeviceFile.DevicePartParams[DevFile.Info.NumberParts];
                        DevFile.Scripts = new DeviceFile.DeviceScripts[DevFile.Info.NumberScripts];

                        //
                        // now read all families if they are there
                        //
                        for (int l_x = 0; l_x < DevFile.Info.NumberFamilies; l_x++)
                        {
                            DevFile.Families[l_x].FamilyID = binRead.ReadUInt16();
                            DevFile.Families[l_x].FamilyType = binRead.ReadUInt16();
                            DevFile.Families[l_x].SearchPriority = binRead.ReadUInt16();
                            DevFile.Families[l_x].FamilyName = binRead.ReadString();
                            DevFile.Families[l_x].ProgEntryScript = binRead.ReadUInt16();
                            DevFile.Families[l_x].ProgExitScript = binRead.ReadUInt16();
                            DevFile.Families[l_x].ReadDevIDScript = binRead.ReadUInt16();
                            DevFile.Families[l_x].DeviceIDMask = binRead.ReadUInt32();
                            DevFile.Families[l_x].BlankValue = binRead.ReadUInt32();
                            DevFile.Families[l_x].BytesPerLocation = binRead.ReadByte();
                            DevFile.Families[l_x].AddressIncrement = binRead.ReadByte();
                            DevFile.Families[l_x].PartDetect = binRead.ReadBoolean();
                            DevFile.Families[l_x].ProgEntryVPPScript = binRead.ReadUInt16();
                            DevFile.Families[l_x].UNUSED1 = binRead.ReadUInt16();
                            DevFile.Families[l_x].EEMemBytesPerWord = binRead.ReadByte();
                            DevFile.Families[l_x].EEMemAddressIncrement = binRead.ReadByte();
                            DevFile.Families[l_x].UserIDHexBytes = binRead.ReadByte();
                            DevFile.Families[l_x].UserIDBytes = binRead.ReadByte();
                            DevFile.Families[l_x].ProgMemHexBytes = binRead.ReadByte();
                            DevFile.Families[l_x].EEMemHexBytes = binRead.ReadByte();
                            DevFile.Families[l_x].ProgMemShift = binRead.ReadByte();
                            DevFile.Families[l_x].TestMemoryStart = binRead.ReadUInt32();
                            DevFile.Families[l_x].TestMemoryLength = binRead.ReadUInt16();
                            DevFile.Families[l_x].Vpp = binRead.ReadSingle();

                            //timijk 2017.02.21
                            //if (DevFile.Families[l_x].FamilyName == "PIC32" && tblPartsList.Rows.Count > 0)
                            //{ DevFile.Families[l_x].DeviceIDMask = 0xFFFF000; }
                        }

                        //timijk 2015.06.08
                        //replace the DevFile.Families if(tblFamilies.Rows.Count>0)
                        if (tblFamilies.Rows.Count > 0)
                        {
                            int l_x = 0;

                            DevFile.Info.NumberFamilies = tblFamilies.Rows.Count;

                            DevFile.Families = new DeviceFile.DeviceFamilyParams[DevFile.Info.NumberFamilies];

                            foreach (DataRow row in tblFamilies.Rows)
                            {
                                DevFile.Families[l_x].FamilyID = (UInt16)row["FamilyID"];
                                DevFile.Families[l_x].FamilyType = (UInt16)row["FamilyType"];
                                DevFile.Families[l_x].SearchPriority = (UInt16)row["SearchPriority"];
                                DevFile.Families[l_x].FamilyName = (String)row["FamilyName"];
                                DevFile.Families[l_x].ProgEntryScript = (UInt16)row["ProgEntryScript"];
                                DevFile.Families[l_x].ProgExitScript = (UInt16)row["ProgExitScript"];
                                DevFile.Families[l_x].ReadDevIDScript = (UInt16)row["ReadDevIDScript"];
                                DevFile.Families[l_x].DeviceIDMask = (UInt32)row["DeviceIDMask"];
                                DevFile.Families[l_x].BlankValue = (UInt32)row["BlankValue"];
                                DevFile.Families[l_x].BytesPerLocation = (Byte)row["BytesPerLocation"];
                                DevFile.Families[l_x].AddressIncrement = (Byte)row["AddressIncrement"];
                                DevFile.Families[l_x].PartDetect = (Boolean)row["PartDetect"];
                                DevFile.Families[l_x].ProgEntryVPPScript = (UInt16)row["ProgEntryVPPScript"];
                                DevFile.Families[l_x].UNUSED1 = (UInt16)row["UNUSED1"];
                                DevFile.Families[l_x].EEMemBytesPerWord = (Byte)row["EEMemBytesPerWord"];
                                DevFile.Families[l_x].EEMemAddressIncrement = (Byte)row["EEMemAddressIncrement"];
                                DevFile.Families[l_x].UserIDHexBytes = (Byte)row["UserIDHexBytes"];
                                DevFile.Families[l_x].UserIDBytes = (Byte)row["UserIDBytes"];
                                DevFile.Families[l_x].ProgMemHexBytes = (Byte)row["ProgMemHexBytes"];
                                DevFile.Families[l_x].EEMemHexBytes = (Byte)row["EEMemHexBytes"];
                                DevFile.Families[l_x].ProgMemShift = (Byte)row["ProgMemShift"];
                                DevFile.Families[l_x].TestMemoryStart = (UInt32)row["TestMemoryStart"];
                                DevFile.Families[l_x].TestMemoryLength = (UInt16)row["TestMemoryLength"];
                                DevFile.Families[l_x].Vpp = (Single)row["Vpp"];

                                //timijk 2017.02.21
                                //if (DevFile.Families[l_x].FamilyName == "PIC32" && tblPartsList.Rows.Count > 0)
                                //{ DevFile.Families[l_x].DeviceIDMask = 0xFFFF000; }

                                l_x++;
                            }

                        }
                        //addTblFamilies(tblFamilies, DevFile.Families);

                        // Create family search table based on priority
                        familySearchTable = new int[DevFile.Info.NumberFamilies];
                        for (int familyIdx = 0; familyIdx < DevFile.Info.NumberFamilies; familyIdx++)
                        {
                            familySearchTable[DevFile.Families[familyIdx].SearchPriority] = familyIdx;
                        }
                        //
                        // now read all parts if they are there
                        //
                        int l_y = DevFile.Info.NumberParts - tblPartsList.Rows.Count;

                        for (int l_x = 0; l_x < l_y; l_x++)
                        {
                            DevFile.PartsList[l_x].PartName = binRead.ReadString();
                            DevFile.PartsList[l_x].Family = binRead.ReadUInt16();

                            if (tblPartsList.Rows.Count > 0 &&
                                DevFile.PartsList[l_x].PartName.Length >= 5 &&
                                DevFile.PartsList[l_x].PartName.Substring(0, 5) == "PIC32")
                            {
                                // timijk 2015.04.07 merge with PIC32MX support
                                DevFile.PartsList[l_x].PartName = "*" + DevFile.PartsList[l_x].PartName;
                                DevFile.PartsList[l_x].Family = 0xFFFF;   //disable original PIC32 parts
                            }
                            DevFile.PartsList[l_x].DeviceID = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].ProgramMem = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].EEMem = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EEAddr = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].ConfigWords = binRead.ReadByte();
                            DevFile.PartsList[l_x].ConfigAddr = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].UserIDWords = binRead.ReadByte();
                            DevFile.PartsList[l_x].UserIDAddr = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].BandGapMask = binRead.ReadUInt32();
                            // Init config arrays
                            DevFile.PartsList[l_x].ConfigMasks = new ushort[KONST.NumConfigMasks];
                            DevFile.PartsList[l_x].ConfigBlank = new ushort[KONST.NumConfigMasks];
                            for (int l_index = 0; l_index < KONST.MaxReadCfgMasks; l_index++)
                            {
                                DevFile.PartsList[l_x].ConfigMasks[l_index] = binRead.ReadUInt16();
                            }
                            for (int l_index = 0; l_index < KONST.MaxReadCfgMasks; l_index++)
                            {
                                DevFile.PartsList[l_x].ConfigBlank[l_index] = binRead.ReadUInt16();
                            }
                            DevFile.PartsList[l_x].CPMask = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].CPConfig = binRead.ReadByte();
                            DevFile.PartsList[l_x].OSSCALSave = binRead.ReadBoolean();
                            DevFile.PartsList[l_x].IgnoreAddress = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].VddMin = binRead.ReadSingle();
                            DevFile.PartsList[l_x].VddMax = binRead.ReadSingle();
                            DevFile.PartsList[l_x].VddErase = binRead.ReadSingle();
                            DevFile.PartsList[l_x].CalibrationWords = binRead.ReadByte();
                            DevFile.PartsList[l_x].ChipEraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemAddrSetScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemAddrBytes = binRead.ReadByte();
                            DevFile.PartsList[l_x].ProgMemRdScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemRdWords = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EERdPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EERdScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EERdLocations = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].UserIDRdPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].UserIDRdScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigRdPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigRdScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemWrPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemWrScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemWrWords = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ProgMemPanelBufs = binRead.ReadByte();
                            DevFile.PartsList[l_x].ProgMemPanelOffset = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].EEWrPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EEWrScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EEWrLocations = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].UserIDWrPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].UserIDWrScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigWrPrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigWrScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].OSCCALRdScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].OSCCALWrScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DPMask = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].WriteCfgOnErase = binRead.ReadBoolean();
                            DevFile.PartsList[l_x].BlankCheckSkipUsrIDs = binRead.ReadBoolean();
                            DevFile.PartsList[l_x].IgnoreBytes = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ChipErasePrepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].BootFlash = binRead.ReadUInt32();
                            //DevFile.PartsList[l_x].UNUSED4 = binRead.ReadUInt32();
                            DevFile.PartsList[l_x].Config9Mask = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigMasks[8] = DevFile.PartsList[l_x].Config9Mask;
                            DevFile.PartsList[l_x].Config9Blank = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigBlank[8] = DevFile.PartsList[l_x].Config9Blank;
                            DevFile.PartsList[l_x].ProgMemEraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EEMemEraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ConfigMemEraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].reserved1EraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].reserved2EraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].TestMemoryRdScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].TestMemoryRdWords = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EERowEraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].EERowEraseWords = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].ExportToMPLAB = binRead.ReadBoolean();
                            DevFile.PartsList[l_x].DebugHaltScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugRunScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugStatusScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugReadExecVerScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugSingleStepScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugBulkWrDataScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugBulkRdDataScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugWriteVectorScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugReadVectorScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugRowEraseScript = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugRowEraseSize = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugReserved5Script = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugReserved6Script = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugReserved7Script = binRead.ReadUInt16();
                            DevFile.PartsList[l_x].DebugReserved8Script = binRead.ReadUInt16();
                            //DevFile.PartsList[l_x].DebugReserved9Script = binRead.ReadUInt16();                                                       
                            DevFile.PartsList[l_x].LVPScript = binRead.ReadUInt16();

                        }

                        foreach (DataRow row in tblPartsList.Rows)
                        {
                            DevFile.PartsList[l_y].PartName = (String)row["PartName"];
                            DevFile.PartsList[l_y].Family = (UInt16)row["Family"];
                            DevFile.PartsList[l_y].DeviceID = (UInt32)row["DeviceID"] & DevFile.Families[DevFile.PartsList[l_y].Family].DeviceIDMask;
                            DevFile.PartsList[l_y].ProgramMem = (UInt32)row["ProgramMem"];
                            DevFile.PartsList[l_y].EEMem = (UInt16)row["EEMem"];
                            DevFile.PartsList[l_y].EEAddr = (UInt32)row["EEAddr"];
                            DevFile.PartsList[l_y].ConfigWords = (Byte)row["ConfigWords"];
                            DevFile.PartsList[l_y].ConfigAddr = (UInt32)row["ConfigAddr"];
                            DevFile.PartsList[l_y].UserIDWords = (Byte)row["UserIDWords"];
                            DevFile.PartsList[l_y].UserIDAddr = (UInt32)row["UserIDAddr"];
                            DevFile.PartsList[l_y].BandGapMask = (UInt32)row["BandGapMask"];
                            // Init config arrays
                            DevFile.PartsList[l_y].ConfigMasks = (ushort[])row["ConfigMasks"];
                            DevFile.PartsList[l_y].ConfigBlank = (ushort[])row["ConfigBlank"];
                            DevFile.PartsList[l_y].CPMask = (UInt16)row["CPMask"];
                            DevFile.PartsList[l_y].CPConfig = (Byte)row["CPConfig"];
                            DevFile.PartsList[l_y].OSSCALSave = (Boolean)row["OSSCALSave"];
                            DevFile.PartsList[l_y].IgnoreAddress = (UInt32)row["IgnoreAddress"];
                            DevFile.PartsList[l_y].VddMin = (Single)row["VddMin"];
                            DevFile.PartsList[l_y].VddMax = (Single)row["VddMax"];
                            DevFile.PartsList[l_y].VddErase = (Single)row["VddErase"];
                            DevFile.PartsList[l_y].CalibrationWords = (Byte)row["CalibrationWords"];
                            DevFile.PartsList[l_y].ChipEraseScript = (UInt16)row["ChipEraseScript"];
                            DevFile.PartsList[l_y].ProgMemAddrSetScript = (UInt16)row["ProgMemAddrSetScript"];
                            DevFile.PartsList[l_y].ProgMemAddrBytes = (Byte)row["ProgMemAddrBytes"];
                            DevFile.PartsList[l_y].ProgMemRdScript = (UInt16)row["ProgMemRdScript"];
                            DevFile.PartsList[l_y].ProgMemRdWords = (UInt16)row["ProgMemRdWords"];
                            DevFile.PartsList[l_y].EERdPrepScript = (UInt16)row["EERdPrepScript"];
                            DevFile.PartsList[l_y].EERdScript = (UInt16)row["EERdScript"];
                            DevFile.PartsList[l_y].EERdLocations = (UInt16)row["EERdLocations"];
                            DevFile.PartsList[l_y].UserIDRdPrepScript = (UInt16)row["UserIDRdPrepScript"];
                            DevFile.PartsList[l_y].UserIDRdScript = (UInt16)row["UserIDRdScript"];
                            DevFile.PartsList[l_y].ConfigRdPrepScript = (UInt16)row["ConfigRdPrepScript"];
                            DevFile.PartsList[l_y].ConfigRdScript = (UInt16)row["ConfigRdScript"];
                            DevFile.PartsList[l_y].ProgMemWrPrepScript = (UInt16)row["ProgMemWrPrepScript"];
                            DevFile.PartsList[l_y].ProgMemWrScript = (UInt16)row["ProgMemWrScript"];
                            DevFile.PartsList[l_y].ProgMemWrWords = (UInt16)row["ProgMemWrWords"];
                            DevFile.PartsList[l_y].ProgMemPanelBufs = (Byte)row["ProgMemPanelBufs"];
                            DevFile.PartsList[l_y].ProgMemPanelOffset = (UInt32)row["ProgMemPanelOffset"];
                            DevFile.PartsList[l_y].EEWrPrepScript = (UInt16)row["EEWrPrepScript"];
                            DevFile.PartsList[l_y].EEWrScript = (UInt16)row["EEWrScript"];
                            DevFile.PartsList[l_y].EEWrLocations = (UInt16)row["EEWrLocations"];
                            DevFile.PartsList[l_y].UserIDWrPrepScript = (UInt16)row["UserIDWrPrepScript"];
                            DevFile.PartsList[l_y].UserIDWrScript = (UInt16)row["UserIDWrScript"];
                            DevFile.PartsList[l_y].ConfigWrPrepScript = (UInt16)row["ConfigWrPrepScript"];
                            DevFile.PartsList[l_y].ConfigWrScript = (UInt16)row["ConfigWrScript"];
                            DevFile.PartsList[l_y].OSCCALRdScript = (UInt16)row["OSCCALRdScript"];
                            DevFile.PartsList[l_y].OSCCALWrScript = (UInt16)row["OSCCALWrScript"];
                            DevFile.PartsList[l_y].DPMask = (UInt16)row["DPMask"];
                            DevFile.PartsList[l_y].WriteCfgOnErase = (Boolean)row["WriteCfgOnErase"];
                            DevFile.PartsList[l_y].BlankCheckSkipUsrIDs = (Boolean)row["BlankCheckSkipUsrIDs"];
                            DevFile.PartsList[l_y].IgnoreBytes = (UInt16)row["IgnoreBytes"];
                            DevFile.PartsList[l_y].ChipErasePrepScript = (UInt16)row["ChipErasePrepScript"];
                            DevFile.PartsList[l_y].BootFlash = (UInt32)row["BootFlash"];
                            //DevFile.PartsList[l_y].UNUSED4 = (UInt32)row[""];
                            DevFile.PartsList[l_y].Config9Mask = (UInt16)row["Config9Mask"];
                            //DevFile.PartsList[l_y].ConfigMasks[8] = DevFile.PartsList[l_y].Config9Mask;
                            DevFile.PartsList[l_y].Config9Blank = (UInt16)row["Config9Blank"];
                            //DevFile.PartsList[l_y].ConfigBlank[8] = DevFile.PartsList[l_y].Config9Blank;
                            DevFile.PartsList[l_y].ProgMemEraseScript = (UInt16)row["ProgMemEraseScript"];
                            DevFile.PartsList[l_y].EEMemEraseScript = (UInt16)row["EEMemEraseScript"];
                            DevFile.PartsList[l_y].ConfigMemEraseScript = (UInt16)row["ConfigMemEraseScript"];
                            DevFile.PartsList[l_y].reserved1EraseScript = (UInt16)row["reserved1EraseScript"];
                            DevFile.PartsList[l_y].reserved2EraseScript = (UInt16)row["reserved2EraseScript"];
                            DevFile.PartsList[l_y].TestMemoryRdScript = (UInt16)row["TestMemoryRdScript"];
                            DevFile.PartsList[l_y].TestMemoryRdWords = (UInt16)row["TestMemoryRdWords"];
                            DevFile.PartsList[l_y].EERowEraseScript = (UInt16)row["EERowEraseScript"];
                            DevFile.PartsList[l_y].EERowEraseWords = (UInt16)row["EERowEraseWords"];
                            DevFile.PartsList[l_y].ExportToMPLAB = (Boolean)row["ExportToMPLAB"];
                            DevFile.PartsList[l_y].DebugHaltScript = (UInt16)row["DebugHaltScript"];
                            DevFile.PartsList[l_y].DebugRunScript = (UInt16)row["DebugRunScript"];
                            DevFile.PartsList[l_y].DebugStatusScript = (UInt16)row["DebugStatusScript"];
                            DevFile.PartsList[l_y].DebugReadExecVerScript = (UInt16)row["DebugReadExecVerScript"];
                            DevFile.PartsList[l_y].DebugSingleStepScript = (UInt16)row["DebugSingleStepScript"];
                            DevFile.PartsList[l_y].DebugBulkWrDataScript = (UInt16)row["DebugBulkWrDataScript"];
                            DevFile.PartsList[l_y].DebugBulkRdDataScript = (UInt16)row["DebugBulkRdDataScript"];
                            DevFile.PartsList[l_y].DebugWriteVectorScript = (UInt16)row["DebugWriteVectorScript"];
                            DevFile.PartsList[l_y].DebugReadVectorScript = (UInt16)row["DebugReadVectorScript"];
                            DevFile.PartsList[l_y].DebugRowEraseScript = (UInt16)row["DebugRowEraseScript"];
                            DevFile.PartsList[l_y].DebugRowEraseSize = (UInt16)row["DebugRowEraseSize"];
                            DevFile.PartsList[l_y].DebugReserved5Script = (UInt16)row["DebugReserved5Script"];
                            DevFile.PartsList[l_y].DebugReserved6Script = (UInt16)row["DebugReserved6Script"];
                            DevFile.PartsList[l_y].DebugReserved7Script = (UInt16)row["DebugReserved7Script"];
                            DevFile.PartsList[l_y].DebugReserved8Script = (UInt16)row["DebugReserved8Script"];
                            DevFile.PartsList[l_y].LVPScript = (UInt16)row["LVPScript"];

                            l_y++;
                        }

                        //addTblPartsList(tblPartsList, DevFile.PartsList);

                        //
                        // now read all scripts if they are there
                        //                    
                        l_y = DevFile.Info.NumberScripts - tblScripts.Rows.Count;
                        for (int l_x = 0; l_x < l_y; l_x++)
                        {
                            DevFile.Scripts[l_x].ScriptNumber = binRead.ReadUInt16();
                            DevFile.Scripts[l_x].ScriptName = binRead.ReadString();
                            DevFile.Scripts[l_x].ScriptVersion = binRead.ReadUInt16();
                            DevFile.Scripts[l_x].UNUSED1 = binRead.ReadUInt32();
                            DevFile.Scripts[l_x].ScriptLength = binRead.ReadUInt16();
                            // init script array
                            DevFile.Scripts[l_x].Script = new ushort[DevFile.Scripts[l_x].ScriptLength];
                            for (int l_index = 0; l_index < DevFile.Scripts[l_x].ScriptLength; l_index++)
                            {
                                DevFile.Scripts[l_x].Script[l_index] = binRead.ReadUInt16();
                            }
                            DevFile.Scripts[l_x].Comment = binRead.ReadString();

                        }

                        foreach (DataRow row in tblScripts.Rows)
                        {
                            DevFile.Scripts[l_y].ScriptNumber = (UInt16)row["ScriptNumber"];
                            DevFile.Scripts[l_y].ScriptName = (String)row["ScriptName"];
                            DevFile.Scripts[l_y].ScriptVersion = (UInt16)row["ScriptVersion"];
                            DevFile.Scripts[l_y].UNUSED1 = (UInt32)row["UNUSED1"];
                            DevFile.Scripts[l_y].ScriptLength = (UInt16)row["ScriptLength"];
                            DevFile.Scripts[l_y].Script = (ushort[])row["Script"];
                            DevFile.Scripts[l_y].Comment = (String)row["Comment"];

                            l_y++;
                        }
                        //addTblScripts(tblScripts, DevFile.Scripts);

                        binRead.Close();
                    }
                    fsDevFile.Close();

                    //dsDeviceFile.WriteXmlSchema("c:\\temp\\dsDeviceFile.sch");

                    //dsDeviceFile.WriteXml("c:\\temp\\dsDeviceFile.xml", System.Data.XmlWriteMode.WriteSchema);

                }
                catch
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool ReadDeviceFileXML(string DeviceFileName)
        {
            string DeviceFileNameXML = DeviceFileName.Replace(".dat", ".xml");
            System.Data.DataSet dsDeviceFile;
            DTBL tblFamilies = new DTBL("Families");
            DTBL tblPartsList = new DTBL("PartsList");
            DTBL tblScripts = new DTBL("Scripts");

            if (File.Exists(DeviceFileNameXML))
            {
                try
                {
                    dsDeviceFile = new System.Data.DataSet("DeviceFile");
                    dsDeviceFile.ReadXml(DeviceFileNameXML, System.Data.XmlReadMode.ReadSchema);
                    tblFamilies = dsDeviceFile.Tables["Families"];
                    tblPartsList = dsDeviceFile.Tables["PartsList"];
                    tblScripts = dsDeviceFile.Tables["Scripts"];
                }
                catch
                {
                    return false;
                }
            }


            bool fileExists = File.Exists(DeviceFileName);
           
            if (fileExists)
            {
                try
                {
                    //FileStream fsDevFile = File.Open(DeviceFileName, FileMode.Open);
                    FileStream fsDevFile = File.OpenRead(DeviceFileName);
                    using (BinaryReader binRead = new BinaryReader(fsDevFile))
                    {
                        //
                        DevFile.Info.VersionMajor = binRead.ReadInt32();
                        DevFile.Info.VersionMinor = binRead.ReadInt32();
                        DevFile.Info.VersionDot = binRead.ReadInt32();
                        DevFile.Info.VersionNotes = binRead.ReadString();
                        DevFile.Info.NumberFamilies = binRead.ReadInt32();
                        DevFile.Info.NumberParts = binRead.ReadInt32() + tblPartsList.Rows.Count;
                        DevFile.Info.NumberScripts = binRead.ReadInt32() + tblScripts.Rows.Count;
                        DevFile.Info.NumberParts = tblPartsList.Rows.Count;
                        DevFile.Info.NumberScripts = tblScripts.Rows.Count;
                        DevFile.Info.Compatibility = binRead.ReadByte();
                        DevFile.Info.UNUSED1A = binRead.ReadByte();
                        DevFile.Info.UNUSED1B = binRead.ReadUInt16();
                        DevFile.Info.UNUSED2 = binRead.ReadUInt32();

                        //addTblInfo(tblInfo, DevFile.Info);

                        // create a version string
                        DeviceFileVersion = string.Format("{0:D1}.{1:D2}.{2:D2}", DevFile.Info.VersionMajor,
                                         DevFile.Info.VersionMinor, DevFile.Info.VersionDot);
                        //
                        // Declare arrays
                        //
                        DevFile.Families = new DeviceFile.DeviceFamilyParams[DevFile.Info.NumberFamilies];
                        DevFile.PartsList = new DeviceFile.DevicePartParams[DevFile.Info.NumberParts];
                        DevFile.Scripts = new DeviceFile.DeviceScripts[DevFile.Info.NumberScripts];
               
                        //timijk 2015.06.08
                        //replace the DevFile.Families if(tblFamilies.Rows.Count>0)
                        if (tblFamilies.Rows.Count > 0)
                        {
                            int l_x = 0;

                            DevFile.Info.NumberFamilies = tblFamilies.Rows.Count;

                            DevFile.Families = new DeviceFile.DeviceFamilyParams[DevFile.Info.NumberFamilies];

                            foreach (DataRow row in tblFamilies.Rows)
                            {
                                DevFile.Families[l_x].FamilyID = (UInt16)row["FamilyID"];
                                DevFile.Families[l_x].FamilyType = (UInt16)row["FamilyType"];
                                DevFile.Families[l_x].SearchPriority = (UInt16)row["SearchPriority"];
                                DevFile.Families[l_x].FamilyName = (String)row["FamilyName"];
                                DevFile.Families[l_x].ProgEntryScript = (UInt16)row["ProgEntryScript"];
                                DevFile.Families[l_x].ProgExitScript = (UInt16)row["ProgExitScript"];
                                DevFile.Families[l_x].ReadDevIDScript = (UInt16)row["ReadDevIDScript"];
                                DevFile.Families[l_x].DeviceIDMask = (UInt32)row["DeviceIDMask"];
                                DevFile.Families[l_x].BlankValue = (UInt32)row["BlankValue"];
                                DevFile.Families[l_x].BytesPerLocation = (Byte)row["BytesPerLocation"];
                                DevFile.Families[l_x].AddressIncrement = (Byte)row["AddressIncrement"];
                                DevFile.Families[l_x].PartDetect = (Boolean)row["PartDetect"];
                                DevFile.Families[l_x].ProgEntryVPPScript = (UInt16)row["ProgEntryVPPScript"];
                                DevFile.Families[l_x].UNUSED1 = (UInt16)row["UNUSED1"];
                                DevFile.Families[l_x].EEMemBytesPerWord = (Byte)row["EEMemBytesPerWord"];
                                DevFile.Families[l_x].EEMemAddressIncrement = (Byte)row["EEMemAddressIncrement"];
                                DevFile.Families[l_x].UserIDHexBytes = (Byte)row["UserIDHexBytes"];
                                DevFile.Families[l_x].UserIDBytes = (Byte)row["UserIDBytes"];
                                DevFile.Families[l_x].ProgMemHexBytes = (Byte)row["ProgMemHexBytes"];
                                DevFile.Families[l_x].EEMemHexBytes = (Byte)row["EEMemHexBytes"];
                                DevFile.Families[l_x].ProgMemShift = (Byte)row["ProgMemShift"];
                                DevFile.Families[l_x].TestMemoryStart = (UInt32)row["TestMemoryStart"];
                                DevFile.Families[l_x].TestMemoryLength = (UInt16)row["TestMemoryLength"];
                                DevFile.Families[l_x].Vpp = (Single)row["Vpp"];

                                //timijk 2017.02.21
                                //if (DevFile.Families[l_x].FamilyName == "PIC32" && tblPartsList.Rows.Count > 0)
                                //{ DevFile.Families[l_x].DeviceIDMask = 0xFFFF000; }

                                l_x++;
                            }

                        }
                        
                        // Create family search table based on priority
                        familySearchTable = new int[DevFile.Info.NumberFamilies];
                        for (int familyIdx = 0; familyIdx < DevFile.Info.NumberFamilies; familyIdx++)
                        {
                            familySearchTable[DevFile.Families[familyIdx].SearchPriority] = familyIdx;
                        }
                        //
                        // now read all parts if they are there
                        //
                        int l_y = DevFile.Info.NumberParts - tblPartsList.Rows.Count;              

                        foreach (DataRow row in tblPartsList.Rows)
                        {
                            DevFile.PartsList[l_y].PartName = (String)row["PartName"];
                            DevFile.PartsList[l_y].Family = (UInt16)row["Family"];
                            DevFile.PartsList[l_y].DeviceID = (UInt32)row["DeviceID"] & DevFile.Families[DevFile.PartsList[l_y].Family].DeviceIDMask;
                            DevFile.PartsList[l_y].ProgramMem = (UInt32)row["ProgramMem"];
                            DevFile.PartsList[l_y].EEMem = (UInt16)row["EEMem"];
                            DevFile.PartsList[l_y].EEAddr = (UInt32)row["EEAddr"];
                            DevFile.PartsList[l_y].ConfigWords = (Byte)row["ConfigWords"];
                            DevFile.PartsList[l_y].ConfigAddr = (UInt32)row["ConfigAddr"];
                            DevFile.PartsList[l_y].UserIDWords = (Byte)row["UserIDWords"];
                            DevFile.PartsList[l_y].UserIDAddr = (UInt32)row["UserIDAddr"];
                            DevFile.PartsList[l_y].BandGapMask = (UInt32)row["BandGapMask"];
                            // Init config arrays
                            DevFile.PartsList[l_y].ConfigMasks = (ushort[])row["ConfigMasks"];
                            DevFile.PartsList[l_y].ConfigBlank = (ushort[])row["ConfigBlank"];
                            DevFile.PartsList[l_y].CPMask = (UInt16)row["CPMask"];
                            DevFile.PartsList[l_y].CPConfig = (Byte)row["CPConfig"];
                            DevFile.PartsList[l_y].OSSCALSave = (Boolean)row["OSSCALSave"];
                            DevFile.PartsList[l_y].IgnoreAddress = (UInt32)row["IgnoreAddress"];
                            DevFile.PartsList[l_y].VddMin = (Single)row["VddMin"];
                            DevFile.PartsList[l_y].VddMax = (Single)row["VddMax"];
                            DevFile.PartsList[l_y].VddErase = (Single)row["VddErase"];
                            DevFile.PartsList[l_y].CalibrationWords = (Byte)row["CalibrationWords"];
                            DevFile.PartsList[l_y].ChipEraseScript = (UInt16)row["ChipEraseScript"];
                            DevFile.PartsList[l_y].ProgMemAddrSetScript = (UInt16)row["ProgMemAddrSetScript"];
                            DevFile.PartsList[l_y].ProgMemAddrBytes = (Byte)row["ProgMemAddrBytes"];
                            DevFile.PartsList[l_y].ProgMemRdScript = (UInt16)row["ProgMemRdScript"];
                            DevFile.PartsList[l_y].ProgMemRdWords = (UInt16)row["ProgMemRdWords"];
                            DevFile.PartsList[l_y].EERdPrepScript = (UInt16)row["EERdPrepScript"];
                            DevFile.PartsList[l_y].EERdScript = (UInt16)row["EERdScript"];
                            DevFile.PartsList[l_y].EERdLocations = (UInt16)row["EERdLocations"];
                            DevFile.PartsList[l_y].UserIDRdPrepScript = (UInt16)row["UserIDRdPrepScript"];
                            DevFile.PartsList[l_y].UserIDRdScript = (UInt16)row["UserIDRdScript"];
                            DevFile.PartsList[l_y].ConfigRdPrepScript = (UInt16)row["ConfigRdPrepScript"];
                            DevFile.PartsList[l_y].ConfigRdScript = (UInt16)row["ConfigRdScript"];
                            DevFile.PartsList[l_y].ProgMemWrPrepScript = (UInt16)row["ProgMemWrPrepScript"];
                            DevFile.PartsList[l_y].ProgMemWrScript = (UInt16)row["ProgMemWrScript"];
                            DevFile.PartsList[l_y].ProgMemWrWords = (UInt16)row["ProgMemWrWords"];
                            DevFile.PartsList[l_y].ProgMemPanelBufs = (Byte)row["ProgMemPanelBufs"];
                            DevFile.PartsList[l_y].ProgMemPanelOffset = (UInt32)row["ProgMemPanelOffset"];
                            DevFile.PartsList[l_y].EEWrPrepScript = (UInt16)row["EEWrPrepScript"];
                            DevFile.PartsList[l_y].EEWrScript = (UInt16)row["EEWrScript"];
                            DevFile.PartsList[l_y].EEWrLocations = (UInt16)row["EEWrLocations"];
                            DevFile.PartsList[l_y].UserIDWrPrepScript = (UInt16)row["UserIDWrPrepScript"];
                            DevFile.PartsList[l_y].UserIDWrScript = (UInt16)row["UserIDWrScript"];
                            DevFile.PartsList[l_y].ConfigWrPrepScript = (UInt16)row["ConfigWrPrepScript"];
                            DevFile.PartsList[l_y].ConfigWrScript = (UInt16)row["ConfigWrScript"];
                            DevFile.PartsList[l_y].OSCCALRdScript = (UInt16)row["OSCCALRdScript"];
                            DevFile.PartsList[l_y].OSCCALWrScript = (UInt16)row["OSCCALWrScript"];
                            DevFile.PartsList[l_y].DPMask = (UInt16)row["DPMask"];
                            DevFile.PartsList[l_y].WriteCfgOnErase = (Boolean)row["WriteCfgOnErase"];
                            DevFile.PartsList[l_y].BlankCheckSkipUsrIDs = (Boolean)row["BlankCheckSkipUsrIDs"];
                            DevFile.PartsList[l_y].IgnoreBytes = (UInt16)row["IgnoreBytes"];
                            DevFile.PartsList[l_y].ChipErasePrepScript = (UInt16)row["ChipErasePrepScript"];
                            DevFile.PartsList[l_y].BootFlash = (UInt32)row["BootFlash"];
                            //DevFile.PartsList[l_y].UNUSED4 = (UInt32)row[""];
                            DevFile.PartsList[l_y].Config9Mask = (UInt16)row["Config9Mask"];
                            //DevFile.PartsList[l_y].ConfigMasks[8] = DevFile.PartsList[l_y].Config9Mask;
                            DevFile.PartsList[l_y].Config9Blank = (UInt16)row["Config9Blank"];
                            //DevFile.PartsList[l_y].ConfigBlank[8] = DevFile.PartsList[l_y].Config9Blank;
                            DevFile.PartsList[l_y].ProgMemEraseScript = (UInt16)row["ProgMemEraseScript"];
                            DevFile.PartsList[l_y].EEMemEraseScript = (UInt16)row["EEMemEraseScript"];
                            DevFile.PartsList[l_y].ConfigMemEraseScript = (UInt16)row["ConfigMemEraseScript"];
                            DevFile.PartsList[l_y].reserved1EraseScript = (UInt16)row["reserved1EraseScript"];
                            DevFile.PartsList[l_y].reserved2EraseScript = (UInt16)row["reserved2EraseScript"];
                            DevFile.PartsList[l_y].TestMemoryRdScript = (UInt16)row["TestMemoryRdScript"];
                            DevFile.PartsList[l_y].TestMemoryRdWords = (UInt16)row["TestMemoryRdWords"];
                            DevFile.PartsList[l_y].EERowEraseScript = (UInt16)row["EERowEraseScript"];
                            DevFile.PartsList[l_y].EERowEraseWords = (UInt16)row["EERowEraseWords"];
                            DevFile.PartsList[l_y].ExportToMPLAB = (Boolean)row["ExportToMPLAB"];
                            DevFile.PartsList[l_y].DebugHaltScript = (UInt16)row["DebugHaltScript"];
                            DevFile.PartsList[l_y].DebugRunScript = (UInt16)row["DebugRunScript"];
                            DevFile.PartsList[l_y].DebugStatusScript = (UInt16)row["DebugStatusScript"];
                            DevFile.PartsList[l_y].DebugReadExecVerScript = (UInt16)row["DebugReadExecVerScript"];
                            DevFile.PartsList[l_y].DebugSingleStepScript = (UInt16)row["DebugSingleStepScript"];
                            DevFile.PartsList[l_y].DebugBulkWrDataScript = (UInt16)row["DebugBulkWrDataScript"];
                            DevFile.PartsList[l_y].DebugBulkRdDataScript = (UInt16)row["DebugBulkRdDataScript"];
                            DevFile.PartsList[l_y].DebugWriteVectorScript = (UInt16)row["DebugWriteVectorScript"];
                            DevFile.PartsList[l_y].DebugReadVectorScript = (UInt16)row["DebugReadVectorScript"];
                            DevFile.PartsList[l_y].DebugRowEraseScript = (UInt16)row["DebugRowEraseScript"];
                            DevFile.PartsList[l_y].DebugRowEraseSize = (UInt16)row["DebugRowEraseSize"];
                            DevFile.PartsList[l_y].DebugReserved5Script = (UInt16)row["DebugReserved5Script"];
                            DevFile.PartsList[l_y].DebugReserved6Script = (UInt16)row["DebugReserved6Script"];
                            DevFile.PartsList[l_y].DebugReserved7Script = (UInt16)row["DebugReserved7Script"];
                            DevFile.PartsList[l_y].DebugReserved8Script = (UInt16)row["DebugReserved8Script"];
                            DevFile.PartsList[l_y].LVPScript = (UInt16)row["LVPScript"];

                            l_y++;
                        }
                        
                        //
                        // now read all scripts if they are there
                        //                    
                        l_y = DevFile.Info.NumberScripts - tblScripts.Rows.Count;

                        foreach (DataRow row in tblScripts.Rows)
                        {
                            DevFile.Scripts[l_y].ScriptNumber = (UInt16)row["ScriptNumber"];
                            DevFile.Scripts[l_y].ScriptName = (String)row["ScriptName"];
                            DevFile.Scripts[l_y].ScriptVersion = (UInt16)row["ScriptVersion"];
                            DevFile.Scripts[l_y].UNUSED1 = (UInt32)row["UNUSED1"];
                            DevFile.Scripts[l_y].ScriptLength = (UInt16)row["ScriptLength"];
                            DevFile.Scripts[l_y].Script = (ushort[])row["Script"];
                            DevFile.Scripts[l_y].Comment = (String)row["Comment"];

                            l_y++;
                        }

                        binRead.Close();
                    }
                    fsDevFile.Close();
                                    
                }
                catch
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool WriteDeviceFileDAT(string DeviceFileName)
        {
            FileStream fsDevFile;

            try
            {
                fsDevFile = File.OpenWrite(DeviceFileName);
            }
            catch
            {
                return false;
            }

            bool fileExists = File.Exists(DeviceFileName);
                   
            if (fileExists)
            {
                try
                {                   
                    using ( BinaryWriter binWrite = new BinaryWriter(fsDevFile))
                    {
                        //timijk 2017.02.21
                        DevFile.Info.VersionNotes = "PIC32MM Errata: Reset Module, Chip Erase";
                        DevFile.Info.NumberParts += 8; //duplicate for another 8 devices
                        //
                        binWrite.Write(DevFile.Info.VersionMajor);
                        binWrite.Write(DevFile.Info.VersionMinor);
                        binWrite.Write(DevFile.Info.VersionDot);
                        binWrite.Write(DevFile.Info.VersionNotes);
                        binWrite.Write(DevFile.Info.NumberFamilies);
                        binWrite.Write(DevFile.Info.NumberParts);
                        binWrite.Write(DevFile.Info.NumberScripts);
                        binWrite.Write(DevFile.Info.Compatibility);
                        binWrite.Write(DevFile.Info.UNUSED1A);
                        binWrite.Write(DevFile.Info.UNUSED1B);
                        binWrite.Write(DevFile.Info.UNUSED2);

                        //
                        // now read all families if they are there
                        //
                        for (int l_x = 0; l_x < DevFile.Info.NumberFamilies; l_x++)
                        {
                            binWrite.Write(DevFile.Families[l_x].FamilyID);
                            binWrite.Write(DevFile.Families[l_x].FamilyType);
                            binWrite.Write(DevFile.Families[l_x].SearchPriority);
                            binWrite.Write(DevFile.Families[l_x].FamilyName);
                            binWrite.Write(DevFile.Families[l_x].ProgEntryScript);
                            binWrite.Write(DevFile.Families[l_x].ProgExitScript);
                            binWrite.Write(DevFile.Families[l_x].ReadDevIDScript);
                            binWrite.Write(DevFile.Families[l_x].DeviceIDMask);
                            binWrite.Write(DevFile.Families[l_x].BlankValue);
                            binWrite.Write(DevFile.Families[l_x].BytesPerLocation);
                            binWrite.Write(DevFile.Families[l_x].AddressIncrement);
                            binWrite.Write(DevFile.Families[l_x].PartDetect);
                            binWrite.Write(DevFile.Families[l_x].ProgEntryVPPScript);
                            binWrite.Write(DevFile.Families[l_x].UNUSED1);
                            binWrite.Write(DevFile.Families[l_x].EEMemBytesPerWord);
                            binWrite.Write(DevFile.Families[l_x].EEMemAddressIncrement);
                            binWrite.Write(DevFile.Families[l_x].UserIDHexBytes);
                            binWrite.Write(DevFile.Families[l_x].UserIDBytes);
                            binWrite.Write(DevFile.Families[l_x].ProgMemHexBytes);
                            binWrite.Write(DevFile.Families[l_x].EEMemHexBytes);
                            binWrite.Write(DevFile.Families[l_x].ProgMemShift);
                            binWrite.Write(DevFile.Families[l_x].TestMemoryStart);
                            binWrite.Write(DevFile.Families[l_x].TestMemoryLength);
                            binWrite.Write(DevFile.Families[l_x].Vpp);

                        }
                                         
                        //
                        // now read all parts if they are there
                        //
                        //int l_y = DevFile.Info.NumberParts;
                        
                        for (int l_x = 0, l_y = 0; l_y < DevFile.Info.NumberParts; l_y++)
                        {
                            if (l_y == 0) l_x = l_y;
                            else
                            {
                                l_x = 1;

                                DevFile.PartsList[l_x].PartName = PIC32MM_List[l_y-1].PartName;
                                DevFile.PartsList[l_x].DeviceID = PIC32MM_List[l_y-1].DeviceID & DevFile.Families[DevFile.PartsList[l_x].Family].DeviceIDMask;
                                DevFile.PartsList[l_x].ProgramMem = PIC32MM_List[l_y-1].ProgramMem;
                            }

                            binWrite.Write(DevFile.PartsList[l_x].PartName);
                            binWrite.Write(DevFile.PartsList[l_x].Family);
                            binWrite.Write(DevFile.PartsList[l_x].DeviceID);
                            binWrite.Write(DevFile.PartsList[l_x].ProgramMem);
                            binWrite.Write(DevFile.PartsList[l_x].EEMem);
                            binWrite.Write(DevFile.PartsList[l_x].EEAddr);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigWords);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigAddr);
                            binWrite.Write(DevFile.PartsList[l_x].UserIDWords);
                            binWrite.Write(DevFile.PartsList[l_x].UserIDAddr);
                            binWrite.Write(DevFile.PartsList[l_x].BandGapMask);
                            // Init config arrays                     
                            for (int l_index = 0; l_index < KONST.MaxReadCfgMasks; l_index++)
                            {
                                binWrite.Write(DevFile.PartsList[l_x].ConfigMasks[l_index]);
                            }

                            for (int l_index = 0; l_index < KONST.MaxReadCfgMasks; l_index++)
                            {
                                binWrite.Write(DevFile.PartsList[l_x].ConfigBlank[l_index]);
                            }

                            binWrite.Write(DevFile.PartsList[l_x].CPMask);
                            binWrite.Write(DevFile.PartsList[l_x].CPConfig);
                            binWrite.Write(DevFile.PartsList[l_x].OSSCALSave);
                            binWrite.Write(DevFile.PartsList[l_x].IgnoreAddress);
                            binWrite.Write(DevFile.PartsList[l_x].VddMin);
                            binWrite.Write(DevFile.PartsList[l_x].VddMax);
                            binWrite.Write(DevFile.PartsList[l_x].VddErase);
                            binWrite.Write(DevFile.PartsList[l_x].CalibrationWords);
                            binWrite.Write(DevFile.PartsList[l_x].ChipEraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemAddrSetScript);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemAddrBytes);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemRdScript);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemRdWords);
                            binWrite.Write(DevFile.PartsList[l_x].EERdPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].EERdScript);
                            binWrite.Write(DevFile.PartsList[l_x].EERdLocations);
                            binWrite.Write(DevFile.PartsList[l_x].UserIDRdPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].UserIDRdScript);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigRdPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigRdScript);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemWrPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemWrScript);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemWrWords);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemPanelBufs);
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemPanelOffset);
                            binWrite.Write(DevFile.PartsList[l_x].EEWrPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].EEWrScript);
                            binWrite.Write(DevFile.PartsList[l_x].EEWrLocations);
                            binWrite.Write(DevFile.PartsList[l_x].UserIDWrPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].UserIDWrScript);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigWrPrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigWrScript);
                            binWrite.Write(DevFile.PartsList[l_x].OSCCALRdScript);
                            binWrite.Write(DevFile.PartsList[l_x].OSCCALWrScript);
                            binWrite.Write(DevFile.PartsList[l_x].DPMask);
                            binWrite.Write(DevFile.PartsList[l_x].WriteCfgOnErase);
                            binWrite.Write(DevFile.PartsList[l_x].BlankCheckSkipUsrIDs);
                            binWrite.Write(DevFile.PartsList[l_x].IgnoreBytes);
                            binWrite.Write(DevFile.PartsList[l_x].ChipErasePrepScript);
                            binWrite.Write(DevFile.PartsList[l_x].BootFlash);
                            //binWrite.Write(DevFile.PartsList[l_x].UNUSED4);
                            binWrite.Write(DevFile.PartsList[l_x].Config9Mask);
                            //binWrite.Write(DevFile.PartsList[l_x].ConfigMasks[8]); //timijk 2017.02.21 double check
                            binWrite.Write(DevFile.PartsList[l_x].Config9Blank);
                            //binWrite.Write(DevFile.PartsList[l_x].ConfigBlank[8]); //timijk 2017.02.21 double check
                            binWrite.Write(DevFile.PartsList[l_x].ProgMemEraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].EEMemEraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].ConfigMemEraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].reserved1EraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].reserved2EraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].TestMemoryRdScript);
                            binWrite.Write(DevFile.PartsList[l_x].TestMemoryRdWords);
                            binWrite.Write(DevFile.PartsList[l_x].EERowEraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].EERowEraseWords);
                            binWrite.Write(DevFile.PartsList[l_x].ExportToMPLAB);
                            binWrite.Write(DevFile.PartsList[l_x].DebugHaltScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugRunScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugStatusScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugReadExecVerScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugSingleStepScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugBulkWrDataScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugBulkRdDataScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugWriteVectorScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugReadVectorScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugRowEraseScript);
                            binWrite.Write(DevFile.PartsList[l_x].DebugRowEraseSize);
                            binWrite.Write(DevFile.PartsList[l_x].DebugReserved5Script);
                            binWrite.Write(DevFile.PartsList[l_x].DebugReserved6Script);
                            binWrite.Write(DevFile.PartsList[l_x].DebugReserved7Script);
                            binWrite.Write(DevFile.PartsList[l_x].DebugReserved8Script);
                            //binWrite.Write(DevFile.PartsList[l_x].DebugReserved9Script);                                                       
                            binWrite.Write(DevFile.PartsList[l_x].LVPScript);

                        }

                        //
                        // now read all scripts if they are there
                        //                    
                        for (int l_x = 0; l_x < DevFile.Info.NumberScripts; l_x++)
                        {
                            binWrite.Write(DevFile.Scripts[l_x].ScriptNumber);
                            binWrite.Write(DevFile.Scripts[l_x].ScriptName);
                            binWrite.Write(DevFile.Scripts[l_x].ScriptVersion);
                            binWrite.Write(DevFile.Scripts[l_x].UNUSED1);
                            binWrite.Write(DevFile.Scripts[l_x].ScriptLength);
                            // init script array
                            for (int l_index = 0; l_index < DevFile.Scripts[l_x].ScriptLength; l_index++)
                            {
                                binWrite.Write(DevFile.Scripts[l_x].Script[l_index]);
                            }
                            binWrite.Write(DevFile.Scripts[l_x].Comment);

                        }

                        binWrite.Close();
                    }
                    fsDevFile.Close();

                   
                }
                catch
                {
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
