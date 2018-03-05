using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_guanliyuanzulist : System.Web.UI.Page
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
        string sql = "select id,roleName,moduleId,remark,state,quanxianzhi,paixu,CONVERT(varchar(100), createtime,20) createtime from role_info where " + ViewState["search"] + "order by paixu asc,createtime desc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
        for (int i = 0; i < rptlist.Items.Count; i++)
        {
            Label lbl_zhuangtai = rptlist.Items[i].FindControl("lbl_zhuangtai") as Label;
            LinkButton lbtn_open = rptlist.Items[i].FindControl("lbtn_open") as LinkButton;
            LinkButton lbtn_close = rptlist.Items[i].FindControl("lbtn_close") as LinkButton;
            if (lbl_zhuangtai.Text.Equals("禁止"))
            {
                lbtn_close.Visible = false;
                lbtn_open.Visible = true;
            }
            else
            {
                lbtn_close.Visible = true;
                lbtn_open.Visible = false;
            }
        }
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string userid = e.CommandArgument.ToString();
        if (cmd == "lbtn_open")
        {
            string sql = "update role_info set state='启用' where id=" + userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "保存成功");
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
            }
        }
        if (cmd == "lbtn_close")
        {
            string sql = "update role_info set state='禁止' where id=" + userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "保存成功");
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
            }
        }
        if (cmd == "lbtshan")
        {
            string sql = "delete from role_info where id=" + userid;
            string sql2 = "delete from manager_info where roleId="+userid;
            string[] sqls = {sql,sql2 };
            bool r = DB.ExecuteSqlsTransaction2(sqls, null);
            if (r)
            {
                SystemTool.AlertShow(this, "删除成功");
                BindInfo();
            }
            else {
                SystemTool.AlertShow(this, "删除失败");
            }
        }
    }
    protected void search_Click(object sender, EventArgs e)
    {

        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string qita = qitatiaojian.Value.ToString().Trim();
        ViewState["search"] = " 1=1";
        if (time1 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), createtime,20),10)>='" + time1 + "'";
        }
        if (time2 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), createtime,20),10)<='" + time2 + "'";
        }
        if (qita != "")
        {
            ViewState["search"] += " and roleName like '%" + qita + "%'";
        }
        BindInfo();
    }
    protected void rptlist_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Repeater rpta = e.Item.FindControl("rpta") as Repeater;
            Label lb = e.Item.FindControl("lb_id") as Label;
            string sql = "select man_name from manager_info where roleId=" + lb.ToolTip;
            rpta.DataSource = DB.ExecuteSqlDataSet(sql, null).Tables[0];
            rpta.DataBind();
        }
        

    }
}