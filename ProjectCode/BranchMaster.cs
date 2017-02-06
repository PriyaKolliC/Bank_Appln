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
    public partial class BranchMaster : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public BranchMaster()
        {
            InitializeComponent();
        }

        private void BranchMaster_Load(object sender, EventArgs e)
        {
            FillCombobox3();
            grid_data();
            //fillgrid();
        }
        protected void fillgrid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT * FROM Branchmasterdetails", con))
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

        protected void FillCombobox3()
        {
            
            SqlConnection conn = new SqlConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select StateId,StateName from States order by StateName", conn);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                comboBox3.DisplayMember = "StateName";
                comboBox3.ValueMember = "StateId";
                comboBox3.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Exception Message
            }
            finally
            {
                conn.Close();
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stateID = Convert.ToInt32(comboBox3.SelectedValue.ToString());
                fill_districts(stateID);
        }
        private void fill_districts(int stateid)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                string x = stateid.ToString();
                SqlCommand cmd = new SqlCommand("select DistrictId,StateId,District from District where StateId=@id order by District", conn);
                cmd.Parameters.AddWithValue("@id", stateid);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                comboBox4.DisplayMember = "District";
                comboBox4.ValueMember = "DistrictId";
                comboBox4.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //Exception Message
            }
            finally
            {
                conn.Close();
            }
        }
        public void refresh_details()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE STUDENTS SET BranchCode=@BranchCode,[Branch Name]=@BranchName,Zone=@Zone,Region=@Region,Address1=@Address1,Address2=@Address2,Address3=@Address3,Pin=@Pin,State=@State,District=@District,[Phone No]=@PhoneNo,[Email Id]=@EmailId,PartCode=@PartCode");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BranchCode", textBox1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Branchname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Zone", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Region", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Address1", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Address2", richTextBox2.Text);
                    cmd.Parameters.AddWithValue("@Address3", richTextBox3.Text);
                    cmd.Parameters.AddWithValue("@Pin", textBox3.Text.ToString());
                    cmd.Parameters.AddWithValue("@State", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@District", comboBox4.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", textBox4.Text.ToString());
                    cmd.Parameters.AddWithValue("@EmailId", textBox5.Text);
                    cmd.Parameters.AddWithValue("@PartCode", textBox6.Text);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Information updated successfully");
                    refresh_details();
                }
                else if (result == DialogResult.No)
                { }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            if (textBox7.Text != "" && textBox8.Text != "")
            {
                SqlDataAdapter adapt = new SqlDataAdapter("select * from Branchmasterdetails where BranchCode='"+textBox7.Text+"' and [Branch Name]='"+textBox8.Text+"'", con);
                DataTable dt = new DataTable();
                adapt.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text == "" && textBox8.Text == "")
            {
                this.dataGridView1.DataSource = null;
                this.dataGridView1.Rows.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || comboBox2.Text == "")
            {
                MessageBox.Show("* Fileds are mandatory");
            }
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Branchmasterdetails (BranchCode,BranchName,Zone,Region,Address1,Address2,Address3,Pin,State,District,[Phone No],[Email Id],PartCode) VALUES (@BranchCode,@BranchName,@Zone,@Region,@Address1,@Address2,@Address3,@Pin,@State,@District,@PhoneNo,@EmailId,@PartCode)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@BranchCode", textBox1.Text.ToString());
                    cmd.Parameters.AddWithValue("@Branchname", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Zone", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@Region", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@Address1", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@Address2", richTextBox2.Text);
                    cmd.Parameters.AddWithValue("@Address3", richTextBox3.Text);
                    cmd.Parameters.AddWithValue("@Pin", textBox3.Text.ToString());
                    cmd.Parameters.AddWithValue("@State", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@District", comboBox4.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", textBox4.Text.ToString());
                    cmd.Parameters.AddWithValue("@EmailId", textBox5.Text);
                    cmd.Parameters.AddWithValue("@PartCode", textBox6.Text);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information saved successfully");
                    refresh_details();

                }

            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells[0].Value.ToString();
                textBox2.Text = row.Cells[1].Value.ToString();
                comboBox1.Text = row.Cells[2].Value.ToString();
                comboBox2.Text = row.Cells["Region"].Value.ToString();
                richTextBox1.Text = row.Cells["Address1"].Value.ToString();
                richTextBox2.Text = row.Cells["Address2"].Value.ToString();
                richTextBox3.Text = row.Cells["Address3"].Value.ToString();
                textBox3.Text = row.Cells["Pin"].Value.ToString();
                comboBox3.Text = row.Cells["State"].Value.ToString();
                comboBox4.Text = row.Cells["District"].Value.ToString();
                textBox4.Text = row.Cells["Phone No"].Value.ToString();
                textBox5.Text = row.Cells["Email Id"].Value.ToString();
                textBox6.Text = row.Cells["PartCode"].Value.ToString();
            }
        }
        protected void grid_data()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Branchmasterdetails", con))
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
        private void button5_Click(object sender, EventArgs e)
        {
            grid_data();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.Add("BranchCode", typeof(string));
            dt.Columns.Add("Branch Name", typeof(string));
            dt.Columns.Add("Zone", typeof(string));
            dt.Columns.Add("Region", typeof(string));
            dt.Columns.Add("Address1", typeof(string));
            dt.Columns.Add("Address2", typeof(string));
            dt.Columns.Add("Address3", typeof(string));
            dt.Columns.Add("Pin", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("District", typeof(string));
            dt.Columns.Add("Phone No", typeof(string));
            dt.Columns.Add("Email Id", typeof(string));
            dt.Columns.Add("PartCode", typeof(string));
            foreach (DataGridViewRow dgv in dataGridView1.Rows)
            {
                dt.Rows.Add(dgv.Cells[0].Value, dgv.Cells[1].Value, dgv.Cells[2].Value, dgv.Cells[3].Value, dgv.Cells[4].Value, dgv.Cells[5].Value, dgv.Cells[6].Value, dgv.Cells[7].Value, dgv.Cells[8].Value, dgv.Cells[9].Value, dgv.Cells[10].Value, dgv.Cells[11].Value, dgv.Cells[12].Value);
            }
            ds.Tables.Add(dt);
            ds.WriteXmlSchema("Sample.xml");
            CrystalReport1 cr = new CrystalReport1();
            cr.SetDataSource(ds);
            ReportView f = new ReportView();
            f.LinkReport(cr);
            f.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Branchmasterdetails WHERE BranchCode='" + textBox1.Text + "'");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Record deleted successfully");
                grid_data();
                refresh_details();
            }
         }
}
}
