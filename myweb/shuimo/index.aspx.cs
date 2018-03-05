using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        
        string sql = @"select top 6 a.id,a.nav_id,a.title,a.article_image,a.author,a.descr,a.detail,a.visited,a.addtime,a.isshow,a.istuijian,isnull(m.muban_url,'') muban_url,n.nav_name,n.isshow from article_info a
left join muban_info m on m.id=a.muban_id
left join nav_info n on n.id=a.nav_id
where a.isshow=1 and n.isshow=1 and a.detail!='album'
order by a.addtime desc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_main.DataSource = ds.Tables[0];
        rpt_main.DataBind();
        //推荐
        sql = @"select top 7 a.id,a.nav_id,a.title,a.article_image,a.author,a.descr,a.detail,a.visited,a.addtime,a.isshow,a.istuijian,isnull(m.muban_url,'') muban_url,n.nav_name,n.isshow from article_info a
left join muban_info m on m.id=a.muban_id
left join nav_info n on n.id=a.nav_id
where a.isshow=1 and istuijian=1 and n.isshow=1 and a.detail!='album'
order by a.addtime desc";
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_tuijian.DataSource = ds.Tables[0];
        rpt_tuijian.DataBind();
        //点击
        sql = @"select top 12 a.id,a.nav_id,a.title,a.article_image,a.author,a.descr,a.detail,a.visited,a.addtime,a.isshow,a.istuijian,isnull(m.muban_url,'') muban_url,n.nav_name,n.isshow from article_info a
left join muban_info m on m.id=a.muban_id
left join nav_info n on n.id=a.nav_id
where a.isshow=1  and n.isshow=1 and a.detail!='album'
order by visited desc";
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_dianji.DataSource = ds.Tables[0];
        rpt_dianji.DataBind();
    }
    protected string getTile()
    {
        string sql = "select title from sys_info";
        string result = DB.ExecuteSqlValue(sql, null);
        if (result != "" && result != "no") {
            return result;
        }else{
        return "";
        }
    }
}