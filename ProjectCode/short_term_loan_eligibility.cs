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
    public partial class short_term_loan_eligibility : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        DataGridViewRow row;
        string check = "";
        public short_term_loan_eligibility()
        {
            InitializeComponent();
        }

        private void short_term_loan_eligibility_Load(object sender, EventArgs e)
        {
            refresh_details();
            //fill_grid();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected void fill_grid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT * FROM st_elg", con))
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
        protected void refresh_details()
        {
            dateTimePicker1.Value = DateTime.Today;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            fill_grid();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            refresh_details();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
                MessageBox.Show("All fields are compulsory");
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand c = new SqlCommand("select date from st_elg where date='" + dateTimePicker1.Text.ToString() + "'");
                    SqlDataReader cRdr = null;
                    connection.Open();
                    c.Connection = connection;
                    cRdr = c.ExecuteReader();
                    while (cRdr.Read())
                    {
                        check = cRdr["date"].ToString();
                    }
                    if (check != "")
                    {
                        MessageBox.Show("record for same date exists");
                        check = "";
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO st_elg (Date,InstlmntNo,MinMmbrshpPrd,MaxLimit,ROI,RnwlPrd,MinShare,TimesOfBasic) VALUES (@d,@ino,@mmp,@ml,@roi,@rp,@ms,@tob)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@d", dateTimePicker1.Text.ToString());
                        cmd.Parameters.AddWithValue("@ino", Convert.ToInt32(textBox2.Text));
                        cmd.Parameters.AddWithValue("@mmp", Convert.ToInt32(textBox3.Text));
                        cmd.Parameters.AddWithValue("@ml", Convert.ToInt32(textBox1.Text));
                        cmd.Parameters.AddWithValue("@roi", Convert.ToInt32(textBox4.Text));
                        cmd.Parameters.AddWithValue("@rp", Convert.ToInt32(textBox5.Text));
                        cmd.Parameters.AddWithValue("@ms", Convert.ToInt32(textBox6.Text));
                        cmd.Parameters.AddWithValue("@tob", Convert.ToInt32(textBox7.Text));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                        connection.Close();
                        refresh_details();
                        fill_grid();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row = this.dataGridView1.Rows[e.RowIndex];
            dateTimePicker1.Text=row.Cells["Date"].Value.ToString();
            textBox1.Text = row.Cells["MaxLimit"].Value.ToString();
            textBox2.Text = row.Cells["InstlmntNo"].Value.ToString();
            textBox3.Text = row.Cells["MinMmbrshpPrd"].Value.ToString();
            textBox4.Text = row.Cells["ROI"].Value.ToString();
            textBox5.Text = row.Cells["RnwlPrd"].Value.ToString();
            textBox6.Text = row.Cells["MinShare"].Value.ToString();
            textBox7.Text = row.Cells["TimesOfBasic"].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE st_elg SET Date=@d,InstlmntNo=@ino,MinMmbrshpPrd=@mmp,MaxLimit=@ml,ROI=@roi,RnwlPrd=@rp,MinShare=@ms,TimesOfBasic=@tob where Date='" + dateTimePicker1.Text + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.Parameters.AddWithValue("@d", dateTimePicker1.Text.ToString());
                    cmd.Parameters.AddWithValue("@ino", Convert.ToInt32(textBox2.Text));
                    cmd.Parameters.AddWithValue("@mmp", Convert.ToInt32(textBox3.Text));
                    cmd.Parameters.AddWithValue("@ml", Convert.ToInt32(textBox1.Text));
                    cmd.Parameters.AddWithValue("@roi", Convert.ToInt32(textBox4.Text));
                    cmd.Parameters.AddWithValue("@rp", Convert.ToInt32(textBox5.Text));
                    cmd.Parameters.AddWithValue("@ms", Convert.ToInt32(textBox6.Text));
                    cmd.Parameters.AddWithValue("@tob", Convert.ToInt32(textBox7.Text));
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Information updated successfully");
                    refresh_details();
                    fill_grid();
                }
                else if (result == DialogResult.No)
                { }
            }
        
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("Select Record to delete");
            else
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM st_elg WHERE Date='" + dateTimePicker1.Text + "'");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show("Record deleted successfully");
                        fill_grid();
                        refresh_details();
                    }
                }
            }
        }
    }
}
