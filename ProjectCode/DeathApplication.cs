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
    public partial class DeathApplication : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        string item="not eligible";
        int sfa = 0,countd = 0,xy=0,chk=0;
        bool sf = false;
        public int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30,
31 };
        public DateTime fromDate;
        int count = 0,z=0,xyz=0;
        public DateTime toDate;
        public int year, month, day, fyear, fmonth, fday, iyear, imonth, iday;
        string xxx,x,y,yyy;
        public DeathApplication()
        {
            InitializeComponent();
        }

        protected void fill_combo2()
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
                comboBox2.DisplayMember = "indx";
                comboBox2.ValueMember = "indx";
                comboBox2.DataSource = ds.Tables[0];
                comboBox2.Text = "--indx";
                comboBox2.Enabled = true;
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            countd++;
            if(countd>=2)
            fill_loadedDetails();
            checkBox1.Visible = false;
            groupBox1.Visible = false;
        }
        protected void fill_details()
        {
            if (String.Equals(comboBox2.Text, "--indx", StringComparison.Ordinal))
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                dateTimePicker1.Value = DateTime.Today;
                checkBox1.Visible = false;
                groupBox1.Visible = false;
            }
        }
        protected void fill_loadedDetails()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string item = comboBox2.SelectedValue.ToString();
                    SqlCommand cmd = new SqlCommand("SELECT fullname FROM memberdetails WHERE indx='" + item + "'");
                    SqlCommand cmd1 = new SqlCommand("SELECT bname FROM memberdetails WHERE indx='" + item + "'");
                    SqlDataReader cmdRdr = null;
                    SqlDataReader cmd1Rdr = null;
                    conn.Open();
                    cmd.Connection = conn;
                    cmd1.Connection = conn;
                    cmdRdr = cmd.ExecuteReader();
                    cmd1Rdr = cmd1.ExecuteReader();
                    while (cmdRdr.Read())
                    {
                        textBox3.Text = cmdRdr["fullname"].ToString();
                    }
                    while (cmd1Rdr.Read())
                    {
                        textBox4.Text = cmd1Rdr["bname"].ToString();
                    }
                }
                fill_shortfall();
                mbf_total();
        }
        protected void fill_shortfall()
        {
            sf = true;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string item = comboBox2.SelectedValue.ToString();
                SqlCommand cmd = new SqlCommand("SELECT dofjoin FROM memberdetails WHERE indx='" + item + "'");
                SqlDataReader cmdRdr = null;
                conn.Open();
                cmd.Connection = conn;         
                cmdRdr = cmd.ExecuteReader();
                while (cmdRdr.Read())
                {
                    xxx = cmdRdr["dofjoin"].ToString();
                }
            }
                DateDifference(Convert.ToDateTime(xxx),Convert.ToDateTime(dateTimePicker2.Text));
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
            if (sf == true)
            {
                int mnths = year * 12 + month;
                sfa = mnths * 150;
                textBox6.Text = sfa.ToString();
                sf = false;
            }
        }

        private void DeathApplication_Load_1(object sender, EventArgs e)
        {
            fill_combo2();
            textBox7.Text = "";
            fill_applnNo();
            fill_details();
            checkBox1.Visible = false;
            groupBox1.Visible = false;
            textBox5.Text = "";
        }
        protected void mbf_total()
        {
            z = 0;
            xyz = 0;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmdt = new SqlCommand("SELECT * FROM mbf_contri where date<'" + Convert.ToDateTime(xxx) + "'");
                    SqlDataReader cmdtRdr = null;
                    conn.Open();
                    cmdt.Connection = conn;
                    cmdtRdr = cmdt.ExecuteReader();
                    while (cmdtRdr.Read())
                    {
                        y = cmdtRdr["amount"].ToString();
                    }
                    yyy = xxx;
                    z = Convert.ToInt32(y);
                    SqlCommand cmd = new SqlCommand("SELECT * FROM mbf_contri where date>'"+Convert.ToDateTime(xxx)+"' and date<='"+Convert.ToDateTime(dateTimePicker2.Text)+"'");
                    SqlDataReader cmdRdr = null;
                    cmd.Connection = conn;
                    cmdRdr = cmd.ExecuteReader();
                    while (cmdRdr.Read() &&x!="")
                    {
                        xxx = cmdRdr["date"].ToString();
                        y=cmdRdr["amount"].ToString();
                        DateDifference(Convert.ToDateTime(xxx),Convert.ToDateTime(yyy));
                        xy = year * 12 + month;
                        if (Convert.ToDateTime(dateTimePicker2.Text) > Convert.ToDateTime(xxx))
                        {
                            z = z * xy;
                            xyz = xyz + z;
                            z = Convert.ToInt32(y);
                            yyy = xxx;
                        }
                        else
                            x = "";
                    }
                    DateDifference(Convert.ToDateTime(xxx), Convert.ToDateTime(dateTimePicker2.Text));
                    xy = year * 12 + month;
                    z = z * xy;
                       xyz = xyz + z;
                    textBox5.Text = xyz.ToString();
                    check();
                }
        }
        protected void check()
        {
            if (Convert.ToInt32(textBox5.Text)!=0 &&Convert.ToInt32(textBox5.Text) - Convert.ToInt32(textBox6.Text) >= 0)
                checkBox1.Visible = true;
            else
                checkBox1.Visible = false;
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (String.Equals(comboBox2.Text, "--indx", StringComparison.Ordinal))
            {
            }
            else
            {
                fill_loadedDetails();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                groupBox1.Visible = true;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT funexp FROM mbf_elg where date<='" + Convert.ToDateTime(dateTimePicker1.Text) + "'");
                    SqlDataReader cmdRdr = null;
                    conn.Open();
                    cmd.Connection = conn;
                    cmdRdr = cmd.ExecuteReader();
                    while (cmdRdr.Read())
                    {
                        y = cmdRdr["funexp"].ToString();
                    }
                    textBox1.Text = y;
                }
            }
            else
                groupBox1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            refresh_details();
        }
        protected void refresh_details()
        {
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox2.Text = "--indx";
            comboBox3.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            groupBox1.Visible = false;
            checkBox1.Checked = false;
            checkBox1.Visible = false;
            item = "noteligible";
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || String.Equals(comboBox2.Text, "--indx", StringComparison.Ordinal) || String.Equals(comboBox3.Text, "", StringComparison.Ordinal))
            {
                MessageBox.Show("All fields are compulsory 1");
            }
            else if (checkBox1.Visible == false)
            {
                fill_deathdet();
            }
            else if (checkBox1.Visible == true)
            {
                if (checkBox1.Checked==true)
                {
                    if (textBox1.Text == "" || comboBox1.Text == "")
                        MessageBox.Show("All fields are compulsory 3");
                    else if (String.Equals(comboBox1.Text, "Cheque", StringComparison.Ordinal) || String.Equals(comboBox1.Text, "DD", StringComparison.Ordinal))
                    {
                        if (textBox2.Text == "")
                            MessageBox.Show("Cheque/DD number is compulsory");
                        else
                        {
                            item = "eligible";
                            fill_deathdet();
                        }
                    }
                }
                    else
                    {
                        item = "eligible";
                        fill_deathdet();
                    }
            }
}
        protected void fill_deathdet()
        {
            if (checkBox1.Visible == false)
            {
                textBox1.Text = "0";
                textBox2.Text = "";
                comboBox1.Text = "";
            }
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cd = new SqlCommand("Insert into death_form (Date,IndexNo,Name,Branch,DOD,Type,Mbf,Shortfall,FunExp,MOP,CD_No,ApplnNo,status) values (@Date,@IndexNo,@Name,@Branch,@DOD,@Type,@Mbf,@Shortfall,@FunExp,@MOP,@CD_No,@ApplnNo,@status)");
                    cd.CommandType = CommandType.Text;
                    cd.Connection = conn;
                    conn.Open();
                    cd.Parameters.AddWithValue("@Date", dateTimePicker1.Value.ToShortDateString());
                    cd.Parameters.AddWithValue("@IndexNo", comboBox2.Text);
                    cd.Parameters.AddWithValue("@Name", textBox3.Text);
                    cd.Parameters.AddWithValue("@Branch", textBox4.Text);
                    cd.Parameters.AddWithValue("@DOD", dateTimePicker2.Value.ToShortDateString());
                    cd.Parameters.AddWithValue("@Type", comboBox3.Text);
                    cd.Parameters.AddWithValue("@Mbf", textBox5.Text.ToString());
                    cd.Parameters.AddWithValue("@Shortfall", textBox6.Text.ToString());
                    cd.Parameters.AddWithValue("@FunExp", textBox1.Text.ToString());
                    cd.Parameters.AddWithValue("@MOP", comboBox1.Text);
                    cd.Parameters.AddWithValue("@CD_No", textBox2.Text);
                    cd.Parameters.AddWithValue("@ApplnNo", textBox7.Text);
                    cd.Parameters.AddWithValue("@Status",item);
                    cd.ExecuteNonQuery();
                    refresh_details();
                    fill_applnNo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("record already exists"); 
            }
        }
        protected void fill_applnNo()
        {
            string l = "DL";
            int m=0, n=0;
            using (SqlConnection conc = new SqlConnection(connectionString))
            {
                string xxxx = "--ApplnNo";
                SqlCommand cmd = new SqlCommand("select max(substring(ApplnNo,3,6)) as ApplnNo from death_form where ApplnNo!='"+xxxx+"'");
                SqlDataReader cmdRdr = null;
                conc.Open();
                cmd.Connection = conc;
                cmdRdr = cmd.ExecuteReader();
                while (cmdRdr.Read())
                {
                    textBox7.Text = cmdRdr["ApplnNo"].ToString();
                }
                if (textBox7.Text == "" || textBox7.Text == null)
                    textBox7.Text = l + "0001";
                else
                {
                    m = Convert.ToInt16(textBox7.Text.Substring(2));
                    if (m > 9 && m < 99)
                    {
                        m = m + 1;
                        textBox7.Text = l + "00" + m.ToString();
                    }
                    else if (m <= 9)
                    {
                        n = m + 1;
                        textBox7.Text = l + "000" + n.ToString();
                    }
                }
            }
        }        
    }
}
