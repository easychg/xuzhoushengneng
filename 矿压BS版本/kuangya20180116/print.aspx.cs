using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        string date = Request.QueryString["date"].ToString();
        string url = Request.QueryString["url"].ToString();
        string imgurl = Server.MapPath("~/" + url);
        string sql = @"select * from prereport where reportdate='" + date + "' order by bracketno";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count <= 0)
        {
            SystemTool.AlertShow(this, "报表为空，请在初撑力与末阻力页面查询数据后再导出此报表");
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append("<table><tr><td colspan='10'>综采支架压力综合日报表【报表日期" + date + "】</td></tr>");
        sb.Append("<tr><td colspan='10'>单位：兆帕  工作面名称：" + ds.Tables[0].Rows[0]["facename"].ToString() + "  认证编号：  打印日期：" + DateTime.Now.ToString("yyyy-MM-dd")+"</td></tr>");
        sb.Append("<tr><td colspan='10'><img src='"+url+"' style='width:90%;'></td></tr>");
        sb.Append("<tr><td colspan='10' style='text-align:left;'>检测数据统计表：</td></tr>");
        sb.Append("<tr><td rowspan='5' colspan='2'>工作面工作阻力统计</td><td colspan='2'>整面</td><td colspan='2'>上部</td><td colspan='2'>中部</td><td colspan='2'>下部</td></tr>");
        sb.Append("<tr><td>最大</td><td>最小</td><td>最大</td><td>最小</td><td>最大</td><td>最小</td><td>最大</td><td>最小</td></tr>");
        sb.Append("<tr><td colspan='2'>平均</td><td colspan='2'>平均</td><td colspan='2'>平均</td><td colspan='2'>平均</td></tr>");
        
        ////导出文件  
        //string dts = DateTime.Now.ToString("yyyyMMddHHmmss");
        ////添加图片
        //mySheet.Shapes.AddPicture(imgurl, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, 5, 30, 550, 200);

        //mySheet.Cells[1, 1] = "综采支架压力综合日报表【报表日期" + date + "】";
        //mySheet.Cells[2, 1] = "单位：兆帕  工作面名称：" + ds.Tables[0].Rows[0]["facename"].ToString() + "  认证编号：  打印日期：" + DateTime.Now.ToString("yyyy-MM-dd");
        //整面最大值、最小值
        //mySheet.Cells[8, 3] = ds.Tables[0].Compute("Max(zlmax)", "true");
        //mySheet.Cells[8, 4] = ds.Tables[0].Compute("Min(zlmin)", "true");
        //mySheet.Cells[9, 3] = ds.Tables[0].Compute("avg(zlavg)", "true");
        string shanga = "0.00";
        string shangb = "0.00";
        string shangc = "0.00";
        string zhonga = "0.00";
        string zhongb = "0.00";
        string zhongc = "0.00";
        string xiaa = "0.00";
        string xiab = "0.00";
        string xiac = "0.00";
        string sqlszx = "select max(zlmax) a,min(zlmin) b,avg(zlavg) c from prereport where reportdate='" + date + "' and distance='上部'";
        DataSet dsszx = DB.ExecuteSqlDataSet(sqlszx, null);
        if (dsszx.Tables[0].Rows.Count > 0)
        {
            shanga = dsszx.Tables[0].Rows[0]["a"].ToString();
             shangb= dsszx.Tables[0].Rows[0]["b"].ToString();
            shangc = dsszx.Tables[0].Rows[0]["c"].ToString();
        }
        sqlszx = "select max(zlmax) a,min(zlmin) b,avg(zlavg) c from prereport where reportdate='" + date + "' and distance='中部'";
        dsszx = DB.ExecuteSqlDataSet(sqlszx, null);
        if (dsszx.Tables[0].Rows.Count > 0)
        {
            zhonga = dsszx.Tables[0].Rows[0]["a"].ToString();
             zhongb= dsszx.Tables[0].Rows[0]["b"].ToString();
             zhongc= dsszx.Tables[0].Rows[0]["c"].ToString();
        }
        sqlszx = "select max(zlmax) a,min(zlmin) b,avg(zlavg) c from prereport where reportdate='" + date + "' and distance='下部'";
        dsszx = DB.ExecuteSqlDataSet(sqlszx, null);
        if (dsszx.Tables[0].Rows.Count > 0)
        {
            xiaa = dsszx.Tables[0].Rows[0]["a"].ToString();
             xiab= dsszx.Tables[0].Rows[0]["b"].ToString();
             xiac= dsszx.Tables[0].Rows[0]["c"].ToString();
        }
        sb.Append("<tr><td>" + ds.Tables[0].Compute("Max(zlmax)", "true") + "</td><td>" + ds.Tables[0].Compute("Min(zlmin)", "true") + "</td><td>"+shanga+"</td><td>"+shangb+"</td><td>"+zhonga+"</td><td>"+zhongb+"</td><td>"+xiaa+"</td><td>"+xiab+"</td></tr>");
        sb.Append("<tr><td colspan='2'>" + ds.Tables[0].Compute("avg(zlavg)", "true") + "</td><td colspan='2'>"+shangc+"</td><td colspan='2'>"+zhongc+"</td><td colspan='2'>"+xiac+"</td></tr>");
        sb.Append("<tr><td rowspan='2'>分机编号</td><td rowspan='2'>支架编号</td><td rowspan='2' colspan='2'>安装位置</td><td colspan='2'>工作阻力</td><td colspan='2'>初撑力</td><td colspan='2'>末阻力</td></tr>");
        sb.Append("<tr><td>最大</td><td>平均</td><td>最小</td><td>平均</td><td>最大</td><td>平均</td></tr>");
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            sb.Append("<tr><td>" + ds.Tables[0].Rows[i]["sensorNo"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["bracketno"].ToString() + "</td><td colspan='2'>" + ds.Tables[0].Rows[i]["distance"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["zlmax"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["zlavg"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["cclmax"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["cclavg"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["mzlmax"].ToString() + "</td><td>" + ds.Tables[0].Rows[i]["mzlavg"].ToString() + "</td></tr>");
            //mySheet.Cells[12 + i, 1] = ds.Tables[0].Rows[i]["sensorNo"].ToString();
            //mySheet.Cells[12 + i, 2] = ds.Tables[0].Rows[i]["bracketno"].ToString();
            //mySheet.Cells[12 + i, 3] = ds.Tables[0].Rows[i]["distance"].ToString();
            //mySheet.Cells[12 + i, 5] = ds.Tables[0].Rows[i]["zlmax"].ToString();
            //mySheet.Cells[12 + i, 6] = ds.Tables[0].Rows[i]["zlavg"].ToString();
            //mySheet.Cells[12 + i, 7] = ds.Tables[0].Rows[i]["cclmax"].ToString();
            //mySheet.Cells[12 + i, 8] = ds.Tables[0].Rows[i]["cclavg"].ToString();
            //mySheet.Cells[12 + i, 9] = ds.Tables[0].Rows[i]["mzlmax"].ToString();
            //mySheet.Cells[12 + i, 10] = ds.Tables[0].Rows[i]["mzlavg"].ToString();
        }
        sb.Append("<tr><td colspan='10' style='text-align:left;'>检测分析结论:</td></tr>");
        sb.Append("<tr><td colspan='10' style='height:40px;'></td></tr>");
        sb.Append("<tr><td colspan='10' style='text-align:left;'>区队意见:</td></tr>");
        sb.Append("<tr><td colspan='10' style='height:40px;'></td></tr>");
        sb.Append("<tr><td colspan='10'> 分管领导签字：__________部门签字：____________报表人：_____________</td></tr>");
        printdiv.InnerHtml = sb.ToString();
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
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode("综采支架压力综合日报表.xlsx"));
        //// 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        //Response.AddHeader("Content-Length", filet.Length.ToString());
        //// 指定返回的是一个不能被客户端读取的流，必须被下载   
        //Response.ContentType = "application/ms-excel";
        //// 把文件流发送到客户端   
        //Response.WriteFile(filet.FullName);
        //// 停止页面的执行   
        //Response.End();
    }
}