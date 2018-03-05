using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Web;

namespace CoreLibrary
{
    public class ExcelHelper
    {
        //ExcelHelper excle = new ExcelHelper();
        //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
        //Response.BinaryWrite(excle.ExportXLS());
        public static DataTable Excel2DataTable(string filePath)
        {
            NPOI.SS.UserModel.ISheet sheet=null;
            #region//初始化信息
            try
            {
                string fileExt = Path.GetExtension(filePath).ToLower();
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileExt==".xls")
                    {
                        sheet = new NPOI.HSSF.UserModel.HSSFWorkbook(file).GetSheetAt(0);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        sheet = new NPOI.XSSF.UserModel.XSSFWorkbook(file).GetSheetAt(0);
                    }
                    else {
                        sheet = null;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            #endregion
            if (sheet == null) return null;
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
            }
            while (rows.MoveNext())
            {
                NPOI.HSSF.UserModel.HSSFRow row = (NPOI.HSSF.UserModel.HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static void ExportXLS(DataTable dt, string[] columns,string fileName)
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("sheet1");
            NPOI.SS.UserModel.IRow row0 = sheet.CreateRow(0);
            for (int i = 0; i < columns.Length; i++) {
                row0.CreateCell(i).SetCellValue(columns[i]);
            }
            for (int i = 0; i < dt.Rows.Count; i++) {
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(i+1);
                for (int j = 0; j < columns.Length; j++)
                {
                    row.CreateCell(j).SetCellValue(dt.Rows[i][j].ToString());
                }
            }  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", fileName));
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            book = null;
            ms.Close();
            ms.Dispose();
        }

        public byte[] ExportXLSX(){
            NPOI.XSSF.UserModel.XSSFWorkbook book = new NPOI.XSSF.UserModel.XSSFWorkbook();
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("test_01");  
            // 第一列  
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);  
            row.CreateCell(0).SetCellValue("第一列第一行");  
            // 第二列  
            NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(1);  
            row2.CreateCell(0).SetCellValue("第二列第一行");  
            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();  
            book.Write(ms);  
            //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));  
            //Response.BinaryWrite(ms.ToArray());
            byte[] excel = ms.ToArray();
            book = null;  
            ms.Close();  
            ms.Dispose();
            return excel;
        }
        public byte[] ExportXLS()
        {
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();  
            NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("sheet1");

            // 第一列  
            NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("第一列第一行");

            // 第二列  
            NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(1);
            row2.CreateCell(0).SetCellValue("第二列第一行");

            //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 10));
            //通过Cell的CellFormula向单元格中写入公式
            //注：直接写公式内容即可，不需要在最前加'='
            //NPOI.SS.UserModel.ICell cell2 = sheet.CreateRow(1).CreateCell(0);
            //cell2.CellFormula = "HYPERLINK(\"测试图片.jpg\",\"测试图片.jpg\")";

            //将图片文件读入一个字符串
            byte[] bytes = System.IO.File.ReadAllBytes(@"c:\ceshi.jpg");
            int pictureIdx = book.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
            NPOI.HSSF.UserModel.HSSFPatriarch patriarch = (NPOI.HSSF.UserModel.HSSFPatriarch)sheet.CreateDrawingPatriarch();
            // 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
            //dx1:图片左边相对excel格的位置(x偏移) 范围值为:0~1023;即输100 偏移的位置大概是相对于整个单元格的宽度的100除以1023大概是10分之一
            //dy1:图片上方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。
            //dx2:图片右边相对excel格的位置(x偏移) 范围值为:0~1023; 原理同上。
            //dy2:图片下方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。
            //col1和row1 :图片左上角的位置，以excel单元格为参考,比喻这两个值为(1,1)，那么图片左上角的位置就是excel表(1,1)单元格的右下角的点(A,1)右下角的点。
            //col2和row2:图片右下角的位置，以excel单元格为参考,比喻这两个值为(2,2)，那么图片右下角的位置就是excel表(2,2)单元格的右下角的点(B,2)右下角的点。
            NPOI.HSSF.UserModel.HSSFClientAnchor anchor = new NPOI.HSSF.UserModel.HSSFClientAnchor(70, 10, 0, 0, 2, 2, 4, 4);
            //把图片插到相应的位置
            NPOI.HSSF.UserModel.HSSFPicture pict = (NPOI.HSSF.UserModel.HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);

            // 写入到客户端    
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));  
            //Response.BinaryWrite(ms.ToArray());
            byte[] excel = ms.ToArray();
            book = null;
            ms.Close();
            ms.Dispose();
            return excel;
        }

        //ICellStyle style = workbook.CreateCellStyle();
        ////设置单元格的样式：水平对齐居中
        //style.Alignment = HorizontalAlignment.CENTER;
        //style.BorderBottom= CellBorderType.THIN;
        //style.BorderLeft= CellBorderType.THIN;
        //style.BorderRight= CellBorderType.THIN;
        //style.BorderTop = CellBorderType.THIN;
        //NPOI.SS.UserModel.ICellStyle style = book.CreateCellStyle();
        ////设置单元格的样式：水平对齐居中
        //style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        //style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        //style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        ////新建一个字体样式对象
        //IFont font = workbook.CreateFont();
        ////设置字体加粗样式
        //font.Boldweight = short.MaxValue;
        ////使用SetFont方法将字体样式添加到单元格样式中 
        //style.SetFont(font);
        ////将新的样式赋给单元格
        //cell.CellStyle = style;

        ////设置单元格的高度
        //row.Height = 30 * 20;
        ////设置单元格的宽度
        //sheet.SetColumnWidth(0, 30 * 256);

        ////设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
        ////CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
        //sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));

        ////通过Cell的CellFormula向单元格中写入公式
        ////注：直接写公式内容即可，不需要在最前加'='
        //ICell cell2 = sheet.CreateRow(1).CreateCell(0);
        //cell2.CellFormula = "HYPERLINK(\"测试图片.jpg\",\"测试图片.jpg\")";

        ////将图片文件读入一个字符串
        //byte[] bytes = System.IO.File.ReadAllBytes(@"c:\ceshi.jpg");
        //int pictureIdx = book.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
        //NPOI.HSSF.UserModel.HSSFPatriarch patriarch = (NPOI.HSSF.UserModel.HSSFPatriarch)sheet.CreateDrawingPatriarch();
        //// 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
        ////dx1:图片左边相对excel格的位置(x偏移) 范围值为:0~1023;即输100 偏移的位置大概是相对于整个单元格的宽度的100除以1023大概是10分之一
        ////dy1:图片上方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。
        ////dx2:图片右边相对excel格的位置(x偏移) 范围值为:0~1023; 原理同上。
        ////dy2:图片下方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。
        ////col1和row1 :图片左上角的位置，以excel单元格为参考,比喻这两个值为(1,1)，那么图片左上角的位置就是excel表(1,1)单元格的右下角的点(A,1)右下角的点。
        ////col2和row2:图片右下角的位置，以excel单元格为参考,比喻这两个值为(2,2)，那么图片右下角的位置就是excel表(2,2)单元格的右下角的点(B,2)右下角的点。
        //NPOI.HSSF.UserModel.HSSFClientAnchor anchor = new NPOI.HSSF.UserModel.HSSFClientAnchor(70, 10, 0, 0, 2, 2, 4, 4);
        ////把图片插到相应的位置
        //NPOI.HSSF.UserModel.HSSFPicture pict = (NPOI.HSSF.UserModel.HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
    }
}
