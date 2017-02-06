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
using System.Globalization;
//using System.Drawing.Design;
namespace WindowsFormsApplication2
{
    public partial class findform : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
//        DataGridView row;
        DataGridViewRow row;
        public findform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("admn_date", typeof(string));
            dt.Columns.Add("index_no", typeof(string));
          dt.Columns.Add("status", typeof(string));
           dt.Columns.Add("ex-service", typeof(string));
            dt.Columns.Add("ph", typeof(string));
            dt.Columns.Add("fullcombo", typeof(string));
            dt.Columns.Add("fullname", typeof(string));
            dt.Columns.Add("fath/husb_name", typeof(string));
            dt.Columns.Add("dob", typeof(string));
            dt.Columns.Add("age", typeof(string));
            dt.Columns.Add("join_date", typeof(string));
            dt.Columns.Add("conf_date", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("phone", typeof(string));
            dt.Columns.Add("res_addr", typeof(string));
            dt.Columns.Add("bcode", typeof(string));
            dt.Columns.Add("bname", typeof(string));
            dt.Columns.Add("region", typeof(string));
            dt.Columns.Add("zone", typeof(string));
            dt.Columns.Add("state", typeof(string)); 
            dt.Columns.Add("accno", typeof(string)); 
            dt.Columns.Add("pfcode", typeof(string));
            dt.Columns.Add("hrms", typeof(string));
            dt.Columns.Add("dsgntn", typeof(string));
            dt.Columns.Add("bpay", typeof(string));
            dt.Columns.Add("nomname", typeof(string));
            dt.Columns.Add("relation", typeof(string));
            dt.Columns.Add("nomage", typeof(string));
            dt.Columns.Add("nomdob", typeof(string));
            dt.Columns.Add("guardian", typeof(string));
            dt.Columns.Add("remarks", typeof(string));
            dt.Columns.Add("sharespaid", typeof(string));
            dt.Columns.Add("sharesno", typeof(string));
            dt.Columns.Add("adminfee", typeof(string));
            dt.Columns.Add("mbf", typeof(string));
            dt.Columns.Add("share", typeof(string));
            dt.Columns.Add("mrf", typeof(string));
            dt.Columns.Add("total", typeof(string));
            //dt.Columns.Add("photo",typeof(Image));
            foreach (DataGridViewRow dgv in dataGridView1.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value, dgv.Cells[8].Value, dgv.Cells[9].Value, dgv.Cells[10].Value, dgv.Cells[11].Value, dgv.Cells[12].Value, dgv.Cells[13].Value, dgv.Cells[14].Value, dgv.Cells[15].Value, dgv.Cells[16].Value, dgv.Cells[17].Value, dgv.Cells[18].Value, dgv.Cells[19].Value, dgv.Cells[20].Value, dgv.Cells[21].Value, dgv.Cells[22].Value, dgv.Cells[23].Value, dgv.Cells[24].Value, dgv.Cells[25].Value, dgv.Cells[26].Value, dgv.Cells[27].Value, dgv.Cells[28].Value, dgv.Cells[29].Value, dgv.Cells[30].Value, dgv.Cells[31].Value, dgv.Cells[32].Value, dgv.Cells[33].Value, dgv.Cells[34].Value, dgv.Cells[35].Value, dgv.Cells[36].Value, dgv.Cells[37].Value);
                //dt.Rows.Add(dgv.Cells[0].Value,dgv.Cells[1].Value, dgv.Cells[2].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("member_details.xml");
            CrystalReport2 cr = new CrystalReport2();
            cr.SetDataSource(ds);
            ReportView f = new ReportView();
            f.LinkReport(cr);
            f.Show();
        }
        protected void fill_grid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM memberdetails order by indx", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dtn = new DataTable())
                        {
                            sda.Fill(dtn);
                            dataGridView1.DataSource = dtn;
                        }
                    }
                }
            }
        }

        private void findform_Load(object sender, EventArgs e)
        {
            
            fill_grid();
        }
        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                fill_grid();
            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM memberdetails where indx like '" + textBox1.Text + "' order by indx", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dtn = new DataTable())
                            {
                                sda.Fill(dtn);
                                dataGridView1.DataSource = dtn;
                            }
                        }
                    }
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM memberdetails where fullname like '" + textBox2.Text + "' order by indx", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dtn = new DataTable())
                        {
                            sda.Fill(dtn);
                            dataGridView1.DataSource = dtn;
                        }
                    }
                }
            }
        
        }
      
    

    
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM memberdetails where bcode like '" + textBox3.Text + "' and fullname like '"+textBox2.Text+"' order by indx", con))
                    {
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dtn = new DataTable())
                            {
                                sda.Fill(dtn);
                                dataGridView1.DataSource = dtn;
                            }
                        }
                    }
                }
            }
            else
            {
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM memberdetails where bcode like '" + textBox3.Text + "' order by indx", con))
                        {
                            cmd.CommandType = CommandType.Text;
                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                using (DataTable dtn = new DataTable())
                                {
                                    sda.Fill(dtn);
                                    dataGridView1.DataSource = dtn;
                                }
                            }
                        }
                    }
                }
        }
        

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM memberdetails where bname like '" + textBox1.Text + "' order by indx", con))
                {
                    cmd.CommandType = CommandType.Text;
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dtn = new DataTable())
                        {
                            sda.Fill(dtn);
                            dataGridView1.DataSource = dtn;
                        }
                    }
                }
            }        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //int i= dataGridView1.SelectedRows[0].Index;
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                SqlCommand cmd = new SqlCommand("delete from memberdetails where indx='"+textBox1.Text+"'");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                fill_grid();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
              row = this.dataGridView1.Rows[e.RowIndex];
            textBox1.Text = row.Cells["indx"].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string status = row.Cells["status"].Value.ToString();
                SqlCommand cmd = new SqlCommand("update memberdetails set status='"+status+"' where indx='"+textBox1.Text+"'");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record updated successfully");
                fill_grid();
            }
        }
    }
}
