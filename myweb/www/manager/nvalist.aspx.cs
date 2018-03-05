using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class manager_nvalist : System.Web.UI.Page
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
        string sql = "select * from nav_info where parent_id=0 order by paixu";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_nav.Items.Clear();
            ddl_nav.DataSource = ds.Tables[0];
            ddl_nav.DataTextField = "nav_name";
            ddl_nav.DataValueField = "id";
            ddl_nav.DataBind();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_nav.Items.Insert(0, list);
        }
        else {
            ddl_nav.Items.Clear();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_nav.Items.Insert(0, list);
        }
        sql = "select * from muban_info";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_muban.Items.Clear();
            ddl_muban.DataSource = ds.Tables[0];
            ddl_muban.DataTextField = "muban_name";
            ddl_muban.DataValueField = "id";
            ddl_muban.DataBind();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_muban.Items.Insert(0, list);
        }
        else
        {
            ddl_muban.Items.Clear();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_muban.Items.Insert(0, list);
        }
    }
    private void BindInfo()
    {
        string sql = "select * from nav_info order by parent_id,paixu";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rptlist.DataSource = ds.Tables[0];
        rptlist.DataBind();
        string sqlmuban = "select * from muban_info";
        DataSet dsmuban = DB.ExecuteSqlDataSet(sqlmuban, null);
        string sqlnav = "select * from nav_info where parent_id=0 order by paixu";
        DataSet dsnav = DB.ExecuteSqlDataSet(sqlnav, null);
        for (int i = 0; i < rptlist.Items.Count; i++) {
            DropDownList nav0 = rptlist.Items[i].FindControl("ddl_nav0") as DropDownList;
            Label lnav0 = rptlist.Items[i].FindControl("lbl_nav0") as Label;
            if (dsnav.Tables[0].Rows.Count > 0)
            {
                nav0.Items.Clear();
                nav0.DataSource = dsnav.Tables[0];
                nav0.DataTextField = "nav_name";
                nav0.DataValueField = "id";
                nav0.DataBind();
                ListItem list = new ListItem("--请选择--", "0");
                nav0.Items.Insert(0, list);
            }
            else
            {
                nav0.Items.Clear();
                ListItem list = new ListItem("--请选择--", "0");
                nav0.Items.Insert(0, list);
            }
            nav0.SelectedValue = lnav0.Text;
            if (lnav0.Text == "0") nav0.Enabled = false;
            DropDownList ddl = rptlist.Items[i].FindControl("ddl_muban") as DropDownList;
            Label muban = rptlist.Items[i].FindControl("lbl_muban") as Label;
            if (dsmuban.Tables[0].Rows.Count > 0)
            {
                ddl.Items.Clear();
                ddl.DataSource = dsmuban.Tables[0];
                ddl.DataTextField = "muban_name";
                ddl.DataValueField = "id";
                ddl.DataBind();
                ListItem list = new ListItem("--请选择--", "0");
                ddl.Items.Insert(0, list);
            }
            else
            {
                ddl.Items.Clear();
                ListItem list = new ListItem("--请选择--", "0");
                ddl.Items.Insert(0, list);
            }
            ddl.SelectedValue = muban.Text;
            DropDownList ddlshow = rptlist.Items[i].FindControl("ddl_show") as DropDownList;
            Label show = rptlist.Items[i].FindControl("lbl_isshow") as Label;
            ddlshow.SelectedValue = show.Text;
        }
        
    }
    protected void rptlist_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string cmd = e.CommandName.ToString();
        string userid = e.CommandArgument.ToString();
        TextBox name = e.Item.FindControl("txt_name") as TextBox;
        DropDownList muban = e.Item.FindControl("ddl_muban") as DropDownList;
        TextBox paixu = e.Item.FindControl("txt_paixu") as TextBox;
        DropDownList show = e.Item.FindControl("ddl_show") as DropDownList;
        DropDownList nav0 = e.Item.FindControl("ddl_nav0") as DropDownList;
        if (cmd == "lbtshan")
        {
            string sql = "delete from nav_info where id=" + userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "删除成功");
                BindInfo();
            }
        }
        if (cmd == "lbtn_edit")
        {
            if (name.Text == "")
            {
                SystemTool.AlertShow(this, "导航栏名称不能为空");
            }
            if (paixu.Text == "")
            {
                SystemTool.AlertShow(this, "排序不能为空");
                return;
            }
            if (!SystemTool.IsInt(paixu.Text))
            {
                paixu.Text = "0";
            }
            string sql = "select id from nav_info where nav_name='" + name.Text + "'";
            string r = DB.ExecuteSqlValue(sql, null);
            if (r != "" && r != "no")
            {
                if (r != userid)
                {
                    SystemTool.AlertShow(this, "导航栏名称已存在");
                    return;
                }
            }
            sql = "update nav_info set nav_name='"+name.Text+"',muban_id='"+muban.SelectedValue+"',paixu='"+paixu.Text+"',isshow='"+show.SelectedValue+"',parent_id='"+nav0.SelectedValue+"' where id=" + userid;
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow_Refresh(this, "保存成功", "nvalist.aspx");
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
            }
        }
    }
    protected void add_modules(object sender, EventArgs e)
    {
        string pid = ddl_nav.SelectedValue;
        string muban_id = ddl_muban.SelectedValue;
        //if (muban_id == "0") {
        //    SystemTool.AlertShow(this, "请选择模板");
        //    return;
        //}
        string name = daohanglanmingcheng.Value;
        if (name == "")
        {
            SystemTool.AlertShow(this, "导航栏名称不能为空");
            return;
        }
        string px = xuhao.Value;
        if (px == "")
        {
            SystemTool.AlertShow(this, "序号不能为空");
            return;
        }
        if (!SystemTool.IsInt(px)) {
            px = "0";
        }
        string isshow = ddl_show.SelectedValue;
        string sql = "select id from nav_info where nav_name='" + name + "'";
        string r = DB.ExecuteSqlValue(sql, null);
        if (r != "" && r != "no")
        {
            SystemTool.AlertShow(this, "导航栏名称已存在");
            return;
        }
        sql = "insert into nav_info(nav_name,muban_id,parent_id,paixu,isshow) values('"+name+"','"+muban_id+"','"+pid+"','"+px+"','"+isshow+"')";
        int result = DB.ExecuteSql(sql, null);
        if (result > 0)
        {
            SystemTool.AlertShow_Refresh(this, "保存成功", "nvalist.aspx");
        }
        else
        {
            SystemTool.AlertShow(this, "添加失败");
        }
    }
}