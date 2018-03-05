using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class listpic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        
    }
    protected static string[] getData() {
        string sql = "select * from article_info where nav_id=5";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string article_image = ds.Tables[0].Rows[0]["article_image"].ToString();
            string[] images = article_image.Split('#');
            return images;
        }
        else
        {
            return null;
        }
    }
    protected string getImages() {
        double cPage = 1;
        double pPage = 12;
        Int32 fori = 0;
        Int32 forj = 0;
        if (Request.QueryString["page"] != null && SystemTool.IsInt(Request.QueryString["page"].ToString())) {
            cPage = Convert.ToDouble(Request.QueryString["page"].ToString());
            fori = Convert.ToInt32( cPage * 12 - 12);
        }
        string[] images = getData();
        double total = images.Length - 1;
        forj =Convert.ToInt32( Math.Min(total, cPage * pPage));
        string html = "";
        for (; fori < forj; fori++)
        {
            html += "<li><a href='/" + images[fori] + "' target='_blank'><img src='/" + images[fori] + "'><span>图片展示</span></a></li>";
        }
        return html == "" ? "暂无数据..." : html;
    }
    protected string getPage() {
        double cPage = 1;
        double pPage = 12;
        if (Request.QueryString["page"] != null && SystemTool.IsInt(Request.QueryString["page"].ToString()))
        {
            cPage = Convert.ToDouble(Request.QueryString["page"].ToString());
        }
        string[] images = getData();
        double totalPage = Math.Ceiling((images.Length - 1) / pPage);
        double prev = cPage - 1 > 0 ? cPage - 1 : cPage;
        double next = cPage + 1 > totalPage ? totalPage : cPage + 1;
        return "页次：" + cPage + "/" + totalPage + " 每页" + pPage + " 总数" + (images.Length - 1) + "<a href='listpic.aspx'>首页</a><a href='listpic.aspx?page=" + prev + "'>上一页</a><a href='listpic.aspx?page=" + next + "'>下一页</a><a href='listpic.aspx?page="+totalPage+"'>尾页</a>";
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
}