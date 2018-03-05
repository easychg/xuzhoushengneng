using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

public partial class zongcaishujuchaxun : System.Web.UI.Page
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

    private void BindInfo()
    {
        //string sql = "SELECT p.AreaName,p.FaceName,BracketNo,convert(varchar(100),time,20) as time,p.sensorNo,Pressure1,Pressure2,(case AlarmH when 'false' then '正常' when'true' then '报警' end) as AlarmH  FROM PressureData p inner join PreSenInfo i on p.SensorNo=i.SensorNo where " + ViewState["search"] + " order by time desc,SensorNo";
        string sql = "SELECT p.AreaName,p.FaceName,BracketNo,convert(varchar(100),time,20) as time,p.sensorNo,Pressure1,Pressure2,(case when AlarmH = 'true' and AlarmL = 'false' then '超压' when AlarmH = 'false' and AlarmL = 'true' then '欠压' else '正常' end)  as AlarmH  FROM PressureData p inner join PreSenInfo i on p.SensorNo=i.SensorNo where " + ViewState["search"] + " order by time desc,SensorNo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
        
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //string cmd = e.CommandName.ToString();
        //string userid = e.CommandArgument.ToString();
        //string sql = "";
        //if (cmd == "lbtshan")
        //{
        //    sql = "delete from manager_info where man_id=" + userid;
        //}
        //if (cmd == "lbtn_open")
        //{
        //    sql = "update manager_info set state='启用' where man_id=" + userid;
        //}
        //if (cmd == "lbtn_close")
        //{
        //    sql = "update manager_info set state='禁止' where man_id=" + userid;
        //}
        //int result = DB.ExecuteSql(sql, null);
        //if (result > 0)
        //{
        //    SystemTool.AlertShow(this, "保存成功");
        //    BindInfo();
        //}
        //else
        //{
        //    SystemTool.AlertShow(this, "保存失败");
        //}
    }
    protected void search_Click(object sender, EventArgs e)
    {

        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string cequ = ddl_cequ.SelectedValue;
        string gongzuomian = ddl_gongzuomian.SelectedValue;
        string yalileixing = ddl_yalileixing.SelectedValue;
        string zhijiabianhao = ddl_zhijiabianhao.SelectedValue;
        ViewState["search"] = " 1=1";
        if (time1 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), time,20),10)>='" + time1 + "'";
        }
        if (time2 != "")
        {
            ViewState["search"] += " and left(CONVERT(varchar(100), time,20),10)<='" + time2 + "'";
        }
        if (cequ != "0") {
            ViewState["search"] += " and p.areaName='"+cequ+"'";
        }
        if (gongzuomian != "0") {
            ViewState["search"] += " and p.FaceName='"+gongzuomian+"'";
        }
        if (yalileixing != "0") {
            ViewState["search"] += " and Type='"+yalileixing+"'";
        }
        if (zhijiabianhao != "0") {
            ViewState["search"] += " and BracketNo='"+zhijiabianhao+"'";
        }
        BindInfo();
        string sql = "select * from dbo.PressurePar where areaname='"+cequ+"' and facename='"+gongzuomian+"'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_p1.Text = ds.Tables[0].Rows[0]["firstconnect"].ToString();
            lbl_p2.Text = ds.Tables[0].Rows[0]["secondconnect"].ToString();
        }
    }
    protected void lbtn_export_Click(object sender, EventArgs e) {
        string sql = "SELECT p.AreaName,p.FaceName,BracketNo,convert(varchar(100),time,20) as time,p.sensorNo,Pressure1,Pressure2,(case AlarmH when 'false' then '正常' when'true' then '报警' end) as AlarmH  FROM PressureData p inner join PreSenInfo i on p.SensorNo=i.SensorNo where " + ViewState["search"] + " order by time desc,SensorNo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);

        ////模板文件  
        //string TempletFileName = Server.MapPath("~/template/zongcaishujuchaxun.xlsx");//路径 
        ////导出文件  
        //string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        //string ReportFileName = Server.MapPath("~/xiazai/zongcaishujuchaxun" + dts + ".xlsx");

        DataTable dt = ds.Tables[0];// (DataTable)ViewState["excel"];
        //模板文件  
        string TempletFileName = Server.MapPath("~/template/blank.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/ChuChengLiMoZuLi" + dts + ".xlsx");
        TableToExcel(dt, TempletFileName, ReportFileName);

        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=综采数据查询.xlsx");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();

        //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        //object oMissing = System.Reflection.Missing.Value;
        //myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        //Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        //Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];

        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
            
        //    mySheet.Cells[3 + i, 1] = ds.Tables[0].Rows[i]["AreaName"].ToString();
        //    mySheet.Cells[3 + i, 2] = ds.Tables[0].Rows[i]["FaceName"].ToString();
        //    mySheet.Cells[3 + i, 3] = ds.Tables[0].Rows[i]["time"].ToString();
        //    mySheet.Cells[3 + i, 4] = ds.Tables[0].Rows[i]["sensorNo"].ToString();
        //    mySheet.Cells[3 + i, 5] = ds.Tables[0].Rows[i]["BracketNo"].ToString();
        //    mySheet.Cells[3 + i, 6] = ds.Tables[0].Rows[i]["Pressure1"].ToString();
        //    mySheet.Cells[3 + i, 7] = ds.Tables[0].Rows[i]["Pressure2"].ToString();
        //    mySheet.Cells[3 + i, 8] = ds.Tables[0].Rows[i]["AlarmH"].ToString();
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
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("综采数据查询表.xlsx"));
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
    protected void ddl_yalileixing_changed(object sender, EventArgs e) {
        string sql = "SELECT BracketNo FROM PreSenInfo where areaName='" +ddl_cequ.SelectedValue+ "' and FaceName='"+ddl_gongzuomian.SelectedValue+ "' and Type = '" +ddl_yalileixing.SelectedValue+ "' order by BracketNo asc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0) {
            ddl_zhijiabianhao.Items.Clear();
            ddl_zhijiabianhao.DataSource = ds.Tables[0];
            ddl_zhijiabianhao.DataTextField = "BracketNo";
            ddl_zhijiabianhao.DataValueField = "BracketNo";
            ddl_zhijiabianhao.DataBind();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_zhijiabianhao.Items.Insert(0, list);
        }
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
            cella.SetCellValue("测区名称");
            ICell cellb = row.CreateCell(1);
            cellb.SetCellValue("工作面名称");
            ICell cellc = row.CreateCell(2);
            cellc.SetCellValue("时间");
            ICell celld = row.CreateCell(3);
            celld.SetCellValue("传感器编号");
            ICell celle = row.CreateCell(4);
            celle.SetCellValue("支架号");
            ICell cellf = row.CreateCell(5);
            cellf.SetCellValue("前柱");
            ICell cellg = row.CreateCell(6);
            cellg.SetCellValue("后柱");
            ICell cellh = row.CreateCell(7);
            cellh.SetCellValue("报警状态");
            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < 8; j++)
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