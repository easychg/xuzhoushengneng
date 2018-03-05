using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;

public partial class meirigongzuozulifenbuquxian : System.Web.UI.Page
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
                BindData();

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
            
            sql = "select pressuremax,pressuremin from PressurePar where areaname='" + ddl_cequ.SelectedValue + "' and facename='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "'";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                shangxian.Value = ds.Tables[0].Rows[0]["pressuremax"].ToString();
                xiaxian.Value = ds.Tables[0].Rows[0]["pressuremin"].ToString();
            }
            else {
                shangxian.Value = "0";
                xiaxian.Value = "0";
            }
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);

        }
        //SystemTool.JavascriptShow(this, "changclass5()");
    }

    protected void search_Click(object sender, EventArgs e)
    {
        string url = datemin.Value;
        if (url == "")
        {
            SystemTool.AlertShow(this, "开始日期不能为空");
            return;
        }
        url = datemax.Value;
        if (url == "")
        {
            SystemTool.AlertShow(this, "结束日期不能为空");
            return;
        }
        string riqi = datemin.Value;
        string riqi2 = datemax.Value;
        string t1 = "00:00:00";// datemax.Value;
        string t2 = "23:59:59";// datemax2.Value;
        string cequ = ddl_cequ.SelectedValue;
        string gongzuomian = ddl_gongzuomian.SelectedValue;
        string yz = ConfigurationManager.ConnectionStrings["YuZhi"].ToString();//小于此值，舍去yuzhi.Value;
        string zhu = "3";
        string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
        DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
        Bitmap img = DrawImage.DrawingCurve.DrawingImg10_0(cequ, gongzuomian, zhu, yz, riqi, riqi2+" "+t2);
        string str = Server.MapPath("./xiazai/");
        string str2 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + "MeiRiGongZuoZuLiFenBuQuXian.jpg"; 
        img.Save(str + str2);
        img1.ImageUrl = "xiazai/"+str2;
        ViewState["date"] = riqi;
        ViewState["date2"] = riqi2;
        ViewState["baojingzhi"] = "0";
        ViewState["yujingzhi"] = "0";
        string sqlbjz = "select * from dbo.PressurePar where areaname='" + cequ + "' and facename='" + gongzuomian + "'";
        DataSet result = DB.ExecuteSqlDataSet(sqlbjz, null);
        if (result.Tables[0].Rows.Count > 0)
        {
            ViewState["baojingzhi"] = result.Tables[0].Rows[0]["pressuremax"].ToString();
            ViewState["yujingzhi"] = result.Tables[0].Rows[0]["pressuremin"].ToString();
        }
    }
    protected void lbtn_export_Click(object sender, EventArgs e) {
        string url = img1.ImageUrl;
        if (url == "") {
            SystemTool.AlertShow(this, "图片为空，请先刷新");
            return;
        }
        string ReportFileName = Server.MapPath("~/" + url);
        System.IO.FileInfo filet = new System.IO.FileInfo(ReportFileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名   
        Response.AddHeader("Content-Disposition", "attachment; filename=每日工作阻力分布曲线.jpg");
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度   
        Response.AddHeader("Content-Length", filet.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载   
        Response.ContentType = "application/ms-excel";
        // 把文件流发送到客户端   
        Response.WriteFile(filet.FullName);
        // 停止页面的执行   
        Response.End();  
    }
    protected void lbtn_export_ribao_export_Click(object sender, EventArgs e) {
        string url = datemin.Value;
        if (url == "")
        {
            SystemTool.AlertShow(this, "开始日期不能为空");
            return;
        }
        url = datemax.Value;
        if (url == "")
        {
            SystemTool.AlertShow(this, "结束日期不能为空");
            return;
        }
        url = img1.ImageUrl;
        if (url == "")
        {
            SystemTool.AlertShow(this, "图片为空，请先刷新");
            return;
        }
        if (ViewState["date"].ToString() != datemin.Value || ViewState["date2"].ToString()!=datemax.Value)
        {
            SystemTool.AlertShow(this, "当前图片与日期不一致，请先刷新后再导出此报表");
            return;
        }
        string imgurl = Server.MapPath("~/" + url);
        //Response.Write("<script>window.open('print.aspx?date="+datemin.Value+"&url="+url+"','_blank')</script>");
        string sql = @"select facename,sensorNo,bracketNo,distance,max(zlmax) zlmax,AVG(zlavg) zlavg,min(zlmin) zlmin,MAX(cclmax) cclmax,AVG(cclavg) cclavg,MAX(mzlmax) mzlmax,AVG(mzlavg) mzlavg from prereport where reportdate>='" + datemin.Value + "' and reportdate<='" + datemax.Value + "' group by bracketNo,sensorNo,distance,facename order by bracketno";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count <= 0)
        {
            SystemTool.AlertShow(this, "报表为空，请在初撑力与末阻力页面查询数据后再导出此报表");
            return;
        }
        //整面最大值、最小值
        string zhengmianmax = ds.Tables[0].Compute("Max(zlmax)", "true").ToString();
        string zhengmianmin = ds.Tables[0].Compute("Min(zlmin)", "true").ToString();
        string zhengmianavg = Convert.ToDecimal( ds.Tables[0].Compute("avg(zlavg)", "true").ToString()).ToString("0.00");
        string shangbumax = "0.00";
        string shangbumin = "0.00";
        string shangbuavg = "0.00";
        string zhongbumax = "0.00";
        string zhongbumin = "0.00";
        string zhongbuavg = "0.00";
        string xiabumax = "0.00";
        string xiabumin = "0.00";
        string xiabuavg = "0.00";
        string sqlszx = "select max(zlmax) a,min(zlmin) b,avg(zlavg) c from prereport where reportdate>='" + datemin.Value + "' and reportdate<='" + datemax.Value + "' and distance='上部'";
        DataSet dsszx = DB.ExecuteSqlDataSet(sqlszx, null);
        if (dsszx.Tables[0].Rows.Count > 0)
        {
            shangbumax = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["a"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["a"].ToString()).ToString("0.00");
            shangbumin = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["b"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["b"].ToString()).ToString("0.00");
            shangbuavg = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["c"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["c"].ToString()).ToString("0.00");
        }
        sqlszx = "select max(zlmax) a,min(zlmin) b,avg(zlavg) c from prereport where reportdate>='" + datemin.Value + "' and reportdate<='" + datemax.Value + "' and distance='中部'";
        dsszx = DB.ExecuteSqlDataSet(sqlszx, null);
        if (dsszx.Tables[0].Rows.Count > 0)
        {
            zhongbumax = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["a"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["a"].ToString()).ToString("0.00");
            zhongbumin = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["b"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["b"].ToString()).ToString("0.00");
            zhongbuavg = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["c"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["c"].ToString()).ToString("0.00");
        }
        sqlszx = "select max(zlmax) a,min(zlmin) b,avg(zlavg) c from prereport where reportdate>='" + datemin.Value + "' and reportdate<='" + datemax.Value + "' and distance='下部'";
        dsszx = DB.ExecuteSqlDataSet(sqlszx, null);
        if (dsszx.Tables[0].Rows.Count > 0)
        {
            xiabumax = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["a"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["a"].ToString()).ToString("0.00");
            xiabumin = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["b"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["b"].ToString()).ToString("0.00");
            xiabuavg = Convert.ToDecimal(dsszx.Tables[0].Rows[0]["c"].ToString() == "" ? "0" : dsszx.Tables[0].Rows[0]["c"].ToString()).ToString("0.00");
        }
        NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
        NPOI.SS.UserModel.ISheet sheet = book.CreateSheet("sheet1");
        NPOI.SS.UserModel.ICellStyle style = book.CreateCellStyle();
        //设置单元格的样式：水平对齐居中
        style.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
        style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
        style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

        NPOI.SS.UserModel.ICellStyle styleleft = book.CreateCellStyle();
        //设置单元格的样式：水平对齐居左
        styleleft.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
        styleleft.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleft.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleft.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleft.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        NPOI.SS.UserModel.ICellStyle styleleftcenter = book.CreateCellStyle();
        //设置单元格的样式：居左居中
        styleleftcenter.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
        styleleftcenter.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
        styleleftcenter.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleftcenter.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleftcenter.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleftcenter.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
        styleleftcenter.WrapText = true;
        //将新的样式赋给单元格
        //cell.CellStyle = style;
        //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
        //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
        //第一行
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 9)); 
        NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
        NPOI.SS.UserModel.ICell cell0 = row.CreateCell(0);
        cell0.SetCellValue("综采支架压力综合日报表【报表日期" + datemin.Value + "-" + datemax.Value + "】");
        cell0.CellStyle = style;
        row.CreateCell(1).CellStyle = style; row.CreateCell(2).CellStyle = style; row.CreateCell(3).CellStyle = style; row.CreateCell(4).CellStyle = style; row.CreateCell(5).CellStyle = style; row.CreateCell(6).CellStyle = style; row.CreateCell(7).CellStyle = style; row.CreateCell(8).CellStyle = style; row.CreateCell(9).CellStyle = style;
        //第二行
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 0, 9));
        NPOI.SS.UserModel.IRow row1 = sheet.CreateRow(1);
        NPOI.SS.UserModel.ICell cell1 = row1.CreateCell(0);
        cell1.SetCellValue("单位：兆帕  工作面名称：" + ds.Tables[0].Rows[0]["facename"].ToString() + "  认证编号：  打印日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
        cell1.CellStyle = style;
        row1.CreateCell(1).CellStyle = style; row1.CreateCell(2).CellStyle = style; row1.CreateCell(3).CellStyle = style; row1.CreateCell(4).CellStyle = style; row1.CreateCell(5).CellStyle = style; row1.CreateCell(6).CellStyle = style; row1.CreateCell(7).CellStyle = style; row1.CreateCell(8).CellStyle = style; row1.CreateCell(9).CellStyle = style;
        // 第三行
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 0, 9));
        NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(2);
        NPOI.SS.UserModel.ICell cell2 = row2.CreateCell(0);
        cell2.CellStyle = style;
        row2.CreateCell(1).CellStyle = style; row2.CreateCell(2).CellStyle = style; row2.CreateCell(3).CellStyle = style; row2.CreateCell(4).CellStyle = style; row2.CreateCell(5).CellStyle = style; row2.CreateCell(6).CellStyle = style; row2.CreateCell(7).CellStyle = style; row2.CreateCell(8).CellStyle = style; row2.CreateCell(9).CellStyle = style;
        row2.Height = 4800;
        //将图片文件读入一个字符串
        byte[] bytes = System.IO.File.ReadAllBytes(imgurl);
        int pictureIdx = book.AddPicture(bytes, NPOI.SS.UserModel.PictureType.JPEG);
        NPOI.HSSF.UserModel.HSSFPatriarch patriarch = (NPOI.HSSF.UserModel.HSSFPatriarch)sheet.CreateDrawingPatriarch();
        // 插图片的位置  HSSFClientAnchor（dx1,dy1,dx2,dy2,col1,row1,col2,row2) 后面再作解释
        //dx1:图片左边相对excel格的位置(x偏移) 范围值为:0~1023;即输100 偏移的位置大概是相对于整个单元格的宽度的100除以1023大概是10分之一
        //dy1:图片上方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。
        //dx2:图片右边相对excel格的位置(x偏移) 范围值为:0~1023; 原理同上。
        //dy2:图片下方相对excel格的位置(y偏移) 范围值为:0~256 原理同上。
        //col1和row1 :图片左上角的位置，以excel单元格为参考,比喻这两个值为(1,1)，那么图片左上角的位置就是excel表(1,1)单元格的右下角的点(A,1)右下角的点。
        //col2和row2:图片右下角的位置，以excel单元格为参考,比喻这两个值为(2,2)，那么图片右下角的位置就是excel表(2,2)单元格的右下角的点(B,2)右下角的点。
        NPOI.HSSF.UserModel.HSSFClientAnchor anchor = new NPOI.HSSF.UserModel.HSSFClientAnchor(10, 10, 10, 10, 0, 2, 9, 3);
        //把图片插到相应的位置
        NPOI.HSSF.UserModel.HSSFPicture pict = (NPOI.HSSF.UserModel.HSSFPicture)patriarch.CreatePicture(anchor, pictureIdx);
        //第三行
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 0, 9));
        NPOI.SS.UserModel.IRow row3 = sheet.CreateRow(3);
        NPOI.SS.UserModel.ICell cell3 = row3.CreateCell(0);
        cell3.SetCellValue("监测数据统计表：");
        cell3.CellStyle = styleleft;
        row3.CreateCell(1).CellStyle = styleleft; row3.CreateCell(2).CellStyle = styleleft; row3.CreateCell(3).CellStyle = styleleft; row3.CreateCell(4).CellStyle = styleleft; row3.CreateCell(5).CellStyle = styleleft; row3.CreateCell(6).CellStyle = styleleft; row3.CreateCell(7).CellStyle = styleleft; row3.CreateCell(8).CellStyle = styleleft; row3.CreateCell(9).CellStyle = styleleft; 
        //第四行
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 8, 0, 1));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 2, 3));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 4, 5));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 6, 7));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 8, 9));

        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 2, 3));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 4, 5));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 6, 7));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 8, 9));

        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(8, 8, 2, 3));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(8, 8, 4, 5));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(8, 8, 6, 7));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(8, 8, 8, 9));
        NPOI.SS.UserModel.IRow row4 = sheet.CreateRow(4);
        NPOI.SS.UserModel.ICell cell40 = row4.CreateCell(0);
        cell40.SetCellValue("工作面工作阻力统计：");
        cell40.CellStyle = style;

        NPOI.SS.UserModel.ICell cell41 = row4.CreateCell(1);cell41.CellStyle = style;
        NPOI.SS.UserModel.ICell cell42 = row4.CreateCell(2); cell42.SetCellValue("整面"); cell42.CellStyle = style;
        NPOI.SS.UserModel.ICell cell43 = row4.CreateCell(3); cell43.CellStyle = style;
        NPOI.SS.UserModel.ICell cell44 = row4.CreateCell(4); cell44.SetCellValue("上部"); cell44.CellStyle = style;
        NPOI.SS.UserModel.ICell cell45 = row4.CreateCell(5); cell45.CellStyle = style;
        NPOI.SS.UserModel.ICell cell46 = row4.CreateCell(6); cell46.SetCellValue("中部"); cell46.CellStyle = style;
        NPOI.SS.UserModel.ICell cell47 = row4.CreateCell(7); cell47.CellStyle = style;
        NPOI.SS.UserModel.ICell cell48 = row4.CreateCell(8); cell48.SetCellValue("下部"); cell48.CellStyle = style;
        NPOI.SS.UserModel.ICell cell49 = row4.CreateCell(9); cell49.CellStyle = style;

        NPOI.SS.UserModel.IRow row5 = sheet.CreateRow(5);
        NPOI.SS.UserModel.ICell cell50 = row5.CreateCell(0); cell50.CellStyle = style;
        NPOI.SS.UserModel.ICell cell51 = row5.CreateCell(1); cell51.CellStyle = style;
        NPOI.SS.UserModel.ICell cell52 = row5.CreateCell(2); cell52.SetCellValue("最大"); cell52.CellStyle = style;
        NPOI.SS.UserModel.ICell cell53 = row5.CreateCell(3); cell53.SetCellValue("最小"); cell53.CellStyle = style;
        NPOI.SS.UserModel.ICell cell54 = row5.CreateCell(4); cell54.SetCellValue("最大"); cell54.CellStyle = style;
        NPOI.SS.UserModel.ICell cell55 = row5.CreateCell(5); cell55.SetCellValue("最小"); cell55.CellStyle = style;
        NPOI.SS.UserModel.ICell cell56 = row5.CreateCell(6); cell56.SetCellValue("最大"); cell56.CellStyle = style;
        NPOI.SS.UserModel.ICell cell57 = row5.CreateCell(7); cell57.SetCellValue("最小"); cell57.CellStyle = style;
        NPOI.SS.UserModel.ICell cell58 = row5.CreateCell(8); cell58.SetCellValue("最大"); cell58.CellStyle = style;
        NPOI.SS.UserModel.ICell cell59 = row5.CreateCell(9); cell59.SetCellValue("最小"); cell59.CellStyle = style;
        NPOI.SS.UserModel.IRow row6 = sheet.CreateRow(6);
        NPOI.SS.UserModel.ICell cell61 = row6.CreateCell(1); cell61.CellStyle = style;
        NPOI.SS.UserModel.ICell cell62 = row6.CreateCell(2); cell62.SetCellValue("平均"); cell62.CellStyle = style;
        NPOI.SS.UserModel.ICell cell63 = row6.CreateCell(3); cell63.CellStyle = style;
        NPOI.SS.UserModel.ICell cell64 = row6.CreateCell(4); cell64.SetCellValue("平均"); cell64.CellStyle = style;
        NPOI.SS.UserModel.ICell cell65 = row6.CreateCell(5); cell65.CellStyle = style;
        NPOI.SS.UserModel.ICell cell66 = row6.CreateCell(6); cell66.SetCellValue("平均"); cell66.CellStyle = style;
        NPOI.SS.UserModel.ICell cell67 = row6.CreateCell(7); cell67.CellStyle = style;
        NPOI.SS.UserModel.ICell cell68 = row6.CreateCell(8); cell68.SetCellValue("平均"); cell68.CellStyle = style;
        NPOI.SS.UserModel.ICell cell69 = row6.CreateCell(9); cell69.CellStyle = style;
        NPOI.SS.UserModel.IRow row7 = sheet.CreateRow(7);
        NPOI.SS.UserModel.ICell cell70 = row7.CreateCell(0); cell70.CellStyle = style;
        NPOI.SS.UserModel.ICell cell71 = row7.CreateCell(1); cell71.CellStyle = style;
        NPOI.SS.UserModel.ICell cell72 = row7.CreateCell(2); cell72.SetCellValue(zhengmianmax); cell72.CellStyle = style;
        NPOI.SS.UserModel.ICell cell73 = row7.CreateCell(3); cell73.SetCellValue(zhengmianmin); cell73.CellStyle = style;
        NPOI.SS.UserModel.ICell cell74 = row7.CreateCell(4); cell74.SetCellValue(shangbumax); cell74.CellStyle = style;
        NPOI.SS.UserModel.ICell cell75 = row7.CreateCell(5); cell75.SetCellValue(shangbumin); cell75.CellStyle = style;
        NPOI.SS.UserModel.ICell cell76 = row7.CreateCell(6); cell76.SetCellValue(zhongbumax); cell76.CellStyle = style;
        NPOI.SS.UserModel.ICell cell77 = row7.CreateCell(7); cell77.SetCellValue(zhongbumin); cell77.CellStyle = style;
        NPOI.SS.UserModel.ICell cell78 = row7.CreateCell(8); cell78.SetCellValue(xiabumax); cell78.CellStyle = style;
        NPOI.SS.UserModel.ICell cell79 = row7.CreateCell(9); cell79.SetCellValue(xiabumin); cell79.CellStyle = style;
        NPOI.SS.UserModel.IRow row8 = sheet.CreateRow(8);
        NPOI.SS.UserModel.ICell cell80 = row8.CreateCell(0); cell80.CellStyle = style;
        NPOI.SS.UserModel.ICell cell81 = row8.CreateCell(1); cell81.CellStyle = style;
        NPOI.SS.UserModel.ICell cell82 = row8.CreateCell(2); cell82.SetCellValue(zhengmianavg); cell82.CellStyle = style;
        NPOI.SS.UserModel.ICell cell83 = row8.CreateCell(3); cell83.CellStyle = style;
        NPOI.SS.UserModel.ICell cell84 = row8.CreateCell(4); cell84.SetCellValue(shangbuavg); cell84.CellStyle = style;
        NPOI.SS.UserModel.ICell cell85 = row8.CreateCell(5); cell85.CellStyle = style;
        NPOI.SS.UserModel.ICell cell86 = row8.CreateCell(6); cell86.SetCellValue(zhongbuavg); cell86.CellStyle = style;
        NPOI.SS.UserModel.ICell cell87 = row8.CreateCell(7); cell87.CellStyle = style;
        NPOI.SS.UserModel.ICell cell88 = row8.CreateCell(8); cell88.SetCellValue(xiabuavg); cell88.CellStyle = style;
        NPOI.SS.UserModel.ICell cell89 = row8.CreateCell(9); cell89.CellStyle = style;
        //第5行
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 10, 0, 0));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 10, 1, 1));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 10, 2, 3));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 4, 5));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 6, 7));
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 8, 9));
        NPOI.SS.UserModel.IRow row9 = sheet.CreateRow(9);
        NPOI.SS.UserModel.ICell cell90 = row9.CreateCell(0); cell90.SetCellValue("分机编号"); cell90.CellStyle = style;
        NPOI.SS.UserModel.ICell cell91 = row9.CreateCell(1); cell91.SetCellValue("支架编号"); cell91.CellStyle = style;
        NPOI.SS.UserModel.ICell cell92 = row9.CreateCell(2); cell92.SetCellValue("安装位置"); cell92.CellStyle = style;
        NPOI.SS.UserModel.ICell cell93 = row9.CreateCell(3); cell93.CellStyle = style;
        NPOI.SS.UserModel.ICell cell94 = row9.CreateCell(4); cell94.SetCellValue("工作阻力"); cell94.CellStyle = style;
        NPOI.SS.UserModel.ICell cell95 = row9.CreateCell(5); cell95.CellStyle = style;
        NPOI.SS.UserModel.ICell cell96 = row9.CreateCell(6); cell96.SetCellValue("初撑力"); cell96.CellStyle = style;
        NPOI.SS.UserModel.ICell cell97 = row9.CreateCell(7); cell97.CellStyle = style;
        NPOI.SS.UserModel.ICell cell98 = row9.CreateCell(8); cell98.SetCellValue("末阻力"); cell98.CellStyle = style;
        NPOI.SS.UserModel.ICell cell99 = row9.CreateCell(9); cell99.CellStyle = style;
        NPOI.SS.UserModel.IRow row10 = sheet.CreateRow(10);
        NPOI.SS.UserModel.ICell cell100 = row10.CreateCell(0); cell100.CellStyle = style;
        NPOI.SS.UserModel.ICell cell101 = row10.CreateCell(1); cell101.CellStyle = style;
        NPOI.SS.UserModel.ICell cell102 = row10.CreateCell(2); cell102.CellStyle = style;
        NPOI.SS.UserModel.ICell cell103 = row10.CreateCell(3); cell103.CellStyle = style;
        NPOI.SS.UserModel.ICell cell104 = row10.CreateCell(4); cell104.SetCellValue("最大"); cell104.CellStyle = style;
        NPOI.SS.UserModel.ICell cell105 = row10.CreateCell(5); cell105.SetCellValue("平均"); cell105.CellStyle = style;
        NPOI.SS.UserModel.ICell cell106 = row10.CreateCell(6); cell106.SetCellValue("最大"); cell106.CellStyle = style;
        NPOI.SS.UserModel.ICell cell107 = row10.CreateCell(7); cell107.SetCellValue("平均"); cell107.CellStyle = style;
        NPOI.SS.UserModel.ICell cell108 = row10.CreateCell(8); cell108.SetCellValue("最大"); cell108.CellStyle = style;
        NPOI.SS.UserModel.ICell cell109 = row10.CreateCell(9); cell109.SetCellValue("平均"); cell109.CellStyle = style;
        //
        string jiancefenxi = "支架编号：";
        for (int i = 11; i < ds.Tables[0].Rows.Count+11; i++)
        {
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(i, i, 2, 3));
            NPOI.SS.UserModel.IRow row11 = sheet.CreateRow(i);
            NPOI.SS.UserModel.ICell cell110 = row11.CreateCell(0); cell110.SetCellValue(ds.Tables[0].Rows[i-11]["sensorNo"].ToString()); cell110.CellStyle = style;
            NPOI.SS.UserModel.ICell cell111 = row11.CreateCell(1); cell111.SetCellValue(ds.Tables[0].Rows[i-11]["bracketno"].ToString()); cell111.CellStyle = style;
            NPOI.SS.UserModel.ICell cell112 = row11.CreateCell(2); cell112.SetCellValue(ds.Tables[0].Rows[i-11]["distance"].ToString()); cell112.CellStyle = style;
            NPOI.SS.UserModel.ICell cell113 = row11.CreateCell(3); cell113.CellStyle = style;
            NPOI.SS.UserModel.ICell cell114 = row11.CreateCell(4); cell114.SetCellValue(ds.Tables[0].Rows[i-11]["zlmax"].ToString()); cell114.CellStyle = style;
            NPOI.SS.UserModel.ICell cell115 = row11.CreateCell(5); cell115.SetCellValue(Convert.ToDecimal(ds.Tables[0].Rows[i-11]["zlavg"].ToString()==""?"0.00":ds.Tables[0].Rows[i-11]["zlavg"].ToString()).ToString("0.00")); cell115.CellStyle = style;
            NPOI.SS.UserModel.ICell cell116 = row11.CreateCell(6); cell116.SetCellValue(ds.Tables[0].Rows[i-11]["cclmax"].ToString()); cell116.CellStyle = style;
            NPOI.SS.UserModel.ICell cell117 = row11.CreateCell(7); cell117.SetCellValue(Convert.ToDecimal(ds.Tables[0].Rows[i-11]["zlavg"].ToString()==""?"0.00":ds.Tables[0].Rows[i-11]["cclavg"].ToString()).ToString("0.00")); cell117.CellStyle = style;
            NPOI.SS.UserModel.ICell cell118 = row11.CreateCell(8); cell118.SetCellValue(ds.Tables[0].Rows[i-11]["mzlmax"].ToString()); cell118.CellStyle = style;
            NPOI.SS.UserModel.ICell cell119 = row11.CreateCell(9); cell119.SetCellValue(Convert.ToDecimal(ds.Tables[0].Rows[i-11]["zlavg"].ToString()==""?"0.00":ds.Tables[0].Rows[i-11]["mzlavg"].ToString()).ToString("0.00")); cell119.CellStyle = style;
            if (ViewState["yujingzhi"].ToString() != "0") {
                decimal fenxi = Convert.ToDecimal(ds.Tables[0].Rows[i - 11]["zlavg"].ToString() == "" ? "0.00" : ds.Tables[0].Rows[i - 11]["zlavg"].ToString());
                decimal yujingzhi = Convert.ToDecimal(ViewState["yujingzhi"].ToString());
                if (fenxi >= yujingzhi) {
                    jiancefenxi += ""+ds.Tables[0].Rows[i-11]["bracketno"].ToString()+"超压（"+fenxi.ToString("0.00")+"），";
                }
            }
            
        }
        jiancefenxi += "请注意观察";
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(ds.Tables[0].Rows.Count + 11, ds.Tables[0].Rows.Count + 11, 0, 9));
        NPOI.SS.UserModel.IRow rowa = sheet.CreateRow(ds.Tables[0].Rows.Count + 11);
        NPOI.SS.UserModel.ICell cella0 = rowa.CreateCell(0); cella0.SetCellValue("监测分析结论：" ); cella0.CellStyle = styleleft;
        NPOI.SS.UserModel.ICell cella1 = rowa.CreateCell(1); cella1.CellStyle = style;
        NPOI.SS.UserModel.ICell cella2 = rowa.CreateCell(2); cella2.CellStyle = style;
        NPOI.SS.UserModel.ICell cella3 = rowa.CreateCell(3); cella3.CellStyle = style;
        NPOI.SS.UserModel.ICell cella4 = rowa.CreateCell(4); cella4.CellStyle = style;
        NPOI.SS.UserModel.ICell cella5 = rowa.CreateCell(5); cella5.CellStyle = style;
        NPOI.SS.UserModel.ICell cella6 = rowa.CreateCell(6); cella6.CellStyle = style;
        NPOI.SS.UserModel.ICell cella7 = rowa.CreateCell(7); cella7.CellStyle = style;
        NPOI.SS.UserModel.ICell cella8 = rowa.CreateCell(8); cella8.CellStyle = style;
        NPOI.SS.UserModel.ICell cella9 = rowa.CreateCell(9); cella9.CellStyle = style;
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(ds.Tables[0].Rows.Count + 12, ds.Tables[0].Rows.Count + 12, 0, 9));
        NPOI.SS.UserModel.IRow rowb = sheet.CreateRow(ds.Tables[0].Rows.Count + 12);
        rowb.Height = 2400;
        NPOI.SS.UserModel.ICell cellb0 = rowb.CreateCell(0); cellb0.SetCellValue("" + jiancefenxi); cellb0.CellStyle = styleleftcenter;
        NPOI.SS.UserModel.ICell cellb1 = rowb.CreateCell(1); cellb1.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb2 = rowb.CreateCell(2); cellb2.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb3 = rowb.CreateCell(3); cellb3.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb4 = rowb.CreateCell(4); cellb4.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb5 = rowb.CreateCell(5); cellb5.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb6 = rowb.CreateCell(6); cellb6.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb7 = rowb.CreateCell(7); cellb7.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb8 = rowb.CreateCell(8); cellb8.CellStyle = style;
        NPOI.SS.UserModel.ICell cellb9 = rowb.CreateCell(9); cellb9.CellStyle = style;
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(ds.Tables[0].Rows.Count + 13, ds.Tables[0].Rows.Count + 13, 0, 9));
        NPOI.SS.UserModel.IRow rowc = sheet.CreateRow(ds.Tables[0].Rows.Count + 13);
        NPOI.SS.UserModel.ICell cellc0 = rowc.CreateCell(0); cellc0.SetCellValue("区队意见："); cellc0.CellStyle = styleleft;
        NPOI.SS.UserModel.ICell cellc1 = rowc.CreateCell(1); cellc1.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc2 = rowc.CreateCell(2); cellc2.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc3 = rowc.CreateCell(3); cellc3.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc4 = rowc.CreateCell(4); cellc4.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc5 = rowc.CreateCell(5); cellc5.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc6 = rowc.CreateCell(6); cellc6.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc7 = rowc.CreateCell(7); cellc7.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc8 = rowc.CreateCell(8); cellc8.CellStyle = style;
        NPOI.SS.UserModel.ICell cellc9 = rowc.CreateCell(9); cellc9.CellStyle = style;
        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(ds.Tables[0].Rows.Count + 14, ds.Tables[0].Rows.Count + 14, 0, 9));
        NPOI.SS.UserModel.IRow rowd = sheet.CreateRow(ds.Tables[0].Rows.Count + 14);
        rowd.Height = 2400;
        NPOI.SS.UserModel.ICell celld0 = rowd.CreateCell(0); celld0.SetCellValue("               领导签字：____________部门签字：____________报表人：____________"); celld0.CellStyle = styleleft;
        NPOI.SS.UserModel.ICell celld1 = rowd.CreateCell(1); celld1.CellStyle = style;
        NPOI.SS.UserModel.ICell celld2 = rowd.CreateCell(2); celld2.CellStyle = style;
        NPOI.SS.UserModel.ICell celld3 = rowd.CreateCell(3); celld3.CellStyle = style;
        NPOI.SS.UserModel.ICell celld4 = rowd.CreateCell(4); celld4.CellStyle = style;
        NPOI.SS.UserModel.ICell celld5 = rowd.CreateCell(5); celld5.CellStyle = style;
        NPOI.SS.UserModel.ICell celld6 = rowd.CreateCell(6); celld6.CellStyle = style;
        NPOI.SS.UserModel.ICell celld7 = rowd.CreateCell(7); celld7.CellStyle = style;
        NPOI.SS.UserModel.ICell celld8 = rowd.CreateCell(8); celld8.CellStyle = style;
        NPOI.SS.UserModel.ICell celld9 = rowd.CreateCell(9); celld9.CellStyle = style;
        // 写入到客户端    
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        book.Write(ms);
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
        Response.BinaryWrite(ms.ToArray());
        book = null;
        ms.Close();
        ms.Dispose();
    }
}