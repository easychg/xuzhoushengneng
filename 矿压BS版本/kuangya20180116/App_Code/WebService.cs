using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Data;

/// <summary>
///WebService 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
 [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    public WebService () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }
    [WebMethod]
    public string ZongcaiZaiXianJianCe(string cequ, string gongzuomian)
    {
        //支架号及对应值
        string sql = @"select psi.bracketno,isnull(pnd.pressure1,0) pressure1,isnull(pnd.pressure2,0) pressure2,isnull(txstate,'故障') txstate from PreSenInfo psi 
left join PreNewData pnd on psi.areaname=pnd.areaname and psi.facename=pnd.facename and psi.sensorno=pnd.sensorno
where psi.areaname='" + cequ + "' and psi.facename='" + gongzuomian + "' order by psi.BracketNo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1}", "categories", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                if (i == 0)
                {
                    sb.Append("\"" + ds.Tables[0].Rows[i]["bracketno"].ToString() + "\"");
                }
                else {
                    sb.Append(",\"" + ds.Tables[0].Rows[i]["bracketno"].ToString() + "\"");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series0", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["pressure1"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["pressure1"].ToString() + "");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series1", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["pressure2"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["pressure2"].ToString() + "");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "txstate", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("\"" + ds.Tables[0].Rows[i]["txstate"].ToString() + "\"");
                }
                else
                {
                    sb.Append(",\"" + ds.Tables[0].Rows[i]["txstate"].ToString() + "\"");
                }
            }
            sb.Append("],");
        }
        else {
            sb.Append(string.Format("\"{0}\":{1},", "categories", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series0", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series1", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "txstate", "[]"));
        }
        //警戒最大值最小值
        sql = "select pressuremax,pressuremin from PressurePar where areaname='" + cequ + "' and facename='" + gongzuomian + "'";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1},", "max", ds.Tables[0].Rows[0]["pressuremax"].ToString()));
            sb.Append(string.Format("\"{0}\":{1}", "min", ds.Tables[0].Rows[0]["pressuremin"].ToString()));
        }
        else {
            sb.Append(string.Format("\"{0}\":{1},", "max", "0"));
            sb.Append(string.Format("\"{0}\":{1}", "min", "0"));
        }
        
        sb.Append("}");
        return sb.ToString();
    }
    /// <summary>
    /// 综采工作阻力历史数据分析曲线
    /// </summary>
    /// <param name="cequ"></param>
    /// <param name="gongzuomian"></param>
    /// <returns></returns>
    [WebMethod]
    public string ZCGZZLLSSJFXQX(string cequ, string gongzuomian, string bracketno,string t1,string t2)
    {
        //支架号及对应值
        string sql = @"select avg(pressure1) pressure1,avg(pressure2) pressure2,sensorno,time from PressureData where areaname='" + cequ + "' and facename='" + gongzuomian + "'  and left(convert(nvarchar(50),time,20),10)>='" + t1 + "' and left(convert(nvarchar(50),time,20),10) <='" + t2 + "' and sensorno=(select SensorNo from PreSenInfo where areaName='" + cequ + "' and facename='" + gongzuomian + "' and bracketno='" + bracketno + "') group by sensorno,time";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1}", "categories", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("\"" + ds.Tables[0].Rows[i]["time"].ToString() + "\"");
                }
                else
                {
                    sb.Append(",\"" + ds.Tables[0].Rows[i]["time"].ToString() + "\"");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series0", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["pressure1"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["pressure1"].ToString() + "");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series1", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["pressure2"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["pressure2"].ToString() + "");
                }
            }
            sb.Append("],");
        }
        else
        {
            sb.Append(string.Format("\"{0}\":{1},", "categories", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series0", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series1", "[]"));
            
        }
        //警戒最大值最小值
        sql = "select pressuremax,pressuremin from PressurePar where areaname='" + cequ + "' and facename='" + gongzuomian + "'";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1},", "max", ds.Tables[0].Rows[0]["pressuremax"].ToString()));
            sb.Append(string.Format("\"{0}\":{1},", "min", ds.Tables[0].Rows[0]["pressuremin"].ToString()));
        }
        else
        {
            sb.Append(string.Format("\"{0}\":{1},", "max", "0"));
            sb.Append(string.Format("\"{0}\":{1},", "min", "0"));
        }
        //进尺数据详情
        sql = "select sum(hcjcnose) hcjcnose,sum(hcjcmid) hcjcmid,sum(hcjctail) hcjctail from Huicai where AreaName = '" + cequ + "' and FaceName = '" + gongzuomian + "' and left(convert(nvarchar(50),datetime,20),10) <='" + t2 + "' ";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1},", "head", ds.Tables[0].Rows[0]["hcjcnose"].ToString()));
            sb.Append(string.Format("\"{0}\":{1},", "body", ds.Tables[0].Rows[0]["hcjcmid"].ToString()));
            sb.Append(string.Format("\"{0}\":{1}", "foot", ds.Tables[0].Rows[0]["hcjctail"].ToString()));
        }
        else
        {
            sb.Append(string.Format("\"{0}\":{1},", "head", "0"));
            sb.Append(string.Format("\"{0}\":{1},", "body", "0"));
            sb.Append(string.Format("\"{0}\":{1}", "foot", "0"));
        }
        sb.Append("}");
        return sb.ToString();
    }
    /// <summary>
    /// 每日工作阻力分布分析
    /// </summary>
    /// <param name="cequ"></param>
    /// <param name="gongzuomian"></param>
    /// <param name="yuzhi"></param>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <returns></returns>
    [WebMethod]
    public string MRGZZLFBQX(string cequ, string gongzuomian, string yuzhi, string t1, string t2)
    {
        //支架号及对应值
        string sql = @"select BracketNo,avg(Pressure1) as p1,avg(Pressure2) as p2,p.time,avg(Pressure1+Pressure2)/2 as p3 from PressureData p 
inner join PreSenInfo s on p.areaName = s.AreaName and p.FaceName = s.FaceName and p.SensorNo = s.SensorNo 
where p.areaName = '"+cequ+"' and p.FaceName = '"+gongzuomian+"' and s.Type = '液压支架' and time >='"+t1+"' And time<='"+t2+"' and (pressure1 > '"+yuzhi+"' or Pressure2 >'"+yuzhi+"') group by BracketNo,p.time order by BracketNo";
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        StringBuilder sb = new StringBuilder();
        sb.Append("{");
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1}", "categories", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("\"" + ds.Tables[0].Rows[i]["BracketNo"].ToString() + "\"");
                }
                else
                {
                    sb.Append(",\"" + ds.Tables[0].Rows[i]["BracketNo"].ToString() + "\"");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series0", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["p1"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["p1"].ToString() + "");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series1", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["p2"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["p2"].ToString() + "");
                }
            }
            sb.Append("],");
            sb.Append(string.Format("\"{0}\":{1}", "series2", "["));
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("" + ds.Tables[0].Rows[i]["p3"].ToString() + "");
                }
                else
                {
                    sb.Append("," + ds.Tables[0].Rows[i]["p3"].ToString() + "");
                }
            }
            sb.Append("],");
        }
        else
        {
            sb.Append(string.Format("\"{0}\":{1},", "categories", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series0", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series1", "[]"));
            sb.Append(string.Format("\"{0}\":{1},", "series2", "[]"));

        }
        //警戒最大值最小值
        sql = "select pressuremax,pressuremin from PressurePar where areaname='" + cequ + "' and facename='" + gongzuomian + "'";
        ds = DB.ExecuteSqlDataSet(sql, null);
        if (ds.Tables[0].Rows.Count > 0)
        {
            sb.Append(string.Format("\"{0}\":{1},", "max", ds.Tables[0].Rows[0]["pressuremax"].ToString()));
            sb.Append(string.Format("\"{0}\":{1}", "min", ds.Tables[0].Rows[0]["pressuremin"].ToString()));
        }
        else
        {
            sb.Append(string.Format("\"{0}\":{1},", "max", "0"));
            sb.Append(string.Format("\"{0}\":{1}", "min", "0"));
        }
       
        sb.Append("}");
        return sb.ToString();
    }
}
