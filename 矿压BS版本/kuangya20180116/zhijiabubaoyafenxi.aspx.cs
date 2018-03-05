using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
using System.Configuration;
using System.Data;

public partial class zhijiabubaoyafenxi : System.Web.UI.Page
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
                BindData();

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
        sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ds.Tables[0].Rows[0]["areaName"].ToString() + "'";//工作面
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomian.Items.Clear();
            ddl_gongzuomian.DataSource = ds.Tables[0];
            ddl_gongzuomian.DataTextField = "workfaceName";
            ddl_gongzuomian.DataValueField = "workfaceName";
            ddl_gongzuomian.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);
        }

    }




    protected void ddl_cequ_changed(object sender, EventArgs e)
    {
        string sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ddl_cequ.SelectedValue + "'";//工作面
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomian.Items.Clear();
            ddl_gongzuomian.DataSource = ds.Tables[0];
            ddl_gongzuomian.DataTextField = "workfaceName";
            ddl_gongzuomian.DataValueField = "workfaceName";
            ddl_gongzuomian.DataBind();
            //sql = "select * from PressurePar where areaname='" + ddl_cequ.SelectedValue + "' and facename='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "'";
            //ds = DB.ExecuteSqlDataSet(sql, null);
            sql = "select BracketNo from PreSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "' order by bracketno";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddl_zhijiabianhao.Items.Clear();
                ddl_zhijiabianhao.DataSource = ds.Tables[0];
                ddl_zhijiabianhao.DataTextField = "BracketNo";
                ddl_zhijiabianhao.DataValueField = "BracketNo";
                ddl_zhijiabianhao.DataBind();
            }
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);

        }
        //SystemTool.JavascriptShow(this, "changclass5()");
    }
    protected void ddl_gongzuomian_changed(object sender, EventArgs e)
    {
        string cequ = ddl_cequ.SelectedValue;
        string gongzuomian = ddl_gongzuomian.SelectedValue;
        string sql = "select BracketNo from PreSenInfo where areaName='" + cequ + "' and FaceName='" + gongzuomian + "' order by bracketno";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_zhijiabianhao.Items.Clear();
            ddl_zhijiabianhao.DataSource = ds.Tables[0];
            ddl_zhijiabianhao.DataTextField = "BracketNo";
            ddl_zhijiabianhao.DataValueField = "BracketNo";
            ddl_zhijiabianhao.DataBind();
        }

    }

    protected void search_Click(object sender, EventArgs e)
    {
        string dts = datemin.Value;
        string dte = datemax.Value;
        string cequ = ddl_cequ.SelectedValue;
        string gzm = ddl_gongzuomian.SelectedValue;
        string zjbh = ddl_zhijiabianhao.SelectedValue;
        DataTable dt_img = new DataTable();
        DataTable dt_export = new DataTable();
        string zhu = "4";//左柱和右柱
        string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        //Bitmap img = DrawImage.DrawingCurve.DrawingImg(cequ, gzm, zjbh, dts, dte);
        Bitmap img = DrawImage.DrawingCurve.DrawingImg8(cequ, gzm, zhu, zjbh, dts, dte, dt_img, dt_export);
        string str = Server.MapPath("./xiazai/");
        string str2 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "zhijiabubaoyafenxiquxian.jpg";
        img.Save(str + str2);
        panel1.BackImageUrl = "xiazai/" + str2;
        StringBuilder sb = new StringBuilder();
        sb.Append("[");
        for (int i = 0; i < dt_img.Rows.Count; i++)
        {
            if (i == 0)
            {
                sb.Append("{");
            }
            else
            {
                sb.Append(",{");
            }
            sb.Append(string.Format("\"{0}\":\"{1}\"", "content", dt_img.Rows[i]["content"].ToString()));
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
        Response.AddHeader("Content-Disposition", "attachment; filename=支架不保压分析曲线.jpg");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
}