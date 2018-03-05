using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class test02 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// 创建Excel中导出的柱状图表的数据
    /// </summary>
    //private void CreateTableData_Histogram(Microsoft.Office.Interop.Excel._Worksheet worksheet, Microsoft.Office.Interop.Excel.Range range)
    //{
    //    range = worksheet.Columns;
    //    range.ColumnWidth = 15;

    //    DataTable DTReportGetPoValue_KByScopeByBU = GetReportGetPoValue_KByScopeByBU();

    //    if (DTReportGetPoValue_KByScopeByBU != null)
    //    {
    //        int rowIndex = DTReportGetPoValue_KByScopeByBU.Rows.Count;
    //        int colIndex = DTReportGetPoValue_KByScopeByBU.Columns.Count;
    //        worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[2, 2], (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[rowIndex + 2, colIndex + 2]).NumberFormat = "#,###,###,##0.00";
    //        for (int k = 0; k < colIndex; k++)
    //        {
    //            range.Cells[1, k + 1] = gvPOValue_kByScopeByBU.HeaderRow.Cells[k].Text;
    //            if (gvPOValue_kByScopeByBU.HeaderRow.Cells[k].Text == "AllPriceUnit")
    //            {
    //                range.Cells[1, k + 1] = "Total";
    //            }
    //            string colName = gvPOValue_kByScopeByBU.HeaderRow.Cells[k].Text;
    //            if (colName.IndexOf("&amp;") > -1)
    //            {
    //                int indexNum = colName.IndexOf("&");
    //                colName = colName.Substring(0, indexNum) + "&" + colName.Substring(indexNum + 5);
    //                worksheet.Cells[1, k + 1] = colName;
    //            }

    //        }

    //        for (int j = 0; j < rowIndex; j++)
    //        {

    //            for (int k = 0; k < colIndex; k++)
    //            {
    //                range.Cells[j + 2, k + 1] = DTReportGetPoValue_KByScopeByBU.Rows[j][k].ToString();
    //                if (DTReportGetPoValue_KByScopeByBU.Rows[j][k].ToString() == "总计")
    //                    range.Cells[j + 2, k + 1] = "total";
    //            }
    //        }
    //    }
    //}

    ///// <summary>
    ///// 创建Excel中导出的饼状图表的数据
    ///// </summary>
    //private void CreateTableData_PieChart(Microsoft.Office.Interop.Excel._Worksheet worksheet, Microsoft.Office.Interop.Excel.Range range)
    //{
    //    range = worksheet.Columns;
    //    range.ColumnWidth = 15;

    //    DataTable DTReportGetPoValue_KByScopeByBU = GetReportGetPoValue_KByScopeByBU();

    //    if (DTReportGetPoValue_KByScopeByBU != null)
    //    {
    //        int rowIndex = DTReportGetPoValue_KByScopeByBU.Rows.Count;
    //        int colIndex = DTReportGetPoValue_KByScopeByBU.Columns.Count;

    //        for (int i = 0; i < rowIndex; i++)
    //        {
    //            for (int j = 0; j < colIndex; j++)
    //            {
    //                string colName = gvPOValue_kByScopeByBU.HeaderRow.Cells[j].Text;
    //                if (colName.IndexOf("&amp;") > -1)
    //                {
    //                    int indexNum = colName.IndexOf("&");
    //                    colName = colName.Substring(0, indexNum) + "&" + colName.Substring(indexNum + 5);
    //                    worksheet.Cells[rowIndex + 38, j + 1] = colName;
    //                }
    //                if (i == 0 && j < colIndex - 1 && j > 0)
    //                {
    //                    range.Cells[rowIndex + 38 - 1, j + 1] = gvPOValue_kByScopeByBU.HeaderRow.Cells[j].Text;
    //                }
    //                if (i == rowIndex - 1 && j > 0 && j < colIndex - 1)
    //                {
    //                    range.Cells[rowIndex + 39 - 1, j + 1] = DTReportGetPoValue_KByScopeByBU.Rows[i][j].ToString();
    //                }

    //            }
    //        }
    //        worksheet.get_Range((Microsoft.Office.Interop.Excel.Range)worksheet.Cells[rowIndex + 39 - 1, 2], (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[rowIndex + 39 - 1, colIndex + 2]).NumberFormat = "#,###,###,###,##0.00";
    //    }
    //}

    ///// <summary>
    ///// 导出
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void btnExportDetails_Click(object sender, EventArgs e)
    //{
    //    Excel.Application app = new Excel.Application();
    //    if (app == null)
    //    {
    //        return;
    //    }
    //    //以下是EXCEL.APPLICATION控制EXCEL方法
    //    app.Visible = false; //如果只想用程序控制该excel而不想让用户操作时候，可以设置为false
    //    app.UserControl = true;
    //    app.DisplayAlerts = false;

    //    Excel.Workbooks workbooks = app.Workbooks;
    //    Excel.Workbook workbook = workbooks.Add(Type.Missing); //根据模板产生新的workbook
    //    //stringinit();
    //    Excel.Sheets sheets = workbook.Worksheets;
    //    Excel._Worksheet worksheet = (Excel._Worksheet)sheets.get_Item(1);
    //    if (worksheet == null)
    //    {
    //        return;
    //    }

    //    Excel.Range range = null;
    //    CreateTableData_Histogram(worksheet, range); //这里是生成柱状图的数据源
    //    CreateTableData_PieChart(worksheet, range);//这里是生产饼状图的数据源
    //    GetSavingTB_Histogram(worksheet, workbook);//柱状图
    //    GetSavingTB_PieChart(worksheet, workbook); //饼状图


    //    FileInfo file = null;
    //    try
    //    {
    //        string fileName = "ProcurementInvolvement_" + MUID + "_" + DateTime.Now.ToString("yyMMddhhmmss") + ".xls";
    //        string sPath = strFileFullPath + "\\" + fileName;//新的存放路径
    //        workbook.SaveCopyAs(sPath);
    //        file = new FileInfo(sPath);
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        if (range != null)
    //        {
    //            Marshal.ReleaseComObject(range);
    //            range = null;
    //        }
    //        if (sheets != null)
    //        {
    //            Marshal.ReleaseComObject(sheets);
    //            sheets = null;
    //        }
    //        if (workbook != null)
    //        {
    //            workbook.Close(false, Type.Missing, Type.Missing);
    //            Marshal.ReleaseComObject(workbook);
    //            workbook = null;
    //        }
    //        if (app != null)
    //        {
    //            app.Workbooks.Close();
    //            app.Quit();
    //            Marshal.ReleaseComObject(app);
    //            app = null;
    //        }
    //        GC.Collect();
    //    }
    //    Response.Clear();
    //    Response.Charset = "GB2312";
    //    Response.ContentEncoding = System.Text.Encoding.UTF8;
    //    // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
    //    Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
    //    // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
    //    Response.AddHeader("Content-Length", file.Length.ToString());

    //    // 指定返回的是一个不能被客户端读取的流，必须被下载 
    //    Response.ContentType = "application/ms-excel";

    //    // 把文件流发送到客户端 
    //    Response.WriteFile(file.FullName);
    //    // 停止页面的执行 
    //    Response.End();
    //}

    ///// <summary>
    ///// 生成柱状图表
    ///// </summary>
    ///// <param name="worksheet"></param>
    ///// <param name="workbook"></param>
    //private void GetSavingTB_Histogram(Excel._Worksheet worksheet, Excel.Workbook workbook)
    //{
    //    if (gvPOValue_kByScopeByBU.Rows.Count > 0)
    //    {
    //        int rowIndex = gvPOValue_kByScopeByBU.Rows.Count;
    //        int colIndex = gvPOValue_kByScopeByBU.HeaderRow.Cells.Count;


    //        worksheet.get_Range((Excel.Range)worksheet.Cells[1, 1], (Excel.Range)worksheet.Cells[rowIndex + 2, colIndex]).Font.Name = "Arial";
    //        worksheet.get_Range((Excel.Range)worksheet.Cells[1, 1], (Excel.Range)worksheet.Cells[rowIndex + 2, colIndex]).Font.Size = 10;
    //        Excel.Chart xlChart = (Excel.Chart)workbook.Charts.Add(Type.Missing, Type.Missing, 1, Type.Missing);
    //        Excel.Range range = worksheet.get_Range((Excel.Range)worksheet.Cells[1, 1], (Excel.Range)worksheet.Cells[rowIndex, colIndex]);

    //        xlChart.ChartWizard(range, Excel.XlChartType.xlColumnStacked, Type.Missing, Excel.XlRowCol.xlColumns, 1, 1, true, "", "", "", Type.Missing);
    //        workbook.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, "Sheet1");
    //        workbook.ActiveChart.HasLegend = true;
    //        workbook.ActiveChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom;
    //        workbook.ActiveChart.Legend.Font.Bold = 1;
    //        //Excel.Legend l = null;
    //        //l = (Excel.Legend)workbook.ActiveChart.Legend.LegendEntries(8);
    //        //l.Font.ColorIndex = 2;
    //        workbook.ActiveChart.PlotArea.Top = 20;
    //        workbook.ActiveChart.PlotArea.Left = 20;
    //        workbook.ActiveChart.PlotArea.Height = 200;
    //        Excel.Axis yAxis = (Excel.Axis)workbook.ActiveChart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
    //        yAxis.HasMajorGridlines = false;
    //        yAxis.HasMinorGridlines = false;
    //        yAxis.TickLabels.Font.Bold = 1;
    //        yAxis.TickLabels.NumberFormat = "#,###,###,##0";
    //        Excel.Axis xAxis = (Excel.Axis)workbook.ActiveChart.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
    //        xAxis.TickLabels.Font.Bold = 1;
    //        xAxis.AxisTitle.Font.Italic = false;
    //        xAxis.AxisTitle.Font.Size = 5;
    //        string chart = "chart";
    //        if (System.Configuration.ConfigurationManager.AppSettings["ExcelLan"] != null)
    //        {
    //            if (System.Configuration.ConfigurationManager.AppSettings["ExcelLan"].ToString().ToUpper() == "CN")
    //                chart = "图表";
    //        }
    //        worksheet.Shapes.Item(chart + " 1").Left = (float)(Convert.ToDouble(range.Left) + 20);
    //        worksheet.Shapes.Item(chart + " 1").Top = (float)(double)range.Top + (float)(double)range.Height + 100f;
    //        worksheet.Shapes.Item(chart + " 1").Width = 1010;// 3 * 20 + (int)(workbook.ActiveChart.Legend.Width) + 200; //调图表的宽度，这样根据柱子的数量乘以一个宽度值再加上图例的宽度再加上余量（左右边距什么的）出来的图就不会太小了，基本上不错了，稍微放大缩小下就好，非常方便
    //        worksheet.Shapes.Item(chart + " 1").Height = 300;// 3 * 10 + 200; //调图表的高度

    //        workbook.ActiveChart.PlotArea.Width = 1000; //设置绘图区宽度
    //        workbook.ActiveChart.PlotArea.Top = 20;
    //        workbook.ActiveChart.PlotArea.Height = 250; //设置绘图区高度
    //        workbook.ActiveChart.PlotArea.Left = 20;
    //        Excel.Series oSeries;
    //        //oSeries = (Excel.Series)workbook.ActiveChart.SeriesCollection(2);
    //        //oSeries.ChartType = Excel.XlChartType.xlLineMarkers;
    //        //oSeries.HasDataLabels = true;
    //        //oSeries.Border.ColorIndex = 3;
    //        //oSeries.MarkerBackgroundColor = 255;
    //        //oSeries.MarkerForegroundColor = 255;
    //        //少一个生成双y轴的语句
    //        //oSeries.AxisGroup = Excel.XlAxisGroup.xlSecondary;//生成y轴次坐标轴 既可生成双y轴
    //        //oSeries = (Excel.Series)workbook.ActiveChart.SeriesCollection(1);       
    //        //oSeries.Interior.ColorIndex = 11;//蓝色
    //        oSeries = (Excel.Series)workbook.ActiveChart.SeriesCollection(colIndex - 1);
    //        oSeries.HasDataLabels = true;
    //        oSeries.Interior.ColorIndex = 2;
    //        //oSeries.Interior.ColorIndex = 37;
    //        //Excel.DataLabels oDLabels;
    //        //oDLabels = (Excel.Series)workbook.ActiveChart.SeriesCollection(colIndex - 1) as Excel.DataLabels;
    //        //oDLabels.Font.ColorIndex = 3;

    //        //Excel.DataLabels d = (Excel.DataLabels)oSeries.DataLabels(1);
    //        //d.Position = Excel.XlDataLabelPosition.xlLabelPositionAbove;
    //        //未完成 少一个将datalabel 的位置改变的语句
    //        //Excel.XlAxisGroup.xlSecondary; 设置此轴

    //    }
    //}

    ///// <summary>
    ///// 生成饼状图表
    ///// </summary>
    ///// <param name="worksheet"></param>
    ///// <param name="workbook"></param>
    //private void GetSavingTB_PieChart(Excel._Worksheet worksheet, Excel.Workbook workbook)
    //{
    //    if (gvPOValue_kByScopeByBU.Rows.Count > 0)
    //    {
    //        int rowIndex = gvPOValue_kByScopeByBU.Rows.Count;
    //        int colIndex = gvPOValue_kByScopeByBU.HeaderRow.Cells.Count;

    //        worksheet.get_Range((Excel.Range)worksheet.Cells[rowIndex + 38 - 1, 1], (Excel.Range)worksheet.Cells[rowIndex + 39, 9]).Font.Name = "Arial";
    //        worksheet.get_Range((Excel.Range)worksheet.Cells[rowIndex + 38, 1], (Excel.Range)worksheet.Cells[rowIndex + 39, 9]).Font.Size = 10;

    //        Excel.Chart xlChart = (Excel.Chart)workbook.Charts.Add(Type.Missing, Type.Missing, 1, Type.Missing);
    //        Excel.Range range = worksheet.get_Range((Excel.Range)worksheet.Cells[rowIndex + 38 - 1, 1], (Excel.Range)worksheet.Cells[rowIndex + 39 - 1, colIndex - 1]);

    //        xlChart.ChartWizard(range, Excel.XlChartType.xlPie, Type.Missing, Excel.XlRowCol.xlRows, 1, 1, false, "", Type.Missing, Type.Missing, Type.Missing);

    //        //xlChart.ChartWizard(chartRage, Excel.XlChartType.xl3DColumn, Missing.Value, Excel.XlRowCol.xlColumns, 1, 1, true, "实验室效率分析", "上机时间", "上机次数", Missing.Value);

    //        workbook.ActiveChart.Location(Excel.XlChartLocation.xlLocationAsObject, "Sheet1");
    //        //workbook.ActiveChart.HasLegend = true;
    //        //workbook.ActiveChart.Legend.Position = Excel.XlLegendPosition.xlLegendPositionBottom;
    //        //Excel.Legend l = null;
    //        //l = (Excel.Legend)workbook.ActiveChart.Legend.LegendEntries(8);
    //        //l.Font.ColorIndex = 2;

    //        //workbook.ActiveChart.PlotArea.Top = 20;
    //        //workbook.ActiveChart.PlotArea.Left = 20;
    //        //workbook.ActiveChart.PlotArea.Height = 200;
    //        //Excel.Axis yAxis = (Excel.Axis)workbook.ActiveChart.Axes(Excel.XlAxisType.xlValue, Excel.XlAxisGroup.xlPrimary);
    //        //yAxis.HasMajorGridlines = false;
    //        //yAxis.HasMinorGridlines = false;
    //        //Excel.Axis xAxis = (Excel.Axis)workbook.ActiveChart.Axes(Excel.XlAxisType.xlCategory, Excel.XlAxisGroup.xlPrimary);
    //        //xAxis.AxisTitle.Font.Italic = false;
    //        //xAxis.AxisTitle.Font.Size = 5;
    //        string chart = "chart";
    //        if (System.Configuration.ConfigurationManager.AppSettings["ExcelLan"] != null)
    //        {
    //            if (System.Configuration.ConfigurationManager.AppSettings["ExcelLan"].ToString().ToUpper() == "CN")
    //                chart = "图表";
    //        }
    //        worksheet.Shapes.Item(chart + " 2").Left = (float)(Convert.ToDouble(range.Left) + 150);
    //        worksheet.Shapes.Item(chart + " 2").Top = (float)(double)range.Top + (float)(double)range.Height + 50f;
    //        worksheet.Shapes.Item(chart + " 2").Width = 400;// 3 * 20 + (int)(workbook.ActiveChart.Legend.Width) + 200; //调图表的宽度，这样根据柱子的数量乘以一个宽度值再加上图例的宽度再加上余量（左右边距什么的）出来的图就不会太小了，基本上不错了，稍微放大缩小下就好，非常方便
    //        worksheet.Shapes.Item(chart + " 2").Height = 250;// 3 * 10 + 200; //调图表的高度

    //        //workbook.ActiveChart.PlotArea.Width = 1000; //设置绘图区宽度
    //        //workbook.ActiveChart.PlotArea.Top = 20;
    //        //workbook.ActiveChart.PlotArea.Height = 250; //设置绘图区高度
    //        //workbook.ActiveChart.PlotArea.Left = 20;
    //        Excel.Series oSeries;

    //        oSeries = (Excel.Series)workbook.ActiveChart.SeriesCollection(1);
    //        //oSeries.ChartType = Excel.XlChartType.xlLineMarkers;
    //        oSeries.HasDataLabels = true;
    //        //oSeries.Border.ColorIndex = 3;
    //        //oSeries.MarkerBackgroundColor = 255;
    //        //oSeries.MarkerForegroundColor = 255;
    //        //少一个生成双y轴的语句
    //        //oSeries.AxisGroup = Excel.XlAxisGroup.xlSecondary;//生成y轴次坐标轴 既可生成双y轴
    //        //oSeries = (Excel.Series)workbook.ActiveChart.SeriesCollection(1);       
    //        //oSeries.Interior.ColorIndex = 11;//蓝色

    //        //oSeries = (Excel.Series)workbook.ActiveChart.SeriesCollection(8);
    //        //oSeries.HasDataLabels = true;
    //        //oSeries.Interior.ColorIndex = 2;

    //        //oSeries.Interior.ColorIndex = 37;
    //        //Excel.DataLabels d = (Excel.DataLabels)oSeries.DataLabels(1);
    //        //d.Position = Excel.XlDataLabelPosition.xlLabelPositionAbove;
    //        //未完成 少一个将datalabel 的位置改变的语句
    //        //Excel.XlAxisGroup.xlSecondary; 设置此轴

    //    }
    //}
}