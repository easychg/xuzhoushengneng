using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class maoganmaosuoshujuchaxun : System.Web.UI.Page
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
        string sql = @"SELECT Location,b.AreaName,b.roadwayName,convert(varchar(100),time,20) as time,b.sensorNo,Stress,(case AlarmH when 'false' then '正常' when'true' then '报警' end) as AlarmH 
FROM BoltData  b inner join BoltSenInfo bi on b.sensorNo=bi.sensorNo 
where " + ViewState["search"] + " order by time desc,Location";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();

    }
    protected void search_Click(object sender, EventArgs e)
    {

        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string cequ = ddl_cequ.SelectedValue;
        string roadway = ddl_roadway.SelectedValue;
        string anzhuangweizhi = ddl_anzhuangweizhi.SelectedValue;
        ViewState["search"] = " 1=1";
        if (time1 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), time,20),10)>='" + time1 + "'";
        }
        if (time2 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), time,20),10)<='" + time2 + "'";
        }
        if (cequ != "0")
        {
            ViewState["search"] += " and b.areaName='" + cequ + "'";
        }
        if (roadway != "0")
        {
            ViewState["search"] += " and b.roadwayName='" + roadway + "'";
        }
        if (anzhuangweizhi != "0")
        {
            ViewState["search"] += " and Location='" + anzhuangweizhi + "'";
        }
        BindInfo();
    }
    protected void lbtn_export_Click(object sender, EventArgs e)
    {
        string sql = @"SELECT Location,b.AreaName,b.roadwayName,convert(varchar(100),time,20) as time,b.sensorNo,Stress,(case AlarmH when 'false' then '正常' when'true' then '报警' end) as AlarmH 
FROM BoltData  b inner join BoltSenInfo bi on b.sensorNo=bi.sensorNo 
where " + ViewState["search"] + " order by time desc,Location";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        string[] columns = { "位置", "测区名称", "巷道名称", "时间", "传感器编号", "应力值（KN）", "报警状态" };
        NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("sheet1");
        NPOI.SS.UserModel.IRow row0 = sheet.CreateRow(0);
        for (int i = 0; i < columns.Length; i++)
        {
            row0.CreateCell(i).SetCellValue(columns[i]);
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(i + 1);
            for (int j = 0; j < columns.Length; j++)
            {
                row.CreateCell(j).SetCellValue(ds.Tables[0].Rows[i][j].ToString());
            }
        }
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        book.Write(ms);
        HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", "锚杆锚索数据查询"));
        HttpContext.Current.Response.BinaryWrite(ms.ToArray());
        book = null;
        ms.Close();
        ms.Dispose();
        ////模板文件  
        //string TempletFileName = Server.MapPath("~/template/maoganmaosuoshujuchaxun.xlsx");//路径 
        ////导出文件  
        //string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        //string ReportFileName = Server.MapPath("~/xiazai/maoganmaosuoshujuchaxun" + dts + ".xlsx");
        //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        //object oMissing = System.Reflection.Missing.Value;
        //myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        //Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        //Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];

        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{

        //    mySheet.Cells[2 + i, 1] = ds.Tables[0].Rows[i]["AreaName"].ToString();
        //    mySheet.Cells[2 + i, 2] = ds.Tables[0].Rows[i]["roadwayname"].ToString();
        //    mySheet.Cells[2 + i, 3] = ds.Tables[0].Rows[i]["time"].ToString();
        //    mySheet.Cells[2 + i, 4] = ds.Tables[0].Rows[i]["sensorNo"].ToString();
        //    mySheet.Cells[2 + i, 5] = ds.Tables[0].Rows[i]["location"].ToString();
        //    mySheet.Cells[2 + i, 6] = ds.Tables[0].Rows[i]["stress"].ToString();
        //    mySheet.Cells[2 + i, 7] = ds.Tables[0].Rows[i]["alarmh"].ToString();
            
        //}

        //try
        //{
        //    myBook.SaveAs(ReportFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        //}

        //catch (Exception ex)
        //{


        //}
        ////wb.Save();
        //myBook.Close(Type.Missing, Type.Missing, Type.Missing);
        ////wbs.Close();
        //myExcel.Quit();
        //myBook = null;
        ////wbs = null;
        //myExcel = null;
        //GC.Collect();


        ////filess.Close();  
        //System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        //Response.Clear();
        //Response.Charset = "GB2312";
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        //// 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("锚杆锚索数据查询表.xlsx"));
        //// 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        //Response.AddHeader("Content-Length", filet.Length.ToString());
        //// 指定返回的是一个不能被客户端读取的流，必须被下载   
        //Response.ContentType = "application/ms-excel";
        //// 把文件流发送到客户端   
        //Response.WriteFile(filet.FullName);
        //// 停止页面的执行   
        //Response.End();
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
            sql = "SELECT Location FROM BoltSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and roadwayName='" + ds.Tables[0].Rows[0]["roadName"].ToString() + "' order by Location asc";
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
        string sql = "SELECT Location FROM BoltSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and roadwayName='" + ddl_roadway.SelectedValue + "' order by Location asc";
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