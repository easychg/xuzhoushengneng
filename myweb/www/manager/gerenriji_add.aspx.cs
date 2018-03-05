using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.IO;

public partial class manager_gerenriji_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
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
        if (!SystemTool.IsInt(Request.QueryString["id"].ToString()))
        {
            Response.Redirect("404.html");
        }
        string sql = "select * from article_info where id=" + Request.QueryString["id"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string navid = ds.Tables[0].Rows[0]["nav_id"].ToString();

            string title = ds.Tables[0].Rows[0]["title"].ToString();
            txttitle.Text = title;
            string article_image = ds.Tables[0].Rows[0]["article_image"].ToString();
            string[] images = article_image.Split('#');
            string html = "";
            for (int i = 0; i < images.Length - 1; i++)
            {
                string bianhao = "b" + i;
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
        }

    }
    protected void btnok_Click(object sender, EventArgs e)
    {
        string navid = "7";
        string title = txttitle.Text;
        if (title == "")
        {
            SystemTool.AlertShow(this, "标题不能为空");
            return;
        }
        string author = txtauthor.Text;
        if (author == "")
        {
            SystemTool.AlertShow(this, "作者不能为空");
            return;
        }
        string suoluetu = jiexi_picture(txtname.Text);
        if (suoluetu == "")
        {
            SystemTool.AlertShow(this, "缩略图不能为空");
            return;
        }
        string descr = txtdescr.Text;
        if (descr == "")
        {
            SystemTool.AlertShow(this, "简介不能为空");
            return;
        }
        string tuijian = ddl_tuijian.SelectedValue;
        string detail = txtbeizhu.Value;
        if (detail == "")
        {
            SystemTool.AlertShow(this, "内容不能为空");
            return;
        }
        string isshow = ddl_show.SelectedValue;
        string mubanid = "0";
        string sql = "insert into article_info(nav_id,title,article_image,author,descr,detail,isshow,istuijian,muban_id) values(@nav_id,@title,@article_image,@author,@descr,@detail,@isshow,@istuijian,@muban_id)";
        if (Request.QueryString["id"] != null)
        {
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
        if (result > 0)
        {
            SystemTool.AlertShow_Refresh(this, "保存成功", "gerenriji_list.aspx");
        }
        else
        {
            SystemTool.AlertShow(this, "保存失败");
            return;
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
                    else
                    {
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