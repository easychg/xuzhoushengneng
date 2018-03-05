using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_guanliyuanzu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                init();
                if (null != Request.QueryString["manId"])
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
        //初始化输入框数据
        string sql = "select * from role_info where id=" + SystemTool.NoHtml(Request.QueryString["manId"]);
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtname.Text = ds.Tables[0].Rows[0]["roleName"].ToString();
            txtpassok.Text = ds.Tables[0].Rows[0]["quanxianzhi"].ToString();
            txtbeizhu.Value = ds.Tables[0].Rows[0]["remark"].ToString();
            ddlState.SelectedValue = ds.Tables[0].Rows[0]["state"].ToString();
            paixu.Text = ds.Tables[0].Rows[0]["paixu"].ToString();
        }
        txtname.ReadOnly = true;
        //初始化checkbox数据
        string[] moduleIds = ds.Tables[0].Rows[0]["moduleId"].ToString().Split(',');
        for (int i = 0; i < rpt.Items.Count; i++) {
            CheckBox ckb = rpt.Items[i].FindControl("ckb") as CheckBox;
            Repeater rpta=rpt.Items[i].FindControl("rpta") as Repeater;
            for (int k = 0; k < moduleIds.Length; k++)
            {
                if (ckb.ToolTip == moduleIds[k].ToString())
                {
                    ckb.Checked = true;
                }
            }
            for (int j = 0; j < rpta.Items.Count; j++) {
                CheckBox ckba = rpta.Items[j].FindControl("ckba") as CheckBox;
                for (int k = 0; k < moduleIds.Length; k++) {
                    if (ckb.ToolTip == moduleIds[k].ToString())
                    {
                        ckb.Checked = true;
                    }
                    if (ckba.ToolTip == moduleIds[k].ToString()) {
                        ckba.Checked = true;
                    }
                }
            }
        }
        //string[] diqu_ids = ds.Tables[0].Rows[0]["diqu_ids"].ToString().Split(',');
        //for (int i = 0; i < rpt_sheng.Items.Count; i++)
        //{
        //    CheckBox ckb = rpt_sheng.Items[i].FindControl("ckb") as CheckBox;
        //    Repeater rpta = rpt_sheng.Items[i].FindControl("rpta_sheng") as Repeater;
        //    for (int k = 0; k < diqu_ids.Length; k++)
        //    {
        //        if (ckb.ToolTip == diqu_ids[k].ToString())
        //        {
        //            ckb.Checked = true;
        //        }
        //    }
        //    for (int j = 0; j < rpta.Items.Count; j++)
        //    {
        //        CheckBox ckba = rpta.Items[j].FindControl("ckba") as CheckBox;
        //        for (int k = 0; k < diqu_ids.Length; k++)
        //        {
        //            if (ckb.ToolTip == diqu_ids[k].ToString())
        //            {
        //                ckb.Checked = true;
        //            }
        //            if (ckba.ToolTip == diqu_ids[k].ToString())
        //            {
        //                ckba.Checked = true;
        //            }
        //        }
        //    }
        //}
    }
    protected void init()
    {
        //页面初始化
        string sql = "select * from module_info where parentId=0 order by paixu";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt.DataSource = ds.Tables[0];
        rpt.DataBind();
        //sql = "SELECT diqu_id,diqu_name,isopen FROM diqu_info WHERE diqu_top=0 AND zhen_top=0 ORDER BY diqu_px";
        //ds = DB.ExecuteSqlDataSet(sql, null);
        //rpt_sheng.DataSource = ds.Tables[0];
        //rpt_sheng.DataBind();
        
    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        if (txtname.Text == "")
        {
            SystemTool.AlertShow(this, "组名称不能为空");
            return;
        }
        //string diqu_ids = "";
        //for (int i = 0; i < rpt_sheng.Items.Count; i++)
        //{
        //    CheckBox ckb = rpt_sheng.Items[i].FindControl("ckb") as CheckBox;
        //    if (ckb.Checked == true)
        //    {
        //        diqu_ids += ckb.ToolTip + ",";
        //    }
        //    Repeater rpta = rpt_sheng.Items[i].FindControl("rpta_sheng") as Repeater;
        //    for (int j = 0; j < rpta.Items.Count; j++)
        //    {
        //        CheckBox ckba = rpta.Items[j].FindControl("ckba") as CheckBox;
        //        if (ckba.Checked == true)
        //        {
        //            diqu_ids += ckba.ToolTip + ",";
        //        }
        //    }
        //}
        //if (diqu_ids.Length > 0)
        //{
        //    diqu_ids = diqu_ids.Substring(0, diqu_ids.Length - 1);
        //}
        //else
        //{
        //    SystemTool.AlertShow(this, "请选择地区");
        //    return;
        //}
        string moduleIds = "";
        for (int i = 0; i < rpt.Items.Count; i++) {
            CheckBox ckb = rpt.Items[i].FindControl("ckb") as CheckBox;
            if (ckb.Checked == true) {
                moduleIds += ckb.ToolTip + ",";
            }
            Repeater rpta = rpt.Items[i].FindControl("rpta") as Repeater;
            for (int j = 0; j < rpta.Items.Count; j++) {
                CheckBox ckba = rpta.Items[j].FindControl("ckba") as CheckBox;
                if (ckba.Checked == true) {
                    moduleIds += ckba.ToolTip + ",";
                }
            }
        }
        if (moduleIds.Length > 0)
        {
            moduleIds = moduleIds.Substring(0, moduleIds.Length - 1);
        }
        else {
            SystemTool.AlertShow(this, "请选择组权限");
            return;
        }
        string sql = "";
        int result = 0;
        if (null != Request.QueryString["manId"])
        {
            //更新操作
            sql = "update role_info set roleName='" + SystemTool.NoHtml(txtname.Text) + "',moduleId='" + moduleIds + "',remark='" + SystemTool.NoHtml(txtbeizhu.Value) + "',state='" + ddlState.SelectedValue + "',quanxianzhi=" + SystemTool.NoHtml(txtpassok.Text) + ",paixu=" + SystemTool.NoHtml(paixu.Text) + ",diqu_ids='' where id=" + SystemTool.NoHtml(Request.QueryString["manId"]);
            result = DB.ExecuteSql(sql, null);
        }
        else
        {
            sql = "select id from role_info where roleName='" + SystemTool.NoHtml(txtname.Text) + "'";
            string r = DB.ExecuteSqlValue(sql, null);
            if (r != "" && r != "no")
            {
                SystemTool.AlertShow(this, "组名称已存在");
                return;
            }
            sql = "insert into role_info (roleName,moduleId,remark,state,quanxianzhi,paixu,diqu_ids) values('" + SystemTool.NoHtml(txtname.Text) + "','" + moduleIds + "','" + SystemTool.NoHtml(txtbeizhu.Value) + "','" + ddlState.SelectedValue + "'," + SystemTool.NoHtml(txtpassok.Text) + "," + SystemTool.NoHtml(paixu.Text) + ",'')";
            result = DB.ExecuteSql(sql, null);
        }

        if (result > 0)
        {
            SystemTool.AlertShow_Refresh1(this, "保存成功", "guanliyuanzulist.aspx");
        }
        else
        {
            SystemTool.AlertShow(this, "保存失败");
            return;
        }
    }
    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
            CheckBox ckb = e.Item.FindControl("ckb") as CheckBox;
            Repeater rpta = e.Item.FindControl("rpta") as Repeater;
            string sql = "select * from module_info where parentId="+ckb.ToolTip +" order by paixu";
            rpta.DataSource = DB.ExecuteSqlDataSet(sql, null).Tables[0];
            rpta.DataBind();
        }
    }
    protected void rpt_sheng_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox ckb = e.Item.FindControl("ckb") as CheckBox;
            Repeater rpta = e.Item.FindControl("rpta_sheng") as Repeater;
            string sql = "SELECT diqu_id,diqu_name,isopen FROM diqu_info WHERE diqu_top=" + ckb.ToolTip + " AND zhen_top=0  ORDER BY  diqu_px";
            rpta.DataSource = DB.ExecuteSqlDataSet(sql, null).Tables[0];
            rpta.DataBind();
        }
    }
}