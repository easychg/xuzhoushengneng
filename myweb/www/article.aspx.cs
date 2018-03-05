using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class article : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        string id = "0";
        string nid = "0";
        if (null != Request.QueryString["id"] && SystemTool.IsInt(Request.QueryString["id"].ToString()))
        {
            id = Request.QueryString["id"].ToString();
        }
        string sql = "select * from article_info where id=" + id + " and isshow=1 and detail !='album'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            p_title.InnerHtml = ds.Tables[0].Rows[0]["title"].ToString();
            p_box.InnerHtml = "<span>发布时间：" + ds.Tables[0].Rows[0]["addtime"].ToString() + "</span><span>作者：" + ds.Tables[0].Rows[0]["author"].ToString() + "</span><span>点击：" + ds.Tables[0].Rows[0]["visited"].ToString() + "</span>";
            p_detail.InnerHtml = ds.Tables[0].Rows[0]["detail"].ToString();
            nid = ds.Tables[0].Rows[0]["nav_id"].ToString();
        }
        else {
            p_title.InnerHtml = "";
            p_box.InnerHtml = "";
            p_detail.InnerHtml = "参数有误！";
        }
        prev.InnerHtml = "上一篇：没有了";
        sql = "select max(id) from article_info where id<" + id + " and isshow=1 and nav_id="+nid;
        string previd = DB.ExecuteSqlValue(sql, null);
        if (previd != "" && previd != "no")
        {
            sql = "select a.title,m.muban_url from article_info a left join muban_info m on m.id=a.muban_id where a.id=" + previd;
            DataSet result = DB.ExecuteSqlDataSet(sql, null);
            if (result.Tables[0].Rows.Count>0) {
                prev.InnerHtml = "上一篇：<a href='" + result.Tables[0].Rows[0]["muban_url"].ToString() + "?id=" + previd + "'>" + result.Tables[0].Rows[0]["title"].ToString() + "</a>";
            }
        }
        next.InnerHtml = "下一篇：没有了";
        sql = "select min(id) from article_info where id>" + id + " and isshow=1 and nav_id=" + nid;
        string nextid = DB.ExecuteSqlValue(sql, null);
        if (nextid != "" && nextid != "no")
        {
            sql = "select a.title,m.muban_url from article_info a left join muban_info m on m.id=a.muban_id where a.id=" + nextid;
            DataSet result = DB.ExecuteSqlDataSet(sql, null);
            if (result.Tables[0].Rows.Count>0)
            {
                next.InnerHtml = "下一篇：<a href='" + result.Tables[0].Rows[0]["muban_url"].ToString() + "?id=" + nextid + "'>" + result.Tables[0].Rows[0]["title"].ToString() + "</a>";
            }
        }
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
    protected string getLocation()
    {
        string id = "0";
        if (null != Request.QueryString["id"] && SystemTool.IsInt(Request.QueryString["id"].ToString()))
        {
            id = DB.ExecuteSqlValue("select nav_id from article_info where id=" + Request.QueryString["id"].ToString() + " and isshow=1", null);
            if (id == "" || id == "no") {
                id = "-1";
            }
        }
        if (id == "7")
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