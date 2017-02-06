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
    public partial class LoanApplication : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        string check = "";
        int y = 0, z = 0;
        public LoanApplication()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
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
            comboBox1.Text = "--index";
            comboBox2.Text = "--type";
            textBox8.Text = "";
            textBox9.Text = "";
        }

        private void LoanApplication_Load(object sender, EventArgs e)
        {
            fill_combo();
            refresh_details();
            fill_applnNo();
            fill_grid();
        }
        protected void fill_applnNo()
        {
            string x = "SL";
            using (SqlConnection conc = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("select max(substring(appln_no,3,6)) as appln_no from loan_appln");
                    /*where fullname like '" + textBox3.Text.Substring(0, 1) + "%'*/
                SqlDataReader cmdRdr = null;
                conc.Open();
                cmd.Connection = conc;
                cmdRdr = cmd.ExecuteReader();
                while (cmdRdr.Read())
                {
                    textBox1.Text = cmdRdr["appln_no"].ToString();
                }
                if (textBox1.Text == "" || textBox1.Text == null)
                    textBox1.Text = x + "0001";
                else
                {
                    z = Convert.ToInt16(textBox1.Text.Substring(2));
                    if (z > 9 && z < 99)
                    {
                        z = z + 1;
                        textBox1.Text = x + "00" + z.ToString();
                    }
                    else if (z <= 9)
                    {
                         y = z + 1;
                        textBox1.Text = x + "000" + y.ToString();
                    }
                }
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
                comboBox1.Text = "--index";
                comboBox1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_names();
            fill_contri();
        }
        protected void fill_names()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string item = comboBox1.SelectedValue.ToString();
                if (comboBox1.SelectedValue.ToString() == "--index")
                {
                }
                else
                {
                    SqlCommand cmdnew = new SqlCommand("SELECT fullname FROM memberdetails WHERE indx='" + item + "'");
                    SqlCommand cmdnew1 = new SqlCommand("SELECT bname FROM memberdetails WHERE indx='" + item + "'");
                    SqlDataReader fullnameRdr = null;
                    SqlDataReader bnameRdr = null;
                    conn.Open();
                    cmdnew.Connection = conn;
                    cmdnew1.Connection = conn;
                    fullnameRdr = cmdnew.ExecuteReader();
                    bnameRdr = cmdnew1.ExecuteReader();
                    while (fullnameRdr.Read())
                    {
                        textBox8.Text = fullnameRdr["fullname"].ToString();
                    }
                    while (bnameRdr.Read())
                    {
                        textBox9.Text = bnameRdr["bname"].ToString();
                    }
                }
            }
        }
        protected void fill_contri()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string item = comboBox1.SelectedValue.ToString();
                if (comboBox1.SelectedValue.ToString() == "--index")
                {
                }
                else
                {
                    SqlCommand cmdnew = new SqlCommand("SELECT marf FROM memberdetails WHERE indx='" + item + "'");
                    SqlCommand cmdnew1 = new SqlCommand("SELECT mbf FROM memberdetails WHERE indx='" + item + "'");
                    SqlCommand cmdnew2 = new SqlCommand("SELECT amount FROM mrf_contri WHERE date<='" + dateTimePicker1.Text + "' order by date DESC");
                    SqlCommand cmdnew3 = new SqlCommand("SELECT share FROM memberdetails WHERE indx='" + item + "'");
                    SqlDataReader marfRdr = null;
                    SqlDataReader mrfRdr = null;
                    SqlDataReader mbfRdr = null;
                    SqlDataReader shareRdr = null;
                    conn.Open();
                    cmdnew.Connection = conn;
                    cmdnew1.Connection = conn;
                    cmdnew2.Connection = conn;
                    cmdnew3.Connection = conn;
                    marfRdr = cmdnew.ExecuteReader();
                    mrfRdr = cmdnew2.ExecuteReader();
                    mbfRdr = cmdnew1.ExecuteReader();
                    shareRdr = cmdnew3.ExecuteReader();
                    while (marfRdr.Read())
                    {
                        textBox4.Text = marfRdr["marf"].ToString();
                    }
                    while (mrfRdr.Read())
                    {
                        textBox5.Text = mrfRdr["amount"].ToString();
                    }
                    while (mbfRdr.Read())
                    {
                        textBox7.Text = mbfRdr["mbf"].ToString();
                    }
                    while (shareRdr.Read())
                    {
                        textBox6.Text = shareRdr["share"].ToString();
                    }

                }
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
                MessageBox.Show("All fields are compulsory");
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand c = new SqlCommand("select name from loan_appln where appln_no='" + textBox1.Text.ToString() + "'");
                    SqlDataReader cRdr = null;
                    connection.Open();
                    c.Connection = connection;
                    cRdr = c.ExecuteReader();
                    while (cRdr.Read())
                    {
                        check = cRdr["name"].ToString();
                    }
                    if (check != "")
                    {
                        MessageBox.Show("record for same appln_no exists");
                        check = "";
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO loan_appln (appln_no,DateOfAppln,IndexNo,Type,LoanAmount,Purpose,MARF,MRF,MBF,SHARE,Name,Branch) VALUES (@apn,@doa,@ino,@t,@la,@pp,@MARF,@MRF,@MBF,@SHARE,@Name,@Branch)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = connection;
                        cmd.Parameters.AddWithValue("@apn",textBox1.Text);
                        cmd.Parameters.AddWithValue("@doa", dateTimePicker1.Text.ToString());
                        cmd.Parameters.AddWithValue("@ino", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@t", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@la", textBox2.Text);
                        cmd.Parameters.AddWithValue("@pp", textBox3.Text);
                        cmd.Parameters.AddWithValue("@MARF", Convert.ToInt32(textBox4.Text));
                        cmd.Parameters.AddWithValue("@MRF", Convert.ToInt32(textBox5.Text));
                        cmd.Parameters.AddWithValue("@MBF", Convert.ToInt32(textBox7.Text));
                        cmd.Parameters.AddWithValue("@SHARE", Convert.ToInt32(textBox6.Text));
                        cmd.Parameters.AddWithValue("@Name", textBox8.Text);
                        cmd.Parameters.AddWithValue("@Branch", textBox9.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                        connection.Close();
                        refresh_details();
                        fill_grid();
                        fill_applnNo();
                    }
                }
            }

        }
        protected void fill_grid()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmdn = new SqlCommand("SELECT * FROM loan_appln", con))
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

        private void button5_Click(object sender, EventArgs e)
        {
            LoanVerification lv = new LoanVerification();
            lv.Show();
        }
    }
}
