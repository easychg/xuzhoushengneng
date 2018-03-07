using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Web
{
    public partial class mysqltest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                bindData();
            }
        }

        private void bindData()
        {
            string sql = "SELECT * FROM phome_ecms_article";
            rpt_a.DataSource = MysqlHelper.GetDataSet(MysqlHelper.Conn, CommandType.Text, sql, null).Tables[0];
            rpt_a.DataBind();
        }
    }
}