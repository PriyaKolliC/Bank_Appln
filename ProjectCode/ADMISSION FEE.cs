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
    public partial class ADMISSION_FEE : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public ADMISSION_FEE()
        {
            InitializeComponent();
        }

       
        protected void fillgrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT * FROM admsson_fee", con))
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
            dateTimePicker1.Text = "";
            textBox1.Text = "";
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
                MessageBox.Show("* fields are mandatory");
            else
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO admsson_fee (Date,Amount) VALUES (@date,@amount)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text.ToString());
                        cmd.Parameters.AddWithValue("@amount", textBox1.Text);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                        refresh_details();
                        fillgrid();
                    }
                }

                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        MessageBox.Show("Date is unique!!Cannot contain duplicate values");
                    }
                }
            }
        }

        private void ADMISSION_FEE_Load(object sender, EventArgs e)
        {
            fillgrid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE admsson_fee SET Date=@date,Amount=@amount where Date='"+dateTimePicker1.Text+"'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text.ToString());
                    cmd.Parameters.AddWithValue("@amount", textBox1.Text);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Information updated successfully");
                    refresh_details();
                    fillgrid();
                }
                else if (result == DialogResult.No)
                { }
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedIndex = dataGridView1.SelectedCells[0].RowIndex;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM admsson_fee WHERE Date='" + dataGridView1.SelectedCells[selectedIndex].Value.ToString() + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Record deleted successfully");
                    fillgrid();
                    refresh_details();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                dateTimePicker1.Text = row.Cells["Date"].Value.ToString();
                textBox1.Text = row.Cells["Amount"].Value.ToString();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Amount", typeof(string));
            foreach (DataGridViewRow dgv in dataGridView1.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Admission_Fee.xml");
            admission_fee cr = new admission_fee();
            cr.SetDataSource(ds);
            ReportView f = new ReportView();
            f.LinkReport(cr);
            f.Show();
        }
    }
}
