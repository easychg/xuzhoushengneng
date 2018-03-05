using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class kuangjingxinxishezhi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
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
        //测区
        string sql = "select * from AreaInfo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_AreaInfo.DataSource = ds;
            ddl_AreaInfo.DataValueField = "areaname";
            ddl_AreaInfo.DataTextField = "areaname";
            ddl_AreaInfo.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            ddl_AreaInfo.Items.Insert(0, item);

            ddl_cequ2.DataSource = ds;
            ddl_cequ2.DataValueField = "areaname";
            ddl_cequ2.DataTextField = "areaname";
            ddl_cequ2.DataBind();
            ListItem item2 = new ListItem("--请选择--", "0");
            ddl_cequ2.Items.Insert(0, item2);
        }
        
    }

    private void BindInfo()
    {
        //测区
        string sql = "select * from AreaInfo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_cequ.DataSource = ds.Tables[0];
        rpt_cequ.DataBind();
        //工作面
        sql = "select * from workfaceinfo";
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_workface.DataSource = ds.Tables[0];
        rpt_workface.DataBind();
        //巷道
        sql = "select * from roadinfo";
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdao.DataSource = ds.Tables[0];
        rpt_xiangdao.DataBind();
    }
    protected void lbtn_cequ_Click(object sender, EventArgs e)
    {
        string aname = txtcequ.Value;
        if (aname == "") {
            SystemTool.AlertShow(this, "测区名称不能为空");
            return;
        }
        string sqlck = "select * from areainfo where areaname='" + aname + "'";
        DataSet dsck = DB.ExecuteSqlDataSet(sqlck, null);
        if (dsck.Tables[0].Rows.Count > 0)
        {
            SystemTool.AlertShow(this, "测区名称已存在");
            return;
        }
        string sql = "insert into areainfo (areaname) values('"+aname+"')";
        int result =DB.ExecuteSql(sql,null);
        if (result > 0)
        {
            SystemTool.JavascriptShow(this, "changclass1()");
            sql = "select * from AreaInfo";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpt_cequ.DataSource = ds.Tables[0];
            rpt_cequ.DataBind();
            txtcequ.Value = "";
            BindData();
        }
        else {
            SystemTool.AlertShow(this, "操作失败");
        }
    }
    protected void lbtn_gongzuomian_Click(object sender, EventArgs e) {
        string aname = txtgongzuomian.Value;
        string cequ = ddl_AreaInfo.SelectedValue;
        if (cequ == "0") {
            SystemTool.AlertShow(this, "测区名称不能为空");
            return;
        }
        if (aname == "")
        {
            SystemTool.AlertShow(this, "工作面名称不能为空");
            return;
        }
        string sqlck = "select * from workfaceinfo where areaname='" + cequ + "' and workfacename='"+aname+"'";
        DataSet dsck = DB.ExecuteSqlDataSet(sqlck, null);
        if (dsck.Tables[0].Rows.Count > 0)
        {
            SystemTool.AlertShow(this, "工作面名称已存在");
            SystemTool.JavascriptShow(this, "changclass22()");
            return;
        }
        string sql = "insert into workfaceinfo (areaname,workfacename) values('"+cequ+"','" + aname + "')";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            SystemTool.JavascriptShow(this, "changclass2()");
            sql = "select * from workfaceinfo";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpt_workface.DataSource = ds.Tables[0];
            rpt_workface.DataBind();
            ddl_AreaInfo.SelectedValue = "0";
            txtgongzuomian.Value = "";
        }
        else
        {
            SystemTool.AlertShow(this, "操作失败");
        }
    }
    protected void lbtn_xiangdao_Click(object sender, EventArgs e) {
        string aname = txtxiangdao.Value;
        string cequ = ddl_cequ2.SelectedValue;
        if (cequ == "0")
        {
            SystemTool.AlertShow(this, "测区名称不能为空");
            return;
        }
        if (aname == "")
        {
            SystemTool.AlertShow(this, "巷道名称不能为空");
            return;
        }
        string sqlck = "select * from roadinfo where areaname='" + cequ + "' and roadname='" + aname + "'";
        DataSet dsck = DB.ExecuteSqlDataSet(sqlck, null);
        if (dsck.Tables[0].Rows.Count > 0)
        {
            SystemTool.AlertShow(this, "巷道名称已存在");
            SystemTool.JavascriptShow(this, "changclass33()");
            return;
        }
        string sql = "insert into roadinfo (areaname,roadname) values('" + cequ + "','" + aname + "')";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            SystemTool.JavascriptShow(this, "changclass3()");
            sql = "select * from roadinfo";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rpt_xiangdao.DataSource = ds.Tables[0];
            rpt_xiangdao.DataBind();
            ddl_cequ2.SelectedValue = "0";
            txtxiangdao.Value = "";
        }
        else
        {
            SystemTool.AlertShow(this, "操作失败");
        }
    }
}