using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

public partial class test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void lbtn_daoru_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string dt = DateTime.Now.ToString("yyyyMMddHHmmss");
            FileUpload1.SaveAs(Server.MapPath("~/daoru/") + dt + FileUpload1.FileName);
            //导入数据
            DataTable dta = ExcelToTable(Server.MapPath("~/daoru/") + dt + FileUpload1.FileName);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择要导入的Excel");
        }
    }
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
                    string currentstr = "'" + sheet.GetRow(i).GetCell(j) + "',";
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
            }
        }
        return dt;
    }
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


}