<%@ WebHandler Language="C#" Class="GetImage" %>

using System;
using System.Web;

public class GetImage : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string action =  context.Request.QueryString["action"];
        string cequ = context.Request.QueryString["cequ"];
        string gongzuomian=context.Request.QueryString["gongzuomian"];
        string roadway = context.Request.QueryString["roadway"];
        string mgType = context.Request.QueryString["mgType"];
        string page = context.Request.QueryString["page"];
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        switch (action)
        {  
            case "image1":
                DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
                System.Drawing.Bitmap img = DrawImage.DrawingCurve.DrawingImg2(cequ, gongzuomian, page);
                string str = HttpContext.Current.Server.MapPath("./xiazai/");
                string str2 = "ZhongCaiGongZuoZuLiShiShiZaiXianJianCe" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                //img.Save(str + str2);
                img.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                break;
            case "image2":
                //顶板离层实时在线监测
                DrawImage.DrawingCurve dc2 = new DrawImage.DrawingCurve(strConn);
                System.Drawing.Bitmap img2 = DrawImage.DrawingCurve.DrawingImg13(cequ, roadway,page);
                img2.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                break;
            case "image3":
                //锚杆、锚索实时在线监测
                DrawImage.DrawingCurve dc3 = new DrawImage.DrawingCurve(strConn);
                System.Drawing.Bitmap img3 = DrawImage.DrawingCurve.DrawingImg21(cequ, roadway, mgType,page);
                img3.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                break;
            case "image4":
                //围岩应力实时在线监测
                //DrawingCurve MyDc = new DrawingCurve();
                //System.Drawing.Bitmap img4 = new System.Drawing.Bitmap(100, 100);
                //img4 = MyDc.DrawingImg23(cequ, roadway);
                //img4.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                
                DrawImage.DrawingCurve dc4 = new DrawImage.DrawingCurve(strConn);
                System.Drawing.Bitmap img4 = DrawImage.DrawingCurve.DrawingImg23(cequ, roadway,page);
                img4.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                break;
            case "image5":
                //活柱缩量在线监测
                DrawImage.DrawingCurve dc5 = new DrawImage.DrawingCurve(strConn);
                System.Drawing.Bitmap img5 = DrawImage.DrawingCurve.DrawingImg25(cequ, gongzuomian,page);
                img5.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}