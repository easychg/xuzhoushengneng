using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_guanliyuan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null) {
                init();
                if (null != Request.QueryString["manId"])
                {
                    BindInfo();
                }
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    private void BindInfo()
    {
        string sql = "select man_id,man_name,man_psw,manager_info.remark, manager_info.state state,roleName,roleId from manager_info,role_info where roleId=id and man_id=" + SystemTool.NoHtml(Request.QueryString["manId"]);
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0) {
            txtname.Text = ds.Tables[0].Rows[0]["man_name"].ToString();
            txtpassok.Text = ds.Tables[0].Rows[0]["man_psw"].ToString();
            txtbeizhu.Value = ds.Tables[0].Rows[0]["remark"].ToString();
            ddljiaose.SelectedValue = ds.Tables[0].Rows[0]["roleId"].ToString();
            ddlState.SelectedValue = ds.Tables[0].Rows[0]["state"].ToString();
            if (ddljiaose.SelectedValue == "0") {
                msg.Text = "该管理员所在的组（" + ds.Tables[0].Rows[0]["roleName"].ToString() + "）已被禁止，请启用该组后再修改";
            }
        }
        
        txtname.ReadOnly = true;
    }
    protected void init() {
        string sql = "select * from role_info where state='启用' order by paixu";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddljiaose, ds.Tables[0], "id", "roleName", "--请选择--");
    }
    
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (txtname.Text == "") {
            SystemTool.AlertShow(this, "管理员名称不能为空");
            return;
        }
        if (txtpassok.Text != txtpassok2.Text) {
            SystemTool.AlertShow(this, "两次输入密码不一致");
            return;
        }
        string sql = "";
        int result = 0;
        if (null != Request.QueryString["manId"])
        {
            //更新操作
            if (txtpassok.Text == "")
            {//密码不更新
                sql = "UPDATE manager_info set roleId='" + ddljiaose.SelectedValue + "',remark='" + SystemTool.NoHtml(txtbeizhu.Value) + "',state='" + ddlState.SelectedValue + "' where man_id=" + SystemTool.NoHtml(Request.QueryString["manId"]);
            }
            else {
                sql = "UPDATE manager_info set man_psw='" + SystemTool.jimi(SystemTool.NoHtml(txtpassok.Text)) + "',roleId='" + ddljiaose.SelectedValue + "',remark='" + SystemTool.NoHtml(txtbeizhu.Value) + "',state='" + ddlState.SelectedValue + "' where man_id=" + SystemTool.NoHtml(Request.QueryString["manId"]);
            }
            
            result = DB.ExecuteSql(sql, null);
        }
        else {
            sql = "select man_id from manager_info where man_name='" + SystemTool.NoHtml(txtname.Text) + "'";
            string r = DB.ExecuteSqlValue(sql, null);
            if (r != "" && r != "no")
            {
                SystemTool.AlertShow(this, "管理员名称已存在");
                return;
            }
            if (txtpassok.Text == "")
            {
                SystemTool.AlertShow(this, "管理员密码不能为空");
                return;
            }
            sql = "insert into manager_info (man_name,man_psw,roleId,remark,state) values('" + SystemTool.NoHtml(txtname.Text) + "','" + SystemTool.jimi(SystemTool.NoHtml(txtpassok.Text)) + "'," + ddljiaose.SelectedValue + ",'" + SystemTool.NoHtml(txtbeizhu.Value) + "','" + ddlState.SelectedValue + "')";
            result = DB.ExecuteSql(sql, null);
        }
        
        if (result > 0) {
            SystemTool.AlertShow_Refresh1(this, "保存成功","guanliyuanlist.aspx");
        } else {
            SystemTool.AlertShow(this, "保存失败");
            return;
        }
    }
}