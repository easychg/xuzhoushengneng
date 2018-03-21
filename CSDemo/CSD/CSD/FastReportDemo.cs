using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSD
{
    public partial class FastReportDemo : Form
    {
        public FastReportDemo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select * from fastReportForPrint where frxName='frxa.frx'";
            DataSet dsa = getDataSet(sql);
            DataSet FDataSet = new DataSet();
            if (dsa.Tables[0].Rows.Count > 0) {
                sql = dsa.Tables[0].Rows[0]["sqlStr"].ToString() + " where id=1";
                FDataSet = getDataSet(sql);
                FastReport.Report report1 = new FastReport.Report();
                try
                {
                    // load the existing report
                    report1.Load(dsa.Tables[0].Rows[0]["frxName"].ToString());
                    // register the dataset
                    report1.RegisterData(FDataSet);
                    report1.GetDataSource(dsa.Tables[0].Rows[0]["fromTable"].ToString()).Enabled = true;
                    // run the report
                    report1.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // free resources used by report
                    report1.Dispose();
                }
            }
            
            //DataTable table = new DataTable();
            //table.TableName = "S_User";
            //FDataSet.Tables.Add(table);
            //table.Columns.Add("Name", typeof(string));
            //table.Columns.Add("Pwd", typeof(string));
            //table.Rows.Add("管理员", "19A2854144B63A8F7617A6F225019B12");
            //table.Rows.Add("超级管理员", "EBEC10142B9FC9E4E67B0CFD347E23E6");

            
        }
        private DataSet getDataSet(string str)
        {
            SqlConnection conn = new SqlConnection("Data Source=.;Initial Catalog=CSDemo;uid=sa;pwd=123;");
            DataSet ds = new DataSet();
            try
            {
                SqlCommand cmd = new SqlCommand(str, conn);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                da.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;

            }
        }
    }
    public static class MyFunctionspd {
        public static string pdwj() {
            return "";
        }
    }
}
