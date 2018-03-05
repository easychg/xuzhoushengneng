using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class taizhang_genzong_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {

                if (null != Request.QueryString["dingdanbianhao"])
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
        if (Request.QueryString["dingdanbianhao"].ToString() != "")
        {
            lbl_dingdanbianhao.Text = Request.QueryString["dingdanbianhao"].ToString() + "详情";
            string sql = "select * from taizhang_info where dingdanbianhao='" + Request.QueryString["dingdanbianhao"] + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpt_taizhang.DataSource = ds.Tables[0];
            rpt_taizhang.DataBind();
            for (int i = 0; i < rpt_taizhang.Items.Count; i++) {
                Label taizhang_id = rpt_taizhang.Items[i].FindControl("lblyidaohuo") as Label;
                string sqla = "select sum(daohuo) from jilu_info where taizhang_id=" + taizhang_id.Text;
                string resulta = DB.ExecuteSqlValue(sqla, null);
                if (resulta != "" && resulta != "no")
                {
                    taizhang_id.Text = resulta;
                }
                else
                {
                    taizhang_id.Text = "0";
                }
            }
        }
    }
    protected string isshow() {
        if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
        {
            if (Request.Cookies[Cookie.ComplanyId].Value == "1")
            {
                return "";
            }
            else {
                return "none";
            }
            //return "";
        }
        else {
            return "none";
        }
    }
    
}