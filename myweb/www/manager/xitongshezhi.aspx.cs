using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class manager_xitongshezhi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    private void BindInfo()
    {
        string sql = "select * from sys_info ";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string title = ds.Tables[0].Rows[0]["title"].ToString();
            txttitle.Text = title;
            string keywords = ds.Tables[0].Rows[0]["keywords"].ToString();
            txtkeywords.Text = keywords;
            string description = ds.Tables[0].Rows[0]["description"].ToString();
            txtdescription.Text = description;
            string beian = ds.Tables[0].Rows[0]["beian"].ToString();
            txtbeian.Value = beian;
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        string title = txttitle.Text;
        string keywords = txtkeywords.Text;
        string description = txtdescription.Text;
        string beian = txtbeian.Value;
        string sql = "update sys_info set title=@title,keywords=@keywords,description=@description,beian=@beian ";
        SqlParameter[] parm = new SqlParameter[] { 
                              new SqlParameter("@title",SqlDbType.NVarChar){Value=title},
                              new SqlParameter("@keywords",SqlDbType.NVarChar){Value=keywords},
                              new SqlParameter("@description",SqlDbType.NVarChar){Value=description},
                              new SqlParameter("@beian",SqlDbType.NVarChar){Value=beian},
                              };
        int result = DB.ExecuteSql(sql, parm);
        if (result > 0)
        {
            SystemTool.AlertShow_Refresh(this, "保存成功", "xitongshezhi.aspx");
        }
        else
        {
            SystemTool.AlertShow(this, "保存失败");
            return;
        }
    }
}