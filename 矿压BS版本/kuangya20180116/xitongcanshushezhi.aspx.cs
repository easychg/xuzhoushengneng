using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class xitongcanshushezhi : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (null != Request.Cookies[Cookie.ComplanyId] && Request.Cookies[Cookie.ComplanyId] != null)
            {
                ViewState["gongzuomiancanshu"] = " 1=1";
                ViewState["chuanganqicanshu"] = " 1=1";
                ViewState["xiangdaocanshuliebiao"] = " 1=1";
                ViewState["chuanganqiliebiao"] = " 1=1";
                ViewState["xiangdaocanshuliebiao3_1"]=" 1=1";
                ViewState["chuanganqiliebiao3_2"]=" 1=1";
                ViewState["xiangdaocanshuliebiao4_1"]=" 1=1";
                ViewState["chuanganqiliebiao4_2"]=" 1=1";
                ViewState["rpt_gongzuomiancanshuliebiao5_1"]=" 1=1";
                ViewState["rpt_chuanganqiliebiao5_2"]=" 1=1";
                BindData();
                BindInfo();
            }
            else
            {
                SystemTool.AlertShow_Refresh2(this, "Login.aspx");
            }
        }
    }

    private void BindData()
    {
        //测区
        string sql = "select * from AreaInfo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {

            ddl_cequ1.DataSource = ds;
            ddl_cequ1.DataValueField = "areaname";
            ddl_cequ1.DataTextField = "areaname";
            ddl_cequ1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_cequ1.Items.Insert(0, item1);

            ddl_cequ2_1.DataSource = ds.Tables[0];
            ddl_cequ2_1.DataValueField = "areaname";
            ddl_cequ2_1.DataTextField = "areaname";
            ddl_cequ2_1.DataBind();
            ListItem item2 = new ListItem("--请选择--", "0");
            ddl_cequ2_1.Items.Insert(0, item2);

            ddl_cequmingcheng3_1.DataSource = ds.Tables[0];
            ddl_cequmingcheng3_1.DataValueField = "areaname";
            ddl_cequmingcheng3_1.DataTextField = "areaname";
            ddl_cequmingcheng3_1.DataBind();
            ListItem item3 = new ListItem("--请选择--", "0");
            ddl_cequmingcheng3_1.Items.Insert(0, item3);

            ddl_cequmingcheng4_1.DataSource = ds.Tables[0];
            ddl_cequmingcheng4_1.DataValueField = "areaname";
            ddl_cequmingcheng4_1.DataTextField = "areaname";
            ddl_cequmingcheng4_1.DataBind();
            ListItem item4 = new ListItem("--请选择--", "0");
            ddl_cequmingcheng4_1.Items.Insert(0, item4);

            ddl_cequmingcheng5_1.DataSource = ds.Tables[0];
            ddl_cequmingcheng5_1.DataValueField = "areaname";
            ddl_cequmingcheng5_1.DataTextField = "areaname";
            ddl_cequmingcheng5_1.DataBind();
            ListItem item5 = new ListItem("--请选择--", "0");
            ddl_cequmingcheng5_1.Items.Insert(0, item5);
            //ddl_AreaInfo.DataSource = ds;
            //ddl_AreaInfo.DataValueField = "areaname";
            //ddl_AreaInfo.DataTextField = "areaname";
            //ddl_AreaInfo.DataBind();
            //ListItem item = new ListItem("--请选择--", "0");
            //ddl_AreaInfo.Items.Insert(0, item);

            //ddl_cequ2.DataSource = ds;
            //ddl_cequ2.DataValueField = "areaname";
            //ddl_cequ2.DataTextField = "areaname";
            //ddl_cequ2.DataBind();
            //ListItem item2 = new ListItem("--请选择--", "0");
            //ddl_cequ2.Items.Insert(0, item2);
        }
        //工作面
        sql = "select * from workfaceinfo";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0) {
            ddl_gongzuomian1.DataSource = ds;
            ddl_gongzuomian1.DataValueField = "workfacename";
            ddl_gongzuomian1.DataTextField = "workfacename";
            ddl_gongzuomian1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian1.Items.Insert(0, item1);
            //ddl_gongzuomian1_2
            ddl_gongzuomian1_2.DataSource = ds;
            ddl_gongzuomian1_2.DataValueField = "workfacename";
            ddl_gongzuomian1_2.DataTextField = "workfacename";
            ddl_gongzuomian1_2.DataBind();
            ListItem item2 = new ListItem("--请选择--", "0");
            ddl_gongzuomian1_2.Items.Insert(0, item2);

            ddl_gongzuomianmingcheng5_1.DataSource = ds.Tables[0];
            ddl_gongzuomianmingcheng5_1.DataValueField = "workfacename";
            ddl_gongzuomianmingcheng5_1.DataTextField = "workfacename";
            ddl_gongzuomianmingcheng5_1.DataBind();
            ListItem item2233445 = new ListItem("--请选择--", "0");
            ddl_gongzuomianmingcheng5_1.Items.Insert(0, item2233445);

            ddl_suoshugongzuomian5_2.DataSource = ds.Tables[0];
            ddl_suoshugongzuomian5_2.DataValueField = "workfacename";
            ddl_suoshugongzuomian5_2.DataTextField = "workfacename";
            ddl_suoshugongzuomian5_2.DataBind();
            ListItem item22334455 = new ListItem("--请选择--", "0");
            ddl_suoshugongzuomian5_2.Items.Insert(0, item22334455);
        }
        //巷道
        sql = "select * from roadinfo";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0) {
            ddl_xiangdao_2_1.DataSource = ds;
            ddl_xiangdao_2_1.DataValueField = "roadname";
            ddl_xiangdao_2_1.DataTextField = "roadname";
            ddl_xiangdao_2_1.DataBind();
            ListItem item2 = new ListItem("--请选择--", "0");
            ddl_xiangdao_2_1.Items.Insert(0, item2);

            ddl_suoshuxiangdao2_2.DataSource = ds;
            ddl_suoshuxiangdao2_2.DataValueField = "roadname";
            ddl_suoshuxiangdao2_2.DataTextField = "roadname";
            ddl_suoshuxiangdao2_2.DataBind();
            ListItem item22 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao2_2.Items.Insert(0, item22);

            ddl_xiangdaomingcheng3_1.DataSource = ds.Tables[0];
            ddl_xiangdaomingcheng3_1.DataValueField = "roadname";
            ddl_xiangdaomingcheng3_1.DataTextField = "roadname";
            ddl_xiangdaomingcheng3_1.DataBind();
            ListItem item223 = new ListItem("--请选择--", "0");
            ddl_xiangdaomingcheng3_1.Items.Insert(0, item223);

            ddl_suoshuxiangdao3_2.DataSource = ds.Tables[0];
            ddl_suoshuxiangdao3_2.DataValueField = "roadname";
            ddl_suoshuxiangdao3_2.DataTextField = "roadname";
            ddl_suoshuxiangdao3_2.DataBind();
            ListItem item2233 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao3_2.Items.Insert(0, item2233);

            ddl_xiangdaomingcheng4_1.DataSource = ds.Tables[0];
            ddl_xiangdaomingcheng4_1.DataValueField = "roadname";
            ddl_xiangdaomingcheng4_1.DataTextField = "roadname";
            ddl_xiangdaomingcheng4_1.DataBind();
            ListItem item22334 = new ListItem("--请选择--", "0");
            ddl_xiangdaomingcheng4_1.Items.Insert(0, item22334);

            ddl_suoshuxiangdao4_2.DataSource = ds.Tables[0];
            ddl_suoshuxiangdao4_2.DataValueField = "roadname";
            ddl_suoshuxiangdao4_2.DataTextField = "roadname";
            ddl_suoshuxiangdao4_2.DataBind();
            ListItem item223344 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao4_2.Items.Insert(0, item223344);
        }
        init();
    }
    private void init() {
        txtchuanganqizushu1.Text = "";
        gangjing1_1.Text = "";
        yalishangxian1.Text = "";
        gangjing1_2.Text = "";
        yalixiaxian1.Text = "";
        pinghengqianjindingbaojingzhi1.Text = "";
        lbl_state1_1.Text = "i";
        txtchuangganqibianhao.Value = "";
        txtzhijiabianhao.Value = "";
        txtjulicailiaoxiang.Value = "";
        lbl_state1_2.Text = "i";
        txtchuanganqizushu2_1.Value = "";
        txtweiyiyujingzhi2_1.Value = "";
        txtweiyibaojingzhi2_1.Value = "";
        lbl_cequ2_1.Text = "i";
        txtchuanganqibianhao2_2.Value = "";
        txtanzhuangweizhi2_2.Value = "";
        txtajidianshendu.Value = "";
        txtbjidianshendu.Value = "";
        lbl_cequ2_2.Text = "i";
        txtchuanganqizushu3_1.Value = "";
        txtzhijing3_1.Value = "";
        txtmaogankanglaqiangdu3_1.Value = "";
        txtmaoganbaojingzhi3_1.Value = "";
        txtmaoganyujinli3_1.Value = "";
        txtmaosuoyujinli3_1.Value = "";
        txtmaosuobaojingzhi3_1.Value = "";
        lbl_cequ3_1.Text = "i";
        txtchuanganqibianhao3_2.Value = "";
        txtanzhuangweizhi3_2.Value = "";
        txtchuzhuangzhi3_2.Value = "";
        lbl_cequ3_2.Text = "i";
        txtchuanganqizushu4_1.Value = "";
        lbl_cequ4_1.Text = "i";
        txtchuanganqibianhao4_2.Value = "";
        txtanzhuangweizhi4_2.Value = "";
        txtanzhuangshendu4_2.Value = "";
        lbl_cequ4_2.Text = "i";
        txtchuanganqizushu5_1.Value = "";
        txtsuoliangyujingzhi5_1.Value = "";
        suoliangbaojingzhi5_1.Value = "";
        lbl_cequ5_1.Text = "i";
        txtchuanganqibianhao5_2.Value = "";
        txtzhijiabianhao5_2.Value = "";
        txtjulicailiaoxiang5_2.Value = "";
        lbl_cequ5_2.Text = "i";
    }

    private void BindInfo()
    {
        //工作面参数列表
        string sql = "SELECT * FROM PressurePar where " + ViewState["gongzuomiancanshu"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_gongzuomiancanshu1.DataSource = ds.Tables[0];
        rpt_gongzuomiancanshu1.DataBind();
        //传感器参数列表
        sql = "SELECT * FROM PreSenInfo where " + ViewState["chuanganqicanshu"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqi.DataSource = ds.Tables[0];
        rpt_chuanganqi.DataBind();
        //巷道参数列表
        sql = "SELECT * FROM DisplacementPar where " + ViewState["xiangdaocanshuliebiao"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdaocanshuliebiao.DataSource = ds.Tables[0];
        rpt_xiangdaocanshuliebiao.DataBind();
        //传感器列表
        sql = "SELECT * FROM DisSenInfo where " + ViewState["chuanganqiliebiao"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao2_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao2_2.DataBind();
        //巷道参数列表3-1
        sql = "select * from BoltPar where " + ViewState["xiangdaocanshuliebiao3_1"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdaocanshuliebiao3_1.DataSource = ds.Tables[0];
        rpt_xiangdaocanshuliebiao3_1.DataBind();
        //传感器列表3-2
        sql = "SELECT * FROM BoltSenInfo where " + ViewState["chuanganqiliebiao3_2"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao3_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao3_2.DataBind();
        //巷道参数列表4-1
        sql = "select * from DrillPar where " + ViewState["xiangdaocanshuliebiao4_1"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdaocanshuliebiao4_1.DataSource = ds.Tables[0];
        rpt_xiangdaocanshuliebiao4_1.DataBind();
        //传感器列表4-2
        sql = "SELECT * FROM DrillSenInfo1 where " + ViewState["chuanganqiliebiao4_2"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao4_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao4_2.DataBind();
        //活柱缩量工作面参数列表
        sql = "SELECT * FROM HuoZhuPar where " + ViewState["rpt_gongzuomiancanshuliebiao5_1"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_gongzuomiancanshuliebiao5_1.DataSource = ds.Tables[0];
        rpt_gongzuomiancanshuliebiao5_1.DataBind();
        //活柱缩量传感器列表
        sql = "SELECT * FROM HuoZhuSenInfo where " + ViewState["rpt_chuanganqiliebiao5_2"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao5_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao5_2.DataBind();
        ////工作面
        //sql = "select * from workfaceinfo";
        //ds = DB.ExecuteSqlDataSet(sql, null);
        ////rpt_workface.DataSource = ds.Tables[0];
        ////rpt_workface.DataBind();
        ////巷道
        //sql = "select * from roadinfo";
        //ds = DB.ExecuteSqlDataSet(sql, null);
        //rpt_xiangdao.DataSource = ds.Tables[0];
        //rpt_xiangdao.DataBind();
    }
    //工作面参数添加,修改
    protected void lbtn_gongzuomiancanshu1_Click(object sender, EventArgs e)
    {
        string cequmingc = ddl_cequ1.SelectedValue;
        string gongzuomianmingcheng = ddl_gongzuomian1.SelectedValue;
        string chuanganqizushu = txtchuanganqizushu1.Text;
        string diyiyalitongdao = ddl_diyiyalitongdaojie1.SelectedValue;
        string gangjing1 = gangjing1_1.Text;
        string yalishangxian = yalishangxian1.Text;
        string dieryalitongdao = ddl_dieryalitongdaojie1.SelectedValue;
        string gangjing2 = gangjing1_2.Text;
        string yalixiaxian = yalixiaxian1.Text;
        string qianjindingbaojingzhi = pinghengqianjindingbaojingzhi1.Text;
        
        if (lbl_state1_1.Text == "i")
        {
            //插入
            string sqlck = "select facename from PressurePar where facename='" + gongzuomianmingcheng + "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.AlertShow(this, "工作面名称已存在");
                return;
            }

            string sql = "insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass1()");
                sql = "SELECT * FROM PressurePar where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_gongzuomiancanshu1.DataSource = ds.Tables[0];
                rpt_gongzuomiancanshu1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else { 
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update PressurePar set sennumber='"+chuanganqizushu+"',firstconnect='"+diyiyalitongdao+"',secondconnect='"+dieryalitongdao+"',firstd='"+gangjing1+"',sencondd='"+gangjing2+"',pressuremax='"+yalishangxian+"',pressuremin='"+yalixiaxian+"',pressurealarm='"+qianjindingbaojingzhi+"' where areaname='" + cequmingc + "' and facename='" + gongzuomianmingcheng + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass1()");
                sql = "SELECT * FROM PressurePar where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_gongzuomiancanshu1.DataSource = ds.Tables[0];
                rpt_gongzuomiancanshu1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string cequmingc0 = ddl_cequ1.SelectedValue="0";
        string gongzuomianmingcheng0 = ddl_gongzuomian1.SelectedValue="0";
        string chuanganqizushu0 = txtchuanganqizushu1.Text = "";
        string diyiyalitongdao0 = ddl_diyiyalitongdaojie1.SelectedValue="左柱";
        string gangjing10 = gangjing1_1.Text = "";
        string yalishangxian0 = yalishangxian1.Text = "";
        string dieryalitongdao0 = ddl_dieryalitongdaojie1.SelectedValue="左柱";
        string gangjing20 = gangjing1_2.Text = "";
        string yalixiaxian0 = yalixiaxian1.Text = "";
        string qianjindingbaojingzhi0 = pinghengqianjindingbaojingzhi1.Text = "";
        lbl_state1_1.Text = "i";
        
    }
    //传感器参数添加修改
    protected void lbtn_chuanganqi_Click(object sender, EventArgs e) {
        string suoshugongzuomian = ddl_gongzuomian1_2.SelectedValue;
        string chuanganqibainhao = txtchuangganqibianhao.Value;
        string yalileixing = ddl_yalileixing.SelectedValue;
        string zhijiabainhao = txtzhijiabianhao.Value;
        string julicailiaoxiang = txtjulicailiaoxiang.Value;
        string shiyongzhuangtai = ddl_state_12.SelectedValue;
        string cequmingcheng = ddl_cequ1.SelectedValue;// lbl_cequmingcheng1_2.Text;
        if (lbl_state1_2.Text == "i")
        {
            //插入
            string sqlck = "select facename from PreSenInfo where AreaName='" + cequmingcheng + "' and facename='" + suoshugongzuomian + "' and SensorNo='" + chuanganqibainhao + "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.AlertShow(this, "工作面名称已存在");
                return;
            }

            string sql = "insert into PreSenInfo(areaname,facename,sensorno,bracketno,Type,distance,usestate,look)values('" + cequmingcheng + "','" + suoshugongzuomian + "','" + chuanganqibainhao + "','" + zhijiabainhao + "','" + yalileixing + "','" + julicailiaoxiang + "','" + shiyongzhuangtai + "','显示')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass1()");
                sql = "SELECT * FROM PreSenInfo where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqi.DataSource = ds.Tables[0];
                rpt_chuanganqi.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update PreSenInfo set areaname='" + cequmingcheng + "',facename='" + suoshugongzuomian + "',sensorno='" + chuanganqibainhao + "',Type='" + yalileixing + "',distance='" + julicailiaoxiang + "',usestate='" + shiyongzhuangtai + "' where areaname='" + cequmingcheng + "' and facename='" + suoshugongzuomian + "' and bracketno='" + zhijiabainhao + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass1()");
                sql = "SELECT * FROM PreSenInfo where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqi.DataSource = ds.Tables[0];
                rpt_chuanganqi.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }

        //string suoshugongzuomian = ddl_gongzuomian1_2.SelectedValue;
        string chuanganqibainhao0 = txtchuangganqibianhao.Value="";
        string yalileixing0 = ddl_yalileixing.SelectedValue="液压支架";
        string zhijiabainhao0 = txtzhijiabianhao.Value="";
        string julicailiaoxiang0 = txtjulicailiaoxiang.Value="";
        string shiyongzhuangtai0 = ddl_state_12.SelectedValue="使用";
        //string cequmingcheng = lbl_cequmingcheng1_2.Text;
        lbl_state1_2.Text = "i";
    }
    //查询工作面参数列表，传感器列表
    protected void lbtn_search1_Click(object sender, EventArgs e) {
        ViewState["gongzuomiancanshu"] = " 1=1";
        ViewState["chuanganqicanshu"] = " 1=1";
        string cequmingc = ddl_cequ1.SelectedValue;
        string gongzuomianmingcheng =ddl_gongzuomian1_2.SelectedValue= ddl_gongzuomian1.SelectedValue;
        if (cequmingc != "0") {
            ViewState["gongzuomiancanshu"] += " and areaname='" + cequmingc + "'";
            ViewState["chuanganqicanshu"] += " and areaname='" + cequmingc + "'";
        }
        if (gongzuomianmingcheng != "0") {
            ViewState["gongzuomiancanshu"] += " and facename='"+gongzuomianmingcheng+"'";
            ViewState["chuanganqicanshu"] += " and facename='" + gongzuomianmingcheng + "'";
        }
        //SystemTool.JavascriptShow(this, "changclass1()");
        //工作面参数
        string sql = "SELECT * FROM PressurePar where " + ViewState["gongzuomiancanshu"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_gongzuomiancanshu1.DataSource = ds.Tables[0];
        rpt_gongzuomiancanshu1.DataBind();
        //传感器参数
        sql = "SELECT * FROM PreSenInfo where " + ViewState["chuanganqicanshu"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqi.DataSource = ds.Tables[0];
        rpt_chuanganqi.DataBind();
    }
    //工作面参数修改,删除
    protected void rpt_gongzuomiancanshu1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from PressurePar where areaname='" + areaname.Text + "' and facename='" + facename + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0) {
                string cequmingc = ddl_cequ1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string gongzuomianmingcheng = ddl_gongzuomian1.SelectedValue = ds.Tables[0].Rows[0]["facename"].ToString();
                string chuanganqizushu = txtchuanganqizushu1.Text = ds.Tables[0].Rows[0]["sennumber"].ToString();
                string diyiyalitongdao = ddl_diyiyalitongdaojie1.SelectedValue = ds.Tables[0].Rows[0]["firstconnect"].ToString();
                string gangjing1 = gangjing1_1.Text = ds.Tables[0].Rows[0]["firstd"].ToString();
                string yalishangxian = yalishangxian1.Text = ds.Tables[0].Rows[0]["pressuremax"].ToString();
                string dieryalitongdao = ddl_dieryalitongdaojie1.SelectedValue = ds.Tables[0].Rows[0]["secondconnect"].ToString();
                string gangjing2 = gangjing1_2.Text = ds.Tables[0].Rows[0]["sencondd"].ToString();
                string yalixiaxian = yalixiaxian1.Text = ds.Tables[0].Rows[0]["pressuremin"].ToString();
                string qianjindingbaojingzhi = pinghengqianjindingbaojingzhi1.Text = ds.Tables[0].Rows[0]["pressurealarm"].ToString();
                lbl_state1_1.Text = "u";
            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from PressurePar where areaname='"+areaname.Text+"' and facename='"+facename+"'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                sql = "SELECT * FROM PressurePar where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_gongzuomiancanshu1.DataSource = ds.Tables[0];
                rpt_gongzuomiancanshu1.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
     
        
    }
    //传感器修改删除
    protected void rpt_chuanganqi_ItemCommand(object source, RepeaterCommandEventArgs e) {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        Label bracketno = e.Item.FindControl("lbl_bracketno") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from PreSenInfo where areaname='" + areaname.Text + "' and facename='" + facename + "' and bracketno='" + bracketno.Text + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lbl_cequmingcheng1_2.Text = ddl_cequ2.SelectedValue;

                string gongzuomianmingcheng = ddl_gongzuomian1_2.SelectedValue = ds.Tables[0].Rows[0]["facename"].ToString();
                string chuanganqibainhao = txtchuangganqibianhao.Value = ds.Tables[0].Rows[0]["sensorno"].ToString();
                string yalileixing = ddl_yalileixing.SelectedValue = ds.Tables[0].Rows[0]["type"].ToString();
                string zhijiabainhao = txtzhijiabianhao.Value = ds.Tables[0].Rows[0]["bracketno"].ToString();
                string julicailiaoxiang = txtjulicailiaoxiang.Value = ds.Tables[0].Rows[0]["distance"].ToString();
                string shiyongzhuangtai = ddl_state_12.SelectedValue = ds.Tables[0].Rows[0]["usestate"].ToString();
                string cequmingcheng = lbl_cequmingcheng1_2.Text = ds.Tables[0].Rows[0]["areaname"].ToString();
                ddl_cequ1.SelectedValue =  ds.Tables[0].Rows[0]["areaname"].ToString();
                lbl_state1_2.Text = "u";
            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from PreSenInfo where areaname='" + areaname.Text + "' and facename='" + facename + "' and bracketno='" + bracketno.Text+ "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                sql = "SELECT * FROM PreSenInfo where " + ViewState["chuanganqicanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqi.DataSource = ds.Tables[0];
                rpt_chuanganqi.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
    }
    

    //巷道参数添加修改
    protected void lbtn_save2_1_Click(object sender, EventArgs e) {
        string cequmingcheng = ddl_cequ2_1.SelectedValue;
        string xiangdaomingcheng = ddl_xiangdao_2_1.SelectedValue;
        string chuanganqizushu = txtchuanganqizushu2_1.Value;
        string weiyiyujingzhi = txtweiyiyujingzhi2_1.Value;
        string weiyibaojingzhi = txtweiyibaojingzhi2_1.Value;
        if (lbl_cequ2_1.Text == "i")
        {
            //插入
            string sqlck = "select roadwayname from DisplacementPar where roadwayname='" + xiangdaomingcheng + "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass2()");
                SystemTool.AlertShow(this, "工作面名称已存在");
                return;
            }

            string sql = "insert into DisplacementPar (areaname,roadwayname,disyujingvale,displacementalarm,sennumber,datacycle,soundalarm)values('" + cequmingcheng + "','" + xiangdaomingcheng + "','" + weiyiyujingzhi + "','" + weiyibaojingzhi + "','" + chuanganqizushu + "','2','0')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass2()");
                SystemTool.AlertShow(this, "操作成功");
                sql = "SELECT * FROM DisplacementPar where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update DisplacementPar set disyujingvale='"+weiyiyujingzhi+"',displacementalarm='"+weiyibaojingzhi+"',sennumber='"+chuanganqizushu+"' where areaname='" + cequmingcheng + "' and roadwayname='" + xiangdaomingcheng + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass2()");
                sql = "SELECT * FROM DisplacementPar where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string cequmingcheng0 = ddl_cequ2_1.SelectedValue="0";
        string xiangdaomingcheng0 = ddl_xiangdao_2_1.SelectedValue="0";
        string chuanganqizushu0 = txtchuanganqizushu2_1.Value="";
        string weiyiyujingzhi0 = txtweiyiyujingzhi2_1.Value="";
        string weiyibaojingzhi0 = txtweiyibaojingzhi2_1.Value="";
        lbl_cequ2_1.Text = "i";
    }
    //传感器添加修改
    protected void lbtn_save_2_2_Click(object sender, EventArgs e) {
        string xiangdaomingcheng = ddl_suoshuxiangdao2_2.SelectedValue;
        string chuanganqibianhao = txtchuanganqibianhao2_2.Value;
        string anzhuangweizhi = txtanzhuangweizhi2_2.Value;
        string ajidian = txtajidianshendu.Value;
        string bjidian = txtbjidianshendu.Value;
        string shiyongzhuangtai = ddl_shiyongzhuangtai2_2.SelectedValue;
        string cequmingcheng = ddl_cequ2_1.SelectedValue;

        if (lbl_cequ2_2.Text == "i")
        {
            //插入
            //string sqlck = "select roadwayname from DisSenInfo where roadwayname='" + xiangdaomingcheng + "'";
            string sqlck = "select areaName from DisSenInfo where areaName='" + cequmingcheng + "' and roadwayName ='" + xiangdaomingcheng + "' and sensorNo ='" + chuanganqibianhao + "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass2()");
                SystemTool.AlertShow(this, "该传感器信息已存在");
                return;
            }

            string sql = "insert into DisSenInfo(areaname,roadwayname,sensorno,location,pointdeptha,pointdepthb,usestate)values('" + cequmingcheng + "','" + xiangdaomingcheng + "','" + chuanganqibianhao + "','" + anzhuangweizhi + "','" + ajidian + "','"+bjidian+"','"+shiyongzhuangtai+"')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass2()");
                SystemTool.AlertShow(this, "操作成功");
                sql = "SELECT * FROM DisSenInfo where " + ViewState["chuanganqiliebiao"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao2_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao2_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update DisSenInfo set sensorno='"+chuanganqibianhao+"',location='"+anzhuangweizhi+"',pointdeptha='"+ajidian+"',pointdepthb='"+bjidian+"',usestate='"+shiyongzhuangtai+"' where areaname='" + cequmingcheng + "' and roadwayname='" + xiangdaomingcheng + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass2()");
                sql = "SELECT * FROM DisSenInfo where " + ViewState["chuanganqiliebiao"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao2_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao2_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string xiangdaomingcheng0 = ddl_suoshuxiangdao2_2.SelectedValue="0";
        string chuanganqibianhao0 = txtchuanganqibianhao2_2.Value="";
        string anzhuangweizhi0 = txtanzhuangweizhi2_2.Value="";
        string ajidian0 = txtajidianshendu.Value="";
        string bjidian0 = txtbjidianshendu.Value="";
        string shiyongzhuangtai0 = ddl_shiyongzhuangtai2_2.SelectedValue="0";
        //string cequmingcheng0 = ddl_cequ2_1.SelectedValue="0";
        lbl_cequ2_2.Text = "i";
    }
    //查询围岩移动：巷道参数列表，传感器列表
    protected void lbtn_search2_Click(object sender, EventArgs e) {
        ViewState["xiangdaocanshuliebiao"] = " 1=1";
        ViewState["chuanganqiliebiao"] = " 1=1";
        string cequmingc = ddl_cequ2_1.SelectedValue;
        string gongzuomianmingcheng = ddl_xiangdao_2_1.SelectedValue;// = ddl_xiangdao_2_1.SelectedValue;
        if (cequmingc != "0")
        {
            ViewState["gongzuomiancanshu"] += " and areaname='" + cequmingc + "'";
            ViewState["chuanganqiliebiao"] += " and areaname='" + cequmingc + "'";
        }
        if (gongzuomianmingcheng != "0")
        {
            ViewState["gongzuomiancanshu"] += " and roadwayname='" + gongzuomianmingcheng + "'";
            ViewState["chuanganqiliebiao"] += " and roadwayname='" + gongzuomianmingcheng + "'";
        }
        //SystemTool.JavascriptShow(this, "changclass1()");
        //工作面参数
        string sql = "SELECT * FROM DisplacementPar where " + ViewState["gongzuomiancanshu"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdaocanshuliebiao.DataSource = ds.Tables[0];
        rpt_xiangdaocanshuliebiao.DataBind();
        ////传感器参数
        sql = "SELECT * FROM DisSenInfo where " + ViewState["chuanganqiliebiao"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao2_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao2_2.DataBind();
        SystemTool.JavascriptShow(this, "changclass2()");
    }
    //巷道参数列表修改删除
    protected void rpt_xiangdaocanshuliebiao_ItemCommand(object source, RepeaterCommandEventArgs e) {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from DisplacementPar where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string cequmingc = ddl_cequ1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string cequmingcheng0 = ddl_cequ2_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string xiangdaomingcheng0 = ddl_xiangdao_2_1.SelectedValue = ds.Tables[0].Rows[0]["roadwayname"].ToString();
                string chuanganqizushu0 = txtchuanganqizushu2_1.Value = ds.Tables[0].Rows[0]["sennumber"].ToString();
                string weiyiyujingzhi0 = txtweiyiyujingzhi2_1.Value = ds.Tables[0].Rows[0]["disyujingvale"].ToString();
                string weiyibaojingzhi0 = txtweiyibaojingzhi2_1.Value = ds.Tables[0].Rows[0]["displacementalarm"].ToString();
                lbl_cequ2_1.Text = "u";
                
            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from DisplacementPar where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                sql = "SELECT * FROM DisplacementPar where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass2()");
    }
    //传感器列表修改删除22
    protected void rpt_chuanganqiliebiao2_2_ItemCommand(object source, RepeaterCommandEventArgs e) {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from DisSenInfo where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string xiangdaomingcheng0 = ddl_suoshuxiangdao2_2.SelectedValue = ds.Tables[0].Rows[0]["roadwayname"].ToString();
                string chuanganqibianhao0 = txtchuanganqibianhao2_2.Value = ds.Tables[0].Rows[0]["sensorno"].ToString();
                string anzhuangweizhi0 = txtanzhuangweizhi2_2.Value = ds.Tables[0].Rows[0]["location"].ToString();
                string ajidian0 = txtajidianshendu.Value = ds.Tables[0].Rows[0]["pointdeptha"].ToString();
                string bjidian0 = txtbjidianshendu.Value = ds.Tables[0].Rows[0]["pointdepthb"].ToString();
                string shiyongzhuangtai0 = ddl_shiyongzhuangtai2_2.SelectedValue = ds.Tables[0].Rows[0]["usestate"].ToString();
                string cequmingcheng0 = ddl_cequ2_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                lbl_cequ2_2.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from DisSenInfo where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                sql = "SELECT * FROM DisSenInfo where " + ViewState["gongzuomiancanshu"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao2_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao2_2.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass2()");
    }


    //添加修改巷道参数3-1
    protected void lbtn_save3_1_Click(object sender, EventArgs e) {
        string cequmingcheng = ddl_cequmingcheng3_1.SelectedValue;
        string xiangdaomingcheng = ddl_xiangdaomingcheng3_1.SelectedValue;
        string chuanganqizushu = txtchuanganqizushu3_1.Value;
        string zhijing = txtzhijing3_1.Value;
        string maogankanglaqiangdu = txtmaogankanglaqiangdu3_1.Value;
        string maoganbaojingzhi = txtmaoganbaojingzhi3_1.Value;
        string maoganyujinli = txtmaoganyujinli3_1.Value;
        string maosuoyujinli = txtmaosuoyujinli3_1.Value;
        string maosuobaojingzhi = txtmaosuobaojingzhi3_1.Value;

        if (lbl_cequ3_1.Text == "i")
        {
            //插入
            //string sqlck = "select roadwayname from DisplacementPar where roadwayname='" + xiangdaomingcheng + "'";
            string sqlck = "select areaname from BoltPar where areaName='"+cequmingcheng+ "' and roadwayName ='"+xiangdaomingcheng+ "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass3()");
                SystemTool.AlertShow(this, "该巷道参数已存在");
                return;
            }

            string sql = "insert into BoltPar(areaname,roadwayname,d,p,alarmmaxmgf,alarmmgf,alarmmaxmsf,alarmmsf,datacycle,soundalarm,sennumber)values('" + cequmingcheng + "','" + xiangdaomingcheng + "','"+zhijing+"','"+maogankanglaqiangdu+"','"+maoganbaojingzhi+"','"+maoganyujinli+"','"+maosuobaojingzhi+"','"+maosuoyujinli+"','2','0','"+chuanganqizushu+"')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass3()");
                SystemTool.AlertShow(this, "操作成功");
                //巷道参数列表3-1
                sql = "select * from BoltPar where " + ViewState["xiangdaocanshuliebiao3_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao3_1.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao3_1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update BoltPar set d='" + zhijing + "',p='" + maogankanglaqiangdu + "',alarmmaxmgf='" + maoganbaojingzhi + "',alarmmgf='" + maoganyujinli + "',alarmmaxmsf='" + maosuobaojingzhi + "',alarmmsf='" + maosuoyujinli + "',SenNumber='" + chuanganqizushu + "' where areaname='" + cequmingcheng + "' and roadwayname='" + xiangdaomingcheng + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass3()");
                //巷道参数列表3-1
                sql = "select * from BoltPar where " + ViewState["xiangdaocanshuliebiao3_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao3_1.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao3_1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string cequmingcheng0 = ddl_cequmingcheng3_1.SelectedValue = "0";
        string xiangdaomingcheng0 = ddl_xiangdaomingcheng3_1.SelectedValue="0";
        string chuanganqizushu0 = txtchuanganqizushu3_1.Value="";
        string zhijing0 = txtzhijing3_1.Value="";
        string maogankanglaqiangdu0 = txtmaogankanglaqiangdu3_1.Value="";
        string maoganbaojingzhi0 = txtmaoganbaojingzhi3_1.Value="";
        string maoganyujinli0 = txtmaoganyujinli3_1.Value="";
        string maosuoyujinli0 = txtmaosuoyujinli3_1.Value="";
        string maosuobaojingzhi0 = txtmaosuobaojingzhi3_1.Value="";
        lbl_cequ3_1.Text = "i";
    }
    //添加修改传感器参数列表
    protected void lbtn_save_3_2_Click(object sender, EventArgs e)
    {
        string xiangdaomingcheng = ddl_suoshuxiangdao3_2.SelectedValue;
        string chuanganqibianhao = txtchuanganqibianhao3_2.Value;
        string maoguleixing = ddl_maoguleixing3_2.SelectedValue;
        string anzhuangweizhi = txtanzhuangweizhi3_2.Value;
        string chuzhuangzhi = txtchuzhuangzhi3_2.Value;
        string shiyongzhuangtai = ddl_shiyongzhuangtai3_2.SelectedValue;
        string cequmingcheng = ddl_cequmingcheng3_1.SelectedValue;
        if (lbl_cequ3_2.Text == "i")
        {
            //插入
            //string sqlck = "select roadwayname from BoltSenInfo where roadwayname='" + xiangdaomingcheng + "'";
            string sqlck = "select areaname from BoltSenInfo where areaName='"+cequmingcheng+ "' and roadwayName ='"+xiangdaomingcheng+ "' and sensorNo ='" +chuanganqibianhao+ "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass3()");
                SystemTool.AlertShow(this, "该传感器信息已存在");
                return;
            }

            string sql = "insert into BoltSenInfo(areaname,roadwayname,sensorno,mgtype,location,starvalue,usestate)values('" + cequmingcheng + "','" + xiangdaomingcheng + "','" + chuanganqibianhao + "','" + maoguleixing + "','" + anzhuangweizhi + "','" + chuzhuangzhi + "','" + shiyongzhuangtai + "')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass3()");
                SystemTool.AlertShow(this, "操作成功");
                //传感器列表3-2
                sql = "SELECT * FROM BoltSenInfo where " + ViewState["chuanganqiliebiao3_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao3_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao3_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update BoltSenInfo set mgtype='"+maoguleixing+"',location='"+anzhuangweizhi+"',starvalue='"+chuzhuangzhi+"',usestate='"+shiyongzhuangtai+"' where areaname='" + cequmingcheng + "' and roadwayname='" + xiangdaomingcheng + "' and sensorno='"+chuanganqibianhao+"'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass3()");
                //传感器列表3-2
                sql = "SELECT * FROM BoltSenInfo where " + ViewState["chuanganqiliebiao3_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao3_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao3_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string xiangdaomingcheng0 = ddl_suoshuxiangdao3_2.SelectedValue="0";
        string chuanganqibianhao0 = txtchuanganqibianhao3_2.Value="";
        //string maoguleixing0 = ddl_maoguleixing3_2.SelectedValue="0";
        string anzhuangweizhi0 = txtanzhuangweizhi3_2.Value="";
        string chuzhuangzhi0 = txtchuzhuangzhi3_2.Value="";
        //string shiyongzhuangtai0 = ddl_shiyongzhuangtai3_2.SelectedValue;
        string cequmingcheng0 = ddl_cequmingcheng3_1.SelectedValue="0";
        lbl_cequ3_2.Text = "i";
    }
    //查询锚杆所应力巷道参数列表，传感器列表
    protected void lbtn_search3_Click(object sender, EventArgs e)
    {
        ViewState["xiangdaocanshuliebiao3_1"] = " 1=1";
        ViewState["chuanganqiliebiao3_2"] = " 1=1";
        string cequmingc = ddl_cequmingcheng3_1.SelectedValue;
        string gongzuomianmingcheng = ddl_xiangdaomingcheng3_1.SelectedValue;// = ddl_xiangdao_2_1.SelectedValue;
        if (cequmingc != "0")
        {
            ViewState["xiangdaocanshuliebiao3_1"] += " and areaname='" + cequmingc + "'";
            ViewState["chuanganqiliebiao3_2"] += " and areaname='" + cequmingc + "'";
        }
        if (gongzuomianmingcheng != "0")
        {
            ViewState["xiangdaocanshuliebiao3_1"] += " and roadwayname='" + gongzuomianmingcheng + "'";
            ViewState["chuanganqiliebiao3_2"] += " and roadwayname='" + gongzuomianmingcheng + "'";
        }
        //SystemTool.JavascriptShow(this, "changclass1()");
        //巷道参数列表3-1
        string sql = "select * from BoltPar where " + ViewState["xiangdaocanshuliebiao3_1"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdaocanshuliebiao3_1.DataSource = ds.Tables[0];
        rpt_xiangdaocanshuliebiao3_1.DataBind();
        //传感器列表3-2
        sql = "SELECT * FROM BoltSenInfo where " + ViewState["chuanganqiliebiao3_2"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao3_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao3_2.DataBind();
        SystemTool.JavascriptShow(this, "changclass3()");
    }
    //巷道参数列表修改删除3-1
    protected void rpt_xiangdaocanshuliebiao3_1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from BoltPar where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string cequmingc = ddl_cequ1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string cequmingcheng = ddl_cequmingcheng3_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string xiangdaomingcheng = ddl_xiangdaomingcheng3_1.SelectedValue = ds.Tables[0].Rows[0]["roadwayname"].ToString();
                string chuanganqizushu0 = txtchuanganqizushu3_1.Value = ds.Tables[0].Rows[0]["SenNumber"].ToString();
                string zhijing = txtzhijing3_1.Value = ds.Tables[0].Rows[0]["d"].ToString();
                string maogankanglaqiangdu = txtmaogankanglaqiangdu3_1.Value = ds.Tables[0].Rows[0]["p"].ToString();
                string maoganbaojingzhi = txtmaoganbaojingzhi3_1.Value = ds.Tables[0].Rows[0]["alarmmaxmgf"].ToString();
                string maoganyujinli = txtmaoganyujinli3_1.Value = ds.Tables[0].Rows[0]["alarmmgf"].ToString();
                string maosuoyujinli = txtmaosuoyujinli3_1.Value = ds.Tables[0].Rows[0]["alarmmsf"].ToString();
                string maosuobaojingzhi = txtmaosuobaojingzhi3_1.Value = ds.Tables[0].Rows[0]["alarmmaxmsf"].ToString();
                lbl_cequ3_1.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from BoltPar where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                //巷道参数列表3-1
                sql = "select * from BoltPar where " + ViewState["xiangdaocanshuliebiao3_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao3_1.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao3_1.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass3()");
    }
    //传感器列表修改删除32
    protected void rpt_chuanganqiliebiao3_2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        Label chuanganqibianhao = e.Item.FindControl("lbl_SenNumber") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from BoltSenInfo where areaname='" + areaname.Text + "' and roadwayname='" + facename + "' and sensorno='" + chuanganqibianhao.Text + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string xiangdaomingcheng = ddl_suoshuxiangdao3_2.SelectedValue = ds.Tables[0].Rows[0]["roadwayname"].ToString();
                string chuanganqibianhao0 = txtchuanganqibianhao3_2.Value = ds.Tables[0].Rows[0]["sensorno"].ToString();
                string maoguleixing = ddl_maoguleixing3_2.SelectedValue = ds.Tables[0].Rows[0]["mgtype"].ToString();
                string anzhuangweizhi = txtanzhuangweizhi3_2.Value = ds.Tables[0].Rows[0]["location"].ToString();
                string chuzhuangzhi = txtchuzhuangzhi3_2.Value = ds.Tables[0].Rows[0]["starvalue"].ToString();
                string shiyongzhuangtai = ddl_shiyongzhuangtai3_2.SelectedValue = ds.Tables[0].Rows[0]["usestate"].ToString();
                string cequmingcheng = ddl_cequmingcheng3_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                lbl_cequ3_2.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from BoltSenInfo where areaname='" + areaname.Text + "' and roadwayname='" + facename + "' and sensorno='" + chuanganqibianhao.Text + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                //传感器列表3-2
                sql = "SELECT * FROM BoltSenInfo where " + ViewState["chuanganqiliebiao3_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao3_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao3_2.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass3()");
    }


    //围岩应力：巷道参数添加修改
    protected void lbtn_save4_1_Click(object sender, EventArgs e)
    {
        string cequmingcheng = ddl_cequmingcheng4_1.SelectedValue;
        string xiangdaomingcheng = ddl_xiangdaomingcheng4_1.SelectedValue;
        string chuanganqizushu = txtchuanganqizushu4_1.Value;

        if (lbl_cequ4_1.Text == "i")
        {
            //插入
            //string sqlck = "select roadwayname from DrillPar where roadwayname='" + xiangdaomingcheng + "'";
            string sqlck = "select areaname from DrillPar where areaName='"+cequmingcheng+ "' and roadwayName ='"+xiangdaomingcheng+ "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass4()");
                SystemTool.AlertShow(this, "该巷道参数已存在");
                return;
            }

            string sql = "insert into DrillPar(areaname,roadwayname,pressalarm,datacycle,soundalarm,sennumber)values('" + cequmingcheng + "','" + xiangdaomingcheng + "','15','2','0','" + chuanganqizushu + "')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass4()");
                SystemTool.AlertShow(this, "操作成功");
                //巷道参数列表4-1
                sql = "select * from DrillPar where " + ViewState["xiangdaocanshuliebiao4_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao4_1.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao4_1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update DrillPar set sennumber='"+chuanganqizushu+"' where areaname='" + cequmingcheng + "' and roadwayname='" + xiangdaomingcheng + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                SystemTool.JavascriptShow(this, "changclass4()");
                //巷道参数列表4-1
                sql = "select * from DrillPar where " + ViewState["xiangdaocanshuliebiao4_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao4_1.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao4_1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string cequmingcheng0 = ddl_cequmingcheng4_1.SelectedValue="0";
        string xiangdaomingcheng0 = ddl_xiangdaomingcheng4_1.SelectedValue="0";
        string chuanganqizushu0 = txtchuanganqizushu4_1.Value="";
        lbl_cequ4_1.Text = "i";
    }
    //围岩应力：传感器添加修改
    protected void lbtn_save_4_2_Click(object sender, EventArgs e)
    {
        string xiangdaomingcheng = ddl_suoshuxiangdao4_2.SelectedValue;
        string chuanganqibianhao = txtchuanganqibianhao4_2.Value;
        string anzhuangweizhi = txtanzhuangweizhi4_2.Value;
        string anzhuangshendu = txtanzhuangshendu4_2.Value;
        string shiyongzhuangtai = ddl_shiyongzhuangtai4_2.SelectedValue;
        string cequmingcheng = ddl_cequmingcheng4_1.SelectedValue;
        if (lbl_cequ4_2.Text == "i")
        {
            //插入
            //string sqlck = "select roadwayname from DrillSenInfo1 where roadwayname='" + xiangdaomingcheng + "'";
            string sqlck = "select areaname from DrillSenInfo1 where areaName='"+cequmingcheng+ "' and roadwayName ='"+xiangdaomingcheng+"'and sensorNo ='"+chuanganqibianhao+ "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass4()");
                SystemTool.AlertShow(this, "该传感器信息已存在");
                return;
            }

            string sql = "insert into DrillSenInfo1(areaname,roadwayname,sensorno,location,depth,usestate)values('" + cequmingcheng + "','" + xiangdaomingcheng + "','" + chuanganqibianhao + "','" + anzhuangweizhi + "','" + anzhuangshendu + "','" + shiyongzhuangtai + "')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass4()");
                SystemTool.AlertShow(this, "操作成功");
                //传感器列表4-2
                sql = "SELECT * FROM DrillSenInfo1 where " + ViewState["chuanganqiliebiao4_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao4_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao4_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update DrillSenInfo1 set location='"+anzhuangweizhi+"',depth='"+anzhuangshendu+"',usestate='"+shiyongzhuangtai+"' where areaname='" + cequmingcheng + "' and roadwayname='" + xiangdaomingcheng + "' and sensorno='" + chuanganqibianhao + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass4()");
                SystemTool.AlertShow(this, "操作成功");
               
                //传感器列表4-2
                sql = "SELECT * FROM DrillSenInfo1 where " + ViewState["chuanganqiliebiao4_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao4_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao4_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string xiangdaomingcheng0 = ddl_suoshuxiangdao4_2.SelectedValue="0";
        string chuanganqibianhao0 = txtchuanganqibianhao4_2.Value="";
        string anzhuangweizhi0 = txtanzhuangweizhi4_2.Value="";
        string anzhuangshendu0 = txtanzhuangshendu4_2.Value="";
        //string shiyongzhuangtai0 = ddl_shiyongzhuangtai4_2.SelectedValue;
        string cequmingcheng0 = ddl_cequmingcheng4_1.SelectedValue="0";
        lbl_cequ4_2.Text = "i";
    }
    //查询围岩应力：巷道参数列表，传感器列表
    protected void lbtn_search4_Click(object sender, EventArgs e)
    {
        ViewState["xiangdaocanshuliebiao4_1"] = " 1=1";
        ViewState["chuanganqiliebiao4_2"] = " 1=1";
        string cequmingc = ddl_cequmingcheng4_1.SelectedValue;
        string gongzuomianmingcheng = ddl_xiangdaomingcheng4_1.SelectedValue;// = ddl_xiangdao_2_1.SelectedValue;
        if (cequmingc != "0")
        {
            ViewState["xiangdaocanshuliebiao4_1"] += " and areaname='" + cequmingc + "'";
            ViewState["chuanganqiliebiao4_2"] += " and areaname='" + cequmingc + "'";
        }
        if (gongzuomianmingcheng != "0")
        {
            ViewState["xiangdaocanshuliebiao4_1"] += " and roadwayname='" + gongzuomianmingcheng + "'";
            ViewState["chuanganqiliebiao4_2"] += " and roadwayname='" + gongzuomianmingcheng + "'";
        }
        //SystemTool.JavascriptShow(this, "changclass1()");
        //巷道参数列表4-1
        string sql = "select * from DrillPar where " + ViewState["xiangdaocanshuliebiao4_1"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_xiangdaocanshuliebiao4_1.DataSource = ds.Tables[0];
        rpt_xiangdaocanshuliebiao4_1.DataBind();
        //传感器列表4-2
        sql = "SELECT * FROM DrillSenInfo1 where " + ViewState["chuanganqiliebiao4_2"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao4_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao4_2.DataBind();
        SystemTool.JavascriptShow(this, "changclass4()");
    }
    //巷道参数列表修改删除4-1
    protected void rpt_xiangdaocanshuliebiao4_1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;

        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from DrillPar where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string cequmingc = ddl_cequ1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string cequmingcheng = ddl_cequmingcheng4_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string xiangdaomingcheng = ddl_xiangdaomingcheng4_1.SelectedValue = ds.Tables[0].Rows[0]["roadwayname"].ToString();
                string chuanganqizushu = txtchuanganqizushu4_1.Value = ds.Tables[0].Rows[0]["sennumber"].ToString();
                lbl_cequ4_1.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from DrillPar where areaname='" + areaname.Text + "' and roadwayname='" + facename + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                //巷道参数列表4-1
                sql = "select * from DrillPar where " + ViewState["xiangdaocanshuliebiao4_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_xiangdaocanshuliebiao4_1.DataSource = ds.Tables[0];
                rpt_xiangdaocanshuliebiao4_1.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass4()");
    }
    //传感器列表修改删除4-2
    protected void rpt_chuanganqiliebiao4_2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        Label chuanganqibianhao = e.Item.FindControl("lbl_SenNumber") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from DrillSenInfo1 where areaname='" + areaname.Text + "' and roadwayname='" + facename + "' and sensorno='" + chuanganqibianhao.Text + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {

                string xiangdaomingcheng = ddl_suoshuxiangdao4_2.SelectedValue = ds.Tables[0].Rows[0]["roadwayname"].ToString();
                string chuanganqibianhao0 = txtchuanganqibianhao4_2.Value = ds.Tables[0].Rows[0]["sensorno"].ToString();
                string anzhuangweizhi = txtanzhuangweizhi4_2.Value = ds.Tables[0].Rows[0]["location"].ToString();
                string anzhuangshendu = txtanzhuangshendu4_2.Value = ds.Tables[0].Rows[0]["depth"].ToString();
                string shiyongzhuangtai = ddl_shiyongzhuangtai4_2.SelectedValue = ds.Tables[0].Rows[0]["usestate"].ToString();
                string cequmingcheng = ddl_cequmingcheng4_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                lbl_cequ4_2.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from DrillSenInfo1 where areaname='" + areaname.Text + "' and roadwayname='" + facename + "' and sensorno='" + chuanganqibianhao.Text + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                //传感器列表4-2
                sql = "SELECT * FROM DrillSenInfo1 where " + ViewState["chuanganqiliebiao4_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao4_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao4_2.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass4()");
    }


    //活柱缩量：工作面参数添加修改
    protected void lbtn_save5_1_Click(object sender, EventArgs e) {
        string cequmingcheng = ddl_cequmingcheng5_1.SelectedValue;// = ds.Tables[0].Rows[0]["areaname"].ToString();
        string gongzuomianmingcheng = ddl_gongzuomianmingcheng5_1.SelectedValue;// = ds.Tables[0].Rows[0]["facename"].ToString();
        string chuanganqizushu = txtchuanganqizushu5_1.Value;// = ds.Tables[0].Rows[0]["sennumber"].ToString();
        string suoliangyujingzhi= txtsuoliangyujingzhi5_1.Value;// = ds.Tables[0].Rows[0]["yujingvalue"].ToString();
        string suoliangbaojingzhi= suoliangbaojingzhi5_1.Value;// = ds.Tables[0].Rows[0]["alarmvalue"].ToString();

        if (lbl_cequ5_1.Text == "i")
        {
            //插入
            string sqlck = "select areaname from HuoZhuPar where areaName='" +cequmingcheng+ "' and FaceName ='"+gongzuomianmingcheng+ "'";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass5()");
                SystemTool.AlertShow(this, "该工作面活柱缩量参数已存在");
                return;
            }

            string sql = "insert into HuoZhuPar(areaname,facename,firstconnect,secondconnect,yujingvalue,alarmvalue,datacycle,soundalarm,sennumber)values('"+cequmingcheng+"','"+gongzuomianmingcheng+"','左柱','右柱','"+suoliangyujingzhi+"','"+suoliangbaojingzhi+"','2','0','"+chuanganqizushu+"')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass5()");
                SystemTool.AlertShow(this, "操作成功");
                //巷道参数列表4-1
                sql = "select * from HuoZhuPar where " + ViewState["rpt_gongzuomiancanshuliebiao5_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_gongzuomiancanshuliebiao5_1.DataSource = ds.Tables[0];
                rpt_gongzuomiancanshuliebiao5_1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update HuoZhuPar set yujingvalue='" + suoliangyujingzhi + "',alarmvalue='" + suoliangbaojingzhi + "',sennumber='" + chuanganqizushu + "' where areaname='" + cequmingcheng + "' and facename='" + gongzuomianmingcheng + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                SystemTool.JavascriptShow(this, "changclass5()");
                //巷道参数列表4-1
                sql = "select * from HuoZhuPar where " + ViewState["rpt_gongzuomiancanshuliebiao5_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_gongzuomiancanshuliebiao5_1.DataSource = ds.Tables[0];
                rpt_gongzuomiancanshuliebiao5_1.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string cequmingcheng0 = ddl_cequmingcheng5_1.SelectedValue = "0";
        string xiangdaomingcheng0 = ddl_gongzuomianmingcheng5_1.SelectedValue = "0";
        string chuanganqizushu0 = txtchuanganqizushu5_1.Value = "";
        txtsuoliangyujingzhi5_1.Value = "";
        suoliangbaojingzhi5_1.Value = "";
        lbl_cequ5_1.Text = "i";
    }
    //活柱缩量：传感器列表添加修改
    protected void lbtn_save_5_2_Click(object sender, EventArgs e) {
        string cequmingcheng = ddl_cequmingcheng5_1.SelectedValue;
        string suoshugongzuomian = ddl_suoshugongzuomian5_2.SelectedValue;
        string chuanganqibainhao = txtchuanganqibianhao5_2.Value;
        string zhijiabianhao = txtzhijiabianhao5_2.Value;
        string julicailiaoxiang = txtjulicailiaoxiang5_2.Value;
        string shiyongzhuangtai = ddl_shiyongzhuangtai5_2.SelectedValue;
        if (lbl_cequ5_2.Text == "i")
        {
            //插入
            string sqlck = "select areaname from HuoZhuSenInfo where AreaName='" +cequmingcheng+ "' and FaceName ='"+suoshugongzuomian+ "' and SensorNo ='"+chuanganqibainhao+ "' ";
            string re = DB.ExecuteSqlValue(sqlck, null);
            if (re != "" && re != "no")
            {
                SystemTool.JavascriptShow(this, "changclass5()");
                SystemTool.AlertShow(this, "该传感器信息已存在");
                return;
            }

            string sql = "insert into HuoZhuSenInfo(areaname,facename,sensorno,bracketno,distance,usestate)values('"+cequmingcheng+"','"+suoshugongzuomian+"','"+chuanganqibainhao+"','"+zhijiabianhao+"','"+julicailiaoxiang+"','"+shiyongzhuangtai+"')";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass5()");
                SystemTool.AlertShow(this, "操作成功");
                //传感器列表5-2
                sql = "SELECT * FROM HuoZhuSenInfo where " + ViewState["rpt_chuanganqiliebiao5_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao5_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao5_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        else
        {
            //修改
            //string sql = @"insert into PressurePar(areaname,facename,sennumber,firstconnect,secondconnect,firstd,sencondd,pressuremax,pressuremin,pressurealarm)
            //values('" + cequmingc + "','" + gongzuomianmingcheng + "','" + chuanganqizushu + "','" + diyiyalitongdao + "','" + dieryalitongdao + "','" + gangjing1 + "','" + gangjing2 + "','" + yalishangxian + "','" + yalixiaxian + "','" + qianjindingbaojingzhi + "')";
            string sql = "update HuoZhuSenInfo set bracketno='"+zhijiabianhao+"',distance='"+julicailiaoxiang+"',usestate='"+shiyongzhuangtai+"' where areaname='" + cequmingcheng + "' and facename='" + suoshugongzuomian + "' and sensorno='" + chuanganqibainhao + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.JavascriptShow(this, "changclass5()");
                SystemTool.AlertShow(this, "操作成功");

                //传感器列表5-2
                sql = "SELECT * FROM HuoZhuSenInfo where " + ViewState["rpt_chuanganqiliebiao5_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao5_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao5_2.DataBind();

            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        string cequmingcheng0 = ddl_cequmingcheng5_1.SelectedValue="0";
        string suoshugongzuomian0 = ddl_suoshugongzuomian5_2.SelectedValue="0";
        string chuanganqibainhao0 = txtchuanganqibianhao5_2.Value="";
        string zhijiabianhao0 = txtzhijiabianhao5_2.Value="";
        string julicailiaoxiang0 = txtjulicailiaoxiang5_2.Value="";
        string shiyongzhuangtai0 = ddl_shiyongzhuangtai5_2.SelectedValue="0";
        lbl_cequ5_2.Text = "i";
    }
    //活柱缩量查询：工作面参数列表，传感器参数列表
    protected void lbtn_search5_Click(object sender, EventArgs e)
    {
        ViewState["rpt_gongzuomiancanshuliebiao5_1"] = " 1=1";
        ViewState["rpt_chuanganqiliebiao5_2"] = " 1=1";
        string cequmingc = ddl_cequmingcheng5_1.SelectedValue;
        string gongzuomianmingcheng = ddl_gongzuomianmingcheng5_1.SelectedValue;// = ddl_xiangdao_2_1.SelectedValue;
        if (cequmingc != "0")
        {
            ViewState["rpt_gongzuomiancanshuliebiao5_1"] += " and areaname='" + cequmingc + "'";
            ViewState["rpt_chuanganqiliebiao5_2"] += " and areaname='" + cequmingc + "'";
        }
        if (gongzuomianmingcheng != "0")
        {
            ViewState["rpt_gongzuomiancanshuliebiao5_1"] += " and facename='" + gongzuomianmingcheng + "'";
            ViewState["rpt_chuanganqiliebiao5_2"] += " and facename='" + gongzuomianmingcheng + "'";
        }
        //SystemTool.JavascriptShow(this, "changclass1()");
        //活柱缩量工作面参数列表
        string sql = "SELECT * FROM HuoZhuPar where " + ViewState["rpt_gongzuomiancanshuliebiao5_1"];
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_gongzuomiancanshuliebiao5_1.DataSource = ds.Tables[0];
        rpt_gongzuomiancanshuliebiao5_1.DataBind();
        //活柱缩量传感器列表
        sql = "SELECT * FROM HuoZhuSenInfo where " + ViewState["rpt_chuanganqiliebiao5_2"];
        ds = DB.ExecuteSqlDataSet(sql, null);
        rpt_chuanganqiliebiao5_2.DataSource = ds.Tables[0];
        rpt_chuanganqiliebiao5_2.DataBind();
        SystemTool.JavascriptShow(this, "changclass5()");
    }
    //活柱缩量工作面参数列表修改删除
    protected void rpt_gongzuomiancanshuliebiao5_1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;

        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "select * from HuoZhuPar where areaname='" + areaname.Text + "' and facename='" + facename + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                //string cequmingc = ddl_cequ1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string cequmingcheng = ddl_cequmingcheng5_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string xiangdaomingcheng = ddl_gongzuomianmingcheng5_1.SelectedValue = ds.Tables[0].Rows[0]["facename"].ToString();
                string chuanganqizushu = txtchuanganqizushu5_1.Value = ds.Tables[0].Rows[0]["sennumber"].ToString();
                txtsuoliangyujingzhi5_1.Value = ds.Tables[0].Rows[0]["yujingvalue"].ToString();
                suoliangbaojingzhi5_1.Value = ds.Tables[0].Rows[0]["alarmvalue"].ToString();
                lbl_cequ5_1.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from HuoZhuPar where areaname='" + areaname.Text + "' and facename='" + facename + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                //巷道参数列表4-1
                sql = "select * from HuoZhuPar where " + ViewState["rpt_gongzuomiancanshuliebiao5_1"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_gongzuomiancanshuliebiao5_1.DataSource = ds.Tables[0];
                rpt_gongzuomiancanshuliebiao5_1.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass5()");
    }
    //活柱缩量传感器列表修改删除
    protected void rpt_chuanganqiliebiao5_2_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Label areaname = e.Item.FindControl("lbl_areaname") as Label;
        Label chuanganqibianhao = e.Item.FindControl("lbl_SenNumber") as Label;
        string cmd = e.CommandName.ToString();
        string facename = e.CommandArgument.ToString();
        string sql = "";
        if (cmd == "lbtn_xiugai")
        {
            BindData();
            sql = "SELECT * FROM HuoZhuSenInfo where areaname='" + areaname.Text + "' and facename='" + facename + "' and sensorno='" + chuanganqibianhao.Text + "'";
            DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            if (ds.Tables[0].Rows.Count > 0)
            {

                
                string cequmingcheng0 = ddl_cequmingcheng5_1.SelectedValue = ds.Tables[0].Rows[0]["areaname"].ToString();
                string suoshugongzuomian0 = ddl_suoshugongzuomian5_2.SelectedValue = ds.Tables[0].Rows[0]["facename"].ToString();
                string chuanganqibainhao0 = txtchuanganqibianhao5_2.Value = ds.Tables[0].Rows[0]["sensorno"].ToString();
                string zhijiabianhao0 = txtzhijiabianhao5_2.Value = ds.Tables[0].Rows[0]["bracketno"].ToString();
                string julicailiaoxiang0 = txtjulicailiaoxiang5_2.Value = ds.Tables[0].Rows[0]["distance"].ToString();
                string shiyongzhuangtai0 = ddl_shiyongzhuangtai5_2.SelectedValue = ds.Tables[0].Rows[0]["usestate"].ToString();
                lbl_cequ5_2.Text = "u";

            }
        }
        if (cmd == "lbtn_delete")
        {
            sql = "delete from HuoZhuSenInfo where areaname='" + areaname.Text + "' and facename='" + facename + "' and sensorno='" + chuanganqibianhao.Text + "'";
            int result = DB.ExecuteSql(sql, null);
            if (result > 0)
            {
                SystemTool.AlertShow(this, "操作成功");
                //传感器列表4-2
                sql = "SELECT * FROM HuoZhuSenInfo where " + ViewState["rpt_chuanganqiliebiao5_2"];
                DataSet ds = DB.ExecuteSqlDataSet(sql, null);
                rpt_chuanganqiliebiao5_2.DataSource = ds.Tables[0];
                rpt_chuanganqiliebiao5_2.DataBind();
            }
            else
            {
                SystemTool.AlertShow(this, "操作失败");
            }
        }
        SystemTool.JavascriptShow(this, "changclass5()");
    }

    //活柱缩量异步刷新下拉框
    
    protected void ddl_test1_changed(object sender, EventArgs e)
    {
        init();
        string sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='" + ddl_cequ1.SelectedValue + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        ddl_gongzuomian1.Items.Clear();
        ddl_gongzuomian1_2.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomian1.DataSource = ds;
            ddl_gongzuomian1.DataValueField = "workfaceName";
            ddl_gongzuomian1.DataTextField = "workfaceName";
            ddl_gongzuomian1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian1.Items.Insert(0, item1);

            ddl_gongzuomian1_2.DataSource = ds.Tables[0];
            ddl_gongzuomian1_2.DataValueField = "workfaceName";
            ddl_gongzuomian1_2.DataTextField = "workfaceName";
            ddl_gongzuomian1_2.DataBind();
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_gongzuomian1_2.Items.Insert(0, item11);
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomian1.Items.Insert(0, item1);
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_gongzuomian1_2.Items.Insert(0, item11);
        }
        //SystemTool.JavascriptShow(this, "changclass5()");
    }
    protected void ddl_test2_changed(object sender, EventArgs e)
    {
        init();
        string sql = "SELECT roadName FROM RoadInfo where areaName='" + ddl_cequ2_1.SelectedValue + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        ddl_xiangdao_2_1.Items.Clear();
        ddl_suoshuxiangdao2_2.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_xiangdao_2_1.DataSource = ds;
            ddl_xiangdao_2_1.DataValueField = "roadName";
            ddl_xiangdao_2_1.DataTextField = "roadName";
            ddl_xiangdao_2_1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_xiangdao_2_1.Items.Insert(0, item1);

            ddl_suoshuxiangdao2_2.DataSource = ds.Tables[0];
            ddl_suoshuxiangdao2_2.DataValueField = "roadName";
            ddl_suoshuxiangdao2_2.DataTextField = "roadName";
            ddl_suoshuxiangdao2_2.DataBind();
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao2_2.Items.Insert(0, item11);
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_xiangdao_2_1.Items.Insert(0, item1);
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao2_2.Items.Insert(0, item11);
        }
        SystemTool.JavascriptShow(this, "changclass2()");
    }
    protected void ddl_test3_changed(object sender, EventArgs e)
    {
        init();
        string sql = "SELECT roadName FROM RoadInfo where areaName='" + ddl_cequmingcheng3_1.SelectedValue + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        ddl_xiangdaomingcheng3_1.Items.Clear();
        ddl_suoshuxiangdao3_2.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_xiangdaomingcheng3_1.DataSource = ds;
            ddl_xiangdaomingcheng3_1.DataValueField = "roadName";
            ddl_xiangdaomingcheng3_1.DataTextField = "roadName";
            ddl_xiangdaomingcheng3_1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_xiangdaomingcheng3_1.Items.Insert(0, item1);

            ddl_suoshuxiangdao3_2.DataSource = ds.Tables[0];
            ddl_suoshuxiangdao3_2.DataValueField = "roadName";
            ddl_suoshuxiangdao3_2.DataTextField = "roadName";
            ddl_suoshuxiangdao3_2.DataBind();
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao3_2.Items.Insert(0, item11);
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_xiangdaomingcheng3_1.Items.Insert(0, item1);
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao3_2.Items.Insert(0, item11);
        }
        SystemTool.JavascriptShow(this, "changclass3()");
    }
    protected void ddl_test4_changed(object sender, EventArgs e)
    {
        init();
        string sql = "SELECT roadName FROM RoadInfo where areaName='" + ddl_cequmingcheng4_1.SelectedValue + "'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        ddl_xiangdaomingcheng4_1.Items.Clear();
        ddl_suoshuxiangdao4_2.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_xiangdaomingcheng4_1.DataSource = ds;
            ddl_xiangdaomingcheng4_1.DataValueField = "roadName";
            ddl_xiangdaomingcheng4_1.DataTextField = "roadName";
            ddl_xiangdaomingcheng4_1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_xiangdaomingcheng4_1.Items.Insert(0, item1);

            ddl_suoshuxiangdao4_2.DataSource = ds.Tables[0];
            ddl_suoshuxiangdao4_2.DataValueField = "roadName";
            ddl_suoshuxiangdao4_2.DataTextField = "roadName";
            ddl_suoshuxiangdao4_2.DataBind();
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao4_2.Items.Insert(0, item11);
        }
        else
        {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_xiangdaomingcheng4_1.Items.Insert(0, item1);
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshuxiangdao4_2.Items.Insert(0, item11);
        }
        SystemTool.JavascriptShow(this, "changclass4()");
    }  
    protected void ddl_test_changed(object sender, EventArgs e)
    {
        init();
        string sql = "SELECT workfaceName FROM WorkfaceInfo where areaName='"+ddl_cequmingcheng5_1.SelectedValue+"'";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        ddl_gongzuomianmingcheng5_1.Items.Clear();
        ddl_suoshugongzuomian5_2.Items.Clear();
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_gongzuomianmingcheng5_1.DataSource = ds;
            ddl_gongzuomianmingcheng5_1.DataValueField = "workfaceName";
            ddl_gongzuomianmingcheng5_1.DataTextField = "workfaceName";
            ddl_gongzuomianmingcheng5_1.DataBind();
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomianmingcheng5_1.Items.Insert(0, item1);

            ddl_suoshugongzuomian5_2.DataSource = ds.Tables[0];
            ddl_suoshugongzuomian5_2.DataValueField = "workfaceName";
            ddl_suoshugongzuomian5_2.DataTextField = "workfaceName";
            ddl_suoshugongzuomian5_2.DataBind();
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshugongzuomian5_2.Items.Insert(0, item11);
        }
        else {
            ListItem item1 = new ListItem("--请选择--", "0");
            ddl_gongzuomianmingcheng5_1.Items.Insert(0, item1);
            ListItem item11 = new ListItem("--请选择--", "0");
            ddl_suoshugongzuomian5_2.Items.Insert(0, item11);
        }
        SystemTool.JavascriptShow(this, "changclass5()");
    }  


    protected void lbtn_gongzuomian_Click(object sender, EventArgs e)
    {
        //string aname = txtgongzuomian.Value;
        //string cequ = ddl_AreaInfo.SelectedValue;
        //if (cequ == "0")
        //{
        //    SystemTool.AlertShow(this, "测区名称不能为空");
        //    return;
        //}
        //if (aname == "")
        //{
        //    SystemTool.AlertShow(this, "工作面名称不能为空");
        //    return;
        //}
        //string sql = "insert into workfaceinfo (areaname,workfacename) values('" + cequ + "','" + aname + "')";
        //int result = DB.ExecuteSql(sql, null);
        //if (result > 0)
        //{
        //    SystemTool.JavascriptShow(this, "changclass2()");
        //    sql = "select * from workfaceinfo";
        //    DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        //    rpt_workface.DataSource = ds.Tables[0];
        //    rpt_workface.DataBind();
        //    ddl_AreaInfo.SelectedValue = "0";
        //    txtgongzuomian.Value = "";
        //}
        //else
        //{
        //    SystemTool.AlertShow(this, "操作失败");
        //}
    }
    protected void lbtn_xiangdao_Click(object sender, EventArgs e)
    {
        //string aname = txtxiangdao.Value;
        //string cequ = ddl_cequ2.SelectedValue;
        //if (cequ == "0")
        //{
        //    SystemTool.AlertShow(this, "测区名称不能为空");
        //    return;
        //}
        //if (aname == "")
        //{
        //    SystemTool.AlertShow(this, "巷道名称不能为空");
        //    return;
        //}
        //string sql = "insert into roadinfo (areaname,roadname) values('" + cequ + "','" + aname + "')";
        //int result = DB.ExecuteSql(sql, null);
        //if (result > 0)
        //{
        //    SystemTool.JavascriptShow(this, "changclass3()");
        //    sql = "select * from roadinfo";
        //    DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        //    rpt_xiangdao.DataSource = ds.Tables[0];
        //    rpt_xiangdao.DataBind();
        //    ddl_cequ2.SelectedValue = "0";
        //    txtxiangdao.Value = "";
        //}
        //else
        //{
        //    SystemTool.AlertShow(this, "操作失败");
        //}
    }
}