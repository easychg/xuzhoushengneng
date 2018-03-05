using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class daoru_record : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            BindInfo();
        }
    }

    private void BindInfo()
    {
        string sql = "select * from daoru_record";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_record.DataSource = ds.Tables[0];
        rpt_record.DataBind();
    }
}