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
    public partial class IssuedLoanDetails : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public IssuedLoanDetails()
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
                DataTable d = new DataTable();
                d.Columns.Add("StartDate", typeof(string));
                d.Columns.Add("EndDate", typeof(string));
                d.Rows.Add(dateTimePicker1.Value.ToShortDateString(), dateTimePicker2.Value.ToShortDateString());
                ds.Tables.Add(d);
                string item = "Loan Granted";
                SqlCommand cmd = new SqlCommand("SELECT appln_no,IndexNo,Name,Branch,ElgAmount,NOI,EMI,MOP,Cheque_DD_No,CONVERT(VARCHAR(10),IssueDate ,101) as IssueDate FROM granted_loans where status='" + item + "'");
                conn.Open();
                cmd.Connection = conn;
                dt.Load(cmd.ExecuteReader());
                ds.Tables.Add(dt);
                ds.WriteXmlSchema("issued_loan_details.xml");
                conn.Close();
                IssuedLoanDetls cr = new IssuedLoanDetls();
                cr.SetDataSource(ds);
                ReportView f = new ReportView();
                f.LinkReport(cr);
                f.Show();
            }
        }

        private void IssuedLoanDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
