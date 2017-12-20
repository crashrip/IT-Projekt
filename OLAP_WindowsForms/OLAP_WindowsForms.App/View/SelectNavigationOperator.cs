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
    public partial class SelectNavigationOperator : Form
    {
        private UserInput userInput;
        private string currentSelection;

        // saves the navigation operators and corresponding tables as strings
        Dictionary<string, string> AGS_NAVSTEP_SCHEMA = new Dictionary<string, string>();

        public SelectNavigationOperator(UserInput p)
        {
            userInput = p;
            InitializeComponent();
            ComboItem.GetComboboxContent(comboBoxNav, "AGS_NAVSTEP_SCHEMA", "NAVSS_OPNAME");
            ComboItem.GetComboboxContent(ComboBoxCube, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
        }

        // on selection of combo box
        private void comboBoxNav_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSelection = comboBoxNav.Text;

            if (currentSelection == "drillAcrossToCube")
            {
                ComboBoxCube.Visible = true;
            }
            else
            {
                ComboBoxCube.Visible = false;
            }
        }
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "Select Navigation Operator",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (currentSelection == "drillAcrossToCube")
            {
                userInput.SelectComboBoxCube(ComboBoxCube.Text);
            }
            this.Close();
        }

        private void FillDictionaryAGS_NAVSTEP_SCHEMA()
        {
            AGS_NAVSTEP_SCHEMA.Add("drillAcrossToCube", "AGS_NAVSS_DRILL_ACROSS_TO_CUBE");
            AGS_NAVSTEP_SCHEMA.Add("moveToPrevNode", "AGS_NAVSS_MOVE_TO_PREV_NODE");
            AGS_NAVSTEP_SCHEMA.Add("relate", "AGS_NAVSS_RELATE");
        }

        private void ComboBoxCube_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("ComboboxCube.text=" + ComboBoxCube.Text);
        }
    }
}
