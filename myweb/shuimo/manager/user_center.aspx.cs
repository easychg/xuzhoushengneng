using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_user_center : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {

                HttpCookie id = Request.Cookies[Cookie.ComplanyId];
                ViewState["userid"] = id.Value.ToString();
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }


    }

    private void BindInfo()
    {


        string sql = "SELECT man_name,roleName FROM manager_info LEFT JOIN role_info ON roleId=id WHERE man_id='" + ViewState["userid"] + "'";

        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblacc.Text = ds.Tables[0].Rows[0]["man_name"].ToString();
            lbljuese.InnerText = ds.Tables[0].Rows[0]["roleName"].ToString();
        }


    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (txtpass1.Text == "" && txtpass2.Text == "") {
            SystemTool.AlertShow(this, "密码不能为空!");
            return;
        }
        if (txtpass1.Text != "" || txtpass2.Text != "")
        {
            if (txtpass1.Text.Trim() != txtpass2.Text.Trim())
            {
                SystemTool.AlertShow(this, "两次密码不一致!");
                return;
            }
        }
        if (txtpass1.Text != "")
        {
            string sql = "update manager_info set man_psw='" + SystemTool.jimi(txtpass1.Text) + "'  where man_id=" + ViewState["userid"];
            int i = DB.ExecuteSql(sql, null);
            if (i > 0)
            {
                SystemTool.AlertShow(this, "操作成功!");
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败!");
            }
        }


    }
}