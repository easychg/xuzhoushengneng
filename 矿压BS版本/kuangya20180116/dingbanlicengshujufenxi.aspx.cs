using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Text;
using System.Configuration;

public partial class dingbanlicengshujufenxi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                HttpCookie userid = Request.Cookies[Cookie.ComplanyId];
                ViewState["userid"] = userid.Value;
                ViewState["search"] = " 1=0";
                BindData();
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    private void BindData()
    {
        string sql = "SELECT areaName FROM AreaInfo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_cequ.Items.Clear();
            ddl_cequ.DataSource = ds.Tables[0];
            ddl_cequ.DataTextField = "areaname";
            ddl_cequ.DataValueField = "areaname";
            ddl_cequ.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_cequ.Items.Insert(0, item1);
        }
    }

    private void BindInfo()
    {
        
    }
    protected void search_Click(object sender, EventArgs e)
    {

        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string cequ = ddl_cequ.SelectedValue;
        string roadway = ddl_roadway.SelectedValue;
        string anzhuangweizhi = ddl_anzhuangweizhi.SelectedValue;
        DrawingCurve MyDc = new DrawingCurve();
        DataTable a = new DataTable();
        DataTable b = new DataTable();
        string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        Bitmap img = DrawImage.DrawingCurve.DrawingImg20(cequ, roadway, "4", anzhuangweizhi, time1, time2, a, b);
        string str = Server.MapPath("./xiazai/");
        img.Save(str + "dingbanlicengshujufenxi.jpg");
        panel1.BackImageUrl = "xiazai/dingbanlicengshujufenxi.jpg";
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < a.Rows.Count; i++)
        {
            if (i == 0)
            {
                sb.Append("{");
            }
            else
            {
                sb.Append(",{");
            }
            sb.Append(string.Format("\"{0}\":\"{1}\"", "content", a.Rows[i]["content"].ToString()));
            sb.Append("}");
        }
        sb.Append("]");
        txtjson.Text = sb.ToString();
    }
    protected void lbtn_export_Click(object sender, EventArgs e)
    {
        string url = panel1.BackImageUrl;
        if (url == "")
        {
            SystemTool.AlertShow(this, "图片为空，请先查询");
            return;
        }
        string ReportFileName = Server.MapPath("~/" + url);
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=顶板离层数据分析.jpg");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
    protected void ddl_cequ_changed(object sender, EventArgs e)
    {
        string sql = "SELECT roadName FROM RoadInfo where areaName='" + ddl_cequ.SelectedValue + "'";//工作面
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_roadway.Items.Clear();
            ddl_roadway.DataSource = ds.Tables[0];
            ddl_roadway.DataTextField = "roadName";
            ddl_roadway.DataValueField = "roadName";
            ddl_roadway.DataBind();
            sql = "SELECT Location FROM DisSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and roadwayName='" + ds.Tables[0].Rows[0]["roadName"].ToString() + "' order by Location";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_anzhuangweizhi.Items.Clear();
                ddl_anzhuangweizhi.DataSource = ds.Tables[0];
                ddl_anzhuangweizhi.DataTextField = "Location";
                ddl_anzhuangweizhi.DataValueField = "Location";
                ddl_anzhuangweizhi.DataBind();
            }
            else
            {
                ddl_anzhuangweizhi.Items.Clear();
                ListItem item1 = new ListItem("--请选择--", "0");
                ddl_anzhuangweizhi.Items.Insert(0, item1);
            }
        }
        else
        {
            ddl_roadway.Items.Clear();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_roadway.Items.Insert(0, item1);
            ddl_anzhuangweizhi.Items.Clear();
            ddl_anzhuangweizhi.Items.Insert(0, item1);
        }
        //SystemTool.JavascriptShow(this, "changclass5()");
    }
    protected void ddl_roadway_changed(object sender, EventArgs e)
    {
        string sql = "SELECT Location FROM DisSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and roadwayName='" + ddl_roadway.SelectedValue + "' order by Location";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_anzhuangweizhi.Items.Clear();
            ddl_anzhuangweizhi.DataSource = ds.Tables[0];
            ddl_anzhuangweizhi.DataTextField = "Location";
            ddl_anzhuangweizhi.DataValueField = "Location";
            ddl_anzhuangweizhi.DataBind();
        }
        else
        {
            ddl_anzhuangweizhi.Items.Clear();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_anzhuangweizhi.Items.Insert(0, item1);
        }
    }

}