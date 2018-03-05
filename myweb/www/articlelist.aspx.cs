using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class articlelist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        double cPage = 1;
        if (null != Request.QueryString["cpage"] && SystemTool.IsInt(Request.QueryString["cpage"].ToString()))
        {
            cPage = Convert.ToDouble(Request.QueryString["cpage"].ToString());
        }
        string id = "0";
        if (null != Request.QueryString["nid"] && SystemTool.IsInt(Request.QueryString["nid"].ToString()))
        {
            id = Request.QueryString["nid"].ToString();
        }
        string sql = @"with t as (
select ROW_NUMBER() OVER(ORDER BY id DESC) AS ROWNUM, * from article_info where nav_id=" + id + " ) select * from t where  rownum>"+(cPage*6-6)+" and rownum<="+(cPage*6);
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_main.DataSource = ds.Tables[0];
        rpt_main.DataBind();
    }
    protected string getPage() {
        double total = 0;
        double totalPage = 1;
        double pPage = 6;
        double cPage = 1;
        if (null != Request.QueryString["cpage"] && SystemTool.IsInt(Request.QueryString["cpage"].ToString()))
        {
            cPage = Convert.ToDouble(Request.QueryString["cpage"].ToString());
        }
        double prev = cPage - 1 <= 0 ? 1 : cPage - 1;
        string id = "0";
        if (null != Request.QueryString["nid"] && SystemTool.IsInt(Request.QueryString["nid"].ToString()))
        {
            id = Request.QueryString["nid"].ToString();
        }
        string sql = "select count(id) from article_info where nav_id=" + id;
        string result = DB.ExecuteSqlValue(sql, null);
        if (result != "" && result != "no")
        {
            total = Convert.ToInt32(result);
            totalPage = Math.Ceiling(total / pPage);
        }
        double next = (cPage + 1) >= totalPage ? totalPage : cPage + 1;
        return "页次：" + cPage + "/" + totalPage + " 每页" + pPage + " 总数" + total + "<a href='articlelist.aspx?nid=" + id + "'>首页</a><a href='articlelist.aspx?nid=" + id + "&cpage=" + prev + "'>上一页</a><a href='articlelist.aspx?nid=" + id + "&cpage=" + next + "'>下一页</a><a href='articlelist.aspx?nid=" + id + "&cpage=" + totalPage + "'>尾页</a>";
    }
    protected string getTile()
    {
        string sql = "select title from sys_info";
        string result = DB.ExecuteSqlValue(sql, null);
        if (result != "" && result != "no")
        {
            return result;
        }
        else
        {
            return "";
        }
    }
    protected string getLocation() {
        string id = "0";
        if (null != Request.QueryString["nid"]&&SystemTool.IsInt( Request.QueryString["nid"].ToString())) {
            id = Request.QueryString["nid"].ToString();
        }
        if (id=="7")
        {
            return "<a href='index.aspx' target='_blank'>网站首页</a>>><a href='#' target='_blank'>我的日记</a></span><b>个人日记</b>";
        }
        if (id == "8")
        {
            return "<a href='index.aspx' target='_blank'>网站首页</a>>><a href='#' target='_blank'>我的日记</a></span><b>学习笔记</b>";
        }
        if (id == "11")
        {
            return "<a href='index.aspx' target='_blank'>网站首页</a>>><a href='#' target='_blank'>技术文章</a></span><b></b>";
        }
        return "";
    }
}