using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            // initialize text
            textBox_hostname.Text = "localhost";
            textBox_port.Text = "5432";
            textBox_username.Text = "postgres";
            /*textBox_password.Text = "k1556077";*/ // alex
            textBox_password.Text = "1556474"; // maria
            textBox_dbName.Text = "postgres";
        }

        private void button_submit_Click(object sender, EventArgs e)
        {
            string hostname = textBox_hostname.Text;
            string port = textBox_port.Text;
            string username = textBox_username.Text;
            string password = textBox_password.Text;
            string dbName = textBox_dbName.Text;

            DBContext.Service().LogIn(hostname, port, username, password, dbName);

            try
            {
                string connectionString =
                    String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};",
                    hostname, port, username, password, dbName);
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wrong login data. The program will be closed.", "Connection failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}