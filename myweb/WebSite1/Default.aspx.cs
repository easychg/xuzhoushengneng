using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) {
            Textbox1.Text = Class1.a;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Class1.a = Textbox1.Text;
    }
}