using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Threading;
using System.Data.SqlClient;
using System.IO;

public partial class manager_articleadd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                init();
                if (null != Request.QueryString["id"] && Request.QueryString["id"].ToString() != "")
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
        if (!SystemTool.IsInt(Request.QueryString["id"].ToString())) {
            Response.Redirect("404.html");
        }
        string sql = "select * from article_info where id=" + Request.QueryString["id"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string navid = ds.Tables[0].Rows[0]["nav_id"].ToString();
            sql = "select * from nav_info where id="+navid;
            DataSet dsnav = DB.ExecuteSqlDataSet(sql, null);
            if (dsnav.Tables[0].Rows.Count > 0) {
                string nav0 = dsnav.Tables[0].Rows[0]["parent_id"].ToString();
                if (nav0 == "0")
                {
                    //当前为一级导航栏
                    ddl_nav0.SelectedValue = navid;
                }
                else {
                    sql = "select * from nav_info where parent_id="+nav0;
                    dsnav = DB.ExecuteSqlDataSet(sql, null);
                    if (dsnav.Tables[0].Rows.Count > 0)
                    {
                        ddl_nav.Items.Clear();
                        ddl_nav.DataSource = dsnav.Tables[0];
                        ddl_nav.DataTextField = "nav_name";
                        ddl_nav.DataValueField = "id";
                        ddl_nav.DataBind();
                        ListItem list = new ListItem("--请选择--", "0");
                        ddl_nav.Items.Insert(0, list);
                        ddl_nav.SelectedValue = navid;
                    }
                    else {
                        ddl_nav.Items.Clear();
                        ListItem list = new ListItem("--请选择--", "0");
                        ddl_nav.Items.Insert(0, list);
                    }
                }

            }
            string title = ds.Tables[0].Rows[0]["title"].ToString();
            txttitle.Text = title;
            string article_image = ds.Tables[0].Rows[0]["article_image"].ToString();
            string[] images = article_image.Split('#');
            string html = "";
            for (int i = 0; i < images.Length-1; i++) {
                string bianhao = "b"+i;
                string _width = "100";
                string _height = "100";
                var aaa = "<div id='div" + bianhao + "'  class='imgbox hasboth'  style='background-color:#fff;width:" + _width + "px;height:" + _height + "px;'>";
                var aa = " <div class='w_upload' style='width:" + _width + "px;height:" + _height + "px;' >    <a class='item_close' onclick=deletepic('" + bianhao + "')>删除</a><span style='width:" + _width + "px;height:" + _height + "px;' class='item_box'>";
                var bb = "<img id='img" + bianhao + "' src='/" + images[i] + "'  style='background:url(images/aa1.gif) no-repeat center;width:" + _width + "px;height:" + _height + "px;'/></span>";
                var dds = "</div></div>";
                html += aaa + aa + bb + dds;
            }
            txtname.Text = article_image;
            box.InnerHtml = html;
            string author = ds.Tables[0].Rows[0]["author"].ToString();
            txtauthor.Text = author;
            string descr = ds.Tables[0].Rows[0]["descr"].ToString();
            txtdescr.Text = descr;
            string detail = ds.Tables[0].Rows[0]["detail"].ToString();
            txtbeizhu.Value = detail;
            string isshow = ds.Tables[0].Rows[0]["isshow"].ToString();
            ddl_show.SelectedValue = isshow;
            string muban_id = ds.Tables[0].Rows[0]["muban_id"].ToString();
            ddl_muban.SelectedValue = muban_id;
        }
        
    }
    protected void init()
    {
        string sql = "select * from nav_info where parent_id=0 order by paixu";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_nav0.Items.Clear();
            ddl_nav0.DataSource = ds.Tables[0];
            ddl_nav0.DataTextField = "nav_name";
            ddl_nav0.DataValueField = "id";
            ddl_nav0.DataBind();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_nav0.Items.Insert(0, list);
        }
        else {
            ddl_nav0.Items.Clear();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_nav0.Items.Insert(0, list);
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
        else {
            ddl_muban.Items.Clear();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_muban.Items.Insert(0, list);
        }
    }

    protected void btnok_Click(object sender, EventArgs e)
    {
        string navid = "0";
        if (ddl_nav.Items.Count > 1)
        {
            if (ddl_nav.SelectedValue == "0")
            {
                SystemTool.AlertShow(this, "请选择二级导航");
                return;
            }
            navid = ddl_nav.SelectedValue;
        }
        else {
            if (ddl_nav0.SelectedValue == "0") {
                SystemTool.AlertShow(this, "请选择一级导航");
                return;
            }
            navid = ddl_nav0.SelectedValue;
        }
        string mubanid = ddl_muban.SelectedValue;
        if (mubanid == "0") {
            SystemTool.AlertShow(this, "模板不能为空");
            return;
        }
        string title = txttitle.Text;
        if (title == "") {
            SystemTool.AlertShow(this, "标题不能为空");
            return;
        }
        string author = txtauthor.Text;
        if (author == "") {
            SystemTool.AlertShow(this, "作者不能为空");
            return;
        }
        string suoluetu = jiexi_picture(txtname.Text);
        if (suoluetu == "") {
            SystemTool.AlertShow(this, "缩略图不能为空");
            return;
        }
        string descr = txtdescr.Text;
        if (descr == "") {
            SystemTool.AlertShow(this, "简介不能为空");
            return;
        }
        string tuijian = ddl_tuijian.SelectedValue;
        string detail = txtbeizhu.Value;
        if (detail == "") {
            SystemTool.AlertShow(this, "内容不能为空");
            return;
        }
        string isshow = ddl_show.SelectedValue;
        string sql = "insert into article_info(nav_id,title,article_image,author,descr,detail,isshow,istuijian,muban_id) values(@nav_id,@title,@article_image,@author,@descr,@detail,@isshow,@istuijian,@muban_id)";
        if (Request.QueryString["id"] != null) {
            sql = "update article_info set nav_id=@nav_id,title=@title,article_image=@article_image,author=@author,descr=@descr,detail=@detail,isshow=@isshow,istuijian=@istuijian,muban_id=@muban_id where id=" + Request.QueryString["id"].ToString();
        }
        SqlParameter[] parm = new SqlParameter[] { 
                              new SqlParameter("@nav_id",SqlDbType.Int){Value=navid},
                              new SqlParameter("@title",SqlDbType.NVarChar){Value=title},
                              new SqlParameter("@article_image",SqlDbType.NVarChar){Value=suoluetu},
                              new SqlParameter("@author",SqlDbType.NVarChar){Value=author},
                              new SqlParameter("@descr",SqlDbType.NVarChar){Value=descr},
                              new SqlParameter("@detail",SqlDbType.NText){Value=detail},
                              new SqlParameter("@isshow",SqlDbType.Int){Value=isshow},
                              new SqlParameter("@istuijian",SqlDbType.Int){Value=tuijian},
                              new SqlParameter("@muban_id",SqlDbType.Int){Value=mubanid},
                              };
        int result = DB.ExecuteSql(sql, parm);
        if (result > 0) {
            SystemTool.AlertShow_Refresh(this, "保存成功","article_list.aspx");
        } else {
            SystemTool.AlertShow(this, "保存失败");
            return;
        }
    }
    protected void ddl_nav_changed(object sender, EventArgs e) {
        string pid = ddl_nav0.SelectedValue;
        string sql = "select * from nav_info where parent_id="+pid+" order by paixu";
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
        else
        {
            ddl_nav.Items.Clear();
            ListItem list = new ListItem("--请选择--", "0");
            ddl_nav.Items.Insert(0, list);
        }
    }
    protected string jiexi_picture(string text)
    {
        string pictures = "";
        if (text != null)
        {
            string[] arr = text.Split('#');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].ToString() != "")
                {
                    string pic = arr[i].ToString();
                    Thread.Sleep(100);
                    string imgSaveName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".jpg";
                    if (pic.Contains("base"))
                    {
                        pic = pic.Replace("data:image/jpeg;base64,", "");
                        bool flag = StringToFile(pic, Server.MapPath("../public/images/" + imgSaveName));
                        pic = imgSaveName;
                        pictures += "public/images/" + imgSaveName + "#";
                    }
                    else {
                        pictures += pic + "#";
                    }
                }
            }
        }
        return pictures;
    }
    private static bool StringToFile(string base64String, string fileName)
    {
        try
        {
            //string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"/beapp/" + fileName; 

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            if (!string.IsNullOrEmpty(base64String) && File.Exists(fileName))
            {
                bw.Write(Convert.FromBase64String(base64String));
            }
            bw.Close();
            fs.Close();
            return true;

        }
        catch
        {
            return false;
        }
    }
}