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
    public partial class Form1 : Form
    {
        // storing string data source of the database, declared globally para ma access ng ibang class
        public const string constring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lb\source\repos\monitoring_system\monitoring_system\Database1.mdf;Integrated Security=True";
        // storing the role and branch of the user that logged in to navigate to their db
        public static string rolestring, branchstring;
        // connecting to the database
        SqlConnection con = new SqlConnection(constring);

        string username, password;

        public Form1()
        {
            InitializeComponent();
        }

        private void login_Click(object sender, EventArgs e)
        {
            loginq();
        }
        private void loginq()
        {
            try
            {
                // getting the username and password input of the user
                username = tbusername.Text;
                password = tbpassword.Text;
                // openning the connection to database
                con.Open();
                // creating and running the command
                SqlCommand cmd = new SqlCommand("select Full_name,Password from [dbo].[users] where Full_name ='" + username + "'and password ='" + password + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                // checking if the user exist
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    // closing the connection to run a new opearation
                    con.Close();
                    Get_role();
                }
                else
                {
                    con.Close();
                    // either username and password input does not exist or is incorrect 
                    MessageBox.Show("Invalid Login please check username and password", "error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Get_role()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                // same command, getting the username information 
                cmd.CommandText = "SELECT * FROM [dbo].[users] where Full_name ='" + username + "'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // reading the userinfo
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // getting the role of the user to redirect user properly
                    rolestring = (dr["Role"].ToString());
                    branchstring = (dr["Branch"].ToString());
                    // if role is emploee or the user is from the 3 branches
                    // open branch landing page
                    if (rolestring == "employee")
                    { 
                        Branch_landing_page blp = new Branch_landing_page();
                        blp.Show();
                    }
                    // if role is admin or the user is from the main branch
                    // open main branch landing page
                    else if(rolestring == "admin")
                    {
                        main_landing_page mlp = new main_landing_page();
                        mlp.Show();
                    }
                    this.Hide();
                }
                con.Close();
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString(), "error!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
