using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using ReaderB;

namespace kapian
{
    public partial class Form1 : Form
    {
        private bool fAppClosed; //在测试模式下响应关闭应用程序
        private byte fComAdr = 0xff; //当前操作的ComAdr
        private int ferrorcode;
        private byte fBaud;
        private double fdminfre;
        private double fdmaxfre;
        private byte Maskadr;
        private byte MaskLen;
        private byte MaskFlag;
        private int fCmdRet = 30; //所有执行指令的返回值
        private int fOpenComIndex; //打开的串口索引号
        private bool fIsInventoryScan;
        private bool fisinventoryscan_6B;
        private byte[] fOperEPC = new byte[36];
        private byte[] fPassWord = new byte[4];
        private byte[] fOperID_6B = new byte[8];
        private int CardNum1 = 0;
        ArrayList list = new ArrayList();
        private bool fTimer_6B_ReadWrite;
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        private int frmcomportindex;
        private bool ComOpen = false;
        private bool breakflag = false;
        private double x_z;
        private double y_f;
        private string epc = "";//读数据、写数据、块擦除 原ComboBox_EPC2存放数据
        public Form1()
        {
            InitializeComponent();
            InitComList();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            int port = 0;
            int openresult, i;
            openresult = 30;
            string temp;
            Cursor = Cursors.WaitCursor;
            if (Edit_CmdComAddr.Text == "")
                Edit_CmdComAddr.Text = "FF";
            fComAdr = Convert.ToByte(Edit_CmdComAddr.Text, 16); // $FF;
            fComAdr = Convert.ToByte("FF", 16);
            try
            {
                if (ComboBox_COM.SelectedIndex == 0)//Auto
                {
                    fBaud = Convert.ToByte(ComboBox_baud2.SelectedIndex);
                    if (fBaud > 2)
                        fBaud = Convert.ToByte(fBaud + 2);
                    openresult = StaticClassReaderB.AutoOpenComPort(ref port, ref fComAdr, fBaud, ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    if (openresult == 0)
                    {
                        ComOpen = true;
                        // Button3_Click(sender, e); //自动执行读取写卡器信息
                        //if (fBaud > 3)
                        //{
                        //    ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                        //}
                        //else
                        //{
                        //    ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                        //}
                        //Button3_Click(sender, e); //自动执行读取写卡器信息
                        if ((fCmdRet == 0x35) | (fCmdRet == 0x30))
                        {
                            MessageBox.Show("串口通讯错误", "信息提示");
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                            ComOpen = false;
                        }
                    }
                }
                else
                {
                    temp = ComboBox_COM.SelectedItem.ToString();
                    temp = temp.Trim();
                    port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                    for (i = 6; i >= 0; i--)
                    {
                        fBaud = Convert.ToByte(i);
                        if (fBaud == 3)
                            continue;
                        openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                        fOpenComIndex = frmcomportindex;
                        if (openresult == 0x35)
                        {
                            MessageBox.Show("串口已打开", "信息提示");
                            return;
                        }
                        if (openresult == 0)
                        {
                            ComOpen = true;
                            //Button3_Click(sender, e); //自动执行读取写卡器信息
                            //if (fBaud > 3)
                            //{
                            //    ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                            //}
                            //else
                            //{
                            //    ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                            //}
                            if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                            {
                                ComOpen = false;
                                MessageBox.Show("串口通讯错误", "信息提示");
                                StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                                return;
                            }
                            //RefreshStatus();
                            break;
                        }

                    }
                }
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            string com = "";
            if ((fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30))
            {
                //ComboBox_AlreadyOpenCOM.Items.Add("COM" + Convert.ToString(fOpenComIndex));
                //ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
                com = Convert.ToString(fOpenComIndex);
                //Button3.Enabled = true;
                //button20.Enabled = true;
                //Button5.Enabled = true;
                //Button1.Enabled = true;
                //button2.Enabled = true;
                //Button_WriteEPC_G2.Enabled = true;
                //Button_SetMultiReadProtect_G2.Enabled = true;
                //Button_RemoveReadProtect_G2.Enabled = true;
                //Button_CheckReadProtected_G2.Enabled = true;
                //button4.Enabled = true;
                //SpeedButton_Query_6B.Enabled = true;
                //button6.Enabled = true;
                //button8.Enabled = true;
                //button9.Enabled = true;
                //button12.Enabled = true;
                //button_OffsetTime.Enabled = true;
                //button_settigtime.Enabled = true;
                //button_gettigtime.Enabled = true;
                ComOpen = true;
            }
            if ((fOpenComIndex == -1) && (openresult == 0x30))
                MessageBox.Show("串口通讯错误", "信息提示");

            byte[] TrType = new byte[2];
            byte[] VersionInfo = new byte[2];
            byte ReaderType = 0;
            byte ScanTime = 0;
            byte dmaxfre = 0;
            byte dminfre = 0;
            byte powerdBm = 0;
            
            fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime, frmcomportindex);
            //if ((ComboBox_AlreadyOpenCOM.Items.Count != 0) & (fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30) & (fCmdRet == 0))
            if ((com!="") & (fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30) & (fCmdRet == 0))
            {
                fComAdr = Convert.ToByte("00", 16);
                temp = "COM"+com;
                frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
            }
            //RefreshStatus();
            lbl_status.Text = startRun(1);
            timer1.Enabled = true;//主动模式
        }
        #region 初始化端口，选择59732波特率，选择主动运行
        private void InitComList()
        {
            int i = 0;
            ComboBox_COM.Items.Clear();
            ComboBox_COM.Items.Add(" AUTO");
            for (i = 1; i < 13; i++)
                ComboBox_COM.Items.Add(" COM" + Convert.ToString(i));
            ComboBox_COM.SelectedIndex = 0;
            //RefreshStatus();
            ComboBox_baud2.SelectedIndex = 3;
            comboBox4.SelectedIndex = 1;
        }
        #endregion

        private void btn_set_Click(object sender, EventArgs e)
        {
            //lbl_status.Text = startRun(1);
            //int Reader_bit0;
            //int Reader_bit1;
            //int Reader_bit2;
            //int Reader_bit3;
            //int Reader_bit4;
            //byte[] Parameter = new byte[6];
            ////Parameter[0] = Convert.ToByte(comboBox4.SelectedIndex);//工作模式
            //Parameter[0] = Convert.ToByte(1);//主动工作模式
            //Reader_bit0 = 0;//EU band
            //Reader_bit1 = 1;//输出模式 RS232/RS485输出？？
            //Reader_bit2 = 0;//开启蜂鸣器
            //Reader_bit3 = 0;//字字节
            //Reader_bit4 = 0;//不是 SYRIS485输出
            //Parameter[1] = Convert.ToByte(Reader_bit0 * 1 + Reader_bit1 * 2 + Reader_bit2 * 4 + Reader_bit3 * 8 + Reader_bit4 * 16);
            //Parameter[2] = 3;//用户区
            //Parameter[3] = Convert.ToByte("02", 16);//起始字地址
            //Parameter[4] = Convert.ToByte(48);//读取字数
            //Parameter[5] = Convert.ToByte(5); //单张过滤时间
            //fCmdRet = StaticClassReaderB.SetWorkMode(ref fComAdr, Parameter, frmcomportindex);
            //timer1.Enabled = true;//主动模式
            //if (fCmdRet == 0)
            //{
            //    //应答模式
            //    if (comboBox4.SelectedIndex == 0)
            //    {
            //        button10.Enabled = false;
            //        //button11.Enabled = false;//清除按钮
            //        button10.Text = "获取";
            //        timer1.Enabled = false;
            //    }
            //}
            //else {
            //    if (comboBox4.SelectedIndex == 1)
            //    {
            //        timer1.Enabled = true;//主动模式
            //    }
                
            //}
            //AddCmdLog("SetWorkMode", "设置", fCmdRet);
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled)
            {
                lbl_status.Text=startRun(0);
                button10.Text = "继续运行";
                comboBox4.SelectedIndex = 0;
            }
            else
            {
                lbl_status.Text=startRun(1);
                button10.Text = "停止读取";
                comboBox4.SelectedIndex = 1;
            }
        }
        /// <summary>
        ///  返回中文运行状态
        /// </summary>
        /// <param name="zhudong">1是主动0是应答</param>
        /// <returns></returns>
        private string startRun(int zhudong) {
            int Reader_bit0;
            int Reader_bit1;
            int Reader_bit2;
            int Reader_bit3;
            int Reader_bit4;
            byte[] Parameter = new byte[6];
            //Parameter[0] = Convert.ToByte(comboBox4.SelectedIndex);//工作模式
            Parameter[0] = Convert.ToByte(zhudong);//主动工作模式
            Reader_bit0 = 0;//EU band
            Reader_bit1 = 1;//输出模式 RS232/RS485输出？？
            Reader_bit2 = 0;//开启蜂鸣器
            Reader_bit3 = 0;//字字节
            Reader_bit4 = 0;//不是 SYRIS485输出
            Parameter[1] = Convert.ToByte(Reader_bit0 * 1 + Reader_bit1 * 2 + Reader_bit2 * 4 + Reader_bit3 * 8 + Reader_bit4 * 16);
            Parameter[2] = 3;//用户区
            Parameter[3] = Convert.ToByte("02", 16);//起始字地址
            Parameter[4] = Convert.ToByte(48);//读取字数
            Parameter[5] = Convert.ToByte(5); //单张过滤时间
            fCmdRet = StaticClassReaderB.SetWorkMode(ref fComAdr, Parameter, frmcomportindex);
            return fCmdRet > 0 ? "正在运行" : "停止运行";
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                fIsInventoryScan = true;
            GetData();
            if (fAppClosed)
                Close();
            fIsInventoryScan = false;
        }
        private void GetData()
        {
            byte[] ScanModeData = new byte[40960];
            int ValidDatalength, i;
            string temp, temps;
            ValidDatalength = 0;
            fCmdRet = StaticClassReaderB.ReadActiveModeData(ScanModeData, ref ValidDatalength, frmcomportindex);
            if (fCmdRet == 0)
            {
                temp = "";
                temps = ByteArrayToHexString(ScanModeData);
                for (i = 0; i < ValidDatalength; i++)
                {
                    temp = temp + temps.Substring(i * 2, 2) + " ";
                }
                listBox3.Items.Add(temp+"\n\t");
                listBox3.SelectedIndex = listBox3.Items.Count - 1;
            }
            // AddCmdLog("Get", "获取", fCmdRet);
        }
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            //查询标签
            
            if (fIsInventoryScan)
                return;
            while (true) {
                epc=Inventory();
                if (epc != "") break;
            }
            
            

            //读取标签
            //if (fIsInventoryScan)
            //    return;
            //fIsInventoryScan = true;
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            byte EPClength = 0;
            string str;
            byte[] CardData = new byte[320];
            //if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}

            MaskFlag = 0;//使能
            Maskadr = Convert.ToByte("00", 16);//掩模起始字节地址
            MaskLen = Convert.ToByte("00", 16);//掩模字节数
            //if (textBox1.Text == "")
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}
            //if (ComboBox_EPC2.Items.Count == 0)
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}
            //if (ComboBox_EPC2.SelectedItem == null)
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}
            str = epc;// ComboBox_EPC2.SelectedItem.ToString();
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(str.Length / 2);
            byte[] EPC = new byte[ENum * 2];
            EPC = HexStringToByteArray(str);
            //if (C_Reserve.Checked)
            //    Mem = 0;
            //if (C_EPC.Checked)
            //    Mem = 1;
            //if (C_TID.Checked)
            //    Mem = 2;
            //if (C_User.Checked)
                Mem = 3;
            //if (Edit_AccessCode2.Text == "")
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}
            //if (Edit_WordPtr.Text == "")
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}
                WordPtr = Convert.ToByte("00", 16);//起始地址:(字/16进制数)
                Num = Convert.ToByte(4);//读/块擦除长度：(0-120/字/10进制数)
            //if (Edit_AccessCode2.Text.Length != 8)
            //{
            //    fIsInventoryScan = false;
            //    return;
            //}
            fPassWord = HexStringToByteArray("00000000");//访问密码
            fCmdRet = StaticClassReaderB.ReadCard_G2(ref fComAdr, EPC, Mem, WordPtr, Num, fPassWord, Maskadr, MaskLen, MaskFlag, CardData, EPClength, ref ferrorcode, frmcomportindex);
            if (fCmdRet == 0)
            {
                byte[] daw = new byte[Num * 2];
                Array.Copy(CardData, daw, Num * 2);
                //listBox1.Items.Add(ByteArrayToHexString(daw));
                //listBox1.SelectedIndex = listBox1.Items.Count - 1;
                //AddCmdLog("ReadData", "读", fCmdRet);
                textBox1.Text = ByteArrayToHexString(daw);
            }
            //if (ferrorcode != -1)
            //{
            //    StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +
            //     " '读' 返回错误=0x" + Convert.ToString(ferrorcode, 2) +
            //     "(" + GetErrorCodeDesc(ferrorcode) + ")";
            //    ferrorcode = -1;
            //}
            //fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }
        private string Inventory()
        {
            int i;
            int CardNum = 0;
            int Totallen = 0;
            int EPClen, m;
            byte[] EPC = new byte[5000];
            int CardIndex;
            string temps;
            string s, sEPC;
            bool isonlistview;
            fIsInventoryScan = true;
            byte AdrTID = 0;
            byte LenTID = 0;
            byte TIDFlag = 0;
            sEPC = "";
                AdrTID = 0;
                LenTID = 0;
                TIDFlag = 0;
            
            ListViewItem aListItem = new ListViewItem();
            fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, frmcomportindex);
            if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))//代表已查找结束，
            {
                byte[] daw = new byte[Totallen];
                Array.Copy(EPC, daw, Totallen);
                temps = ByteArrayToHexString(daw);
                fInventory_EPC_List = temps;            //存贮记录
                m = 0;

                /*   while (ListView1_EPC.Items.Count < CardNum)
                  {
                      aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString());
                      aListItem.SubItems.Add("");
                      aListItem.SubItems.Add("");
                      aListItem.SubItems.Add("");
                 * 
                  }*/
                if (CardNum == 0)
                {
                    fIsInventoryScan = false;
                    return "";
                }
                for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                {
                    EPClen = daw[m];
                    sEPC = temps.Substring(m * 2 + 2, EPClen * 2);
                    m = m + EPClen + 1;
                    if (sEPC.Length != EPClen * 2)
                        return "";
                    //isonlistview = false;
                    //for (i = 0; i < ListView1_EPC.Items.Count; i++)     //判断是否在Listview列表内
                    //{
                    //    if (sEPC == ListView1_EPC.Items[i].SubItems[1].Text)
                    //    {
                    //        aListItem = ListView1_EPC.Items[i];
                    //        ChangeSubItem(aListItem, 1, sEPC);
                    //        isonlistview = true;
                    //    }
                    //}
                    //if (!isonlistview)
                    //{
                    //    aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString());
                    //    aListItem.SubItems.Add("");
                    //    aListItem.SubItems.Add("");
                    //    aListItem.SubItems.Add("");
                    //    s = sEPC;
                    //    ChangeSubItem(aListItem, 1, s);
                    //    s = (sEPC.Length / 2).ToString().PadLeft(2, '0');
                    //    ChangeSubItem(aListItem, 2, s);
                    //    if (!CheckBox_TID.Checked)
                    //    {
                    //        if (ComboBox_EPC1.Items.IndexOf(sEPC) == -1)
                    //        {
                    //            ComboBox_EPC1.Items.Add(sEPC);
                    //            ComboBox_EPC2.Items.Add(sEPC);
                    //            ComboBox_EPC3.Items.Add(sEPC);
                    //            ComboBox_EPC4.Items.Add(sEPC);
                    //            ComboBox_EPC5.Items.Add(sEPC);
                    //            ComboBox_EPC6.Items.Add(sEPC);
                    //        }
                    //    }

                    //}
                }
            }
            //if (!CheckBox_TID.Checked)
            //{
            //    if ((ComboBox_EPC1.Items.Count != 0))
            //    {
            //        ComboBox_EPC1.SelectedIndex = 0;
            //        ComboBox_EPC2.SelectedIndex = 0;
            //        ComboBox_EPC3.SelectedIndex = 0;
            //        ComboBox_EPC4.SelectedIndex = 0;
            //        ComboBox_EPC5.SelectedIndex = 0;
            //        ComboBox_EPC6.SelectedIndex = 0;
            //    }
            //}

            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
            return sEPC;
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            byte WNum = 0;
            byte EPClength = 0;
            byte Writedatalen = 0;
            int WrittenDataNum = 0;
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            //if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            //{
            //    return;
            //}
            //if (checkBox1.Checked)
            //    MaskFlag = 1;
            //else
                MaskFlag = 0;//使能
                Maskadr = Convert.ToByte("00", 16);//掩模起始字节地址：
                MaskLen = Convert.ToByte("00", 16);//掩模字节数：
            //if (ComboBox_EPC2.Items.Count == 0)
            //    return;
            //if (ComboBox_EPC2.SelectedItem == null)
            //    return;
                str = epc;// ComboBox_EPC2.SelectedItem.ToString();
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(ENum * 2);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(str);
            //if (C_Reserve.Checked)
            //    Mem = 0;
            //if (C_EPC.Checked)
            //    Mem = 1;
            //if (C_TID.Checked)
            //    Mem = 2;
            //if (C_User.Checked)
                Mem = 3;//用户区
            //if (Edit_WordPtr.Text == "")
            //{
            //    MessageBox.Show("起始地址为空", "信息提示");
            //    return;
            //}
            //if (textBox1.Text == "")
            //{
            //    MessageBox.Show("读/块擦除长度", "信息提示");
            //    return;
            //}
                int a = Convert.ToInt32("00", 16);
            if (Convert.ToInt32("00", 16) + 4 > 120)//起始地址+擦除长度
                return;
            //if (Edit_AccessCode2.Text == "")
            //{
            //    return;
            //}
            WordPtr = Convert.ToByte("00", 16);//起始地址
            Num = Convert.ToByte("4");//擦除长度
            //if (Edit_AccessCode2.Text.Length != 8)
            //{
            //    return;
            //}
            fPassWord = HexStringToByteArray("00000000");//访问密码
            if (textBox1.Text == "")
                return;
            s2 = textBox1.Text;
            if (s2.Length % 4 != 0)
            {
                MessageBox.Show("以字为单位输入.", "写");
                return;
            }
            WNum = Convert.ToByte(s2.Length / 4);
            byte[] Writedata = new byte[WNum * 2];
            Writedata = HexStringToByteArray(s2);
            Writedatalen = Convert.ToByte(WNum * 2);
            //if ((checkBox_pc.Checked) && (C_EPC.Checked))
            //{
            //    WordPtr = 1;
            //    Writedatalen = Convert.ToByte(Edit_WriteData.Text.Length / 2 + 2);
            //    Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            //}
            fCmdRet = StaticClassReaderB.WriteCard_G2(ref fComAdr, EPC, Mem, WordPtr, Writedatalen, Writedata, fPassWord, Maskadr, MaskLen, MaskFlag, WrittenDataNum, EPClength, ref ferrorcode, frmcomportindex);
            //AddCmdLog("Write data", "写", fCmdRet);
            if (fCmdRet == 0)
            {
                lbl_status.Text = "写入成功";
                //StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "‘写EPC”指令返回=0x00" +
                //  "(写EPC成功)";
            }  
        }
    }
}
