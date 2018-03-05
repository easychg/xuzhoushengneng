<%@ WebHandler Language="C#" Class="handlerHelper" %>

using System;
using System.Web;

public class handlerHelper : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private static Random random = new Random();
    public void ProcessRequest(HttpContext context)
    {
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string action = context.Request["action"];
        switch (action)
        {
            case "getVcode":
                context.Response.ContentType = "image/jpeg";
                System.Drawing.Image image = new System.Drawing.Bitmap(60, 30);
                //随即产生一个四位数
                int code = random.Next(1000, 10000);
                string codeString = code.ToString();
                //验证码放进Session中以便代码回传的时候验证用户输入的验证码是否正确
                context.Session["Code"] = codeString;
                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image))
                {
                    g.Clear(System.Drawing.Color.WhiteSmoke);
                    System.Drawing.StringFormat sf = new System.Drawing.StringFormat();
                    sf.Alignment = System.Drawing.StringAlignment.Center;
                    sf.LineAlignment = System.Drawing.StringAlignment.Center;
                    g.DrawString(codeString, new System.Drawing.Font("Arial", 14), System.Drawing.Brushes.Blue, new System.Drawing.RectangleF(0, 0, image.Width, image.Height), sf);
                    image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                break;
            case "ts1":
                context.Response.ContentType = "text/plain";
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"[{'name': 'Tokyo','data': [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]}, {'name': 'New York','data': [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]}]");
                //LitJson.JsonMapper.ToJson();
                context.Response.Write(sb.ToString().Replace("'", "\""));
                break;
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}