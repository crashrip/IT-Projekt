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
    public partial class Save : Form
    {
        public UserInput ui;

        public Save(UserInput ui)
        {
            this.ui = ui;
            InitializeComponent();
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if(schema_name.Text.Equals("") || schema_name.Text == null)
            {
                MessageBox.Show("You have to enter a name first!","Save failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else
            {
                Console.WriteLine("worked");
                ui.name = schema_name.Text;
                ui.description = description_text.Text;
                ui.insert();
                this.Close();
            }
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
