using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Drawing.Imaging;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Drawing.Drawing2D;

namespace DrawImage
{
   
    public class DrawingCurve
    {
        private static string strConn {get;set;}
        public DrawingCurve(string str) {
            strConn = str;
        }
        /// <summary>
        /// 综采工作阻力历史数据分析曲线
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="FaceName"></param>
        /// <param name="zhengjia">1左柱2右柱3整架4左柱和右柱</param>
        /// <param name="BracketNo"></param>
        /// <param name="dts"></param>
        /// <param name="dte"></param>
        /// <param name="dt_img"></param>
        /// <param name="dt_export"></param>
        /// <returns></returns>
        public static Bitmap DrawingImg6(string AreaName, string FaceName, string zhengjia, string BracketNo, string dts, string dte, DataTable dt_img, DataTable dt_export)
        {
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            string sqltime = "SELECT min(time) as startT,max(time) as endT FROM PressureData where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());
                }
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;
            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("综采工作阻力历史数据分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("P(MPa)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressuremax"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                firstD = Convert.ToDecimal(result.Tables[0].Rows[0]["firstd"].ToString());
                secondD = Convert.ToDecimal(result.Tables[0].Rows[0]["sencondd"].ToString());
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            Point rec2 = new Point(intLeft, intYLong - intEnd);
            Point rec3 = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            decimal prevKN = 0;
            decimal prevMPa2 = 0;
            decimal prevKN2 = 0;
            decimal prevMPa3 = 0;
            decimal prevKN3 = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                string content = "";
                string sqlq = @"select max(pressure1) maxpre,min(pressure1) minpre,avg(pressure1) avgpre,max(pressure2) maxpre2,min(pressure2) minpre2,avg(pressure2) avgpre2,max(pressure1+pressure2)/2 maxpre3,min(pressure1+pressure2)/2 minpre3,avg(pressure1+pressure2)/2 avgpre3 from pressuredata 
where areaName='" + AreaName + "' and FaceName = '" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (zhengjia == "1" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，左柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "左柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，左柱(低)" + min.ToString() + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa = min;
                            prevKN = kn2;
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa = avg;
                            prevKN = kn3;
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                        rec = dec;
                        sb.Append("{\"" + i + "\":\"左柱" + prevMPa.ToString() + "MPa" + prevKN.ToString() + "KN\"}");
                        content += "左柱" + prevMPa.ToString("0.00") + "MPa    " + prevKN.ToString("0.00") + "KN    ";
                    }
                    //左柱右柱显示在一张图上
                    if (zhengjia == "4")
                    {
                        sb.Append(",");
                    }
                }
                //************************************压力2
                if (zhengjia == "2" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre2"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre2"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                            rec2 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，右柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "右柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，右柱(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa2 = min;
                            prevKN2 = kn2;
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "右柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa2 = avg;
                            prevKN2 = kn3;
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec2.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                        rec2 = dec;
                        sb.Append("{\"" + i + "\":\"右柱" + prevMPa2.ToString() + "MPa" + prevKN2.ToString() + "KN\"}");
                        content += "右柱" + prevMPa2.ToString("0.00") + "MPa    " + prevKN2.ToString("0.00") + "KN    ";
                    }
                }
                //**************************************整架
                if (zhengjia == "3")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre3"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre3"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"整架(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，整架(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "整架(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，整架(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa3 = min;
                            prevKN3 = kn2;
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa3 = avg;
                            prevKN3 = kn3;
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec3.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                        rec3 = dec;
                        sb.Append("{\"" + i + "\":\"整架" + prevMPa3.ToString() + "MPa" + prevKN3.ToString() + "KN\"}");
                        content += "整架" + prevMPa3.ToString("0.00") + "MPa    " + prevKN3.ToString("0.00") + "KN    ";
                    }
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //初掌力与末阻力
        public static Bitmap DrawingImg7(string AreaName, string FaceName, string zhengjia, string BracketNo, string dts, string dte, DataTable dt_img, DataTable dt_export,decimal CCMin,decimal ZLvalue)
        {
            string[,] ccmml = new string[32,3];
            string[,] ccmml2 = new string[32, 3];
            string[,] ccmml3 = new string[32, 3];
            int chuchengl = 1;
            int mozhuli = 0;
            int chuchengl2 = 1;
            int mozhuli2 = 0;
            int chuchengl3 = 1;
            int mozhuli3 = 0;
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            string sqltime = "SELECT min(time) as startT,max(time) as endT FROM PressureData where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());
                }
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;

            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            dt_export.Columns.Add("shijian", typeof(string));
            dt_export.Columns.Add("mozuli", typeof(string));
            dt_export.Columns.Add("chuchengli", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("初掌力与末阻力分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("P(MPa)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressuremax"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                firstD = Convert.ToDecimal(result.Tables[0].Rows[0]["firstd"].ToString());
                secondD = Convert.ToDecimal(result.Tables[0].Rows[0]["sencondd"].ToString());
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            Point rec2 = new Point(intLeft, intYLong - intEnd);
            Point rec3 = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            decimal prevKN = 0;
            decimal prevMPa2 = 0;
            decimal prevKN2 = 0;
            decimal prevMPa3 = 0;
            decimal prevKN3 = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                for (int m = 31; m > 0; m--) {
                    ccmml[m, 0] = ccmml[m - 1, 0];
                    ccmml[m, 1] = ccmml[m - 1, 1];
                    ccmml[m, 2] = ccmml[m - 1, 2];
                }
                for (int m = 31; m > 0; m--)
                {
                    ccmml2[m, 0] = ccmml2[m - 1, 0];
                    ccmml2[m, 1] = ccmml2[m - 1, 1];
                    ccmml2[m, 2] = ccmml2[m - 1, 2];
                }
                for (int m = 31; m > 0; m--)
                {
                    ccmml3[m, 0] = ccmml3[m - 1, 0];
                    ccmml3[m, 1] = ccmml3[m - 1, 1];
                    ccmml3[m, 2] = ccmml3[m - 1, 2];
                }
                string content = "";
                string sqlq = @"select max(pressure1) maxpre,min(pressure1) minpre,avg(pressure1) avgpre,max(pressure2) maxpre2,min(pressure2) minpre2,avg(pressure2) avgpre2,max(pressure1+pressure2)/2 maxpre3,min(pressure1+pressure2)/2 minpre3,avg(pressure1+pressure2)/2 avgpre3 from pressuredata 
where areaName='" + AreaName + "' and FaceName = '" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (zhengjia == "1" || zhengjia == "4")
                {

                    if (dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["minpre"].ToString() != "" && dsq.Tables[0].Rows[0]["avgpre"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString());
                        decimal dpt = 3M;
                        if (max-min>dpt)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            if (chuchengl == 1 && mozhuli == 0 && min < ZLvalue)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml[m, 0]) > Convert.ToDecimal(ccmml[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, rec.Y, 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre"].ToString(), "");
                                        chuchengl = 0;
                                        mozhuli = 1;
                                        break;
                                    }
                                }
                            }
                            rec = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，左柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "左柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，左柱(低)" + min.ToString() + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa = min;
                            prevKN = kn2;
                            ccmml[0, 0] = dsq.Tables[0].Rows[0]["maxpre"].ToString();
                            
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa = avg;
                            prevKN = kn3;
                            ccmml[0, 0] = dsq.Tables[0].Rows[0]["avgpre"].ToString();
                            if (chuchengl == 0 && mozhuli == 1 && Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString()) > CCMin) {
                                g.DrawRectangle(new Pen(Color.Red,2), intLeft + i, intYLong - intEnd - (int)(avg * 10), 2, 2);
                                dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), "", Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString()).ToString("0.00"));
                                chuchengl = 1;
                                mozhuli = 0;
                            }
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                        rec = dec;
                        sb.Append("{\"" + i + "\":\"左柱" + prevMPa.ToString() + "MPa" + prevKN.ToString() + "KN\"}");
                        content += "左柱" + prevMPa.ToString("0.00") + "MPa    " + prevKN.ToString("0.00") + "KN    ";
                    }
                    //左柱右柱显示在一张图上
                    if (zhengjia == "4")
                    {
                        sb.Append(",");
                    }
                }
                //************************************压力2
                if (zhengjia == "2" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre2"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre2"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString());
                        if (max - min > 3)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                            if (chuchengl2 == 1 && mozhuli2 == 0 && min < ZLvalue)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml2[m, 0]) > Convert.ToDecimal(ccmml2[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, rec2.Y, 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre2"].ToString(), "");
                                        chuchengl2 = 0;
                                        mozhuli2 = 1;
                                        break;
                                    }
                                }
                            }
                            rec2 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                            
                            rec2 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，右柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "右柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，右柱(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa2 = min;
                            prevKN2 = kn2;
                            ccmml2[0, 0] = dsq.Tables[0].Rows[0]["maxpre2"].ToString();
                            
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "右柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa2 = avg;
                            prevKN2 = kn3;
                            ccmml2[0, 0] = dsq.Tables[0].Rows[0]["avgpre2"].ToString();
                            if (chuchengl2 == 0 && mozhuli2 == 1 && Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString()) > CCMin)
                            {
                                g.DrawRectangle(new Pen(Color.Red, 2), intLeft + i, intYLong - intEnd - (int)(avg * 10), 2, 2);
                                dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), "",  Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString()).ToString("0.00"));
                                chuchengl2 = 1;
                                mozhuli2 = 0;
                            }
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec2.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                        rec2 = dec;
                        sb.Append("{\"" + i + "\":\"右柱" + prevMPa2.ToString() + "MPa" + prevKN2.ToString() + "KN\"}");
                        content += "右柱" + prevMPa2.ToString("0.00") + "MPa    " + prevKN2.ToString("0.00") + "KN    ";
                    }
                }
                //**************************************整架
                if (zhengjia == "3")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre3"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre3"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString());
                        if (max - min > 3)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec3, dec);
                            if (chuchengl3 == 1 && mozhuli3 == 0 && min < ZLvalue)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml3[m, 0]) > Convert.ToDecimal(ccmml3[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, rec3.Y, 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre3"].ToString(), "");
                                        chuchengl3 = 0;
                                        mozhuli3 = 1;
                                        break;
                                    }
                                }
                            }
                            rec3 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"整架(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，整架(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "整架(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，整架(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa3 = min;
                            prevKN3 = kn2;
                            ccmml3[0, 0] = dsq.Tables[0].Rows[0]["maxpre3"].ToString();
                            if (chuchengl3 == 1 && mozhuli3 == 0 && min < ZLvalue * 2)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml3[m, 0]) > Convert.ToDecimal(ccmml3[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, intYLong - intEnd - (int)(max * 10), 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre3"].ToString(), "");
                                        chuchengl3 = 0;
                                        mozhuli3 = 1;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa3 = avg;
                            prevKN3 = kn3;
                            ccmml3[0, 0] = dsq.Tables[0].Rows[0]["avgpre3"].ToString();
                            if (chuchengl3 == 0 && mozhuli3 == 1 && Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString()) > CCMin)
                            {
                                g.DrawRectangle(new Pen(Color.Red, 2), intLeft + i, intYLong - intEnd - (int)(avg * 10), 2, 2);
                                dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), "", Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString()).ToString("0.00"));
                                chuchengl3 = 1;
                                mozhuli3 = 0;
                            }
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec3.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec3, dec);
                        rec3 = dec;
                        sb.Append("{\"" + i + "\":\"整架" + prevMPa3.ToString() + "MPa" + prevKN3.ToString() + "KN\"}");
                        content += "整架" + prevMPa3.ToString("0.00") + "MPa    " + prevKN3.ToString("0.00") + "KN    ";
                    }
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //综采不保压分析曲线
        public static Bitmap DrawingImg8(string AreaName, string FaceName, string zhengjia, string BracketNo, string dts, string dte, DataTable dt_img, DataTable dt_export)
        {
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            string sqltime = "SELECT min(time) as startT,max(time) as endT FROM PressureData where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());
                }
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;
            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("综采不保压分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("P(MPa)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressuremax"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                firstD = Convert.ToDecimal(result.Tables[0].Rows[0]["firstd"].ToString());
                secondD = Convert.ToDecimal(result.Tables[0].Rows[0]["sencondd"].ToString());
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            Point rec2 = new Point(intLeft, intYLong - intEnd);
            Point rec3 = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            decimal prevKN = 0;
            decimal prevMPa2 = 0;
            decimal prevKN2 = 0;
            decimal prevMPa3 = 0;
            decimal prevKN3 = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                string content = "";
                string sqlq = @"select max(pressure1) maxpre,min(pressure1) minpre,avg(pressure1) avgpre,max(pressure2) maxpre2,min(pressure2) minpre2,avg(pressure2) avgpre2,max(pressure1+pressure2)/2 maxpre3,min(pressure1+pressure2)/2 minpre3,avg(pressure1+pressure2)/2 avgpre3 from pressuredata 
where areaName='" + AreaName + "' and FaceName = '" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (zhengjia == "1" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，左柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "左柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，左柱(低)" + min.ToString() + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa = min;
                            prevKN = kn2;
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa = avg;
                            prevKN = kn3;
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                        rec = dec;
                        sb.Append("{\"" + i + "\":\"左柱" + prevMPa.ToString() + "MPa" + prevKN.ToString() + "KN\"}");
                        content += "左柱" + prevMPa.ToString("0.00") + "MPa    " + prevKN.ToString("0.00") + "KN    ";
                    }
                    //左柱右柱显示在一张图上
                    if (zhengjia == "4")
                    {
                        sb.Append(",");
                    }
                }
                //************************************压力2
                if (zhengjia == "2" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre2"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre2"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                            rec2 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，右柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "右柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，右柱(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa2 = min;
                            prevKN2 = kn2;
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "右柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa2 = avg;
                            prevKN2 = kn3;
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec2.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                        rec2 = dec;
                        sb.Append("{\"" + i + "\":\"右柱" + prevMPa2.ToString() + "MPa" + prevKN2.ToString() + "KN\"}");
                        content += "右柱" + prevMPa2.ToString("0.00") + "MPa    " + prevKN2.ToString("0.00") + "KN    ";
                    }
                }
                //**************************************整架
                if (zhengjia == "3")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre3"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre3"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"整架(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，整架(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "整架(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，整架(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa3 = min;
                            prevKN3 = kn2;
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa3 = avg;
                            prevKN3 = kn3;
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec3.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                        rec3 = dec;
                        sb.Append("{\"" + i + "\":\"整架" + prevMPa3.ToString() + "MPa" + prevKN3.ToString() + "KN\"}");
                        content += "整架" + prevMPa3.ToString("0.00") + "MPa    " + prevKN3.ToString("0.00") + "KN    ";
                    }
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //安全阀开启分析
        public static Bitmap DrawingImg9(string AreaName, string FaceName, string zhengjia, string BracketNo, string dts, string dte, DataTable dt_img, DataTable dt_export, decimal CCMin, decimal ZLvalue)
        {
            string[,] ccmml = new string[32, 3];
            string[,] ccmml2 = new string[32, 3];
            string[,] ccmml3 = new string[32, 3];
            int chuchengl = 1;
            int mozhuli = 0;
            int chuchengl2 = 1;
            int mozhuli2 = 0;
            int chuchengl3 = 1;
            int mozhuli3 = 0;
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            string sqltime = "SELECT min(time) as startT,max(time) as endT FROM PressureData where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName='" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());
                }
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;

            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            dt_export.Columns.Add("shijian", typeof(string));
            dt_export.Columns.Add("mozuli", typeof(string));
            dt_export.Columns.Add("chuchengli", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("安全阀开启分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("P(MPa)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressuremax"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                firstD = Convert.ToDecimal(result.Tables[0].Rows[0]["firstd"].ToString());
                secondD = Convert.ToDecimal(result.Tables[0].Rows[0]["sencondd"].ToString());
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            Point rec2 = new Point(intLeft, intYLong - intEnd);
            Point rec3 = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            decimal prevKN = 0;
            decimal prevMPa2 = 0;
            decimal prevKN2 = 0;
            decimal prevMPa3 = 0;
            decimal prevKN3 = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                for (int m = 31; m > 0; m--)
                {
                    ccmml[m, 0] = ccmml[m - 1, 0];
                    ccmml[m, 1] = ccmml[m - 1, 1];
                    ccmml[m, 2] = ccmml[m - 1, 2];
                }
                for (int m = 31; m > 0; m--)
                {
                    ccmml2[m, 0] = ccmml2[m - 1, 0];
                    ccmml2[m, 1] = ccmml2[m - 1, 1];
                    ccmml2[m, 2] = ccmml2[m - 1, 2];
                }
                for (int m = 31; m > 0; m--)
                {
                    ccmml3[m, 0] = ccmml3[m - 1, 0];
                    ccmml3[m, 1] = ccmml3[m - 1, 1];
                    ccmml3[m, 2] = ccmml3[m - 1, 2];
                }
                string content = "";
                string sqlq = @"select max(pressure1) maxpre,min(pressure1) minpre,avg(pressure1) avgpre,max(pressure2) maxpre2,min(pressure2) minpre2,avg(pressure2) avgpre2,max(pressure1+pressure2)/2 maxpre3,min(pressure1+pressure2)/2 minpre3,avg(pressure1+pressure2)/2 avgpre3 from pressuredata 
where areaName='" + AreaName + "' and FaceName = '" + FaceName + "' and SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (zhengjia == "1" || zhengjia == "4")
                {

                    if (dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString());
                        if (max - min > 3)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            if (chuchengl == 1 && mozhuli == 0 && min < ZLvalue)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml[m, 0]) > Convert.ToDecimal(ccmml[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, rec.Y, 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre"].ToString(), "");
                                        chuchengl = 0;
                                        mozhuli = 1;
                                        break;
                                    }
                                }
                            }
                            rec = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，左柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "左柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，左柱(低)" + min.ToString() + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa = min;
                            prevKN = kn2;
                            ccmml[0, 0] = dsq.Tables[0].Rows[0]["maxpre"].ToString();
                            
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                            rec = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa = avg;
                            prevKN = kn3;
                            ccmml[0, 0] = dsq.Tables[0].Rows[0]["avgpre"].ToString();
                            if (chuchengl == 0 && mozhuli == 1 && Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre"].ToString()) > CCMin)
                            {
                                g.DrawRectangle(new Pen(Color.Red, 2), intLeft + i, intYLong - intEnd - (int)(avg * 10), 2, 2);
                                dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), "", dsq.Tables[0].Rows[0]["avgpre"].ToString());
                                chuchengl = 1;
                                mozhuli = 0;
                            }
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                        rec = dec;
                        sb.Append("{\"" + i + "\":\"左柱" + prevMPa.ToString() + "MPa" + prevKN.ToString() + "KN\"}");
                        content += "左柱" + prevMPa.ToString("0.00") + "MPa    " + prevKN.ToString("0.00") + "KN    ";
                    }
                    //左柱右柱显示在一张图上
                    if (zhengjia == "4")
                    {
                        sb.Append(",");
                    }
                }
                //************************************压力2
                if (zhengjia == "2" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre2"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre2"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre2"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                            if (chuchengl2 == 1 && mozhuli2 == 0 && min < ZLvalue)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml2[m, 0]) > Convert.ToDecimal(ccmml2[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, rec2.Y, 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre2"].ToString(), "");
                                        chuchengl2 = 0;
                                        mozhuli2 = 1;
                                        break;
                                    }
                                }
                            }
                            rec2 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，右柱(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "右柱(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，右柱(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa2 = min;
                            prevKN2 = kn2;
                            ccmml2[0, 0] = dsq.Tables[0].Rows[0]["maxpre2"].ToString();
                            
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                            rec2 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"右柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "右柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa2 = avg;
                            prevKN2 = kn3;
                            ccmml2[0, 0] = dsq.Tables[0].Rows[0]["avgpre2"].ToString();
                            if (chuchengl2 == 0 && mozhuli2 == 1 && Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre2"].ToString()) > CCMin)
                            {
                                g.DrawRectangle(new Pen(Color.Red, 2), intLeft + i, intYLong - intEnd - (int)(avg * 10), 2, 2);
                                dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), "", dsq.Tables[0].Rows[0]["avgpre2"].ToString());
                                chuchengl2 = 1;
                                mozhuli2 = 0;
                            }
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec2.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.DarkBlue, 1), rec2, dec);
                        rec2 = dec;
                        sb.Append("{\"" + i + "\":\"右柱" + prevMPa2.ToString() + "MPa" + prevKN2.ToString() + "KN\"}");
                        content += "右柱" + prevMPa2.ToString("0.00") + "MPa    " + prevKN2.ToString("0.00") + "KN    ";
                    }
                }
                //**************************************整架
                if (zhengjia == "3")
                {
                    if (dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "" && dsq.Tables[0].Rows[0]["maxpre3"].ToString() != "")
                    {
                        decimal max = Convert.ToDecimal(dsq.Tables[0].Rows[0]["maxpre3"].ToString());
                        decimal min = Convert.ToDecimal(dsq.Tables[0].Rows[0]["minpre3"].ToString());
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString());
                        if (max - min > 6)
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(max * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            if (chuchengl3 == 1 && mozhuli3 == 0 && min < ZLvalue * 2)
                            {
                                for (int m = 0; m < 30; m++)
                                {
                                    if (Convert.ToDecimal(ccmml3[m, 0]) > Convert.ToDecimal(ccmml3[m + 1, 0]))
                                    {
                                        g.DrawRectangle(new Pen(Color.Green, 2), intLeft + i, rec3.Y, 2, 2);
                                        dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), dsq.Tables[0].Rows[0]["maxpre3"].ToString(), "");
                                        chuchengl3 = 0;
                                        mozhuli3 = 1;
                                        break;
                                    }
                                }
                            }
                            rec3 = dec;
                            //定义终点
                            dec = new Point(intLeft + i, intYLong - intEnd - (int)(min * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn = max * (firstD * firstD / 4000) * (decimal)Math.PI;
                            decimal kn2 = min * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"整架(高)" + max.ToString() + "MPa" + kn.ToString() + "KN，整架(低)" + min.ToString() + "MPa" + kn2.ToString() + "KN\"}");
                            content += "整架(高)" + max.ToString("0.00") + "MPa    " + kn.ToString("0.00") + "KN，整架(低)" + min.ToString("0.00") + "MPa    " + kn2.ToString("0.00") + "KN    ";
                            prevMPa3 = min;
                            prevKN3 = kn2;
                            ccmml3[0, 0] = dsq.Tables[0].Rows[0]["maxpre3"].ToString();
                            
                        }
                        else
                        {
                            //定义终点
                            Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 10));
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                            rec3 = dec;
                            decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                            sb.Append("{\"" + i + "\":\"左柱" + max.ToString() + "MPa" + kn3.ToString() + "KN\"}");
                            content += "左柱" + max.ToString("0.00") + "MPa    " + kn3.ToString("0.00") + "KN    ";
                            prevMPa3 = avg;
                            prevKN3 = kn3;
                            ccmml3[0, 0] = dsq.Tables[0].Rows[0]["avgpre3"].ToString();
                            if (chuchengl3 == 0 && mozhuli3 == 1 && Convert.ToDecimal(dsq.Tables[0].Rows[0]["avgpre3"].ToString()) > CCMin)
                            {
                                g.DrawRectangle(new Pen(Color.Red, 2), intLeft + i, intYLong - intEnd - (int)(avg * 10), 2, 2);
                                dt_export.Rows.Add(dta.AddSeconds(second1200 * i).ToString(), "", dsq.Tables[0].Rows[0]["avgpre3"].ToString());
                                chuchengl3 = 1;
                                mozhuli3 = 0;
                            }
                        }
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec3.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 1), rec3, dec);
                        rec3 = dec;
                        sb.Append("{\"" + i + "\":\"整架" + prevMPa3.ToString() + "MPa" + prevKN3.ToString() + "KN\"}");
                        content += "整架" + prevMPa3.ToString("0.00") + "MPa    " + prevKN3.ToString("0.00") + "KN    ";
                    }
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //每日工作阻力分布曲线
        public static Bitmap DrawingImg10(string AreaName, string FaceName, string zhengjia, string yuzhi, string dts, string dte)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;
            StringBuilder sb = new StringBuilder();//冗余变量
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("每日工作阻力分布曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("工作面名称：" + AreaName, new Font("宋体", 14), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString("日期：" + dts.ToString().Substring(0, 10), new Font("宋体", 14), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（支架编号）", new Font("宋体", 14), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("P(MPa)", new Font("宋体", 14), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressuremax"].ToString(), new Font("宋体", 16), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                string ss2 = result.Tables[0].Rows[0]["pressuremin"].ToString();
                decimal a2 = Convert.ToDecimal(ss2);
                int r2 = Convert.ToInt32(a2);
                int intJingJieXian2 = intYLong - intEnd - r2 * 10;
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian2, intXLong - intRight, intJingJieXian2); //绘制横向预警线
                g.DrawString("预警值" + result.Tables[0].Rows[0]["pressuremin"].ToString(), new Font("宋体", 16), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian2 - 10));

                firstD = Convert.ToDecimal(result.Tables[0].Rows[0]["firstd"].ToString());
                secondD = Convert.ToDecimal(result.Tables[0].Rows[0]["sencondd"].ToString());
            }
            #endregion

            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 16), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘制 x刻度和x时间单位

            #region
            string sql = @"select BracketNo,avg(Pressure1) as p1,avg(Pressure2) as p2,avg(Pressure1+Pressure2)/2 as p3 from PressureData p inner join PreSenInfo s on p.areaName = s.AreaName and p.FaceName = s.FaceName and p.SensorNo = s.SensorNo where p.areaName = '"
                + AreaName + "' and p.FaceName = '" + FaceName + "' and s.Type = '液压支架' and time between '" + dts + "' And '" + dte + "' and (pressure1 >= '" + yuzhi + "' or Pressure2 >='" + yuzhi + "') group by BracketNo order by BracketNo";
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = 1200 / intXMax;
            if (zhengjia == "1")
            {
                Point p = new Point(intLeft, intYLong - intEnd);
                Point dot = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    p.X = intLeft + i * intXScale;
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["BracketNo"].ToString(), new Font("宋体", 16), Brushes.Black, p.X - 10, p.Y);
                    if (i > 0)
                    {
                        g.DrawLine(Pens.Blue, dot.X, dot.Y, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10));
                        g.DrawRectangle(Pens.Blue, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10) - 1, 2, 2);
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10);
                        dot.X = p.X;
                    }
                    else
                    {
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10);
                        g.DrawRectangle(Pens.Blue, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10) - 1, 2, 2);
                    }
                    g.DrawString(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()).ToString("0.00"), new Font("宋体", 14), Brushes.Blue, dot.X - 15, dot.Y - 20);
                }
            }
            if (zhengjia == "2")
            {
                Point p = new Point(intLeft, intYLong - intEnd);
                Point dot = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    p.X = intLeft + i * intXScale;
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["BracketNo"].ToString(), new Font("宋体", 16), Brushes.Black, p.X - 10, p.Y);
                    if (i > 0)
                    {
                        g.DrawLine(Pens.Green, dot.X, dot.Y, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10));
                        g.DrawRectangle(Pens.Green, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10) - 1, 2, 2);
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10);
                        dot.X = p.X;
                    }
                    else
                    {
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10);
                        g.DrawRectangle(Pens.Green, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10) - 1, 2, 2);
                    }
                    g.DrawString(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()).ToString("0.00"), new Font("宋体", 14), Brushes.Green, dot.X - 15, dot.Y - 20);
                }
            }
            if (zhengjia == "3")
            {
                Point p = new Point(intLeft, intYLong - intEnd);
                Point dot = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    p.X = intLeft + i * intXScale;
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["BracketNo"].ToString(), new Font("宋体", 16), Brushes.Black, p.X - 10, p.Y);
                    if (i > 0)
                    {
                        g.DrawLine(Pens.Red, dot.X, dot.Y, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10));
                        g.DrawRectangle(Pens.Red, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10) - 1, 2, 2);
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10);
                        dot.X = p.X;
                    }
                    else
                    {
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10);
                        g.DrawRectangle(Pens.Red, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10) - 1, 2, 2);
                    }
                    g.DrawString(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()).ToString("0.00"), new Font("宋体", 14), Brushes.Red, dot.X - 15, dot.Y - 20);
                }
            }
            #endregion
            return img;
        }
        //000
        //每日工作阻力分布曲线
        public static Bitmap DrawingImg10_0(string AreaName, string FaceName, string zhengjia, string yuzhi, string dts, string dte)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;
            StringBuilder sb = new StringBuilder();//冗余变量
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("每日工作阻力分布曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("工作面名称：" + AreaName, new Font("宋体", 14), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString("日期：" + dts.ToString().Substring(0, 10), new Font("宋体", 14), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（支架编号）", new Font("宋体", 14), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("P(MPa)", new Font("宋体", 14), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressuremax"].ToString(), new Font("宋体", 16), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                string ss2 = result.Tables[0].Rows[0]["pressuremin"].ToString();
                decimal a2 = Convert.ToDecimal(ss2);
                int r2 = Convert.ToInt32(a2);
                int intJingJieXian2 = intYLong - intEnd - r2 * 10;
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian2, intXLong - intRight, intJingJieXian2); //绘制横向预警线
                g.DrawString("预警值" + result.Tables[0].Rows[0]["pressuremin"].ToString(), new Font("宋体", 16), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian2 - 10));

                firstD = Convert.ToDecimal(result.Tables[0].Rows[0]["firstd"].ToString());
                secondD = Convert.ToDecimal(result.Tables[0].Rows[0]["sencondd"].ToString());
            }
            #endregion

            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 16), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘制 x刻度和x时间单位

            #region
            string sql = @"select facename,sensorNo,bracketNo,distance,AVG(zlavg) p3 from prereport where reportdate>='" + dts + "' and reportdate<='" + dte + "' group by bracketNo,sensorNo,distance,facename order by bracketno";
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = 1200 / intXMax;
            if (zhengjia == "1")
            {
                Point p = new Point(intLeft, intYLong - intEnd);
                Point dot = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    p.X = intLeft + i * intXScale;
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["BracketNo"].ToString(), new Font("宋体", 16), Brushes.Black, p.X - 10, p.Y);
                    if (i > 0)
                    {
                        g.DrawLine(Pens.Blue, dot.X, dot.Y, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10));
                        g.DrawRectangle(Pens.Blue, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10) - 1, 2, 2);
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10);
                        dot.X = p.X;
                    }
                    else
                    {
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10);
                        g.DrawRectangle(Pens.Blue, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()) * 10) - 1, 2, 2);
                    }
                    g.DrawString(Convert.ToDecimal(ds.Tables[0].Rows[i]["p1"].ToString()).ToString("0.00"), new Font("宋体", 14), Brushes.Blue, dot.X - 15, dot.Y - 20);
                }
            }
            if (zhengjia == "2")
            {
                Point p = new Point(intLeft, intYLong - intEnd);
                Point dot = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    p.X = intLeft + i * intXScale;
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["BracketNo"].ToString(), new Font("宋体", 16), Brushes.Black, p.X - 10, p.Y);
                    if (i > 0)
                    {
                        g.DrawLine(Pens.Green, dot.X, dot.Y, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10));
                        g.DrawRectangle(Pens.Green, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10) - 1, 2, 2);
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10);
                        dot.X = p.X;
                    }
                    else
                    {
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10);
                        g.DrawRectangle(Pens.Green, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()) * 10) - 1, 2, 2);
                    }
                    g.DrawString(Convert.ToDecimal(ds.Tables[0].Rows[i]["p2"].ToString()).ToString("0.00"), new Font("宋体", 14), Brushes.Green, dot.X - 15, dot.Y - 20);
                }
            }
            if (zhengjia == "3")
            {
                Point p = new Point(intLeft, intYLong - intEnd);
                Point dot = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    p.X = intLeft + i * intXScale;
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["BracketNo"].ToString(), new Font("宋体", 16), Brushes.Black, p.X - 10, p.Y);
                    if (i > 0)
                    {
                        g.DrawLine(Pens.Black, dot.X, dot.Y, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10));
                        g.DrawRectangle(Pens.Black, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10) - 1, 2, 2);
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10);
                        dot.X = p.X;
                    }
                    else
                    {
                        dot.Y = p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10);
                        g.DrawRectangle(Pens.Black, p.X, p.Y - Convert.ToInt32(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()) * 10) - 1, 2, 2);
                    }
                    g.DrawString(Convert.ToDecimal(ds.Tables[0].Rows[i]["p3"].ToString()).ToString("0.00"), new Font("宋体", 14), Brushes.Black, dot.X - 15, dot.Y - 20);
                }
            }
            #endregion
            return img;
        }
        //顶板离层历史数据分析曲线
        public static Bitmap DrawingImg20(string AreaName, string roadwayName, string zhengjia, string Location, string dts, string dte, DataTable dt_img, DataTable dt_export)
        {
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            string sqltime = "SELECT min(time) as startT,max(time) as endT FROM DisplacementData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from DisSenInfo where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());
                }
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 25;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高
            decimal firstD = 0;
            decimal secondD = 0;
            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString("顶板离层历史数据分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString("安装位置：" + Location, new Font("宋体", 12), Brushes.Black, new Point(intLeft + 100, intTop - 30));//安装位置 a值曲线 b值曲线
            g.DrawString("A值曲线", new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 400, intTop - 30));//安装位置 a值曲线 b值曲线
            g.DrawString("B值曲线", new Font("宋体", 12), Brushes.Green, new Point(intLeft + 560, intTop - 30));//安装位置 a值曲线 b值曲线
            //g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("S(mm)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制警戒线，报警值
            #region
            string sqlbjz = "select * from DisplacementPar where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["displacementAlarm"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 2;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["displacementAlarm"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            Point rec2 = new Point(intLeft, intYLong - intEnd);
            Point rec3 = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            decimal prevKN = 0;
            decimal prevMPa2 = 0;
            decimal prevKN2 = 0;
            decimal prevMPa3 = 0;
            decimal prevKN3 = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                string content = "";
                string sqlq = @"Select avg(Displacement1) as Displacement1,avg(Displacement2) as Displacement2 
FROM DisplacementData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from DisSenInfo where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (zhengjia == "1" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["Displacement1"].ToString() != "")
                    {
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["Displacement1"].ToString());
                        //定义终点
                        Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 2));
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                        rec = dec;
                        content += "浅基点" + avg.ToString("0.00") + "mm    ";
                        prevMPa = avg;
                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                        rec = dec;
                        content += "浅基点" + prevMPa.ToString("0.00") + "mm    ";
                    }
                    //左柱右柱显示在一张图上
                    if (zhengjia == "4")
                    {
                        sb.Append(",");
                    }
                }
                //************************************压力2
                if (zhengjia == "2" || zhengjia == "4")
                {
                    if (dsq.Tables[0].Rows[0]["Displacement2"].ToString() != "")
                    {
                        decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["Displacement2"].ToString());
                        //定义终点
                        Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * 2));
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                        rec2 = dec;
                        decimal kn3 = avg * (firstD * firstD / 4000) * (decimal)Math.PI;
                        content += "深基点" + avg.ToString("0.00") + "mm    ";
                        prevMPa2 = avg;

                    }
                    else
                    {
                        //数据库无数据，取上一条记录
                        //定义终点
                        Point dec = new Point(intLeft + i, rec2.Y);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 1), rec2, dec);
                        rec2 = dec;
                        content += "深基点" + prevMPa2.ToString("0.00") + "mm    ";
                    }
                }

                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        /// <summary>
        /// 柱状图  综采工作阻力实时在线监测
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="FaceName"></param>
        /// <param name="BracketNo"></param>
        /// <param name="dts"></param>
        /// <param name="dte"></param>
        /// <returns></returns>
        //综采工作阻力实时在线监测
        public static Bitmap DrawingImg2(string AreaName, string FaceName, string page)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 90; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 800;   //图片大小 高
            string biaoti = "综采工作阻力实时在线监测";//标题
            string danweiX = "支架号";//X轴单位
            string danweiY = "P(MPa)";//Y轴单位


            //支架号及对应值
            string sql = @"with t as (
select ROW_NUMBER() OVER (ORDER BY psi.BracketNo ASC) AS XUHAO,psi.bracketno,isnull(pnd.pressure1,0) pressure1,isnull(pnd.pressure2,0) pressure2,txstate,usestate,pnd.alarmh,pnd.alarml from PreSenInfo psi 
left join PreNewData pnd on psi.areaname=pnd.areaname and psi.facename=pnd.facename and psi.sensorno=pnd.sensorno
where psi.areaname='" + AreaName + "' and psi.facename='" + FaceName + "') select * from t where xuhao "+page;
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            //g.SmoothingMode = SmoothingMode.HighQuality;
            //g.CompositingQuality = CompositingQuality.HighQuality;
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题

            string sqlbjz = "select pressuremax,pressuremin from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            if (result.Tables[0].Rows.Count > 0)
            {
                decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremax"].ToString());
                decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremin"].ToString());

                string ss = result.Tables[0].Rows[0]["pressureMax"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("压力上限" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 20));

                string ss2 = result.Tables[0].Rows[0]["pressuremin"].ToString();
                decimal a2 = Convert.ToDecimal(ss2);
                int r2 = Convert.ToInt32(a2);
                intJingJieXian = intYLong - intEnd - r2 * 10;
                g.DrawLine(new Pen(Color.Green, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("压力下限" + r2, new Font("宋体", 12), Brushes.Green, new Point(intXLong - intRight, intJingJieXian - 20));
            }

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 50)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            g.DrawString("压力状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 30);//工作状态
            g.DrawString("工作状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 50);//工作状态
            int xoffset = intXScale / 10;
            if (ds.Tables[0].Rows.Count > 0) {
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    p.X = intLeft + i * intXScale + xoffset*5;
                    g.DrawString(ds.Tables[0].Rows[i]["bracketno"].ToString(), new Font("宋体", 12), Brushes.Black, p);//支架编号

                    string usestate = ds.Tables[0].Rows[i]["usestate"].ToString();
                    string tongxunstate = ds.Tables[0].Rows[i]["txstate"].ToString();//通讯状态
                    string chuangganqistate = ds.Tables[0].Rows[i]["alarmh"].ToString();//超压
                    string changanqistate2 = ds.Tables[0].Rows[i]["alarml"].ToString();//欠压
                    if (usestate == "使用")
                    {
                        g.DrawString("使用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 50);//工作状态
                        if (tongxunstate == "正常")
                        {
                            if (changanqistate2.ToLower() == "true" || changanqistate2 == "1")
                            {
                                g.DrawString("欠压", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                            }
                            else if (chuangganqistate.ToLower() == "true" || chuangganqistate == "1" || chuangganqistate == "超压" || chuangganqistate == "报警")
                            {
                                g.DrawString("超压", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                            }
                            
                            else
                            {
                                g.DrawString("正常", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                            }
                            //g.DrawString(ds.Tables[0].Rows[i]["alarmh"].ToString()+" "+changanqistate2.ToString().ToLower(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        }
                        else
                        { //故障
                            g.DrawString("故障", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        }
                    }
                    else
                    { //停用
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 50);//工作状态
                    }

                    if (usestate=="使用")
                    {
                        
                        p.X = intLeft + i * intXScale + xoffset;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 40);//工作状态
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure1"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 10;
                        if (pressuse1 > decimalyalishangxian)
                        {
                            g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 4, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X - 7, p.Y - pressure_1 - 20);//压力1数值
                            //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString()=="使用"?"超压":"故障", new Font("宋体", 12), Brushes.Red, p.X, p.Y + 15);//压力状态
                        }
                        else if (pressuse1 > decimalyalixiaxian)
                        {
                            g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 4, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X - 7, p.Y - pressure_1 - 20);//压力1数值
                            //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString() == "使用" ? "正常" : "故障", new Font("宋体", 12), Brushes.Green, p.X, p.Y + 15);//压力状态
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_1, xoffset * 4, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X - 7, p.Y - pressure_1 - 20);//压力1数值
                            //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString() == "使用" ? "欠压" : "故障", new Font("宋体", 12), Brushes.Orange, p.X, p.Y + 15);//压力状态
                        }

                        decimal pressuse2 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure2"].ToString());
                        int pressure_2 = Convert.ToInt32(pressuse2) * 10;
                        p.X = intLeft + i * intXScale + xoffset * 6;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 40);//工作状态
                        if (pressuse2 > decimalyalishangxian)
                        {
                            g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_2, xoffset * 4, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X - 3, p.Y - pressure_2 - 20);//压力2数值
                            //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString() == "使用" ? "超压" : "故障", new Font("宋体", 12), Brushes.Red, p.X, p.Y + 15);//压力状态
                        }
                        else if (pressuse2 > decimalyalixiaxian)
                        {
                            g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_2, xoffset * 4, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X - 3, p.Y - pressure_2 - 20);//压力2数值
                            //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString() == "使用" ? "正常" : "故障", new Font("宋体", 12), Brushes.Green, p.X, p.Y + 15);//压力状态
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_2, xoffset * 4, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X - 3, p.Y - pressure_2 - 20);//压力2数值
                            //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString() == "使用" ? "欠压" : "故障", new Font("宋体", 12), Brushes.Orange, p.X, p.Y + 15);//压力状态
                        }

                    }
                    else
                    {
                       
                        p.X = intLeft + i * intXScale + xoffset;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Red, p.X, p.Y + 40);//工作状态
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure1"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 10;
                        g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_1, xoffset * 4, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Gray, p.X - 7, p.Y - pressure_1 - 20);//压力1数值
                        decimal pressuse2 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure2"].ToString());
                        int pressure_2 = Convert.ToInt32(pressuse2) * 10;
                        p.X = intLeft + i * intXScale + xoffset * 6;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Red, p.X, p.Y + 40);//工作状态
                        g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_2, xoffset * 4, pressure_2);//压力2柱状图
                        g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Gray, p.X - 3, p.Y - pressure_2 - 20);//压力2数值
                        //g.DrawString("故障", new Font("宋体", 12), Brushes.Gray, p.X, p.Y + 15);//压力状态
                    }


                }
            }
            

            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e

            Point p3 = new Point(intLeft, intYLong - intEnd + 70);
            g.FillRectangle(Brushes.Red, p3.X - 80, p3.Y, 20, 20);//压力2柱状图
            g.DrawString("超压", new Font("宋体", 14), Brushes.Black, p3.X - 60, p3.Y);//压力2数值
            g.FillRectangle(Brushes.Green, p3.X - 20 + 10, p3.Y, 20, 20);//压力2柱状图
            g.DrawString("正常", new Font("宋体", 14), Brushes.Black, p3.X + 10, p3.Y);//压力2数值
            g.FillRectangle(Brushes.Orange, p3.X + 40 + 20, p3.Y, 20, 20);//压力2柱状图
            g.DrawString("欠压", new Font("宋体", 14), Brushes.Black, p3.X + 60 + 20, p3.Y);//压力2数值
            g.FillRectangle(Brushes.Gray, p3.X + 100 + 30, p3.Y, 20, 20);//压力2柱状图
            g.DrawString("故障", new Font("宋体", 14), Brushes.Black, p3.X + 120 + 30, p3.Y);//压力2数值

            return img;
        }
        //顶板离层监测
        //顶板离层在线监测
        public static Bitmap DrawingImg13(string AreaName, string FaceName,string page)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 60;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 5;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 160; //右边距
            int intTop = 100;    //上边距
            int intEnd = 130;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 120;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 830;   //图片大小 高
            string biaoti = "顶板离层实时在线监测";//标题
            string danweiX = "安装位置（m）";//X轴单位
            string danweiY = "S(mm)";//Y轴单位


            //安装位置
            string sql = @"with t as (
select ROW_NUMBER() OVER (ORDER BY dsi.Location ASC) AS XUHAO, dsi.areaname,dsi.roadwayname,dsi.sensorno,dsi.location,dsi.pointdeptha,dsi.pointdepthb,dsi.usestate,dnd.stationno,dnd.displacement1,dnd.displacement2,dnd.alarmh,dnd.alarmv,dnd.txstate
 from DisNewData dnd
left join DisSenInfo dsi on dsi.sensorNo=dnd.SensorNo and dsi.AreaName=dnd.AreaName and dsi.roadwayName=dnd.roadwayName
where dsi.areaname='" + AreaName + "' and dsi.roadwayname='" + FaceName + "' ) select * from t where xuhao "+page;
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题

            string sqlbjz = "select disyujingvale,displacementalarm from DisplacementPar where areaname='" + AreaName + "' and roadwayname='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            if (result.Tables[0].Rows.Count > 0)
            {
                decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["displacementalarm"].ToString());
                decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["disyujingvale"].ToString());

                string ss = result.Tables[0].Rows[0]["displacementalarm"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * 2;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));

                string ss2 = result.Tables[0].Rows[0]["disyujingvale"].ToString();
                decimal a2 = Convert.ToDecimal(ss2);
                int r2 = Convert.ToInt32(a2);
                intJingJieXian = intYLong - intEnd - r2 * 2;
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("预警" + r2, new Font("宋体", 12), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian - 10));
            }

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 120)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd-20));//X轴 单位 安装位置
            Point p = new Point(intLeft, intYLong - intEnd);
            g.DrawString("位移状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 60);//位移状态
            g.DrawString("工作状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 80);//工作状态
            int xoffset = intXScale / 10;
            if (ds.Tables[0].Rows.Count > 0)
            {
                //g.RotateTransform(30);
                //for (int i = 0; i < intXMax; i++)
                //{
                //    g.DrawString(ds.Tables[0].Rows[i]["location"].ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
                //}
                //g.RotateTransform(-30);
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    p.X = intLeft + i * intXScale + xoffset * 3;
                    g.RotateTransform(30);
                    Point p00 = new Point(0, 0);
                    Point pb = new Point(intLeft + i * intXScale+ xoffset*2, intYLong-intEnd);
                    Point pa = PointRotate(p00, pb, 30);
                    //g.DrawString("123456789", new Font("宋体", 16), Brushes.Black, pa.X, pa.Y);//绘制x坐标
                    g.DrawString(ds.Tables[0].Rows[i]["location"].ToString(), new Font("宋体", 14), Brushes.Black, pa.X, pa.Y);//安装位置
                    g.RotateTransform(-30);
                    //g.DrawString("000", new Font("宋体", 14), Brushes.Black, p.X, p.Y);//安装位置
                    string usestate = ds.Tables[0].Rows[i]["usestate"].ToString();
                    string tongxunstate = ds.Tables[0].Rows[i]["txstate"].ToString();//通讯状态
                    string chuangganqistate = ds.Tables[0].Rows[i]["alarmh"].ToString();
                    if (usestate == "使用")
                    {
                        g.DrawString("使用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 80);//工作状态
                        if (tongxunstate == "正常")
                        {
                            if (chuangganqistate.ToString().ToLower() == "true"||chuangganqistate == "1" || chuangganqistate == "超压" || chuangganqistate == "报警")
                            {
                                g.DrawString("报警", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//位移状态
                            }
                            else
                            {
                                g.DrawString("正常", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//位移状态
                            }

                        }
                        else
                        { //故障
                            g.DrawString("故障", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//位移状态
                        }
                    }
                    else
                    { //停用
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//位移状态
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 80);//工作状态
                    }

                    if (usestate == "使用")
                    {
                        
                        p.X = intLeft + i * intXScale + xoffset;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 50);//工作状态
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["displacement1"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                        if (pressuse1 > decimalyalishangxian)
                        {
                            g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X, p.Y - pressure_1 - 20);//压力1数值
                        }
                        else if (pressuse1 > decimalyalixiaxian)
                        {
                            g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_1 - 20);//压力1数值
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_1 - 20);//压力1数值
                        }
                        g.DrawString("A", new Font("宋体", 16), Brushes.Gray, p.X , p.Y - pressure_1 - 40);//
                        decimal pressuse2 = Convert.ToDecimal(ds.Tables[0].Rows[i]["displacement2"].ToString());
                        int pressure_2 = Convert.ToInt32(pressuse2) * 2;
                        p.X = intLeft + i * intXScale + xoffset * 6;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 50);//工作状态
                        if (pressuse2 > decimalyalishangxian)
                        {
                            g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_2 - 20);//压力2数值
                        }
                        else if (pressuse2 > decimalyalixiaxian)
                        {
                            g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_2 - 20);//压力2数值
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_2 - 20);//压力2数值
                        }
                        g.DrawString("B", new Font("宋体", 16), Brushes.Gray, p.X , p.Y - pressure_2 - 40);//

                    }
                    else
                    {
                        p.X = intLeft + i * intXScale + xoffset;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Gray, p.X, p.Y + 30);//使用状态
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Gray, p.X, p.Y + 50);//工作状态
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["displacement1"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                        g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_1 - 20);//压力1数值
                        g.DrawString("A", new Font("宋体", 16), Brushes.Gray, p.X , p.Y - pressure_1 - 40);//
                        decimal pressuse2 = Convert.ToDecimal(ds.Tables[0].Rows[i]["displacement2"].ToString());
                        int pressure_2 = Convert.ToInt32(pressuse2) * 2;
                        p.X = intLeft + i * intXScale + xoffset * 6;
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Gray, p.X, p.Y + 30);//使用状态
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Gray, p.X, p.Y + 50);//工作状态
                        g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                        g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X , p.Y - pressure_2 - 20);//压力2数值
                        g.DrawString("B", new Font("宋体", 16), Brushes.Gray, p.X , p.Y - pressure_2 - 40);//
                    }
                }
            }


            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e

            Point p3 = new Point(intLeft, intYLong - intEnd + 70);
            g.FillRectangle(Brushes.Red, p3.X - 80, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("超压", new Font("宋体", 14), Brushes.Black, p3.X - 60, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Green, p3.X - 20 + 10, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("正常", new Font("宋体", 14), Brushes.Black, p3.X + 10, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Orange, p3.X + 40 + 20, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("欠压", new Font("宋体", 14), Brushes.Black, p3.X + 60 + 20, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Gray, p3.X + 100 + 30, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("故障", new Font("宋体", 14), Brushes.Black, p3.X + 120 + 30, p3.Y + 40);//压力2数值

            return img;
        }
        //锚杆/索在线监测
        public static Bitmap DrawingImg21(string AreaName, string FaceName, string mgType,string page)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 100;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 6;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 160; //右边距
            int intTop = 100;    //上边距
            int intEnd = 130;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 100;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 830;   //图片大小 高
            string biaoti = mgType + "实时在线监测";//标题  锚杆、锚索
            string danweiX = "安装位置（m）";//X轴单位
            string danweiY = "F(KN)";//Y轴单位


            //安装位置
            string sql = @"with t as (
select ROW_NUMBER() OVER (ORDER BY bsi.Location ASC) AS XUHAO, bnd.areaname,bnd.roadwayname,bnd.stationno,bnd.sensorno,bnd.type,bnd.stress,bnd.alarmh,bnd.alarmv,bnd.txstate,bsi.mgtype,bsi.location,bsi.starvalue,bsi.usestate 
FROM BoltNewData bnd
left join BoltSenInfo bsi on bnd.areaname=bsi.areaname and bnd.roadwayname=bsi.roadwayname and bnd.sensorno=bsi.sensorno
where bnd.areaname='" + AreaName + "' and bnd.roadwayname='" + FaceName + "' and bnd.type='" + mgType + "' ) select * from t where xuhao "+page;
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题

            string sqlbjz = "select * from  BoltPar where areaname='" + AreaName + "' and roadwayname='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            if (result.Tables[0].Rows.Count > 0)
            {
                decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["AlarmMaxMSF"].ToString());
                decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString());
                if (mgType == "锚索")
                {
                    string ss = result.Tables[0].Rows[0]["AlarmMaxMSF"].ToString();
                    decimal a = Convert.ToDecimal(ss);
                    int r = Convert.ToInt32(a);
                    int intJingJieXian = intYLong - intEnd - r;
                    g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                    g.DrawString("报警" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                }
                else
                {
                    string ss2 = result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString();
                    decimal a2 = Convert.ToDecimal(ss2);
                    int r2 = Convert.ToInt32(a2);
                    int intJingJieXian = intYLong - intEnd - r2;
                    g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                    g.DrawString("预警" + r2, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
                }



            }

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 100)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            g.DrawString("压力状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 60);//工作状态
            g.DrawString("工作状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 80);//工作状态
            int xoffset = intXScale / 10;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    p.X = intLeft + i * intXScale + xoffset;
                    g.RotateTransform(30);
                    Point p00 = new Point(0, 0);
                    Point pb = new Point(intLeft + i * intXScale + xoffset * 2, intYLong - intEnd);
                    Point pa = PointRotate(p00, pb, 30);
                    //g.DrawString("123456789", new Font("宋体", 16), Brushes.Black, pa.X, pa.Y);//绘制x坐标
                    g.DrawString(ds.Tables[0].Rows[i]["location"].ToString(), new Font("宋体", 14), Brushes.Black, pa.X, pa.Y);//安装位置
                    g.RotateTransform(-30);

                    //g.DrawString(ds.Tables[0].Rows[i]["location"].ToString(), new Font("宋体", 14), Brushes.Black, p.X, p.Y);//安装位置

                    string usestate = ds.Tables[0].Rows[i]["usestate"].ToString();
                    string tongxunstate = ds.Tables[0].Rows[i]["txstate"].ToString();//通讯状态
                    string chuangganqistate = ds.Tables[0].Rows[i]["alarmh"].ToString();
                    if (usestate == "使用")
                    {
                        g.DrawString("使用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 80);//工作状态
                        if (tongxunstate == "正常")
                        {
                            if (chuangganqistate.ToString().ToLower() == "true" || chuangganqistate == "1" || chuangganqistate == "超压" || chuangganqistate == "报警")
                            {
                                g.DrawString("报警", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                            }
                            else {
                                g.DrawString("正常", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                            }
                            
                        }
                        else { //故障
                            g.DrawString("故障", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                        }
                    }
                    else { //停用
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 80);//工作状态
                    }

                    if (usestate == "使用")//
                    {
                        //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 50);//工作状态
                        p.X = intLeft + i * intXScale + xoffset;
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["stress"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                        if (pressuse1 > decimalyalishangxian)
                        {
                            g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        }
                        else if (pressuse1 > decimalyalixiaxian)
                        {
                            g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        }
                        else
                        {
                            g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        }


                    }
                    else
                    {
                        //g.DrawString(ds.Tables[0].Rows[i]["usestate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//使用状态
                        //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Red, p.X, p.Y + 50);//工作状态
                        p.X = intLeft + i * intXScale + xoffset;
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["stress"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                        g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值

                    }
                }
            }


            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e

            Point p3 = new Point(intLeft, intYLong - intEnd + 70);
            g.FillRectangle(Brushes.Red, p3.X - 80, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("超压", new Font("宋体", 14), Brushes.Black, p3.X - 60, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Green, p3.X - 20 + 10, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("正常", new Font("宋体", 14), Brushes.Black, p3.X + 10, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Orange, p3.X + 40 + 20, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("欠压", new Font("宋体", 14), Brushes.Black, p3.X + 60 + 20, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Gray, p3.X + 100 + 30, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("故障", new Font("宋体", 14), Brushes.Black, p3.X + 120 + 30, p3.Y + 40);//压力2数值

            return img;
        }
        //锚杆/索数据分析
        public static Bitmap DrawingImg22(string AreaName, string roadwayName, string mgType, string Location, string dts, string dte, DataTable dt_img)
        {
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            decimal BoltMax = 0;//根据最大值画纵坐标
            string sqltime = "SELECT min(time) as startT,max(time) as endT,max(stress) as stress FROM BoltData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo =(select SensorNo FROM BoltSenInfo where  areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            //string sqltime = "SELECT min(time) as startT,max(time) as endT FROM DisplacementData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from DisSenInfo where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());

                }
                if (dstime.Tables[0].Rows[0]["stress"].ToString() != "")
                {
                    decimal stress = Convert.ToDecimal(dstime.Tables[0].Rows[0]["stress"].ToString());
                    if (stress > BoltMax)
                    {
                        BoltMax = stress;
                    }
                }
            }
            string sqlbjz = "select * from BoltPar where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString();
                decimal a = Convert.ToDecimal(ss);
                if (BoltMax < a) BoltMax = a;
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            int intYMultiple = 25;    //零刻度的值 Y intYScale = 50;
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            if (BoltMax <= 0) BoltMax = 600;
            decimal diffy = 1;//600高度比实际值，用实际值*此比例即所占像素数量
            decimal diff = BoltMax % intYMax;
            intYMultiple = (int)Math.Ceiling((BoltMax + diff) / intYMax);
            diffy = 600 / (BoltMax + diff);

            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高 600像素高固定
            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(Location + mgType + "数据分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            //g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            //g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("F(KN)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制初始值，报警值
            #region
            sqlbjz = "select * from BoltPar where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "'";
            result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - (int)(a * diffy);
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
            }
            string sqlcsz = "select StarValue from BoltSenInfo where AreaName = '" + AreaName + "' and roadwayName = '" + roadwayName + "' and  Location = '" + Location + "'";
            result = ExecuteSqlDataSet(sqlcsz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["StarValue"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - r * (int)diffy;
                g.DrawLine(new Pen(Color.Green, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘初始值
                g.DrawString("初始值" + result.Tables[0].Rows[0]["StarValue"].ToString(), new Font("宋体", 12), Brushes.Green, new Point(intXLong - intRight, intJingJieXian - 10));
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                string content = "";
                string sqlq = @"Select avg(Stress) as Stress FROM BoltData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from BoltSenInfo where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "' and mgType='" + mgType + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (dsq.Tables[0].Rows[0]["Stress"].ToString() != "")
                {
                    decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["Stress"].ToString());
                    //定义终点
                    Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * diffy));
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                    rec = dec;
                    content += "应力值" + avg.ToString("0.00") + "KN    ";
                    prevMPa = avg;
                }
                else
                {
                    //数据库无数据，取上一条记录
                    //定义终点
                    Point dec = new Point(intLeft + i, rec.Y);
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                    rec = dec;
                    content += "应力值" + prevMPa.ToString("0.00") + "KN    ";
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //围岩应力在线监测
        public static Bitmap DrawingImg23(string AreaName, string FaceName,string page)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 100;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 6;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 160; //右边距
            int intTop = 100;    //上边距
            int intEnd = 150;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 100;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 850;   //图片大小 高
            string biaoti = "围岩应力实时在线监测";//标题  锚杆、锚索
            string danweiX = "安装位置";//X轴单位
            string danweiY = "P(Ma)";//Y轴单位


            //安装位置
            string sql = @"with t as (
select ROW_NUMBER() OVER (ORDER BY dsi.sensorno ASC) AS XUHAO, dsi.areaname,dsi.roadwayname,dsi.sensorno,dsi.location,dsi.depth,dsi.usestate,dnd.stationno,isnull(dnd.stress,0) stress,dnd.alarmh,dnd.alarmv,dnd.txstate from DrillSenInfo1 dsi
left join DrillNewData dnd on dsi.areaname=dnd.areaname and dsi.roadwayname=dnd.roadwayname and dsi.sensorno=dnd.sensorno
where dsi.areaname='" + AreaName + "' and dsi.roadwayname='" + FaceName + "' )select * from t where xuhao "+page;
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题

            //string sqlbjz = "select * from  BoltPar where areaname='" + AreaName + "' and roadwayname='" + FaceName + "'";
            //DataSet result = DB.ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            //if (result.Tables[0].Rows.Count > 0)
            //{
            //    decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["AlarmMaxMSF"].ToString());
            //    decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString());
            //    if (mgType == "锚索")
            //    {
            //        string ss = result.Tables[0].Rows[0]["AlarmMaxMSF"].ToString();
            //        decimal a = Convert.ToDecimal(ss);
            //        int r = Convert.ToInt32(a);
            //        int intJingJieXian = intYLong - intEnd - r;
            //        g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
            //        g.DrawString("报警" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
            //    }
            //    else
            //    {
            //        string ss2 = result.Tables[0].Rows[0]["AlarmMaxMGF"].ToString();
            //        decimal a2 = Convert.ToDecimal(ss2);
            //        int r2 = Convert.ToInt32(a2);
            //        int intJingJieXian = intYLong - intEnd - r2;
            //        g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
            //        g.DrawString("预警" + r2, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
            //    }
            //}

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 100)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            g.DrawString("压力状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 60);//工作状态
            g.DrawString("工作状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 80);//工作状态
            g.DrawString("深度(m)：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 100);//工作状态
            int xoffset = intXScale / 10;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    p.X = intLeft + i * intXScale + xoffset * 4;
                    g.RotateTransform(30);
                    Point p00 = new Point(0, 0);
                    Point pb = new Point(intLeft + i * intXScale + xoffset * 2, intYLong - intEnd);
                    Point pa = PointRotate(p00, pb, 30);
                    //g.DrawString("123456789", new Font("宋体", 16), Brushes.Black, pa.X, pa.Y);//绘制x坐标
                    g.DrawString(ds.Tables[0].Rows[i]["location"].ToString(), new Font("宋体", 14), Brushes.Black, pa.X, pa.Y);//安装位置
                    g.RotateTransform(-30);
                    //g.DrawString(ds.Tables[0].Rows[i]["location"].ToString(), new Font("宋体", 14), Brushes.Black, p.X, p.Y);//安装位置

                    string usestate = ds.Tables[0].Rows[i]["usestate"].ToString();
                    string tongxunstate = ds.Tables[0].Rows[i]["txstate"].ToString();//通讯状态
                    string chuangganqistate = ds.Tables[0].Rows[i]["alarmh"].ToString();
                    if (usestate == "使用")
                    {
                        g.DrawString("使用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 80);//工作状态
                        if (tongxunstate == "正常")
                        {
                            if (chuangganqistate.ToString().ToLower() == "true" || chuangganqistate == "1" || chuangganqistate == "超压" || chuangganqistate == "报警")
                            {
                                g.DrawString("报警", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                            }
                            else
                            {
                                g.DrawString("正常", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                            }
                        }
                        else
                        { //故障
                            g.DrawString("故障", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                        }
                    }
                    else
                    { //停用
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 60);//使用状态
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 80);//工作状态
                    }
                    g.DrawString(ds.Tables[0].Rows[i]["depth"].ToString(), new Font("宋体", 14), Brushes.Black, p.X, p.Y + 100);//深度
                    if (usestate=="使用")
                    {

                        //g.DrawString("使用", new Font("宋体", 12), Brushes.Black, p.X, p.Y + 30);//工作状态
                        //g.DrawString(ds.Tables[0].Rows[i]["depth"].ToString(), new Font("宋体", 14), Brushes.Black, p.X, p.Y + 50);//深度
                        p.X = intLeft + i * intXScale + xoffset;
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["stress"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                        if (chuangganqistate == "1" || chuangganqistate=="正常")
                        {
                            g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        }
                        else {
                            g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        }
                        //if (pressuse1 > decimalyalishangxian)
                        //{
                        //    g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                        //    g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 8), Brushes.Orange, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        //}
                        //else if (pressuse1 > decimalyalixiaxian)
                        //{
                        //    g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                        //    g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 8), Brushes.Red, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        //}
                        //else
                        //{
                        //    g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                        //    g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 8), Brushes.Green, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值
                        //}


                    }
                    else
                    {
                        //g.DrawString("故障", new Font("宋体", 12), Brushes.Red, p.X, p.Y + 30);//工作状态
                        //g.DrawString(ds.Tables[0].Rows[i]["depth"].ToString(), new Font("宋体", 14), Brushes.Black, p.X, p.Y + 50);//深度
                        p.X = intLeft + i * intXScale + xoffset;
                        decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["stress"].ToString());
                        int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                        g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_1, xoffset * 8, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset * 3, p.Y - pressure_1 - 20);//压力1数值

                    }
                }
            }


            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e

            Point p3 = new Point(intLeft, intYLong - intEnd + 90);
            g.FillRectangle(Brushes.Red, p3.X - 80, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("超压", new Font("宋体", 14), Brushes.Black, p3.X - 60, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Green, p3.X - 20 + 10, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("正常", new Font("宋体", 14), Brushes.Black, p3.X + 10, p3.Y + 40);//压力2数值
            g.FillRectangle(Brushes.Gray, p3.X + 40 + 20, p3.Y + 40, 20, 20);//压力2柱状图
            g.DrawString("故障", new Font("宋体", 14), Brushes.Black, p3.X + 60 + 20, p3.Y + 40);//压力2数值
            //g.FillRectangle(Brushes.Gray, p3.X + 100 + 30, p3.Y + 10, 20, 20);//压力2柱状图
            //g.DrawString("故障", new Font("宋体", 14), Brushes.Black, p3.X + 120 + 30, p3.Y + 10);//压力2数值

            return img;
        }
        //围岩应力数据分析
        public static Bitmap DrawingImg24(string AreaName, string roadwayName, string Location, string dts, string dte, DataTable dt_img)
        {
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            decimal BoltMax = 0;//根据最大值画纵坐标
            string sqltime = "SELECT min(time) as startT,max(time) as endT,max(stress) as stress FROM DrillData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo =(select SensorNo FROM DrillSenInfo1 where  areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            //string sqltime = "SELECT min(time) as startT,max(time) as endT FROM DisplacementData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from DisSenInfo where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());

                }
                if (dstime.Tables[0].Rows[0]["stress"].ToString() != "")
                {
                    decimal stress = Convert.ToDecimal(dstime.Tables[0].Rows[0]["stress"].ToString());
                    if (stress > BoltMax)
                    {
                        BoltMax = stress;
                    }
                }
            }
            string sqlbjz = "select * from DrillPar where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressalarm"].ToString();
                decimal a = Convert.ToDecimal(ss);
                if (BoltMax < a) BoltMax = a;
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            int intYMultiple = 25;    //零刻度的值 Y intYScale = 50;
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            if (BoltMax <= 0) BoltMax = 600;
            decimal diffy = 1;//600高度比实际值，用实际值*此比例即所占像素数量
            decimal diff = BoltMax % intYMax;
            intYMultiple = (int)Math.Ceiling((BoltMax + diff) / intYMax);
            diffy = 600 / (BoltMax + diff);

            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高 600像素高固定
            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(Location + "围岩应力历史数据分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            //g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            //g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("F(MPa)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制初始值，报警值
            #region
            sqlbjz = "select * from DrillPar where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "'";
            result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["pressalarm"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - (int)(a * diffy);
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["pressalarm"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));
            }
            //string sqlcsz = "select StarValue from BoltSenInfo where AreaName = '" + AreaName + "' and roadwayName = '" + roadwayName + "' and  Location = '" + Location + "'";
            //result = DB.ExecuteSqlDataSet(sqlcsz, null);
            //if (result.Tables[0].Rows.Count > 0)
            //{
            //    string ss = result.Tables[0].Rows[0]["StarValue"].ToString();
            //    decimal a = Convert.ToDecimal(ss);
            //    int r = Convert.ToInt32(a);
            //    int intJingJieXian = intYLong - intEnd - r * (int)diffy;
            //    g.DrawLine(new Pen(Color.Green, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘初始值
            //    g.DrawString("初始值" + result.Tables[0].Rows[0]["StarValue"].ToString(), new Font("宋体", 12), Brushes.Green, new Point(intXLong - intRight, intJingJieXian - 10));
            //}
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                string content = "";
                string sqlq = @"Select avg(Stress) as Stress FROM DrillData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from DrillSenInfo1 where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (dsq.Tables[0].Rows[0]["Stress"].ToString() != "")
                {
                    decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["Stress"].ToString());
                    //定义终点
                    Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * diffy));
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                    rec = dec;
                    content += "应力值" + avg.ToString("0.00") + "MPa    ";
                    prevMPa = avg;
                }
                else
                {
                    //数据库无数据，取上一条记录
                    //定义终点
                    Point dec = new Point(intLeft + i, rec.Y);
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                    rec = dec;
                    content += "应力值" + prevMPa.ToString("0.00") + "MPa    ";
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //活柱缩量在线监测
        public static Bitmap DrawingImg25(string AreaName, string FaceName,string page)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 25;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 90; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 800;   //图片大小 高
            string biaoti = "活柱缩量实时在线监测";//标题
            string danweiX = "支架号";//X轴单位
            string danweiY = "S(mm)";//Y轴单位


            //支架号及对应值
            string sql = @"with t as (
select ROW_NUMBER() OVER (ORDER BY hzsi.BracketNo ASC) AS XUHAO,hzsi.areaname,hzsi.facename,hzsi.sensorno,hzsi.bracketno,hzsi.distance,hzsi.usestate,hznd.stationno,isnull(hznd.huozhu1,0) pressure1,isnull(hznd.huozhu2,0) pressure2,hznd.alarmstate,hznd.alarmv,hznd.txstate from HuoZhuSenInfo hzsi
left join HuoZhuNewData hznd on hznd.areaname=hzsi.areaname and hznd.facename=hzsi.facename and hznd.sensorno=hzsi.sensorno where hzsi.areaname='" + AreaName + "' and hzsi.facename='" + FaceName + "' ) select * from t where xuhao "+page;
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题

            string sqlbjz = "select AlarmValue as pressuremax,YujingValue as pressuremin from dbo.HuoZhuPar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            if (result.Tables[0].Rows.Count > 0)
            {
                decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremax"].ToString());
                decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremin"].ToString());

                int r = Convert.ToInt32(result.Tables[0].Rows[0]["pressuremax"].ToString());
                int intJingJieXian = intYLong - intEnd - r * 2;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 20));

                r = Convert.ToInt32(result.Tables[0].Rows[0]["pressuremin"].ToString());
                intJingJieXian = intYLong - intEnd - r * 2;
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("预警值" + r, new Font("宋体", 12), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian - 20));
            }

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 50)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            g.DrawString("缩量状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 30);//缩量状态
            g.DrawString("工作状态：", new Font("宋体", 12), Brushes.Black, p.X - 80, p.Y + 50);//工作状态
            int xoffset = intXScale / 10;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["bracketno"].ToString(), new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y);//支架编号

                    string usestate = ds.Tables[0].Rows[i]["usestate"].ToString();
                    string tongxunstate = ds.Tables[0].Rows[i]["txstate"].ToString();//通讯状态
                    string chuangganqistate = ds.Tables[0].Rows[i]["alarmstate"].ToString();
                    if (usestate == "使用")
                    {
                        g.DrawString("使用", new Font("宋体", 12), Brushes.Black, p.X + xoffset*5, p.Y + 50);//工作状态
                        if (tongxunstate == "正常")
                        {
                            if (chuangganqistate.ToString().ToLower() == "true" || chuangganqistate == "1" || chuangganqistate == "超压" || chuangganqistate == "报警")
                            {
                                g.DrawString("报警", new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y + 30);//缩量状态
                            }
                            else
                            {
                                g.DrawString("正常", new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y + 30);//缩量状态
                            }

                        }
                        else
                        { //故障
                            g.DrawString("故障", new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y + 30);//缩量状态
                        }
                    }
                    else
                    { //停用
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y + 30);//使用状态
                        g.DrawString("停用", new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y + 50);//工作状态
                    }

                    if (usestate == "使用")
                    {
                        if (tongxunstate == "正常")
                        {
                            //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y + 30);//工作状态
                            p.X = intLeft + i * intXScale + xoffset;
                            decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure1"].ToString());
                            int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                            if (pressuse1 > decimalyalishangxian)
                            {
                                g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                                g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                            }
                            else if (pressuse1 > decimalyalixiaxian)
                            {
                                g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                                g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                            }
                            else
                            {
                                g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                                g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                            }
                            decimal pressuse2 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure2"].ToString());
                            int pressure_2 = Convert.ToInt32(pressuse2) * 2;
                            p.X = intLeft + i * intXScale + xoffset * 6;
                            if (pressuse2 > decimalyalishangxian)
                            {
                                g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                                g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_2 - 20);//压力2数值
                            }
                            else if (pressuse2 > decimalyalixiaxian)
                            {
                                g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                                g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_2 - 20);//压力2数值
                            }
                            else
                            {
                                g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                                g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_2 - 20);//压力2数值
                            }
                        }
                        else
                        {
                            //g.DrawString(ds.Tables[0].Rows[i]["txstate"].ToString(), new Font("宋体", 12), Brushes.Red, p.X + xoffset * 5, p.Y + 30);//工作状态
                            p.X = intLeft + i * intXScale + xoffset;
                            decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure1"].ToString());
                            int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                            g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                            g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                            decimal pressuse2 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure2"].ToString());
                            int pressure_2 = Convert.ToInt32(pressuse2) * 2;
                            p.X = intLeft + i * intXScale + xoffset * 6;
                            g.FillRectangle(Brushes.Gray, p.X, p.Y - pressure_2, xoffset * 3, pressure_2);//压力2柱状图
                            g.DrawString(pressuse2.ToString("0.00"), new Font("宋体", 10), Brushes.Black, p.X + xoffset, p.Y - pressure_2 - 20);//压力2数值
                        }
                    }
                    else
                    {
                        //g.DrawString("停用", new Font("宋体", 12), Brushes.Gray, p.X + xoffset * 5, p.Y + 30);//工作状态
                    }
                }
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e
            return img;
        }
        //活柱缩量分析
        public static Bitmap DrawingImg26(string AreaName, string FaceName, string dtss, string dtes)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 25;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 90; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 800;   //图片大小 高
            string biaoti = "活柱缩量分析（最大值）";//标题
            string danweiX = "支架号";//X轴单位
            string danweiY = "S(mm)";//Y轴单位


            //支架号及对应值
            string sql = @"select max(huozhu1) as pressure1,bracketno from HuoZhuSenInfo hzsi
left join HuoZhuData hzd on hzd.areaname=hzsi.areaname and hzsi.facename=hzd.facename and hzsi.sensorno=hzd.sensorno where hzsi.areaname='" + AreaName + "' and hzsi.facename='" + FaceName + "' and time>='" + dtss + "' and time<='" + dtes + "' group by hzsi.sensorno ,bracketno order by bracketno";
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题

            string sqlbjz = "select AlarmValue as pressuremax,YujingValue as pressuremin from dbo.HuoZhuPar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            if (result.Tables[0].Rows.Count > 0)
            {
                decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremax"].ToString());
                decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremin"].ToString());

                int r = Convert.ToInt32(result.Tables[0].Rows[0]["pressuremax"].ToString());
                int intJingJieXian = intYLong - intEnd - r * 2;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 20));

                r = Convert.ToInt32(result.Tables[0].Rows[0]["pressuremin"].ToString());
                intJingJieXian = intYLong - intEnd - r * 2;
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("预警值" + r, new Font("宋体", 12), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian - 20));
            }

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 50)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            int xoffset = intXScale / 10;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    g.DrawString(ds.Tables[0].Rows[i]["bracketno"].ToString(), new Font("宋体", 12), Brushes.Black, p.X + xoffset * 5, p.Y);//支架编号
                    p.X = intLeft + i * intXScale + xoffset;
                    decimal pressuse1 = Convert.ToDecimal(ds.Tables[0].Rows[i]["pressure1"].ToString());
                    int pressure_1 = Convert.ToInt32(pressuse1) * 2;
                    if (pressuse1 > decimalyalishangxian)
                    {
                        g.FillRectangle(Brushes.Red, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 8), Brushes.Red, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                    }
                    else if (pressuse1 > decimalyalixiaxian)
                    {
                        g.FillRectangle(Brushes.Orange, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 8), Brushes.Red, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Green, p.X, p.Y - pressure_1, xoffset * 3, pressure_1);//压力1柱状图
                        g.DrawString(pressuse1.ToString("0.00"), new Font("宋体", 8), Brushes.Red, p.X + xoffset, p.Y - pressure_1 - 20);//压力1数值
                    }
                }
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e
            return img;
        }
        //活柱缩量数据分析
        public static Bitmap DrawingImg27(string AreaName, string roadwayName, string Location, string dts, string dte, DataTable dt_img)
        {
            //时间划分 得出 开始时间dta，结束时间dtb， 总秒数totalSeconds
            #region
            //DateTime[] dt = new DateTime[25];
            //dte += " 23:59:59";
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            decimal BoltMax = 0;//根据最大值画纵坐标
            string sqltime = "SELECT min(time) as startT,max(time) as endT,max(HuoZhu1) as stress FROM HuoZhuData where areaName='" + AreaName + "' and FaceName='" + roadwayName + "' and SensorNo =(select SensorNo FROM HuoZhuSenInfo where  areaName='" + AreaName + "' and FaceName='" + roadwayName + "' and BracketNo = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            //string sqltime = "SELECT min(time) as startT,max(time) as endT FROM DisplacementData where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and SensorNo = (select SensorNo from DisSenInfo where areaName='" + AreaName + "' and roadwayName='" + roadwayName + "' and Location = '" + Location + "') and time between '" + Convert.ToDateTime(dts) + "' And '" + Convert.ToDateTime(dte) + "'";
            DataSet dstime = ExecuteSqlDataSet(sqltime, null);
            if (dstime.Tables[0].Rows.Count > 0)
            {
                if (dstime.Tables[0].Rows[0]["startT"].ToString() != "" && dstime.Tables[0].Rows[0]["endT"].ToString() != "")
                {
                    dta = Convert.ToDateTime(dstime.Tables[0].Rows[0]["startT"].ToString());
                    dtb = Convert.ToDateTime(dstime.Tables[0].Rows[0]["endT"].ToString());

                }
                if (dstime.Tables[0].Rows[0]["stress"].ToString() != "")
                {
                    decimal stress = Convert.ToDecimal(dstime.Tables[0].Rows[0]["stress"].ToString());
                    if (stress > BoltMax)
                    {
                        BoltMax = stress;
                    }
                }
            }
            string sqlbjz = "select * from HuoZhuPar where areaName='" + AreaName + "' and FaceName='" + roadwayName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["AlarmValue"].ToString();
                decimal a = Convert.ToDecimal(ss);
                if (BoltMax < a) BoltMax = a;
            }
            TimeSpan ts = dtb.Subtract(dta);
            double totalSeconds = ts.TotalSeconds;//总秒数
            #endregion
            int intYMultiple = 25;    //零刻度的值 Y intYScale = 50;
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            if (BoltMax <= 0) BoltMax = 600;
            decimal diffy = 1;//600高度比实际值，用实际值*此比例即所占像素数量
            decimal diff = BoltMax % intYMax;
            intYMultiple = (int)Math.Ceiling((BoltMax + diff) / intYMax);
            diffy = 600 / (BoltMax + diff);

            int intLeft = 50;   //左边距
            int intRight = 120; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            int intXLong = 1200 + intLeft + intRight;   //图片大小 长
            int intYLong = 600 + intEnd + intTop;   //图片大小 高 600像素高固定
            StringBuilder sb = new StringBuilder();//冗余变量
            dt_img.Columns.Add("offset", typeof(string));
            dt_img.Columns.Add("content", typeof(string));
            //绘制 标题，工作面支架编号，日期，x单位，y单位，x轴，y轴，y虚轴，x虚轴
            #region
            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(Location + "活柱缩量历史数据分析曲线", new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            //g.DrawString("工作面名称：" + AreaName + "  支架编号：" + BracketNo, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            //g.DrawString("日期：" + dta.ToString() + "至" + dtb.ToString(), new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawString("（时间）", new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd - 10));//X轴 单位
            g.DrawString("S(mm)", new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制 右Y轴虚线
            for (int i = (intYLong - intEnd); i >= intTop; i = i - intYScale)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向虚线
            }
            #endregion
            //绘制初始值，报警值
            #region
            sqlbjz = "select * from HuoZhuPar where areaName='" + AreaName + "' and FaceName='" + roadwayName + "'";
            result = ExecuteSqlDataSet(sqlbjz, null);
            if (result.Tables[0].Rows.Count > 0)
            {
                string ss = result.Tables[0].Rows[0]["AlarmValue"].ToString();
                decimal a = Convert.ToDecimal(ss);
                int r = Convert.ToInt32(a);
                int intJingJieXian = intYLong - intEnd - (int)(a * diffy);
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result.Tables[0].Rows[0]["AlarmValue"].ToString(), new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 10));

                ss = result.Tables[0].Rows[0]["YujingValue"].ToString();
                a = Convert.ToDecimal(ss);
                r = Convert.ToInt32(a);
                intJingJieXian = intYLong - intEnd - (int)(a * diffy);
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘初始值
                g.DrawString("预警值" + result.Tables[0].Rows[0]["YujingValue"].ToString(), new Font("宋体", 12), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian - 10));
            }
            #endregion
            //绘制 x刻度和x时间单位
            #region
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            double second24 = totalSeconds / intXMax;//间隔,25个横坐标
            g.RotateTransform(30);
            for (int i = 0; i < intXMax + 1; i++)
            {
                g.DrawString(dta.AddSeconds(i * second24).ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);
            #endregion
            //绘制 y刻度和y数量单位
            #region
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            #endregion
            //绘图 横坐标每一个像素绘制一个点  ***********主图**************
            #region
            sb.Append("{");
            sb.Append(string.Format("\"{0}\":{1}", "data", "["));

            double second1200 = totalSeconds / 1200;//1200像素
            Point rec = new Point(intLeft, intYLong - intEnd);
            decimal prevMPa = 0;
            for (int i = 0; i < (intXLong - intLeft - intRight); i++)
            {
                string content = "";
                string sqlq = @"Select avg(HuoZhu1) as Stress FROM HuoZhuData where areaName='" + AreaName + "' and FaceName='" + roadwayName + "' and SensorNo = (select SensorNo from HuoZhuSenInfo where areaName='" + AreaName + "' and FaceName='" + roadwayName + "' and BracketNo = '" + Location + "') and time between '" + dta.AddSeconds(second1200 * i) + "' and '" + dta.AddSeconds(second1200 * i + second1200) + "'";
                DataSet dsq = ExecuteSqlDataSet(sqlq, null);
                //***************************压力1
                if (dsq.Tables[0].Rows[0]["Stress"].ToString() != "")
                {
                    decimal avg = Convert.ToDecimal(dsq.Tables[0].Rows[0]["Stress"].ToString());
                    //定义终点
                    Point dec = new Point(intLeft + i, intYLong - intEnd - (int)(avg * diffy));
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                    rec = dec;
                    content += "缩量值" + avg.ToString("0.00") + "mm    ";
                    prevMPa = avg;
                }
                else
                {
                    //数据库无数据，取上一条记录
                    //定义终点
                    Point dec = new Point(intLeft + i, rec.Y);
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Blue, 1), rec, dec);
                    rec = dec;
                    content += "缩量值" + prevMPa.ToString("0.00") + "mm    ";
                }
                dt_img.Rows.Add(i.ToString(), content);
            }
            sb.Append("]");
            sb.Append("}");
            #endregion
            return img;
        }
        //DrawingImg,DrawingImg3,DrawingImg4 未使用，慎动！！！
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="FaceName"></param>
        /// <param name="BracketNo"></param>
        /// <param name="dts">开始时间 yyyy-MM-dd HH:mm:ss</param>
        /// <param name="dte">结束时间 yyyy-MM-dd HH:mm:ss</param>
        /// <returns></returns>
        public static Bitmap DrawingImg(string AreaName, string FaceName, string BracketNo, string dts, string dte)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 90; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1340;   //图片大小 长
            int intYLong = 800;   //图片大小 高
            string biaoti = "综采工作阻力历史数据分析曲线";//标题
            string gongzuomian = "工作面名称：" + AreaName + "  支架编号：" + BracketNo;//工作面名称，支架编号
            string riqi = "日期：" + dts + "至" + dte;//日期
            string danweiX = "时间";//X轴单位
            string danweiY = "P(MPa)";//Y轴单位

            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString(gongzuomian, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString(riqi, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 50)
            {

                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            DateTime[] dt = new DateTime[24];
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            TimeSpan ts = dtb.Subtract(dta);
            int sec = (int)ts.TotalSeconds;//总秒数
            int tssec = 0;
            int tsec = sec / 24;//间隔
            for (int i = 0; i < 24; i++)
            {
                if (tssec < sec)
                {
                    dt[i] = dta.AddSeconds(tssec);
                }
                else
                {
                    dt[i] = dtb;
                }
                if (i == 23)
                {
                    dt[i] = dtb;//强制最后一段时间为选择的最后时间
                }
                tssec += tsec;
            }
            g.RotateTransform(30);
            //g.DrawString("123456789", new Font("宋体", 12), Brushes.Black, new Point(375, 535));//+40,-25
            for (int i = 0; i < 24; i++)
            {
                g.DrawString(dt[i].ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }

            string sqlbjz = "select pressuremax from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            string result = ExecuteSqlValue(sqlbjz, null);
            if (result != "" && result != "no")
            {
                int r = Convert.ToInt32(result);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 20));
            }

            int prevX_1 = intLeft;//上一次X轴坐标
            int Y_1 = intYLong - intEnd;//y坐标原点0
            int prevY_1 = Y_1;//上一次Y轴坐标
            int currentY_1 = Y_1;//当前Y坐标
            decimal prevValue_1 = 0;//上一次 Y值
            Point p1x = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < 23; i++)
            {
                p1x.X = intLeft + i * intXScale;
                string sql = @"select avg(Pressure1) as pressure,max(Pressure1) as maxpre,min(Pressure1) as minpre from PressureData where SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "')  and areaName='" + AreaName + "' and FaceName ='" + FaceName + "' and time>='" + dt[i] + "' and time<'" + dt[i + 1] + "' ";
                DataTable dtt = ExecuteSqlDataSet(sql, null).Tables[0];
                if (dtt.Rows.Count > 0)
                {
                    decimal avg = Convert.ToDecimal(dtt.Rows[0]["pressure"].ToString() == "" ? "0" : dtt.Rows[0]["pressure"].ToString());
                    decimal max = Convert.ToDecimal(dtt.Rows[0]["maxpre"].ToString() == "" ? "0" : dtt.Rows[0]["maxpre"].ToString());
                    decimal min = Convert.ToDecimal(dtt.Rows[0]["minpre"].ToString() == "" ? "0" : dtt.Rows[0]["minpre"].ToString());
                    if (max - min > 3)
                    {
                        //绘制第一段
                        currentY_1 = Convert.ToInt32(Y_1 - max * 10);//y坐标
                        //定义起点
                        Point rec = new Point(prevX_1, prevY_1);
                        //定义终点
                        Point dec = new Point(p1x.X, currentY_1);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 3), rec, dec);

                        g.DrawString(max.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY_1 - 40));
                        //绘制第二段
                        int currentY2 = Convert.ToInt32(Y_1 - min * 10);//第二段Y坐标
                        //定义起点
                        rec = new Point(p1x.X, currentY_1);
                        //定义终点
                        dec = new Point(p1x.X, currentY2);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 3), rec, dec);
                        g.DrawString(min.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY2 - 40));
                        prevX_1 = p1x.X;//x坐标
                        prevY_1 = currentY2;//y坐标
                        prevValue_1 = min;//y值

                    }
                    else
                    {
                        currentY_1 = Convert.ToInt32(Y_1 - avg);
                        //定义起点
                        Point rec = new Point(prevX_1, prevY_1);
                        //定义终点
                        Point dec = new Point(p1x.X, currentY_1);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 3), rec, dec);
                        g.DrawString(avg.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY_1 - 40));
                        prevX_1 = p1x.X;//x坐标
                        prevY_1 = currentY_1;//y坐标
                        prevValue_1 = avg;//y值
                    }
                }
                else
                {
                    //数据不存在，取上一次数据
                    //定义起点
                    Point rec = new Point(prevX_1, prevY_1);
                    //定义终点
                    Point dec = new Point(p1x.X, currentY_1);
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Red, 3), rec, dec);
                    g.DrawString(prevValue_1.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY_1 - 40));
                    prevX_1 = p1x.X;//x坐标
                    prevY_1 = currentY_1;//y坐标

                }
            }
            int prevX_2 = intLeft;
            int Y_2 = intYLong - intEnd;//y坐标原点0
            int prevY_2 = Y_2;//y坐标原点0
            int currentY_2 = Y_2;
            decimal prevValue_2 = 0;
            Point p2x = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < 23; i++)
            {
                p2x.X = intLeft + i * intXScale;
                string sql = @"select avg(Pressure2) as pressure,max(Pressure2) as maxpre,min(Pressure2) as minpre from PressureData where SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and areaName='" + AreaName + "' and FaceName ='" + FaceName + "' and time>='" + dt[i] + "' and time<'" + dt[i + 1] + "' ";
                DataTable dtt = ExecuteSqlDataSet(sql, null).Tables[0];
                if (dtt.Rows.Count > 0)
                {
                    decimal avg = Convert.ToDecimal(dtt.Rows[0]["pressure"].ToString() == "" ? "0" : dtt.Rows[0]["pressure"].ToString());
                    decimal max = Convert.ToDecimal(dtt.Rows[0]["maxpre"].ToString() == "" ? "0" : dtt.Rows[0]["maxpre"].ToString());
                    decimal min = Convert.ToDecimal(dtt.Rows[0]["minpre"].ToString() == "" ? "0" : dtt.Rows[0]["minpre"].ToString());
                    if (max - min > 3)
                    {
                        //绘制第一段
                        currentY_2 = Convert.ToInt32(Y_2 - max * 10);//y坐标
                        //定义起点
                        Point rec = new Point(prevX_2, prevY_2);
                        //定义终点
                        Point dec = new Point(p2x.X, currentY_2);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                        g.DrawString(max.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY_2 - 20));
                        //绘制第二段
                        int currentY2 = Convert.ToInt32(Y_2 - min * 10);//第二段Y坐标
                        //定义起点
                        rec = new Point(p2x.X, currentY_2);
                        //定义终点
                        dec = new Point(p2x.X, currentY2);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                        g.DrawString(min.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY2 - 20));
                        prevX_2 = p2x.X;
                        prevY_2 = currentY2;
                        prevValue_2 = min;
                    }
                    else
                    {
                        currentY_2 = Convert.ToInt32(Y_2 - avg);
                        //定义起点
                        Point rec = new Point(prevX_2, prevY_2);
                        //定义终点
                        Point dec = new Point(p2x.X, currentY_2);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                        g.DrawString(avg.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY_2 - 20));
                        prevX_2 = p2x.X;
                        prevY_2 = currentY_2;
                        prevValue_2 = avg;
                    }
                }
                else
                {
                    //定义起点
                    Point rec = new Point(prevX_2, prevY_2);
                    //定义终点
                    Point dec = new Point(p2x.X, currentY_2);
                    //绘制趋势折线
                    g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                    g.DrawString(prevValue_2.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY_2 - 20));
                    prevX_2 = p1x.X;
                    prevY_2 = currentY_2;
                }
            }

            return img;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="FaceName"></param>
        /// <param name="BracketNo"></param>
        /// <param name="dts"></param>
        /// <param name="dte"></param>
        /// <returns></returns>
        public static Bitmap DrawingImg3(string AreaName, string FaceName, string yali, string yuzhi, string dts, string dte)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 100;   //左边距
            int intRight = 90; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1640;   //图片大小 长
            int intYLong = 800;   //图片大小 高
            string biaoti = "每日工作阻力分布曲线";//标题
            string danweiX = "支架号";//X轴单位
            string danweiY = "P(MPa)";//Y轴单位


            //支架号及对应值
            string sql = @"select BracketNo,avg(Pressure1) as p1,avg(Pressure2) as p2,p.time,avg(Pressure1+Pressure2)/2 as p3 from PressureData p 
inner join PreSenInfo s on p.areaName = s.AreaName and p.FaceName = s.FaceName and p.SensorNo = s.SensorNo 
where p.areaName = '" + AreaName + "' and p.FaceName = '" + FaceName + "' and s.Type = '液压支架' and time >='" + dts + "' And time<='" + dte + "' and (pressure1 > '" + yuzhi + "' or Pressure2 >'" + yuzhi + "') group by BracketNo,p.time order by BracketNo";
            DataSet ds = ExecuteSqlDataSet(sql, null);
            intXMax = ds.Tables[0].Rows.Count == 0 ? 1 : ds.Tables[0].Rows.Count;
            intXLong = intXMax * 400;
            intXScale = (intXLong - intLeft - intRight) / intXMax;


            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong / 2) - 100, 10));//标题
            g.DrawString("工作面名称：" + AreaName, new Font("宋体", 12), Brushes.Blue, new Point((intXLong / 2), 40));//标题
            string sqlbjz = "select pressuremax,pressuremin from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            DataSet result = ExecuteSqlDataSet(sqlbjz, null);
            decimal decimalyalishangxian = 0;
            decimal decimalyalixiaxian = 0;
            if (result.Tables[0].Rows.Count > 0)
            {
                decimalyalishangxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremax"].ToString());
                decimalyalixiaxian = Convert.ToDecimal(result.Tables[0].Rows[0]["pressuremin"].ToString());

                int r = Convert.ToInt32(result.Tables[0].Rows[0]["pressuremax"].ToString());
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + r, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 20));

                r = Convert.ToInt32(result.Tables[0].Rows[0]["pressuremin"].ToString());
                intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Orange, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("预警值" + r, new Font("宋体", 12), Brushes.Orange, new Point(intXLong - intRight, intJingJieXian - 20));
            }

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 50)
            {
                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴 虚线
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight + 20, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);

            int xoffset = intXScale;
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < intXMax; i++)
                {
                    p.X = intLeft + i * intXScale + xoffset;
                    //绘制横坐标刻度和直线
                    g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                    p.X = intLeft + i * intXScale + xoffset;
                    g.DrawString(ds.Tables[0].Rows[i]["bracketno"].ToString(), new Font("宋体", 12), Brushes.Black, p.X - 15, p.Y);//支架编号
                    string str_p1 = ds.Tables[0].Rows[i]["p1"].ToString();
                    string str_p2 = ds.Tables[0].Rows[i]["p2"].ToString();
                    decimal dec_p1 = Convert.ToDecimal(str_p1);
                    decimal dec_p2 = Convert.ToDecimal(str_p2);
                    if (yali == "1")
                    {
                        int int_p1 = Convert.ToInt32(dec_p1) * 10;
                        g.DrawLine(new Pen(Color.Red, 2), intLeft, intYLong - intEnd, p.X, intYLong - intEnd - int_p1); //
                    }
                    else if (yali == "2")
                    {
                        int int_p2 = Convert.ToInt32(dec_p2) * 10;
                        g.DrawLine(new Pen(Color.Red, 2), intLeft, intYLong - intEnd, p.X, intYLong - intEnd - int_p2); //
                    }
                    else if (yali == "3")
                    {
                        decimal dec_p3 = (dec_p1 + dec_p2) / 2;
                        int int_p3 = Convert.ToInt32(dec_p3) * 10;
                        g.DrawLine(new Pen(Color.Red, 2), intLeft, intYLong - intEnd, p.X, intYLong - intEnd - int_p3); //
                    }
                }
            }


            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy                 s
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy            e
            return img;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AreaName"></param>
        /// <param name="FaceName"></param>
        /// <param name="yali">1整架2左柱3右柱</param>
        /// <param name="dts"></param>
        /// <param name="dte"></param>
        /// <returns></returns>
        public static Bitmap DrawingImg4(string AreaName, string FaceName, string yali, string BracketNo, string dts, string dte)
        {
            //int intXMultiple = 1;    //零刻度的值 X
            int intYMultiple = 5;    //零刻度的值 Y
            int intXMax = 24;    //最大刻度(点数) X
            int intYMax = 12;    //最大刻度(点数) Y
            int intLeft = 50;   //左边距
            int intRight = 90; //右边距
            int intTop = 100;    //上边距
            int intEnd = 100;    //下边距
            int intXScale = 50;    //一刻度长度 X
            int intYScale = 50;    //一刻度高度 Y
            //int intData = 0;    //记录数
            int intXLong = 1340;   //图片大小 长
            int intYLong = 800;   //图片大小 高
            string biaoti = "综采工作阻力历史数据分析曲线";//标题
            string gongzuomian = "工作面名称：" + AreaName + "  支架编号：" + BracketNo;//工作面名称，支架编号
            string riqi = "日期：" + dts + "至" + dte;//日期
            string danweiX = "时间";//X轴单位
            string danweiY = "P(MPa)";//Y轴单位

            Pen pen1 = new Pen(Color.Gray);
            pen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            Bitmap img = new Bitmap(intXLong, intYLong); //图片大小
            Graphics g = Graphics.FromImage(img);
            g.Clear(Color.Snow);
            g.DrawString(biaoti, new Font("宋体", 16), Brushes.Black, new Point((intXLong - intLeft - intRight) / 2 - 50, 10));//标题
            g.DrawString(gongzuomian, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 100, intTop - 30));//工作面 支架编号
            g.DrawString(riqi, new Font("宋体", 12), Brushes.Blue, new Point(intLeft + 800, intTop - 30));//日期
            g.DrawLine(new Pen(Color.Black, 2), intLeft, intYLong - intEnd, intXLong - intRight, intYLong - intEnd); //绘制横向 X轴
            for (int i = (intYLong - intEnd); i >= intTop; i = i - 50)
            {

                g.DrawLine(pen1, intLeft, i, intXLong - intRight, i); //绘制横向 X轴
            }
            g.DrawString(danweiX, new Font("宋体", 12), Brushes.Black, new Point(intXLong - intRight, intYLong - intEnd));//X轴 单位
            Point p = new Point(intLeft, intYLong - intEnd);
            for (int i = 0; i < intXMax; i++)
            {
                p.X = intLeft + i * intXScale;
                //绘制横坐标刻度和直线
                g.DrawLine(Pens.Black, p, new Point(p.X, p.Y - 5));
                //g.DrawString(Convert.ToString(i + intXMultiple), new Font("宋体", 12), Brushes.Black, p);

            }
            DateTime[] dt = new DateTime[24];
            DateTime dta = Convert.ToDateTime(dts);//开始时间
            DateTime dtb = Convert.ToDateTime(dte);//结束时间
            TimeSpan ts = dtb.Subtract(dta);
            int sec = (int)ts.TotalSeconds;//总秒数
            int tssec = 0;
            int tsec = sec / 24;//间隔
            for (int i = 0; i < 24; i++)
            {
                if (tssec < sec)
                {
                    dt[i] = dta.AddSeconds(tssec);
                }
                else
                {
                    dt[i] = dtb;
                }
                if (i == 23)
                {
                    dt[i] = dtb;//强制最后一段时间为选择的最后时间
                }
                tssec += tsec;
            }
            g.RotateTransform(30);
            //g.DrawString("123456789", new Font("宋体", 12), Brushes.Black, new Point(375, 535));//+40,-25
            for (int i = 0; i < 24; i++)
            {
                g.DrawString(dt[i].ToString(), new Font("宋体", 12), Brushes.Black, new Point(395 + 43 * i, 580 - 25 * i));//绘制横坐标
            }
            g.RotateTransform(-30);

            g.DrawLine(new Pen(Color.Black, 2), intLeft, intTop, intLeft, intYLong - intEnd);   //绘制纵向 Y轴
            g.DrawLine(pen1, intXLong - intRight, intTop, intXLong - intRight, intYLong - intEnd);   //绘制纵向 右Y轴 虚线
            g.DrawString(danweiY, new Font("宋体", 12), Brushes.Black, new Point(intLeft - 40, intTop - 30));//Y轴 单位
            Point p1 = new Point(intLeft - 10, intYLong - intEnd);
            for (int j = 0; j <= intYMax; j++)
            {
                p1.Y = intYLong - intEnd - j * intYScale;
                Point pt = new Point(p1.X + 10, p1.Y);
                //绘制纵坐标的刻度和直线
                g.DrawLine(Pens.Black, pt, new Point(p1.X + 15, p1.Y));
                //绘制纵坐标的文字说明
                g.DrawString(Convert.ToString(j * intYMultiple), new Font("宋体", 12), Brushes.Black, new Point(p1.X - 25, p1.Y - 8));
            }

            string sqlbjz = "select pressuremax from dbo.PressurePar where areaname='" + AreaName + "' and facename='" + FaceName + "'";
            string result = ExecuteSqlValue(sqlbjz, null);
            if (result != "" && result != "no")
            {
                int r = Convert.ToInt32(result);
                int intJingJieXian = intYLong - intEnd - r * 10;
                g.DrawLine(new Pen(Color.Red, 2), intLeft, intJingJieXian, intXLong - intRight, intJingJieXian); //绘制横向警戒线
                g.DrawString("报警值" + result, new Font("宋体", 12), Brushes.Red, new Point(intXLong - intRight, intJingJieXian - 20));
            }
            if (yali == "1")
            {
                //平均值
                int prevX_0 = intLeft;
                int Y_0 = intYLong - intEnd;//y坐标原点0
                int prevY_0 = Y_0;//y坐标原点0
                int currentY_0 = Y_0;
                decimal prevValue_0 = 0;
                Point p0x = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < 23; i++)
                {
                    p0x.X = intLeft + i * intXScale;
                    string sql = @"select avg(Pressure1+Pressure2)/2 as pressure,max(Pressure1+Pressure2)/2 as maxpre,min(Pressure1+Pressure2)/2 as minpre from PressureData where SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and areaName='" + AreaName + "' and FaceName ='" + FaceName + "' and time>='" + dt[i] + "' and time<'" + dt[i + 1] + "' ";
                    DataTable dtt = ExecuteSqlDataSet(sql, null).Tables[0];
                    if (dtt.Rows.Count > 0)
                    {
                        decimal avg = Convert.ToDecimal(dtt.Rows[0]["pressure"].ToString() == "" ? "0" : dtt.Rows[0]["pressure"].ToString());
                        decimal max = Convert.ToDecimal(dtt.Rows[0]["maxpre"].ToString() == "" ? "0" : dtt.Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dtt.Rows[0]["minpre"].ToString() == "" ? "0" : dtt.Rows[0]["minpre"].ToString());
                        if (max - min > 3)
                        {
                            //绘制第一段
                            currentY_0 = Convert.ToInt32(Y_0 - max * 10);//y坐标
                            //定义起点
                            Point rec = new Point(prevX_0, prevY_0);
                            //定义终点
                            Point dec = new Point(p0x.X, currentY_0);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 3), rec, dec);
                            g.DrawString(max.ToString("0.00"), new Font("宋体", 12), Brushes.Blue, new Point(p0x.X, currentY_0 - 20));
                            //绘制第二段
                            int currentY0 = Convert.ToInt32(Y_0 - min * 10);//第二段Y坐标
                            //定义起点
                            rec = new Point(p0x.X, currentY_0);
                            //定义终点
                            dec = new Point(p0x.X, currentY0);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 3), rec, dec);
                            g.DrawString(min.ToString("0.00"), new Font("宋体", 12), Brushes.Blue, new Point(p0x.X, currentY0 - 20));
                            prevX_0 = p0x.X;
                            prevY_0 = currentY0;
                            prevValue_0 = min;
                        }
                        else
                        {
                            currentY_0 = Convert.ToInt32(Y_0 - avg);
                            //定义起点
                            Point rec = new Point(prevX_0, prevY_0);
                            //定义终点
                            Point dec = new Point(p0x.X, currentY_0);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Blue, 3), rec, dec);
                            g.DrawString(avg.ToString("0.00"), new Font("宋体", 12), Brushes.Blue, new Point(p0x.X, currentY_0 - 20));
                            prevX_0 = p0x.X;
                            prevY_0 = currentY_0;
                            prevValue_0 = avg;
                        }
                    }
                    else
                    {
                        //定义起点
                        Point rec = new Point(prevX_0, prevY_0);
                        //定义终点
                        Point dec = new Point(p0x.X, currentY_0);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Blue, 3), rec, dec);
                        g.DrawString(prevValue_0.ToString("0.00"), new Font("宋体", 12), Brushes.Blue, new Point(p0x.X, currentY_0 - 20));
                        prevX_0 = p0x.X;
                        prevY_0 = currentY_0;
                    }
                }
            }
            else if (yali == "2")
            {
                int prevX_1 = intLeft;//上一次X轴坐标
                int Y_1 = intYLong - intEnd;//y坐标原点0
                int prevY_1 = Y_1;//上一次Y轴坐标
                int currentY_1 = Y_1;//当前Y坐标
                decimal prevValue_1 = 0;//上一次 Y值
                Point p1x = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < 23; i++)
                {
                    p1x.X = intLeft + i * intXScale;
                    string sql = @"select avg(Pressure1) as pressure,max(Pressure1) as maxpre,min(Pressure1) as minpre from PressureData where SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "')  and areaName='" + AreaName + "' and FaceName ='" + FaceName + "' and time>='" + dt[i] + "' and time<'" + dt[i + 1] + "' ";
                    DataTable dtt = ExecuteSqlDataSet(sql, null).Tables[0];
                    if (dtt.Rows.Count > 0)
                    {
                        decimal avg = Convert.ToDecimal(dtt.Rows[0]["pressure"].ToString() == "" ? "0" : dtt.Rows[0]["pressure"].ToString());
                        decimal max = Convert.ToDecimal(dtt.Rows[0]["maxpre"].ToString() == "" ? "0" : dtt.Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dtt.Rows[0]["minpre"].ToString() == "" ? "0" : dtt.Rows[0]["minpre"].ToString());
                        if (max - min > 3)
                        {
                            //绘制第一段
                            currentY_1 = Convert.ToInt32(Y_1 - max * 10);//y坐标
                            //定义起点
                            Point rec = new Point(prevX_1, prevY_1);
                            //定义终点
                            Point dec = new Point(p1x.X, currentY_1);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 3), rec, dec);

                            g.DrawString(max.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY_1 - 20));
                            //绘制第二段
                            int currentY2 = Convert.ToInt32(Y_1 - min * 10);//第二段Y坐标
                            //定义起点
                            rec = new Point(p1x.X, currentY_1);
                            //定义终点
                            dec = new Point(p1x.X, currentY2);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 3), rec, dec);
                            g.DrawString(min.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY2 - 20));
                            prevX_1 = p1x.X;//x坐标
                            prevY_1 = currentY2;//y坐标
                            prevValue_1 = min;//y值

                        }
                        else
                        {
                            currentY_1 = Convert.ToInt32(Y_1 - avg);
                            //定义起点
                            Point rec = new Point(prevX_1, prevY_1);
                            //定义终点
                            Point dec = new Point(p1x.X, currentY_1);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Red, 3), rec, dec);
                            g.DrawString(avg.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY_1 - 20));
                            prevX_1 = p1x.X;//x坐标
                            prevY_1 = currentY_1;//y坐标
                            prevValue_1 = avg;//y值
                        }
                    }
                    else
                    {
                        //数据不存在，取上一次数据
                        //定义起点
                        Point rec = new Point(prevX_1, prevY_1);
                        //定义终点
                        Point dec = new Point(p1x.X, currentY_1);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Red, 3), rec, dec);
                        g.DrawString(prevValue_1.ToString("0.00"), new Font("宋体", 12), Brushes.Red, new Point(p1x.X, currentY_1 - 20));
                        prevX_1 = p1x.X;//x坐标
                        prevY_1 = currentY_1;//y坐标

                    }
                }
            }
            else if (yali == "3")
            {
                int prevX_2 = intLeft;
                int Y_2 = intYLong - intEnd;//y坐标原点0
                int prevY_2 = Y_2;//y坐标原点0
                int currentY_2 = Y_2;
                decimal prevValue_2 = 0;
                Point p2x = new Point(intLeft, intYLong - intEnd);
                for (int i = 0; i < 23; i++)
                {
                    p2x.X = intLeft + i * intXScale;
                    string sql = @"select avg(Pressure2) as pressure,max(Pressure2) as maxpre,min(Pressure2) as minpre from PressureData where SensorNo = (select SensorNo from PreSenInfo where areaName = '" + AreaName + "' and FaceName='" + FaceName + "' and BracketNo = '" + BracketNo + "') and areaName='" + AreaName + "' and FaceName ='" + FaceName + "' and time>='" + dt[i] + "' and time<'" + dt[i + 1] + "' ";
                    DataTable dtt = ExecuteSqlDataSet(sql, null).Tables[0];
                    if (dtt.Rows.Count > 0)
                    {
                        decimal avg = Convert.ToDecimal(dtt.Rows[0]["pressure"].ToString() == "" ? "0" : dtt.Rows[0]["pressure"].ToString());
                        decimal max = Convert.ToDecimal(dtt.Rows[0]["maxpre"].ToString() == "" ? "0" : dtt.Rows[0]["maxpre"].ToString());
                        decimal min = Convert.ToDecimal(dtt.Rows[0]["minpre"].ToString() == "" ? "0" : dtt.Rows[0]["minpre"].ToString());
                        if (max - min > 3)
                        {
                            //绘制第一段
                            currentY_2 = Convert.ToInt32(Y_2 - max * 10);//y坐标
                            //定义起点
                            Point rec = new Point(prevX_2, prevY_2);
                            //定义终点
                            Point dec = new Point(p2x.X, currentY_2);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                            g.DrawString(max.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY_2 - 20));
                            //绘制第二段
                            int currentY2 = Convert.ToInt32(Y_2 - min * 10);//第二段Y坐标
                            //定义起点
                            rec = new Point(p2x.X, currentY_2);
                            //定义终点
                            dec = new Point(p2x.X, currentY2);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                            g.DrawString(min.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY2 - 20));
                            prevX_2 = p2x.X;
                            prevY_2 = currentY2;
                            prevValue_2 = min;
                        }
                        else
                        {
                            currentY_2 = Convert.ToInt32(Y_2 - avg);
                            //定义起点
                            Point rec = new Point(prevX_2, prevY_2);
                            //定义终点
                            Point dec = new Point(p2x.X, currentY_2);
                            //绘制趋势折线
                            g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                            g.DrawString(avg.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY_2 - 20));
                            prevX_2 = p2x.X;
                            prevY_2 = currentY_2;
                            prevValue_2 = avg;
                        }
                    }
                    else
                    {
                        //定义起点
                        Point rec = new Point(prevX_2, prevY_2);
                        //定义终点
                        Point dec = new Point(p2x.X, currentY_2);
                        //绘制趋势折线
                        g.DrawLine(new Pen(Color.Green, 3), rec, dec);
                        g.DrawString(prevValue_2.ToString("0.00"), new Font("宋体", 12), Brushes.Green, new Point(p2x.X, currentY_2 - 20));
                        prevX_2 = p2x.X;
                        prevY_2 = currentY_2;
                    }
                }
            }



            return img;
        }
        //执行单条SQL语句,返回一个DataSet,主要用于查询
        private static DataSet ExecuteSqlDataSet(string strSQL, SqlParameter[] pmts)
        {
            SqlConnection myCn = new SqlConnection(strConn);
            try
            {
                myCn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(strSQL, myCn);
                sda.SelectCommand.CommandType = CommandType.Text;
                if (pmts != null)
                {
                    foreach (SqlParameter p in pmts)
                    {
                        if (p != null)
                        {
                            sda.SelectCommand.Parameters.Add(p);
                        }

                    }
                }
                DataSet ds = new DataSet("ds");
                sda.Fill(ds);
                return ds;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                myCn.Close();
            }
        }
        //返回单值
        private static string ExecuteSqlValue(string strSQL, SqlParameter[] pmts)
        {
            SqlConnection myCn = new SqlConnection(strConn);
            SqlCommand myCmd = new SqlCommand(strSQL, myCn);
            try
            {
                myCn.Open();
                if (pmts != null)
                {
                    foreach (SqlParameter p in pmts)
                    {
                        if (p != null)
                        {
                            myCmd.Parameters.Add(p);
                        }
                    }
                }

                object r = myCmd.ExecuteScalar();
                if (Object.Equals(r, null))
                {
                    return "no";
                }
                else
                {
                    return r.ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                myCmd.Dispose();
                myCn.Close();
            }
        }

        /// 对一个坐标点按照一个中心进行旋转 
        /// </summary> 
        /// <param name="center">中心点</param> 
        /// <param name="p1">要旋转的点</param> 
        /// <param name="angle">旋转角度，笛卡尔直角坐标</param> 
        /// <returns></returns> 
        private static Point PointRotate(Point center, Point p1, double angle)
        {
            Point tmp = new Point();
            double angleHude = angle * Math.PI / 180;/*角度变成弧度*/
            double x1 = (p1.X - center.X) * Math.Cos(angleHude) + (p1.Y - center.Y) * Math.Sin(angleHude) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angleHude) + (p1.Y - center.Y) * Math.Cos(angleHude) + center.Y;
            tmp.X = (int)x1;
            tmp.Y = (int)y1;
            return tmp;
        }
    }
}
