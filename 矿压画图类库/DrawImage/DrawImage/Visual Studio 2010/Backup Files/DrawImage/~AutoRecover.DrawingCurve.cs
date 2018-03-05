using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using System.Drawing;

namespace DrawImage
{
    public class DrawingCurve
    {
        public DrawingCurve()
        {
            //intXScale = (intXLong - intLeft - intRight) / (intXMax + 1);//一刻度长度 X
            //intYScale = (intYLong - intTop - intEnd) / (intYMax + 1);//一刻度高度 Y
        }

        public Bitmap DrawingImg(string AreaName, string FaceName, string BracketNo, string dts, string dte)
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
            string result = DB.ExecuteSqlValue(sqlbjz, null);
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
                DataTable dtt = DB.ExecuteSqlDataSet(sql, null).Tables[0];
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
                DataTable dtt = DB.ExecuteSqlDataSet(sql, null).Tables[0];
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
    }
}
