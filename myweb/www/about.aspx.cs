using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class about : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        string sql = "select * from article_info where nav_id=6";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            content.InnerHtml = ds.Tables[0].Rows[0]["detail"].ToString();
        }
        else {
            content.InnerHtml = "暂无数据...";
        }
        
    }
}