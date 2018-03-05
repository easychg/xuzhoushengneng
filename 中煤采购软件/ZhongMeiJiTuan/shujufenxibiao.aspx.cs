using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class shujufenxibiao : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                string dt1 = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                string dt2 = DateTime.Now.ToString("yyyy-MM-dd");
                datemin.Value = dt1;
                datemax.Value = dt2;
                ViewState["search"] = " dingdanriqi>='"+dt1+"' and dingdanriqi<='"+dt2+"'";
                BindInfo();
                //表头
                string sql = "select count(taizhang_id) from taizhang_info where jihualeixing='应急采购' and " + ViewState["search"];
                string result = DB.ExecuteSqlValue(sql, null);
                if (result == "" || result == "no")
                {
                    result = "0";
                }
                sql = "select count(taizhang_id) from taizhang_info where " + ViewState["search"];
                string resulta = DB.ExecuteSqlValue(sql, null);
                if (resulta == "" || resulta == "no")
                {
                    resulta = "0";
                }
                divheader.InnerHtml = dt1 + "--" + dt2 + "（单位：元） 共实施各类采购" + resulta + "次，其中应急采购" + result + "次";
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }
    protected void lbtn_search_Click(object sender, EventArgs e) {
        string dt1 = datemin.Value;
        string dt2 = datemax.Value;
        if (dt1 == "") {
            SystemTool.AlertShow(this, "开始时间不能为空");
            return;
        }
        if (dt2 == "") {
            SystemTool.AlertShow(this, "结束时间不能为空");
            return;
        }
        ViewState["search"] = " dingdanriqi>='" + dt1 + "' and dingdanriqi<='" + dt2 + "'";
        BindInfo();
        //表头
        string sql = "select count(taizhang_id) from taizhang_info where jihualeixing='应急采购' and " + ViewState["search"];
        string result = DB.ExecuteSqlValue(sql, null);
        if (result == "" || result == "no")
        {
            result = "0";
        }
        sql = "select count(taizhang_id) from taizhang_info where " + ViewState["search"];
        string resulta = DB.ExecuteSqlValue(sql, null);
        if (resulta == "" || resulta == "no")
        {
            resulta = "0";
        }
        divheader.InnerHtml = dt1 + "--" + dt2 + "（单位：元） 共实施各类采购" + resulta + "次，其中应急采购" + result + "次";
    }
    protected void lbtn_daochu_Click(object sender, EventArgs e) {

        //模板文件  
        string TempletFileName = Server.MapPath("~/template/tshujufenxi.xlsx");//路径 
        //导出文件  
        string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        string ReportFileName = Server.MapPath("~/xiazai/sujufenxi" + dts + ".xlsx");
        Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
        object oMissing = System.Reflection.Missing.Value;
        myExcel.Application.Workbooks.Open(TempletFileName, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);
        Microsoft.Office.Interop.Excel.Workbook myBook = myExcel.Workbooks[1];
        Microsoft.Office.Interop.Excel.Worksheet mySheet = (Microsoft.Office.Interop.Excel.Worksheet)myBook.Worksheets[1];
        DataTable dtcaigoufangshi = Caigoufangshi();
        DataTable dtjihualeixing = Jihualeixing();
        DataTable dtwuzishuxing = Wuzishuxing();
        for (int i = 0; i < dtcaigoufangshi.Rows.Count; i++) {
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "公开招标") {
                mySheet.Cells[3, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[3, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[3, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[3, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[3, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[3, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[3, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[3, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[3, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[3, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "邀请招标")
            {
                mySheet.Cells[4, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[4, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[4, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[4, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[4, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[4, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[4, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[4, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[4, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[4, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "集团长协")
            {
                mySheet.Cells[5, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[5, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[5, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[5, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[5, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[5, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[5, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[5, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[5, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[5, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "公司长协")
            {
                mySheet.Cells[6, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[6, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[6, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[6, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[6, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[6, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[6, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[6, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[6, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[6, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "公开询价")
            {
                mySheet.Cells[7, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[7, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[7, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[7, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[7, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[7, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[7, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[7, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[7, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[7, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "固定范围询比价")
            {
                mySheet.Cells[8, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[8, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[8, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[8, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[8, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[8, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[8, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[8, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[8, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[8, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "单一来源")
            {
                mySheet.Cells[9, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[9, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[9, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[9, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[9, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[9, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[9, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[9, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[9, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[9, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "实地询价")
            {
                mySheet.Cells[10, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[10, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[10, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[10, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[10, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[10, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[10, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[10, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[10, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[10, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "电商公开")
            {
                mySheet.Cells[11, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[11, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[11, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[11, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[11, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[11, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[11, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[11, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[11, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[11, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
            if (dtcaigoufangshi.Rows[i]["caigoufangshi"].ToString() == "总计")
            {
                mySheet.Cells[13, 2] = dtcaigoufangshi.Rows[i]["五家沟"].ToString();
                mySheet.Cells[13, 3] = dtcaigoufangshi.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[13, 4] = dtcaigoufangshi.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[13, 5] = dtcaigoufangshi.Rows[i]["动力中心"].ToString();
                mySheet.Cells[13, 6] = dtcaigoufangshi.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[13, 7] = dtcaigoufangshi.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[13, 8] = dtcaigoufangshi.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[13, 9] = dtcaigoufangshi.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[13, 10] = dtcaigoufangshi.Rows[i]["其它"].ToString();
                mySheet.Cells[13, 11] = dtcaigoufangshi.Rows[i]["合计"].ToString();
            }
        }
        for (int i = 0; i < dtjihualeixing.Rows.Count; i++)
        {
            if (dtjihualeixing.Rows[i]["jihualeixing"].ToString() == "应急采购" || dtjihualeixing.Rows[i]["jihualeixing"].ToString() == "应急")
            {
                mySheet.Cells[16, 2] = dtjihualeixing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[16, 3] = dtjihualeixing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[16, 4] = dtjihualeixing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[16, 5] = dtjihualeixing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[16, 6] = dtjihualeixing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[16, 7] = dtjihualeixing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[16, 8] = dtjihualeixing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[16, 9] = dtjihualeixing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[16, 10] = dtjihualeixing.Rows[i]["其它"].ToString();
                mySheet.Cells[16, 11] = dtjihualeixing.Rows[i]["合计"].ToString();
            }
            if (dtjihualeixing.Rows[i]["jihualeixing"].ToString() == "常规采购" || dtjihualeixing.Rows[i]["jihualeixing"].ToString() == "常规")
            {
                mySheet.Cells[17, 2] = dtjihualeixing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[17, 3] = dtjihualeixing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[17, 4] = dtjihualeixing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[17, 5] = dtjihualeixing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[17, 6] = dtjihualeixing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[17, 7] = dtjihualeixing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[17, 8] = dtjihualeixing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[17, 9] = dtjihualeixing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[17, 10] = dtjihualeixing.Rows[i]["其它"].ToString();
                mySheet.Cells[17, 11] = dtjihualeixing.Rows[i]["合计"].ToString();
            }
            if (dtjihualeixing.Rows[i]["jihualeixing"].ToString() == "总计")
            {
                mySheet.Cells[18, 2] = dtjihualeixing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[18, 3] = dtjihualeixing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[18, 4] = dtjihualeixing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[18, 5] = dtjihualeixing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[18, 6] = dtjihualeixing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[18, 7] = dtjihualeixing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[18, 8] = dtjihualeixing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[18, 9] = dtjihualeixing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[18, 10] = dtjihualeixing.Rows[i]["其它"].ToString();
                mySheet.Cells[18, 11] = dtjihualeixing.Rows[i]["合计"].ToString();
            }
        }
        for (int i = 0; i < dtwuzishuxing.Rows.Count; i++)
        {
            if (dtwuzishuxing.Rows[i]["wuzishuxing"].ToString() == "设备")
            {
                mySheet.Cells[21, 2] = dtwuzishuxing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[21, 3] = dtwuzishuxing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[21, 4] = dtwuzishuxing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[21, 5] = dtwuzishuxing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[21, 6] = dtwuzishuxing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[21, 7] = dtwuzishuxing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[21, 8] = dtwuzishuxing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[21, 9] = dtwuzishuxing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[21, 10] = dtwuzishuxing.Rows[i]["其它"].ToString();
                mySheet.Cells[21, 11] = dtwuzishuxing.Rows[i]["合计"].ToString();
            }
            if (dtwuzishuxing.Rows[i]["wuzishuxing"].ToString() == "配件")
            {
                mySheet.Cells[22, 2] = dtwuzishuxing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[22, 3] = dtwuzishuxing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[22, 4] = dtwuzishuxing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[22, 5] = dtwuzishuxing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[22, 6] = dtwuzishuxing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[22, 7] = dtwuzishuxing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[22, 8] = dtwuzishuxing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[22, 9] = dtwuzishuxing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[22, 10] = dtwuzishuxing.Rows[i]["其它"].ToString();
                mySheet.Cells[22, 11] = dtwuzishuxing.Rows[i]["合计"].ToString();
            }
            if (dtwuzishuxing.Rows[i]["wuzishuxing"].ToString() == "金属材料")
            {
                mySheet.Cells[23, 2] = dtwuzishuxing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[23, 3] = dtwuzishuxing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[23, 4] = dtwuzishuxing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[23, 5] = dtwuzishuxing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[23, 6] = dtwuzishuxing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[23, 7] = dtwuzishuxing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[23, 8] = dtwuzishuxing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[23, 9] = dtwuzishuxing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[23, 10] = dtwuzishuxing.Rows[i]["其它"].ToString();
                mySheet.Cells[23, 11] = dtwuzishuxing.Rows[i]["合计"].ToString();
            }
            if (dtwuzishuxing.Rows[i]["wuzishuxing"].ToString() == "非金属材料")
            {
                mySheet.Cells[24, 2] = dtwuzishuxing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[24, 3] = dtwuzishuxing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[24, 4] = dtwuzishuxing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[24, 5] = dtwuzishuxing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[24, 6] = dtwuzishuxing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[24, 7] = dtwuzishuxing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[24, 8] = dtwuzishuxing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[24, 9] = dtwuzishuxing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[24, 10] = dtwuzishuxing.Rows[i]["其它"].ToString();
                mySheet.Cells[24, 11] = dtwuzishuxing.Rows[i]["合计"].ToString();
            }
            if (dtwuzishuxing.Rows[i]["wuzishuxing"].ToString() == "总计")
            {
                mySheet.Cells[25, 2] = dtwuzishuxing.Rows[i]["五家沟"].ToString();
                mySheet.Cells[25, 3] = dtwuzishuxing.Rows[i]["南阳坡"].ToString();
                mySheet.Cells[25, 4] = dtwuzishuxing.Rows[i]["元宝湾"].ToString();
                mySheet.Cells[25, 5] = dtwuzishuxing.Rows[i]["动力中心"].ToString();
                mySheet.Cells[25, 6] = dtwuzishuxing.Rows[i]["洗运中心"].ToString();
                mySheet.Cells[25, 7] = dtwuzishuxing.Rows[i]["炫昂建材"].ToString();
                mySheet.Cells[25, 8] = dtwuzishuxing.Rows[i]["永皓电厂"].ToString();
                mySheet.Cells[25, 9] = dtwuzishuxing.Rows[i]["赤钰冶金"].ToString();
                mySheet.Cells[25, 10] = dtwuzishuxing.Rows[i]["其它"].ToString();
                mySheet.Cells[25, 11] = dtwuzishuxing.Rows[i]["合计"].ToString();
            }
        }
        //表头
        string sql = "select count(taizhang_id) from taizhang_info where jihualeixing like'应急%' and " + ViewState["search"];
        string result = DB.ExecuteSqlValue(sql, null);
        if (result == "" || result == "no")
        {
            result = "0";
        }
        sql = "select count(taizhang_id) from taizhang_info where " + ViewState["search"];
        string resulta = DB.ExecuteSqlValue(sql, null);
        if (resulta == "" || resulta == "no")
        {
            resulta = "0";
        }
        mySheet.Cells[1, 1] = datemin.Value + "--" + datemax.Value + "（单位：元） 共实施各类采购" + resulta + "次，其中应急采购" + result + "次";
    
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
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("数据分析.xlsx"));
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
    private void BindInfo()
    {
        StringBuilder sb = new StringBuilder();
       //采购方式
        DataTable dt = Caigoufangshi();
        string str=getDR(dt,"caigoufangshi","公开招标");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "邀请招标");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "集团长协");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "公司长协");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "公开询价");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "固定范围询价");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "单一来源");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "实地询价");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "电商公开");
        sb.Append(str);
        str = getDR(dt, "caigoufangshi", "总计");
        sb.Append(str);
        divcaigoufangshi.InnerHtml = sb.ToString();
        //采购性质
        StringBuilder sbb = new StringBuilder();
        dt = Jihualeixing();
        str = getDR(dt, "jihualeixing", "应急采购");
        sbb.Append(str);
        str = getDR(dt, "jihualeixing", "常规采购");
        sbb.Append(str);
        str = getDR(dt, "jihualeixing", "总计");
        sbb.Append(str);
        divjihualeixing.InnerHtml = sbb.ToString();
        //物资属性
        StringBuilder sbc = new StringBuilder();
        dt = Wuzishuxing();
        str = getDR(dt, "wuzishuxing", "设备");
        sbc.Append(str);
        str = getDR(dt, "wuzishuxing", "配件");
        sbc.Append(str);
        str = getDR(dt, "wuzishuxing", "金属材料");
        sbc.Append(str);
        str = getDR(dt, "wuzishuxing", "非金属材料");
        sbc.Append(str);
        str = getDR(dt, "wuzishuxing", "总计");
        sbc.Append(str);
        divwuzishuxing.InnerHtml = sbc.ToString();

        

        //rpt_caigoufangshi.DataSource = Caigoufangshi();
        //rpt_caigoufangshi.DataBind();
       
        //rpt_jihualeixing.DataSource = Jihualeixing();
        //rpt_jihualeixing.DataBind();
       
        //rpt_wuzishuxing.DataSource = Wuzishuxing();
        //rpt_wuzishuxing.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="col_1">字段名</param>
    /// <param name="col_v">字段对应值</param>
    /// <param name="str">要返回的值的字段参数</param>
    /// <returns></returns>
    protected string getDRVallue(DataTable dt, string col_1,string col_v, string str) {
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][col_1].ToString() == col_v)
            {
                return dt.Rows[i][str].ToString();
            }
        }
        return "";
    }
    protected string getDR(DataTable dt, string Rstr,string str) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][Rstr].ToString() == str)
            {
                if (dt.Rows[i][Rstr].ToString() == "总计")
                {
                    sb.AppendFormat(@"<tr class='text-c' style='background-color:{0}'>
				<td width='200' style='font-size:16px;'>{1}</td>
				<td width='100'>{2}</td>
                <td width='100'>{3}</td>
				<td width='100'>{4}</td>
                <td width='100'>{5}</td>
                <td width='100'>{6}</td>
				<td width='100'>{7}</td>
                <td width='100'>{8}</td>
				<td width='100'>{9}</td>
                <td width='100'>{10}</td>
                <td width='100'>{11}</td></tr>", "#E5BBB9", dt.Rows[i][Rstr].ToString(), dt.Rows[i]["五家沟"].ToString(), dt.Rows[i]["南阳坡"].ToString(), dt.Rows[i]["元宝湾"].ToString(), dt.Rows[i]["动力中心"].ToString(), dt.Rows[i]["洗运中心"].ToString(), dt.Rows[i]["炫昂建材"].ToString(), dt.Rows[i]["永皓电厂"].ToString(), dt.Rows[i]["赤钰冶金"].ToString(), dt.Rows[i]["其它"].ToString(), dt.Rows[i]["合计"].ToString());
                }
                else {
                    sb.AppendFormat(@"<tr class='text-c' style='background-color:{0}'>
				<td width='200' style='font-size:16px;'>{1}</td>
				<td width='100'>{2}</td>
                <td width='100'>{3}</td>
				<td width='100'>{4}</td>
                <td width='100'>{5}</td>
                <td width='100'>{6}</td>
				<td width='100'>{7}</td>
                <td width='100'>{8}</td>
				<td width='100'>{9}</td>
                <td width='100'>{10}</td>
                <td width='100'>{11}</td></tr>", "", dt.Rows[i][Rstr].ToString(), dt.Rows[i]["五家沟"].ToString(), dt.Rows[i]["南阳坡"].ToString(), dt.Rows[i]["元宝湾"].ToString(), dt.Rows[i]["动力中心"].ToString(), dt.Rows[i]["洗运中心"].ToString(), dt.Rows[i]["炫昂建材"].ToString(), dt.Rows[i]["永皓电厂"].ToString(), dt.Rows[i]["赤钰冶金"].ToString(), dt.Rows[i]["其它"].ToString(), dt.Rows[i]["合计"].ToString());
                }
                return sb.ToString();

            }
        }
        sb.AppendFormat(@"<tr class='text-c' style='background-color:{0}'>
				<td width='200' style='font-size:16px;'>{1}</td>
				<td width='100'>{2}</td>
                <td width='100'>{3}</td>
				<td width='100'>{4}</td>
                <td width='100'>{5}</td>
                <td width='100'>{6}</td>
				<td width='100'>{7}</td>
                <td width='100'>{8}</td>
				<td width='100'>{9}</td>
                <td width='100'>{10}</td>
                <td width='100'>{11}</td></tr>", "", str, "", "", "", "", "", "", "", "", "","");
        return sb.ToString();
    }
    protected DataTable Caigoufangshi() {
        //采购方式
        string sql = @"with ws as(
select * from(
select caigoufangshi,case shiyongdanwei 
when '五家沟' then '五家沟' 
when '南阳坡' then '南阳坡' 
when '元宝湾' then '元宝湾' 
when '动力中心' then '动力中心' 
when '洗运中心' then '洗运中心' 
when '炫昂建材' then '炫昂建材' 
when '永皓电厂' then '永皓电厂'
when '赤钰冶金' then '赤钰冶金'  
else '其它' end shiyongdanwei,hanshuijine
from taizhang_info where " + ViewState["search"].ToString() + @"
)as t
pivot(sum(hanshuijine) for shiyongdanwei in ([五家沟],[南阳坡],[元宝湾],[动力中心],[洗运中心],[炫昂建材],[永皓电厂],[赤钰冶金],[其它])) 
as pvt
)
select *,(select isnull(wsa.五家沟,0)+isnull(wsa.南阳坡,0)+isnull(wsa.元宝湾,0)+isnull(wsa.动力中心,0)+isnull(wsa.洗运中心,0)+isnull(wsa.炫昂建材,0)+isnull(wsa.永皓电厂,0)+isnull(wsa.赤钰冶金,0)+isnull(wsa.其它,0)  from ws wsa where wsa.caigoufangshi=wsw.caigoufangshi) '合计'
from ws wsw
union all 
select '总计' as caigoufangshi,sum(wsb.五家沟),sum(wsb.南阳坡),sum(wsb.元宝湾),sum(wsb.动力中心),sum(wsb.洗运中心),sum(wsb.炫昂建材),sum(wsb.永皓电厂),sum(wsb.赤钰冶金),sum(wsb.其它)
,(sum(isnull(wsb.五家沟,0))+sum(isnull(wsb.南阳坡,0))+sum(isnull(wsb.元宝湾,0))+sum(isnull(wsb.动力中心,0))+sum(isnull(wsb.洗运中心,0))+sum(isnull(wsb.炫昂建材,0))+sum(isnull(wsb.永皓电厂,0))+sum(isnull(wsb.赤钰冶金,0))+sum(isnull(wsb.其它,0)))
from ws wsb";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        return ds.Tables[0];
    }
    protected DataTable Jihualeixing() {
        //计划类型
        string sql = @"with ws as(
select * from(
select jihualeixing,case shiyongdanwei 
when '五家沟' then '五家沟' 
when '南阳坡' then '南阳坡' 
when '元宝湾' then '元宝湾' 
when '动力中心' then '动力中心' 
when '洗运中心' then '洗运中心' 
when '炫昂建材' then '炫昂建材' 
when '永皓电厂' then '永皓电厂'
when '赤钰冶金' then '赤钰冶金'  
else '其它' end shiyongdanwei,hanshuijine
from taizhang_info where " + ViewState["search"].ToString() + @"
)as t
pivot(sum(hanshuijine) for shiyongdanwei in ([五家沟],[南阳坡],[元宝湾],[动力中心],[洗运中心],[炫昂建材],[永皓电厂],[赤钰冶金],[其它])) 
as pvt
)
select *,(select isnull(wsa.五家沟,0)+isnull(wsa.南阳坡,0)+isnull(wsa.元宝湾,0)+isnull(wsa.动力中心,0)+isnull(wsa.洗运中心,0)+isnull(wsa.炫昂建材,0)+isnull(wsa.永皓电厂,0)+isnull(wsa.赤钰冶金,0)+isnull(wsa.其它,0)  from ws wsa where wsa.jihualeixing=wsw.jihualeixing) '合计'
from ws wsw
union all 
select '总计' as jihualeixing,sum(wsb.五家沟),sum(wsb.南阳坡),sum(wsb.元宝湾),sum(wsb.动力中心),sum(wsb.洗运中心),sum(wsb.炫昂建材),sum(wsb.永皓电厂),sum(wsb.赤钰冶金),sum(wsb.其它)
,(sum(isnull(wsb.五家沟,0))+sum(isnull(wsb.南阳坡,0))+sum(isnull(wsb.元宝湾,0))+sum(isnull(wsb.动力中心,0))+sum(isnull(wsb.洗运中心,0))+sum(isnull(wsb.炫昂建材,0))+sum(isnull(wsb.永皓电厂,0))+sum(isnull(wsb.赤钰冶金,0))+sum(isnull(wsb.其它,0)))
from ws wsb";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        return ds.Tables[0];
    }
    protected DataTable Wuzishuxing() {
        //物资属性
        string sql = @"with ws as(
select * from(
select wuzishuxing,case shiyongdanwei 
when '五家沟' then '五家沟' 
when '南阳坡' then '南阳坡' 
when '元宝湾' then '元宝湾' 
when '动力中心' then '动力中心' 
when '洗运中心' then '洗运中心' 
when '炫昂建材' then '炫昂建材' 
when '永皓电厂' then '永皓电厂'
when '赤钰冶金' then '赤钰冶金'  
else '其它' end shiyongdanwei,hanshuijine
from taizhang_info where " + ViewState["search"].ToString() + @"
)as t
pivot(sum(hanshuijine) for shiyongdanwei in ([五家沟],[南阳坡],[元宝湾],[动力中心],[洗运中心],[炫昂建材],[永皓电厂],[赤钰冶金],[其它])) 
as pvt
)
select *,(select isnull(wsa.五家沟,0)+isnull(wsa.南阳坡,0)+isnull(wsa.元宝湾,0)+isnull(wsa.动力中心,0)+isnull(wsa.洗运中心,0)+isnull(wsa.炫昂建材,0)+isnull(wsa.永皓电厂,0)+isnull(wsa.赤钰冶金,0)+isnull(wsa.其它,0)  from ws wsa where wsa.wuzishuxing=wsw.wuzishuxing) '合计'
from ws wsw
union all 
select '总计' as wuzishuxing,sum(wsb.五家沟),sum(wsb.南阳坡),sum(wsb.元宝湾),sum(wsb.动力中心),sum(wsb.洗运中心),sum(wsb.炫昂建材),sum(wsb.永皓电厂),sum(wsb.赤钰冶金),sum(wsb.其它)
,(sum(isnull(wsb.五家沟,0))+sum(isnull(wsb.南阳坡,0))+sum(isnull(wsb.元宝湾,0))+sum(isnull(wsb.动力中心,0))+sum(isnull(wsb.洗运中心,0))+sum(isnull(wsb.炫昂建材,0))+sum(isnull(wsb.永皓电厂,0))+sum(isnull(wsb.赤钰冶金,0))+sum(isnull(wsb.其它,0)))
from ws wsb";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        return ds.Tables[0];
    }
}