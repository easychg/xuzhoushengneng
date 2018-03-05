using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text.RegularExpressions;

public partial class manager_mubanlist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                HttpCookie userid = Request.Cookies[Cookie.ComplanyId];
                ViewState["userid"] = userid.Value;
                ViewState["search"] = " 1=1";
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
        string sql = "select * from muban_info";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string userid = e.CommandArgument.ToString();
        TextBox name = e.Item.FindControl("txt_name") as TextBox;
        TextBox url = e.Item.FindControl("txt_url") as TextBox;
        if (cmd == "lbtshan")
        {
            string sql = "delete from muban_info where id=" + userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "删除成功");
                BindInfo();
            }
        }
        if (cmd == "lbtn_edit")
        {
            if (name.Text== "") { 
                SystemTool.AlertShow(this, "模板名称不能为空");
            }
            if (url.Text == "") {
                SystemTool.AlertShow(this, "模板地址不能为空");
            }
            string sql = "select id from muban_info where muban_name='" + name.Text + "'";
            string r = DB.ExecuteSqlValue(sql, null);
            if (r != "" && r != "no")
            {
                if (r != userid) {
                    SystemTool.AlertShow(this, "模板名称已存在");
                    return;
                }
            }
            sql = "update muban_info set muban_name='"+name.Text+"',muban_url='"+url.Text+"' where id="+userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow_Refresh(this, "保存成功", "mubanlist.aspx");
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow(this, "添加失败");
            }
        }
    }
    protected void add_modules(object sender, EventArgs e)
    {
        string name = mubanmingcheng.Value;
        if (name == "")
        {
            SystemTool.AlertShow(this, "模板名称不能为空");
            return;
        }
        string url = mubandizhi.Value;
        if (url == "")
        {
            SystemTool.AlertShow(this, "模块地址不能为空");
            return;
        }
        string sql = "select id from muban_info where muban_name='" + name + "'";
        string r = DB.ExecuteSqlValue(sql, null);
        if (r != "" && r != "no")
        {
            SystemTool.AlertShow(this, "模板名称已存在");
            return;
        }
        sql = "insert into muban_info (muban_name,muban_url) values('"+name+"','"+url+"')";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            SystemTool.AlertShow_Refresh(this, "保存成功", "mubanlist.aspx");
        }
        else {
            SystemTool.AlertShow(this, "添加失败");
        }
    }
}