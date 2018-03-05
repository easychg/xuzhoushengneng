using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ascx_nav : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            string sql = @"select isnull(m.muban_url,'#') muban_url,n.id,n.parent_id,n.nav_name from nav_info n
left join muban_info m on m.id=n.muban_id
where n.isshow=1 and n.parent_id=0
order by n.paixu";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            rptlist.DataSource = ds.Tables[0];
            rptlist.DataBind();
            for (int i = 0; i < rptlist.Items.Count; i++)
            {
                Label id = rptlist.Items[i].FindControl("lbl_navid") as Label;
                Repeater rptb = rptlist.Items[i].FindControl("rptlistb") as Repeater;
                string sqlb = @"select isnull(m.muban_url,'#') muban_url,n.id,n.parent_id,n.nav_name from nav_info n
left join muban_info m on m.id=n.muban_id
where n.isshow=1 and n.parent_id=" + id.Text + " order by n.paixu";
                DataSet dsb = DB.ExecuteSqlDataSet(sqlb, null);
                rptb.DataSource = dsb.Tables[0];
                rptb.DataBind();
            }
        }
    }
}