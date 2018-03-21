using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataSet FDataSet = new DataSet();
            DataTable table = new DataTable();
            table.TableName = "S_User";
            FDataSet.Tables.Add(table);
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Pwd", typeof(string));
            table.Rows.Add("管理员", "19A2854144B63A8F7617A6F225019B12");
            //table.Rows.Add("超级管理员", "EBEC10142B9FC9E4E67B0CFD347E23E6");
            FastReport.Report report1 = new FastReport.Report();
            try
            {
                // load the existing report
                report1.Load("user.frx");
                // register the dataset
                report1.RegisterData(FDataSet);
                report1.GetDataSource("S_User").Enabled = true;
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

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
    public static class MyFunctionspd {
        public static string pdwj() {
            return "";
        }
    }
}
