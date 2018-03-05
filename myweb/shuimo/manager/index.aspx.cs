using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class manager_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                //获取用户名
                bind_tb();
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }

        }
    }

    private void bind_tb()
    {
        string man_id = Request.Cookies[Cookie.ComplanyId].Value;
        string sql = "select m.man_name,isnull(r.roleName,'') roleName from manager_info m left join role_info r on r.id=m.roleId where m.man_id='" + man_id + "'";
            DataTable dt = DB.ExecuteSqlDataTable(sql, null);
            if (dt.Rows.Count > 0)
            {
                lbl_name.Text=dt.Rows[0]["man_name"].ToString();
                lbl_title.Text = dt.Rows[0]["roleName"].ToString();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        
    }

    protected void lbtn_logout_Click(object sender, EventArgs e)
    {
        if (Request.Cookies[Cookie.ComplanyId] != null)
        {
            HttpCookie httpcookie = Request.Cookies[Cookie.ComplanyId];
            httpcookie.Expires = DateTime.Now.AddDays(-1);
            Response.AppendCookie(httpcookie);

            if (Request.Cookies[Cookie.ComplanyId] != null)
            {

                HttpCookie hc3 = Request.Cookies[Cookie.ComplanyId];

                hc3.Expires = DateTime.Now.AddDays(-1);
                hc3.Values.Clear();
                Response.Cookies.Set(hc3);


            }
            SystemTool.AlertShow_Refresh2(this, "Login.aspx");
        }
        else
        {
            SystemTool.AlertShow_Refresh2(this, "Login.aspx");
        }
    }
    protected void BindInfo() {
        string sql = "select moduleId from role_info where state='启用' and id=(select roleId from manager_info where man_id=" + Request.Cookies[Cookie.ComplanyId].Value + ")";
        string moduleId= DB.ExecuteSqlValue(sql, null);
        if (moduleId != "" && moduleId != "no")
        {
            ViewState["moduleId"] = " and id in(" + moduleId + ") ";
            sql = "select * from module_info where parentId=0" + ViewState["moduleId"] + " order by paixu";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpt.DataSource = ds.Tables[0];
            rpt.DataBind();
        }
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
            Label lb = e.Item.FindControl("rptId") as Label;
            Repeater rpta = e.Item.FindControl("rpta") as Repeater;
            
string sql = "select * from module_info where parentId=" + lb.Text + ViewState["moduleId"]+" order by paixu";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpta.DataSource = ds.Tables[0];
            rpta.DataBind();
        }
    }
}