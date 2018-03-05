using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class jilu_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {

                if (null != Request.QueryString["id"])
                {
                    BindInfo();
                }
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    private void BindInfo()
    {
        if (Request.QueryString["id"].ToString() != "")
        {
            lbl_dingdanbianhao.Text = "无记录";
            string sql = @"select * from jilu_info j
left join taizhang_info t on t.taizhang_id=j.taizhang_id
left join manager_info m on m.man_id=j.manager_id where j.taizhang_id='" + Request.QueryString["id"] + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpt_taizhang.DataSource = ds.Tables[0];
            rpt_taizhang.DataBind();
            if (ds.Tables[0].Rows.Count > 0) {
                lbl_dingdanbianhao.Text = ds.Tables[0].Rows[0]["dingjiawuzimingcheng"].ToString()+"到货记录";
            }

        }
    }

}