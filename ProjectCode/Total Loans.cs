using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApplication2
{
    public partial class Total_Loans : Form
    {
        string x;
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public Total_Loans()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimePicker1.Text) > Convert.ToDateTime(dateTimePicker2.Text))
            {
                MessageBox.Show("Start date cannot be greater than end date");
            }
            else
            {
                filterby_Date();
            }   
        }
        protected void filterby_Date()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                dt.Columns.Add("TotalLoans", typeof(Int32));
                dt.Columns.Add("Approved Loans %", typeof(double));
                dt.Columns.Add("Rejected Loans %", typeof(double));
                dt.Columns.Add("StartDate", typeof(string));
                dt.Columns.Add("EndDate", typeof(string));
                string item="Loan Granted";
                SqlCommand cmd = new SqlCommand("SELECT Count(appln_no) as TotalLoans FROM granted_loans where DateOfAppln>='" + Convert.ToDateTime(dateTimePicker1.Text) + "' and DateOfAppln<='" + Convert.ToDateTime(dateTimePicker2.Text) + "'");
                SqlCommand cmd1 = new SqlCommand("SELECT Count(appln_no) as ApprovedLoans FROM granted_loans where status='" + item + "' and DateOfAppln>='" + Convert.ToDateTime(dateTimePicker1.Text) + "' and DateOfAppln<='" + Convert.ToDateTime(dateTimePicker2.Text) + "'");
                SqlDataReader cmdRdr = null;
                SqlDataReader cmd1Rdr = null;
                conn.Open();
                cmd.Connection = conn;
                cmd1.Connection = conn;
                cmdRdr = cmd.ExecuteReader();
                cmd1Rdr = cmd1.ExecuteReader();
                while (cmdRdr.Read())
                {
                    x = cmdRdr["TotalLoans"].ToString();
                }
                double y = Convert.ToInt32(x);
                if (y.ToString() == "")
                    y = 0;
                while (cmd1Rdr.Read())
                {
                    x = cmd1Rdr["ApprovedLoans"].ToString();
                }
                double z = Convert.ToInt32(x);
                if (z.ToString() == "")
                    z = 0;
                double a=0;
                double w = y - z;
                if (z == 0)
                    a = 0;
                else
                {
                    a = z * 100 / y;
                    a = Math.Round(a, 2);
                }
                double r =0;
                if (w == 0)
                    r = 0;
                else
                {
                    r = w * 100 / y;
                    r = Math.Round(r, 2);
                }
                dt.Rows.Add(y,a,r,dateTimePicker1.Value.ToShortDateString(),dateTimePicker2.Value.ToShortDateString());
                MessageBox.Show("TotalLoans are '" + y + "' and approved are '"+z+"' and %s are '"+a+"' '"+r+"'");
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("LoanPercent.xml");
                LoanPercent cr = new LoanPercent();
                cr.SetDataSource(ds);
                ReportView f = new ReportView();
                f.LinkReport(cr);
                f.Show();
            }
        }
    }
}
