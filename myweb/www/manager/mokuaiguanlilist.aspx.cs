using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_mokuaiguanlilist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                init();
                HttpCookie userid = Request.Cookies[Cookie.ComplanyId];
                ViewState["userid"] = userid.Value;
                ViewState["search"] = " 1=1";
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }
    protected void init() {
        string sql = "select id,moduleName from dbo.module_info where parentId=0";
        DataTable dt = DB.ExecuteSqlDataSet(sql, null).Tables[0];
        SystemTool.bindDropDownList(ddlmoduleName, dt, "id", "moduleName", "--请选择--");
    }
    private void BindInfo()
    {
        string sql = "select m.id,m.moduleName mmoduleName,isnull(mm.moduleName,'空') mmmoduleName,case when m.modelHref='' then '空' else m.modelHref end modelHref,m.tubiao tubiao from module_info m left join module_info mm on mm.id=m.parentId and mm.parentId=0 where " + ViewState["search"] + " order by m.paixu,m.parentId";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string userid = e.CommandArgument.ToString();
        if (cmd == "lbtshan")
        {
            string sqlchck = "SELECT	id FROM	dbo.module_info WHERE parentId=" + userid;
            string id = DB.ExecuteSqlValue(sqlchck, null);
            if (id != "" && id != "no")
            {
                SystemTool.AlertShow(this, "此模块中包含其他模块，请先删除其他模块");
                return;
            }
            string sql = "delete from module_info where id=" + userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "删除成功");
                BindInfo();
            }
        }
        if (cmd == "lbtn_edit")
        {
            string sql = "select id,moduleName,parentId,modelHref,paixu,tubiao from module_info where id=" + userid;
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["parentId"].ToString() == "0")
                {
                    ddlmoduleName.SelectedValue = "--";
                }
                else {
                    ddlmoduleName.SelectedValue = ds.Tables[0].Rows[0]["parentId"].ToString();
                }
                
                hidid.Value = ds.Tables[0].Rows[0]["id"].ToString();
                qitatiaojian.Value = ds.Tables[0].Rows[0]["moduleName"].ToString();
                mokuaidizhi.Value = ds.Tables[0].Rows[0]["modelHref"].ToString();
                paixu.Value = ds.Tables[0].Rows[0]["paixu"].ToString();
                tubiao.Value = ds.Tables[0].Rows[0]["tubiao"].ToString();
            }
        }
    }
    protected void search_Click(object sender, EventArgs e)
    {
        ViewState["search"] = " 1=1";
        string qita =SystemTool.NoHtml(qitatiaojian.Value.ToString().Trim());
        string id = ddlmoduleName.SelectedValue;
        string mkdz = mokuaidizhi.Value;
        string px = paixu.Value;
        string tb = tubiao.Value;
        if (qita != "")
        {
            ViewState["search"] += " and m.moduleName like '%" + qita + "%'";
        }
        if (id != "--") {
            ViewState["search"] += " and m.parentId="+id;
        }
        if (mkdz != "")
        {
            ViewState["search"] += " and m.modelHref like '%" + mkdz + "%'";
        }
        if (px != "")
        {
            ViewState["search"] += " and m.paixu =" + px;
        }
        if (tb != "") {
            ViewState["search"] += " and m.tubiao like '%"+tb+"%'";
        }
        BindInfo();
    }
    protected void add_modules(object sender, EventArgs e)
    {
        string bjmkname = qitatiaojian.Value;
        if (bjmkname == "")
        {
            SystemTool.AlertShow(this, "模块名称不能为空");
            return;
        }
        string px = paixu.Value;
        if (px == "")
        {
            SystemTool.AlertShow(this, "模块序号不能为空");
            return;
        }
        if (!Regex.IsMatch(px, @"^[+-]?\d*$"))
        {
            SystemTool.AlertShow(this, "模块序号必须为整数");
            return;
        }
        string tb = tubiao.Value;
        string mkdz = mokuaidizhi.Value;
        string sql = "";
        int result = 0;
        string pid = ddlmoduleName.SelectedValue == "--" ? "0" : ddlmoduleName.SelectedValue;
        if (hidid.Value != "")
        {
            sql = "select id from module_info where moduleName='" + bjmkname + "' and id<>" + hidid.Value;
            string r = DB.ExecuteSqlValue(sql, null);
            if (r != "" && r != "no")
            {
                SystemTool.AlertShow(this, "模块名称已存在");
                return;
            }
            //更新操作

            sql = "update module_info set paixu=" + px + " ,moduleName='" + bjmkname + "',parentId=" + pid + ",modelHref='" + mkdz + "',tubiao='" + tb + "' where id=" + hidid.Value;
            result = DB.ExecuteSql(sql, null);
            hidid.Value = "";
        }
        else
        {
            sql = "select id from module_info where moduleName='" + bjmkname + "'";
            string r = DB.ExecuteSqlValue(sql, null);
            if (r != "" && r != "no")
            {
                SystemTool.AlertShow(this, "模块名称已存在");
                return;
            }
            sql = "insert into module_info (moduleName,parentId,modelHref,paixu,tubiao) values('" + bjmkname + "'," + pid + ",'" + mkdz + "'," + px + ",'" + tb + "')";
            result = DB.ExecuteSql(sql, null);
            hidid.Value = "";
        }

        if (result > 0)
        {
            SystemTool.AlertShow_Refresh(this, "保存成功", "mokuaiguanlilist.aspx");
        }
        else
        {
            SystemTool.AlertShow(this, "保存失败");
            return;
        }
    }
}