using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;

namespace Web
{
    
    public partial class ReflactTest : System.Web.UI.Page
    {
        class test
        {
            public int id { get; set; }
            public string test_name { get; set; }
            public DateTime addtime { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            test t = new test();
            t.id = 1;
            t = (test)GetModel(t);
            DataTable dt = Model2DataTable(t);
            rpt_a.DataSource = dt;
            rpt_a.DataBind();
        }
        #region
        static string strConn = "Data Source=.;Initial Catalog=test_1;User ID=sa;Password=123;Pooling=true;MAX Pool Size=30;Min Pool Size=5;Connection Lifetime=33330;";
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
        public static int ExecuteSql_New(string strSQL, CommandType cmdType, SqlParameter[] pmts)
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
        public static bool ExecuteSqlsTransaction(string[] strSQLs, SqlParameter[] pmts)
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
        #endregion
        public static DataTable Model2DataTable<T>(T model) {
            Type type = model.GetType();
            string na = type.Name;
            DataTable dt = new DataTable(na);
            DataRow dr = dt.NewRow();
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(model, null);
                string name = i.Name;
                dt.Columns.Add(name, i.PropertyType);
                dr[name] = obj;
            }
            dt.Rows.Add(dr);
            return dt;
        }
        public static object DataTable2Model<T>(DataTable dt,T model) {
            Type type = model.GetType();
            string na = type.Name;
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            foreach (PropertyInfo i in ps)
            {
                //object obj = i.GetValue(model, null);
                string name = i.Name;
                foreach (DataColumn dr in dt.Columns)
                {
                    if (dr.ColumnName.ToString() == name.ToString())
                    {
                        i.SetValue(model, Convert.ChangeType(dt.Rows[0][dr.ColumnName].ToString(), i.PropertyType), null);//类型转换。
                    }
                }
            }
            return model;
        }
        public static List<T> DataTable2ModelList<T>(DataTable dt)
        {
            //List<T> models = new List<T>();
            
            //Type type = model.GetType();
            //string na = type.Name;
            //System.Reflection.PropertyInfo[] ps = type.GetProperties();
            //foreach (PropertyInfo i in ps)
            //{
            //    //object obj = i.GetValue(model, null);
            //    string name = i.Name;
            //    foreach (DataColumn dr in dt.Columns)
            //    {
            //        if (dr.ColumnName.ToString() == name.ToString())
            //        {
            //            i.SetValue(model, Convert.ChangeType(dt.Rows[0][dr.ColumnName].ToString(), i.PropertyType), null);//类型转换。
            //        }
            //    }
            //}
            //return models;
            return null;
        }
        public static int DeleteModel<T>(T model) {
            Type type = model.GetType();
            string na = type.Name;
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            string sqla = "delete * from  " + na + " where id=";
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(model, null);
                string name = i.Name;
                if (name == "id")
                {
                    sqla += "'" + obj + "'";
                }
                else
                {
                    return 0;
                }
            }
            return 1;
        }
        public static object GetModel<T>(T model){
            Type type = model.GetType();
            string na = type.Name;
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            string sqla = "select * from  " + na + " where id=";
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(model, null);
                string name = i.Name;
                if (name == "id")
                {
                    sqla += "'"+obj+"'";
                }
            }
            DataTable dt = ExecuteSqlDataTable(sqla, null) ;
             
            foreach (PropertyInfo i in ps)
            {
                //object obj = i.GetValue(model, null);
                string name = i.Name;
                foreach (DataColumn dr in dt.Columns)
                {
                    if (dr.ColumnName.ToString() == name.ToString())
                    {
                        i.SetValue(model, Convert.ChangeType(dt.Rows[0][dr.ColumnName].ToString(), i.PropertyType), null);//类型转换。
                    }
                }
            }
            return model;
        }
        public static int UpdateModel<T>(T model)
        {

            Type type = model.GetType();
            string na = type.Name;
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            string sqla = "update "+na+" set ";
            string sqlb = "";
            string sqlc = " where ";
            string name = "";
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(model, null);
                name = i.Name;
                if (name == "id")
                {
                    sqlc += name + "='" + obj + "' ";
                }
                else {
                    sqlb += name + "='" + obj + "', ";
                }
            }
            sqlb = sqlb.Substring(0,sqlb.Length - 2);
            string sql = sqla + sqlb + sqlc;
            return 0;
        }
        public static int SaveModel<T>(T model)
        {

            Type type = model.GetType();
            string na = type.Name;
            System.Reflection.PropertyInfo[] ps = type.GetProperties();
            string sqla = "insert into " + na + " ( ";
            string sqlb = "values (";
            string name = "";
            foreach (PropertyInfo i in ps)
            {
                object obj = i.GetValue(model, null);
                name = i.Name;
                if (name != "id") {
                    sqla += name + ",";
                    sqlb += "'" + obj + "',";
                }
            }
            sqla = sqla.Substring(0, sqla.Length - 1);
            sqlb = sqlb.Substring(0, sqlb.Length - 1);
            string sql = sqla+") " + sqlb+")";
            return 0;
        }
    }
}