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
    public partial class settlement : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        string type,times,mbf,minA,maxA;
        string bnft;
        public settlement()
        {
            InitializeComponent();
        }

        private void settlement_Load(object sender, EventArgs e)
        {
            fill_combo();
        }
        protected void fill_combo()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string item = "eligible";
                string query = "select ApplnNo from death_form where status='"+item+"' order by ApplnNo";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                cmd.ExecuteNonQuery();
                con.Close();
                comboBox1.DisplayMember = "ApplnNo";
                comboBox1.ValueMember = "ApplnNo";
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.Text = "--ApplnNo";
                comboBox1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_details();
        }
        protected void fill_details()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string item = comboBox1.SelectedValue.ToString();
                if (comboBox1.SelectedValue.ToString() == "--ApplnNo")
                {

                }
                else
                {
                    SqlCommand cmdnew = new SqlCommand("SELECT CONVERT(VARCHAR(10),DOD ,101) as DOD FROM death_form WHERE ApplnNo='" + item + "'");
                    
                    SqlCommand cmdnew2 = new SqlCommand("SELECT Type FROM death_form WHERE ApplnNo='" + item + "'");
                    SqlDataReader DODRdr = null;
                    SqlDataReader AmtRdr = null;
                    SqlDataReader FERdr = null;
                    SqlDataReader TypeRdr = null;
                    SqlDataReader timesRdr = null;
                    conn.Open();
                    cmdnew.Connection = conn;
                    cmdnew2.Connection = conn;
                    DODRdr = cmdnew.ExecuteReader();
                   
                   TypeRdr = cmdnew2.ExecuteReader();
                    while (DODRdr.Read())
                    {
                        textBox1.Text = DODRdr["DOD"].ToString();
                    }
                    SqlCommand cmdnew1 = new SqlCommand("SELECT FunExp FROM mbf_elg WHERE date<='" + Convert.ToDateTime(textBox1.Text) + "'");
                    cmdnew1.Connection = conn;
                    FERdr = cmdnew1.ExecuteReader();
                    while (FERdr.Read())
                    {
                        textBox2.Text = FERdr["FunExp"].ToString();
                    }
                    while (TypeRdr.Read())
                    {
                        type = TypeRdr["Type"].ToString();
                    }
                    if (String.Equals(type, "Accidental", StringComparison.Ordinal))
                    {
                        SqlCommand cmdx = new SqlCommand("SELECT AccdntlBnft FROM mbf_elg WHERE date<='" + Convert.ToDateTime(textBox1.Text) + "'");
                        SqlDataReader cmdxRdr = null;
                        cmdx.Connection = conn;
                        cmdxRdr = cmdx.ExecuteReader();
                        while (cmdxRdr.Read())
                        {
                            bnft = cmdxRdr["AccdntlBnft"].ToString();
                        }
                        textBox4.Text = bnft;
                    }
                    SqlCommand cmdnewx = new SqlCommand("SELECT times FROM mbf_elg WHERE date<='" + Convert.ToDateTime(textBox1.Text) + "'");
                    cmdnewx.Connection = conn;
                   timesRdr = cmdnewx.ExecuteReader();
                    while (timesRdr.Read())
                    {
                        times = timesRdr["times"].ToString();
                    }
                    //MessageBox.Show("'"+times+"'");
                    SqlDataReader mbfRdr = null;
                    SqlCommand cmdnewy = new SqlCommand("SELECT mbf FROM death_form WHERE ApplnNo='" + item + "'");
                    cmdnewy.Connection = conn;
                    mbfRdr = cmdnewy.ExecuteReader();
                    while (mbfRdr.Read())
                    {
                        mbf = mbfRdr["mbf"].ToString();
                    }
                    int ax = Convert.ToInt32(mbf) * Convert.ToInt32(times);
                    //MessageBox.Show("'" + mbf + "'");
                    //MessageBox.Show("'" + ax + "'");
                    SqlDataReader minARdr = null;
                    SqlCommand cmdnewz = new SqlCommand("SELECT MinAmt FROM mbf_elg WHERE date<='" + Convert.ToDateTime(textBox1.Text)+ "'");
                    cmdnewz.Connection = conn;
                    minARdr = cmdnewz.ExecuteReader();
                    while (minARdr.Read())
                    {
                        minA = minARdr["MinAmt"].ToString();
                    }
                   // MessageBox.Show("'" + minA+ "'");
                    SqlDataReader maxARdr = null;
                    SqlCommand cmdnewf = new SqlCommand("SELECT MaxAmt FROM mbf_elg WHERE date<='" + Convert.ToDateTime(textBox1.Text) + "'");
                    cmdnewf.Connection = conn;
                    maxARdr = cmdnewf.ExecuteReader();
                    while (maxARdr.Read())
                    {
                        maxA = maxARdr["MaxAmt"].ToString();
                    }
                    //MessageBox.Show("'" + maxA + "'");
                    if (ax >= Convert.ToInt64(minA) && ax < Convert.ToInt32(maxA))
                    {
                        textBox3.Text = ax.ToString();
                    }
                    else if (ax > Convert.ToInt32(maxA))
                    {
                        textBox3.Text = minA;
                    }
                    else if (ax == Convert.ToInt32(maxA))
                    {
                        textBox3.Text = maxA;
                    }
                    else if (ax <Convert.ToInt32(minA))
                    {
                        textBox3.Text = ax.ToString();
                    }
                    if (textBox4.Text == "")
                        textBox4.Text = "0";
                    if (textBox5.Text == "")
                        textBox5.Text = "0";
                    if (textBox3.Text == "")
                        textBox3.Text = "0";
                    
                    textBox5.Text = (Convert.ToInt32(textBox4.Text) + Convert.ToInt32(textBox5.Text) - Convert.ToInt32(textBox3.Text)).ToString();
                }
            }
        }
    }
}
