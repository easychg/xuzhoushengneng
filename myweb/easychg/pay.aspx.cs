using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

public partial class pay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    private static string Md5(string str)
    {
        string cl = str;
        string pwd = "";
        MD5 md5 = MD5.Create();//实例化一个md5对像
        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        for (int i = 0; i < s.Length; i++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 

            pwd = pwd + s[i].ToString("X");

        }
        return pwd;
    }
    protected static string GetTimeStamp()
    {
        TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return Convert.ToInt64(ts.TotalSeconds).ToString();
    }
    protected void lbtn_pay_Click(object sender, EventArgs e)
    {
        Pay("2");
    }
    protected void lbtn_pay2_Click(object sender, EventArgs e)
    {
        Pay("1");
    }
    protected static void Pay(string type) {
        string timestamp = GetTimeStamp();
        string outTradeNo = timestamp;
        string tradeName = "0";
        string amount = "10";
        string appkey = "13ee4d9745714236a75b6004f7fed46f";
        string payType = type;
        string notifyUrl = "http://www.easychg.com/notify.aspx";
        string synNotifyUrl = "http://www.easychg.com/zhifuzhong.htm";
        string payuserid = "1";
        string channel = "baidu";
        string backparams = "1";
        string method = "trpay.trade.create.wap";
        string version = "1.0";
        string stringA = "amount=" + amount + "&appkey=" + appkey + "&backparams=" + backparams + "&channel=" + channel + "&method=" + method + "&notifyUrl=" + notifyUrl + "&outTradeNo=" + outTradeNo + "&payType=" + payType + "&payuserid=" + payuserid + "&synNotifyUrl=" + synNotifyUrl + "&timestamp=" + timestamp + "&tradeName=" + tradeName + "&version=" + version;
        string SignTemp = stringA + "&appSceret=4dd6aeebb98a490c9c77b533e2f9d38c";
        string sign = Md5(SignTemp).ToUpper();

        StringBuilder sbHtml = new StringBuilder();
        sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='http://pay.trsoft.xin/order/trpayGetWay' method='post'>");
        sbHtml.Append("<input type='hidden' name='outTradeNo' value='" + outTradeNo + "'/>");
        sbHtml.Append("<input type='hidden' name='tradeName' value='" + tradeName + "'/>");
        sbHtml.Append("<input type='hidden' name='amount' value='" + amount + "'/>");
        sbHtml.Append("<input type='hidden' name='appkey' value='" + appkey + "'/>");
        sbHtml.Append("<input type='hidden' name='payType' value='" + payType + "'/>");
        sbHtml.Append("<input type='hidden' name='notifyUrl' value='" + notifyUrl + "'/>");
        sbHtml.Append("<input type='hidden' name='synNotifyUrl' value='" + synNotifyUrl + "'/>");
        sbHtml.Append("<input type='hidden' name='payuserid' value='" + payuserid + "'/>");
        sbHtml.Append("<input type='hidden' name='channel' value='" + channel + "'/>");
        sbHtml.Append("<input type='hidden' name='backparams' value='" + backparams + "'/>");
        sbHtml.Append("<input type='hidden' name='method' value='" + method + "'/>");
        sbHtml.Append("<input type='hidden' name='version' value='" + version + "'/>");
        sbHtml.Append("<input type='hidden' name='timestamp' value='" + timestamp + "'/>");
        sbHtml.Append("<input type='hidden' name='sign' value='" + sign + "'/>");
        sbHtml.Append("<input type='submit' value='银联支付' style='display:none;'></form>");
        sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");
        HttpContext.Current.Response.Write(sbHtml.ToString());
        
    }
}