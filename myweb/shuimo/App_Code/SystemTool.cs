using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net;



/// <summary>
/// SystemTool 的摘要说明
/// </summary>
public class SystemTool
{
    public SystemTool()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    public static string getbiaoti()
    {
        string biaoti = "";
        string sql = "select biaoti from web_info";
        biaoti = DB.ExecuteSqlValue(sql, null);

        return biaoti;
    }
    public static string getguanjianci()
    {
        string biaoti = "";
        string sql = "select guanjianci from web_info";
        biaoti = DB.ExecuteSqlValue(sql, null);

        return biaoti;
    }
    public static string getmiaoshu()
    {
        string biaoti = "";
        string sql = "select miaoshu from web_info";
        biaoti = DB.ExecuteSqlValue(sql, null);

        return biaoti;
    }

    public static string path = HttpContext.Current.Request.PhysicalApplicationPath + "log";
    public static void WriteLog(string type, string className, string content)
    {
        if (!Directory.Exists(path))//如果日志目录不存在就创建
        {
            Directory.CreateDirectory(path);
        }

        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");//获取当前系统时间
        string filename = path + "/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";//用日期对日志文件命名

        //创建或打开日志文件，向日志文件末尾追加记录
        StreamWriter mySw = File.AppendText(filename);

        //向日志文件写入内容
        string write_content = time + " " + type + " " + className + ": " + content;
        mySw.WriteLine(write_content);

        //关闭日志文件
        mySw.Close();
    }
    ///<summary> 
    ///产生指定长度的随机字符串 
    ///</summary> 
    ///<param name="Len">字符串长度</param> 
    ///<param name="type"></param> 
    ///<returns>返回给定长度的随机字符串</returns> 
    public static string RandNumber(int Len)
    {
        string s = "0,1,2,3,4,5,6,7,8,9";
        string[] Chars = s.Split(',');
        Random Rnd = new Random();
        int Start = 0, End = 9;
        s = "";
        for (int i = 0; i < Len; i++)
        {
            s += Chars[Rnd.Next(Start, End)];
            System.Threading.Thread.Sleep(5);//延时,避免重复 
        }
        return s;
    }
    public static bool IsSafeSqlString(string str)
    {
        return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
    }
    public static string jimi(string cl)
    {
        string pwd = "";
        MD5 md5 = MD5.Create();//实例化一个md5对像
        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] s = md5.ComputeHash(Encoding.Default.GetBytes(cl));
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        for (int i = 0; i < s.Length; i++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符

            pwd = pwd + s[i].ToString("x2");

        }
        return pwd;
    }

    /// <summary>
    /// 保存数据到txt中
    /// </summary>
    /// <param name="str">要保存的数据</param>
    /// <param name="filePath">虚拟路径</param>D:\log/aa.txt
    public static void SaveTxtInfo(string str, string filePath)
    {
        string fileName = HttpContext.Current.Server.MapPath(filePath);
        StreamWriter sw = null;
        FileStream oFileStream = null;
        // FileStream objFileStream = new FileStream("D:\\"+filePath+".txt", FileMode.OpenOrCreate, FileAccess.Write);
        try
        {
            if (!System.IO.File.Exists(fileName))
            {
                oFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            }
            else
            {
                oFileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            }
            //sw = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
            //sw.WriteLine(str);
            //sw.Close();
            //objFileStream.Close();
            sw = new StreamWriter(oFileStream, Encoding.Default);
            sw.Write(str);
            sw.WriteLine();
        }
        catch (IOException ee)
        {
            HttpContext.Current.Response.Write("<script>alert(" + ee.Message + ")</script>");
        }
        finally
        {
            if (sw != null)
            {
                sw.Close();
                oFileStream.Close();
            }
        }
    }

    public static string NoHtml(string Htmlstring)
    {
        if (Htmlstring == null)
        {
            return "";
        }
        else
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);

            //删除与数据库相关的词
            Htmlstring = Regex.Replace(Htmlstring, "select", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "insert", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete from", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "count''", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop table", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "truncate", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "asc", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "mid", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "char", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "exec master", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net localgroup administrators", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "and", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net user", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "or", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "net", "", RegexOptions.IgnoreCase);
            //Htmlstring = Regex.Replace(Htmlstring, "*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "-", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "delete", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "drop", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, "script", "", RegexOptions.IgnoreCase);

            //特殊的字符
            Htmlstring = Htmlstring.Replace("<", "");
            Htmlstring = Htmlstring.Replace(">", "");
            Htmlstring = Htmlstring.Replace("*", "");
            Htmlstring = Htmlstring.Replace("-", "");
            Htmlstring = Htmlstring.Replace("?", "");
            Htmlstring = Htmlstring.Replace("'", "''");
            Htmlstring = Htmlstring.Replace(",", "");
            Htmlstring = Htmlstring.Replace("/", "");
            Htmlstring = Htmlstring.Replace(";", "");
            Htmlstring = Htmlstring.Replace("*/", "");
            Htmlstring = Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
    }
    public static bool IsNum(string Str)
    {
        bool bl = false;
        string Rx = @"^[1-9]\d*$";
        if (Regex.IsMatch(Str, Rx))
        {
            bl = true;
        }
        else
        {
            bl = false;
        }
        return bl;
    }


    public static string CutString(string content, int num, int nub)
    {
        if (content.ToString().Length > num)
            return content.ToString().Substring(nub, num);
        else
            return content.ToString();
    }

    /// <summary>
    /// 获取客户端IP地址
    /// </summary>
    /// <returns>若失败则返回回送地址</returns>
    public static string GetIP()
    {
        //获取本机外网IP地址
        HttpRequest request = HttpContext.Current.Request;
        string result = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(result))
        {
            result = request.ServerVariables["REMOTE_ADDR"];
        }
        if (string.IsNullOrEmpty(result))
        {
            result = request.UserHostAddress;
        }
        if (string.IsNullOrEmpty(result))
        {
            result = "0.0.0.0";
        }
        string ip = result;
        return ip;
    }

    /// <summary>
    /// 检查IP地址格式
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public static bool IsIP(string ip)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
    }
    ///
    /// 根据IP获取省市
    ///
    public static string GetAddressByIp(string ip)
    {
        // string ip = TextBox1.Text.ToString().Trim(); //"115.193.217.249";
        string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;
        string res = GetDataByPost(PostUrl);//该条请求返回的数据为：res=1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
        if (res == "-3")
        {
            return "";
        }
        string[] arr = getAreaInfoList(res);

        return arr[0] + arr[1].ToString().Trim() + arr[2].ToString();
    }
    ///
    /// Post请求数据
    ///
    ///
    ///
    public static string GetDataByPost(string url)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        string s = "anything";
        byte[] requestBytes = System.Text.Encoding.ASCII.GetBytes(s);
        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = requestBytes.Length;
        Stream requestStream = req.GetRequestStream();
        requestStream.Write(requestBytes, 0, requestBytes.Length);
        requestStream.Close();
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
        StreamReader sr = new StreamReader(res.GetResponseStream(), System.Text.Encoding.Default);
        string backstr = sr.ReadToEnd();
        sr.Close();
        res.Close();
        return backstr;
    }


    public static string sendtele(string shouji, string neirong)
    {
          //string yanzhengma = SystemTool.RandNumber(6);
        //  Session["codes"] = yanzhengma;
         //string neirong = "您本次操作的验证码是:" + yanzhengma + "   请勿将验证码告知他人并确认是您本人操作！【HelpChina】";
        // string neirong = "尊敬的[" + nicheng + "用户]，您的帐号[" + zhanghao + "]已经创建，密码是[" + pass + "]，请登录平台完善资料！【HelpChina】";
        Encoding myEncoding = Encoding.GetEncoding("UTF-8");
        //string url = "http://sh2.ipyy.com/sms.aspx?action=send&userid=  '' & account = DhansCw & password = Dahanshoucwd & mobile = " + shouji + " & content = " + neirong;
        //string url = "http://sh2.ipyy.com/sms.aspx?action=send&userid= '' & account = maotong & password = maotong & mobile = " + shouji + " & content = " + neirong;
        string url = "http://sh2.ipyy.com/sms.aspx?action=send&userid=%27%27 &account=xzhmzx&password=hmzx125&mobile=" + shouji + "&content=" + neirong;


        byte[] postBytes = Encoding.ASCII.GetBytes(url);
        HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
        myRequest.Method = "POST";
        myRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
        myRequest.ContentLength = postBytes.Length;
        try
        {
            using (Stream reqStream = myRequest.GetRequestStream())
            {
                reqStream.Write(postBytes, 0, postBytes.Length);
            }
            HttpWebResponse response = (HttpWebResponse)myRequest.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding("utf-8")))
            {
                return reader.ReadToEnd();
            }
        }
        catch
        {
            return "aa";
        }
    }

    ///
    /// 处理所要的数据
    ///
    ///
    ///
    public static string[] getAreaInfoList(string ipData)
    {
        //1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
        string[] areaArr = new string[10];
        string[] newAreaArr = new string[3];
        try
        {
            //取所要的数据，这里只取省市
            areaArr = ipData.Split('\t');
            newAreaArr[0] = areaArr[3];//国
            newAreaArr[1] = areaArr[4];//省
            newAreaArr[2] = areaArr[5];//市
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
        return newAreaArr;
    }
    public static void AlertShow(Page page, string text)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "aa", "<script>alert('" + text + "')</script>");
    }

    public static void AlertShow_Refresh(Page page, string text, string url)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "aa", "<script>alert('" + text + "');document.location.href = document.location.href='" + url + "'</script>");
    }

    public static void AlertShow_Refreshb(Page page, string text, string url)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "aa", "<script>alert('" + text + "');location.href = '" + url + "'</script>");
    }

    public static void AlertShow_Refresh1(Page page, string text, string url)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "aa", "<script>alert('" + text + "');window.parent.location.href = window.parent.location.href='" + url + "'</script>");
    }

    public static void AlertShow(Page page, string funName, string text)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), funName, "<script>alert('" + text + "')</script>");
    }
    public static void JavascriptShow(Page page, string text)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "JavascriptShow", "<script>" + text + "</script>");
    }
    public static void JavascriptShow(Page page, string funName, string text)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), funName, "<script>" + text + "</script>");
    }

    public static string ChangeHtml(string html)
    {
        return html = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "").Replace("&nbsp;", "");
    }

    public static string ChangeHtml(string html, int num)
    {
        html = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", "").Replace("&nbsp;", "");
        return html.Length > num ? html.Substring(0, num).ToString() + "..." : html;
    }

    /************************************拼音简码************************************/
    public static string GetPYCode(string mystr)
    {
        const string c_CC = "驁,簿,錯,鵽,樲,鰒,腂,夻,攈,穒,鱳,旀,桛,漚,曝,囕,鶸,蜶,籜,鶩,鑂,韻,咗";
        const string c_PY = "A,B,C,D,E,F,G,H,J,K,L,M,N,O,P,Q,R,S,T,W,X,Y,Z";
        int lngChar;
        string strChar;
        string[] arrCC;
        string[] arrPY;
        string str = "";
        arrCC = c_CC.Split(',');
        arrPY = c_PY.Split(',');
        for (int i = 0; i <= mystr.Length - 1; i++)
        {
            strChar = mystr.Substring(i, 1).ToString();
            lngChar = (int)strChar[0];
            if (lngChar >= 19968 && lngChar <= 40869)
            {
                //只处理中文字符
                for (int j = 0; j <= 22; j++)
                {
                    if (strChar.CompareTo(arrCC[j]) <= 0)
                    {
                        strChar = arrPY[j];
                        break;
                    }
                }
            }
            str = str + strChar;
        }
        return str;
    }
    public static string add(string content)
    {
        if (content != "")
        {
            if (!content.Contains("http"))
            {
                // content = "http://1.datongguang.com/" + content;
                content = "http://192.168.1.81:8089/" + content;
            }
        }
        else
        {
            content = "http://192.168.1.19:81/images/nopic1.jpg";
        }
        return content.ToString();
    }
    /// 
    /// 检查指定的文本是否匹配验证码要判断的文本是否匹配 
    /// 
    public static bool CheckCode(string text)
    {
        string txt = System.Web.HttpContext.Current.Session["checkcode"] as string;
        return text == txt;
    }
    /// <summary>
    /// 不提示信息跳出框架返回
    /// </summary>
    /// <param name="page"></param>
    /// <param name="url"></param>
    public static void AlertShow_Refresh2(Page page, string url)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "aa", "<script>window.parent.location.href = window.parent.location.href='" + url + "'</script>");
    }
    //绑定下拉框

    //绑定下拉框
    //统一规范使用 "--请选择--"
    public static void bindDropDownList(DropDownList dropDownlist, DataTable dt, string value, string text, string add)
    {
        dropDownlist.DataSource = dt;
        dropDownlist.DataValueField = value;
        dropDownlist.DataTextField = text;
        dropDownlist.DataBind();
        if (add != "")
        {
            dropDownlist.Items.Insert(0, new ListItem(add, "--"));
        }
    }
    public static void bindDropDownList_0(DropDownList dropDownlist, DataTable dt, string value, string text, string add)
    {
        dropDownlist.DataSource = dt;
        dropDownlist.DataValueField = value;
        dropDownlist.DataTextField = text;
        dropDownlist.DataBind();
        if (add != "")
        {
            dropDownlist.Items.Insert(0, new ListItem(add, "0"));
        }
    }
    public static void bindSelect(System.Web.UI.HtmlControls.HtmlSelect select, DataTable dt, string value, string text, string add)
    {
        select.DataSource = dt;
        select.DataValueField = value;
        select.DataTextField = text;
        select.DataBind();
        if (add != "")
        {
            select.Items.Insert(0, new ListItem(add, "--"));
        }
    }
    //统一规范使用 "--请选择--"

    ////public static void bindDropDownList(DropDownList dropDownlist, DataTable dt, string value, string text)
    ////{
    ////    dropDownlist.DataSource = dt;
    ////    dropDownlist.DataValueField = value;
    ////    dropDownlist.DataTextField = text;
    ////    dropDownlist.DataBind();
    ////    //dropDownlist.Items.Insert(0, new ListItem(add, "--0--"));
    ////}
    //public static void bindDropDownList_0(DropDownList dropDownlist, DataTable dt, string value, string text, string add)
    //{
    //    dropDownlist.DataSource = dt;
    //    dropDownlist.DataValueField = value;
    //    dropDownlist.DataTextField = text;
    //    dropDownlist.DataBind();
    //    if (add != "")
    //    {
    //        dropDownlist.Items.Insert(0, new ListItem(add, "0"));
    //    }
    //}
    //public static void bindDropDownList(DropDownList dropDownlist, DataTable dt, string value, string text, string add)
    //{
    //    dropDownlist.DataSource = dt;
    //    dropDownlist.DataValueField = value;
    //    dropDownlist.DataTextField = text;
    //    dropDownlist.DataBind();
    //    if (add != "")
    //    {
    //        dropDownlist.Items.Insert(0, new ListItem(add, "0"));
    //    }
    //}
    //public static void bindSelect(System.Web.UI.HtmlControls.HtmlSelect select, DataTable dt, string value, string text, string add)
    //{
    //    select.DataSource = dt;
    //    select.DataValueField = value;
    //    select.DataTextField = text;
    //    select.DataBind();
    //    if (add != "")
    //    {
    //        select.Items.Insert(0, new ListItem(add, "--"));
    //    }
    //}
    /// 把dataset数据转换成json的格式
    /// </summary>
    /// <param name="ds">dataset数据集</param>
    /// <returns>json格式的字符串</returns>
    public static string GetJsonByDataset(DataSet ds)
    {
        if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
        {
            //如果查询到的数据为空则返回标记ok:false
            return "{\"ok\":false}";
        }

        StringBuilder sb = new StringBuilder();
        // sb.Append("{\"ok\":true,");
        sb.Append("[");
        foreach (DataTable dt in ds.Tables)
        {
            //   sb.Append(string.Format("\"{0}\":[", dt.TableName));

            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("{");
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    sb.AppendFormat("\"{0}\":\"{1}\",", dr.Table.Columns[i].ColumnName.Replace("\"", "\\\"").Replace("\'", "\\\'"), ObjToStr(dr[i]).Replace("\"", "\\\"").Replace("\'", "\\\'")).Replace(Convert.ToString((char)13), "\\r\\n").Replace(Convert.ToString((char)10), "\\r\\n");
                }
                sb.Remove(sb.ToString().LastIndexOf(','), 1);

                if (dt.Rows.IndexOf(dr) == 0)
                {
                    sb.Append(",open:true");
                }
                sb.Append("},");
            }

            sb.Remove(sb.ToString().LastIndexOf(','), 1);
            sb.Append("],");
        }
        sb.Remove(sb.ToString().LastIndexOf(','), 1);
        // sb.Append("}");
        return sb.ToString();
    }
    /// <summary>
    /// 将object转换成为string
    /// </summary>
    /// <param name="ob">obj对象</param>
    /// <returns></returns>
    public static string ObjToStr(object ob)
    {
        if (ob == null)
        {
            return string.Empty;
        }
        else
            return ob.ToString();
    }

    #region
    /// <summary>
    /// 正则验证
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public static bool IsEmail(string email)
    {
        return Regex.IsMatch(email, "^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
    }
    public static bool IsPhone(string phone)
    {
        return Regex.IsMatch(phone, "^(\\d{3.4}-)\\d{7,8}$");//正确格式：xxx/xxxx-xxxxxxx/xxxxxxxx；
    }
    public static bool IsUserNameOrPassword(string str_)
    {
        return Regex.IsMatch(str_, "^[a-zA-Z]\\w{5,15}$");//正确格式："[A-Z][a-z]_[0-9]"组成,并且第一个字必须为字母6~16位；
    }
    public static bool IsIDCard(string idCard)
    {
        return Regex.IsMatch(idCard, "^\\d{15}|\\d{18}$");//验证身份证号（15位或18位数字）
    }
    public static bool IsAZaz09(string str)
    {
        return Regex.IsMatch(str, "^[A-Za-z0-9]+$");//只能输入由数字和26个英文字母组成的字符串
    }
    public static bool IsIntOrDecimal(string num)
    {
        return Regex.IsMatch(num, "^[0-9]+\\.{0,1}[0-9]{0,2}$");//整数或者小数
    }
    public static bool IsInt(string num)
    {
        return Regex.IsMatch(num, "^[0-9]*$");//整数
    }
    public static bool IsIntZ(string num)
    {
        return Regex.IsMatch(num, "^\\+?[1-9][0-9]*$");//只能输入非零的正整数
    }
    public static bool IsIntF(string num)
    {
        return Regex.IsMatch(num, "^\\-[1-9][0-9]*$");//只能输入非零的负整数
    }
    public static bool IsDecimal(string num)
    {
        return Regex.IsMatch(num, "^[0-9]+(.[0-9]{2})?$");//两位小数
    }
    public static bool IsZW(string zh)
    {
        return Regex.IsMatch(zh, "^[\u4e00-\u9fa5]{0,}$");//汉字
    }
    public static bool IsURL(string url)
    {
        return Regex.IsMatch(url, "^http://([\\w-]+\\.)+[\\w-]+(/[\\w-./?%&=]*)?$");//网址
    }
    #endregion

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="password"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DESEncryptMD5(string str, string key)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray;
        inputByteArray = Encoding.Default.GetBytes(str);
        des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
        des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
            ret.AppendFormat("{0:X2}", b);
        return ret.ToString();
    }
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="password"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DESDecryptMD5(string str, string key)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        int len;
        len = str.Length / 2;
        byte[] inputByteArray = new byte[len];
        int x, i;
        for (x = 0; x < len; x++)
        {
            i = Convert.ToInt32(str.Substring(x * 2, 2), 16);
            inputByteArray[x] = (byte)i;
        }
        des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
        des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Encoding.Default.GetString(ms.ToArray());
    }

}
