using System;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;
namespace WindowsFormsApplication2
{
    public partial class MemberLedger : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        string admn_date,dob;
        string[] date = new string[10], month = new string[10];
        string[] mbf = new string[10], share = new string[10], mrf =new string[10], marf = new string[10];
        int mbf_total = 0,mrf_total=0,share_total=0,marf_total=0;
        DataGridViewCheckBoxColumn chkbx;
        public MemberLedger()
        {
            InitializeComponent();
        }

        private void MemberLedger_Load(object sender, EventArgs e)
        {
            chkbx = new DataGridViewCheckBoxColumn();
            chkbx.ValueType = typeof(bool);
            chkbx.Name = "Chk";
            chkbx.HeaderText = "chkbx";
            dataGridView1.Columns.Add(chkbx);
            fill_grid();
         }
        protected void refresh_details()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            fill_grid();
        }
        protected void fill_grid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT indx,fullname,bname FROM memberdetails order by indx", con))
                {
                    cmdn.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmdn))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                }
            }
     
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
               textBox1.Text = row.Cells["indx"].Value.ToString();
               textBox2.Text = row.Cells["fullname"].Value.ToString();
                 bool chkvalue = Convert.ToBoolean(row.Cells[0].Value);
                 if (!chkvalue)
                 {
                     using (SqlConnection con = new SqlConnection(connectionString))
                     {
                         SqlCommand cmd1 = new SqlCommand("select dofadm from memberdetails where indx='"+textBox1.Text+"' and fullname='"+textBox2.Text+"'");
                         SqlCommand cmd2 = new SqlCommand("select dob from memberdetails where indx='" + textBox1.Text + "' and fullname='" + textBox2.Text + "'");
                         SqlDataReader cmd1Rdr = null;
                         SqlDataReader cmd2Rdr = null;
                         con.Open();
                         cmd1.Connection = con;
                         cmd2.Connection = con;
                         cmd1Rdr = cmd1.ExecuteReader();
                         cmd2Rdr = cmd2.ExecuteReader();
                         while (cmd1Rdr.Read())
                         {
                             admn_date = cmd1Rdr["dofadm"].ToString();
                         }
                         while (cmd2Rdr.Read())
                         {
                             dob = cmd2Rdr["dob"].ToString();
                         }
                         DataSet ds = new DataSet();
                         DataTable dt = new DataTable();
                         dt.Columns.Add("indx", typeof(string));
                         dt.Columns.Add("fullname", typeof(string));
                         dt.Columns.Add("admn_date", typeof(string));
                         dt.Columns.Add("dob", typeof(string));
                         dt.Rows.Add(textBox1.Text, textBox2.Text, admn_date, dob); 
                             using (SqlCommand cmdn = new SqlCommand("SELECT * FROM subscription where indx='"+textBox1.Text+"'", con))
                             {
                                 cmdn.CommandType = CommandType.Text;
                                 using (SqlDataAdapter sda = new SqlDataAdapter(cmdn))
                                 {
                                     using (DataTable dt1 = new DataTable())
                                     {
                                         sda.Fill(dt1);
                                         dataGridView2.DataSource = dt1;
                                     }
                                 }
                             }
                         DataTable dt2 = new DataTable();
                         dt2.Columns.Add("date",typeof(DateTime));
                         dt2.Columns.Add("month", typeof(string));
                         dt2.Columns.Add("mbf", typeof(Int32));
                         dt2.Columns.Add("share", typeof(Int32));
                         dt2.Columns.Add("marf", typeof(Int32));
                         dt2.Columns.Add("mrf", typeof(Int32));
                         foreach (DataGridViewRow dgv in dataGridView2.Rows)
                         {
                             dt2.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value); 
                         }
                         foreach (DataGridViewRow dgv in dataGridView2.Rows)
                         {
                             marf_total = marf_total + Convert.ToInt32(dgv.Cells[6].Value);
                             mrf_total = mrf_total + Convert.ToInt32(dgv.Cells[7].Value);
                             mbf_total = mbf_total + Convert.ToInt32(dgv.Cells[4].Value);
                             share_total = share_total + Convert.ToInt32(dgv.Cells[5].Value);
                         }
                         DataTable dt3 = new DataTable();
                         dt3.Columns.Add("marf_total", typeof(Int32));
                         dt3.Columns.Add("mbf_total", typeof(Int32));
                         dt3.Columns.Add("mrf_total", typeof(Int32));
                         dt3.Columns.Add("share_total", typeof(Int32));
                         dt3.Rows.Add(marf_total, mbf_total, mrf_total, share_total);
                         ds.Tables.Add(dt);
                         ds.Tables.Add(dt2);
                         ds.Tables.Add(dt3);
                         ds.WriteXmlSchema("sub_report.xml");
                         subs_report cr = new subs_report();
                         cr.SetDataSource(ds);
                         ReportView f = new ReportView();
                         f.LinkReport(cr);
                         f.Show();
                     }
                 }
            } 
         }
            
        }
    }

