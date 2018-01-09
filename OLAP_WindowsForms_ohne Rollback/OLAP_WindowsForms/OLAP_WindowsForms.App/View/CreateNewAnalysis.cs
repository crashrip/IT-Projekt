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
    public partial class CreateNewAnalysis : Form
    {
        public CreateNewAnalysis()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            LinkedList<Insert_item> list = new LinkedList<Insert_item>();

            // save ASS_SID 
            DataTable dt = DBContext.Service().GetData(
              "SELECT MAX(AGS_SID) FROM AGS_ANALYSIS_Graph_SCHEMA");

            DataTable dt2 = dt.Copy();
            DataRow[] dr = dt2.Select();
            String index = dr[0].ItemArray[0].ToString();
            int ags_sid = Int32.Parse(index)+1;
            Console.WriteLine(ags_sid);

            // AGS_ANALYSIS_GRAPH_SCHEMA
            list.AddFirst(new Insert_item("AGS_SID", ags_sid));
            list.AddLast(new Insert_item("AGS_NAME", AGS_NAME.Text));
            list.AddLast(new Insert_item("AGS_DESCRIPTION", AGS_DESCRITPION.Text));
            DBContext.Service().insertWithoutPKtest("AGS_ANALYSIS_GRAPH_SCHEMA", list);

            // enter userinput -> define schema
            this.Hide();
            this.Close();
            UserInput userinput = new UserInput(ags_sid);
            userinput.ShowDialog();
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?\nNothing will be saved!", "New Analysis-Schema",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
