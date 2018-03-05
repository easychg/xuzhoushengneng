using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_welcome : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
        {
            HttpCookie userid = Request.Cookies[Cookie.ComplanyId];
            ViewState["userid"] = userid.Value;
        }
        else
        {
            SystemTool.AlertShow_Refresh2(this, "Login.aspx");
        }
    }
}