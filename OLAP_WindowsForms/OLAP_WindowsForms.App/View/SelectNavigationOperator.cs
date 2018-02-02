﻿using Npgsql;
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
    /// <summary>
    /// 
    /// </summary>
    public partial class SelectNavigationOperator : Form
    {

        private UserInput userInput;
        private string agsNavstepSchema, selection;

        private Operators operators;

        // saves the navigation operators and corresponding tables as strings
        private Dictionary<string, string> AGS_NAVSTEP_SCHEMA_dictionary = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui">Reference to previous window.</param>
        public SelectNavigationOperator(UserInput ui)
        {
            userInput = ui;
            InitializeComponent();

            ComboItem.SetComboboxContent(ComboBox_AgsNavstepSchema, "AGS_NAVSTEP_SCHEMA", "NAVSS_OPNAME");

            FillDictionary_AGS_NAVSTEP_SCHEMA();
        }

        // on selection of comboBoxNav
        private void ComboBox_AgsNavstepSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_Selection.Visible = true;

            agsNavstepSchema = ComboBox_AgsNavstepSchema.Text;
            Console.WriteLine("[SelectedIndexChanged] agsNavstepSchema: " + agsNavstepSchema);

            if (agsNavstepSchema == "drillDownToLevel")
            {
                // TODO
            }
            else if (agsNavstepSchema == "rollUpToLevel")
            {
                // TODO
            }
            else if (agsNavstepSchema == "moveToNode")
            {
                // TODO
            }
            else if (agsNavstepSchema == "refocusSliceCond")
            {
                // TODO
            }
            else if (agsNavstepSchema == "refocusBMsrCond")
            {
                // TODO
            }
            else if (agsNavstepSchema == "refocusMeasure")
            {
                // TODO
            }
            else if (agsNavstepSchema == "refocusAMsrFilter")
            {
                // TODO
            }
            else if (agsNavstepSchema == "drillAcrossToCube")
            {
                ComboItem.SetComboboxContent(ComboBox_Selection, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
            }
            else if (agsNavstepSchema == "refocusScoreFilter")
            {
                // TODO
            }
            else
            {
                // TODO some error message
            }

            /*
            // TODO delete this is not needed anymore
            if (agsNavstepSchema == "moveToPrevNode")
            {
                DataRow[] dr_ags_nass_dim_qual = DBContext.Service().GetData("SELECT DIM_SID FROM AGS_NASS_DIM_QUAL").Copy().Select();
                DataRow[] dr_dw_dimension = DBContext.Service().GetData("SELECT DIM_SID, DIM_NAME FROM DW_DIMENSION").Copy().Select();

                Console.WriteLine("[dr_ags_nass_dim_qual] " + dr_ags_nass_dim_qual[0].ItemArray[0].ToString());
                Console.WriteLine("[dr_dw_dimension] " + dr_dw_dimension[0].ItemArray[0].ToString());

                DataRow[] dataRow = new DataRow[dr_dw_dimension.Length-1];
                int index = 0;

                for (int i = 0; i < dr_dw_dimension.Length; i++)
                {
                    if (dr_dw_dimension[i].ItemArray[0].Equals(dr_ags_nass_dim_qual[i].ItemArray[0]))
                    {
                        ComboBox_Selection.Items.Add(dr_dw_dimension[i]);
                        Console.WriteLine("[dr_dw_dimension[i]]" + dr_dw_dimension[i]);
                            
                        dataRow[index] = dr_dw_dimension[i];
                        index++;
                        
                    }
                }
                Console.WriteLine("[dataRow[0].ItemArray[0]] " + dataRow[0].ItemArray[0].ToString());
                Console.WriteLine("[dataRow[0].ItemArray[1]] " + dataRow[0].ItemArray[1].ToString());

                int j = 0;
                while (dataRow[j] != null)
                {
                    Console.WriteLine("[dataRow[j].ItemArray[1]] " + dataRow[j].ItemArray[1].ToString());
                    ComboItem item = new ComboItem(
                        Int32.Parse(dataRow[j].ItemArray[0].ToString()),
                        dataRow[j].ItemArray[1].ToString());
                    Console.WriteLine("[comboItem] " + item.toString());
                    ComboBoxCube.Items.Add(item.Text);
                    j++;
                }
            }*/
        }


        private void ComboBox_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            selection = ComboBox_Selection.Text;
            Console.WriteLine("[SelectedIndexChanged] ComboBox_Selection: " + selection);
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            // get table from dictionary
            string table = null;
            if (AGS_NAVSTEP_SCHEMA_dictionary.ContainsKey(agsNavstepSchema))
            {
                table = AGS_NAVSTEP_SCHEMA_dictionary[agsNavstepSchema];
            }
            Console.WriteLine("[SUBMIT] Table: " + table);

            // open connection and begin transaction
            Console.WriteLine("[SUBMIT] open connection and begin transaction");
            NpgsqlConnection connection = DBContext.Service().getConnection();
            connection.Open();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            // create statement with currentSelection
            Console.WriteLine("[SUBMIT] agsNavstepSchema: " + agsNavstepSchema);
            string statement = "SELECT NAVSS_SID FROM AGS_NAVSTEP_SCHEMA WHERE NAVSS_OPNAME = \'" + agsNavstepSchema + "\'";

            // get private key
            Console.WriteLine("[SUBMIT] get coulumnPK");
            int columnPK = Int32.Parse(DBContext.Service().getStringFromStmt(statement, 0, 0));
            string columnPKName = "NAVSS_SID";

            // create a list of items
            LinkedList<Insert_item> list = new LinkedList<Insert_item>();

            // START insertion
            if (table == "AGS_NAVSS_DRILL_DOWN_TO_LEVEL")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_ROLL_UP_TO_LEVEL")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_MOVE_TO_NODE")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_REFOCUS_SLICE_COND")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_REFOCUS_BMSR_COND_PARS")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_REFOCUS_MEASURE_PARS")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_REFOCUS_AMSR_FILTER_PARS")
            {
                // TODO
            }
            else if (table == "AGS_NAVSS_DRILL_ACROSS_TO_CUBE") // TODO changed to NAVSS
            {
                string stmt = "SELECT CUBE_SID FROM DW_CUBE WHERE CUBE_NAME = \'" + selection + "\'";
                Int32 cube_sid = Int32.Parse(DBContext.Service().getStringFromStmt(stmt, 0, 0));
                list.AddLast(new Insert_item("CUBE_SID", cube_sid));

                userInput.SelectComboBoxCube(selection);
            }
            else if (table == "AGS_NAVSS_REFOCUS_SCORE_FILTER_PARS")
            {
                // TODO
            }
            else
            {
                // TODO some error message window
                Console.WriteLine("[SUBMIT] error");
                this.Close();
            }

            // check if private key exists already in table
            bool keyExistsAlready = false;
            DataRow[] dr = DBContext.Service().GetData("SELECT " + columnPKName + " FROM " + table).Copy().Select();

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
                Console.WriteLine("[SUBMIT] calling function insinto(" + connection + ", " + transaction + ", " + table + ", " + columnPK + ", " + columnPKName + ", " + list + ")");
                DBContext.Service().insinto(connection, transaction, table, columnPK, columnPKName, list);
            }

            // close window
            Console.WriteLine("[SUBMIT] finished");
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "Select Navigation Operator", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        // operatoren
        private void FillDictionary_AGS_NAVSTEP_SCHEMA()
        {
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillDownToLevel", "AGS_NAVSS_DRILL_DOWN_TO_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("rollUpToLevel", "AGS_NAVSS_ROLL_UP_TO_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToNode", "AGS_NAVSS_MOVE_TO_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusSliceCond", "AGS_NAVSS_REFOCUS_SLICE_COND");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusBMsrCond", "AGS_NAVSS_REFOCUS_BMSR_COND_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusMeasure", "AGS_NAVSS_REFOCUS_MEASURE_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusAMsrFilter", "AGS_NAVSS_REFOCUS_AMSR_FILTER_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillAcrossToCube", "AGS_NAVSS_DRILL_ACROSS_TO_CUBE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusScoreFilter", "AGS_NAVSS_REFOCUS_SCORE_FILTER_PARS");
        }
    }
}
