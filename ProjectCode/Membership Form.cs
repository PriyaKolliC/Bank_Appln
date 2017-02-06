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
namespace WindowsFormsApplication2
{
    public partial class Membership_Form : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30,
31 };
        public DateTime fromDate;
        int temp = 0,count=0;
        string ex, ph;
        int z;
        public DateTime toDate;
        int y;
        public int year,month,day,fyear,fmonth,fday,iyear,imonth,iday;
        string firstletter;
        public Membership_Form()
        {
            InitializeComponent();
        }

        private void label23_Click(object sender, EventArgs e)
        {

        }
        protected void age_calc()
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateTimePicker2.Value.Year * 100 + dateTimePicker2.Value.Month) * 100 + dateTimePicker2.Value.Day;
            string age = ((int)(a - b) / 10000).ToString();
            textBox5.Text = age;
        }
        protected void age_calcn()
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateTimePicker5.Value.Year * 100 + dateTimePicker5.Value.Month) * 100 + dateTimePicker5.Value.Day;
            string age = ((int)(a - b) / 10000).ToString();
            textBox15.Text = age;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                textBox16.Text = open.FileName;
            }
        }
        protected void total()
        {
            long i,j,k,l,res;
            if (textBox21.Text == "")
                i = 0;
            else
                i = Convert.ToInt64(textBox21.Text);
            if (textBox22.Text == "")
                j = 0;
            else
                j = Convert.ToInt64(textBox22.Text); 
            if (textBox23.Text == "")
                k = 0;
            else
                k = Convert.ToInt64(textBox23.Text);
            if (textBox24.Text == "")
                l = 0;
            else
                l = Convert.ToInt64(textBox24.Text);
            res = i + j + k + l;
            textBox25.Text = res.ToString();
        }
        
        private void textBox21_TextChanged_1(object sender, EventArgs e)
        {
            total();
        }

        private void textBox22_TextChanged_1(object sender, EventArgs e)
        {
            total();
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            total();
        }
        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            total();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected void calc_fee()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                long admnfee,marf,mbf,share;
                SqlCommand cmd_af = new SqlCommand("Select Amount from admsson_fee where Date<='"+dateTimePicker1.Text+"' order by Date DESC");
                SqlCommand cmd_marf = new SqlCommand("Select Amount from marf_contri where Date<='" + dateTimePicker1.Text + "' order by Date DESC");
                SqlCommand cmd_mbf = new SqlCommand("Select Amount from mbf_contri where Date<='" + dateTimePicker1.Text + "' order by Date DESC");
                SqlCommand cmd_share = new SqlCommand("Select Amount from share_contribution where Date<='" + dateTimePicker1.Text + "' order by Date DESC");
                cmd_af.Connection = connection;
                cmd_marf.Connection = connection;
                cmd_mbf.Connection = connection;
                cmd_share.Connection = connection;
                connection.Open(); 
                admnfee = Convert.ToInt64(cmd_af.ExecuteScalar());
                marf = Convert.ToInt64(cmd_marf.ExecuteScalar());
                mbf = Convert.ToInt64(cmd_mbf.ExecuteScalar());
                share = Convert.ToInt64(cmd_share.ExecuteScalar());
                textBox21.Text = admnfee.ToString();
                textBox22.Text = mbf.ToString();
                textBox23.Text = share.ToString();
                textBox24.Text = marf.ToString();
                cmd_af.ExecuteNonQuery();
                cmd_marf.ExecuteNonQuery();
                cmd_mbf.ExecuteNonQuery();
                cmd_share.ExecuteNonQuery();
            }
            

        }

        private void Membership_Form_Load(object sender, EventArgs e)
        {
            calc_fee();
            age_calc();
            age_calcn();
            DateTime intoday = DateTime.Today;
            iyear = dateTimePicker3.Value.Year+60;
            imonth = dateTimePicker3.Value.Month;
            iday = dateTimePicker3.Value.Day;
            DateTime intial = new DateTime(iyear,imonth,iday);
                DateDifference(intoday,intial);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            calc_fee();
        }

        protected void DateDifference(DateTime d1, DateTime d2)
        {
            count=count+1;
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
            month = (toDate.Month+ 12) - (fromDate.Month + increment);
            increment = 1;
            }
            else
            {       
                month = (toDate.Month) - (fromDate.Month + increment);
                increment = 0;
            }
            year = toDate.Year - (fromDate.Year + increment);             
            label40.Text = year + " year(s)" + month + " month(s) " + day + " day(s) are left for service";                                
        }

        

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker4.Value.Year < dateTimePicker3.Value.Year)
            {
                MessageBox.Show("Date of confirmation cannot be less than date of joining");
            }
            else if (dateTimePicker4.Value.Year == dateTimePicker3.Value.Year && dateTimePicker4.Value.Month < dateTimePicker3.Value.Month)
            {
                MessageBox.Show("Date of confirmation cannot be less than date of joining");
            }
            else if (dateTimePicker4.Value.Year == dateTimePicker3.Value.Year && dateTimePicker4.Value.Day < dateTimePicker3.Value.Day)
            {
                MessageBox.Show("Date of confirmation cannot be less than date of joining");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "")
                textBox10.Text = "0";
            if (textBox13.Text == "")
                textBox13.Text = "0";
            if (textBox15.Text == "")
                textBox15.Text = "0";
            if (textBox14.Text == "")
                textBox14.Text = "0";
            if (textBox18.Text == "")
                textBox18.Text = "0";
            if (checkBox1.Checked == true)
                ex = "exservice";
            else
                ex = "not exservice";
            if (checkBox2.Checked == true)
                ph = "handicapped";
            else
                ph = "not ph";
            
            if (textBox3.Text == "" || textBox4.Text == "" || dateTimePicker1.Text == "" || dateTimePicker2.Text == "" || dateTimePicker3.Text == "" || dateTimePicker4.Text == "" || textBox7.Text == "" || textBox6.Text == "" || textBox11.Text == "" || textBox12.Text == "" || textBox9.Text == ""||textBox13.Text==""||comboBox7.Text==""||comboBox6.Text=="")
            {
                MessageBox.Show("* Fields are mandatory");
            }
            if (textBox7.Text.Length > 10) // 
            {
                MessageBox.Show("mobile number cannot exceed 10 digits");
            }
            /*if (!phone_vldn())
            {
                MessageBox.Show("enter valid phone number");
            }*/
            if (textBox9.Text.Length > 16)
            {
                MessageBox.Show("account number cannot exceed 16 digits");
            }
            else if (temp == 1 && dateTimePicker5.Text == "" && textBox17.Text == "")
            {
                MessageBox.Show("* Fields are mandatory");
            }
                
            else
            {
                Image img = pictureBox1.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO memberdetails (dofadm,indx,status,exservice,ph,fullcombo,fullname,frhname,dob,age,dofjoin,dofconf,email,phone,resaddr,bcode,bname,region,zone,state,accno,pfcode,hrmscode,designation,basicpay,nomname,relation,nomage,nomdob,guardian,remarks,sharesno,sharepaid,adminfee,mbf,share,marf,total,photo) values(@dofadm,@indx,@status,@exservice,@ph,@fullcombo,@fullname,@frhname,@dob,@age,@dofjoin,@dofconf,@email,@phone,@resaddr,@bcode,@bname,@region,@zone,@state,@accno,@pfcode,@hrmscode,@designation,@basicpay,@nomname,@relation,@nomage,@nomdob,@guardian,@remarks,@sharesno,@sharepaid,@adminfee,@mbf,@share,@marf,@total,@photo)");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    cmd.Parameters.AddWithValue("@dofadm", dateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@indx", textBox1.Text);
                    cmd.Parameters.AddWithValue("@status", textBox2.Text);
                    cmd.Parameters.AddWithValue("@exservice", ex);
                    cmd.Parameters.AddWithValue("@ph", ph);
                    cmd.Parameters.AddWithValue("@fullcombo", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@fullname", textBox3.Text);
                    cmd.Parameters.AddWithValue("@frhname", textBox4.Text);
                    cmd.Parameters.AddWithValue("@dob", dateTimePicker2.Text);
                    cmd.Parameters.AddWithValue("@age", Convert.ToInt16(textBox5.Text));
                    cmd.Parameters.AddWithValue("@dofjoin", dateTimePicker3.Text);
                    cmd.Parameters.AddWithValue("@dofconf", dateTimePicker4.Text);
                    cmd.Parameters.AddWithValue("@email", textBox6.Text);
                    cmd.Parameters.AddWithValue("@phone", textBox7.Text);
                    cmd.Parameters.AddWithValue("@resaddr", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@bcode", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@bname", textBox8.Text);
                    cmd.Parameters.AddWithValue("@region", textBox26.Text);
                    cmd.Parameters.AddWithValue("@zone", textBox27.Text);
                    cmd.Parameters.AddWithValue("@state", textBox28.Text);
                    cmd.Parameters.AddWithValue("@accno", textBox9.Text);
                    cmd.Parameters.AddWithValue("@pfcode", textBox11.Text);
                    cmd.Parameters.AddWithValue("@hrmscode", textBox12.Text);
                    cmd.Parameters.AddWithValue("@designation", comboBox6.Text);
                    cmd.Parameters.AddWithValue("@basicpay", Convert.ToInt32(textBox10.Text));
                    cmd.Parameters.AddWithValue("@nomname", textBox13.Text);
                    cmd.Parameters.AddWithValue("@relation", comboBox7.Text);
                    cmd.Parameters.AddWithValue("@nomage", Convert.ToInt16(textBox15.Text));
                    cmd.Parameters.AddWithValue("@nomdob", dateTimePicker5.Text);
                    cmd.Parameters.AddWithValue("@guardian", textBox17.Text);
                    cmd.Parameters.AddWithValue("@remarks", textBox19.Text);
                    cmd.Parameters.AddWithValue("@sharesno", Convert.ToInt16(textBox14.Text));
                    cmd.Parameters.AddWithValue("@sharepaid", Convert.ToInt16(textBox18.Text));
                    cmd.Parameters.AddWithValue("@adminfee", Convert.ToInt16(textBox21.Text));
                    cmd.Parameters.AddWithValue("@mbf", Convert.ToInt16(textBox22.Text));
                    cmd.Parameters.AddWithValue("@marf", Convert.ToInt16(textBox24.Text));
                    cmd.Parameters.AddWithValue("@share", Convert.ToInt16(textBox23.Text));
                    cmd.Parameters.AddWithValue("@total", Convert.ToInt16(textBox25.Text));
                    cmd.Parameters.AddWithValue("@photo", arr);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Information saved successfully");
                }

            }
            }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox15.Text == "")
            {
            }
            else if (Convert.ToInt16(textBox15.Text)<18)
            {
                temp = 1;
                label30.Text="DOB*";
                label31.Text = "Guardian Appointer *";
            }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            using(SqlConnection conn=new SqlConnection(connectionString))
            {
                
                    SqlCommand cmd1 = new SqlCommand("SELECT branchname FROM branchmasterdetails WHERE branchcode='"+comboBox2.Text+"'");
                    SqlCommand cmd2 = new SqlCommand("SELECT region FROM branchmasterdetails WHERE branchcode='" + comboBox2.Text + "'");
                    SqlCommand cmd3 = new SqlCommand("SELECT zone FROM branchmasterdetails WHERE branchcode='" + comboBox2.Text + "'");
                    SqlCommand cmd4 = new SqlCommand("SELECT state FROM branchmasterdetails WHERE branchcode='" + comboBox2.Text + "'");
                    SqlDataReader bnameRdr = null;
                    SqlDataReader regionRdr = null;
                    SqlDataReader zoneRdr = null;
                    SqlDataReader stateRdr = null;
                    conn.Open();
                    cmd1.Connection = conn;
                    cmd2.Connection = conn;
                    cmd3.Connection = conn;
                    cmd4.Connection = conn;
                    bnameRdr = cmd1.ExecuteReader();
                    regionRdr = cmd2.ExecuteReader();
                    zoneRdr = cmd3.ExecuteReader();
                    stateRdr = cmd4.ExecuteReader();
                    
                    while (bnameRdr.Read())
                    {
                       textBox8.Text = bnameRdr["branchname"].ToString();
                    }
                    
                    while (regionRdr.Read())
                    {
                        textBox26.Text = regionRdr["region"].ToString();

                    }
                    while (zoneRdr.Read())
                    {
                        textBox27.Text = zoneRdr["zone"].ToString();
                    }
                    while (stateRdr.Read())
                    {
                         textBox28.Text= stateRdr["state"].ToString();
                    }                    
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            age_calc();
            fyear = dateTimePicker2.Value.Year + 60;
            fmonth = dateTimePicker2.Value.Month;
            fday = dateTimePicker2.Value.Day;
            DateTime fdatetime = new DateTime(fyear, fmonth, fday);
            DateTime thisday = DateTime.Today;
            DateDifference(thisday, fdatetime);
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            age_calcn();
        }

        protected bool phone_vldn()
        {
            string s = textBox7.Text;
            int result;
            if (s == "")
            {
                return true;
            }
            else if (int.TryParse(s, out result))
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }

        private void button9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            textBox1.Text = "";
            textBox3.Text = ""; 
            textBox4.Text = ""; 
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox19.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            textBox28.Text = "";
            dateTimePicker1.Text = DateTime.Today.ToString();
            dateTimePicker2.Text = DateTime.Today.ToString();
            dateTimePicker3.Text = DateTime.Today.ToString();
            dateTimePicker4.Text = DateTime.Today.ToString();
            dateTimePicker5.Text = DateTime.Today.ToString();
            pictureBox1.Image = null;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            findform newff = new findform();
            newff.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            findform newff = new findform();
            newff.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            findform newff = new findform();
            newff.Show();
        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            findform ff = new findform();
            ff.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op2 = new OpenFileDialog();
            op2.Multiselect = true;
            if (op2.ShowDialog() == DialogResult.OK)
            {
                foreach (string filename in op2.FileNames)
                {
                    textBox20.Text = textBox20.Text + "||" + filename;
                    byte[] FileData = File.ReadAllBytes(filename);
                    if (textBox1.Text == "")
                        MessageBox.Show("enter details first");
                    else
                    {
                        using (SqlConnection con = new SqlConnection(connectionString))
                        {
                            SqlCommand cmd = new SqlCommand("insert into documents (indx,name,data) values(@indx,@name,@data)");
                            cmd.Parameters.AddWithValue("@indx", textBox1.Text);
                            cmd.Parameters.AddWithValue("@name", filename);
                            cmd.Parameters.AddWithValue("@data", FileData);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("some error");
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
                    firstletter = textBox3.Text.Substring(0, 1);
                    using (SqlConnection conc = new SqlConnection(connectionString))
                    {
                        SqlCommand cmd = new SqlCommand("select max(substring(indx,2,5)) as indx from memberdetails where fullname like '" + textBox3.Text.Substring(0, 1) + "%'");
                        SqlDataReader cmdRdr = null;
                        conc.Open();
                        cmd.Connection = conc;
                        cmdRdr = cmd.ExecuteReader();
                        while (cmdRdr.Read())
                        {
                            textBox1.Text = cmdRdr["indx"].ToString();
                        }
                        if (textBox1.Text == "" || textBox1.Text == null)
                            textBox1.Text = firstletter + "001";
                        else
                        {
                            z = Convert.ToInt16(textBox1.Text.Substring(1));
                            if (z > 9 && z < 99)
                            {
                                z = z + 1;
                                textBox1.Text = firstletter + "0" + z.ToString();
                            }
                            else if (z <= 9)
                            {
                                y = z + 1;
                                textBox1.Text = firstletter + "00" + y.ToString();
                            }
                        }
                    }
        }

        
        
               
    }
}
