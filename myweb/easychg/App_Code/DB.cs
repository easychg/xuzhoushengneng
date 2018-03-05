using System;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mail;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

/// <summary>
/// DB 的摘要说明
/// </summary>
public static class DB
{
    static string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();

    //页面跳转
    public static void JavascriptShow(Page page, string text)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "JavascriptShow", "<script>" + text + "</script>");
    }


  
    //执行单条SQL语句,主要用于增删改
    public static int ExecuteSql(string strSQL, SqlParameter[] pmts)
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
            return myCmd.ExecuteNonQuery();
            //return 0;
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
    //执行单条SQL语句,主要用于增删改
    public static int ExecuteSql_New(string strSQL,CommandType cmdType, SqlParameter[] pmts)
    {
        SqlConnection myCn = new SqlConnection(strConn);
        SqlCommand myCmd = new SqlCommand(strSQL, myCn);
        myCmd.CommandType = cmdType;
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
            return myCmd.ExecuteNonQuery();
            //return 0;
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
    /// <summary>
    /// 发送邮件
    /// </summary>    
    /// <param name="MailTo">目标地址</param>
    /// <param name="Subject">标题</param>
    /// <param name="MailBody">内容</param>

    public static void SetToMail(string MailTo, string Subject, string MailBody)
    {
        MailMessage objMailMessage = new MailMessage();
        objMailMessage.From = "844954290@qq.com";//源邮件地址 
        objMailMessage.To = MailTo;//目的邮件地址，也就是发给我哈 
        objMailMessage.Subject = Subject;//发送邮件的标题 
        objMailMessage.Body = MailBody;//发送邮件的内容 
        objMailMessage.Fields.Add(" http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
        //用户名 
        objMailMessage.Fields.Add(" http://schemas.microsoft.com/cdo/configuration/sendusername", "844954290@qq.com");
        //密码 
        objMailMessage.Fields.Add(" http://schemas.microsoft.com/cdo/configuration/sendpassword", "kebi19491001");
        //如果没有上述三行代码，则出现如下错误提示：服务器拒绝了一个或多个收件人地址。服务器响应为: 554 : Client host rejected: Access denied 
        //SMTP地址 
        SmtpMail.SmtpServer = "smtp.foxmail.com";
        //开始发送邮件 
        SmtpMail.Send(objMailMessage);
    }


    //执行单条SQL语句,返回一个DataSet,主要用于查询
    public static DataSet ExecuteSqlDataSet(string strSQL, SqlParameter[] pmts)
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

    //执行单条SQL语句,返回一个DataTable,主要用于查询
    public static DataTable ExecuteSqlDataTable(string strSQL, SqlParameter[] pmts)
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
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        finally
        {
            myCn.Close();
        }
    }


    //返回单值
    public static string ExecuteSqlValue(string strSQL, SqlParameter[] pmts)
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

    //执行多条SQL语句,开启数据库事务
    public static void ExecuteSqlsTransaction(string[] strSQLs, SqlParameter[] pmts)
    {
        SqlConnection myCn = null;
        SqlTransaction tx = null;
        SqlCommand myCmd = null;
        try
        {
            myCn = new SqlConnection(strConn);
            myCn.Open();
            myCmd = new SqlCommand();
            myCmd.Connection = myCn;
            tx = myCn.BeginTransaction(IsolationLevel.Serializable);
            for (int i = 0; i < strSQLs.Length; i++)
            {
                if (strSQLs[i] == "")
                    continue;

                myCmd.Parameters.Clear();
                myCmd.CommandText = strSQLs[i];

                if (pmts != null)
                {
                    foreach (SqlParameter p in Get_PmtsCopy(pmts))
                    {
                        if (p != null)
                        {
                            myCmd.Parameters.Add(p);
                        }
                    }
                }
                myCmd.ExecuteNonQuery();
            }
            tx.Commit();
        }
        catch (Exception ex)
        {
            tx.Rollback();
            throw new Exception(ex.Message);
        }
        finally
        {
            myCmd.Dispose();
            tx.Dispose();
            myCn.Close();
        }
    }
    //执行多条SQL语句,开启数据库事务
    public static bool ExecuteSqlsTransaction2(string[] strSQLs, SqlParameter[] pmts)
    {
        bool b = true;
        SqlConnection myCn = null;
        SqlTransaction tx = null;
        SqlCommand myCmd = null;
        try
        {
            myCn = new SqlConnection(strConn);
            myCn.Open();
            myCmd = new SqlCommand();
            myCmd.Connection = myCn;
            tx = myCn.BeginTransaction(IsolationLevel.Serializable);
            myCmd.Transaction = tx;
            for (int i = 0; i < strSQLs.Length; i++)
            {
                if (strSQLs[i] == "")
                    continue;

                myCmd.Parameters.Clear();
                myCmd.CommandText = strSQLs[i];

                if (pmts != null)
                {
                    foreach (SqlParameter p in Get_PmtsCopy(pmts))
                    {
                        if (p != null)
                        {
                            myCmd.Parameters.Add(p);
                        }
                    }
                }
                myCmd.ExecuteNonQuery();
            }
            tx.Commit();
            
        }
        catch (Exception ex)
        {
            tx.Rollback();
            b = false;
            throw new Exception(ex.Message);
        }
        finally
        {
            myCmd.Dispose();
            tx.Dispose();
            myCn.Close();
        }
        return b;
    }
    public static SqlParameter[] Get_PmtsCopy(SqlParameter[] pmts)
    {
        if (pmts == null)
        {
            return null;
        }

        if (pmts.Length == 0)
        {
            return null;
        }

        SqlParameter[] copyTo = new SqlParameter[pmts.Length];

        for (int i = 0; i < pmts.Length; i++)
        {
            if (pmts[i] != null)
            {
                copyTo[i] = (SqlParameter)((ICloneable)pmts[i]).Clone();
            }
        }

        return copyTo;
    }

    public static bool HasDangerousContents(string contents)
    {
        bool bReturnValue = false;
        if (contents.Length > 0)
        {
            //convert to lower
            string sLowerStr = contents.ToLower();
            //RegularExpressions
            string sRxStr = @"(\sand\s)|(\sand\s)|(\slike\s)|(select\s)|(insert\s)|(delete\s)|(update\s[\s\S].*\sset)|(create\s)|(\stable)|(<[iframe|/iframe|script|/script])|(')|(\sexec)|(\sdeclare)|(\struncate)|(\smaster)|(\sbackup)|(\smid)|(\scount)";
            //Match
            bool bIsMatch = false;
            System.Text.RegularExpressions.Regex sRx = new System.Text.RegularExpressions.Regex(sRxStr);
            bIsMatch = sRx.IsMatch(sLowerStr, 0);
            if (bIsMatch)
            {
                bReturnValue = true;
            }
        }
        return bReturnValue;
    }

    public static DateTime SysDate()
    {
        SqlConnection myCn = new SqlConnection(strConn);
        SqlCommand myCmd = new SqlCommand("select getdate()", myCn);
        try
        {
            myCn.Open();

            object r = myCmd.ExecuteScalar();

            return DateTime.Parse(r.ToString());
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

    public static List<T> ConvertToList<T>(DataTable dt) where T : new()
    {
        List<T> __list = new List<T>();
        //todo:转换代码
        //取得这个类的真实类型。应该是Person
        Type type = typeof(T);
        foreach (DataRow dr in dt.Rows)
        {
            //每行遍历时建一个这个类的实例。
            T t = new T();
            //取得这个类的所有公共属性            
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (dt.Columns.Contains(pi.Name))
                {
                    object value;
                    if (pi.Name.Contains("id"))
                        value = dr[pi.Name].ToString();
                     else
                        value= dr[pi.Name];
                    if (value != DBNull.Value)
                    {
                        pi.SetValue(t, value, null);
                    }
                }
            }
            __list.Add(t);
        }
        return __list;
    }
    public static List<T> ConvertToList2<T>(DataTable dt) where T : new()
    {
        List<T> __list = new List<T>();
        //todo:转换代码
        //取得这个类的真实类型。应该是Person
        Type type = typeof(T);
        foreach (DataRow dr in dt.Rows)
        {
            //每行遍历时建一个这个类的实例。
            T t = new T();
            //取得这个类的所有公共属性            
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (dt.Columns.Contains(pi.Name))
                {
                    DateTime _tmpdate;
                    object value;
                    if (DateTime.TryParse(dr[pi.Name].ToString(), out _tmpdate))
                    {
                        value = _tmpdate.ToString("yyyy-MM-dd");
                    }
                    else
                    { 
                        value = dr[pi.Name].ToString();
                    }
                    if (value != DBNull.Value)
                    {
                        pi.SetValue(t, value, null);
                    }
                }
            }
            __list.Add(t);
        }
        return __list;
    }


    #region 返回一个DataTable +ExecuteGetDataTable ExecuteGetDataTable(string sql, CommandType type, params SqlParameter[] parameters)
    /// <summary>
    /// 取得一个DataTable
    /// </summary>
    /// <param name="sql"></param>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static DataTable ExecuteGetDataTable(string sql, CommandType type, params SqlParameter[] parameters)
    {
        using (SqlDataAdapter da = new SqlDataAdapter(sql, strConn))
        {
            da.SelectCommand.CommandType = type;
            if (parameters != null)
            {
                da.SelectCommand.Parameters.AddRange(parameters);
            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
    #endregion

    #region 返回一个分页查询的DataTable +DataTable ExecuteGetPagerDataTable(string sql, int currPage, int pageSize)
    public static DataTable ExecuteGetPagerDataTable(string sql, int currPage, int pageSize)
    {
        int b1 = (currPage - 1) * pageSize + 1;
        int b2 = currPage * pageSize;

        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){ParameterName="@p1",SqlDbType= SqlDbType.Int,Value=b1},
                new SqlParameter(){ParameterName="@p2",SqlDbType= SqlDbType.Int,Value=b2}
            };
        /*string sql = @"SELECT  *
                        FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY 工号 ) AS rowid ,
                                            *
                                  FROM      dbo.Employee
                                ) AS #t
                        WHERE   #t.rowid BETWEEN @p1 AND @p2";*/
        DataTable __dt = ExecuteGetDataTable(sql, System.Data.CommandType.Text, parameters);
        return __dt;
    }

    #endregion

    public static List<T> ConvertToList<T>(DataTable dt, int dateFormat) where T : new()
    {
        List<T> __list = new List<T>();
        //todo:转换代码
        //取得这个类的真实类型。应该是Person
        Type type = typeof(T);
        foreach (DataRow dr in dt.Rows)
        {
            //每行遍历时建一个这个类的实例。
            T t = new T();
            //取得这个类的所有公共属性            
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (dt.Columns.Contains(pi.Name))
                {
                    object value = dr[pi.Name];
                    if (value != DBNull.Value)
                    {
                        //todo:可在这里添加if来处理是否处理包含id的字段名是否转成字符串
                        //id类型的字段也转成字符串
                        if(pi.Name.Contains("id")||pi.Name.Contains("Id"))
                        {
                            pi.SetValue(t, value.ToString(), null);
                        }
                        else if (value is DateTime)
                        {
                            //如果是DateTime类型的才进行以下转换
                            switch (dateFormat)
                            {
                                case 1:
                                    //转成短日期型字符串——注意此时T类的此字段必须是string类型
                                    pi.SetValue(t, DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd"), null);
                                    break;
                                case 2:
                                    //转成长日期型字符串——注意此时T类的此字段必须是string类型
                                    pi.SetValue(t, value.ToString(), null);
                                    break;
                                case 0:
                                //不转换，仍是日期型。注意此时如果目的用于Json，将是不正常的日期，须客户端JS函数ChangeDateFormat作进一步转换成可辩认的日期。
                                //——注意此时T类的此字段必须是DateTime类型
                                default:
                                    pi.SetValue(t, value, null);
                                    break;
                            }
                        }
                        else
                        {
                            pi.SetValue(t, value, null);
                        }
                    }
                }
            }
            __list.Add(t);
        }
        return __list;
    }
    /// <summary>
    /// 分页调用
    /// </summary>
    /// <param name="table">表名</param>
    /// <param name="pageSize">一页显示数量</param>
    /// <param name="pageIndex">当前页</param>
    /// <param name="sql">sql</param>
    public static DataSet Paging(string table, int pageSize, int pageIndex, string where, string key, out int count)
    {
        string sql = "select top " + pageSize + " *  from  " + table + " where " + key + " not  in (select top " + ((pageIndex - 1) * pageSize) + " " + key + " from " + table + " where  " + where + " order  by " + key + " desc ) and " + where + " order by " + key + " desc";
        string sqlCount = "select count(*) as count  from " + table + " where  " + where;
        count = Convert.ToInt32(DB.ExecuteSqlValue(sqlCount, null));
        DataSet ds = DB.ExecuteSqlDataSet(sql, null);
        return ds;
    }
}
