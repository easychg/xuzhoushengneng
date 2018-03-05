using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Text;

public partial class huozhusuoliangshujufenxi : System.Web.UI.Page
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
        Bitmap img = DrawImage.DrawingCurve.DrawingImg27(cequ, roadway,anzhuangweizhi,time1, time2, a);
        string str = Server.MapPath("./xiazai/");
        img.Save(str + "huozhusuoliangshujufenxi.jpg");
        panel1.BackImageUrl = "xiazai/huozhusuoliangshujufenxi.jpg";
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
            SystemTool.AlertShow(this, "图片为空，请先刷新");
            return;
        }
        string ReportFileName = Server.MapPath("~/" + url);
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=活柱缩量数据分析.jpg");
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
        string sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ddl_cequ.SelectedValue + "'";//工作面
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_roadway.Items.Clear();
            ddl_roadway.DataSource = ds.Tables[0];
            ddl_roadway.DataTextField = "workfaceName";
            ddl_roadway.DataValueField = "workfaceName";
            ddl_roadway.DataBind();
            sql = "SELECT BracketNo FROM HuoZhuSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "' order by BracketNo";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_anzhuangweizhi.Items.Clear();
                ddl_anzhuangweizhi.DataSource = ds.Tables[0];
                ddl_anzhuangweizhi.DataTextField = "BracketNo";
                ddl_anzhuangweizhi.DataValueField = "BracketNo";
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
        string sql = "SELECT BracketNo FROM HuoZhuSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + ddl_roadway.SelectedValue + "' order by BracketNo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_anzhuangweizhi.Items.Clear();
            ddl_anzhuangweizhi.DataSource = ds.Tables[0];
            ddl_anzhuangweizhi.DataTextField = "BracketNo";
            ddl_anzhuangweizhi.DataValueField = "BracketNo";
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