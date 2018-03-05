using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class notify : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("success");
        string outTradeNo = Request.Form["outTradeNo"];
        string status = Request.Form["status"];
        SystemTool.SaveTxtInfo(status,"1.txt");
        if (status=="2")
        {
            SystemTool.SaveTxtInfo(status+"0", "1.txt");
            Response.Redirect("success.aspx");
        }
    }
}