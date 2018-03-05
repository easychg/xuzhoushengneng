using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class huozhusuoliangzaixianjiance : System.Web.UI.Page
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
                BindInfo();
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
            ListItem list = new ListItem("--请选择--", "0");
            ddl_cequ.Items.Insert(0, list);
        }
        //sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ds.Tables[0].Rows[0]["areaName"].ToString() + "'";//工作面
        //ds = DB.ExecuteSqlDataSet(sql, null);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddl_gongzuomian.Items.Clear();
        //    ddl_gongzuomian.DataSource = ds.Tables[0];
        //    ddl_gongzuomian.DataTextField = "workfaceName";
        //    ddl_gongzuomian.DataValueField = "workfaceName";
        //    ddl_gongzuomian.DataBind();
        //}
    }

    private void BindInfo()
    {


    }
    protected void ddl_cequ_changed(object sender, EventArgs e)
    {
        txtsuoliangyujingzhi.Value = "0";
        txtsuoliangbaojingzhi.Value ="0";
        txtchuanganqizushu.Value="0";
        string sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ddl_cequ.SelectedValue + "'";//工作面
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomian.Items.Clear();
            ddl_gongzuomian.DataSource = ds.Tables[0];
            ddl_gongzuomian.DataTextField = "workfaceName";
            ddl_gongzuomian.DataValueField = "workfaceName";
            ddl_gongzuomian.DataBind();
            string workfacename = ds.Tables[0].Rows[0]["workfaceName"].ToString();
            sql = "select * from PressurePar where areaname='" + ddl_cequ.SelectedValue + "' and facename='" + workfacename + "'";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                sql = "select * from HuoZhuPar where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + workfacename + "'";
                ds = DB.ExecuteSqlDataSet(sql, null);
                if (ds.Tables[0].Rows.Count > 0) {
                    txtsuoliangyujingzhi.Value = ds.Tables[0].Rows[0]["YujingValue"].ToString();
                    txtsuoliangbaojingzhi.Value = ds.Tables[0].Rows[0]["AlarmValue"].ToString();
                }
            }
            sql="select * from HuoZhuSenInfo where areaName='" +ddl_cequ.SelectedValue+ "' and FaceName='"+workfacename +"'";
            ds=DB.ExecuteSqlDataSet(sql,null);
            txtchuanganqizushu.Value=ds.Tables[0].Rows.Count.ToString();

            sql = @"select count(*) as total from HuoZhuSenInfo hzsi
left join HuoZhuNewData hznd on hznd.areaname=hzsi.areaname and hznd.facename=hzsi.facename and hznd.sensorno=hzsi.sensorno
where hzsi.areaname='" + ddl_cequ.SelectedValue + "' and hzsi.facename='" + workfacename + "' ";
            ds = DB.ExecuteSqlDataSet(sql, null);
            decimal total = Convert.ToDecimal(ds.Tables[0].Rows[0]["total"].ToString());
            ddl_page.Items.Clear();
            Int32 p = 16;//每页条数
            int pages = Convert.ToInt32(Math.Ceiling(total / p));
            for (int i = 0; i < pages; i++)
            {
                ListItem list = new ListItem((i + 1).ToString(), "between " + (1 + i * p) + " and " + (i * p + p));
                ddl_page.Items.Add(list);
            }
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);
            ListItem list = new ListItem("1", "between 0 and 0");
            ddl_page.Items.Add(list);
        }
        
        //SystemTool.JavascriptShow(this, "changclass5()");
    }

}