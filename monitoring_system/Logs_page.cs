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

namespace monitoring_system
{
    public partial class Logs_page : Form
    {
        SqlConnection con = new SqlConnection(Form1.constring);
        string branch = Form1.branchstring;

        public Logs_page()
        {
            InitializeComponent();
        }

        private void lblcashier_Click(object sender, EventArgs e)
        {
            Branch_landing_page blp = new Branch_landing_page();
            blp.Show();
            this.Hide();
        }

        private void Logs_page_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.branch3logs' table. You can move, or remove it, as needed.
            this.branch3logsTableAdapter.Fill(this.database1DataSet.branch3logs);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Date,Action,Product FROM "+branch+"logs";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void lblrestock_Click(object sender, EventArgs e)
        {
            Restock_products rp = new Restock_products();
            rp.Show();
            this.Hide();
        }
    }
}
