using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class fenzhanjiduankoushezhi : System.Web.UI.Page
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
        string sql = "select * from commport where  " + ViewState["search"] + "";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
      
    }
    
    protected void search_Click(object sender, EventArgs e)
    {

        string suoshucequ = ddl_AreaInfo.SelectedValue;
        string mianxiangxinxi = ddl_WorkfaceInfo.SelectedValue;
        string fenzhanhao = ddl_fenzhanhao.SelectedValue;
        string duankouhao = ddl_duankouhao.SelectedValue;
        string botelv=ddl_botelv.SelectedValue;
        string shiyongzhuangtai=ddl_shiyongzhuangtai.SelectedValue;
        ViewState["search"] = " 1=1";
        if (suoshucequ!="0")
        {
            ViewState["search"] += " and areaname='" + suoshucequ + "'";
        }
        if (mianxiangxinxi != "0")
        {
            ViewState["search"] += " and areaface='" + mianxiangxinxi + "'";
        }
        if (fenzhanhao != "0")
        {
            ViewState["search"] += " and stationNo='"+fenzhanhao+"'";
        }
        if (botelv != "0")
        {
            ViewState["search"] += " and baudrate='" + botelv + "'";
        }
        if (duankouhao != "0")
        {
            ViewState["search"] += " and commno='" + duankouhao + "'";
        }
        if (shiyongzhuangtai != "0")
        {
            ViewState["search"] += " and usestate='" + shiyongzhuangtai + "'";
        }
        BindInfo();
    }
    protected void lbtn_delete_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rptlist.Items.Count; i++)
        {
            CheckBox ckb = rptlist.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true)
            {
                ids += ckb.ToolTip + ",";
            }
        }
        if (ids.Length > 0)
        {
            ids = ids.Substring(0, ids.Length - 1);
        }
        else
        {
            SystemTool.AlertShow(this, "请选择需要删除的数据");
            return;
        }
        string sql = "delete from commport where stationno in(" + ids + ")";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            BindInfo();
            SystemTool.AlertShow(this, "删除成功");
        }
        else
        {
            SystemTool.AlertShow(this, "删除失败");
        }
    }
}