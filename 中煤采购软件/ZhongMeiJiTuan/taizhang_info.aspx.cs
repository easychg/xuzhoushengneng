using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class taizhang_info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                BindData();
                ViewState["user_id"] = Request.Cookies[Cookie.ComplanyId].Value;
                if (null != Request.QueryString["id"])
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

    private void BindData()
    {
        string sql = "select * from jihualeixing";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_jihualeixing, ds.Tables[0], "mingcheng", "mingcheng", "");
        sql = "select * from dianshangpingtai";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_dianshangpingtai, ds.Tables[0], "mingcheng", "mingcheng", "");
        sql = "select * from fukuanfangshi";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_fukuanfangshi, ds.Tables[0], "mingcheng", "mingcheng", "");
        sql = "select * from jiaohuodidian";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_jiaohuodidian, ds.Tables[0], "mingcheng", "mingcheng", "");
        sql = "select * from wuzishuxing";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_wuzishuxing, ds.Tables[0], "mingcheng", "mingcheng", "");
        sql = "select * from zijinlaiyuan";
        ds = DB.ExecuteSqlDataSet(sql, null);
        SystemTool.bindDropDownList_0(ddl_zijinlaiyuan, ds.Tables[0], "mingcheng", "mingcheng", "");
    }

    private void BindInfo()
    {
        if (Request.QueryString["id"].ToString() != "")
        {
            string sql = "select * from taizhang_info where taizhang_id="+Request.QueryString["id"];
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0) {
                yuefen.Text = ds.Tables[0].Rows[0]["yuefen"].ToString();
                shiyongdanwei.Text = ds.Tables[0].Rows[0]["shiyongdanwei"].ToString();
                jihuabianhao.Text = ds.Tables[0].Rows[0]["jihuabianhao"].ToString();
                ddl_zijinlaiyuan.SelectedValue = ds.Tables[0].Rows[0]["zijinlaiyuan"].ToString();
                ddl_jihualeixing.SelectedValue = ds.Tables[0].Rows[0]["jihualeixing"].ToString();
                xunjiayuan.Text = ds.Tables[0].Rows[0]["xunjiayuan"].ToString();

                caigoubianhao.Text = ds.Tables[0].Rows[0]["caigoubianhao"].ToString();
                ddl_wuzishuxing.SelectedValue = ds.Tables[0].Rows[0]["wuzishuxing"].ToString();
                wuzibianma.Text = ds.Tables[0].Rows[0]["wuzibianma"].ToString();
                dingjiawuzimingcheng.Text = ds.Tables[0].Rows[0]["dingjiawuzimingcheng"].ToString();
                dingjiaguigexinghao.Text = ds.Tables[0].Rows[0]["dingjiaguigexinghao"].ToString();
                dingjiadanwei.Text = ds.Tables[0].Rows[0]["dingjiadanwei"].ToString();

                dingjiashuliang.Text = ds.Tables[0].Rows[0]["dingjiashuliang"].ToString();
                hanshuidanjia.Text = ds.Tables[0].Rows[0]["hanshuidanjia"].ToString();
                hanshuijine.Text = ds.Tables[0].Rows[0]["hanshuijine"].ToString();
                shuilv.Text = ds.Tables[0].Rows[0]["shuilv"].ToString();
                caigoufangshi.Text = ds.Tables[0].Rows[0]["caigoufangshi"].ToString();
                suoshuxieyihao.Text = ds.Tables[0].Rows[0]["suoshuxieyihao"].ToString();

                dingdanbianhao.Text = ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
                dinghuoyiju.Text = ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
                //jiaohuoshijian.Text = ds.Tables[0].Rows[0]["jiaohuoqi"].ToString();
                ddl_jiaohuodidian.SelectedValue = ds.Tables[0].Rows[0]["jiaohuodidian"].ToString();
                zhizaoshang.Text = ds.Tables[0].Rows[0]["zhizaoshang"].ToString();
                wuzipinpai.Text = ds.Tables[0].Rows[0]["wuzipinpai"].ToString();
                gonghuochangshang.Text = ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();

                ddl_dianshangpingtai.SelectedValue = ds.Tables[0].Rows[0]["dianshangpingtai"].ToString();
                wangdianmingcheng.Text = ds.Tables[0].Rows[0]["wangdianmingcheng"].ToString();
                ddl_fukuanfangshi.SelectedValue = ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
                changshangdianhua.Text = ds.Tables[0].Rows[0]["changshangdianhua"].ToString();
                dingdanriqi.Text = ds.Tables[0].Rows[0]["dingdanriqi"].ToString();
                jiaohuoqi.Text = ds.Tables[0].Rows[0]["jiaohuoqi"].ToString();

                yingdaoshijian.Text = ds.Tables[0].Rows[0]["yingdaoshijian"].ToString();
                shidaoshijian.Text = ds.Tables[0].Rows[0]["shidaoshijian"].ToString();
                shiyongshouming.Text = ds.Tables[0].Rows[0]["shiyongshouming"].ToString();
                zhibaoqi.Text = ds.Tables[0].Rows[0]["zhibaoqi"].ToString();
                qita.Text = ds.Tables[0].Rows[0]["qita"].ToString();
                beizhu.Value = ds.Tables[0].Rows[0]["beizhu"].ToString();
                ddl_state.SelectedValue = ds.Tables[0].Rows[0]["isfinished"].ToString();
                yuefen.Enabled = false;
                shiyongdanwei.Enabled = false;
                jihuabianhao.Enabled = false;
                ddl_zijinlaiyuan.Enabled = false;
                ddl_jihualeixing.Enabled = false;
                xunjiayuan.Enabled = false;

                caigoubianhao.Enabled = false;
                ddl_wuzishuxing.Enabled = false;
                wuzibianma.Enabled = false;
                dingjiawuzimingcheng.Enabled = false;
                dingjiaguigexinghao.Enabled = false;
                dingjiadanwei.Enabled = false;

                dingjiashuliang.Enabled = false;
                hanshuidanjia.Enabled = false;
                hanshuijine.Enabled = false;
                shuilv.Enabled = false;
                caigoufangshi.Enabled = false;
                suoshuxieyihao.Enabled = false;

                dingdanbianhao.Enabled = false;
                dinghuoyiju.Enabled = false;
                //jiaohuoshijian.Text =
                ddl_jiaohuodidian.Enabled = false;
                zhizaoshang.Enabled = false;
                wuzipinpai.Enabled = false;
                gonghuochangshang.Enabled = false;

                ddl_dianshangpingtai.Enabled = false;
                wangdianmingcheng.Enabled = false;
                ddl_fukuanfangshi.Enabled = false;
                changshangdianhua.Enabled = false;
                dingdanriqi.Enabled = false;
                jiaohuoqi.Enabled = false;

                yingdaoshijian.Enabled = false;
                
                shiyongshouming.Enabled = false;
                zhibaoqi.Enabled = false;
                qita.Enabled = false;
                beizhu.Disabled = true;
                if (ds.Tables[0].Rows[0]["isfinished"].ToString() == "1") {

                    shidaoshijian.Enabled = false;
                    ddl_state.Enabled = false;

                }
            }
            sql = "select man_name from manager_info where man_id=" + ViewState["user_id"];
            string resulta = DB.ExecuteSqlValue(sql, null);
            if (resulta != "" && resulta != "no") {
                lbl_user.Text = resulta;
            }
            sql = "select sum(daohuo) from jilu_info where taizhang_id="+Request.QueryString["id"];
            resulta = DB.ExecuteSqlValue(sql, null);
            if (resulta != "" && resulta != "no")
            {
                lblyidaohuo.Text = resulta;
            }
            else {
                lblyidaohuo.Text = "0";
            }
        }
    }
    protected void lbtn_save_Click(object sender, EventArgs e)
    {
        
        string yuefena=yuefen.Text;// = ds.Tables[0].Rows[0]["yuefen"].ToString();
        string shiyongdanweia=shiyongdanwei.Text;// = ds.Tables[0].Rows[0]["shiyongdanwei"].ToString();
        string jihuabianhaoa=jihuabianhao.Text;// = ds.Tables[0].Rows[0]["jihuabianhao"].ToString();
        string zijinlaiyuana=ddl_zijinlaiyuan.SelectedValue;// = ds.Tables[0].Rows[0]["zijinlaiyuan"].ToString();
        string jihualeixinga=ddl_jihualeixing.SelectedValue;// = ds.Tables[0].Rows[0]["jihualeixing"].ToString();
        string xunjiayuana=xunjiayuan.Text;// = ds.Tables[0].Rows[0]["xunjiayuan"].ToString();
        string caigoubianhaoa=caigoubianhao.Text;// = ds.Tables[0].Rows[0]["caigoubianhao"].ToString();
        string wuzishuxinga=ddl_wuzishuxing.SelectedValue;// = ds.Tables[0].Rows[0]["wuzishuxing"].ToString();
        string wuzibianmaa=wuzibianma.Text;// = ds.Tables[0].Rows[0]["wuzibianma"].ToString();
        string dingjiawuzimingchenga=dingjiawuzimingcheng.Text;// = ds.Tables[0].Rows[0]["dingjiawuzimingcheng"].ToString();
        string dingjiaguigexinghaoa=dingjiaguigexinghao.Text;// = ds.Tables[0].Rows[0]["dingjiaguigexinghao"].ToString();
        string dingjiadanweia=dingjiadanwei.Text;// = ds.Tables[0].Rows[0]["dingjiadanwei"].ToString();
        string dingjiashulianga=dingjiashuliang.Text;// = ds.Tables[0].Rows[0]["dingjiashuliang"].ToString();
        if (dingjiashulianga == "") {
            dingjiashulianga = "0";
        }
        string hanshuidanjiaa=hanshuidanjia.Text;// = ds.Tables[0].Rows[0]["hanshuidanjia"].ToString();
        if (hanshuidanjiaa == "")
        {
            hanshuidanjiaa = "0";
        }
        string hanshuijinea=hanshuijine.Text;// = ds.Tables[0].Rows[0]["hanshuijine"].ToString();
        if (hanshuijinea == "")
        {
            hanshuijinea = "0";
        }
        string shuilva=shuilv.Text;// = ds.Tables[0].Rows[0]["shuilv"].ToString();
        if (shuilva == "")
        {
            shuilva = "0";
        }
        string caigoufangshia=caigoufangshi.Text;// = ds.Tables[0].Rows[0]["caigoufangshi"].ToString();
        string suoshuxieyihaoa=suoshuxieyihao.Text; //= ds.Tables[0].Rows[0]["suoshuxieyihao"].ToString();
        string dingdanbianhaoa=dingdanbianhao.Text;// = ds.Tables[0].Rows[0]["dingdanbianhao"].ToString();
        string dinghuoyijua=dinghuoyiju.Text;// = ds.Tables[0].Rows[0]["dinghuoyiju"].ToString();
        //string jiaohuoshijiana=jiaohuoshijian.Text;// = ds.Tables[0].Rows[0]["jiaohuoshijian"].ToString();
        string jiaohuodidiana=ddl_jiaohuodidian.SelectedValue;// = ds.Tables[0].Rows[0]["jiaohuodidian"].ToString();
        string zhizaoshanga = zhizaoshang.Text;
        string wuzipinpaia=wuzipinpai.Text;// = ds.Tables[0].Rows[0]["wuzipinpai"].ToString();
        string gonghuochangshanga=gonghuochangshang.Text;// = ds.Tables[0].Rows[0]["gonghuochangshang"].ToString();
        string dianshangpingtaia=ddl_dianshangpingtai.SelectedValue;// = ds.Tables[0].Rows[0]["dianshangpingtai"].ToString();
        string wangdianmingchenga=wangdianmingcheng.Text;// = ds.Tables[0].Rows[0]["wangdianmingcheng"].ToString();
        string fukuanfangshia=ddl_fukuanfangshi.SelectedValue;// = ds.Tables[0].Rows[0]["fukuanfangshi"].ToString();
        string changshangdianhuaa=changshangdianhua.Text;// = ds.Tables[0].Rows[0]["changshangdianhua"].ToString();
        string dingdanriqia=dingdanriqi.Text;// = ds.Tables[0].Rows[0]["dingdanriqi"].ToString();
        string jiaohuoqia=jiaohuoqi.Text;// = ds.Tables[0].Rows[0]["jiaohuoqi"].ToString();
        string yingdaoshijiana=yingdaoshijian.Text;// = ds.Tables[0].Rows[0]["yingdaoshijian"].ToString();
        string shidaoshijiana=shidaoshijian.Text;// = ds.Tables[0].Rows[0]["shidaoshijian"].ToString();
        string shiyongshouminga=shiyongshouming.Text;// = ds.Tables[0].Rows[0]["shiyongshouming"].ToString();
        string zhibaoqia=zhibaoqi.Text;// = ds.Tables[0].Rows[0]["zhibaoqi"].ToString();
        string qitaa=qita.Text;// = ds.Tables[0].Rows[0]["qita"].ToString();
        string beizhua=beizhu.Value;// = ds.Tables[0].Rows[0]["beizhu"].ToString();
        string state = ddl_state.SelectedValue;
        if (state == "0") {
            shidaoshijiana = "";
        }
        SqlParameter[] pars = new SqlParameter[] { 
                        new SqlParameter("@yuefen", System.Data.SqlDbType.NVarChar){Value=yuefena},
                        new SqlParameter("@shiyongdanwei", System.Data.SqlDbType.NVarChar){Value=shiyongdanweia},
                        new SqlParameter("@jihuabianhao", System.Data.SqlDbType.NVarChar){Value=jihuabianhaoa},
                        new SqlParameter("@zijinlaiyuan", System.Data.SqlDbType.NVarChar){Value=zijinlaiyuana},
                        new SqlParameter("@jihualeixing", System.Data.SqlDbType.NVarChar){Value=jihualeixinga},
                        new SqlParameter("@xunjiayuan", System.Data.SqlDbType.NVarChar){Value=xunjiayuana},
                        new SqlParameter("@caigoubianhao", System.Data.SqlDbType.NVarChar){Value=caigoubianhaoa},
                        new SqlParameter("@wuzishuxing", System.Data.SqlDbType.NVarChar){Value=wuzishuxinga},
                        new SqlParameter("@wuzibianma", System.Data.SqlDbType.NVarChar){Value=wuzibianmaa},
                        new SqlParameter("@dingjiawuzimingcheng", System.Data.SqlDbType.NVarChar){Value=dingjiawuzimingchenga},
                        new SqlParameter("@dingjiaguigexinghao", System.Data.SqlDbType.NVarChar){Value=dingjiaguigexinghaoa},
                        new SqlParameter("@dingjiadanwei", System.Data.SqlDbType.NVarChar){Value=dingjiadanweia},
                        new SqlParameter("@dingjiashuliang", System.Data.SqlDbType.Decimal){Value=dingjiashulianga},
                        new SqlParameter("@hanshuidanjia", System.Data.SqlDbType.Decimal){Value=hanshuidanjiaa},
                        new SqlParameter("@hanshuijine", System.Data.SqlDbType.Decimal){Value=hanshuijinea},
                        new SqlParameter("@shuilv", System.Data.SqlDbType.Decimal){Value=shuilva},
                        new SqlParameter("@caigoufangshi", System.Data.SqlDbType.NVarChar){Value=caigoufangshia},
                        new SqlParameter("@suoshuxieyihao", System.Data.SqlDbType.NVarChar){Value=suoshuxieyihaoa},
                        new SqlParameter("@dingdanbianhao", System.Data.SqlDbType.NVarChar){Value=dingdanbianhaoa},
                        new SqlParameter("@dinghuoyiju", System.Data.SqlDbType.NVarChar){Value=dinghuoyijua},
                        new SqlParameter("@jiaohuodidian", System.Data.SqlDbType.NVarChar){Value=jiaohuodidiana},
                        new SqlParameter("@zhizaoshang", System.Data.SqlDbType.NVarChar){Value=zhizaoshanga},
                        new SqlParameter("@wuzipinpai", System.Data.SqlDbType.NVarChar){Value=wuzipinpaia},
                        new SqlParameter("@gonghuochangshang", System.Data.SqlDbType.NVarChar){Value=gonghuochangshanga},
                        new SqlParameter("@dianshangpingtai", System.Data.SqlDbType.NVarChar){Value=dianshangpingtaia},
                        new SqlParameter("@wangdianmingcheng", System.Data.SqlDbType.NVarChar){Value=wangdianmingchenga},
                        new SqlParameter("@fukuanfangshi", System.Data.SqlDbType.NVarChar){Value=fukuanfangshia},
                        new SqlParameter("@changshangdianhua", System.Data.SqlDbType.NVarChar){Value=changshangdianhuaa},
                        new SqlParameter("@dingdanriqi", System.Data.SqlDbType.NVarChar){Value=dingdanriqia},
                        new SqlParameter("@jiaohuoqi", System.Data.SqlDbType.NVarChar){Value=jiaohuoqia},
                        new SqlParameter("@yingdaoshijian", System.Data.SqlDbType.NVarChar){Value=yingdaoshijiana},
                        new SqlParameter("@shidaoshijian", System.Data.SqlDbType.NVarChar){Value=shidaoshijiana},
                        new SqlParameter("@shiyongshouming", System.Data.SqlDbType.NVarChar){Value=shiyongshouminga},
                        new SqlParameter("@zhibaoqi", System.Data.SqlDbType.NVarChar){Value=zhibaoqia},
                        new SqlParameter("@qita", System.Data.SqlDbType.NVarChar){Value=qitaa},
                        new SqlParameter("@beizhu", System.Data.SqlDbType.NVarChar){Value=beizhua},
                        new SqlParameter("@isfinished", System.Data.SqlDbType.Int){Value=state}
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
            string sql = "update taizhang_info set yuefen=@yuefen,shiyongdanwei=@shiyongdanwei,jihuabianhao=@jihuabianhao,zijinlaiyuan=@zijinlaiyuan,jihualeixing=@jihualeixing,xunjiayuan=@xunjiayuan,caigoubianhao=@caigoubianhao,wuzishuxing=@wuzishuxing,wuzibianma=@wuzibianma,dingjiawuzimingcheng=@dingjiawuzimingcheng,dingjiaguigexinghao=@dingjiaguigexinghao,dingjiadanwei=@dingjiadanwei,dingjiashuliang=@dingjiashuliang,hanshuidanjia=@hanshuidanjia,hanshuijine=@hanshuijine,shuilv=@shuilv,caigoufangshi=@caigoufangshi,suoshuxieyihao=@suoshuxieyihao,dingdanbianhao=@dingdanbianhao,dinghuoyiju=@dinghuoyiju,jiaohuodidian=@jiaohuodidian,zhizaoshang=@zhizaoshang,wuzipinpai=@wuzipinpai,gonghuochangshang=@gonghuochangshang,dianshangpingtai=@dianshangpingtai,wangdianmingcheng=@wangdianmingcheng,fukuanfangshi=@fukuanfangshi,changshangdianhua=@changshangdianhua,dingdanriqi=@dingdanriqi,jiaohuoqi=@jiaohuoqi,yingdaoshijian=@yingdaoshijian,shidaoshijian=@shidaoshijian,shiyongshouming=@shiyongshouming,zhibaoqi=@zhibaoqi,qita=@qita,beizhu=@beizhu,isfinished=@isfinished where taizhang_id=" + Request.QueryString["id"];
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
        else {
            string sql = "insert into taizhang_info( yuefen,shiyongdanwei,jihuabianhao,zijinlaiyuan,jihualeixing,xunjiayuan,caigoubianhao,wuzishuxing,wuzibianma,dingjiawuzimingcheng,dingjiaguigexinghao,dingjiadanwei,dingjiashuliang,hanshuidanjia,hanshuijine,shuilv,caigoufangshi,suoshuxieyihao,dingdanbianhao,dinghuoyiju,jiaohuodidian,zhizaoshang,wuzipinpai,gonghuochangshang,dianshangpingtai,wangdianmingcheng,fukuanfangshi,changshangdianhua,dingdanriqi,jiaohuoqi,yingdaoshijian,shidaoshijian,shiyongshouming,zhibaoqi,qita,beizhu) values (@yuefen,@shiyongdanwei,@jihuabianhao,@zijinlaiyuan,@jihualeixing,@xunjiayuan,@caigoubianhao,@wuzishuxing,@wuzibianma,@dingjiawuzimingcheng,@dingjiaguigexinghao,@dingjiadanwei,@dingjiashuliang,@hanshuidanjia,@hanshuijine,@shuilv,@caigoufangshi,@suoshuxieyihao,@dingdanbianhao,@dinghuoyiju,@jiaohuodidian,@zhizaoshang,@wuzipinpai,@gonghuochangshang,@dianshangpingtai,@wangdianmingcheng,@fukuanfangshi,@changshangdianhua,@dingdanriqi,@jiaohuoqi,@yingdaoshijian,@shidaoshijian,@shiyongshouming,@zhibaoqi,@qita,@beizhu)";
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
    }
}