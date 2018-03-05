using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace spider
{
    public class SpiderHelper
    {
        private static CookieContainer container = null; //存储验证码cookie
        public static string PostPageHtml(string url, string uName, string passwd, string vaildate)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
                request.AllowAutoRedirect = true;
                request.CookieContainer = container;//获取验证码时候获取到的cookie会附加在这个容器里面
                request.KeepAlive = true;//建立持久性连接
                //整数据
                string postData = string.Format("userName={0}&passwd={1}&validateCode={2}&rememberMe=true", uName, passwd, vaildate);
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytepostData = encoding.GetBytes(postData);
                request.ContentLength = bytepostData.Length;
                //设置代理属性WebProxy-------------------------------------------------
                //WebProxy proxy = new WebProxy("111.13.7.120", 80);
                //在发起HTTP请求前将proxy赋值给HttpWebRequest的Proxy属性
                //request.Proxy = proxy;
                //发送数据  using结束代码段释放
                using (Stream requestStm = request.GetRequestStream())
                {
                    requestStm.Write(bytepostData, 0, bytepostData.Length);
                }

                //响应
                response = (HttpWebResponse)request.GetResponse();
                string text = string.Empty;
                using (Stream responseStm = response.GetResponseStream())
                {
                    StreamReader redStm = new StreamReader(responseStm, Encoding.UTF8);
                    text = redStm.ReadToEnd();
                }

                return text;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return msg;
            }

        }
        
        /// <summary>
        /// 获取页面html
        /// </summary>
        public static string GetPageHtml(string url)
        {
            string htmlCode;  
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)";
            request.Timeout = 30000;
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0";
            request.Headers.Add("Accept-Encoding", "gzip, deflate");  
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求

            if (response.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压  
            {
                using (System.IO.Stream streamReceive = response.GetResponseStream())
                {
                    using (var zipStream =
                        new System.IO.Compression.GZipStream(streamReceive, System.IO.Compression.CompressionMode.Decompress))
                    {
                        Encoding enc = GetEncoding(url);
                        using (StreamReader sr = new System.IO.StreamReader(zipStream, enc))
                        {
                            htmlCode = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream streamReceive = response.GetResponseStream())
                {
                    Encoding enc = GetEncoding(url);
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(streamReceive, enc))
                    {
                        htmlCode = sr.ReadToEnd();
                    }
                }
            }
            return htmlCode;
            //Stream responseStream = response.GetResponseStream();
            //StreamReader sr = new StreamReader(responseStream, Encoding.UTF8);
            ////返回结果网页（html）代码
            //string content = sr.ReadToEnd();
            //return content;
        }

        /// <summary>
        /// Http下载文件
        /// </summary>
        public static void HttpDownloadFile(string url)
        {
            int pos = url.LastIndexOf("/") + 1;
            string fileName = url.Substring(pos);
            string path = Application.StartupPath + "\\download";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePathName = path + "\\" + fileName;
            if (File.Exists(filePathName)) return;

            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0; Trident/4.0)";
            request.Proxy = null;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();

            //创建本地文件写入流
            Stream stream = new FileStream(filePathName, FileMode.Create);

            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
        }
        #region 获取图片
        //Image.FromStream(getCodeStream("http://pic.58pic.com/58pic/15/14/14/18e58PICMwt_1024.jpg"));
        public static Stream getImageStream(string codeUrl)
        {

            //验证码请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(codeUrl);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0.1) Gecko/20100101 Firefox/5.0.1";
            request.Accept = "image/webp,*/*;q=0.8";
            request.CookieContainer = new CookieContainer();//!Very Important.!!!
            container = request.CookieContainer;
            var c = request.CookieContainer.GetCookies(request.RequestUri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = container.GetCookies(request.RequestUri);

            Stream stream = response.GetResponseStream();
            return stream;
        }

        #endregion

        #region GetEncoding获取编码方式
        /// <summary>
        /// 获取编码方式
        /// </summary>
        /// <param name="strurl"></param>
        /// <returns></returns>
        public static Encoding GetEncoding(string strurl)
        {
            string urlToCrawl = strurl;
            //generate http request  
            if (urlToCrawl != null && urlToCrawl != "")
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(urlToCrawl);
                //use GET method to get url's html  
                req.Method = "GET";
                req.Accept = "*/*";
                req.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                req.ContentType = "text/xml";
                //use request to get response  
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Encoding enc;
                try
                {
                    if (resp.CharacterSet != "ISO-8859-1")
                        enc = Encoding.GetEncoding(resp.CharacterSet);
                    else
                        enc = Encoding.UTF8;
                }
                catch
                {
                    // *** Invalid encoding passed  
                    enc = Encoding.UTF8;
                }
                string sHTML = string.Empty;
                using (StreamReader read = new StreamReader(resp.GetResponseStream(), enc))
                {
                    sHTML = read.ReadToEnd();
                    Match charSetMatch = Regex.Match(sHTML, "charset=(?<code>[a-zA-Z0-9\\-\"]+)", RegexOptions.IgnoreCase);
                    string sChartSet = charSetMatch.Groups["code"].Value.Replace("\"","");
                    //if it's not utf-8,we should redecode the html.  
                    if (!string.IsNullOrEmpty(sChartSet) && !sChartSet.Equals("utf-8", StringComparison.OrdinalIgnoreCase))
                    {
                        enc = Encoding.GetEncoding(sChartSet);
                    }
                }
                return enc;
            }
            return Encoding.Default;
        }
        #endregion
    }
}
