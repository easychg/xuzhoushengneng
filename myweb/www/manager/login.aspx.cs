using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string name = SystemTool.NoHtml( txt_name.Text);
        string pass = SystemTool.NoHtml( txt_psw.Text);
        if (name == "" || pass == "")
        {
            SystemTool.AlertShow(this, "账户和密码不能为空");
            return;
        }
        if (vCode.Value == "")
        {
            SystemTool.AlertShow(this, "验证码不能为空");
            return;
        }
        if (Session["Code"] == null) {
            SystemTool.AlertShow(this, "验证码已过期");
            return;
        }
        if (Session["Code"].ToString() != vCode.Value)
        {
            SystemTool.AlertShow(this, "验证码不正确");
            return;
        }
        //string sql = "select man_id from manager_info where man_name='" + name + "' and man_psw='" + SystemTool.jimi(pass) + "'";
        string sql = "select man_id,manager_info.state mstate,role_info.state rstate from manager_info,role_info where roleId=id and man_name='" + name + "' and man_psw='" + SystemTool.jimi(pass) + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count == 0) {
            SystemTool.AlertShow(this, "账号或密码错误");
            return;
        }
        if (ds.Tables[0].Rows[0]["mstate"] .ToString()== "禁止")
        {
            SystemTool.AlertShow(this, "该用户被禁用");
            return;
        }
        if (ds.Tables[0].Rows[0]["rstate"].ToString() == "禁止")
        {
            SystemTool.AlertShow(this, "该用户所在的组被禁用");
            return;
        }
        HttpCookie cookid = new HttpCookie(Cookie.ComplanyId, ds.Tables[0].Rows[0]["man_id"].ToString());
        cookid.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookid);
        Response.Redirect("index.aspx");

    }
}