using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class manager_article_list : System.Web.UI.Page
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
        string sql = "select a.id,a.title,a.article_image,a.author,a.descr,a.visited,a.addtime,a.isshow,n.nav_name from article_info a left join nav_info n on n.id=a.nav_id where detail!='album' and " + ViewState["search"] + "order by a.addtime desc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
        for (int i = 0; i < rptlist.Items.Count; i++)
        {
            Label navid = rptlist.Items[i].FindControl("lbl_image") as Label;
            string[] images = navid.Text.Split('#');
            string html = "";
            for (int j = 0; j < images.Length - 1; j++)
            {
                html += "<a href='/" + images[j] + "' target='_blank'><img src='/" + images[j] + "' style='width:30px;height:30px;' /></a>  ";
            }
            navid.Text = html;
        }
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string userid = e.CommandArgument.ToString();
        if (cmd == "lbtshan")
        {
            string sql = "delete from article_info where id=" + userid;
            int r = DB.ExecuteSql(sql, null);
            if (r > 0)
            {
                SystemTool.AlertShow(this, "删除成功");
                BindInfo();
            }
            else
            {
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
            ViewState["search"] += " and left(CONVERT(varchar(100), a.addtime,20),10)>='" + time1 + "'";
        }
        if (time2 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), a.addtime,20),10)<='" + time2 + "'";
        }
        if (qita != "")
        {
            ViewState["search"] += " and a.title like '%" + qita + "%'";
        }
        BindInfo();
    }
}