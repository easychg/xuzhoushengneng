using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;

public partial class huozhusuoliangfenxi : System.Web.UI.Page
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
        }
    }

    private void BindInfo()
    {


    }
    protected void ddl_cequ_changed(object sender, EventArgs e)
    {
        txtsuoliangyujingzhi.Value = "0";
        txtsuoliangbaojingzhi.Value = "0";
        txtchuanganqizushu.Value = "0";
        string sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ddl_cequ.SelectedValue + "'";//工作面
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomian.Items.Clear();
            ddl_gongzuomian.DataSource = ds.Tables[0];
            ddl_gongzuomian.DataTextField = "workfaceName";
            ddl_gongzuomian.DataValueField = "workfaceName";
            ddl_gongzuomian.DataBind();
            string workfacename = ds.Tables[0].Rows[0]["workfaceName"].ToString();
            
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);

        }

        //SystemTool.JavascriptShow(this, "changclass5()");
    }

    protected void lbtn_refresh_Click(object sender, EventArgs e)
    {
        string dtss = datemin.Value + " " + datemax.Value;
        string dtes = datemin.Value + " " + datemax2.Value;
        string cequ = ddl_cequ.SelectedValue;
        string gongzuomain = ddl_gongzuomian.SelectedValue;
        string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        Bitmap img = DrawImage.DrawingCurve.DrawingImg26(cequ, gongzuomain, dtss, dtes);
        string str = Server.MapPath("./xiazai/");
        img.Save(str + "huozhusuoliangfenxi.jpg");
        img1.Src = "xiazai/huozhusuoliangfenxi.jpg";

        string sql = "select * from PressurePar where areaname='" + ddl_cequ.SelectedValue + "' and facename='" + gongzuomain + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sql = "select * from HuoZhuPar where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + gongzuomain + "'";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtsuoliangyujingzhi.Value = ds.Tables[0].Rows[0]["YujingValue"].ToString();
                txtsuoliangbaojingzhi.Value = ds.Tables[0].Rows[0]["AlarmValue"].ToString();
            }
        }
        sql = "select * from HuoZhuSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + gongzuomain + "'";
        ds = DB.ExecuteSqlDataSet(sql, null);
        txtchuanganqizushu.Value = ds.Tables[0].Rows.Count.ToString();
    }
    protected void lbtn_export_Click(object sender, EventArgs e)
    {
        string url = img1.Src;
        string ReportFileName = Server.MapPath("~/" + url);
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=活柱缩量分析.jpg");
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