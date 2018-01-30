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

namespace OLAP_WindowsForms.App.View
{
    public partial class SelectNavigationOperator : Form
    {
        private UserInput userInput;
        private string currentSelection, currentCube;

        // saves the navigation operators and corresponding tables as strings
        Dictionary<string, string> AGS_NAVSTEP_SCHEMA_dictionary = new Dictionary<string, string>();

        public SelectNavigationOperator(UserInput p)
        {
            userInput = p;
            InitializeComponent();

            ComboItem.GetComboboxContent(comboBoxNav, "AGS_NAVSTEP_SCHEMA", "NAVSS_OPNAME");
            ComboItem.GetComboboxContent(ComboBoxCube, "DW_CUBE", "CUBE_SID", "CUBE_NAME");

            FillDictionary_AGS_NAVSTEP_SCHEMA();
        }

        // on selection of combo box
        private void comboBoxNav_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentSelection = comboBoxNav.Text;
            Console.WriteLine("[SUBMIT] Selection: " + currentSelection);

            if (currentSelection == "drillAcrossToCube")
            {
                ComboBoxCube.Visible = true;
            }
            else
            {
                ComboBoxCube.Visible = false;
            }
        }

        private void ComboBoxCube_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentCube = ComboBoxCube.Text;
            Console.WriteLine("[SUBMIT] Cube: " + currentCube);
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

            // userInput.SelectComboBoxCube(ComboBoxCube.Text);

            // insert into function table
            Console.WriteLine("[SUBMIT] Selection: " + currentSelection); // Current selection: drillAcrossToCube

            string table = null;
            if (AGS_NAVSTEP_SCHEMA_dictionary.ContainsKey(currentSelection))
            {
                 table = AGS_NAVSTEP_SCHEMA_dictionary[currentSelection];

                // TODO
            }

            Console.WriteLine("[SUBMIT] Table: " + table); // Table: AGS_NAVSS_DRILL_ACROSS_TO_CUBE

            Console.WriteLine("[SUBMIT] open connection");
            NpgsqlConnection connection = DBContext.Service().getConnection();
            connection.Open();

            Console.WriteLine("[SUBMIT] begin transaction");
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            Console.WriteLine("[SUBMIT] create sql statement");
            Console.WriteLine("[SUBMIT] currentSelection: " + currentSelection);
            String stmt = "SELECT NAVSS_SID FROM AGS_NAVSTEP_SCHEMA WHERE NAVSS_OPNAME = \'" + currentSelection + "\'";

            Console.WriteLine("[SUBMIT] get coulumnPK");
            int columnPK;
            Int32.TryParse(DBContext.Service().getStringFromStmt(stmt, 0, 0), out columnPK);

            Console.WriteLine("[SUBMIT] get cubeId");
            
            String columnNamePK = "NAVSS_SID";

            Console.WriteLine("[SUBMIT] create a list of items");
            LinkedList<Insert_item> list = new LinkedList<Insert_item>();

            Console.WriteLine("[SUBMIT] insert items");

            // START insertion
            if (table == "AGS_NAVSS_MOVE_TO_PREV_NODE")
            {
                Int32 dim_sid = Int32.Parse(DBContext.Service().getStringFromStmt("SELECT DIM_SID FROM DW_DIMENSION WHERE CUBE_NAME = \'" + currentCube + "\'", 0, 0));
                list.AddLast(new Insert_item("DIM_SID", dim_sid));

                userInput.SelectComboBoxCube(dim_sid);
                //userInput.load(); // load new UserInput form TODO
            }

            if (table == "AGS_NAVSS_DRILL_ACROSS_TO_CUBE") {
                Int32 cube_sid = Int32.Parse(DBContext.Service().getStringFromStmt("SELECT CUBE_SID FROM DW_CUBE WHERE CUBE_NAME = \'" + currentCube + "\'", 0, 0));
                list.AddLast(new Insert_item("CUBE_SID", cube_sid));
            }

            Console.WriteLine("[SUBMIT] calling function insinto(" +
                    connection + ", " + transaction + ", " + table + ", " + columnPK + ", " + columnNamePK + ", " + list + ")");

            // key exists already
            bool keyExistsAlready = false;
            DataTable dt = DBContext.Service().GetData("SELECT " + columnNamePK + " FROM " + table);
            DataRow[] dr = dt.Copy().Select();

            for (int i = 0; i < dr.Length; i++)
            {
                if (dr[i].ItemArray[0].Equals(columnPK))
                {
                    keyExistsAlready = true;
                }
            }

            if (keyExistsAlready)
            {
                Console.WriteLine("[SUBMIT] key exists already -> no insert was made");
            }
            else
            {
                DBContext.Service().insinto(connection, transaction, table, columnPK, columnNamePK, list);
            }
            // END insertion


            Console.WriteLine("[SUBMIT] finished");
            
            this.Close();
        }

        private void FillDictionary_AGS_NAVSTEP_SCHEMA()
        {
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillDownOneLevel", "AG_NAVS_DRILL_DOWN_ONE_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillDownToLevel", "AG_NAVS_DRILL_DOWN_TO_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("rollUpOneLevel", "AG_NAVS_ROLL_UP_ONE_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("rollUpToLevel", "AG_NAVS_ROLL_UP_TO_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveDownToNode", "AG_NAVS_MOVE_DOWN_TO_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveUpToNode", "AG_NAVS_MOVE_UP_TO_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveAsideToNode", "AG_NAVS_MOVE_ASIDE_TO_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToNode", "AG_NAVS_MOVE_TO_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToFirstNode", "AG_NAVS_MOVE_TO_FIRST_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToLastNode", "AG_NAVS_MOVE_TO_LAST_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToNextNode", "AG_NAVS_MOVE_TO_NEXT_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToPrevNode", "AGS_NAVSS_MOVE_TO_PREV_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("narrowSliceCond+", "AG_NAVS_NARROW_SLICE_COND_A");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("narrowSliceCond->", "AG_NAVS_NARROW_SLICE_COND_C");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("broadenSliceCond-", "AG_NAVS_BROADEN_SLICE_COND_A");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("broadenSliceCond->", "AG_NAVS_BROADEN_SLICE_COND_C");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusSliceCond", "AG_NAVS_REFOCUS_SLICE_COND");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusSliceCond->", "AG_NAVS_REFOCUS_SLICE_COND_C");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("narrowBMsrCond->", "AG_NAVS_NARROW_BMSR_COND_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("broadenBMsrCond->", "AG_NAVS_BROADEN_BMSR_COND_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusBMsrCond->", "AG_NAVS_REFOCUS_BMSR_COND_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusMeasure->", "AG_NAVS_REFOCUS_MEASURE_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveDownToMeasure", "AG_NAVS_MOVE_DOWN_TO_MEASURE_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveUpToMeasure", "AG_NAVS_MOVE_UP_TO_MEASURE_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("narrowAMsrFilter->", "AG_NAVS_NARROW_AMSR_FILTER_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("broadenAMsrFilter->", "AG_NAVS_BROADEN_AMSR_FILTER_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusAMsrFilter->", "AG_NAVS_REFOCUS_AMSR_FILTER_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillAcrossToCube", "AGS_NAVSS_DRILL_ACROSS_TO_CUBE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("relate", "AGS_NAVSS_RELATE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("target", "AG_NAVS_TARGET");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("rerelate", "AG_NAVS_RERELATE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("retarget", "AG_NAVS_RETARGET");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("correlate", "AG_NAVS_CORRELATE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("narrowScoreFilter", "AG_NAVS_NARROW_SCORE_FILTER_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("broadenScoreFilter", "AG_NAVS_BROADEN_SCORE_FILTER_C_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusScoreFilter", "AG_NAVS_REFOCUS_SCORE_FILTER_C_PARS");
        }
    }
}
