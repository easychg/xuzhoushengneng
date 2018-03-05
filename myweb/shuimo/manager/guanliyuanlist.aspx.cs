using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_guanliyuanlist : System.Web.UI.Page
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
        string sql = "select m.man_id,m.man_name,m.remark,m.state,r.roleName,CONVERT(varchar(100), m.createtime,20) createtime from manager_info m left join role_info r on r.id=m.roleId where  " + ViewState["search"] + " order by createtime desc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
        for (int i=0; i< rptlist.Items.Count; i++) {
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
        string sql = "";
        if (cmd == "lbtshan")
        {
            sql = "delete from manager_info where man_id=" + userid;
        }
        if (cmd == "lbtn_open")
        {
            sql = "update manager_info set state='启用' where man_id=" + userid;
        }
        if (cmd == "lbtn_close")
        {
            sql = "update manager_info set state='禁止' where man_id=" + userid;
        }
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            SystemTool.AlertShow(this, "保存成功");
            BindInfo();
        }
        else {
            SystemTool.AlertShow(this, "保存失败");
        }
    }
    protected void search_Click(object sender, EventArgs e)
    {

        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string qita = qitatiaojian.Value.ToString().Trim();;
        ViewState["search"] = " 1=1";
        if (time1 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), m.createtime,20),10)>='" + time1 + "'";
        }
        if (time2 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), m.createtime,20),10)<='" + time2 + "'";
        }
        if (qita != "")
        {
            ViewState["search"] += " and m.man_name like '%" + qita + "%'";
        }
        BindInfo();
    }
}