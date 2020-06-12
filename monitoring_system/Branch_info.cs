using System;
using System.Collections;
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
    public partial class Branch_info : Form
    {
        SqlConnection con = new SqlConnection(Form1.constring);
        string branch = main_landing_page.branchstring;
        ArrayList valuesList = new ArrayList();

        public Branch_info()
        {
            InitializeComponent();
        }

        private void Branch_info_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.branch1' table. You can move, or remove it, as needed.
            this.branch1TableAdapter.Fill(this.database1DataSet.branch1);
            // TODO: This line of code loads data into the 'database1DataSet.branch1logs' table. You can move, or remove it, as needed.
            this.branch1logsTableAdapter.Fill(this.database1DataSet.branch1logs);
            lblbranch.Text = branch;
            loadproducts();
            loadlogs();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //navigate back to admin landing page
            main_landing_page blp = new main_landing_page();
            blp.Show();
            this.Hide();
        }
        private void loadlogs()
        {
            
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Date,Product FROM " + branch + "logs";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();

            
        }
        private void loadproducts()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Product_name,Product_stock,Products_sold FROM " + branch + "";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }
    }
}
