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
    public partial class Register_product : Form
    {
        SqlConnection con = new SqlConnection(Form1.constring);
        public Register_product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string bramch = tbbranch.Text;
            try
            {
                DialogResult res = MessageBox.Show("Register this new product to branch "+bramch+"", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == DialogResult.OK)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into " + bramch + " values('" + tbname.Text + "', '" + tbdescription.Text + "','0','" + tbprice.Text + "','0')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    tbbranch.Text = "";
                    tbprice.Text = "";
                    tbdescription.Text = "";
                    tbname.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            main_landing_page blp = new main_landing_page();
            blp.Show();
            this.Hide();
        }
    }
}
