using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreLibrary
{
    public class LEDHelper
    {
        private static void send(string strled, int i, string led_IP, int led_wight, int led_height)
        {
            int hLed = DLL.StartSend();
            DLL.SetTransMode(hLed, 1, 0, 2, 1);
            DLL.SetNetworkPara(hLed, 1, led_IP);
            DLL.AddControl(hLed, 1, 1);
            DLL.AddProgram(hLed, 1, 0);
            if (i == 1)
            {
                DLL.AddLnTxtString(hLed, 1, 1, 0, 0, led_wight, led_height, strled, "宋体", 12, 255, false, false, false, 32, 80, 0);
            }
            else if (i == 2)
            {
                DLL.AddQuitText(hLed, 1, 1, 0, 0, led_wight, led_height, 255, "宋体", 12, 0, 0, 0, strled);
            }
            DLL.SendControl(hLed, 1, IntPtr.Zero);  //发送
        }
    }
}
