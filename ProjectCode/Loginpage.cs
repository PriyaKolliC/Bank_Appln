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
    public partial class Loginpage : Form
    {
        string connectionString = "Data Source =.;Initial Catalog=SBH;Uid=sa;pwd=123;MultipleActiveResultSets=True;";
        public Loginpage()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("Please enter valid details");
            else
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand cmd = new SqlCommand("SELECT * FROM LOGIN WHERE USERNAME='" + textBox1.Text + "' AND PASSWRD='" + textBox2.Text + "'");
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = connection;
                    connection.Open();
                    SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adapt.Fill(ds);
                    connection.Close();
                    int count = ds.Tables[0].Rows.Count;
                    if (count == 1)
                    {
                        this.Hide();
                        Homepage frm = new Homepage();
                       frm.Show();
                        //checkposs frm = new checkposs();
                        //frm.Show();
                        
                        }
                    else
                    {
                        MessageBox.Show("Invalid login details");
                    }
                }
            }
        }
    }
}

