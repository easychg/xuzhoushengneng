using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;

public partial class chaxun_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewState["search"] = " 1=1";
            //BindInfo();
            //export();
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                HttpCookie userid = Request.Cookies[Cookie.ComplanyId];
                ViewState["userid"] = userid.Value;
                ViewState["search"] = " and 1=1";
                //string sql = "select * from tiaojian_info";
                //DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    string caoqi = ds.Tables[0].Rows[0]["chaoqi"].ToString();
                //    string weidaohuo = ds.Tables[0].Rows[0]["weidaohuo"].ToString();
                //    double cq = Convert.ToDouble(caoqi);
                //    double wdh = Convert.ToDouble(weidaohuo);
                //    string dscq = DateTime.Now.AddDays(-cq).ToString("yyyy-MM-dd");
                //    string dswdh = DateTime.Now.AddDays(wdh).ToString("yyyy-MM-dd");
                //    datemin.Value = dscq;
                //    datemax.Value = dswdh;
                //    ViewState["search"] += " and b.yingdaoshijian >='" + dscq + "' ";
                //    ViewState["search"] += " and b.yingdaoshijian <='" + dswdh + "' ";
                //}

                //BinDDL();
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "login.aspx");
            }
        }
    }

    //private void BinDDL()
    //{
    //    string sql = "select * from shiyongdanwei";
    //    DataSet ds = DB.ExecuteSqlDataSet(sql, null);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_shiyongdanwei.DataSource = ds;
    //        ddl_shiyongdanwei.DataValueField = "mingcheng";
    //        ddl_shiyongdanwei.DataTextField = "mingcheng";
    //        ddl_shiyongdanwei.DataBind();
    //        ListItem item = new ListItem("--请选择--", "0");
    //        ddl_shiyongdanwei.Items.Insert(0, item);
    //    }
    //    sql = "select * from jihualeixing";
    //    ds = DB.ExecuteSqlDataSet(sql, null);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_jiahualeixing.DataSource = ds;
    //        ddl_jiahualeixing.DataValueField = "mingcheng";
    //        ddl_jiahualeixing.DataTextField = "mingcheng";
    //        ddl_jiahualeixing.DataBind();
    //        ListItem item = new ListItem("--请选择--", "0");
    //        ddl_jiahualeixing.Items.Insert(0, item);
    //    }
    //    sql = "select * from caigoufangshi";
    //    ds = DB.ExecuteSqlDataSet(sql, null);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_caigoufangshi.DataSource = ds;
    //        ddl_caigoufangshi.DataValueField = "mingcheng";
    //        ddl_caigoufangshi.DataTextField = "mingcheng";
    //        ddl_caigoufangshi.DataBind();
    //        ListItem item = new ListItem("--请选择--", "0");
    //        ddl_caigoufangshi.Items.Insert(0, item);
    //    }
    //    sql = "select * from wuzishuxing";
    //    ds = DB.ExecuteSqlDataSet(sql, null);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_wizishuxing.DataSource = ds;
    //        ddl_wizishuxing.DataValueField = "mingcheng";
    //        ddl_wizishuxing.DataTextField = "mingcheng";
    //        ddl_wizishuxing.DataBind();
    //        ListItem item = new ListItem("--请选择--", "0");
    //        ddl_wizishuxing.Items.Insert(0, item);
    //    }
    //    sql = "select * from gonghuochangshang";
    //    ds = DB.ExecuteSqlDataSet(sql, null);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        ddl_gonghuoshang.DataSource = ds;
    //        ddl_gonghuoshang.DataValueField = "mingcheng";
    //        ddl_gonghuoshang.DataTextField = "mingcheng";
    //        ddl_gonghuoshang.DataBind();
    //        ListItem item = new ListItem("--请选择--", "0");
    //        ddl_gonghuoshang.Items.Insert(0, item);
    //    }
    //}

    private void BindInfo()
    {
        string sql = @"select b.dingdanbianhao,b.gonghuochangshang,b.dinghuoyiju,b.caigoufangshi,b.dingdanriqi,b.jihuabianhao,b.yingdaoshijian,(select count(isfinished) from taizhang_info a where a.dingdanbianhao=b.dingdanbianhao and a.isfinished=0) isfinished
from taizhang_info b
where isdelete=0 " + ViewState["search"] + " group by dingdanbianhao,gonghuochangshang,dinghuoyiju,caigoufangshi,dingdanriqi,b.jihuabianhao,b.yingdaoshijian order by b.yingdaoshijian asc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_taizhang.DataSource = ds.Tables[0];
        rpt_taizhang.DataBind();
        lbtn_delete.Visible = false;
        if (ViewState["userid"] != null)
        {
            if (ViewState["userid"].ToString() == "1")
            {
                lbtn_delete.Visible = true;
            }

        }
    }
    protected void lbtn_search_Click(object sender, EventArgs e)
    {
        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string state = ddl_state.SelectedValue;//0全部2未到货3超期
        string dingdanbianhao = txtdingdanbianhao.Value;
        string jihuabianhao = txtjihuabianhao.Value;
        string changshangmingcheng = txtchangshangmingcheng.Value;


        ViewState["search"] = " and 1=1";
        //if (time1.ToString().Trim() != "")
        //{
        //    ViewState["search"] += " and b.yingdaoshijian >='" + time1 + "' ";
        //}
        //if (time2.ToString().Trim() != "")
        //{
        //    ViewState["search"] += " and b.yingdaoshijian <='" + time2 + "'";
        //}
        //if (state == "2")
        //{
        //    ViewState["search"] += " and b.yingdaoshijian >='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
        //}
        //if (state == "3")
        //{
        //    ViewState["search"] += " and b.yingdaoshijian <='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
        //}
        if (dingdanbianhao.Trim() != "")
        {
            ViewState["search"] += " and b.dingdanbianhao ='" + dingdanbianhao + "'";
        }
        if (jihuabianhao.Trim() != "")
        {
            ViewState["search"] += " and b.jihuabianhao ='" + jihuabianhao + "'";
        }
        if (changshangmingcheng.Trim() != "")
        {
            ViewState["search"] += " and b.gonghuochangshang like'%" + changshangmingcheng + "%'";
        }
        BindInfo();
    }
    protected void lbtn_daoru_Click(object sender, EventArgs e)
    {
        //if (FileUpload1.HasFile)
        //{
        //    string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
        //    FileUpload1.SaveAs(Server.MapPath("~/daoru/") + dt + FileUpload1.FileName);
        //    //导入数据
        //    DataTable dta = ExcelToTable(Server.MapPath("~/daoru/") + dt + FileUpload1.FileName);
        //    BindInfo();
        //    if (dta.Rows.Count > 0)
        //    {
        //        string file = Server.MapPath("~/daochu/daochu.xlsx");
        //        string file2 = Server.MapPath("~/xiazai/daochu.xlsx");
        //        TableToExcel(dta, file, file2);
        //        export(file2);//导出错误数据
        //    }



        //}
        //else
        //{
        //    SystemTool.AlertShow(this, "请选择要导入的Excel");
        //}
    }
    #region

    /// <summary>
    /// Excel导入成Datable
    /// </summary>
    /// <param name="file"></param>
    /// <returns>返回导入失败的数据</returns>
    public static DataTable ExcelToTable(string file)
    {

        DataTable dt = new DataTable();
        IWorkbook workbook;
        string fileExt = Path.GetExtension(file).ToLower();
        using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
        {
            //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
            if (fileExt == ".xlsx") { workbook = new XSSFWorkbook(fs); } else if (fileExt == ".xls") { workbook = new HSSFWorkbook(fs); } else { workbook = null; }
            if (workbook == null) { return null; }
            ISheet sheet = workbook.GetSheetAt(0);

            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            List<int> columns = new List<int>();
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueType(header.GetCell(i));
                if (obj == null || obj.ToString() == string.Empty)
                {
                    dt.Columns.Add(new DataColumn("Columns" + i.ToString()));
                }
                else
                    dt.Columns.Add(new DataColumn(obj.ToString()));
                columns.Add(i);
            }
            //数据  
            bool isinsert = true;
            for (int i = sheet.FirstRowNum + 2; i <= sheet.LastRowNum; i++)
            {
                DataRow dr = dt.NewRow();
                //bool hasValue = false;
                string sql = "insert into taizhang_info (yuefen,shiyongdanwei,jihuabianhao,zijinlaiyuan,jihualeixing,xunjiayuan,caigoubianhao,wuzishuxing,wuzibianma,dingjiawuzimingcheng,dingjiaguigexinghao,dingjiadanwei,dingjiashuliang,hanshuidanjia,hanshuijine,shuilv,caigoufangshi,dingjiafangshi,suoshuxieyihao,dingdanbianhao,dinghuoyiju,jiaohuoshijian,jiaohuodidian,wuzipinpai,gonghuochangshang,changshangdianhua,jiyaofawenshijian,dingdanshijian,jiaohuoqi,yijiaohetongshijian,daohuoshijian,beizhu,fukuanfangshi,wuzifenlei,dianshangpingtai,wangdianmingcheng,xunjiafangshi) values(";
                string sql2 = "";
                foreach (int j in columns)
                {

                    if (j != 0)
                    {
                        string currentstr = "'" + sheet.GetRow(i).GetCell(j) + "',";
                        if (j == 22)
                        {
                            //交货时间
                            try
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd HH:mm:ss");
                                currentstr = "'" + cccc + "',";
                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        if (j == 27)
                        {
                            //"纪要发文时间"
                            try
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd HH:mm:ss");
                                currentstr = "'" + cccc + "',";
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        if (j == 28)
                        {
                            //订单时间
                            try
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd HH:mm:ss");
                                currentstr = "'" + cccc + "',";
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        if (j == 30)
                        {
                            //移交合同时间
                            try
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd HH:mm:ss");
                                currentstr = "'" + cccc + "',";
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        if (j == 31)
                        {
                            //到货时间
                            try
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd HH:mm:ss");
                                currentstr = "'" + cccc + "',";
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        sql2 += currentstr;
                    }
                    dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                    if (j == 2)
                    {
                        //使用单位
                        string sqlck = "select shiyongdanwei_id from shiyongdanwei where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 17)
                    {
                        //采购方式
                        string sqlck = "select caigoufangshi_id from caigoufangshi where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 18)
                    {
                        //定价方式
                        string sqlck = "select dingjiafangshi_id from dingjiafangshi where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 21)
                    {
                        //订货依据
                        string sqlck = "select dinghuoyiju_id from dinghuoyiju where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 6)
                    {
                        //询价员
                        string sqlck = "select xunjiayuan_id from xunjiayuan where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 4)
                    {
                        //资金来源
                        string sqlck = "select zijinlaiyuan_id from zijinlaiyuan where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 5)
                    {
                        //计划类型
                        string sqlck = "select jihualeixing_id from jihualeixing where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 33)
                    {
                        //付款方式，数据库中有，要导excel无
                        string sqlck = "select fukuanfangshi_id from fukuanfangshi where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 34)
                    {
                        //物资分类，数据库中有，要导excel无
                        string sqlck = "select wuzifenlei_id from wuzifenlei where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    if (j == 35)
                    {
                        //电商平台，数据库中有，要导excel无
                        string sqlck = "select dianshangpingtai_id from dianshangpingtai where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        string resultck = DB.ExecuteSqlValue(sqlck, null);
                        if (resultck == "" || resultck == "no")
                        {
                            //数据不符，过滤
                            isinsert = false;
                        }
                    }
                    //if (dr[j] != null && dr[j].ToString() != string.Empty)
                    //{
                    //    hasValue = true;
                    //}
                }
                if (sql2.Length > 0)
                {
                    sql2 = sql2.Substring(0, sql2.Length - 1);
                }
                sql += sql2;
                sql += ")";
                try
                {
                    //是否插入，不插入则添加到导出表
                    if (isinsert)
                    {
                        int m = DB.ExecuteSql(sql, null);
                    }
                    else
                    {
                        dt.Rows.Add(dr);
                        isinsert = true;
                    }

                }
                catch (Exception ex)
                {
                    dt.Rows.Add(dr);
                    isinsert = true;
                }

                //if (hasValue)
                //{
                //    dt.Rows.Add(dr);
                //}
            }
        }
        return dt;
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

            //数据  
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 2);
                for (int j = 0; j < dt.Columns.Count; j++)
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

    /// <summary>
    /// 获取单元格类型
    /// </summary>
    /// <param name="cell"></param>
    /// <returns></returns>
    private static object GetValueType(ICell cell)
    {
        if (cell == null)
            return null;
        switch (cell.CellType)
        {
            case CellType.Blank: //BLANK:  
                return null;
            case CellType.Boolean: //BOOLEAN:  
                return cell.BooleanCellValue;
            case CellType.Numeric: //NUMERIC:  
                return cell.NumericCellValue;
            case CellType.String: //STRING:  
                return cell.StringCellValue;
            case CellType.Error: //ERROR:  
                return cell.ErrorCellValue;
            case CellType.Formula: //FORMULA:  
            default:
                return "=" + cell.CellFormula;
        }
    }
    #endregion
    protected void lbtn_daohuo_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rpt_taizhang.Items.Count; i++)
        {
            CheckBox ckb = rpt_taizhang.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true)
            {
                ids += "'" + ckb.ToolTip + "',";
            }
        }
        if (ids.Length > 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择需要操作的数据");
            return;
        }
        string time = DateTime.Now.ToString("yyyy-MM-dd");
        string sql = "update taizhang_info set isfinished=1,shidaoshijian='" + time + "' where dingdanbianhao in(" + ids + ")";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            BindInfo();
            SystemTool.AlertShow(this, "操作成功");
        }
        else
        {
            SystemTool.AlertShow(this, "操作失败");
        }
    }
    protected void lbtn_delete_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rpt_taizhang.Items.Count; i++)
        {
            CheckBox ckb = rpt_taizhang.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true)
            {
                ids += "'" + ckb.ToolTip + "',";
            }
        }
        if (ids.Length > 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择需要操作的数据");
            return;
        }
        string sql = "update taizhang_info set isdelete=1 where dingdanbianhao in(" + ids + ")";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            BindInfo();
            SystemTool.AlertShow(this, "操作成功");
        }
        else
        {
            SystemTool.AlertShow(this, "操作失败");
        }
    }
    protected void lbtn_changxie_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rpt_taizhang.Items.Count; i++)
        {
            CheckBox ckb = rpt_taizhang.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true)
            {
                ids += ckb.ToolTip + ",";
            }
        }
        if (ids.Length > 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择需要操作的数据");
            return;
        }
        string sql = "select * from taizhang_info where taizhang_id in(" + ids + ")";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);

        //模板文件  
        string TempletFileName = Server.MapPath("~/daochu/changxie.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/changxie" + dts + ".xlsx");
        Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        object oMissing = System.Reflection.Missing.Value;
        myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];

        //Microsoft.Office.Interop.Excel.Range r = null;
        //r = (Microsoft.Office.Interop.Excel.Range)mySheet.Cells[3, 3];
        //mySheet.Cells[20, 3] = "123456";

        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)mySheet.Rows[9, Type.Missing];
        //插入行
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            range.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlDirection.xlDown,
                Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
        }
        int h = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[4, 1] = "订单编号：" + ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
            mySheet.Cells[4, 8] = "订单日期：" + ds.Tables[0].Rows[0]["dingdanshijian"].ToString();
            mySheet.Cells[5, 1] = "使用单位：" + ds.Tables[0].Rows[0]["shiyongdanwei"].ToString();
            mySheet.Cells[5, 8] = "所属协议号：" + ds.Tables[0].Rows[0]["suoshuxieyihao"].ToString();
            mySheet.Cells[6, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            mySheet.Cells[6, 8] = "订货依据：" + ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();

        }
        decimal zongjia = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            mySheet.Cells[8 + i, 1] = (i + 1).ToString();
            mySheet.Cells[8 + i, 2] = ds.Tables[0].Rows[i]["wuzibianma"].ToString();
            mySheet.Cells[8 + i, 3] = ds.Tables[0].Rows[i]["dingjiawuzimingcheng"].ToString();
            mySheet.Cells[8 + i, 4] = ds.Tables[0].Rows[i]["dingjiaguigexinghao"].ToString();
            mySheet.Cells[8 + i, 5] = ds.Tables[0].Rows[i]["dingjiadanwei"].ToString();
            mySheet.Cells[8 + i, 6] = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString();
            mySheet.Cells[8 + i, 7] = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString();
            decimal shuliang = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["dingjiashuliang"].ToString());
            decimal danjia = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuidanjia"].ToString());
            zongjia += (danjia * shuliang);
            mySheet.Cells[8 + i, 8] = (shuliang * danjia).ToString();//ds.Tables[0].Rows[i]["dingdanbianhao"].ToString();
            mySheet.Cells[8 + i, 9] = ds.Tables[0].Rows[i]["jiaohuoshijian"].ToString();
            mySheet.Cells[8 + i, 10] = ds.Tables[0].Rows[i]["jiaohuodidian"].ToString();
            mySheet.Cells[8 + i, 11] = ds.Tables[0].Rows[i]["wuzipinpai"].ToString();
            mySheet.Cells[8 + i, 12] = ds.Tables[0].Rows[i]["beizhu"].ToString();
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[9 + h, 1] = "付款方式：" + ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
            mySheet.Cells[9 + h, 6] = "合计(含税" + ds.Tables[0].Rows[0]["shuilv"].ToString() + "%)";
            mySheet.Cells[9 + h, 8] = zongjia.ToString();
            mySheet.Cells[9 + h, 9] = "大写：" + MoneyToChinese(zongjia.ToString());
            mySheet.Cells[11 + h, 1] = "询价员：" + ds.Tables[0].Rows[0]["xunjiayuan"].ToString() + "                                      询价科长：                                   副经理： 										";
        }

        try
        {
            myBook.SaveAs(ReportFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        }

        catch (Exception ex)
        {


        }
        //wb.Save();
        myBook.Close(Type.Missing, Type.Missing, Type.Missing);
        //wbs.Close();
        myExcel.Quit();
        myBook = null;
        //wbs = null;
        myExcel = null;
        GC.Collect();


        //filess.Close();  
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("ChangXieDingHuoTongZhiDan.xlsx"));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
    protected void lbtn_putong_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rpt_taizhang.Items.Count; i++)
        {
            CheckBox ckb = rpt_taizhang.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true)
            {
                ids += ckb.ToolTip + ",";
            }
        }
        if (ids.Length > 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择需要操作的数据");
            return;
        }
        string sql = "select * from taizhang_info where taizhang_id in(" + ids + ")";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);

        //模板文件  
        string TempletFileName = Server.MapPath("~/daochu/putong.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/putong" + dts + ".xlsx");
        Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        object oMissing = System.Reflection.Missing.Value;
        myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];

        //Microsoft.Office.Interop.Excel.Range r = null;
        //r = (Microsoft.Office.Interop.Excel.Range)mySheet.Cells[3, 3];
        //mySheet.Cells[20, 3] = "123456";

        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)mySheet.Rows[8, Type.Missing];
        //插入行
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            range.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlDirection.xlDown,
                Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
        }
        int h = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[4, 1] = "订单编号：" + ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
            mySheet.Cells[4, 9] = "订单日期：" + ds.Tables[0].Rows[0]["dingdanshijian"].ToString();
            mySheet.Cells[5, 1] = "使用单位：" + ds.Tables[0].Rows[0]["shiyongdanwei"].ToString();
            mySheet.Cells[5, 9] = "订货依据：" + ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
            mySheet.Cells[6, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            mySheet.Cells[6, 9] = "采购方式：" + ds.Tables[0].Rows[0]["caigoufangshi"].ToString();

        }
        decimal zongjia = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            mySheet.Cells[8 + i, 1] = (i + 1).ToString();
            mySheet.Cells[8 + i, 2] = ds.Tables[0].Rows[i]["wuzibianma"].ToString();
            mySheet.Cells[8 + i, 3] = ds.Tables[0].Rows[i]["dingjiawuzimingcheng"].ToString();
            mySheet.Cells[8 + i, 4] = ds.Tables[0].Rows[i]["dingjiaguigexinghao"].ToString();
            mySheet.Cells[8 + i, 5] = ds.Tables[0].Rows[i]["dingjiadanwei"].ToString();
            mySheet.Cells[8 + i, 6] = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString();
            mySheet.Cells[8 + i, 7] = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString();
            decimal shuliang = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["dingjiashuliang"].ToString());
            decimal danjia = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuidanjia"].ToString());
            zongjia += (danjia * shuliang);
            mySheet.Cells[8 + i, 8] = (shuliang * danjia).ToString();//ds.Tables[0].Rows[i]["dingdanbianhao"].ToString();
            mySheet.Cells[8 + i, 9] = ds.Tables[0].Rows[i]["jiaohuoshijian"].ToString();
            mySheet.Cells[8 + i, 10] = ds.Tables[0].Rows[i]["jiaohuodidian"].ToString();
            mySheet.Cells[8 + i, 11] = ds.Tables[0].Rows[i]["jihuabianhao"].ToString();
            // mySheet.Cells[8 + i, 12] = ds.Tables[0].Rows[i]["beizhu"].ToString();
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[8 + h, 1] = "付款方式：" + ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
            mySheet.Cells[8 + h, 6] = "合计(含税" + ds.Tables[0].Rows[0]["shuilv"].ToString() + "%)";
            mySheet.Cells[8 + h, 8] = zongjia.ToString();
            mySheet.Cells[8 + h, 9] = "大写：" + MoneyToChinese(zongjia.ToString());
            mySheet.Cells[10 + h, 1] = "询价员：" + ds.Tables[0].Rows[0]["xunjiayuan"].ToString() + "                                      询价科长：                                   副经理： 										";
        }

        try
        {
            myBook.SaveAs(ReportFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        }

        catch (Exception ex)
        {


        }
        //wb.Save();
        myBook.Close(Type.Missing, Type.Missing, Type.Missing);
        //wbs.Close();
        myExcel.Quit();
        myBook = null;
        //wbs = null;
        myExcel = null;
        GC.Collect();


        //filess.Close();  
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("PuTongDingHuoTongZhiDan.xlsx"));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
    protected void lbtn_wanggou_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rpt_taizhang.Items.Count; i++)
        {
            CheckBox ckb = rpt_taizhang.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true)
            {
                ids += ckb.ToolTip + ",";
            }
        }
        if (ids.Length > 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择需要操作的数据");
            return;
        }
        string sql = "select * from taizhang_info where taizhang_id in(" + ids + ")";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);

        //模板文件  
        string TempletFileName = Server.MapPath("~/daochu/wanggou.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/wanggou" + dts + ".xlsx");
        Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        object oMissing = System.Reflection.Missing.Value;
        myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];

        //Microsoft.Office.Interop.Excel.Range r = null;
        //r = (Microsoft.Office.Interop.Excel.Range)mySheet.Cells[3, 3];
        //mySheet.Cells[20, 3] = "123456";

        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)mySheet.Rows[9, Type.Missing];
        //插入行
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            range.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlDirection.xlDown,
                Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
        }
        int h = ds.Tables[0].Rows.Count;
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[3, 1] = "订单编号：" + ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
            mySheet.Cells[4, 1] = "电商平台：" + ds.Tables[0].Rows[0]["dianshangpingtai"].ToString();
            mySheet.Cells[4, 9] = "订单日期：" + ds.Tables[0].Rows[0]["dingdanshijian"].ToString();
            mySheet.Cells[5, 1] = "网店名称：" + ds.Tables[0].Rows[0]["wangdianmingcheng"].ToString();
            mySheet.Cells[5, 9] = "订货依据：" + ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
            mySheet.Cells[6, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            mySheet.Cells[6, 9] = "询价方式：" + ds.Tables[0].Rows[0]["xunjiafangshi"].ToString();

        }
        decimal zongjia = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            mySheet.Cells[9 + i, 1] = (i + 1).ToString();
            mySheet.Cells[9 + i, 2] = ds.Tables[0].Rows[i]["shiyongdanwei"].ToString();
            mySheet.Cells[9 + i, 3] = ds.Tables[0].Rows[i]["dingjiawuzimingcheng"].ToString();
            mySheet.Cells[9 + i, 4] = ds.Tables[0].Rows[i]["dingjiaguigexinghao"].ToString();
            mySheet.Cells[9 + i, 5] = ds.Tables[0].Rows[i]["dingjiadanwei"].ToString();
            mySheet.Cells[9 + i, 6] = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString();
            mySheet.Cells[9 + i, 7] = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString();
            decimal shuliang = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["dingjiashuliang"].ToString());
            decimal danjia = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuidanjia"].ToString());
            zongjia += (danjia * shuliang);
            mySheet.Cells[9 + i, 8] = (shuliang * danjia).ToString();//ds.Tables[0].Rows[i]["dingdanbianhao"].ToString();
            mySheet.Cells[9 + i, 9] = ds.Tables[0].Rows[i]["jiaohuoshijian"].ToString();
            mySheet.Cells[9 + i, 10] = ds.Tables[0].Rows[i]["jiaohuodidian"].ToString();
            mySheet.Cells[9 + i, 11] = ds.Tables[0].Rows[i]["wuzipinpai"].ToString();
            //mySheet.Cells[9 + i, 12] = ds.Tables[0].Rows[i]["beizhu"].ToString();
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[10 + h, 1] = "付款方式：" + ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
            mySheet.Cells[10 + h, 8] = zongjia.ToString();
            mySheet.Cells[10 + h, 9] = "大写：（含" + ds.Tables[0].Rows[0]["shuilv"].ToString() + "%的增值税）" + MoneyToChinese(zongjia.ToString());
            mySheet.Cells[12 + h, 1] = "询价员：" + ds.Tables[0].Rows[0]["xunjiayuan"].ToString() + "                                      询价科长：                                   副经理： 										";
        }

        try
        {
            myBook.SaveAs(ReportFileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        }

        catch (Exception ex)
        {


        }
        //wb.Save();
        myBook.Close(Type.Missing, Type.Missing, Type.Missing);
        //wbs.Close();
        myExcel.Quit();
        myBook = null;
        //wbs = null;
        myExcel = null;
        GC.Collect();


        //filess.Close();  
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("网购DingHuoTongZhiDan.xlsx"));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();
    }
    protected void export(string ReportFileName)
    {
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("taizhang.xlsx"));
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
    /// 金额转为大写金额
    /// </summary>
    /// <param name="LowerMoney"></param>
    /// <returns></returns>
    public string MoneyToChinese(string LowerMoney)
    {
        string functionReturnValue = null;
        bool IsNegative = false; // 是否是负数
        if (LowerMoney.Trim().Substring(0, 1) == "-")
        {
            // 是负数则先转为正数
            LowerMoney = LowerMoney.Trim().Remove(0, 1);
            IsNegative = true;
        }
        string strLower = null;
        string strUpart = null;
        string strUpper = null;
        int iTemp = 0;
        // 保留两位小数 123.489→123.49　　123.4→123.4
        LowerMoney = Math.Round(double.Parse(LowerMoney), 2).ToString();
        if (LowerMoney.IndexOf(".") > 0)
        {
            if (LowerMoney.IndexOf(".") == LowerMoney.Length - 2)
            {
                LowerMoney = LowerMoney + "0";
            }
        }
        else
        {
            LowerMoney = LowerMoney + ".00";
        }
        strLower = LowerMoney;
        iTemp = 1;
        strUpper = "";
        while (iTemp <= strLower.Length)
        {
            switch (strLower.Substring(strLower.Length - iTemp, 1))
            {
                case ".":
                    strUpart = "圆";
                    break;
                case "0":
                    strUpart = "零";
                    break;
                case "1":
                    strUpart = "壹";
                    break;
                case "2":
                    strUpart = "贰";
                    break;
                case "3":
                    strUpart = "叁";
                    break;
                case "4":
                    strUpart = "肆";
                    break;
                case "5":
                    strUpart = "伍";
                    break;
                case "6":
                    strUpart = "陆";
                    break;
                case "7":
                    strUpart = "柒";
                    break;
                case "8":
                    strUpart = "捌";
                    break;
                case "9":
                    strUpart = "玖";
                    break;
            }

            switch (iTemp)
            {
                case 1:
                    strUpart = strUpart + "分";
                    break;
                case 2:
                    strUpart = strUpart + "角";
                    break;
                case 3:
                    strUpart = strUpart + "";
                    break;
                case 4:
                    strUpart = strUpart + "";
                    break;
                case 5:
                    strUpart = strUpart + "拾";
                    break;
                case 6:
                    strUpart = strUpart + "佰";
                    break;
                case 7:
                    strUpart = strUpart + "仟";
                    break;
                case 8:
                    strUpart = strUpart + "万";
                    break;
                case 9:
                    strUpart = strUpart + "拾";
                    break;
                case 10:
                    strUpart = strUpart + "佰";
                    break;
                case 11:
                    strUpart = strUpart + "仟";
                    break;
                case 12:
                    strUpart = strUpart + "亿";
                    break;
                case 13:
                    strUpart = strUpart + "拾";
                    break;
                case 14:
                    strUpart = strUpart + "佰";
                    break;
                case 15:
                    strUpart = strUpart + "仟";
                    break;
                case 16:
                    strUpart = strUpart + "万";
                    break;
                default:
                    strUpart = strUpart + "";
                    break;
            }

            strUpper = strUpart + strUpper;
            iTemp = iTemp + 1;
        }

        strUpper = strUpper.Replace("零拾", "零");
        strUpper = strUpper.Replace("零佰", "零");
        strUpper = strUpper.Replace("零仟", "零");
        strUpper = strUpper.Replace("零零零", "零");
        strUpper = strUpper.Replace("零零", "零");
        strUpper = strUpper.Replace("零角零分", "整");
        strUpper = strUpper.Replace("零分", "整");
        strUpper = strUpper.Replace("零角", "零");
        strUpper = strUpper.Replace("零亿零万零圆", "亿圆");
        strUpper = strUpper.Replace("亿零万零圆", "亿圆");
        strUpper = strUpper.Replace("零亿零万", "亿");
        strUpper = strUpper.Replace("零万零圆", "万圆");
        strUpper = strUpper.Replace("零亿", "亿");
        strUpper = strUpper.Replace("零万", "万");
        strUpper = strUpper.Replace("零圆", "圆");
        strUpper = strUpper.Replace("零零", "零");

        // 对壹圆以下的金额的处理
        if (strUpper.Substring(0, 1) == "圆")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "零")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "角")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "分")
        {
            strUpper = strUpper.Substring(1, strUpper.Length - 1);
        }
        if (strUpper.Substring(0, 1) == "整")
        {
            strUpper = "零圆整";
        }
        functionReturnValue = strUpper;

        if (IsNegative == true)
        {
            return "负" + functionReturnValue;
        }
        else
        {
            return functionReturnValue;
        }
    }

    protected void rpt_taizhang_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string userid = e.CommandArgument.ToString();
        if (cmd == "lbtn_open")
        {
            string sql = "update role_info set state='启用' where id=" + userid;
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
}