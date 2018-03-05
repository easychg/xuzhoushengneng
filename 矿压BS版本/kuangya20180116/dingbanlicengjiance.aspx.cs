using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class dingbanlicengjiance : System.Web.UI.Page
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
        //sql = "SELECT roadName FROM RoadInfo where areaName='" + ds.Tables[0].Rows[0]["areaName"].ToString() + "'";//巷道
        //ds = DB.ExecuteSqlDataSet(sql, null);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    ddl_xiangdao.Items.Clear();
        //    ddl_xiangdao.DataSource = ds.Tables[0];
        //    ddl_xiangdao.DataTextField = "roadName";
        //    ddl_xiangdao.DataValueField = "roadName";
        //    ddl_xiangdao.DataBind();
        //}
    }

    private void BindInfo()
    {


    }
    protected void ddl_cequ_changed(object sender, EventArgs e)
    {
        string sql = "SELECT roadName FROM RoadInfo where areaName='" + ddl_cequ.SelectedValue + "'";//巷道
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_xiangdao.Items.Clear();
            ddl_xiangdao.DataSource = ds.Tables[0];
            ddl_xiangdao.DataTextField = "roadName";
            ddl_xiangdao.DataValueField = "roadName";
            ddl_xiangdao.DataBind();
            string roadwayname = ds.Tables[0].Rows[0]["roadname"].ToString();
            sql = "select * from DisplacementPar where areaname='" + ddl_cequ.SelectedValue + "' and roadwayname='" + roadwayname + "'";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtchuanganqizushu.Value = ds.Tables[0].Rows[0]["sennumber"].ToString();
                txtweiyiyujingzhi.Value = ds.Tables[0].Rows[0]["disyujingvale"].ToString();
                txtweiyibaojingzhi.Value = ds.Tables[0].Rows[0]["displacementalarm"].ToString();
            }
            sql = "select * from DisSenInfo where areaName='" + ddl_cequ.SelectedValue + "' and roadwayName='" + roadwayname + "' order by Location asc ";
            ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0) {
                txtajidianshendu.Value = ds.Tables[0].Rows[0]["pointdeptha"].ToString();
                txtbjidianshendu.Value = ds.Tables[0].Rows[0]["pointdepthb"].ToString();
            }
            sql = @"select count(*) as total from DisNewData dnd
left join DisSenInfo dsi on dsi.sensorNo=dnd.SensorNo and dsi.AreaName=dnd.AreaName and dsi.roadwayName=dnd.roadwayName
where dsi.areaname='" + ddl_cequ.SelectedValue + "' and dsi.roadwayname='" + roadwayname + "' ";
            ds = DB.ExecuteSqlDataSet(sql, null);
            decimal total = Convert.ToDecimal(ds.Tables[0].Rows[0]["total"].ToString());
            ddl_page.Items.Clear();
            Int32 p = 10;//每页条数
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
            ddl_xiangdao.Items.Insert(0, item1);
            ListItem list = new ListItem("1", "between 0 and 0");
            ddl_page.Items.Add(list);
        }
        //SystemTool.JavascriptShow(this, "changclass5()");
    }

}