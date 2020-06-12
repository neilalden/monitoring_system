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
    public partial class Branch_landing_page : Form
    {

        SqlConnection con = new SqlConnection(Form1.constring);
        //the branch of the logged in employee will open their branch's db
        string branch = Form1.branchstring;
        //data to show to the cart
        Hashtable cart = new Hashtable();
        //data to update the database
        Hashtable db = new Hashtable();
        // get current date
        string datestring = DateTime.Now.ToString("yyyy-MMM-dd");
        public Branch_landing_page()
        {
            InitializeComponent();
        }

        private void Branch_landing_page_Load(object sender, EventArgs e)
        {
            label5.Text = branch +"   -  " +label5.Text;
            // on load, generate a list off all available products from the branch
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
                l.Location = new Point(257, (51 ) + 25);
                l.Click += new EventHandler(l_Click);
                flowLayoutPanel1.Controls.Add(l);
            }


        }
        // onclick event for the list of products
        protected void l_Click(object sender, EventArgs e)
        {
            try {
                // show the selected product from the list for the user to buy
                string selected = ((Label)sender).Name;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM " + branch + " where Product_name = '" + selected+"'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader dr = cmd.ExecuteReader();
                lblprice.Visible = true;
                lblstock.Visible = true;
                if (dr.Read()) {
                    lblselected.Text = (dr["Product_name"].ToString());
                    lblprice.Text = (dr["Product_price"].ToString());
                    lblstock.Text = (dr["Product_stock"].ToString());
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void lblrestock_Click(object sender, EventArgs e)
        {

            Restock_products rp = new Restock_products();
            rp.Show();
            this.Hide();
        }

        private void lbllogs_Click(object sender, EventArgs e)
        {
            Logs_page lp = new Logs_page();
            lp.Show();
            this.Hide();    
        }

        // check and remove non intiger values from the amount text box
        private void tbamount_TextChanged(object sender, EventArgs e)
        {
            if (lblselected.Text == "Select a product" || lblselected.Text == "") {
                
                tbamount.Text = "";
            }
            else {
                if (System.Text.RegularExpressions.Regex.IsMatch(tbamount.Text, "[^0-9]"))
                {
                    MessageBox.Show("Please enter only numbers.");
                    tbamount.Text = tbamount.Text.Remove(tbamount.Text.Length - 1);
                }
            }
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            try
            {
                // when no product is selected from the list, do nothing
                if (lblselected.Text == "Select a product" || lblselected.Text == "")
                {

                }
                else
                {
                    // store all bought products in a hashtable
                    string slct = lblselected.Text;
                    int amnt = int.Parse(tbamount.Text);
                    float z = float.Parse(lblprice.Text) * amnt;
                    if (cart.Contains(slct))
                    {
                        cart[slct] = amnt + (int)cart[slct];
                        db[slct] = amnt + (int)cart[slct];
                    }
                    else
                    {
                        cart.Add(slct+"  x  "+amnt+"  =", z);
                        db.Add(slct,amnt);
                    }
                    Populate_cart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void Populate_cart()
        {
            // show the bought products that is stored in the hastable of the customer in a list
            flowLayoutPanel2.Controls.Clear();
            float total = 0;
            foreach (DictionaryEntry item in cart)
            {
                Label l = new Label();
                l.Name = item.Key.ToString();
                l.Text = item.Key.ToString() + "    "+item.Value;
                l.Font = new Font("Montserrat", 12f, FontStyle.Regular);
                l.Size = new Size(250, 30);
                l.Margin = new Padding(3);
                l.Location = new Point(257, (51) + 25);
                l.Click += new EventHandler(l_Click);
                flowLayoutPanel2.Controls.Add(l);
                total += float.Parse(item.Value.ToString());
            }
            lblselected.Text = "Select a product";
            lblprice.Text = "";
            lblstock.Text = "";
            tbamount.Text = "";
            lbltotal.Text = total.ToString();
        }
        // check out the customer and update the database
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Do you want to finish transaction?", "Are you sure?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == DialogResult.OK)
            {
                // for storing products data from db 
                Hashtable getdatafromdb = new Hashtable();
                
                    // loop through bought products
                    foreach (DictionaryEntry item in db)
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM " + branch + " where Product_name = '" + item.Key.ToString() + "'";
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            // get and store data of the bought products
                            getdatafromdb.Add((dr["Product_stock"].ToString()), (dr["Products_sold"].ToString()));
                        }
                        con.Close();
                        // update the stocks and sold products in the db
                        foreach (DictionaryEntry data in getdatafromdb)
                        {
                            int x = int.Parse(data.Key.ToString()) - int.Parse(item.Value.ToString());
                            int y = int.Parse(data.Value.ToString()) + int.Parse(item.Value.ToString());
                            con.Open();
                            SqlCommand cmnd = con.CreateCommand();
                            cmnd.CommandType = CommandType.Text;
                            cmnd.CommandText = "update " + branch + " SET Product_stock = '" + x + "', Products_sold = '" + y + "' where Product_name = '" + item.Key.ToString() + "'";
                            cmnd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    logentry();
                    flowLayoutPanel2.Controls.Clear();
                    lbltotal.Text = "00.00";
               
            }
        }
        private void logentry()
        {
            //save transaction log to db
            string currentmonth = DateTime.Now.ToString("yyyyMM");
            foreach (DictionaryEntry item in cart) {
                string placeholder = item.Key.ToString().Remove(item.Key.ToString().Length - 2);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into [dbo].[" + branch + "logs] values('" +datestring +"', 'sold','" + placeholder + "','"+currentmonth+"','"+lbltotal.Text+"')";
                cmd.ExecuteNonQuery();
                con.Close();
            }
            cart.Clear();
            db.Clear();
        }

        // switch account or log out
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
