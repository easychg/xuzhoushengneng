using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_user_list : System.Web.UI.Page
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
				SystemTool.AlertShow_Refresh2(this, "login.aspx");
			}
		}
    }

	private void BindInfo()
	{
		string sql = "select * from manager_info where " + ViewState["search"] + " ";
		DataSet ds = DB.ExecuteSqlDataSet(sql, null);
		rptlist.DataSource = ds.Tables[0];
		rptlist.DataBind();
		for (int i = 0; i < rptlist.Items.Count; i++)
		{
			Label state = rptlist.Items[i].FindControl("userstate") as Label;
			LinkButton lbtnOpen = rptlist.Items[i].FindControl("lbtnOpen") as LinkButton;
			LinkButton lbtnClose = rptlist.Items[i].FindControl("lbtnClose") as LinkButton;
            lbtnClose.Visible = false;
            lbtnOpen.Visible = false;
            //if (state.Text.Equals("启用"))
            //{
            //    lbtnClose.Visible = false;
            //    lbtnOpen.Visible = true;
            //}
            //else
            //{
            //    lbtnClose.Visible = true;
            //    lbtnOpen.Visible = false;
            //}
		}
	}

	protected void search_OnClick(object sender, EventArgs e)
	{
		string time1 = datemin.Value;
		string time2 = datemax.Value;
		string weixin = userweixin.Value;
		string phone = userphone.Value;
		ViewState["search"] = " 1=1";
		if (time1.ToString().Trim() != "")
		{
			ViewState["search"] += " and addtime >='" + time1 + "' ";
		}
		if (time2.ToString().Trim() != "")
		{
			ViewState["search"] += " and addtime <='" + time2 + " 23:59:59'";
		}
		if (weixin != "")
		{
			ViewState["search"] += " and user_email like '%" + weixin + "%'";
		}
		if (phone != "")
		{
			ViewState["search"] += " and user_phone like '%" + phone + "%'";
		}
		BindInfo();
	}

	protected void lbtDelAll_OnClick(object sender, EventArgs e)
	{
		string ids = "";
		for (int i = 0; i < rptlist.Items.Count; i++)
		{
			CheckBox ckb = rptlist.Items[i].FindControl("ckb") as CheckBox;
			if (ckb.Checked == true)
			{
				ids += ckb.ToolTip + ",";
			}
		}
		if (ids.Length > 0)
		{
			ids = ids.Substring(0, ids.Length - 1);
		}
		else
		{
			SystemTool.AlertShow(this, "请选择需要删除的用户");
			return;
		}
		string sql = "delete from manager_info where man_id in(" + ids + ")";
		int result = DB.ExecuteSql(sql, null);
		if (result > 0)
		{
			BindInfo();
			SystemTool.AlertShow(this, "删除成功");
		}
		else
		{
			SystemTool.AlertShow(this, "删除失败");
		}
	}

	protected void rptlist_OnItemCommand(object source, RepeaterCommandEventArgs e)
	{
		string cmd = e.CommandName.ToString();
		string userid = e.CommandArgument.ToString();
		if (cmd == "lbtnDel")
		{
			string sql = "delete from manager_info where man_id=" + userid;
			int result = DB.ExecuteSql(sql, null);
			if (result > 0)
			{
				SystemTool.AlertShow(this, "操作成功");
				BindInfo();
			}
			else
			{
				SystemTool.AlertShow(this, "操作失败");
			}
		}
		if (cmd == "lbtnOpen")
		{
			string sql = "update manager_info set state='禁用' where man_id=" + userid;
			int result = DB.ExecuteSql(sql, null);
			if (result > 0)
			{
				SystemTool.AlertShow(this, "操作成功");
				BindInfo();
			}
			else
			{
				SystemTool.AlertShow(this, "操作失败");
			}
		}
		if (cmd == "lbtnClose")
		{
			string sql = "update manager_info set state='启用' where user_id=" + userid;
			int result = DB.ExecuteSql(sql, null);
			if (result > 0)
			{
				SystemTool.AlertShow(this, "操作成功");
				BindInfo();
			}
			else
			{
				SystemTool.AlertShow(this, "操作失败");
			}
		}
	}
}