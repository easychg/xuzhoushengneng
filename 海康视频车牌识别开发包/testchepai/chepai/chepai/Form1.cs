using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace chepai
{
    public partial class Form1 : Form
    {
        private Int32 m_lUserID = -1;
        private Int32[] m_lAlarmHandle = new Int32[200];
        private Int32 iListenHandle = -1;
        private CHCNetSDK.MSGCallBack m_falarmData = null;
        public delegate void UpdateListBoxCallback(string strAlarmTime, string strDevIP, string strAlarmMsg);
        public Form1()
        {
            InitializeComponent();
            bool m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                MessageBox.Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                
                //保存SDK日志 To save the SDK log
                //CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
                for (int i = 0; i < 200; i++)
                {
                    m_lAlarmHandle[i] = -1;
                }
                //设置报警回调函数
                m_falarmData = new CHCNetSDK.MSGCallBack(MsgCallback);
                CHCNetSDK.NET_DVR_SetDVRMessageCallBack_V30(m_falarmData, IntPtr.Zero);

                //添加到布防
                string ip = txt_ip.Text;
                string port = txt_port.Text;
                string name = txt_name.Text;
                string password = txt_password.Text;
                setAlarm(ip, port, name, password);//登录，布防


                //启动监听
                byte[] strIP = new byte[16 * 16];
                uint dwValidNum = 0;
                Boolean bEnableBind = false;
                //获取本地PC网卡IP，启动监听
                if (CHCNetSDK.NET_DVR_GetLocalIP(strIP, ref dwValidNum, ref bEnableBind))
                {
                    if (dwValidNum > 0)
                    {
                        //取第一张网卡的IP地址为默认监听端口
                        textBoxListenIP.Text = System.Text.Encoding.UTF8.GetString(strIP, 0, 16);
                        string sLocalIP = textBoxListenIP.Text;
                        ushort wLocalPort = ushort.Parse(textBoxListenPort.Text);
                        iListenHandle = CHCNetSDK.NET_DVR_StartListen_V30(sLocalIP, wLocalPort, m_falarmData, IntPtr.Zero);
                        if (iListenHandle >= 0) { 
                            //启动成功
                        }
                    }
                }
            }
        }

        #region 设置报警回调，设置布防，返回车牌号到窗体页面
        public void MsgCallback(int lCommand, ref CHCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            //通过lCommand来判断接收到的报警信息类型，不同的lCommand对应不同的pAlarmInfo内容
            switch (lCommand)
            {
                case CHCNetSDK.COMM_ITS_PLATE_RESULT://交通抓拍结果上传(新报警信息类型)
                    ProcessCommAlarm_ITSPlate(ref pAlarmer, pAlarmInfo, dwBufLen, pUser);
                    break;
                default:
                    break;
            }
        }
        private void ProcessCommAlarm_ITSPlate(ref CHCNetSDK.NET_DVR_ALARMER pAlarmer, IntPtr pAlarmInfo, uint dwBufLen, IntPtr pUser)
        {
            CHCNetSDK.NET_ITS_PLATE_RESULT struITSPlateResult = new CHCNetSDK.NET_ITS_PLATE_RESULT();
            uint dwSize = (uint)Marshal.SizeOf(struITSPlateResult);

            struITSPlateResult = (CHCNetSDK.NET_ITS_PLATE_RESULT)Marshal.PtrToStructure(pAlarmInfo, typeof(CHCNetSDK.NET_ITS_PLATE_RESULT));

            //保存抓拍图片
            //for (int i = 0; i < struITSPlateResult.dwPicNum; i++)
            //{
            //    if (struITSPlateResult.struPicInfo[i].dwDataLen != 0)
            //    {
            //        string str = "D:/UserID_" + pAlarmer.lUserID + "_Pictype_" + struITSPlateResult.struPicInfo[i].byType + "_Num" + (i + 1) + ".jpg";
            //        FileStream fs = new FileStream(str, FileMode.Create);
            //        int iLen = (int)struITSPlateResult.struPicInfo[i].dwDataLen;
            //        byte[] by = new byte[iLen];
            //        Marshal.Copy(struITSPlateResult.struPicInfo[i].pBuffer, by, 0, iLen);
            //        fs.Write(by, 0, iLen);
            //        fs.Close();
            //    }
            //}
            //报警设备IP地址
            string strIP = pAlarmer.sDeviceIP;

            //抓拍时间：年月日时分秒
            string strTimeYear = string.Format("{0:D4}", struITSPlateResult.struSnapFirstPicTime.wYear) +
                string.Format("{0:D2}", struITSPlateResult.struSnapFirstPicTime.byMonth) +
                string.Format("{0:D2}", struITSPlateResult.struSnapFirstPicTime.byDay) + " "
                + string.Format("{0:D2}", struITSPlateResult.struSnapFirstPicTime.byHour) + ":"
                + string.Format("{0:D2}", struITSPlateResult.struSnapFirstPicTime.byMinute) + ":"
                + string.Format("{0:D2}", struITSPlateResult.struSnapFirstPicTime.bySecond) + ":"
                + string.Format("{0:D3}", struITSPlateResult.struSnapFirstPicTime.wMilliSec);

            //上传结果
            //string stringAlarm = "抓拍上传，" + "车牌：" + struITSPlateResult.struPlateInfo.sLicense + "，车辆序号：" + struITSPlateResult.struVehicleInfo.dwIndex;
            string stringAlarm = struITSPlateResult.struPlateInfo.sLicense;
            if (InvokeRequired)
            {
                object[] paras = new object[3];
                paras[0] = strTimeYear;//当前系统时间为：DateTime.Now.ToString();
                paras[1] = strIP;
                paras[2] = stringAlarm;
                lbl_chepaihao.BeginInvoke(new UpdateListBoxCallback(UpdateClientList), paras);
            }
            else
            {
                //创建该控件的主线程直接更新信息列表 
                UpdateClientList(DateTime.Now.ToString(), strIP, stringAlarm);
            }
        }
        public void UpdateClientList(string strAlarmTime, string strDevIP, string strAlarmMsg)
        {
            //列表新增报警信息
            string time = strAlarmTime;//抓取时间
            string ip = strDevIP;//抓取设备ip
            string num = strAlarmMsg;//车牌号
            lbl_chepaihao.Text = num;
            //listViewAlarmInfo.Items.Add(new ListViewItem(new string[] { strAlarmTime, strDevIP, strAlarmMsg }));
        }
        public void setAlarm(string ip,string port,string name,string password){
            if (ip==""||port==""||name==""||password=="")
            {
                return;
            }
            string DVRIPAddress = ip; //设备IP地址或者域名 Device IP
            Int16 DVRPortNumber = Int16.Parse(port);//设备服务端口号 Device Port
            string DVRUserName = name;//设备登录用户名 User name to login
            string DVRPassword = password;//设备登录密码 Password to login
            CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

            //登录设备 Login the device
            m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
            if (m_lUserID >= 0) { 
                //登录成功

                CHCNetSDK.NET_DVR_SETUPALARM_PARAM struAlarmParam = new CHCNetSDK.NET_DVR_SETUPALARM_PARAM();
                struAlarmParam.dwSize = (uint)Marshal.SizeOf(struAlarmParam);
                struAlarmParam.byLevel = 1; //0- 一级布防,1- 二级布防
                struAlarmParam.byAlarmInfoType = 1;//智能交通设备有效，新报警信息类型
                m_lAlarmHandle[m_lUserID] = CHCNetSDK.NET_DVR_SetupAlarmChan_V41(m_lUserID, ref struAlarmParam);//布防
                if (m_lAlarmHandle[m_lUserID]>=0){
                    //布防成功
                }
            }
        }
        #endregion
        private void brn_add_Click(object sender, EventArgs e)
        {

        }
    }
}
