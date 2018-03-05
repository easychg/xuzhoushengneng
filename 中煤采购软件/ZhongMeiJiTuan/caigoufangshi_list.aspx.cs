using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class caigoufangshi_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //ViewState["search"] = " 1=1";
            //BindInfo();
            //export();
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                HttpCookie userid = Request.Cookies[Cookie.ComplanyId];
                ViewState["userid"] = userid.Value;
                ViewState["search"] = " 1=1";
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "login.aspx");
            }
        }
    }

    private void BindInfo()
    {
        string sql = "select * from caigoufangshi where " + ViewState["search"] + " order by addtime desc";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_shiyongdanwei.DataSource = ds.Tables[0];
        rpt_shiyongdanwei.DataBind();
    }
    protected void lbtn_search_Click(object sender, EventArgs e)
    {
        ViewState["search"] = " 1=1";
        string sydw = inshiyongdanwei.Value;
        if (sydw != "")
        {
            ViewState["search"] += " and mingcheng like '%" + sydw + "%'";

        }
        BindInfo();
    }
    protected void lbtn_save_Click(object sender, EventArgs e)
    {
        string sydw = inshiyongdanwei.Value;
        if (sydw == "")
        {
            SystemTool.AlertShow(this, "请输入使用单位");
            return;
        }
        string sql = "select count(caigoufangshi_id) from caigoufangshi where mingcheng='" + sydw + "'";
        string result = DB.ExecuteSqlValue(sql, null);
        if (result != "" && result != "no")
        {
            if (result == "0")
            {
                //未查询
                sql = "insert into caigoufangshi(mingcheng) values('" + sydw + "')";
                int r = DB.ExecuteSql(sql, null);
                if (r > 0)
                {
                    SystemTool.AlertShow(this, "保存成功");
                    inshiyongdanwei.Value = "";
                    BindInfo();
                }
                else
                {
                    SystemTool.AlertShow(this, "保存失败");
                }
            }
            else
            {

                SystemTool.AlertShow(this, sydw + "已存在，请勿重复插入");
            }
        }
    }
    protected void lbtn_dels_Click(object sender, EventArgs e)
    {
        string ids = "";
        for (int i = 0; i < rpt_shiyongdanwei.Items.Count; i++)
        {
            CheckBox ckb = rpt_shiyongdanwei.Items[i].FindControl("ckb") as CheckBox;
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
        string sql = "delete from caigoufangshi where caigoufangshi_id in(" + ids + ")";
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