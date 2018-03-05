using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class jilu_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                ViewState["user_id"] = Request.Cookies[Cookie.ComplanyId].Value;
                //if (null != Request.QueryString["id"])
                //{
                //    BindInfo();
                //}
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    

    //private void BindInfo()
    //{
    //    if (Request.QueryString["id"].ToString() != "")
    //    {
    //        string sql = "select * from taizhang_info where taizhang_id=" + Request.QueryString["id"];
    //        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            yuefen.Text = ds.Tables[0].Rows[0]["yuefen"].ToString();
    //            shiyongdanwei.Text = ds.Tables[0].Rows[0]["shiyongdanwei"].ToString();
    //            jihuabianhao.Text = ds.Tables[0].Rows[0]["jihuabianhao"].ToString();
    //            ddl_zijinlaiyuan.SelectedValue = ds.Tables[0].Rows[0]["zijinlaiyuan"].ToString();
    //            ddl_jihualeixing.SelectedValue = ds.Tables[0].Rows[0]["jihualeixing"].ToString();
    //            xunjiayuan.Text = ds.Tables[0].Rows[0]["xunjiayuan"].ToString();

    //            caigoubianhao.Text = ds.Tables[0].Rows[0]["caigoubianhao"].ToString();
    //            ddl_wuzishuxing.SelectedValue = ds.Tables[0].Rows[0]["wuzishuxing"].ToString();
    //            wuzibianma.Text = ds.Tables[0].Rows[0]["wuzibianma"].ToString();
    //            dingjiawuzimingcheng.Text = ds.Tables[0].Rows[0]["dingjiawuzimingcheng"].ToString();
    //            dingjiaguigexinghao.Text = ds.Tables[0].Rows[0]["dingjiaguigexinghao"].ToString();
    //            dingjiadanwei.Text = ds.Tables[0].Rows[0]["dingjiadanwei"].ToString();

    //            dingjiashuliang.Text = ds.Tables[0].Rows[0]["dingjiashuliang"].ToString();
    //            hanshuidanjia.Text = ds.Tables[0].Rows[0]["hanshuidanjia"].ToString();
    //            hanshuijine.Text = ds.Tables[0].Rows[0]["hanshuijine"].ToString();
    //            shuilv.Text = ds.Tables[0].Rows[0]["shuilv"].ToString();
    //            caigoufangshi.Text = ds.Tables[0].Rows[0]["caigoufangshi"].ToString();
    //            suoshuxieyihao.Text = ds.Tables[0].Rows[0]["suoshuxieyihao"].ToString();

    //            dingdanbianhao.Text = ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
    //            dinghuoyiju.Text = ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
    //            //jiaohuoshijian.Text = ds.Tables[0].Rows[0]["jiaohuoqi"].ToString();
    //            ddl_jiaohuodidian.SelectedValue = ds.Tables[0].Rows[0]["jiaohuodidian"].ToString();
    //            zhizaoshang.Text = ds.Tables[0].Rows[0]["zhizaoshang"].ToString();
    //            wuzipinpai.Text = ds.Tables[0].Rows[0]["wuzipinpai"].ToString();
    //            gonghuochangshang.Text = ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();

    //            ddl_dianshangpingtai.SelectedValue = ds.Tables[0].Rows[0]["dianshangpingtai"].ToString();
    //            wangdianmingcheng.Text = ds.Tables[0].Rows[0]["wangdianmingcheng"].ToString();
    //            ddl_fukuanfangshi.SelectedValue = ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
    //            changshangdianhua.Text = ds.Tables[0].Rows[0]["changshangdianhua"].ToString();
    //            dingdanriqi.Text = ds.Tables[0].Rows[0]["dingdanriqi"].ToString();
    //            jiaohuoqi.Text = ds.Tables[0].Rows[0]["jiaohuoqi"].ToString();

    //            yingdaoshijian.Text = ds.Tables[0].Rows[0]["yingdaoshijian"].ToString();
    //            shidaoshijian.Text = ds.Tables[0].Rows[0]["shidaoshijian"].ToString();
    //            shiyongshouming.Text = ds.Tables[0].Rows[0]["shiyongshouming"].ToString();
    //            zhibaoqi.Text = ds.Tables[0].Rows[0]["zhibaoqi"].ToString();
    //            qita.Text = ds.Tables[0].Rows[0]["qita"].ToString();
    //            beizhu.Value = ds.Tables[0].Rows[0]["beizhu"].ToString();
    //            ddl_state.SelectedValue = ds.Tables[0].Rows[0]["isfinished"].ToString();
    //            if (ds.Tables[0].Rows[0]["isfinished"].ToString() == "1")
    //            {
    //                ddl_state.Enabled = false;
    //            }
    //        }
    //        sql = "select man_name from manager_info where man_id=" + ViewState["user_id"];
    //        string resulta = DB.ExecuteSqlValue(sql, null);
    //        if (resulta != "" && resulta != "no")
    //        {
    //            lbl_user.Text = resulta;
    //        }
    //    }
    //}
    protected void lbtn_save_Click(object sender, EventArgs e)
    {
        string daohuoshuliang = txtdaohuoshuliang.Text;
        if (daohuoshuliang == "") {
            daohuoshuliang = "0";
        }
        string taizhangid = "0";
        if (null != Request.QueryString["id"]) {
            taizhangid = Request.QueryString["id"].ToString();
        }
        string manid = ViewState["user_id"].ToString();
        string bz = beizhu.Value;
   
        SqlParameter[] pars = new SqlParameter[] { 
                        new SqlParameter("@daohuoshuliang", System.Data.SqlDbType.NVarChar){Value=daohuoshuliang},
                        new SqlParameter("@taizhangid", System.Data.SqlDbType.NVarChar){Value=taizhangid},
                        new SqlParameter("@manid", System.Data.SqlDbType.NVarChar){Value=manid},
                        new SqlParameter("@bz", System.Data.SqlDbType.NVarChar){Value=bz}
                       
                    };

        if (null != Request.QueryString["id"])
        {
            string sqlck = "select taizhang_id from taizhang_info where taizhang_id='" + Request.QueryString["id"] + "' and manager_id=" + ViewState["user_id"];
            string resultck = DB.ExecuteSqlValue(sqlck, null);
            if (resultck == "" || resultck == "no")
            {
                SystemTool.AlertShow(this, "非添加者，不可操作此记录");
                return;
            }
            string sql = "insert into jilu_info (daohuo,taizhang_id,manager_id,beizhu) values(@daohuoshuliang,@taizhangid,@manid,@bz)";
            //string sql = "update taizhang_info set yuefen=@yuefen,shiyongdanwei=@shiyongdanwei,jihuabianhao=@jihuabianhao,zijinlaiyuan=@zijinlaiyuan,jihualeixing=@jihualeixing,xunjiayuan=@xunjiayuan,caigoubianhao=@caigoubianhao,wuzishuxing=@wuzishuxing,wuzibianma=@wuzibianma,dingjiawuzimingcheng=@dingjiawuzimingcheng,dingjiaguigexinghao=@dingjiaguigexinghao,dingjiadanwei=@dingjiadanwei,dingjiashuliang=@dingjiashuliang,hanshuidanjia=@hanshuidanjia,hanshuijine=@hanshuijine,shuilv=@shuilv,caigoufangshi=@caigoufangshi,suoshuxieyihao=@suoshuxieyihao,dingdanbianhao=@dingdanbianhao,dinghuoyiju=@dinghuoyiju,jiaohuodidian=@jiaohuodidian,zhizaoshang=@zhizaoshang,wuzipinpai=@wuzipinpai,gonghuochangshang=@gonghuochangshang,dianshangpingtai=@dianshangpingtai,wangdianmingcheng=@wangdianmingcheng,fukuanfangshi=@fukuanfangshi,changshangdianhua=@changshangdianhua,dingdanriqi=@dingdanriqi,jiaohuoqi=@jiaohuoqi,yingdaoshijian=@yingdaoshijian,shidaoshijian=@shidaoshijian,shiyongshouming=@shiyongshouming,zhibaoqi=@zhibaoqi,qita=@qita,beizhu=@beizhu,isfinished=@isfinished where taizhang_id=" + Request.QueryString["id"];
            int result = DB.ExecuteSql(sql, pars);
            if (result > 0)
            {
                SystemTool.AlertShow_Refresh1(this, "保存成功", "taizhang_list.aspx");
            }
            else
            {
                SystemTool.AlertShow(this, "保存失败");
                return;
            }
        }
        //else
        //{
        //    string sql = "insert into taizhang_info( yuefen,shiyongdanwei,jihuabianhao,zijinlaiyuan,jihualeixing,xunjiayuan,caigoubianhao,wuzishuxing,wuzibianma,dingjiawuzimingcheng,dingjiaguigexinghao,dingjiadanwei,dingjiashuliang,hanshuidanjia,hanshuijine,shuilv,caigoufangshi,suoshuxieyihao,dingdanbianhao,dinghuoyiju,jiaohuodidian,zhizaoshang,wuzipinpai,gonghuochangshang,dianshangpingtai,wangdianmingcheng,fukuanfangshi,changshangdianhua,dingdanriqi,jiaohuoqi,yingdaoshijian,shidaoshijian,shiyongshouming,zhibaoqi,qita,beizhu) values (@yuefen,@shiyongdanwei,@jihuabianhao,@zijinlaiyuan,@jihualeixing,@xunjiayuan,@caigoubianhao,@wuzishuxing,@wuzibianma,@dingjiawuzimingcheng,@dingjiaguigexinghao,@dingjiadanwei,@dingjiashuliang,@hanshuidanjia,@hanshuijine,@shuilv,@caigoufangshi,@suoshuxieyihao,@dingdanbianhao,@dinghuoyiju,@jiaohuodidian,@zhizaoshang,@wuzipinpai,@gonghuochangshang,@dianshangpingtai,@wangdianmingcheng,@fukuanfangshi,@changshangdianhua,@dingdanriqi,@jiaohuoqi,@yingdaoshijian,@shidaoshijian,@shiyongshouming,@zhibaoqi,@qita,@beizhu)";
        //    int result = DB.ExecuteSql(sql, pars);
        //    if (result > 0)
        //    {
        //        SystemTool.AlertShow_Refresh1(this, "保存成功", "taizhang_list.aspx");
        //    }
        //    else
        //    {
        //        SystemTool.AlertShow(this, "保存失败");
        //        return;
        //    }
        //}
    }
}