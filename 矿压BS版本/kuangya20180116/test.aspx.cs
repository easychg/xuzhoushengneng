using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.Text;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DrawingCurve MyDc = new DrawingCurve();
        Bitmap img = new Bitmap(100, 100);
        //img = MyDc.Drawing();
        DataTable a = new DataTable();
        DataTable b = new DataTable();
        //img = MyDc.DrawingImg10("21102", "21102综采工作面", "3", "0", "2016-01-01 00:00:00", "2017-01-01 23:59:59");
        //img = MyDc.DrawingImgtest("21102", "21102综采工作面", "1", "083", "2016-06-04", "2016-06-04", a, b, 15, 10);
        //img = MyDc.DrawingImg21("3101测区", "3101工作面顺槽","锚索");
        //img = MyDc.DrawingImg24("3101测区", "3101工作面", "800", "2016-06-04", "2017-06-04", a);
        //img = MyDc.DrawingImg25("3101测区", "3101工作面");
        //img = MyDc.DrawingImg26("3101测区", "3101工作面","2016-09-02 00:00:00","2016-09-03 23:59:59");
        //img = MyDc.DrawingImg27("3101测区", "3101工作面", "035", "2016-04-02 00:00:00", "2016-09-03 23:59:59", a);
        img = MyDc.DrawColumn(a);
        string str = Server.MapPath("./xiazai/");
        img.Save(str + "ZhongCaiGongZuoZuLiShiShiZaiXianJianCe.jpg");   
        //img1.ImageUrl = "xiazai/ZhongCaiGongZuoZuLiShiShiZaiXianJianCe.jpg";
        panel1.BackImageUrl = "xiazai/ZhongCaiGongZuoZuLiShiShiZaiXianJianCe.jpg";
        //string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        //DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        //Bitmap img = DrawImage.DrawingCurve.DrawingImg3("3101测区", "3101工作面", "1", "20", "2016-08-29 00:00:01", "2017-08-29 23:00:00");
        //string str = Server.MapPath("./xiazai/");
        //img.Save(str + "MeiRiGongZuoZuLiFenBuQuXian.jpg");
        //img1.ImageUrl = "xiazai/MeiRiGongZuoZuLiFenBuQuXian.jpg";
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < a.Rows.Count; i++) {
            if (i == 0)
            {
                sb.Append("{");
            }
            else {
                sb.Append(",{");
            }
            sb.Append(string.Format("\"{0}\":\"{1}\"", "content", a.Rows[i]["content"].ToString()));
            sb.Append("}");
        }
        sb.Append("]");
        txtjson.Text = sb.ToString();
    }
}