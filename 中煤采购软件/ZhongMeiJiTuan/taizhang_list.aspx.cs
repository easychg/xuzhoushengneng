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
using System.Data.SqlClient;

public partial class taizhang_list : System.Web.UI.Page
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
                ViewState["search"] = " 1=1";
                BinDDL();
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "login.aspx");
            }
        }
    }

    private void BinDDL()
    {
        string sql = "select * from jihualeixing";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_jihualeixing, ds.Tables[0], "mingcheng", "mingcheng", "--请选择--");
        sql = "select * from dianshangpingtai";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_dianshangpingtai, ds.Tables[0], "mingcheng", "mingcheng", "--请选择--");
        sql = "select * from fukuanfangshi";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_fukuanfangshi, ds.Tables[0], "mingcheng", "mingcheng", "--请选择--");
        sql = "select * from jiaohuodidian";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_jiaohuodidian, ds.Tables[0], "mingcheng", "mingcheng", "--请选择--");
        sql = "select * from wuzishuxing";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_wuzishuxing, ds.Tables[0], "mingcheng", "mingcheng", "--请选择--");
        sql = "select * from zijinlaiyuan";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_zijinlaiyuan, ds.Tables[0], "mingcheng", "mingcheng", "--请选择--");
    }

    private void BindInfo()
    {
        string sql = "select * from taizhang_info where " + ViewState["search"] + " and isdelete=0 order by taizhang_id desc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_taizhang.DataSource = ds.Tables[0];
        rpt_taizhang.DataBind();
        for (int i = 0; i < rpt_taizhang.Items.Count; i++) {
            Label userid = rpt_taizhang.Items[i].FindControl("lbl_user") as Label;
            string sqla = "select man_name from manager_info where man_id="+userid.Text;
            string resulta = DB.ExecuteSqlValue(sqla, null);
            if (resulta != "" && resulta != "no") {
                userid.Text = resulta;
            }
        }
        lbtn_delete.Visible = false;
        if (ViewState["userid"] != null)
        {
            if (ViewState["userid"].ToString() == "1") {
                lbtn_delete.Visible = true;
            }
            
        }
    }
    protected void lbtn_search_Click(object sender, EventArgs e)
    {
        string time1 = datemin.Value;
        string time2 = datemax.Value;
        string dianshangpingtai = ddl_dianshangpingtai.SelectedValue;
        string fukuanfangshi = ddl_fukuanfangshi.SelectedValue;
        string jiaohuodidian = ddl_jiaohuodidian.SelectedValue;
        string jihualeixing = ddl_jihualeixing.SelectedValue;
        string wuzishuxing = ddl_wuzishuxing.SelectedValue;
        string zijinlaiyuan = ddl_zijinlaiyuan.SelectedValue;
        string jihuabianhao = txtjihuabianhao.Value;
        string changshangmingcheng = txtchangshangmingcheng.Value;
        ViewState["search"] = " 1=1";
        if (time1.ToString().Trim() != "")
        {
            ViewState["search"] += " and convert(nvarchar(10),addtime,120) >='" + time1 + "' ";
        }
        if (time2.ToString().Trim() != "")
        {
            ViewState["search"] += " and convert(nvarchar(10),addtime,120) <='" + time2 + "'";
        }
        if (dianshangpingtai != "0")
        {
            ViewState["search"] += " and dianshangpingtai='" + dianshangpingtai + "'";
        }
        if (fukuanfangshi != "0")
        {
            ViewState["search"] += " and fukuanfangshi= '" + fukuanfangshi + "'";
        }
        if (jiaohuodidian != "0")
        {
            ViewState["search"] += " and jiaohuodidian= '" + jiaohuodidian + "'";
        }
        if (jihualeixing != "0")
        {
            ViewState["search"] += " and jihualeixing= '" + jihualeixing + "'";
        }
        if (wuzishuxing != "0")
        {
            ViewState["search"] += " and wuzishuxing= '" + wuzishuxing + "'";
        }
        if (zijinlaiyuan != "0")
        {
            ViewState["search"] += " and zijinlaiyuan= '" + zijinlaiyuan + "'";
        }
        if (jihuabianhao.Trim() != "") {
            ViewState["search"] += " and jihuabianhao= '" + jihuabianhao + "'";
        }
        if (changshangmingcheng.Trim() != "")
        {
            ViewState["search"] += " and gonghuochangshang like'%" + changshangmingcheng + "%'";
        }
        BindInfo();
    }
    protected void lbtn_daoru_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            FileUpload1.SaveAs(Server.MapPath("~/daoru/") + dt + FileUpload1.FileName);
            //导入数据
            DataTable dta=ExcelToTable(Server.MapPath("~/daoru/") + dt + FileUpload1.FileName);
            BindInfo();
            if (dta.Rows.Count > 0) {
                string file = Server.MapPath("~/template/taizhang.xlsx");
                string file2 = Server.MapPath("~/xiazai/taizhang.xlsx");
                TableToExcel(dta, file, file2);
                export(file2);//导出错误数据
            }
            
            
            
        }
        else {
            SystemTool.AlertShow(this, "请选择要导入的Excel");
        }
    }
    #region
    
    /// <summary>
    /// Excel导入成Datable
    /// </summary>
    /// <param name="file"></param>
    /// <returns>返回导入失败的数据</returns>
    protected DataTable ExcelToTable(string file)
    {
        string managerid = ViewState["userid"].ToString();
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
            string record = "";
            for (int i = sheet.FirstRowNum + 2; i <= sheet.LastRowNum; i++)
            {
                record = "";
                DataRow dr = dt.NewRow();
                //bool hasValue = false;
                string yuefen ="";
                string shiyongdanwei ="";
                string jihuabianhao  ="";
                string zijinlaiyuan ="";
                string jihualeixing  ="";
                string xunjiayuan  ="";
                string caigoubianhao  ="";
                string wuzishuxing  ="";
                string wuzibianma ="";
                string dingjiawuzimingcheng  ="";
                string dingjiaguigexinghao ="";
                string dingjiadanwei ="";
                string dingjiashuliang  ="0";
                string hanshuidanjia ="0";
                string hanshuijine  ="0";
                string shuilv ="0";
                string caigoufangshi  ="";
                string suoshuxieyihao ="";
                string dingdanbianhao  ="";
                string dinghuoyiju ="";
                string jiaohuodidian ="";
                string zhizaoshang  ="";
                string wuzipinpai  ="";
                string gonghuochangshang  ="";
                string dianshangpingtai  ="";
                string wangdianmingcheng  ="";
                string fukuanfangshi  ="";
                string changshangdianhua  ="";
                string dingdanriqi  ="";
                string jiaohuoqi  ="";
                string yingdaoshijian  ="";
                string shidaoshijian  ="";
                string shiyongshouming  ="";
                string zhibaoqi  ="";
                string qita  ="";
                string beizhu = "";
                for (int j = 0; j < 37;j++ )
                {
                    
                    #region 日期转换
                    if (j == 29)
                    {
                        //订单日期
                        try
                        {
                            var bbbb = sheet.GetRow(i).GetCell(j);
                            var b = (bbbb == null);
                            if (!b)
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd");
                                dingdanriqi = cccc;
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            record += "第" + i + "行" + j + "列："+ex.ToString()+"；";
                            //转换失败则不进行插入操作，但仍进行循环到导出表
                            isinsert = false;
                        }
                    }
                    if (j == 31)
                    {
                        //应到时间
                        try
                        {
                            var bbbb = sheet.GetRow(i).GetCell(j);
                            var b = (bbbb == null);
                            if (!b)
                            {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd");
                                yingdaoshijian = cccc;
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            record += "第" + i + "行" + j + "列：" + ex.ToString()+"；";
                            //转换失败则不进行插入操作，但仍进行循环到导出表
                            isinsert = false;
                        }
                    }
                    
                    if (j == 32)
                    {
                        //实到时间
                        try
                        {
                           var bbbb = sheet.GetRow(i).GetCell(j);
                           var b = (bbbb == null);
                            if (!b) {
                                var aaaa = sheet.GetRow(i).GetCell(j).ToString();
                                DateTime ddd = Convert.ToDateTime(aaaa);
                                string cccc = ddd.ToString("yyyy-MM-dd");
                                shidaoshijian = cccc;
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            record += "第" + i + "行" + j + "列：" + ex.ToString()+"；";
                            //转换失败则不进行插入操作，但仍进行循环到导出表
                            isinsert = false;
                        }
                    }
                    #endregion
                    dr[j] = sheet.GetRow(i).GetCell(j);
                    //dr[j] = GetValueType(sheet.GetRow(i).GetCell(j));
                    #region 限制条件查询

                    if (j == 27)
                    {
                        string ss = "";
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b) {
                                
                            ss = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        //付款方式
                        string sqlck2 = @"if exists(select isxianzhi from fukuanfangshi where isxianzhi=1) select fukuanfangshi_id from fukuanfangshi where isxianzhi=1 and mingcheng=@mingcheng else select sum(0) from fukuanfangshi";
                        SqlParameter[] parsa = new SqlParameter[] { 
                        new SqlParameter("@mingcheng", System.Data.SqlDbType.NVarChar){Value=ss}
                        };
                        string resultck2 = DB.ExecuteSqlValue(sqlck2, parsa);
                        if (resultck2 == "0")
                        {
                            record += "第" + i + "行" + j + "列：限制" + ss + "；" ;
                            //无限制条件，不加限制
                            isinsert = false;
                        }
                        //string sqlck = "select shiyongdanwei_id from shiyongdanwei where mingcheng='" + sheet.GetRow(i).GetCell(j) + "'";
                        //string resultck = DB.ExecuteSqlValue(sqlck, null);
                        //if (resultck == "" || resultck == "no")
                        //{
                        //    //数据不符，过滤
                        //    isinsert = false;
                        //}
                    }
                    if (j == 8)
                    {
                        string ss = "";
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {

                            ss = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        //物资属性
                        string sqlck2 = @"if exists(select isxianzhi from wuzishuxing where isxianzhi=1) select wuzishuxing_id from wuzishuxing where isxianzhi=1 and mingcheng=@mingcheng else select sum(0) from wuzishuxing";
                        SqlParameter[] parsa = new SqlParameter[] { 
                        new SqlParameter("@mingcheng", System.Data.SqlDbType.NVarChar){Value=ss}
                        };
                        string resultck2 = DB.ExecuteSqlValue(sqlck2, parsa);
                        if (resultck2 == "0")
                        {
                            record += "第" + i + "行" + j + "列：限制" + ss + "；";
                            //无限制条件，不加限制
                            isinsert = false;
                        }
                    }
                    if (j == 25)
                    {
                        string ss = "";
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {

                            ss = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        //电商平台
                        string sqlck2 = @"if exists(select isxianzhi from dianshangpingtai where isxianzhi=1) select dianshangpingtai_id from dianshangpingtai where isxianzhi=1 and mingcheng=@mingcheng else select sum(0) from dianshangpingtai";
                        SqlParameter[] parsa = new SqlParameter[] { 
                        new SqlParameter("@mingcheng", System.Data.SqlDbType.NVarChar){Value=ss}
                        };
                        string resultck2 = DB.ExecuteSqlValue(sqlck2, parsa);
                        if (resultck2 == "0")
                        {
                            record += "第" + i + "行" + j + "列：限制" + ss + "；";
                            //无限制条件，不加限制
                            isinsert = false;
                        }
                    }
                    if (j == 4)
                    {
                        string ss = "";
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {

                            ss = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        //资金来源
                        string sqlck2 = @"if exists(select isxianzhi from zijinlaiyuan where isxianzhi=1) select zijinlaiyuan_id from zijinlaiyuan where isxianzhi=1 and mingcheng=@mingcheng else select sum(0) from zijinlaiyuan";
                        SqlParameter[] parsa = new SqlParameter[] { 
                        new SqlParameter("@mingcheng", System.Data.SqlDbType.NVarChar){Value=ss}
                        };
                        string resultck2 = DB.ExecuteSqlValue(sqlck2, parsa);
                        if (resultck2 == "0")
                        {
                            record += "第" + i + "行" + j + "列：限制" + ss + "；";
                            //无限制条件，不加限制
                            isinsert = false;
                        }
                    }
                    if (j == 5)
                    {
                        string ss = "";
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {

                            ss = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        //计划类型
                        string sqlck2 = @"if exists(select isxianzhi from jihualeixing where isxianzhi=1) select jihualeixing_id from jihualeixing where isxianzhi=1 and mingcheng=@mingcheng else select sum(0) from jihualeixing";
                        SqlParameter[] parsa = new SqlParameter[] { 
                        new SqlParameter("@mingcheng", System.Data.SqlDbType.NVarChar){Value=ss}
                        };
                        string resultck2 = DB.ExecuteSqlValue(sqlck2, parsa);
                        if (resultck2 == "0")
                        {
                            record += "第" + i + "行" + j + "列：限制" + ss + "；";
                            //无限制条件，不加限制
                            isinsert = false;
                        }
                    }
                    if (j == 21)
                    {
                        string ss = "";
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {

                            ss = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        //交货地点
                        string sqlck2 = @"if exists(select isxianzhi from jiaohuodidian where isxianzhi=1) select jiaohuodidian_id from jiaohuodidian where isxianzhi=1 and mingcheng=@mingcheng else select sum(0) from jiaohuodidian";
                        SqlParameter[] parsa = new SqlParameter[] { 
                        new SqlParameter("@mingcheng", System.Data.SqlDbType.NVarChar){Value=ss}
                        };
                        string resultck2 = DB.ExecuteSqlValue(sqlck2, parsa);
                        if (resultck2 == "0")
                        {
                            record += "第" + i + "行" + j + "列：限制" + ss + "；";
                            //无限制条件，不加限制
                            isinsert = false;
                        }
                    }
                    #endregion
                    //if (dr[j] != null && dr[j].ToString() != string.Empty)
                    //{
                    //    hasValue = true;
                    //}
                    #region 赋值
                    if (j == 1) {
                        
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {

                            yuefen = sheet.GetRow(i).GetCell(j).ToString();
                        }
                        
                    }
                    
                    if (j == 2)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            shiyongdanwei = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 3)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            jihuabianhao = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 4)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            zijinlaiyuan = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 5)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            jihualeixing = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 6)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            xunjiayuan = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 7)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            caigoubianhao = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 8)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            wuzishuxing = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 9)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            wuzibianma = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 10)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dingjiawuzimingcheng = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 11)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dingjiaguigexinghao = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 12)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dingjiadanwei = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 13)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dingjiashuliang = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 14)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            hanshuidanjia = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 15)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            hanshuijine = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 16)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            shuilv = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 17)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            caigoufangshi = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 18)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            suoshuxieyihao = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 19)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dingdanbianhao = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 20)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dinghuoyiju = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 21)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            jiaohuodidian = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 22)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            zhizaoshang = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 23)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            wuzipinpai = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 24)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            gonghuochangshang = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 25)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            dianshangpingtai = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 26)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            wangdianmingcheng = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 27)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            fukuanfangshi = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 28)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            changshangdianhua = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    //if (j == 28)
                    //{
                    //    if (sheet.GetRow(i).GetCell(j) != null)
                    //    {
                    //        dingdanriqi = sheet.GetRow(i).GetCell(j).ToString();
                    //    }
                    //}
                    if (j == 30)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            jiaohuoqi = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    //if (j == 30)
                    //{
                    //    if (sheet.GetRow(i).GetCell(j) != null)
                    //    {
                    //        yingdaoshijian = sheet.GetRow(i).GetCell(j).ToString();
                    //    }
                    //}
                    //if (j == 31)
                    //{
                    //    if (sheet.GetRow(i).GetCell(j) != null)
                    //    {
                    //        shidaoshijian = sheet.GetRow(i).GetCell(j).ToString();
                    //    }
                    //}
                    if (j == 33)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            shiyongshouming = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 34)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            zhibaoqi = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    if (j == 35)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            qita = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    
                    if (j == 36)
                    {
                        var bbbb = sheet.GetRow(i).GetCell(j);
                        var b = (bbbb == null);
                        if (!b)
                        {
                            beizhu = sheet.GetRow(i).GetCell(j).ToString();
                        }
                    }
                    #endregion
                }
                record += "第" + i + "行插入" + isinsert + "；";
                try
                {
                    
                    string sql = "insert into taizhang_info( yuefen,shiyongdanwei,jihuabianhao,zijinlaiyuan,jihualeixing,xunjiayuan,caigoubianhao,wuzishuxing,wuzibianma,dingjiawuzimingcheng,dingjiaguigexinghao,dingjiadanwei,dingjiashuliang,hanshuidanjia,hanshuijine,shuilv,caigoufangshi,suoshuxieyihao,dingdanbianhao,dinghuoyiju,jiaohuodidian,zhizaoshang,wuzipinpai,gonghuochangshang,dianshangpingtai,wangdianmingcheng,fukuanfangshi,changshangdianhua,dingdanriqi,jiaohuoqi,yingdaoshijian,shidaoshijian,shiyongshouming,zhibaoqi,qita,beizhu,manager_id) values (@yuefen,@shiyongdanwei,@jihuabianhao,@zijinlaiyuan,@jihualeixing,@xunjiayuan,@caigoubianhao,@wuzishuxing,@wuzibianma,@dingjiawuzimingcheng,@dingjiaguigexinghao,@dingjiadanwei,@dingjiashuliang,@hanshuidanjia,@hanshuijine,@shuilv,@caigoufangshi,@suoshuxieyihao,@dingdanbianhao,@dinghuoyiju,@jiaohuodidian,@zhizaoshang,@wuzipinpai,@gonghuochangshang,@dianshangpingtai,@wangdianmingcheng,@fukuanfangshi,@changshangdianhua,@dingdanriqi,@jiaohuoqi,@yingdaoshijian,@shidaoshijian,@shiyongshouming,@zhibaoqi,@qita,@beizhu,@manager_id)";
                    SqlParameter[] pars = new SqlParameter[] { 
                        new SqlParameter("@yuefen", System.Data.SqlDbType.NVarChar){Value=yuefen},
                        new SqlParameter("@shiyongdanwei", System.Data.SqlDbType.NVarChar){Value=shiyongdanwei},
                        new SqlParameter("@jihuabianhao", System.Data.SqlDbType.NVarChar){Value=jihuabianhao},
                        new SqlParameter("@zijinlaiyuan", System.Data.SqlDbType.NVarChar){Value=zijinlaiyuan},
                        new SqlParameter("@jihualeixing", System.Data.SqlDbType.NVarChar){Value=jihualeixing},
                        new SqlParameter("@xunjiayuan", System.Data.SqlDbType.NVarChar){Value=xunjiayuan},
                        new SqlParameter("@caigoubianhao", System.Data.SqlDbType.NVarChar){Value=caigoubianhao},
                        new SqlParameter("@wuzishuxing", System.Data.SqlDbType.NVarChar){Value=wuzishuxing},
                        new SqlParameter("@wuzibianma", System.Data.SqlDbType.NVarChar){Value=wuzibianma},
                        new SqlParameter("@dingjiawuzimingcheng", System.Data.SqlDbType.NVarChar){Value=dingjiawuzimingcheng},
                        new SqlParameter("@dingjiaguigexinghao", System.Data.SqlDbType.NVarChar){Value=dingjiaguigexinghao},
                        new SqlParameter("@dingjiadanwei", System.Data.SqlDbType.NVarChar){Value=dingjiadanwei},
                        new SqlParameter("@dingjiashuliang", System.Data.SqlDbType.Decimal){Value=dingjiashuliang},
                        new SqlParameter("@hanshuidanjia", System.Data.SqlDbType.Decimal){Value=hanshuidanjia},
                        new SqlParameter("@hanshuijine", System.Data.SqlDbType.Decimal){Value=hanshuijine},
                        new SqlParameter("@shuilv", System.Data.SqlDbType.Decimal){Value=shuilv},
                        new SqlParameter("@caigoufangshi", System.Data.SqlDbType.NVarChar){Value=caigoufangshi},
                        new SqlParameter("@suoshuxieyihao", System.Data.SqlDbType.NVarChar){Value=suoshuxieyihao},
                        new SqlParameter("@dingdanbianhao", System.Data.SqlDbType.NVarChar){Value=dingdanbianhao},
                        new SqlParameter("@dinghuoyiju", System.Data.SqlDbType.NVarChar){Value=dinghuoyiju},
                        new SqlParameter("@jiaohuodidian", System.Data.SqlDbType.NVarChar){Value=jiaohuodidian},
                        new SqlParameter("@zhizaoshang", System.Data.SqlDbType.NVarChar){Value=zhizaoshang},
                        new SqlParameter("@wuzipinpai", System.Data.SqlDbType.NVarChar){Value=wuzipinpai},
                        new SqlParameter("@gonghuochangshang", System.Data.SqlDbType.NVarChar){Value=gonghuochangshang},
                        new SqlParameter("@dianshangpingtai", System.Data.SqlDbType.NVarChar){Value=dianshangpingtai},
                        new SqlParameter("@wangdianmingcheng", System.Data.SqlDbType.NVarChar){Value=wangdianmingcheng},
                        new SqlParameter("@fukuanfangshi", System.Data.SqlDbType.NVarChar){Value=fukuanfangshi},
                        new SqlParameter("@changshangdianhua", System.Data.SqlDbType.NVarChar){Value=changshangdianhua},
                        new SqlParameter("@dingdanriqi", System.Data.SqlDbType.NVarChar){Value=dingdanriqi},
                        new SqlParameter("@jiaohuoqi", System.Data.SqlDbType.NVarChar){Value=jiaohuoqi},
                        new SqlParameter("@yingdaoshijian", System.Data.SqlDbType.NVarChar){Value=yingdaoshijian},
                        new SqlParameter("@shidaoshijian", System.Data.SqlDbType.NVarChar){Value=shidaoshijian},
                        new SqlParameter("@shiyongshouming", System.Data.SqlDbType.NVarChar){Value=shiyongshouming},
                        new SqlParameter("@zhibaoqi", System.Data.SqlDbType.NVarChar){Value=zhibaoqi},
                        new SqlParameter("@qita", System.Data.SqlDbType.NVarChar){Value=qita},
                        new SqlParameter("@beizhu", System.Data.SqlDbType.NVarChar){Value=beizhu},
                        new SqlParameter("@manager_id", System.Data.SqlDbType.NVarChar){Value=managerid}
                    };
                    //是否插入，不插入则添加到导出表
                    if (isinsert)
                    {
                        int m = DB.ExecuteSql(sql, pars);
                    }
                    else {
                        dt.Rows.Add(dr);
                        isinsert = true;
                    }
                    
                }
                catch (Exception ex) {
                    record += "第" + i + "行插入状态" + ex.ToString() + "；";
                    dt.Rows.Add(dr);
                    isinsert = true;
                }
                string sqlrecord = "insert into daoru_record (record) values(@record)";
                SqlParameter[] parsm = new SqlParameter[] { 
                        new SqlParameter("@record", System.Data.SqlDbType.NVarChar){Value=record}
                };
                try
                {
                    int r = DB.ExecuteSql(sqlrecord, parsm);
                }
                catch (Exception e) { 
                
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
    public static void TableToExcel(DataTable dt, string file,string file2)
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
            for (int j = 0; j < 37; j++)
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
    protected void lbtn_delete_Click(object sender, EventArgs e)
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
            SystemTool.AlertShow(this, "请选择需要删除的数据");
            return;
        }
        string sql = "update taizhang_info set isdelete=1 where taizhang_id in(" + ids + ")";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            BindInfo();
            SystemTool.AlertShow(this, "删除成功");
        }
        else
        {
            SystemTool.AlertShow(this, "删除失败");
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
        string TempletFileName = Server.MapPath("~/template/tchangxie.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/changxie"+dts+".xlsx");
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
            mySheet.Cells[4, 9] = "订单日期：" + ds.Tables[0].Rows[0]["dingdanriqi"].ToString();
            mySheet.Cells[5, 1] = "使用单位：" + ds.Tables[0].Rows[0]["shiyongdanwei"].ToString();
            mySheet.Cells[5, 9] = "所属合同（协议）号：" + ds.Tables[0].Rows[0]["suoshuxieyihao"].ToString();
            mySheet.Cells[6, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            mySheet.Cells[6, 9] = "订货依据：" + ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
            
        }
        decimal zongjia = 0;
        string SYDW_JE = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
            mySheet.Cells[8+i, 1] = (i+1).ToString();
            mySheet.Cells[8+i, 2] = ds.Tables[0].Rows[i]["shiyongdanwei"].ToString();
            mySheet.Cells[8+i, 3] = ds.Tables[0].Rows[i]["wuzibianma"].ToString();
            mySheet.Cells[8+i, 4] = ds.Tables[0].Rows[i]["dingjiawuzimingcheng"].ToString();
            mySheet.Cells[8+i, 5] = ds.Tables[0].Rows[i]["dingjiaguigexinghao"].ToString();
            mySheet.Cells[8+i, 6] = ds.Tables[0].Rows[i]["dingjiadanwei"].ToString();
            mySheet.Cells[8+i, 7] = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString();
            //decimal shuliang = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["dingjiashuliang"].ToString());
            //decimal danjia = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuidanjia"].ToString());
            //zongjia += (danjia * shuliang);
            decimal hanshuijine = ds.Tables[0].Rows[i]["hanshuijine"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuijine"].ToString());
            zongjia += hanshuijine;
            mySheet.Cells[8+i, 8] = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString();
            mySheet.Cells[8+i, 9] = ds.Tables[0].Rows[i]["hanshuijine"].ToString();
            mySheet.Cells[8+i, 10] = ds.Tables[0].Rows[i]["yingdaoshijian"].ToString();
            mySheet.Cells[8+i, 11] = ds.Tables[0].Rows[i]["jiaohuodidian"].ToString();
            mySheet.Cells[8+i, 12] = ds.Tables[0].Rows[i]["wuzipinpai"].ToString();
            mySheet.Cells[8 + i, 13] = ds.Tables[0].Rows[i]["beizhu"].ToString();
            SYDW_JE += ds.Tables[0].Rows[i]["shiyongdanwei"].ToString() + "：" + ds.Tables[0].Rows[i]["hanshuijine"].ToString() + "元    ";
        }
        if (ds.Tables[0].Rows.Count > 0) {
            mySheet.Cells[10 + h, 1] = "付款方式：" + ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
            mySheet.Cells[10 + h, 7] = "合计(含税" + ds.Tables[0].Rows[0]["shuilv"].ToString() + "%)";
            mySheet.Cells[10 + h, 9] = zongjia.ToString();
            mySheet.Cells[10 + h, 10] = "大写：" + MoneyToChinese(zongjia.ToString());
            mySheet.Cells[11 + h, 1] = SYDW_JE;
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
        string TempletFileName = Server.MapPath("~/template/tputong.xlsx");//路径 
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
            mySheet.Cells[3, 10] = "订单日期：" + ds.Tables[0].Rows[0]["dingdanriqi"].ToString();
            mySheet.Cells[4, 1] = "订单编号：" + ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
            mySheet.Cells[4, 10] = "订货依据：" + ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
            mySheet.Cells[5, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            mySheet.Cells[5, 10] = "采购方式：" + ds.Tables[0].Rows[0]["caigoufangshi"].ToString();
            //mySheet.Cells[6, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            //mySheet.Cells[6, 9] = "采购方式：" + ds.Tables[0].Rows[0]["caigoufangshi"].ToString();

        }
        decimal zongjia = 0;
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            mySheet.Cells[7 + i, 1] = (i + 1).ToString();
            mySheet.Cells[7 + i, 2] = ds.Tables[0].Rows[i]["shiyongdanwei"].ToString();
            mySheet.Cells[7 + i, 3] = ds.Tables[0].Rows[i]["wuzibianma"].ToString();
            mySheet.Cells[7 + i, 4] = ds.Tables[0].Rows[i]["dingjiawuzimingcheng"].ToString();
            mySheet.Cells[7 + i, 5] = ds.Tables[0].Rows[i]["dingjiaguigexinghao"].ToString();
            mySheet.Cells[7 + i, 6] = ds.Tables[0].Rows[i]["dingjiadanwei"].ToString();
            mySheet.Cells[7 + i, 7] = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString();
            mySheet.Cells[7 + i, 8] = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString();
            //decimal shuliang = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["dingjiashuliang"].ToString());
            //decimal danjia = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuidanjia"].ToString());
            //zongjia += (danjia * shuliang);
            //mySheet.Cells[7 + i, 8] = (shuliang * danjia).ToString();
            decimal hanshuijine = ds.Tables[0].Rows[i]["hanshuijine"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuijine"].ToString());
            zongjia += hanshuijine;
            mySheet.Cells[7 + i, 9] = ds.Tables[0].Rows[i]["hanshuijine"].ToString();
            mySheet.Cells[7 + i, 10] = ds.Tables[0].Rows[i]["yingdaoshijian"].ToString();
            mySheet.Cells[7 + i, 11] = ds.Tables[0].Rows[i]["jiaohuodidian"].ToString();
            mySheet.Cells[7 + i, 12] = ds.Tables[0].Rows[i]["wuzipinpai"].ToString();
            mySheet.Cells[7 + i, 13] = ds.Tables[0].Rows[i]["beizhu"].ToString();
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[9 + h, 1] = "付款方式：" + ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
            mySheet.Cells[9 + h, 7] = "合计(含税" + ds.Tables[0].Rows[0]["shuilv"].ToString() + "%)";
            mySheet.Cells[9 + h, 9] = zongjia.ToString();
            mySheet.Cells[9 + h, 10] = "大写：" + MoneyToChinese(zongjia.ToString());
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
        string TempletFileName = Server.MapPath("~/template/ttwanggou.xlsx");//路径 
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
            mySheet.Cells[4, 1] = "订单编号：" + ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
            mySheet.Cells[5, 1] = "电商平台："+ds.Tables[0].Rows[0]["dianshangpingtai"].ToString();
            mySheet.Cells[5, 10] = "订单日期：" + ds.Tables[0].Rows[0]["dingdanriqi"].ToString();
            mySheet.Cells[6, 1] = "网店名称：" +ds.Tables[0].Rows[0]["wangdianmingcheng"].ToString();
            mySheet.Cells[6, 10] = "订货依据：" + ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
            mySheet.Cells[7, 1] = "供应商名称：" + ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
            mySheet.Cells[7, 10] = "询价方式：" +ds.Tables[0].Rows[0]["caigoufangshi"].ToString();

        }
        decimal zongjia = 0;
        string SYDW_JE = "";
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            mySheet.Cells[9 + i, 1] = (i + 1).ToString();
            mySheet.Cells[9 + i, 2] = ds.Tables[0].Rows[i]["shiyongdanwei"].ToString();
            mySheet.Cells[9 + i, 3] = ds.Tables[0].Rows[i]["wuzibianma"].ToString();
            mySheet.Cells[9 + i, 4] = ds.Tables[0].Rows[i]["dingjiawuzimingcheng"].ToString();
            mySheet.Cells[9 + i, 5] = ds.Tables[0].Rows[i]["dingjiaguigexinghao"].ToString();
            mySheet.Cells[9 + i, 6] = ds.Tables[0].Rows[i]["dingjiadanwei"].ToString();
            mySheet.Cells[9 + i, 7] = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString();
            //decimal shuliang = ds.Tables[0].Rows[i]["dingjiashuliang"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["dingjiashuliang"].ToString());
            //decimal danjia = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuidanjia"].ToString());
            //zongjia += (danjia * shuliang);
            decimal hanshuijine = ds.Tables[0].Rows[i]["hanshuijine"].ToString() == "" ? 0 : Convert.ToDecimal(ds.Tables[0].Rows[i]["hanshuijine"].ToString());
            zongjia += hanshuijine;
            mySheet.Cells[9 + i, 8] = ds.Tables[0].Rows[i]["hanshuidanjia"].ToString();
            mySheet.Cells[9 + i, 9] = ds.Tables[0].Rows[i]["hanshuijine"].ToString();
            mySheet.Cells[9 + i, 10] = ds.Tables[0].Rows[i]["yingdaoshijian"].ToString();
            mySheet.Cells[9 + i, 11] = ds.Tables[0].Rows[i]["jiaohuodidian"].ToString();
            mySheet.Cells[9 + i, 12] = ds.Tables[0].Rows[i]["wuzipinpai"].ToString();
            mySheet.Cells[9 + i, 13] = ds.Tables[0].Rows[i]["beizhu"].ToString();
            SYDW_JE += ds.Tables[0].Rows[i]["shiyongdanwei"].ToString()+"：" + ds.Tables[0].Rows[i]["hanshuijine"].ToString()+"元    ";
        }
        if (ds.Tables[0].Rows.Count > 0)
        {
            mySheet.Cells[11 + h, 1] = "付款方式："+ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
            mySheet.Cells[11 + h, 9] = zongjia.ToString();
            mySheet.Cells[11 + h, 10] = "大写：（含" + ds.Tables[0].Rows[0]["shuilv"].ToString() + "%的增值税）" + MoneyToChinese(zongjia.ToString());
            mySheet.Cells[12 + h, 1] = SYDW_JE;
            mySheet.Cells[13 + h, 1] = "询价员：" + ds.Tables[0].Rows[0]["xunjiayuan"].ToString() + "                                      询价科长：                                   副经理： 										";
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
        //string cmd = e.CommandName.ToString();
        //string userid = e.CommandArgument.ToString();
        //if (cmd == "lbtn_open")
        //{
        //    string sql = "update role_info set state='启用' where id=" + userid;
        //    int result = DB.ExecuteSql(sql, null);
        //    if (result > 0)
        //    {
        //        SystemTool.AlertShow(this, "保存成功");
        //        BindInfo();
        //    }
        //    else
        //    {
        //        SystemTool.AlertShow(this, "保存失败");
        //    }
        //}
    }
}