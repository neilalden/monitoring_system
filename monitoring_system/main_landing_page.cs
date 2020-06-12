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
    public partial class main_landing_page : Form
    {
        string currentmonth = DateTime.Now.ToString("yyyyMM");
        SqlConnection con = new SqlConnection(Form1.constring);
        public static string branchstring;

        public main_landing_page()
        {
            InitializeComponent();
        }

        private void main_landing_page_Load(object sender, EventArgs e)
        {
            branch1sales();
        }
        private void branch1sales()
        {
            ArrayList storecurrentmonthsales = new ArrayList();
            ArrayList storelastmonthsales = new ArrayList();

            float total = 0;
            float ttotal = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Sales FROM branch1logs where Current_month = '" + currentmonth + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader dr = cmd.ExecuteReader();
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        storecurrentmonthsales.Add(dr.GetValue(i).ToString());
                    }
                }
                con.Close();
                foreach (object item in storecurrentmonthsales)
                {
                    string holder = (string)item;
                    total += float.Parse(holder);
                }
                branch1current.Text = branch1current.Text + total.ToString();

                //re-running the same commands but this time we're getting last months sales
                int container = int.Parse(currentmonth) - 1;
                con.Open();
                SqlCommand ccmd = con.CreateCommand();
                ccmd.CommandType = CommandType.Text;
                ccmd.CommandText = "SELECT Sales FROM branch1logs where Current_month = '" + container + "'";
                ccmd.ExecuteNonQuery();
                SqlDataAdapter dda = new SqlDataAdapter(ccmd);
                SqlDataReader ddr = ccmd.ExecuteReader();
                int ccount = ddr.FieldCount;
                while (ddr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        storelastmonthsales.Add(ddr.GetValue(i).ToString());
                    }
                }
                con.Close();
                foreach (object item in storelastmonthsales)
                {
                    string holder = (string)item;
                    ttotal += float.Parse(holder);
                }
                branch1last.Text = branch1last.Text + ttotal.ToString();
                con.Close();
                branch2();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void branch2()
        {
            ArrayList storecurrentmonthsales = new ArrayList();
            ArrayList storelastmonthsales = new ArrayList();

            float total = 0;
            float ttotal = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Sales FROM branch2logs where Current_month = '" + currentmonth + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader dr = cmd.ExecuteReader();
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        storecurrentmonthsales.Add(dr.GetValue(i).ToString());
                    }
                }
                con.Close();
                foreach (object item in storecurrentmonthsales)
                {
                    string holder = (string)item;
                    total += float.Parse(holder);
                }
                branch2current.Text = branch2current.Text + total.ToString();


                //re-running the same commands but this time we're getting last months sales
                int container = int.Parse(currentmonth) - 1;
                con.Open();
                SqlCommand ccmd = con.CreateCommand();
                ccmd.CommandType = CommandType.Text;
                ccmd.CommandText = "SELECT Sales FROM branch1logs where Current_month = '" + container + "'";
                ccmd.ExecuteNonQuery();
                SqlDataAdapter dda = new SqlDataAdapter(ccmd);
                SqlDataReader ddr = ccmd.ExecuteReader();
                int ccount = ddr.FieldCount;
                while (ddr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        storelastmonthsales.Add(ddr.GetValue(i).ToString());
                    }
                }
                con.Close();
                foreach (object item in storelastmonthsales)
                {
                    string holder = (string)item;
                    ttotal += float.Parse(holder);
                }
                branch2last.Text = branch2last.Text + ttotal.ToString();
                con.Close();
                branch3();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void branch3()
        {
            ArrayList storecurrentmonthsales = new ArrayList();
            ArrayList storelastmonthsales = new ArrayList();

            float total = 0;
            float ttotal = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Sales FROM branch1logs where Current_month = '" + currentmonth + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                SqlDataReader dr = cmd.ExecuteReader();
                int count = dr.FieldCount;
                while (dr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        storecurrentmonthsales.Add(dr.GetValue(i).ToString());
                    }
                }
                con.Close();
                foreach (object item in storecurrentmonthsales)
                {
                    string holder = (string)item;
                    total += float.Parse(holder);
                }
                branch3current.Text = branch3current.Text + total.ToString();

                //re-running the same commands but this time we're getting last months sales
                int container = int.Parse(currentmonth) - 1;
                con.Open();
                SqlCommand ccmd = con.CreateCommand();
                ccmd.CommandType = CommandType.Text;
                ccmd.CommandText = "SELECT Sales FROM branch1logs where Current_month = '" + container + "'";
                ccmd.ExecuteNonQuery();
                SqlDataAdapter dda = new SqlDataAdapter(ccmd);
                SqlDataReader ddr = ccmd.ExecuteReader();
                int ccount = ddr.FieldCount;
                while (ddr.Read())
                {
                    for (int i = 0; i < count; i++)
                    {
                        storelastmonthsales.Add(ddr.GetValue(i).ToString());
                    }
                }
                con.Close();
                foreach (object item in storelastmonthsales)
                {
                    string holder = (string)item;
                    ttotal += float.Parse(holder);
                }
                branch3last.Text = branch3last.Text + ttotal.ToString();
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        // opens to a page where you monitor the products of the branches
        private void lblbranch3_Click(object sender, EventArgs e)
        {
            branchstring = "branch3";
            opensesame();
        }

        private void lblbranch2_Click(object sender, EventArgs e)
        {
            branchstring = "branch2";
            opensesame();

        }

        private void lblbranch1_Click(object sender, EventArgs e)
        {
            branchstring = "branch1";
            opensesame();
        }
        public void opensesame()
        {
            Branch_info bi = new Branch_info();
            bi.Show();
            this.Hide();
        }
        //navigates to the selected pages
        private void lblregisterproduct_Click(object sender, EventArgs e)
        {
            Register_product rp = new Register_product();
            rp.Show();
            this.Hide();
        }

        private void lblregisteremployee_Click(object sender, EventArgs e)
        {
            Register_employee re = new Register_employee();
            re.Show();
            this.Hide();
        }

        private void lbldelete_Click(object sender, EventArgs e)
        {
            Delete_product dp = new Delete_product();
            dp.Show();
            this.Hide();
        }
    }
}
