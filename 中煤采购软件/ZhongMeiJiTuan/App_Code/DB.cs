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
/// DB ��ժҪ˵��
/// </summary>
public static class DB
{
    static string strConn = ConfigurationManager.ConnectionStrings["webConnectionString"].ToString();

    //ҳ����ת
    public static void JavascriptShow(Page page, string text)
    {
        page.ClientScript.RegisterStartupScript(page.GetType(), "JavascriptShow", "<script>" + text + "</script>");
    }


  
    //ִ�е���SQL���,��Ҫ������ɾ��
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
    //ִ�е���SQL���,��Ҫ������ɾ��
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
    /// �����ʼ�
    /// </summary>    
    /// <param name="MailTo">Ŀ���ַ</param>
    /// <param name="Subject">����</param>
    /// <param name="MailBody">����</param>

    public static void SetToMail(string MailTo, string Subject, string MailBody)
    {
        MailMessage objMailMessage = new MailMessage();
        objMailMessage.From = "844954290@qq.com";//Դ�ʼ���ַ 
        objMailMessage.To = MailTo;//Ŀ���ʼ���ַ��Ҳ���Ƿ����ҹ� 
        objMailMessage.Subject = Subject;//�����ʼ��ı��� 
        objMailMessage.Body = MailBody;//�����ʼ������� 
        objMailMessage.Fields.Add(" http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
        //�û��� 
        objMailMessage.Fields.Add(" http://schemas.microsoft.com/cdo/configuration/sendusername", "844954290@qq.com");
        //���� 
        objMailMessage.Fields.Add(" http://schemas.microsoft.com/cdo/configuration/sendpassword", "kebi19491001");
        //���û���������д��룬��������´�����ʾ���������ܾ���һ�������ռ��˵�ַ����������ӦΪ: 554 : Client host rejected: Access denied 
        //SMTP��ַ 
        SmtpMail.SmtpServer = "smtp.foxmail.com";
        //��ʼ�����ʼ� 
        SmtpMail.Send(objMailMessage);
    }


    //ִ�е���SQL���,����һ��DataSet,��Ҫ���ڲ�ѯ
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

    //ִ�е���SQL���,����һ��DataTable,��Ҫ���ڲ�ѯ
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


    //���ص�ֵ
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

    //ִ�ж���SQL���,�������ݿ�����
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
    //ִ�ж���SQL���,�������ݿ�����
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
        //todo:ת������
        //ȡ����������ʵ���͡�Ӧ����Person
        Type type = typeof(T);
        foreach (DataRow dr in dt.Rows)
        {
            //ÿ�б���ʱ��һ��������ʵ����
            T t = new T();
            //ȡ�����������й�������            
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
        //todo:ת������
        //ȡ����������ʵ���͡�Ӧ����Person
        Type type = typeof(T);
        foreach (DataRow dr in dt.Rows)
        {
            //ÿ�б���ʱ��һ��������ʵ����
            T t = new T();
            //ȡ�����������й�������            
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


    #region ����һ��DataTable +ExecuteGetDataTable ExecuteGetDataTable(string sql, CommandType type, params SqlParameter[] parameters)
    /// <summary>
    /// ȡ��һ��DataTable
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

    #region ����һ����ҳ��ѯ��DataTable +DataTable ExecuteGetPagerDataTable(string sql, int currPage, int pageSize)
    public static DataTable ExecuteGetPagerDataTable(string sql, int currPage, int pageSize)
    {
        int b1 = (currPage - 1) * pageSize + 1;
        int b2 = currPage * pageSize;

        SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter(){ParameterName="@p1",SqlDbType= SqlDbType.Int,Value=b1},
                new SqlParameter(){ParameterName="@p2",SqlDbType= SqlDbType.Int,Value=b2}
            };
        /*string sql = @"SELECT  *
                        FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY ���� ) AS rowid ,
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
        //todo:ת������
        //ȡ����������ʵ���͡�Ӧ����Person
        Type type = typeof(T);
        foreach (DataRow dr in dt.Rows)
        {
            //ÿ�б���ʱ��һ��������ʵ����
            T t = new T();
            //ȡ�����������й�������            
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                if (dt.Columns.Contains(pi.Name))
                {
                    object value = dr[pi.Name];
                    if (value != DBNull.Value)
                    {
                        //todo:�����������if�������Ƿ������id���ֶ����Ƿ�ת���ַ���
                        //id���͵��ֶ�Ҳת���ַ���
                        if(pi.Name.Contains("id")||pi.Name.Contains("Id"))
                        {
                            pi.SetValue(t, value.ToString(), null);
                        }
                        else if (value is DateTime)
                        {
                            //�����DateTime���͵ĲŽ�������ת��
                            switch (dateFormat)
                            {
                                case 1:
                                    //ת�ɶ��������ַ�������ע���ʱT��Ĵ��ֶα�����string����
                                    pi.SetValue(t, DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd"), null);
                                    break;
                                case 2:
                                    //ת�ɳ��������ַ�������ע���ʱT��Ĵ��ֶα�����string����
                                    pi.SetValue(t, value.ToString(), null);
                                    break;
                                case 0:
                                //��ת�������������͡�ע���ʱ���Ŀ������Json�����ǲ����������ڣ���ͻ���JS����ChangeDateFormat����һ��ת���ɿɱ��ϵ����ڡ�
                                //����ע���ʱT��Ĵ��ֶα�����DateTime����
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
    /// ��ҳ����
    /// </summary>
    /// <param name="table">����</param>
    /// <param name="pageSize">һҳ��ʾ����</param>
    /// <param name="pageIndex">��ǰҳ</param>
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
