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
    public partial class LoanVerification : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        string amount, date, datex, bpt, bp, doflapln,rp,ms,s;
        public int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30,
31 };
        public DateTime fromDate,toDate;
        int count = 0,total_months=0;
        public int year, month, day, fyear, fmonth, fday, iyear, imonth, iday;
        public LoanVerification()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_details();
        }

        private void LoanVerification_Load(object sender, EventArgs e)
        {
            fill_combo();
            fill_details();
//            fill_grid();
        }
        protected void fill_combo()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select appln_no from loan_appln order by appln_no";
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
                comboBox1.Text = "--apln";
                comboBox1.Enabled = true;
            }
        }
        protected void fill_details()
        {
               if(String.Equals(comboBox1.Text,"--apln",StringComparison.Ordinal))
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
                    dateTimePicker1.Value = DateTime.Today;
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                    string item = comboBox1.SelectedValue.ToString();
                    SqlCommand cmdnew = new SqlCommand("SELECT Name FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew1 = new SqlCommand("SELECT IndexNo FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew2 = new SqlCommand("SELECT Branch FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew3 = new SqlCommand("SELECT Type FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew4 = new SqlCommand("SELECT Purpose FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew5 = new SqlCommand("SELECT LoanAmount FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew6 = new SqlCommand("SELECT MARF FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew7 = new SqlCommand("SELECT MRF FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew8 = new SqlCommand("SELECT SHARE FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew9 = new SqlCommand("SELECT MBF FROM loan_appln WHERE appln_no='" + item + "'");
                    SqlCommand cmdnew10 = new SqlCommand("SELECT DateOfAppln FROM loan_appln WHERE appln_no='" + item + "'");
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
                       dateTimePicker1.Text = common10Rdr["DateOfAppln"].ToString();
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
                        textBox7.Text = common6Rdr["MARF"].ToString();
                    }
                    while (common7Rdr.Read())
                    {
                        textBox8.Text = common7Rdr["MRF"].ToString();
                    }
                    while (common9Rdr.Read())
                    {
                        textBox10.Text = common9Rdr["MBF"].ToString();
                    }
                    while (common8Rdr.Read())
                    {
                        textBox9.Text = common8Rdr["SHARE"].ToString();
                    }
                }
            }
        }
        protected void DateDifference(DateTime d1, DateTime d2)
        {
            count = count + 1;
            if (d1 > d2)
            {
                fromDate = d2;
                toDate = d1;
            }
            else
            {
                fromDate = d1;
                toDate = d2;
            }
            int increment = 0;
            if (fromDate.Day > toDate.Day)
            {
                increment = monthDay[fromDate.Month - 1];
            }
            if (increment == -1)
            {
                if (DateTime.IsLeapYear(fromDate.Year))
                {
                    increment = 29;
                }
                else
                {
                    increment = 28;
                }
            }
            if (increment != 0)
            {
                day = (toDate.Day + increment) - fromDate.Day;
                increment = 1;
            }
            else
            {
                day = toDate.Day - fromDate.Day;
            }
            if ((fromDate.Month + increment) > toDate.Month)
            {
                month = (toDate.Month + 12) - (fromDate.Month + increment);
                increment = 1;
            }
            else
            {
                month = (toDate.Month) - (fromDate.Month + increment);
                increment = 0;
            }
            year = toDate.Year - (fromDate.Year + increment);
            total_months = year * 12 + month;
            //label40.Text = year + " year(s)" + month + " month(s) " + day + " day(s) are left for service";
        }
        protected void check_cond()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd1 = new SqlCommand("SELECT MaxLimit FROM st_elg WHERE date<='" + dateTimePicker1.Text + "'");
                SqlCommand cmd2 = new SqlCommand("SELECT dofjoin FROM memberdetails WHERE indx='" + textBox1.Text + "'");
                SqlCommand cmd3 = new SqlCommand("SELECT MinMmbrshpPrd FROM st_elg WHERE date<='" + dateTimePicker1.Text + "'");
                SqlCommand cmd4 = new SqlCommand("SELECT TimesOfBasic FROM st_elg WHERE date<='" + dateTimePicker1.Text + "'");
                SqlCommand cmd5 = new SqlCommand("SELECT basicpay FROM memberdetails WHERE indx='" + textBox1.Text + "'");
                SqlCommand cmd8 = new SqlCommand("SELECT MinShare FROM st_elg WHERE date<='" + dateTimePicker1.Text + "'");
                SqlCommand cmd9 = new SqlCommand("SELECT total_share FROM subscription WHERE indx='" + textBox1.Text + "'");
                SqlDataReader cmd1Rdr = null;
                SqlDataReader cmd2Rdr = null;
                SqlDataReader cmd3Rdr = null;
                SqlDataReader cmd4Rdr = null;
                SqlDataReader cmd5Rdr = null;
                SqlDataReader cmd8Rdr = null;
                SqlDataReader cmd9Rdr = null;
                conn.Open();
                cmd1.Connection = conn;
                cmd2.Connection = conn;
                cmd3.Connection = conn;
                cmd4.Connection = conn;
                cmd5.Connection = conn;
                cmd8.Connection = conn;
                cmd9.Connection = conn;
                cmd1Rdr = cmd1.ExecuteReader();
                cmd2Rdr = cmd2.ExecuteReader();
                cmd3Rdr = cmd3.ExecuteReader();
                cmd4Rdr = cmd4.ExecuteReader();
                cmd5Rdr = cmd5.ExecuteReader();
                cmd8Rdr = cmd8.ExecuteReader();
                cmd9Rdr = cmd9.ExecuteReader();
                while (cmd1Rdr.Read())
                {
                    amount = cmd1Rdr["MaxLimit"].ToString();
                }
                while (cmd2Rdr.Read())
                {
                    date = cmd2Rdr["dofjoin"].ToString();
                }
                while (cmd3Rdr.Read())
                {
                    datex = cmd3Rdr["MinMmbrshpPrd"].ToString();
                }
                while (cmd4Rdr.Read())
                {
                    bpt = cmd4Rdr["TimesOfBasic"].ToString();
                }
                while (cmd5Rdr.Read())
                {
                    bp = cmd5Rdr["basicpay"].ToString();
                }
                while (cmd8Rdr.Read())
                {
                    ms = cmd8Rdr["MinShare"].ToString();
                }
                while (cmd9Rdr.Read())
                {
                    s = cmd9Rdr["total_share"].ToString();
                }
                if (String.Equals(textBox4.Text, "Renewal", StringComparison.Ordinal))
                {
                    SqlCommand cmd6 = new SqlCommand("SELECT RnwlPrd FROM st_elg WHERE date<='" + dateTimePicker1.Text + "'");
                    SqlCommand cmd7 = new SqlCommand("SELECT Date FROM st_elg WHERE date<='" + dateTimePicker1.Text + "'");
                    SqlDataReader cmd6Rdr = null;
                    SqlDataReader cmd7Rdr = null;
                    cmd6.Connection = conn;
                    cmd7.Connection = conn;
                    cmd6Rdr = cmd6.ExecuteReader();
                    cmd7Rdr = cmd7.ExecuteReader();
                    while (cmd6Rdr.Read())
                    {
                        rp = cmd6Rdr["RnwlPrd"].ToString();
                    }
                    while (cmd7Rdr.Read())
                    {
                        doflapln = cmd7Rdr["Date"].ToString();
                    }
                    DateDifference(DateTime.Today, Convert.ToDateTime(doflapln));
                    if (total_months < Convert.ToInt32(rp))
                    {
                        label17.Text = "Rejected";
                        label18.Text = "0";
                        label19.Text = "Renewal Period not complete";
                    }
                    DateDifference(DateTime.Today, Convert.ToDateTime(date));
                    if (Convert.ToInt32(amount) < Convert.ToInt32(textBox6.Text))
                    {
                        label17.Text = "Rejected";
                        label18.Text = "0";
                        label19.Text = "Loan Amount exceeded Maximum limit";
                    }
                    else if (total_months < Convert.ToInt32(datex))
                    {
                        label17.Text = "Rejected";
                        label18.Text = "0";
                        label19.Text = "Minimum Memership Period Not Satisfied";
                    }
                    else if (Convert.ToInt32(s) < Convert.ToInt32(ms))
                    {
                        label17.Text = "Rejected";
                        label18.Text = "0";
                        label19.Text = "Minimum share value not satisfied";
                    }
                    else
                    {
                        int bpx = Convert.ToInt32(bp) * Convert.ToInt32(bpt);
                        if (bpx == 0)
                        {
                            label17.Text = "Not employed";
                            label18.Text = bpx.ToString();
                            label19.Text = "-";
                        }

                        else if (bpx < Convert.ToInt32(amount))
                        {
                            label17.Text = "Loan Granted";
                            label18.Text = bpx.ToString();
                            label19.Text = "-";
                        }
                        else
                        {
                            label17.Text = "Loan Granted";
                            label18.Text = amount;
                            label19.Text = "-";
                        }
                    }
                    if (String.Equals(label17.Text, "Rejected", StringComparison.Ordinal) || String.Equals(label17.Text, "Not employed", StringComparison.Ordinal))
                    {
                        /*,NOI,EMI,MOP,Cheque_DD_No,IssueDate*/
                        SqlCommand cmd = new SqlCommand("INSERT INTO Granted_loans (appln_no,DateOfAppln,IndexNo,Type,LoanAmount,Purpose,MARF,MRF,MBF,SHARE,Name,Branch,STATUS,ElgAmount,Reason) VALUES (@apn,@doa,@ino,@t,@la,@pp,@MARF,@MRF,@MBF,@SHARE,@Name,@Branch,@Stat,@ElgA,@Rsn)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@apn", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@doa", dateTimePicker1.Value.Date.ToString());
                        cmd.Parameters.AddWithValue("@ino", textBox1.Text);
                        cmd.Parameters.AddWithValue("@t", textBox4.Text);
                        cmd.Parameters.AddWithValue("@la", textBox6.Text);
                        cmd.Parameters.AddWithValue("@pp", textBox5.Text);
                        cmd.Parameters.AddWithValue("@MARF", Convert.ToInt32(textBox7.Text));
                        cmd.Parameters.AddWithValue("@MRF", Convert.ToInt32(textBox8.Text));
                        cmd.Parameters.AddWithValue("@MBF", Convert.ToInt32(textBox10.Text));
                        cmd.Parameters.AddWithValue("@SHARE", Convert.ToInt32(textBox9.Text));
                        cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Branch", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Stat", label17.Text);
                        cmd.Parameters.AddWithValue("@ElgA", label18.Text);
                        cmd.Parameters.AddWithValue("@Rsn", label19.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                    }
                    else if (String.Equals(label17.Text, "Loan Granted", StringComparison.Ordinal))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Gloan_det (appln_no,DateOfAppln,IndexNo,Type,LoanAmount,Purpose,MARF,MRF,MBF,SHARE,Name,Branch,STATUS,ElgAmount,Reason) VALUES (@apn,@doa,@ino,@t,@la,@pp,@MARF,@MRF,@MBF,@SHARE,@Name,@Branch,@Stat,@ElgA,@Rsn)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@apn", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@doa", dateTimePicker1.Value.Date.ToString());
                        cmd.Parameters.AddWithValue("@ino", textBox1.Text);
                        cmd.Parameters.AddWithValue("@t", textBox4.Text);
                        cmd.Parameters.AddWithValue("@la", textBox6.Text);
                        cmd.Parameters.AddWithValue("@pp", textBox5.Text);
                        cmd.Parameters.AddWithValue("@MARF", Convert.ToInt32(textBox7.Text));
                        cmd.Parameters.AddWithValue("@MRF", Convert.ToInt32(textBox8.Text));
                        cmd.Parameters.AddWithValue("@MBF", Convert.ToInt32(textBox10.Text));
                        cmd.Parameters.AddWithValue("@SHARE", Convert.ToInt32(textBox9.Text));
                        cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Branch", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Stat", label17.Text);
                        cmd.Parameters.AddWithValue("@ElgA", label18.Text);
                        cmd.Parameters.AddWithValue("@Rsn", label19.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                    }
                }
                else if (String.Equals(textBox4.Text, "New", StringComparison.Ordinal))
                {
                    DateDifference(DateTime.Today, Convert.ToDateTime(date));
                    if (Convert.ToInt32(amount) < Convert.ToInt32(textBox6.Text))
                    {
                        label17.Text = "Rejected";
                        label18.Text = "0";
                        label19.Text = "Loan Amount exceeded Maximum limit";
                    }
                    else if (total_months < Convert.ToInt32(datex))
                    {
                        label17.Text = "Rejected";
                        label18.Text = "0";
                        label19.Text = "Minimum Memership Period Not Satisfied";
                    }
                    else
                    {
                        int bpx = Convert.ToInt32(bp) * Convert.ToInt32(bpt);
                        if (bpx == 0)
                        {
                            label17.Text = "Not employed";
                            label18.Text = bpx.ToString();
                            label19.Text = "-";
                        }
                        else if (bpx < Convert.ToInt32(amount))
                        {
                            label17.Text = "Loan Granted";
                            label18.Text = bpx.ToString();
                            label19.Text = "-";
                        }
                        else if (Convert.ToInt32(s) < Convert.ToInt32(ms))
                        {
                            label17.Text = "Rejected";
                            label18.Text = "0";
                            label19.Text = "Minimum share value not satisfied";
                        }
                        else
                        {
                            label17.Text = "Loan Granted";
                            label18.Text = amount;
                            label19.Text = "-";
                        }
                    }
                    if (String.Equals(label17.Text, "Rejected", StringComparison.Ordinal) || String.Equals(label17.Text, "Not employed", StringComparison.Ordinal))
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Granted_loans (appln_no,DateOfAppln,IndexNo,Type,LoanAmount,Purpose,MARF,MRF,MBF,SHARE,Name,Branch,STATUS,ElgAmount,Reason) VALUES (@apn,@doa,@ino,@t,@la,@pp,@MARF,@MRF,@MBF,@SHARE,@Name,@Branch,@Stat,@ElgA,@Rsn)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@apn", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@doa", dateTimePicker1.Value.Date.ToString());
                        cmd.Parameters.AddWithValue("@ino", textBox1.Text);
                        cmd.Parameters.AddWithValue("@t", textBox4.Text);
                        cmd.Parameters.AddWithValue("@la", textBox6.Text);
                        cmd.Parameters.AddWithValue("@pp", textBox5.Text);
                        cmd.Parameters.AddWithValue("@MARF", Convert.ToInt32(textBox7.Text));
                        cmd.Parameters.AddWithValue("@MRF", Convert.ToInt32(textBox8.Text));
                        cmd.Parameters.AddWithValue("@MBF", Convert.ToInt32(textBox10.Text));
                        cmd.Parameters.AddWithValue("@SHARE", Convert.ToInt32(textBox9.Text));
                        cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Branch", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Stat", label17.Text);
                        cmd.Parameters.AddWithValue("@ElgA", label18.Text);
                        cmd.Parameters.AddWithValue("@Rsn", label19.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO gloan_det (appln_no,DateOfAppln,IndexNo,Type,LoanAmount,Purpose,MARF,MRF,MBF,SHARE,Name,Branch,STATUS,ElgAmount) VALUES (@apn,@doa,@ino,@t,@la,@pp,@MARF,@MRF,@MBF,@SHARE,@Name,@Branch,@Stat,@ElgA)");
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.Parameters.AddWithValue("@apn", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@doa", dateTimePicker1.Value.Date.ToString());
                        cmd.Parameters.AddWithValue("@ino", textBox1.Text);
                        cmd.Parameters.AddWithValue("@t", textBox4.Text);
                        cmd.Parameters.AddWithValue("@la", textBox6.Text);
                        cmd.Parameters.AddWithValue("@pp", textBox5.Text);
                        cmd.Parameters.AddWithValue("@MARF", Convert.ToInt32(textBox7.Text));
                        cmd.Parameters.AddWithValue("@MRF", Convert.ToInt32(textBox8.Text));
                        cmd.Parameters.AddWithValue("@MBF", Convert.ToInt32(textBox10.Text));
                        cmd.Parameters.AddWithValue("@SHARE", Convert.ToInt32(textBox9.Text));
                        cmd.Parameters.AddWithValue("@Name", textBox2.Text);
                        cmd.Parameters.AddWithValue("@Branch", textBox3.Text);
                        cmd.Parameters.AddWithValue("@Stat", label17.Text);
                        cmd.Parameters.AddWithValue("@ElgA", label18.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Information saved successfully");
                    }
                }
            }
        }
            
        private void button1_Click(object sender, EventArgs e)
        {
            check_cond();
        }
    }
}
