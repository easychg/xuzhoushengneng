using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manager_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string name = SystemTool.NoHtml( txt_name.Text);
        string pass = SystemTool.NoHtml( txt_psw.Text);
        if (name == "" || pass == "")
        {
            SystemTool.AlertShow(this, "账户和密码不能为空");
            return;
        }
        if (vCode.Value == "")
        {
            SystemTool.AlertShow(this, "验证码不能为空");
            return;
        }
        if (Session["Code"] == null) {
            SystemTool.AlertShow(this, "验证码已过期");
            return;
        }
        if (Session["Code"].ToString() != vCode.Value)
        {
            SystemTool.AlertShow(this, "验证码不正确");
            return;
        }
        //string sql = "select man_id from manager_info where man_name='" + name + "' and man_psw='" + SystemTool.jimi(pass) + "'";
        string sql = "select man_id,manager_info.state mstate,role_info.state rstate from manager_info,role_info where roleId=id and man_name='" + name + "' and man_psw='" + SystemTool.jimi(pass) + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count == 0) {
            SystemTool.AlertShow(this, "账号或密码错误");
            return;
        }
        if (ds.Tables[0].Rows[0]["mstate"] .ToString()== "禁止")
        {
            SystemTool.AlertShow(this, "该用户被禁用");
            return;
        }
        if (ds.Tables[0].Rows[0]["rstate"].ToString() == "禁止")
        {
            SystemTool.AlertShow(this, "该用户所在的组被禁用");
            return;
        }

        //登录记录
        //string area = SystemTool.GetAddressByIp(SystemTool.GetIP());
        string ip =  SystemTool.GetIP();
        string dz = "";
        //if (SystemTool.IsIP(ip)) {
        //    dz = GetAddressByIp(ip);
        //}
        string managerid = ds.Tables[0].Rows[0]["man_id"].ToString();
        string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        //string sqlin = "insert into denglujilu(man_id,addtime,dengluip,dengludizhi) values(" + managerid + ",'" + time + "','" + ip + "','"+dz+"')";
        //int result=DB.ExecuteSql(sqlin, null);
        //if (result <= 0) {
        //    SystemTool.AlertShow(this, "网路连接错误，请重新登录");
        //    return;
        //}
        HttpCookie cookid = new HttpCookie(Cookie.ComplanyId, ds.Tables[0].Rows[0]["man_id"].ToString());
        cookid.Expires = DateTime.Now.AddDays(1);
        Response.Cookies.Add(cookid);
        Response.Redirect("index.aspx");
      //  SystemTool.AlertShow_Refresh(this, "登录成功", "index.aspx");

    }
    protected string GetAddressByIp(string ip)
    {
        // string ip = TextBox1.Text.ToString().Trim(); //"115.193.217.249";
        string PostUrl = "http://int.dpool.sina.com.cn/iplookup/iplookup.php?ip=" + ip;
        string res = SystemTool.GetDataByPost(PostUrl);//该条请求返回的数据为：res=1\t115.193.210.0\t115.194.201.255\t中国\t浙江\t杭州\t电信
        SystemTool.SaveTxtInfo(res, "log/res.txt");
        if (res.Contains("-"))
        {
            return "";
        }
        string[] arr = SystemTool.getAreaInfoList(res);
        if (arr.Length > 2)
        {
            return arr[0] + arr[1].ToString().Trim() + arr[2].ToString();
        }
        else {
            return "";
        }
        
    }
   
}