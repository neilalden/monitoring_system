using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace monitoring_system
{
    public partial class Register_employee : Form
    {
        SqlConnection con = new SqlConnection(Form1.constring);
        public Register_employee()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            main_landing_page blp = new main_landing_page();
            blp.Show();
            this.Hide();
        }

        private void Register_employee_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbpass.Text == tbconfirmpass.Text) {
                    DialogResult res = MessageBox.Show("Register new employee?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.OK)
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into users values('" + tbname.Text + "', '" + tbcontactnumber.Text + "','" + tbconfirmpass.Text + "','employee','" + tbbranch.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        tbbranch.Text = "";
                        tbconfirmpass.Text = "";
                        tbpass.Text = "";
                        tbname.Text = "";
                        tbcontactnumber.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Password does not match!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
