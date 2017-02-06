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
using System.IO;
using System.Data.OleDb;

namespace WindowsFormsApplication2
{
    public partial class subscriptions : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        string check = "";
        int z = 0, mrf_temp = 0,i;
        bool import = false;
        private string Excel07ConString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        private string Excel03ConString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
        OpenFileDialog fd = new OpenFileDialog();
        DataGridViewRow row;
        public subscriptions()
        {
            InitializeComponent();
        }

        private void subscriptions_Load(object sender, EventArgs e)
        {
            fill_combo();
            fill_grid();
            refresh_details();
            fill_combo2();
        }
        private string GetConnectionString()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = "C:\\MyExcel.xlsx";
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }
            return sb.ToString();
        }
        protected void fill_combo2()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select paytpe from paymodes";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                con.Close();

                comboBox2.DisplayMember = "paytpe";
                comboBox2.ValueMember = "paytpe";
                comboBox2.DataSource = ds.Tables[0];
                comboBox2.Text = "--Select paymode";
                comboBox2.Enabled = true;
            }
        }
        protected void fill_combo()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select indx from memberdetails order by indx";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                con.Close();

                comboBox1.DisplayMember = "indx";
                comboBox1.ValueMember = "indx";
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.Text = "--Select index";
                comboBox1.Enabled = true;
                refresh_details();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_name();
            fill_fees();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmdn = new SqlCommand("SELECT total_share FROM subscription where indx='" + comboBox1.SelectedValue.ToString() + "' order by total_share");
                SqlDataReader totalshareRdr = null;
                con.Open();
                cmdn.Connection = con;
                totalshareRdr = cmdn.ExecuteReader();
                while (totalshareRdr.Read())
                {
                    z = Convert.ToInt16(totalshareRdr["total_share"]);
                }
                textBox7.Text = z.ToString();
            }
        }
        protected void fill_name()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string item = comboBox1.SelectedValue.ToString();
                if (comboBox1.SelectedValue.ToString() == "--Select index")
                {
                }
                else
                {
                    SqlCommand cmdnew = new SqlCommand("SELECT fullname FROM memberdetails WHERE indx='" + item + "'");
                    SqlDataReader fullnameRdr = null;
                    conn.Open();
                    cmdnew.Connection = conn;
                    fullnameRdr = cmdnew.ExecuteReader();
                    while (fullnameRdr.Read())
                    {
                        textBox1.Text = fullnameRdr["fullname"].ToString();
                    }
                }
            }
        }
        protected void fill_fees()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string item = comboBox1.SelectedValue.ToString();
                conn.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT mbf FROM memberdetails WHERE indx='" + item + "'");
                SqlDataReader mbfRdr = null;
                cmd1.Connection = conn;
                mbfRdr = cmd1.ExecuteReader();
                while (mbfRdr.Read())
                {
                    textBox2.Text = mbfRdr["mbf"].ToString();
                }
                SqlCommand cmd2 = new SqlCommand("SELECT marf FROM memberdetails WHERE indx='" + item + "'");
                SqlDataReader marfRdr = null;
                cmd2.Connection = conn;
                marfRdr = cmd2.ExecuteReader();
                while (marfRdr.Read())
                {
                    textBox4.Text = marfRdr["marf"].ToString();
                }
                SqlCommand cmd3 = new SqlCommand("SELECT share FROM memberdetails WHERE indx='" + item + "'");
                SqlDataReader shareRdr = null;
                cmd3.Connection = conn;
                shareRdr = cmd3.ExecuteReader();
                while (shareRdr.Read())
                {
                    textBox3.Text = shareRdr["share"].ToString();
                }
                total_fee();
            }
        }
        protected void total_fee()
        {
            int x;
            if (textBox5.Text == "")
            {
                x = Convert.ToInt16(textBox2.Text) + Convert.ToInt16(textBox3.Text) + Convert.ToInt16(textBox4.Text);
            }
            else
            {
                x = Convert.ToInt16(textBox2.Text) + Convert.ToInt16(textBox3.Text) + Convert.ToInt16(textBox4.Text) + Convert.ToInt16(textBox5.Text);
            }
            textBox6.Text = x.ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            total_fee();
        }

        protected void share_check()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmdn = new SqlCommand("SELECT total_share FROM subscription where indx='" + comboBox1.SelectedValue.ToString() + "' order by total_share");
                SqlDataReader totalshareRdr = null;
                con.Open();
                cmdn.Connection = con;
                totalshareRdr = cmdn.ExecuteReader();

                while (totalshareRdr.Read())
                {
                    z = Convert.ToInt16(totalshareRdr["total_share"]);
                }
                if (textBox3.Text == "")
                {
                    textBox7.Text = "";
                }
                else
                {
                    textBox7.Text = (Convert.ToInt16(textBox3.Text) + z).ToString();
                    if (Convert.ToInt16(textBox7.Text) >= 4500)
                    {
                        label8.Text = "MRF *";
                        mrf_temp = 1;
                    }
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            refresh_details();
        }
        protected void refresh_details()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            comboBox1.Text = "--Select index";
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
        }
        protected void fill_grid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT * FROM subscription", con))
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

        private void button1_Click(object sender, EventArgs e)
        {
                using (SqlConnection co = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("select name from subscription where month='" + dateTimePicker2.Value.Month.ToString() + "' and indx='" + comboBox1.SelectedValue.ToString() + "'");
                    SqlDataReader cmdRdr = null;
                    co.Open();
                    cmd.Connection = co;
                    cmdRdr = cmd.ExecuteReader();
                    while (cmdRdr.Read())
                    {
                        check = cmdRdr["name"].ToString();
                    }
                }
                if (check != "")
                {
                    MessageBox.Show("record already exists");
                    check = "";
                }
                else
                {
                    if (mrf_temp == 1 && textBox5.Text == "")
                    {
                        MessageBox.Show("Now MRF is compulsory as share value is greater than 4500");
                        textBox3.ReadOnly = true;
                    }
                    else if (mrf_temp == 0)
                        insertion();
                    else if (mrf_temp == 1 && textBox5.Text != "")
                    {
                        insertion();
                        textBox3.ReadOnly = true;
                    }
                }
            }
        

        private void button4_Click(object sender, EventArgs e)
        {
            refresh_details();
        }
        protected void insertion()
        {
            if (String.Equals(comboBox2.SelectedValue.ToString(), "cheque", StringComparison.Ordinal) || String.Equals(comboBox2.SelectedValue.ToString(), "dd", StringComparison.Ordinal))
            {
                if (textBox8.Text == "")
                {
                    MessageBox.Show("Enter cheque or dd number");
                }
                else
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO subscription (date,month,indx,name,mbf,share,marf,mrf,total,paymode,chq_dd,total_share) VALUES (@date,@month,@indx,@name,@mbf,@share,@marf,@mrf,@total,@paymode,@chq,@totalshare)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@date", dateTimePicker1.Text);
                        cmd.Parameters.AddWithValue("@month", dateTimePicker2.Value.Month.ToString());
                        cmd.Parameters.AddWithValue("@indx", comboBox1.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@name", textBox1.Text);
                        cmd.Parameters.AddWithValue("@mbf", textBox2.Text);
                        cmd.Parameters.AddWithValue("@share", textBox3.Text);
                        cmd.Parameters.AddWithValue("@marf", textBox4.Text);
                        cmd.Parameters.AddWithValue("@mrf", textBox5.Text);
                        cmd.Parameters.AddWithValue("@total", textBox6.Text);
                        cmd.Parameters.AddWithValue("@totalshare", textBox7.Text);
                        cmd.Parameters.AddWithValue("@paymode", comboBox2.SelectedValue);
                        cmd.Parameters.AddWithValue("@chq", textBox8.Text);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                        fill_grid();
                        share_check();
                    }
                }
            }
        }
    

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text == "")
                fill_grid();
            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM subscription where indx like '" + textBox9.Text + "' order by indx", con))
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

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
                fill_grid();
            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM subscription where name like '" + textBox10.Text + "' order by indx", con))
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

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("delete from subscription where indx='" + comboBox1.Text + "' and month='" + row.Cells[1].Value.ToString() + "'");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Record deleted successfully");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            row = this.dataGridView1.Rows[e.RowIndex];
            comboBox1.Text = row.Cells["indx"].Value.ToString();
            textBox1.Text = row.Cells["name"].Value.ToString();
            //dateTimePicker2.Value.Month=Convert.ToInt32(row.Cells["month"].Value);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("update subscription where indx='" + comboBox1.Text + "'");
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                fill_grid();
                MessageBox.Show("Record updated successfully");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string filePath = fd.FileName;
                string extension = Path.GetExtension(filePath);
                string conStr, sheetName;
                textBox11.Text = filePath;
                conStr = string.Empty;
                switch (extension)
                {

                    case ".xls": //Excel 97-03
                        conStr = string.Format(Excel03ConString, filePath);
                        break;

                    case ".xlsx": //Excel 07
                        conStr = string.Format(Excel07ConString, filePath);
                        break;
                }
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.Connection = con;
                        con.Open();
                        DataTable dtExcelSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        con.Close();
                    }
                }
                using (OleDbConnection con = new OleDbConnection(conStr))
                {
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        using (OleDbDataAdapter oda = new OleDbDataAdapter())
                        {
                            DataTable dt = new DataTable();
                            cmd.CommandText = "SELECT * From [" + sheetName + "]";
                            cmd.Connection = con;
                            con.Open();
                            oda.SelectCommand = cmd;
                            oda.Fill(dt);
                            con.Close();
                            dataGridView1.DataSource = dt;
                            import = true;
                        }
                    }
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (import == true)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        SqlCommand cmd = new SqlCommand("select name from subscription where month='" + dataGridView1.Rows[i].Cells["month"].Value.ToString() + "' and indx='" + dataGridView1.Rows[i].Cells["indx"].Value.ToString() + "'");
                        SqlDataReader cmdRdr = null;
                        cmd.Connection = con;
                        cmdRdr = cmd.ExecuteReader();
                        while (cmdRdr.Read())
                        {
                            check = cmdRdr["name"].ToString();
                        }
                        MessageBox.Show("index is " + dataGridView1.Rows[i].Cells["indx"].Value.ToString() + " and month is " + dataGridView1.Rows[i].Cells["month"].Value.ToString() + "");
                        if (check != "")
                        {
                            dataGridView1.Rows[i].Cells["validated"].Value = "no";
                            MessageBox.Show("record already exists");
                            check = "";
                        }
                        else
                        {
                            if (Convert.ToInt32(dataGridView1.Rows[i].Cells["total_share"].Value) >= 4500 && dataGridView1.Rows[i].Cells["mrf"].Value.ToString() == "")
                            {
                                //MessageBox.Show("As total_share till date >=4500 mrf is compulsory for index "+dataGridView1.Rows[i].Cells["indx"].Value.ToString()+" at month "+dataGridView1.Rows[i].Cells["month"].Value.ToString()+"");
                                dataGridView1.Rows[i].Cells["validated"].Value = "no";
                                continue;
                            }
                            else if (String.Equals(dataGridView1.Rows[i].Cells["paymode"].Value.ToString(), "cheque", StringComparison.Ordinal) || String.Equals(dataGridView1.Rows[i].Cells["paymode"].Value.ToString(), "dd", StringComparison.Ordinal))
                            {
                                if (dataGridView1.Rows[i].Cells["chq_dd"].Value.ToString() == "")
                                {
                                    //    MessageBox.Show("Cheque or dd number not provided for index " + dataGridView1.Rows[i].Cells["indx"].Value.ToString() + " at month " + dataGridView1.Rows[i].Cells["month"].Value.ToString() + "");
                                    dataGridView1.Rows[i].Cells["validated"].Value = "no";
                                    continue;
                                }
                            }
                            dataGridView1.Rows[i].Cells["validated"].Value = "yes";
                        }
                        valid_insertion();
                    }
                    MessageBox.Show("Information saved successfully");
                }
            }
        }
        protected void valid_insertion()
        {
            using (SqlConnection connction = new SqlConnection(connectionString))
            {
                connction.Open();
                    if (String.Equals(dataGridView1.Rows[i].Cells["validated"].Value.ToString(), "yes", StringComparison.Ordinal))
                    {
                        SqlCommand cmd1 = new SqlCommand("INSERT INTO subscription (date,month,indx,name,mbf,share,marf,mrf,total,paymode,chq_dd,total_share) VALUES (@date,@month,@indx,@name,@mbf,@share,@marf,@mrf,@total,@paymode,@chq,@total_share)");
                        cmd1.CommandType = CommandType.Text;
                        cmd1.Connection = connction;
                        cmd1.Parameters.AddWithValue("@date", Convert.ToDateTime(dataGridView1.Rows[i].Cells["date"].Value));
                        cmd1.Parameters.AddWithValue("@month", dataGridView1.Rows[i].Cells["month"].Value.ToString());
                        cmd1.Parameters.AddWithValue("@indx", dataGridView1.Rows[i].Cells["indx"].Value.ToString());
                        cmd1.Parameters.AddWithValue("@name", dataGridView1.Rows[i].Cells["name"].Value.ToString());
                        cmd1.Parameters.AddWithValue("@mbf", Convert.ToInt32(dataGridView1.Rows[i].Cells["mbf"].Value));
                        cmd1.Parameters.AddWithValue("@share", Convert.ToInt32(dataGridView1.Rows[i].Cells["share"].Value));
                        cmd1.Parameters.AddWithValue("@marf", Convert.ToInt32(dataGridView1.Rows[i].Cells["marf"].Value));
                        cmd1.Parameters.AddWithValue("@mrf", Convert.ToInt32(dataGridView1.Rows[i].Cells["mrf"].Value));
                        cmd1.Parameters.AddWithValue("@total", Convert.ToInt32(dataGridView1.Rows[i].Cells["total"].Value));
                        cmd1.Parameters.AddWithValue("@total_share", Convert.ToInt32(dataGridView1.Rows[i].Cells["total_share"].Value));
                        cmd1.Parameters.AddWithValue("@paymode", dataGridView1.Rows[i].Cells["paymode"].Value.ToString());
                        cmd1.Parameters.AddWithValue("@chq", dataGridView1.Rows[i].Cells["chq_dd"].Value.ToString());
                        cmd1.ExecuteNonQuery();
                    }
            }
        }
    }
}

