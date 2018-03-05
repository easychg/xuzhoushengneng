using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;

namespace spider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> addresss = new List<string>();
            List<string> ages = new List<string>();
            List<string> names = new List<string>();
            StreamReader sr = new StreamReader("C:\\net\\spider\\spider\\bin\\Debug\\index.html"); 
            string str = sr.ReadToEnd(); 
            sr.Close();
            string strXml = str; //创建用户信息XML
            Regex address = new Regex("<div style=\"float:left\">(?<address>[a-zA-Z0-9\u4e00-\u9fa5&;]+)</div>", RegexOptions.IgnoreCase);
            Regex age = new Regex("<div style=\"float:right\">(?<age>[a-zA-Z0-9\u4e00-\u9fa5\\s]+)</div>", RegexOptions.IgnoreCase);
                              //string aa="<img alt=\"苏苏\" id=\"img_0\" class=\"mmhead\" src=\"./电话聊天_电话交友_美女电话陪聊_电话聊天热线_声讯电话_files/1493205102781_55897_largeHead.jpg\">";
            Regex userRegex = new Regex("<img alt=\"(?<name>[a-zA-Z0-9\u4e00-\u9fa5.]+)\" id=\"img_[a-zA-Z0-9.]*\" class=\"mmhead\" src=\"./电话聊天_电话交友_美女电话陪聊_电话聊天热线_声讯电话_files/(?<image>[a-zA-Z0-9._]+).jpg\">", RegexOptions.IgnoreCase);
            MatchCollection userMatchColl = userRegex.Matches(strXml);
            foreach (Match matchItem in userMatchColl)
            {
                string userName = matchItem.Groups["name"].Value; //获取用户名
                string time = matchItem.Groups["image"].Value; //获取入职时间，并转换日期格式
                string strFormat = String.Format("\"name\":\"{0}\",\"image\":\"{1}\",", userName, "images/"+time+".jpg");
                //richTextBox1.Text += strFormat;
                names.Add(strFormat);
            }
            MatchCollection userMatchColla = address.Matches(strXml);
            foreach (Match matchItem in userMatchColla)
            {
                string email = matchItem.Groups["address"].Value; ; //获取邮箱地址，并检测邮箱格式
                string strFormat = String.Format("\"address\":\"{0}\"", email);
                //richTextBox1.Text += strFormat;
                addresss.Add(strFormat);
            }
            MatchCollection userMatchCollg = age.Matches(strXml);
            foreach (Match matchItem in userMatchCollg)
            {
                
                string email = matchItem.Groups["age"].Value; ; //获取邮箱地址，并检测邮箱格式
                string strFormat = String.Format("\"age\":\"{0}\",", email);
                //richTextBox1.Text += strFormat;
                ages.Add(strFormat);
            }
            string text = "";
            for (int i = 0; i < names.Count; i++) {
                text += "{"+names[i]+ages[i]+addresss[i]+"},\t\n" ;
            }
            int a = names.Count;
            int b = ages.Count;
            int c = addresss.Count;
            richTextBox1.Text = text;
            //string html = SpiderHelper.GetPageHtml(textBox1.Text);//httptest.GetHtml(textBox1.Text);
            //MatchCollection matches = Regex.Matches(html, "<a[a-zA-Z0-9.]*href=\"(?<url>[a-zA-Z0-9\\-.\\s:_=/]+)\"[a-zA-Z0-9\\-\".\\s:_=/#]*>(?<code>[a-zA-Z0-9\\-\".#]+)</a>");
            //for (int i = 0; i < matches.Count; i++) {
            //    richTextBox1.Text += matches[i].Groups["code"].Value + "    " + matches[i].Groups["url"].Value + "\n\t";
            //    for (int j=0; j < matches[i].Groups.Count; j++) {
            //        //richTextBox1.Text += matches[i].Groups[j].Value + "00";
                    
            //    }
            //}
            //foreach (Match item in matches)
            //{
            //    richTextBox1.Text+=item.ToString();
            //}  
            //richTextBox1.Text = html;
            //pictureBox1.BackgroundImage = Image.FromStream(SpiderHelper.getImageStream("http://pic.58pic.com/58pic/15/14/14/18e58PICMwt_1024.jpg"));

        }
    }
}