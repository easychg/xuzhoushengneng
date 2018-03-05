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
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

public partial class chuzhangliyumozuli : System.Web.UI.Page
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
            sql = "select BracketNo from PreSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "'  order by bracketno";
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
    protected void ddl_gongzuomian_changed(object sender, EventArgs e) {
        string cequ = ddl_cequ.SelectedValue;
        string gongzuomian = ddl_gongzuomian.SelectedValue;
        string sql = "select BracketNo from PreSenInfo where areaName='"+cequ+"' and FaceName='"+gongzuomian+"' order by bracketno";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0) {
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
        string zhu = "3";
        if (zhengjia.Checked == true)
        {
            zhu = "3";
        }
        if (zuozhu.Checked == true)
        {
            zhu = "1";
        }
        if (youzhu.Checked == true)
        {
            zhu = "2";
        }
        string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        //Bitmap img = DrawImage.DrawingCurve.DrawingImg(cequ, gzm, zjbh, dts, dte);
        decimal CCL = Convert.ToDecimal( ConfigurationManager.ConnectionStrings["CCmin"].ToString());
        decimal ZLvalue=Convert.ToDecimal( ConfigurationManager.ConnectionStrings["ZLvalue"].ToString());
        Bitmap img = DrawImage.DrawingCurve.DrawingImg7(cequ, gzm, zhu, zjbh, dts, dte, dt_img, dt_export,CCL,ZLvalue);
        string str = Server.MapPath("./xiazai/");
        string str2 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "chuzhangliyumozulifenxiquxian.jpg";
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
        ViewState["excel"] = dt_export;
        //保存分析数据到PreReport
//        string sqlck = "select * from PreReport where areaName='"+cequ+"' and faceName='"+gzm+"' and bracketNo='"+zjbh+"' and reportDate='"+dts+"'";
//        DataSet dsck = DB.ExecuteSqlDataSet(sqlck, null);
//        if (dsck.Tables[0].Rows.Count <=0) {
//            decimal CCLmax = 0;// Convert.ToDecimal(dt_export.Compute("Min(chuchengli)", "true"));
//            decimal MZLmax = 0;// Convert.ToDecimal(dt_export.Compute("Max(mozuli)", "true"));
//            int intCCL = 0;
//            int intMZL = 0;
//            decimal decCCL = 0;
//            decimal decMZL = 0;
//            for (int i = 0; i < dt_export.Rows.Count; i++)
//            {
//                if (dt_export.Rows[i]["chuchengli"].ToString() != "")
//                {
//                    intCCL += 1;
//                    decCCL += Convert.ToDecimal(dt_export.Rows[i]["chuchengli"].ToString());
//                    CCLmax = CCLmax > Convert.ToDecimal(dt_export.Rows[i]["chuchengli"].ToString()) ? CCLmax : Convert.ToDecimal(dt_export.Rows[i]["chuchengli"].ToString());

//                }
//                if (dt_export.Rows[i]["mozuli"].ToString() != "")
//                {
//                    intMZL += 1;
//                    decMZL += Convert.ToDecimal(dt_export.Rows[i]["mozuli"].ToString());
//                    MZLmax = MZLmax > Convert.ToDecimal(dt_export.Rows[i]["mozuli"].ToString()) ? MZLmax : Convert.ToDecimal(dt_export.Rows[i]["mozuli"].ToString());
//                }
//            }
//            decimal CCLavg = intCCL == 0 ? 0 : decCCL / intCCL;
//            decimal MZLavg = intMZL == 0 ? 0 : decMZL / intMZL;
//            decimal GZZLmax = 0;
//            decimal GZZLmin = 0;
//            decimal GZZLavg = 0;
//            string SensorNo = "";
//            string distance = "";
//            dte += " 23:59:59";
//            string sqlgzzl = @"select max(pressure1) maxpre,min(pressure1) minpre,max(pressure2) maxpre2,min(pressure2) minpre2,avg(pressure1+pressure2)/2 avgpre3 from pressuredata 
//where areaName='" + cequ + "' and FaceName = '" + gzm + "' and SensorNo = (select SensorNo from PreSenInfo where areaName = '" + cequ + "' and FaceName='" + gzm + "' and BracketNo = '" + zjbh + "') and time between '" + dts + "' and '" + dte + "' and (Pressure1>=" + CCL + " or Pressure2>=" + CCL + ") ";
//            DataSet dsgzzl = DB.ExecuteSqlDataSet(sqlgzzl, null);
//            if (dsgzzl.Tables[0].Rows.Count > 0)
//            {
//                string m1 = dsgzzl.Tables[0].Rows[0]["maxpre"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["maxpre"].ToString();
//                decimal maxpre = Convert.ToDecimal(m1);
//                string m2 = dsgzzl.Tables[0].Rows[0]["maxpre2"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["maxpre2"].ToString();
//                decimal maxpre2 = Convert.ToDecimal(m2);
//                string m3 = dsgzzl.Tables[0].Rows[0]["avgpre3"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["avgpre3"].ToString();
//                decimal avgpre3 = Convert.ToDecimal(m3);
//                GZZLmax = maxpre > maxpre2 ? maxpre : maxpre2;
//                GZZLavg = avgpre3;
//                string m4 = dsgzzl.Tables[0].Rows[0]["minpre"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["minpre"].ToString();
//                decimal minpre = Convert.ToDecimal(m4);
//                string m5 = dsgzzl.Tables[0].Rows[0]["minpre2"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["minpre2"].ToString();
//                decimal minpre2 = Convert.ToDecimal(m5);
//                GZZLmin = minpre < minpre2 ? minpre : minpre2;

//            }
//            sqlgzzl = "select SensorNo,distance from PreSenInfo where areaName = '" + cequ + "' and FaceName='" + gzm + "' and BracketNo = '" + zjbh + "'";
//            dsgzzl = DB.ExecuteSqlDataSet(sqlgzzl, null);
//            if (dsgzzl.Tables[0].Rows.Count > 0)
//            {
//                SensorNo = dsgzzl.Tables[0].Rows[0]["SensorNo"].ToString();
//                distance = dsgzzl.Tables[0].Rows[0]["distance"].ToString();
//            }
//            sqlgzzl = "insert into PreReport (areaName,faceName,sensorNo,bracketNo,distance,ZLmax,ZLavg,CCLmax,CCLavg,MZLmax,MZLavg,reportDate,zlmin) values('" + cequ + "','" + gzm + "','" + SensorNo + "','" + zjbh + "','" + distance + "','" + GZZLmax + "','" + GZZLavg + "','" + CCLmax + "','" + CCLavg + "','" + MZLmax + "','" + MZLavg + "','" + dts + "','" + GZZLmin + "')";
//            DB.ExecuteSql(sqlgzzl, null);
//        }
        

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
        Response.AddHeader("Content-Disposition", "attachment; filename=初掌力与末阻力分析曲线.jpg");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
    protected void lbtn_excel_Click(object sender, EventArgs e) {
        string url = panel1.BackImageUrl;
        if (url == "")
        {
            SystemTool.AlertShow(this, "图片为空，请先刷新");
            return;
        }
        DataTable dt=(DataTable)ViewState["excel"];
        //模板文件  
        string TempletFileName = Server.MapPath("~/template/blank.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/ChuChengLiMoZuLi"+dts+".xlsx");
        TableToExcel(dt, TempletFileName, ReportFileName);

        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=初掌力与末阻力.xlsx");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
    /// <summary>
    /// Datable导出成Excel
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="file">moban</param>
    /// file2 保存地址
    public static void TableToExcel(DataTable dt, string file, string file2)
    {
        IWorkbook workbook;
        string fileExt = Path.GetExtension(file).ToLower();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }

            ISheet sheet = workbook.GetSheetAt(0);

            //IWorkbook workbook;
            //string fileExt = Path.GetExtension(file).ToLower();
            //if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(); } else { workbook = null; }
            //if (workbook == null) { return; }
            //ISheet sheet = string.IsNullOrEmpty(dt.TableName) ? workbook.CreateSheet("Sheet1") : workbook.CreateSheet(dt.TableName);
            //ISheet sheet = workbook.GetSheetAt(0);
            ////表头  
            //IRow row = sheet.CreateRow(0);
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    ICell cell = row.CreateCell(i);
            //    cell.SetCellValue(dt.Columns[i].ColumnName);
            //}
            IRow row = sheet.CreateRow(0);
            ICell cella = row.CreateCell(0);
            cella.SetCellValue("时间");
            ICell cellb = row.CreateCell(1);
            cellb.SetCellValue("末阻力");
            ICell cellc = row.CreateCell(2);
            cellc.SetCellValue("初撑力");
            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j <3; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
        }
        //转为字节数组  
        MemoryStream stream = new MemoryStream();
        workbook.Write(stream);
        var buf = stream.ToArray();

        //保存为Excel文件  
        using (FileStream fs = new FileStream(file2, FileMode.Create, FileAccess.Write))
        {
            fs.Write(buf, 0, buf.Length);
            fs.Flush();
        }
    }
}