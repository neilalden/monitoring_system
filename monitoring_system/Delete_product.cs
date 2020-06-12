using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace monitoring_system
{
    public partial class Delete_product : Form
    {
        SqlConnection con = new SqlConnection(Form1.constring);
        public Delete_product()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            main_landing_page blp = new main_landing_page();
            blp.Show();
            this.Hide();
        }

        private void Delete_product_Load(object sender, EventArgs e)
        {
            load();
        }
        private void load()
        {
            flowLayoutPanel1.Controls.Clear();
            Hashtable valuesList = new Hashtable();
            int i = 0;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Product_name FROM branch1";
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                valuesList.Add("branch1" + i, dataReader[0].ToString());
                i++;
            }
            con.Close();
            con.Open();
            SqlCommand ccmd = con.CreateCommand();
            ccmd.CommandType = CommandType.Text;
            ccmd.CommandText = "SELECT Product_name FROM branch2";
            SqlDataReader dataReaderr = ccmd.ExecuteReader();
            while (dataReaderr.Read())
            {
                valuesList.Add("branch2" + i, dataReaderr[0].ToString());
                i++;
            }
            con.Close();
            con.Open();
            SqlCommand cccmd = con.CreateCommand();
            cccmd.CommandType = CommandType.Text;
            cccmd.CommandText = "SELECT Product_name FROM branch3";
            SqlDataReader dataReaderrr = cccmd.ExecuteReader();
            while (dataReaderrr.Read())
            {
                valuesList.Add("branch3" + i, dataReaderrr[0].ToString());
                i++;
            }
            con.Close();
            foreach (DictionaryEntry item in valuesList)
            {
                //generating a label name of a product
                Label l = new Label();
                l.Name = item.Key.ToString();
                l.Text = item.Value.ToString();
                l.Font = new Font("Montserrat", 14f, FontStyle.Underline);
                l.Cursor = Cursors.Hand;
                l.Margin = new Padding(3);
                l.Size = new Size(150, 30);
                l.Location = new Point(257, (51) + 25);
                l.Click += new EventHandler(l_Click);
                flowLayoutPanel1.Controls.Add(l);
            }

        }
        // onclick event for deleting the selected product
        protected void l_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = ((Label)sender).Text;
                string ddb = ((Label)sender).Name;
                string db = ddb.Remove(ddb.Length - 1);
                DialogResult result = MessageBox.Show("Are you sure you want to permanently delete this item?", "delete item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM "+db+" WHERE Product_name ='" + selected + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    load();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
