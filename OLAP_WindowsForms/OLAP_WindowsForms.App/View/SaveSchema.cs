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
    public partial class SaveSchema : Form
    {
        public UserInput ui;

        public SaveSchema(UserInput ui)
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
                if (ui.overrideSchema)
                {
                    // Display a MsgBox asking the user to override or save as new schema
                    if (MessageBox.Show("Do you want to override your Schema with your new selection?\nYes = override\nNo = create new Schema", "Override Schema?",
                       MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        
                        ui.updateDB();
                        this.Close();
                        return;
                    } 
                }
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
