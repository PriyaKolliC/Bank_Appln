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
    public partial class MBF_ELIGIBILITY : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public MBF_ELIGIBILITY()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                MessageBox.Show("All fields are compulsory");
            else
            {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                {
            SqlCommand cmd = new SqlCommand("INSERT INTO mbf_elg (Date,Times,FunExp,MinAmt,AccdntlBnft,MaxAmt) values(@date,@times,@fe,@minA,@accB,@maxA)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToShortDateString());
                    cmd.Parameters.AddWithValue("@times", Convert.ToInt32(textBox1.Text));
                    cmd.Parameters.AddWithValue("@fe",Convert.ToInt32( textBox2.Text));
                    cmd.Parameters.AddWithValue("@minA", Convert.ToInt32(textBox3.Text));
                    cmd.Parameters.AddWithValue("@accB", Convert.ToInt32(textBox4.Text));
                    cmd.Parameters.AddWithValue("@maxA", Convert.ToInt32(textBox5.Text));
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    }
                    fill_grid();
            }
        }
        protected void fill_grid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT * FROM mbf_elg", con))
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

        private void MBF_ELIGIBILITY_Load(object sender, EventArgs e)
        {
            fill_grid();
        }
    }
}
