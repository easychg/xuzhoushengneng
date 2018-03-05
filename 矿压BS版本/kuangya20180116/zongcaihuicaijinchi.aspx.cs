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

public partial class zongcaihuicaijinchi : System.Web.UI.Page
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
                ViewState["search_quanbu"] = " 1=1";
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
        string sql = "select * from huicai where " + ViewState["search"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
        //if (ds.Tables[0].Rows.Count == 1) {
        //    txtjitoujinchi.Text = ds.Tables[0].Rows[0]["hcjcnose"].ToString();
        //    txtzhongbujinchi.Text = ds.Tables[0].Rows[0]["hcjcmid"].ToString();
        //    txtjiweijinchi.Text = ds.Tables[0].Rows[0]["hcjctail"].ToString();
        //}
        decimal jitou = 0;
        decimal zhongbu = 0;
        decimal jiwei = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
            decimal cur = 0;
            cur = Convert.ToDecimal(ds.Tables[0].Rows[i]["hcjcnose"].ToString());
            jitou += cur;
            cur = Convert.ToDecimal(ds.Tables[0].Rows[i]["hcjcmid"].ToString());
            zhongbu += cur;
            cur = Convert.ToDecimal(ds.Tables[0].Rows[i]["hcjctail"].ToString());
            jiwei += cur;
        }
        txtjitouzongjinchi.Text = jitou.ToString();
        txtzhongbuzongjinchi.Text = zhongbu.ToString();
        txtjiweizhongjinchi.Text = jiwei.ToString();

    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        Label areaname = e.Item.FindControl("lblareaname") as Label;
        Label datetime = e.Item.FindControl("lbldatetime") as Label;
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            sql = "select * from huicai where areaname='" + areaname.Text + "' and facename='" + facename + "' and datetime='" + datetime.Text + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0) {
                txtjitoujinchi.Text = ds.Tables[0].Rows[0]["hcjcnose"].ToString();
                txtjiweijinchi.Text = ds.Tables[0].Rows[0]["hcjctail"].ToString();
                txtzhongbujinchi.Text = ds.Tables[0].Rows[0]["hcjcmid"].ToString();
                DateTime dt = Convert.ToDateTime(ds.Tables[0].Rows[0]["datetime"].ToString());
                datemin.Value = dt.ToString("yyyy-MM-dd");
                lblstate.Text = "u";
            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from huicai where areaname='" + areaname.Text + "' and facename='" + facename + "' and datetime='" + datetime.Text + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "保存成功");
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
            }
        }
    }
    protected void search_Click(object sender, EventArgs e)
    {
        string time1 = datemin.Value;
        string cequ = ddl_cequ.SelectedValue;
        string gongzuomian = ddl_gongzuomian.SelectedValue;
        ViewState["search_quanbu"] = " 1=1";
        ViewState["search"] = " 1=1";
        if (time1 != "")
        {
            //ViewState["search_quanbu"] += " and left(CONVERT(varchar(100), datetime,20),10)='" + time1 + "'";
        }
        if (cequ != "0")
        {
            //ViewState["search_quanbu"] += " and areaName='" + cequ + "'";
            ViewState["search"] += " and areaName='" + cequ + "'";
        }
        if (gongzuomian != "0")
        {
            //ViewState["search_quanbu"] += " and FaceName='" + gongzuomian + "'";
            ViewState["search"] += " and areaName='" + cequ + "'";
        }
        BindInfo();
    }
    protected void lbtn_add_Click(object sender, EventArgs e) {
        string areaname = ddl_cequ.SelectedValue;
        string facename = ddl_gongzuomian.SelectedValue;
        string datetime = datemin.Value;
        string hcjcnose = txtjitoujinchi.Text;
        string hcjctail = txtjiweijinchi.Text;
        decimal jitou = Convert.ToDecimal(hcjcnose);
        decimal jiwei = Convert.ToDecimal(hcjctail);
        decimal zhongbu = (jitou + jiwei) / 2;
        string hcjcmid = zhongbu.ToString();
        if (lblstate.Text == "i") {
            string sqlck = "select * from huicai where areaname='" + areaname + "' and facename='" + facename + "' and datetime='" + datetime + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sqlck, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SystemTool.AlertShow(this, "回采数据已经添加，如需修改请联系管理员！");
                return;
            }
            string sql = "insert into huicai(id,areaname,facename,datetime,hcjcnose,hcjcmid,hcjctail)values(newid(),'" + areaname + "','" + facename + "','" + datetime + "','" + hcjcnose + "','" + hcjcmid + "','" + hcjctail + "')";
            int r = DB.ExecuteSql(sql, null);
            if (r > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        if (lblstate.Text == "u") {
            //string sql = "insert into huicai(id,areaname,facename,datetime,hcjcnose,hcjcmid,hcjctail)values(newid(),'" + areaname + "','" + facename + "','" + datetime + "','" + hcjcnose + "','" + hcjcmid + "','" + hcjctail + "')";
            string sql = "update huicai set hcjcnose='"+hcjcnose+"',hcjcmid='"+hcjcmid+"',hcjctail='"+hcjctail+"' where areaname='" + areaname + "' and facename='" + facename + "' and datetime='" + datetime + "'";
            int r = DB.ExecuteSql(sql, null);
            if (r > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        
    }
    protected void lbtn_export_Click(object sender, EventArgs e)
    {
        string sql = "select * from huicai where " + ViewState["search"]; 
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        DataTable dt = ds.Tables[0];// (DataTable)ViewState["excel"];
        //模板文件  
        string TempletFileName = Server.MapPath("~/template/blank.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/zongcaihuicaijinchi" + dts + ".xlsx");
        TableToExcel(dt, TempletFileName, ReportFileName);

        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=综采灰采进尺.xlsx");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
        ////模板文件  
        //string TempletFileName = Server.MapPath("~/template/blank.xlsx");//路径 
        ////导出文件  
        //string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        //string ReportFileName = Server.MapPath("~/xiazai/zongcaihuicaijinchi" + dts + ".xlsx");
        //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        //object oMissing = System.Reflection.Missing.Value;
        //myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        //Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        //Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];

        //decimal jitou = 0;
        //decimal zhongbu = 0;
        //decimal jiwei = 0;
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{
        //    decimal cur = 0;
        //    cur = Convert.ToDecimal(ds.Tables[0].Rows[i]["hcjcnose"].ToString());
        //    jitou += cur;
        //    cur = Convert.ToDecimal(ds.Tables[0].Rows[i]["hcjcmid"].ToString());
        //    zhongbu += cur;
        //    cur = Convert.ToDecimal(ds.Tables[0].Rows[i]["hcjctail"].ToString());
        //    jiwei += cur;
        //}
        //mySheet.Cells[1, 1] = "机头总进尺";
        //mySheet.Cells[1, 2] = jitou.ToString();
        //mySheet.Cells[1, 3] = "中部总进尺";
        //mySheet.Cells[1, 4] = zhongbu.ToString();
        //mySheet.Cells[1, 5] = "机尾总进尺";
        //mySheet.Cells[1, 6] = jiwei.ToString();

        //mySheet.Cells[2, 1] = "测区";
        //mySheet.Cells[2, 2] = "工作面";
        //mySheet.Cells[2, 3] = "日期";
        //mySheet.Cells[2, 4] = "机头进尺";
        //mySheet.Cells[2, 5] = "中部进尺";
        //mySheet.Cells[2, 6] = "机尾进尺";
        //for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //{

        //    mySheet.Cells[3 + i, 1] = ds.Tables[0].Rows[i]["AreaName"].ToString();
        //    mySheet.Cells[3 + i, 2] = ds.Tables[0].Rows[i]["FaceName"].ToString();
        //    mySheet.Cells[3 + i, 3] = ds.Tables[0].Rows[i]["datetime"].ToString();
        //    mySheet.Cells[3 + i, 4] = ds.Tables[0].Rows[i]["hcjcnose"].ToString();
        //    mySheet.Cells[3 + i, 5] = ds.Tables[0].Rows[i]["hcjcmid"].ToString();
        //    mySheet.Cells[3 + i, 6] = ds.Tables[0].Rows[i]["hcjctail"].ToString();
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
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("综采回采进尺.xlsx"));
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
    //protected void ddl_yalileixing_changed(object sender, EventArgs e)
    //{
    //    string sql = "SELECT BracketNo FROM PreSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + ddl_gongzuomian.SelectedValue + "' and Type = '" + ddl_yalileixing.SelectedValue + "' order by BracketNo asc";
    //    DataSet ds = DB.ExecuteSqlDataSet(sql, null);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_zhijiabianhao.Items.Clear();
    //        ddl_zhijiabianhao.DataSource = ds.Tables[0];
    //        ddl_zhijiabianhao.DataTextField = "BracketNo";
    //        ddl_zhijiabianhao.DataValueField = "BracketNo";
    //        ddl_zhijiabianhao.DataBind();
    //        ListItem list = new ListItem("--请选择--", "0");
    //        ddl_zhijiabianhao.Items.Insert(0, list);
    //    }
    //}
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

            decimal jitou = 0;
            decimal zhongbu = 0;
            decimal jiwei = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                decimal cur = 0;
                cur = Convert.ToDecimal(dt.Rows[i]["hcjcnose"].ToString());
                jitou += cur;
                cur = Convert.ToDecimal(dt.Rows[i]["hcjcmid"].ToString());
                zhongbu += cur;
                cur = Convert.ToDecimal(dt.Rows[i]["hcjctail"].ToString());
                jiwei += cur;
            }
            IRow row0 = sheet.CreateRow(0);
            ICell cella0 = row0.CreateCell(0);
            cella0.SetCellValue("机头总进尺");
            ICell cella1 = row0.CreateCell(1);
            cella1.SetCellValue(jitou.ToString());
            ICell cella2 = row0.CreateCell(2);
            cella2.SetCellValue("中部总进尺");
            ICell cella3 = row0.CreateCell(3);
            cella3.SetCellValue(zhongbu.ToString());
            ICell cella4 = row0.CreateCell(4);
            cella4.SetCellValue("机尾总进尺");
            ICell cella5 = row0.CreateCell(5);
            cella5.SetCellValue(jiwei.ToString());

            IRow row = sheet.CreateRow(1);
            ICell cella = row.CreateCell(0);
            cella.SetCellValue("测区");
            ICell cellb = row.CreateCell(1);
            cellb.SetCellValue("工作面");
            ICell cellc = row.CreateCell(2);
            cellc.SetCellValue("日期");
            ICell celld = row.CreateCell(3);
            celld.SetCellValue("机头进尺");
            ICell celle = row.CreateCell(4);
            celle.SetCellValue("中部进尺");
            ICell cellf = row.CreateCell(5);
            cellf.SetCellValue("机尾进尺");
            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 2);
                for (int j = 0; j < 5; j++)
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