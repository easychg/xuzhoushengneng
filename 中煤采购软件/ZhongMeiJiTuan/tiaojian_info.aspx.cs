using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class tiaojian_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {

                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    private void BindInfo()
    {
        string sql = "select * from tiaojian_info";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            weidaohuo.Text = ds.Tables[0].Rows[0]["weidaohuo"].ToString();
            chaoqi.Text = ds.Tables[0].Rows[0]["chaoqi"].ToString();

        }
    }
    protected void lbtn_save_Click(object sender, EventArgs e)
    {
        string wdh = weidaohuo.Text.Trim();
        if (wdh == "") {
            wdh = "0";
        }
        string cq = chaoqi.Text.Trim();
        if (cq == "") {
            cq = "0";
        }

        string sql = "update tiaojian_info set chaoqi="+cq+",weidaohuo="+wdh+"";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            SystemTool.AlertShow_Refresh1(this, "保存成功", "tiaojian_info.aspx");
        }
        else
        {
            SystemTool.AlertShow(this, "保存失败");
            return;
        }
    }
}