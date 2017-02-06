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
    public partial class St_loanIssue : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        int p = 0,n=0;
        double a=0,r=0;
        string R,check="";
        public St_loanIssue()
        {
            InitializeComponent();
        }

        private void St_loanIssue_Load(object sender, EventArgs e)
        {
            fill_combo();
            fill_details();
        }
        protected void fill_combo()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string item="Loan Granted";
                string query = "select appln_no from Gloan_det where status='"+item+"' order by appln_no";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                con.Close();
                comboBox1.DisplayMember = "appln_no";
                comboBox1.ValueMember = "appln_no";
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.Text = "--appln";
                comboBox1.Enabled = true;
            }
        }

        protected void fill_details()
        {
            if (String.Equals(comboBox1.Text, "--appln", StringComparison.Ordinal))
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
                textBox10.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string item = comboBox1.SelectedValue.ToString();
                    SqlCommand cmdnew = new SqlCommand("SELECT Name FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew1 = new SqlCommand("SELECT IndexNo FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew2 = new SqlCommand("SELECT Branch FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew3 = new SqlCommand("SELECT Type FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew4 = new SqlCommand("SELECT Purpose FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew5 = new SqlCommand("SELECT LoanAmount FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew6 = new SqlCommand("SELECT MARF FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew7 = new SqlCommand("SELECT MRF FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew8 = new SqlCommand("SELECT SHARE FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew9 = new SqlCommand("SELECT MBF FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew10 = new SqlCommand("SELECT DateOfAppln FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew11 = new SqlCommand("SELECT STATUS FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew12 = new SqlCommand("SELECT ElgAmount FROM gloan_det WHERE appln_no='" + item + "'");
                    SqlDataReader commonRdr = null;
                    SqlDataReader common1Rdr = null;
                    SqlDataReader common2Rdr = null;
                    SqlDataReader common3Rdr = null;
                    SqlDataReader common4Rdr = null;
                    SqlDataReader common5Rdr = null;
                    SqlDataReader common6Rdr = null;
                    SqlDataReader common7Rdr = null;
                    SqlDataReader common8Rdr = null;
                    SqlDataReader common9Rdr = null;
                    SqlDataReader common10Rdr = null;
                    SqlDataReader common11Rdr = null;
                    SqlDataReader common12Rdr = null;
                    conn.Open();
                    cmdnew.Connection = conn;
                    cmdnew1.Connection = conn;
                    cmdnew2.Connection = conn;
                    cmdnew3.Connection = conn;
                    cmdnew4.Connection = conn;
                    cmdnew5.Connection = conn;
                    cmdnew6.Connection = conn;
                    cmdnew7.Connection = conn;
                    cmdnew8.Connection = conn;
                    cmdnew9.Connection = conn;
                    cmdnew10.Connection = conn;
                    cmdnew11.Connection = conn;
                    cmdnew12.Connection = conn;
                    commonRdr = cmdnew.ExecuteReader();
                    common1Rdr = cmdnew1.ExecuteReader();
                    common2Rdr = cmdnew2.ExecuteReader();
                    common3Rdr = cmdnew3.ExecuteReader();
                    common4Rdr = cmdnew4.ExecuteReader();
                    common5Rdr = cmdnew5.ExecuteReader();
                    common6Rdr = cmdnew6.ExecuteReader();
                    common7Rdr = cmdnew7.ExecuteReader();
                    common8Rdr = cmdnew8.ExecuteReader();
                    common9Rdr = cmdnew9.ExecuteReader();
                    common10Rdr = cmdnew10.ExecuteReader();
                    common11Rdr = cmdnew11.ExecuteReader();
                    common12Rdr = cmdnew12.ExecuteReader();
                    while (commonRdr.Read())
                    {
                        textBox2.Text = commonRdr["Name"].ToString();
                    }
                    while (common1Rdr.Read())
                    {
                        textBox1.Text = common1Rdr["IndexNo"].ToString();
                    }
                    while (common3Rdr.Read())
                    {
                        textBox4.Text = common3Rdr["Type"].ToString();
                    }
                    while (common4Rdr.Read())
                    {
                        textBox5.Text = common4Rdr["Purpose"].ToString();
                    }
                    while (common10Rdr.Read())
                    {
                        textBox14.Text = common10Rdr["DateOfAppln"].ToString();
                    }
                    while (common2Rdr.Read())
                    {
                        textBox3.Text = common2Rdr["Branch"].ToString();
                    }
                    while (common5Rdr.Read())
                    {
                        textBox6.Text = common5Rdr["LoanAmount"].ToString();
                    }
                    while (common6Rdr.Read())
                    {
                        textBox6.Text = common6Rdr["MARF"].ToString();
                    }
                    while (common7Rdr.Read())
                    {
                        textBox7.Text = common7Rdr["MRF"].ToString();
                    }
                    while (common9Rdr.Read())
                    {
                        textBox9.Text = common9Rdr["MBF"].ToString();
                    }
                    while (common8Rdr.Read())
                    {
                        textBox8.Text = common8Rdr["SHARE"].ToString();
                    }
                    while (common11Rdr.Read())
                    {
                        textBox10.Text = common11Rdr["STATUS"].ToString();
                    }
                    while (common12Rdr.Read())
                    {
                        textBox11.Text = common12Rdr["ElgAmount"].ToString();
                    }
                    textBox1.ReadOnly = true;
                    textBox2.ReadOnly = true;
                    textBox3.ReadOnly = true;
                    textBox4.ReadOnly = true;
                    textBox5.ReadOnly = true;
                    textBox6.ReadOnly = true;
                    textBox7.ReadOnly = true;
                    textBox8.ReadOnly = true;
                    textBox9.ReadOnly = true;
                    textBox10.ReadOnly = true;
                    textBox11.ReadOnly = true;
                    textBox13.ReadOnly = true;
                    textBox14.ReadOnly = true;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_details();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            calc_emi();
        }
        protected void calc_emi()
        {
            if (textBox12.Text == "")
                textBox13.Text = "0";
            else
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    DateTime item = Convert.ToDateTime(textBox14.Text);
                    SqlCommand cmdnew = new SqlCommand("SELECT ROI FROM st_elg WHERE date<='" + item + "'");
                    SqlDataReader rRdr = null;
                    conn.Open();
                    cmdnew.Connection = conn;
                    rRdr = cmdnew.ExecuteReader();
                    while (rRdr.Read())
                    {
                        R = rRdr["ROI"].ToString();
                    }
                    p = Convert.ToInt32(textBox11.Text);
                    n = Convert.ToInt32(textBox12.Text);
                    r = Convert.ToInt32(R);
                        double y = r / 1200;
                    double x =Math.Pow(1+y,n);
                    a=(p*y*x)/(x-1);
                    a = Math.Truncate(a);
                    textBox13.Text = a.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "" || String.Equals(comboBox1.Text, "--appln", StringComparison.Ordinal))
                MessageBox.Show("All fields are compulsory");
            else
            {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand c = new SqlCommand("select name from granted_loans where appln_no='" + comboBox1.Text + "' and type='" + textBox4.Text + "'");
                SqlDataReader Rdr = null;
                conn.Open();
                c.Connection = conn;
                Rdr = c.ExecuteReader();
                while (Rdr.Read())
                {
                    check = Rdr["name"].ToString();
                }
                if (check != "")
                {
                    MessageBox.Show("Record already exists");
                    check = "";
                }
                else if (textBox12.Text == "" || textBox13.Text == "" || comboBox2.Text == "")
                {
                    MessageBox.Show("All fields are compulsory");
                }
                else
                {
                    if (String.Equals(comboBox2.Text, "Cheque", StringComparison.Ordinal) || String.Equals(comboBox2.Text, "DD", StringComparison.Ordinal))
                    {
                        if (textBox15.Text == "")
                            MessageBox.Show("Enter cheque/dd number");
                        else
                        {
                            save_details();
                        }
                    }
                    else
                        save_details();
                }
            }
        }
        }
        protected void save_details()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO granted_loans (appln_no,DateOfAppln,IndexNo,Type,LoanAmount,Purpose,MARF,MRF,MBF,SHARE,Name,Branch,STATUS,ElgAmount,NOI,EMI,MOP,Cheque_DD_No,IssueDate) VALUES (@apn,@doa,@ino,@t,@la,@pp,@MARF,@MRF,@MBF,@SHARE,@Name,@Branch,@Stat,@ElgA,@NOI,@EMI,@MOP,@cd_no,@id)");
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@apn", comboBox1.Text);
                cmd.Parameters.AddWithValue("@doa", textBox14.Text.ToString());
                cmd.Parameters.AddWithValue("@ino", textBox1.Text);
                cmd.Parameters.AddWithValue("@t", textBox4.Text);
                cmd.Parameters.AddWithValue("@la", textBox11.Text);
                cmd.Parameters.AddWithValue("@pp", textBox5.Text);
                cmd.Parameters.AddWithValue("@MARF", Convert.ToInt32(textBox6.Text));
                cmd.Parameters.AddWithValue("@MRF", Convert.ToInt32(textBox7.Text));
                cmd.Parameters.AddWithValue("@MBF", Convert.ToInt32(textBox9.Text));
                cmd.Parameters.AddWithValue("@SHARE", Convert.ToInt32(textBox8.Text));
                cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                cmd.Parameters.AddWithValue("@Branch", textBox3.Text);
                cmd.Parameters.AddWithValue("@Stat", textBox10.Text);
                cmd.Parameters.AddWithValue("@ElgA", textBox11.Text);
                cmd.Parameters.AddWithValue("@NOI", textBox12.Text);
                cmd.Parameters.AddWithValue("@EMI", textBox13.Text);
                cmd.Parameters.AddWithValue("@MOP", comboBox2.Text);
                cmd.Parameters.AddWithValue("@cd_no", textBox15.Text);
                cmd.Parameters.AddWithValue("@id",dateTimePicker1.Value.Date.ToString());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Information saved successfully");
            }
        }
    }
}
