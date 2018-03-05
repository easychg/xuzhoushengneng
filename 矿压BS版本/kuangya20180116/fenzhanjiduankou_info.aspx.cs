using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class fenzhanjiduankou_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                BindData();
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
        }
        //面巷
        sql = "select * from WorkfaceInfo";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_WorkfaceInfo.DataSource = ds;
            ddl_WorkfaceInfo.DataValueField = "workfacename";
            ddl_WorkfaceInfo.DataTextField = "workfacename";
            ddl_WorkfaceInfo.DataBind();
            ListItem item = new ListItem("--请选择--", "0");
            ddl_WorkfaceInfo.Items.Insert(0, item);
        }
    }
    private void BindInfo()
    {
        if (Request.QueryString["id"].ToString() != "")
        {
            string sql = "select * from commport where stationno=" + Request.QueryString["id"];
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {

                ddl_AreaInfo.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                ddl_AreaInfo.Enabled = false;
                ddl_WorkfaceInfo.SelectedValue = ds.Tables[0].Rows[0]["areaface"].ToString();
                ddl_WorkfaceInfo.Enabled = false; 
                ddl_fenzhanhao.SelectedValue = ds.Tables[0].Rows[0]["stationno"].ToString();
                 ddl_duankouhao.SelectedValue = ds.Tables[0].Rows[0]["commno"].ToString();
                  ddl_botelv.SelectedValue = ds.Tables[0].Rows[0]["baudrate"].ToString();
              ddl_shiyongzhuangtai.SelectedValue = ds.Tables[0].Rows[0]["usestate"].ToString();
            }
        }
    }
    protected void lbtn_save_Click(object sender, EventArgs e)
    {
        string suoshucequ = ddl_AreaInfo.SelectedValue;
        if (suoshucequ == "0") {
            SystemTool.AlertShow(this, "所属测区不能为空");
            return;
        }
        string mianxiangxinxi = ddl_WorkfaceInfo.SelectedValue;
        if (mianxiangxinxi == "0")
        {
            SystemTool.AlertShow(this, "面巷信息不能为空");
            return;
        }
        string fenzhanhao = ddl_fenzhanhao.SelectedValue;
        if (fenzhanhao == "0")
        {
            SystemTool.AlertShow(this, "分站号不能为空");
            return;
        }
        string duankouhao = ddl_duankouhao.SelectedValue;
        if (duankouhao == "0")
        {
            SystemTool.AlertShow(this, "端口号不能为空");
            return;
        }
        string botelv = ddl_botelv.SelectedValue;
        if (botelv == "0")
        {
            SystemTool.AlertShow(this, "波特率不能为空");
            return;
        }
        string shiyongzhuangtai = ddl_shiyongzhuangtai.SelectedValue;
        if (shiyongzhuangtai == "0")
        {
            SystemTool.AlertShow(this, "使用状态不能为空");
            return;
        }

        if (null != Request.QueryString["id"])
        {
            string sqlck = "select * from commport  where areaname='" + suoshucequ + "' and areaface='" + mianxiangxinxi + "' and stationno=" + Request.QueryString["id"];
            DataSet dsck = DB.ExecuteSqlDataSet(sqlck, null);
            if (dsck.Tables[0].Rows.Count <= 0) {
                SystemTool.AlertShow(this, "该分站号不存在");
                return;
            }
            string sql = "update commport set stationno='" + fenzhanhao + "',commno='" + duankouhao + "',baudrate='" + botelv + "',usestate='" + shiyongzhuangtai + "'  where areaname='" + suoshucequ + "' and areaface='" + mianxiangxinxi + "' and stationno=" + Request.QueryString["id"];
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow_Refresh1(this, "保存成功", "fenzhanjiduankoushezhi.aspx");
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
                return;
            }
        }
        else
        {
            string sqlck = "select stationno from commport where stationno='" + fenzhanhao + "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no") {
                SystemTool.AlertShow(this, "该分站号已存在");
                return;
            }
            string sql = "insert into commport (areaname,areaface,stationno,commno,baudrate,usestate) values";
            sql += "('"+suoshucequ+"','"+mianxiangxinxi+"','"+fenzhanhao+"','"+duankouhao+"','"+botelv+"','"+shiyongzhuangtai+"')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow_Refresh1(this, "保存成功", "fenzhanjiduankoushezhi.aspx");
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
                return;
            }
        }
    }
}