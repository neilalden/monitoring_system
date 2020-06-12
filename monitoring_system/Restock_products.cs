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
    public partial class Restock_products : Form
    {
        SqlConnection con = new SqlConnection(Form1.constring);
        //the branch of the logged in employee will open their branch's db
        string branch = Form1.branchstring;
        public Restock_products()
        {
            InitializeComponent();
        }

        private void Restock_products_Load(object sender, EventArgs e)
        {
            load();
        }
        private void load()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Product_name FROM " + branch + "";
            SqlDataReader dataReader = cmd.ExecuteReader();
            ArrayList valuesList = new ArrayList();
            while (dataReader.Read())
            {
                valuesList.Add(dataReader[0].ToString());
            }
            con.Close();
            foreach (string item in valuesList)
            {
                //generating a label name of a product
                Label l = new Label();
                l.Name = item;
                l.Text = item;
                l.Font = new Font("Montserrat", 14f, FontStyle.Underline);
                l.Cursor = Cursors.Hand;
                l.Margin = new Padding(3);
                l.Size = new Size(150, 30);
                l.Location = new Point(257, (51) + 25);
                l.Click += new EventHandler(l_Click);
                flowLayoutPanel1.Controls.Add(l);
            }
        }
        // onclick event for the list of products
        protected void l_Click(object sender, EventArgs e)
        {
            try
            {
                // show the selected product from the list for the user to buy
                string selected = ((Label)sender).Name;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM " + branch + " where Product_name = '" + selected + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader dr = cmd.ExecuteReader();
                lblstock.Visible = true;
                if (dr.Read())
                {
                    lblselected.Text = (dr["Product_name"].ToString());
                    lblstock.Text = (dr["Product_stock"].ToString());
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void lbllogs_Click(object sender, EventArgs e)
        {
            Logs_page lp = new Logs_page();
            lp.Show();
            this.Hide();
        }

        private void lblcashier_Click(object sender, EventArgs e)
        {
            Branch_landing_page lp = new Branch_landing_page();
            lp.Show();
            this.Hide();
        }

        private void tbamount_TextChanged(object sender, EventArgs e)
        {
            if (lblselected.Text == "Select a product" || lblselected.Text == "")
            {

                tbamount.Text = "";
            }
            else
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(tbamount.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter only numbers.");
                    tbamount.Text = tbamount.Text.Remove(tbamount.Text.Length - 1);
                }
            }
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            string datestring = DateTime.Now.ToString("yyyy-MMM-dd");
            try
            {
                int x = int.Parse(lblstock.Text) + int.Parse(tbamount.Text);

                // update or restock the product stocks
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update " + branch + " SET Product_stock = '" + x + "' where Product_name = '" + lblselected.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("success!");
                // register the actiom to logs
                con.Open();
                SqlCommand ccmd = con.CreateCommand();
                ccmd.CommandType = CommandType.Text;
                ccmd.CommandText = "insert into " + branch + "logs values('" + datestring + "', 'restock','" + lblselected.Text + "','','')";
                ccmd.ExecuteNonQuery();
                con.Close();
                lblselected.Text = "Select a product";
                lblstock.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }
    }
}
