<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        
        // 在应用程序启动时运行的代码
        //定时自动刷新任务1秒
        System.Timers.Timer myTimer = new System.Timers.Timer(1000); // 每小时判断一下，1分钟为60000,1小时为3600000
        myTimer.Elapsed += new System.Timers.ElapsedEventHandler(onzaixian); //执行需要操作的代码，OnTimedEvent是要执行的方法名称 
        myTimer.Interval = 1000;
        myTimer.Enabled = true;
        
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        //在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        //在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式 
        //设置为 StateServer 或 SQLServer，则不会引发该事件。

    }
    //自动刷新一次
    public void onzaixian(object sender, System.Timers.ElapsedEventArgs e)
    {
        //查有哪些需要刷新
        
        string time = DateTime.Now.ToString("yyyy-MM-dd");
        
        string time2 = DateTime.Now.ToString("mm:ss");
        string dte = DateTime.Now.ToString("yyyy-MM-dd");
        //time = "2016-02-29";
        //dte = "2016-02-29";
        if (time2 == "30:00" || time2 == "59:59") {
            //每半小时执行一次
            
            string sqldel = "delete from PreReport where reportdate='" + time + "'";
            DB.ExecuteSql(sqldel, null);
            string sql = "select areaname,facename from pressuredata group by areaname,facename";
            System.Data.DataSet ds = DB.ExecuteSqlDataSet(sql, null);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string sql2 = "select bracketno from PreSenInfo where areaname='" + ds.Tables[0].Rows[i]["areaname"].ToString() + "' and facename='" + ds.Tables[0].Rows[i]["facename"].ToString() + "' and type='液压支架'";
                System.Data.DataSet ds2 = DB.ExecuteSqlDataSet(sql2, null);
                for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                {
                    string dts = time;

                    string cequ = ds.Tables[0].Rows[i]["areaname"].ToString();
                    string gzm = ds.Tables[0].Rows[i]["facename"].ToString();
                    string zjbh = ds2.Tables[0].Rows[j]["bracketno"].ToString();
                    System.Data.DataTable dt_img = new System.Data.DataTable();
                    System.Data.DataTable dt_export = new System.Data.DataTable();
                    string zhu = "3";//整架
                    string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();
                    DrawImage.DrawingCurve dc = new DrawImage.DrawingCurve(strConn);
                    //Bitmap img = DrawImage.DrawingCurve.DrawingImg(cequ, gzm, zjbh, dts, dte);
                    decimal CCL = Convert.ToDecimal(ConfigurationManager.ConnectionStrings["YuZhi"].ToString());//小于此值，舍去
                    decimal ZLvalue = Convert.ToDecimal(ConfigurationManager.ConnectionStrings["ZLvalue"].ToString());
                    DrawImage.DrawingCurve.DrawingImg7(cequ, gzm, zhu, zjbh, dts, dte, dt_img, dt_export, CCL, ZLvalue);
                    string sqlck = "select * from PreReport where areaName='" + cequ + "' and faceName='" + gzm + "' and bracketNo='" + zjbh + "' and reportDate='" + dts + "'";
                    System.Data.DataSet dsck = DB.ExecuteSqlDataSet(sqlck, null);
                    if (dsck.Tables[0].Rows.Count <= 0)
                    {
                        decimal CCLmax = 0;// Convert.ToDecimal(dt_export.Compute("Min(chuchengli)", "true"));
                        decimal MZLmax = 0;// Convert.ToDecimal(dt_export.Compute("Max(mozuli)", "true"));
                        int intCCL = 0;
                        int intMZL = 0;
                        decimal decCCL = 0;
                        decimal decMZL = 0;
                        for (int k = 0; k < dt_export.Rows.Count; k++)
                        {
                            if (dt_export.Rows[k]["chuchengli"].ToString() != "")
                            {
                                intCCL += 1;
                                decCCL += Convert.ToDecimal(dt_export.Rows[k]["chuchengli"].ToString());
                                CCLmax = CCLmax > Convert.ToDecimal(dt_export.Rows[k]["chuchengli"].ToString()) ? CCLmax : Convert.ToDecimal(dt_export.Rows[k]["chuchengli"].ToString());

                            }
                            if (dt_export.Rows[k]["mozuli"].ToString() != "")
                            {
                                intMZL += 1;
                                decMZL += Convert.ToDecimal(dt_export.Rows[k]["mozuli"].ToString());
                                MZLmax = MZLmax > Convert.ToDecimal(dt_export.Rows[k]["mozuli"].ToString()) ? MZLmax : Convert.ToDecimal(dt_export.Rows[k]["mozuli"].ToString());
                            }
                        }
                        decimal CCLavg = intCCL == 0 ? 0 : decCCL / intCCL;
                        decimal MZLavg = intMZL == 0 ? 0 : decMZL / intMZL;
                        decimal GZZLmax = 0;
                        decimal GZZLmin = 0;
                        decimal GZZLavg = 0;
                        string SensorNo = "";
                        string distance = "";
                        //dte += " 23:59:59";
                        string sqlgzzl = @"select max(pressure1) maxpre,min(pressure1) minpre,max(pressure2) maxpre2,min(pressure2) minpre2,avg(pressure1+pressure2)/2 avgpre3 from pressuredata 
where areaName='" + cequ + "' and FaceName = '" + gzm + "' and SensorNo = (select SensorNo from PreSenInfo where areaName = '" + cequ + "' and FaceName='" + gzm + "' and BracketNo = '" + zjbh + "') and time between '" + dts + "' and '" + dte + " 23:59:59' and (Pressure1>=" + CCL + " or Pressure2>=" + CCL + ") ";
                        System.Data.DataSet dsgzzl = DB.ExecuteSqlDataSet(sqlgzzl, null);
                        if (dsgzzl.Tables[0].Rows.Count > 0)
                        {
                            string m1 = dsgzzl.Tables[0].Rows[0]["maxpre"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["maxpre"].ToString();
                            decimal maxpre = Convert.ToDecimal(m1);
                            string m2 = dsgzzl.Tables[0].Rows[0]["maxpre2"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["maxpre2"].ToString();
                            decimal maxpre2 = Convert.ToDecimal(m2);
                            string m3 = dsgzzl.Tables[0].Rows[0]["avgpre3"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["avgpre3"].ToString();
                            decimal avgpre3 = Convert.ToDecimal(m3);
                            GZZLmax = maxpre > maxpre2 ? maxpre : maxpre2;
                            GZZLavg = avgpre3;
                            string m4 = dsgzzl.Tables[0].Rows[0]["minpre"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["minpre"].ToString();
                            decimal minpre = Convert.ToDecimal(m4);
                            string m5 = dsgzzl.Tables[0].Rows[0]["minpre2"].ToString() == "" ? "0" : dsgzzl.Tables[0].Rows[0]["minpre2"].ToString();
                            decimal minpre2 = Convert.ToDecimal(m5);
                            GZZLmin = minpre < minpre2 ? minpre : minpre2;

                        }
                        sqlgzzl = "select SensorNo,distance from PreSenInfo where areaName = '" + cequ + "' and FaceName='" + gzm + "' and BracketNo = '" + zjbh + "'";
                        dsgzzl = DB.ExecuteSqlDataSet(sqlgzzl, null);
                        if (dsgzzl.Tables[0].Rows.Count > 0)
                        {
                            SensorNo = dsgzzl.Tables[0].Rows[0]["SensorNo"].ToString();
                            distance = dsgzzl.Tables[0].Rows[0]["distance"].ToString();
                        }

                        sqlgzzl = "insert into PreReport (areaName,faceName,sensorNo,bracketNo,distance,ZLmax,ZLavg,CCLmax,CCLavg,MZLmax,MZLavg,reportDate,zlmin) values('" + cequ + "','" + gzm + "','" + SensorNo + "','" + zjbh + "','" + distance + "','" + GZZLmax + "','" + GZZLavg + "','" + CCLmax + "','" + CCLavg + "','" + MZLmax + "','" + MZLavg + "','" + dts + "','" + GZZLmin + "')";
                        DB.ExecuteSql(sqlgzzl, null);
                    }

                }
            }
        }
    }
</script>
