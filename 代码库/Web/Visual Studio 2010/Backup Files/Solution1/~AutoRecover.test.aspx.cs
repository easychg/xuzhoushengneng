using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CoreLibrary;
using System.Data;

namespace Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string uri = "http://www.baidu.com/";
            HttpClient client = new HttpClient();
            string body = await client.GetStringAsync(uri);
            //ExcelHelper excle = new ExcelHelper();
            //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            //Response.BinaryWrite(excle.ExportXLS());
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            if (file.HasFile) {
                string url = Server.MapPath("~/") + file.FileName;
                file.SaveAs(url);
                //ExcelHelper excel = new ExcelHelper();
                DataTable dt = ExcelHelper.Excel2DataTable(url);
                ExcelHelper.ExportXLS(dt,new string[]{"col1","col2","col3"},"导出");
            }
        }
    }
}