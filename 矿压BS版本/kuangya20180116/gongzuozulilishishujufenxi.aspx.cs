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

public partial class gongzuozulilishishujufenxi : System.Web.UI.Page
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
            sql = "select * from PressurePar where areaname='" + ddl_cequ.SelectedValue + "' and facename='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "'";
            ds = DB.ExecuteSqlDataSet(sql, null);
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);

        }
        //SystemTool.JavascriptShow(this, "changclass5()");
    }
    protected void ddl_yalileixing_changed(object sender, EventArgs e)
    {
        string sql = "SELECT BracketNo FROM PreSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + ddl_gongzuomian.SelectedValue + "' and Type = '" + ddl_yalileixing.SelectedValue + "' order by BracketNo asc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_zhijiabianhao.Items.Clear();
            ddl_zhijiabianhao.DataSource = ds.Tables[0];
            ddl_zhijiabianhao.DataTextField = "BracketNo";
            ddl_zhijiabianhao.DataValueField = "BracketNo";
            ddl_zhijiabianhao.DataBind();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_zhijiabianhao.Items.Insert(0, list);
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
        string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        Bitmap img = DrawImage.DrawingCurve.DrawingImg6(cequ, gzm, "4", zjbh, dts, dte, dt_img, dt_export);
        string str = Server.MapPath("./xiazai/");
        string str2 = DateTime.Now.ToString("yyyyMMddHHmmssfff")+"ZhongCaiGongZuoZuLiLiShiShuJuFenXiQuxian.jpg";
        img.Save(str + str2);
        panel1.BackImageUrl = "xiazai/"+str2;
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

        //进尺数据
        string sql = "select sum(hcjcnose) hcjcnose,sum(hcjcmid) hcjcmid,sum(hcjctail) hcjctail from Huicai where AreaName = '" + cequ + "' and FaceName = '" + gzm + "'";// and left(convert(nvarchar(50),datetime,20),10) <='" + t2 + "' ";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            head.Value = ds.Tables[0].Rows[0]["hcjcnose"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["hcjcnose"].ToString();
            body.Value = ds.Tables[0].Rows[0]["hcjcmid"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["hcjcmid"].ToString();
            foot.Value = ds.Tables[0].Rows[0]["hcjctail"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[0]["hcjctail"].ToString();
        }
        else {
            head.Value = "0.00";
            body.Value = "0.00";
            foot.Value = "0.00";
        }
    }
    protected void lbtn_export_Click(object sender, EventArgs e) {
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
        Response.AddHeader("Content-Disposition", "attachment; filename=综采工作阻力历史数据分析曲线.jpg");
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