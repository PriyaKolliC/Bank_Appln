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
    public partial class LoanApplicationReport : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public LoanApplicationReport()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dateTimePicker1.Text) > Convert.ToDateTime(dateTimePicker2.Text))
            {
                MessageBox.Show("Start date cannot be greater than end date");
            }
            else if (Convert.ToDateTime(dateTimePicker1.Text) < Convert.ToDateTime(dateTimePicker2.Text) && comboBox1.Text != "")
            {
                MessageBox.Show("select only one parameter at a time");
            }
            else if (Convert.ToDateTime(dateTimePicker1.Text) < Convert.ToDateTime(dateTimePicker2.Text) && comboBox2.Text != "")
            {
                MessageBox.Show("select only one parameter at a time");
            }
            else if (Convert.ToDateTime(dateTimePicker1.Text) == Convert.ToDateTime(dateTimePicker2.Text))
            {
                //MessageBox.Show("select only one parameter at a time");
                if (comboBox1.Text == "" && comboBox2.Text != "")
                {
                    MessageBox.Show("status filter");
                    filter_byStatus();
                }
                else if (comboBox1.Text != "" && comboBox2.Text == "")
                {
                    MessageBox.Show("type filter");
                    filter_byType();
                }
                else if (comboBox1.Text == "" && comboBox2.Text == "")
                {
                    MessageBox.Show("date filter");
                    filter_byDate();
                }
                else
                    MessageBox.Show("select only one parameter to filter");
            }
            else
            {
                filter_byDate();
            }
        }
        protected void filter_byType()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
             DataTable dt = new DataTable();
             DataSet ds = new DataSet();
             string query = "SELECT appln_no,  CONVERT(VARCHAR(10),DateOfAppln ,101) as DateOfAppln,IndexNo,Name,Branch,LoanAmount,NOI,EMI,CONVERT(VARCHAR(10),IssueDate ,101) as IssueDate,Status,type FROM granted_loans where type='" + comboBox1.Text + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                dt.Load(cmd.ExecuteReader());
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("typefilter.xml");
                conn.Close();
         CrystalReport3 cr = new CrystalReport3();
                cr.SetDataSource(ds);
                ReportView f = new ReportView();
                f.LinkReport(cr);
                f.Show();
            }
        }
        protected void filter_byStatus()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                string query = "SELECT appln_no,CONVERT(VARCHAR(10),DateOfAppln ,101) as DateOfAppln,IndexNo,Name,Branch,LoanAmount,NOI,EMI,CONVERT(VARCHAR(10),IssueDate ,101) as IssueDate,Status,type FROM granted_loans where status='" + comboBox2.Text + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                dt.Load(cmd.ExecuteReader());
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("statusfilter.xml");
                conn.Close();
                CrystalReport4 cr = new CrystalReport4();
                cr.SetDataSource(ds);
                ReportView f = new ReportView();
                f.LinkReport(cr);
                f.Show();
            }
        }
        protected void filter_byDate()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DataTable dt = new DataTable();
                DataTable d = new DataTable();
                d.Columns.Add("StartDate",typeof(string));
                d.Columns.Add("EndDate",typeof(string));
                d.Rows.Add(dateTimePicker1.Value.ToShortDateString(),dateTimePicker2.Value.ToShortDateString());
                DataSet ds = new DataSet();
                ds.Tables.Add(d);
                string query = "SELECT appln_no,CONVERT(VARCHAR(10),DateOfAppln ,101) as DateOfAppln,IndexNo,Name,Branch,LoanAmount,NOI,EMI,CONVERT(VARCHAR(10),IssueDate ,101) as IssueDate,Status,type FROM granted_loans where DateOfAppln>='" + Convert.ToDateTime(dateTimePicker1.Text) + "' and DateOfAppln<='" + Convert.ToDateTime(dateTimePicker2.Text) + "'";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                dt.Load(cmd.ExecuteReader());
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("datefilter.xml");
                conn.Close();
                CrystalReport5 cr = new CrystalReport5();
                cr.SetDataSource(ds);
                ReportView f = new ReportView();
                f.LinkReport(cr);
                f.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh_details();
        }
        protected void refresh_details()
        {
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
            comboBox1.Text = "";
            comboBox2.Text = "";        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoanApplicationReport_Load(object sender, EventArgs e)
        {
            refresh_details();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}
