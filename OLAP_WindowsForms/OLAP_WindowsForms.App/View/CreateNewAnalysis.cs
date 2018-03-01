using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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

            //prepare Connection
            NpgsqlConnection connection = DBContext.Service().GetConnection();
            connection.Open();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
            try { 
            // save ASS_SID 
            DataTable dt = DBContext.Service().GetData(
              "SELECT MAX(AGS_SID) FROM AGS_ANALYSIS_Graph_SCHEMA");

            DataTable dt2 = dt.Copy();
            DataRow[] dr = dt2.Select();
            
            String index = dr[0].ItemArray[0].ToString();
                int ags_sid = 1;
                if (!(index.Equals("") || index == null)) // if list is empty
                {
                    ags_sid = Int32.Parse(index) + 1;
                }
                //Console.WriteLine("new index: " + index+" index int: "+ags_sid);

            // AGS_ANALYSIS_GRAPH_SCHEMA
            list.AddFirst(new Insert_item("AGS_SID", ags_sid));
            list.AddLast(new Insert_item("AGS_NAME", AGS_NAME.Text));
            list.AddLast(new Insert_item("AGS_DESCRIPTION", AGS_DESCRITPION.Text));
            DBContext.Service().InsertWithoutPK(connection,transaction,"AGS_ANALYSIS_GRAPH_SCHEMA", list);

            transaction.Commit();
            Console.WriteLine("Transaction sucessful");
            // enter userinput -> define schema
            this.Hide();
            this.Close();
            UserInput userinput = new UserInput(ags_sid,true);
            userinput.ShowDialog();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
                try
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back");
                }
                catch (SqlException ex2)
                {
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
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
