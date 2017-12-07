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
    public partial class BaseMenu : Form
    {
        public BaseMenu()
        {
            InitializeComponent();

            // show login dialog
            Login login = new Login() { TopMost = true };
            login.ShowDialog(this);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            UserInput userinput = new UserInput() { TopMost = true };
            userinput.ShowDialog(this);
        }
    }
}
