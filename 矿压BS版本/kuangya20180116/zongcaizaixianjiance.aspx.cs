using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class zongcaizaixianjiance : System.Web.UI.Page
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
        }
        string areaname = ds.Tables[0].Rows[0]["areaName"].ToString();
        sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + areaname + "'";//工作面
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomian.Items.Clear();
            ddl_gongzuomian.DataSource = ds.Tables[0];
            ddl_gongzuomian.DataTextField = "workfaceName";
            ddl_gongzuomian.DataValueField = "workfaceName";
            ddl_gongzuomian.DataBind();
        }
        string workfacename = ds.Tables[0].Rows[0]["workfaceName"].ToString();
        sql = @"select count(*) as total from PreSenInfo psi 
left join PreNewData pnd on psi.areaname=pnd.areaname and psi.facename=pnd.facename and psi.sensorno=pnd.sensorno
where psi.areaname='" + areaname + "' and psi.facename='" + ds.Tables[0].Rows[0]["workfaceName"].ToString() + "' ";
        ds = DB.ExecuteSqlDataSet(sql, null);
        decimal total = Convert.ToDecimal(ds.Tables[0].Rows[0]["total"].ToString());
        ddl_page.Items.Clear();
        Int32 p = 20;//每页条数
        string num = ConfigurationManager.ConnectionStrings["sensornum"].ToString();
        if (num != "")
        {
            p = Convert.ToInt32(num);
        }
        int pages = Convert.ToInt32(Math.Ceiling(total / p));
        for (int i = 0; i < pages; i++)
        {
            ListItem list = new ListItem((i + 1).ToString(), "between " + (1 + i * p) + " and " + (i * p + p));
            ddl_page.Items.Add(list);
        }

        
        sql = "select * from PressurePar where areaname='" + areaname + "' and facename='" + workfacename + "'";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            //txtchuanganqizushu.Value = ds.Tables[0].Rows[0][""].ToString();
            txtdiyitongdaojie.Value = ds.Tables[0].Rows[0]["firstconnect"].ToString();
            txtgangjing1.Value = ds.Tables[0].Rows[0]["firstd"].ToString();
            txtdiertongdaojie.Value = ds.Tables[0].Rows[0]["secondconnect"].ToString();
            txtgangjing2.Value = ds.Tables[0].Rows[0]["sencondd"].ToString();
            txtyalishangxian.Value = ds.Tables[0].Rows[0]["pressuremax"].ToString();
            txtyalixiaxian.Value = ds.Tables[0].Rows[0]["pressuremin"].ToString();
            txtbaojingzhi.Value = ds.Tables[0].Rows[0]["pressurealarm"].ToString();
            sql = "select * from PreSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and FaceName='" + workfacename + "' and Type = '" + ddl_yalileixing.SelectedValue + "' order by BracketNo";
            ds = DB.ExecuteSqlDataSet(sql, null);
            txtchuanganqizushu.Value = ds.Tables[0].Rows.Count.ToString();
          
        }
    }

    private void BindInfo()
    {
        
        
    }
    protected void ddl_cequ_changed(object sender, EventArgs e)
    {
        txtdiyitongdaojie.Value = "";
        txtgangjing1.Value = "";
        txtdiertongdaojie.Value = "";
        txtgangjing2.Value = "";
        txtyalishangxian.Value = "";
        txtyalixiaxian.Value = "";
        txtbaojingzhi.Value = "";
        txtchuanganqizushu.Value = "";

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
                //txtchuanganqizushu.Value = ds.Tables[0].Rows[0][""].ToString();
                txtdiyitongdaojie.Value = ds.Tables[0].Rows[0]["firstconnect"].ToString();
                txtgangjing1.Value = ds.Tables[0].Rows[0]["firstd"].ToString();
                txtdiertongdaojie.Value = ds.Tables[0].Rows[0]["secondconnect"].ToString();
                txtgangjing2.Value = ds.Tables[0].Rows[0]["sencondd"].ToString();
                txtyalishangxian.Value = ds.Tables[0].Rows[0]["pressuremax"].ToString();
                txtyalixiaxian.Value = ds.Tables[0].Rows[0]["pressuremin"].ToString();
                txtbaojingzhi.Value = ds.Tables[0].Rows[0]["pressurealarm"].ToString();
                sql = "select * from PreSenInfo where areaName='"+ddl_cequ.SelectedValue+ "' and FaceName='"+workfacename+ "' and Type = '" +ddl_yalileixing.SelectedValue+ "' order by BracketNo";
                ds = DB.ExecuteSqlDataSet(sql, null);
                txtchuanganqizushu.Value = ds.Tables[0].Rows.Count.ToString();
                sql = @"select count(*) as total from PreSenInfo psi 
left join PreNewData pnd on psi.areaname=pnd.areaname and psi.facename=pnd.facename and psi.sensorno=pnd.sensorno
where psi.areaname='" + ddl_cequ.SelectedValue + "' and psi.facename='" + workfacename + "' ";
                ds = DB.ExecuteSqlDataSet(sql, null);
                decimal total = Convert.ToDecimal(ds.Tables[0].Rows[0]["total"].ToString());
                ddl_page.Items.Clear();
                Int32 p = 20;//每页条数
                string num = ConfigurationManager.ConnectionStrings["sensornum"].ToString();
                if (num != "") {
                    p = Convert.ToInt32(num);
                }
                int pages = Convert.ToInt32(Math.Ceiling(total / p));
                for (int i = 0; i < pages; i++)
                {
                    ListItem list = new ListItem((i + 1).ToString(), "between " + (1 + i * p) + " and " + (i * p + p));
                    ddl_page.Items.Add(list);
                }
            }
            else {
                ddl_page.Items.Clear();
                ListItem list = new ListItem("1", "between 0 and 0");
                ddl_page.Items.Add(list);
            }
            
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian.Items.Insert(0, item1);
            ddl_page.Items.Clear();
            ListItem list = new ListItem("1", "between 0 and 0");
            ddl_page.Items.Add(list);
            
        }
        SystemTool.JavascriptShow(this, "requestData()");
    }
    protected void ddl_page_changed(object sender, EventArgs e) {
        SystemTool.JavascriptShow(this, "requestData()");
    }

}