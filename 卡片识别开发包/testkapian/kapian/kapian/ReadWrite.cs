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
using System.Threading;

namespace kapian
{
    public partial class ReadWrite : Form
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
        private Thread th_read;
        private Thread th_write;
        public ReadWrite()
        {
            InitializeComponent();
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            int port = 0;
            int openresult, i;
            openresult = 30;
            string temp;
            Cursor = Cursors.WaitCursor;
         
            fComAdr = Convert.ToByte("FF", 16);//读写器地址
            try
            {
                if (true)//Auto自动选择COM口
                {
                    fBaud = Convert.ToByte(3);//9600bps19200bps38400bps57600bps115200bps
                    if (fBaud > 2)
                        fBaud = Convert.ToByte(fBaud + 2);
                    openresult = StaticClassReaderB.AutoOpenComPort(ref port, ref fComAdr, fBaud, ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    if (openresult == 0)
                    {
                        ComOpen = true;
                        if ((fCmdRet == 0x35) | (fCmdRet == 0x30))
                        {
                            MessageBox.Show("串口通讯错误", "信息提示");
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                            ComOpen = false;
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
                com = Convert.ToString(fOpenComIndex);
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
            if ((com != "") & (fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30) & (fCmdRet == 0))
            {
                fComAdr = Convert.ToByte("00", 16);
                temp = "COM" + com;
                frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
            }
            //RefreshStatus();
            lbl_status.Text = startRun(0);//设置应答模式
            th_read = new Thread(startRead);
            th_read.Start(); // 启动
            //th_write = new Thread(startWrite);
            //startRead();//运行应答模式
        }
        /// <summary>
        ///  返回中文运行状态
        /// </summary>
        /// <param name="zhudong">1是主动0是应答</param>
        /// <returns></returns>
        private string startRun(int zhudong)
        {
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
            return fCmdRet > 0 ? "主动模式正在运行" : "应答模式正在运行";
        }
        private void startRead() {
            while (true)
            {
                this.Invoke((EventHandler)(delegate
                {
                    lbl_status.Text = "应答模式正在运行";
                }));
                epc = Inventory();
                if (epc != "") {
                    byte WordPtr, ENum;
                    byte Num = 0;
                    byte Mem = 0;
                    byte EPClength = 0;
                    string str;
                    byte[] CardData = new byte[320];
                    MaskFlag = 0;//使能
                    Maskadr = Convert.ToByte("00", 16);//掩模起始字节地址
                    MaskLen = Convert.ToByte("00", 16);//掩模字节数
                    str = epc;// ComboBox_EPC2.SelectedItem.ToString();
                    ENum = Convert.ToByte(str.Length / 4);
                    EPClength = Convert.ToByte(str.Length / 2);
                    byte[] EPC = new byte[ENum * 2];
                    EPC = HexStringToByteArray(str);
                    Mem = 3;
                    WordPtr = Convert.ToByte("00", 16);//起始地址:(字/16进制数)
                    Num = Convert.ToByte(4);//读/块擦除长度：(0-120/字/10进制数)
                    fPassWord = HexStringToByteArray("00000000");//访问密码
                    fCmdRet = StaticClassReaderB.ReadCard_G2(ref fComAdr, EPC, Mem, WordPtr, Num, fPassWord, Maskadr, MaskLen, MaskFlag, CardData, EPClength, ref ferrorcode, frmcomportindex);
                    if (fCmdRet == 0)
                    {
                        byte[] daw = new byte[Num * 2];
                        Array.Copy(CardData, daw, Num * 2);
                        string aaa = ByteArrayToHexString(daw);
                        this.Invoke((EventHandler)(delegate
                        {
                            textBox1.Text = aaa;
                        }));
                        th_read.Suspend();//挂起写线程
                        //if (th_write.ThreadState == ThreadState.Suspended) //停止读取进程，开启写线程
                        //{
                        //     // 停止，挂起线程
                        //    //th_write.Resume();
                        //}
                        
                    }
                    if (fAppClosed)
                        Close();
                }
            }
            
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
                }
            }

            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
            return sEPC;
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
        private bool startWrite(string strs) {
            //while (true) {
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
                MaskFlag = 0;//使能
                Maskadr = Convert.ToByte("00", 16);//掩模起始字节地址：
                MaskLen = Convert.ToByte("00", 16);//掩模字节数：
                str = epc;// ComboBox_EPC2.SelectedItem.ToString();
                ENum = Convert.ToByte(str.Length / 4);
                EPClength = Convert.ToByte(ENum * 2);
                byte[] EPC = new byte[ENum];
                EPC = HexStringToByteArray(str);
                Mem = 3;//用户区
                int a = Convert.ToInt32("00", 16);
                if (Convert.ToInt32("00", 16) + 4 > 120)
                {
                    //起始地址+擦除长度
                    return false;
                }

                WordPtr = Convert.ToByte("00", 16);//起始地址
                Num = Convert.ToByte("4");//擦除长度

                fPassWord = HexStringToByteArray("00000000");//访问密码
                s2 = "";
                //this.Invoke((EventHandler)(delegate
                //{
                //    s2 = textBox1.Text;
                //}));
                s2 = strs;
                
                WNum = Convert.ToByte(s2.Length / 4);
                byte[] Writedata = new byte[WNum * 2];
                Writedata = HexStringToByteArray(s2);
                Writedatalen = Convert.ToByte(WNum * 2);
                fCmdRet = StaticClassReaderB.WriteCard_G2(ref fComAdr, EPC, Mem, WordPtr, Writedatalen, Writedata, fPassWord, Maskadr, MaskLen, MaskFlag, WrittenDataNum, EPClength, ref ferrorcode, frmcomportindex);
                if (fCmdRet == 0)
                {
                    return true;
                    //this.Invoke((EventHandler)(delegate
                    //{
                    //    lbl_status.Text = "写卡成功";
                    //    textBox1.Text = "";
                    //}));
                }
                else
                {
                    return false;
                    //this.Invoke((EventHandler)(delegate
                    //{
                    //    lbl_status.Text = "写卡失败";
                    //    textBox1.Text = "";
                    //}));
                }

                //if (th_read.ThreadState == ThreadState.Suspended) // 如果被挂起了，就唤醒
                //{
                //    Thread.Sleep(5000); //让线程休息5秒钟
                //    th_read.Resume();//开启读线程
                //    th_write.Suspend();//挂起写线程
                //}
            //}
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length== 0)
            {
                MessageBox.Show("输入不能为空", "写");
                return;

            }
            if (textBox1.Text.Length % 4 != 0)
            {
                MessageBox.Show("以字为单位输入.", "写");
                return;
            }
            if (startWrite(textBox1.Text))
            {
                lbl_status.Text = "写卡成功";
                textBox1.Text = "";
                Application.DoEvents();
                Thread.Sleep(5000);
            }
            else {
                lbl_status.Text = "写卡失败";
                textBox1.Text = "";
            }
            if (th_read.ThreadState == ThreadState.Suspended) // 唤醒读线程
            {
                th_read.Resume();
            }
        }
    }
}
