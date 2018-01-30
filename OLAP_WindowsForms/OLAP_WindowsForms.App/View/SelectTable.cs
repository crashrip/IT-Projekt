using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App.View
{
    public partial class SelectTable : Form
    {
        public SelectTable()
        {
            InitializeComponent();
        }

        //Cancel Button
        private void button2_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "Select table",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
