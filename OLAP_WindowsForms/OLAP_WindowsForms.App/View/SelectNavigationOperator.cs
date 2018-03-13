using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App.View
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SelectNavigationOperator : Form
    {

        private UserInput userInput;
        private ComboBox changed_ComboBox;
        private TextBox changed_TextBox;
        private ListBox changed_ListBox;

        private string agsNavstepSchema, selection, schema;
        private int dim_sid;

        // saves the navigation operators and corresponding tables as strings
        private Dictionary<string, string> AGS_NAVSTEP_SCHEMA_dictionary = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui">Reference to previous window.</param>
        public SelectNavigationOperator(UserInput ui)
        {
            // initialize
            userInput = ui;
            InitializeComponent();
            FillDictionary();
            
            // fill ComboBox_AgsNavstepSchema
            foreach (KeyValuePair<string, string> entry in AGS_NAVSTEP_SCHEMA_dictionary) ComboBox_AgsNavstepSchema.Items.Add(entry.Key);

            // fill ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA
            DataTable dt = DBContext.Service().GetData(
                "SELECT ASS_SID, ASS_NAME " +
                "FROM AGS_ANALYSIS_SITUATION_SCHEMA " +
                "WHERE AGS_SID = " + userInput.loaded_ags_sid).Copy();
            ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.DataSource = dt;
            ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.ValueMember = "ASS_SID";
            ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.DisplayMember = "ASS_NAME";
        }

        // on selection of ComboBox_AgsNavstepSchema
        private void ComboBox_AgsNavstepSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            // enable or disable combo box
            ComboBox_Selection.Visible = true;
            ComboBox_Selection2.Visible = false;
            ComboBox_Selection3.Visible = false;
            ComboBox_Selection4.Visible = false;
            ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = false;
            label_DN.Visible = false;
            textBox_DN.Visible = false;

            // get content of ComboBox_AgsNavstepSchema
            agsNavstepSchema = ComboBox_AgsNavstepSchema.Text;
            Console.WriteLine("[ComboBox_AgsNavstepSchema_SelectedIndexChanged] agsNavstepSchema: " + agsNavstepSchema);

            // fill ComboBox_Selection
            if (agsNavstepSchema == "drillDownToLevel" ||
                agsNavstepSchema == "rollUpToLevel" ||
                agsNavstepSchema == "moveToNode" ||
                agsNavstepSchema == "refocusSliceCond")
            {
                // Dim = D
                DataTable dt = DBContext.Service().GetData(
                   "SELECT d.DIM_SID, d.DIM_NAME " +
                   "FROM DW_CUBE_DIMENSION c,  DW_DIMENSION d " +
                   "WHERE c.DIM_SID = d.DIM_SID AND CUBE_SID = " + userInput.ComboBoxCube.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection.DataSource = dt;
                ComboBox_Selection.ValueMember = "DIM_SID";
                ComboBox_Selection.DisplayMember = "DIM_NAME";
            }
            else if (agsNavstepSchema == "refocusBMsrCond")
            {
                // BC: B1, ..., Bb
                DataTable dt = DBContext.Service().GetData(
                   "SELECT BMSR_PRED_SID, BMSR_PRED_NAME " +
                   "FROM DW_BMSR_PREDICATE " +
                   "WHERE BMSR_PRED_SID >= 0 AND CUBE_SID = " + userInput.ComboBoxCube.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection.DataSource = dt;
                ComboBox_Selection.ValueMember = "BMSR_PRED_SID";
                ComboBox_Selection.DisplayMember = "BMSR_PRED_NAME";
            }
            else if (agsNavstepSchema == "refocusMeasure")
            {
                // Msr: M1, ..., Mm
                DataTable dt = DBContext.Service().GetData(
                    "SELECT c.DAMSR_SID, d.DAMSR_NAME " +
                    "FROM DW_CUBE_DERIVED_AGGREGATE_MEASURE c INNER JOIN DW_DERIVED_AGGREGATE_MEASURE d " +
                    "ON c.DAMSR_SID = d.DAMSR_SID " +
                    "WHERE c.CUBE_SID = " + userInput.ComboBoxCube.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection.DataSource = dt;
                ComboBox_Selection.ValueMember = "DAMSR_SID";
                ComboBox_Selection.DisplayMember = "DAMSR_NAME";
            }
            else if (agsNavstepSchema == "refocusAMsrFilter")
            {
                DataTable dt = DBContext.Service().GetData(
                   "SELECT AMSR_PRED_SID, AMSR_PRED_NAME " +
                   "FROM DW_AMSR_PREDICATE " +
                   "WHERE AMSR_PRED_SID > 0 AND CUBE_SID = " + userInput.ComboBoxCube.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection.DataSource = dt;
                ComboBox_Selection.ValueMember = "AMSR_PRED_SID";
                ComboBox_Selection.DisplayMember = "AMSR_PRED_NAME";
            }
            else if (agsNavstepSchema == "drillAcrossToCube")
            {
                ComboItem.SetComboboxContent(ComboBox_Selection, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
            }
            else
            {
                Console.WriteLine("[ComboBox_AgsNavstepSchema_SelectedIndexChanged] THIS SHOULD NOT HAPPEN, THERE IS AN INTERNAL ERROR");
            }
        }

        // 1) on selection of ComboBox_Selection
        private void ComboBox_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get selection
            selection = ComboBox_Selection.Text;

            // not initialized error
            if (selection.Equals("System.Data.DataRowView")) return;
            if (ComboBox_Selection.SelectedValue.ToString().Equals("System.Data.DataRowView")) return;

            Console.WriteLine("[ComboBox_Selection_SelectedIndexChanged] ComboBox_Selection: " + selection);

            // fill ComboBox_Selection2
            if (agsNavstepSchema == "drillDownToLevel")
            {
                // enable ComboBox_Selection2
                ComboBox_Selection2.Visible = true;

                // get current dim_sid
                dim_sid = Int32.Parse(ComboBox_Selection.SelectedValue.ToString());
                Console.WriteLine("[ComboBox_Selection_SelectedIndexChanged] ComboBox_Selection dim_sid: " + dim_sid);

                // find combobox to copy
                if (selection == "Time") changed_ComboBox = userInput.CDW_TIME_GL;
                else if (selection == "Insurant") changed_ComboBox = userInput.CDW_INSURANT_GL;
                else if (selection == "MedService") changed_ComboBox = userInput.CDW_MEDSERVICE_GL;
                else if (selection == "Drug") changed_ComboBox = userInput.CDW_DRUG_GL;
                else if (selection == "Doctor") changed_ComboBox = userInput.CDW_DOCTOR_GL;
                else if (selection == "Hospital") changed_ComboBox = userInput.CDW_HOSPITAL_GL;
                else return;

                DataTable dt = DBContext.Service().GetData(
                    "SELECT * " +
                    "FROM DW_LEVEL " +
                    "WHERE LVL_SID < " + changed_ComboBox.SelectedValue + " AND DIM_SID = " + dim_sid + " AND LVL_SID > 0 " +
                    "ORDER BY LVL_SID DESC"
                ).Copy();

                ComboBox_Selection2.DataSource = dt;
                ComboBox_Selection2.DisplayMember = "LVL_NAME";
                ComboBox_Selection2.ValueMember = "LVL_SID";
            }
            else if (agsNavstepSchema == "rollUpToLevel")
            {
                // enable ComboBox_Selection2
                ComboBox_Selection2.Visible = true;

                // get current dim_sid
                // get current dim_sid
                dim_sid = Int32.Parse(ComboBox_Selection.SelectedValue.ToString());
                Console.WriteLine("[ComboBox_Selection_SelectedIndexChanged] ComboBox_Selection dim_sid: " + dim_sid);

                // find combobox to copy
                if (selection == "Time") changed_ComboBox = userInput.CDW_TIME_GL;
                else if (selection == "Insurant") changed_ComboBox = userInput.CDW_INSURANT_GL;
                else if (selection == "MedService") changed_ComboBox = userInput.CDW_MEDSERVICE_GL;
                else if (selection == "Drug") changed_ComboBox = userInput.CDW_DRUG_GL;
                else if (selection == "Doctor") changed_ComboBox = userInput.CDW_DOCTOR_GL;
                else if (selection == "Hospital") changed_ComboBox = userInput.CDW_HOSPITAL_GL;
                else return;

                DataTable dt = DBContext.Service().GetData(
                    "SELECT * " +
                    "FROM DW_LEVEL " +
                    "WHERE LVL_SID > " + changed_ComboBox.SelectedValue + " AND DIM_SID = " + dim_sid + " AND LVL_SID > 0 " +
                    "ORDER BY LVL_SID DESC"
                ).Copy();

                ComboBox_Selection2.DataSource = dt;
                ComboBox_Selection2.DisplayMember = "LVL_NAME";
                ComboBox_Selection2.ValueMember = "LVL_SID";
            }
            else if (agsNavstepSchema == "moveToNode")
            {
                // enable ComboBox_Selection2
                ComboBox_Selection2.Visible = true;

                // get current dim_sid
                dim_sid = Int32.Parse(ComboBox_Selection.SelectedValue.ToString());
                Console.WriteLine("[ComboBox_Selection_SelectedIndexChanged] ComboBox_Selection dim_sid: " + dim_sid);

                // find combobox to copy
                if (selection == "Time") { changed_ComboBox = userInput.CDW_TIME; changed_TextBox = userInput.TDW_TIME; }
                else if (selection == "Insurant") { changed_ComboBox = userInput.CDW_INSURANT; changed_TextBox = userInput.TDW_INSURANT; }
                else if (selection == "MedService") { changed_ComboBox = userInput.CDW_MEDSERVICE; changed_TextBox = userInput.TDW_MEDSERVICE; }
                else if (selection == "Drug") { changed_ComboBox = userInput.CDW_DRUG; changed_TextBox = userInput.TDW_DRUG; }
                else if (selection == "Doctor") { changed_ComboBox = userInput.CDW_DOCTOR; changed_TextBox = userInput.TDW_DOCTOR; }
                else if (selection == "Hospital") { changed_ComboBox = userInput.CDW_HOSPITAL; changed_TextBox = userInput.TDW_HOSPITAL; }
                else return;

                DataTable dt = DBContext.Service().GetData(
                    "SELECT LVL_NAME, LVL_SID " +
                    "FROM DW_LEVEL " +
                    "WHERE DIM_SID = " + dim_sid + " AND LVL_SID > 0" +
                    "ORDER BY LVL_SID DESC"
                ).Copy();

                ComboBox_Selection2.DataSource = dt;
                ComboBox_Selection2.DisplayMember = "LVL_NAME";
                ComboBox_Selection2.ValueMember = "LVL_SID";
            }
            else if (agsNavstepSchema == "refocusSliceCond")
            {
                // enable ComboBox_Selection2
                ComboBox_Selection2.Visible = true;

                // get current dim_sid
                dim_sid = Int32.Parse(ComboBox_Selection.SelectedValue.ToString());
                Console.WriteLine("[ComboBox_Selection_SelectedIndexChanged] ComboBox_Selection dim_sid: " + dim_sid);

                // find combobox to copy
                int lvl_sid = 0;
                if (selection == "Time") { lvl_sid = Int32.Parse(userInput.CDW_TIME.SelectedValue.ToString()); changed_ListBox = userInput.LDW_TIME; }
                else if (selection == "Insurant") { lvl_sid = Int32.Parse(userInput.CDW_INSURANT.SelectedValue.ToString()); changed_ListBox = userInput.LDW_INSURANT; }
                else if (selection == "MedService") { lvl_sid = Int32.Parse(userInput.CDW_MEDSERVICE.SelectedValue.ToString()); changed_ListBox = userInput.LDW_MEDSERVICE; }
                else if (selection == "Drug") { lvl_sid = Int32.Parse(userInput.CDW_DRUG.SelectedValue.ToString()); changed_ListBox = userInput.LDW_DRUG; }
                else if (selection == "Doctor") { lvl_sid = Int32.Parse(userInput.CDW_DOCTOR.SelectedValue.ToString()); changed_ListBox = userInput.LDW_DOCTOR; }
                else if (selection == "Hospital") { lvl_sid = Int32.Parse(userInput.CDW_HOSPITAL.SelectedValue.ToString()); changed_ListBox = userInput.LDW_HOSPITAL; }
                else return;

                DataTable dt = DBContext.Service().GetData(
                    "SELECT DIM_PRED_SID, DIM_PRED_NAME " +
                    "FROM DW_DIM_PREDICATE " +
                    "WHERE LVL_SID > 0 AND LVL_SID = " + lvl_sid +
                    "ORDER BY LVL_SID DESC"
                ).Copy();

                ComboBox_Selection2.DataSource = dt;
                ComboBox_Selection2.DisplayMember = "DIM_PRED_NAME";
                ComboBox_Selection2.ValueMember = "DIM_PRED_SID";
            }
            else if (agsNavstepSchema == "refocusBMsrCond")
            {
                changed_ListBox = userInput.LDW_BMSR;                

                ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = true;
            }
            else if (agsNavstepSchema == "refocusMeasure")
            {
                changed_ListBox = userInput.LDW_MEASURES;

                ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = true;
            }
            else if (agsNavstepSchema == "refocusAMsrFilter")
            {
                changed_ListBox = userInput.LDW_FILTER;

                ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = true;
            }
            else if (agsNavstepSchema == "drillAcrossToCube")
            {
                Console.WriteLine("[ComboBox_Selection_SelectedIndexChanged] " + ComboBox_Selection.SelectedValue.ToString());
                ComboBox_Selection2.Visible = true;

                // BC: B1, ..., Bb
                DataTable dt = DBContext.Service().GetData(
                   "SELECT BMSR_PRED_SID, BMSR_PRED_NAME " +
                   "FROM DW_BMSR_PREDICATE " +
                   "WHERE BMSR_PRED_SID >= 0 AND CUBE_SID = " + ComboBox_Selection.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection2.DataSource = dt;
                ComboBox_Selection2.ValueMember = "BMSR_PRED_SID";
                ComboBox_Selection2.DisplayMember = "BMSR_PRED_NAME";
            }
            else
            {
                // TODO some error message
                Console.WriteLine("Some error message");
            }
        }

        // 2) on selection of ComboBox_Selection2
        private void ComboBox_Selection2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //error
            if (ComboBox_Selection2.SelectedValue.ToString().Equals("System.Data.DataRowView")) return;

            if (agsNavstepSchema == "moveToNode")
            {
                label_DN.Visible = true;
                textBox_DN.Visible = true;
            }

            if (agsNavstepSchema == "drillAcrossToCube")
            {
                Console.WriteLine("[ComboBox_Selection2_SelectedIndexChanged] " + ComboBox_Selection2.SelectedValue.ToString());
                ComboBox_Selection3.Visible = true;

                // Msr: M1, ..., Mm
                DataTable dt = DBContext.Service().GetData(
                    "SELECT c.DAMSR_SID, d.DAMSR_NAME " +
                    "FROM DW_CUBE_DERIVED_AGGREGATE_MEASURE c INNER JOIN DW_DERIVED_AGGREGATE_MEASURE d " +
                    "ON c.DAMSR_SID = d.DAMSR_SID " +
                    "WHERE c.CUBE_SID = " + ComboBox_Selection2.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection3.DataSource = dt;
                ComboBox_Selection3.ValueMember = "DAMSR_SID";
                ComboBox_Selection3.DisplayMember = "DAMSR_NAME";
            }
            else
            {
                // enable ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA
                ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = true;
            }
        }

        // 3) on selection of ComboBox_Selection3
        private void ComboBox_Selection3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //error
            if (ComboBox_Selection3.SelectedValue.ToString().Equals("System.Data.DataRowView")) return;

            if (agsNavstepSchema == "drillAcrossToCube")
            {
                Console.WriteLine("[ComboBox_Selection3_SelectedIndexChanged] " + ComboBox_Selection3.SelectedValue.ToString());
                ComboBox_Selection4.Visible = true;

                DataTable dt = DBContext.Service().GetData(
                   "SELECT AMSR_PRED_SID, AMSR_PRED_NAME " +
                   "FROM DW_AMSR_PREDICATE " +
                   "WHERE AMSR_PRED_SID > 0 AND CUBE_SID = " + ComboBox_Selection3.SelectedValue.ToString()
                ).Copy();

                ComboBox_Selection4.DataSource = dt;
                ComboBox_Selection4.ValueMember = "AMSR_PRED_SID";
                ComboBox_Selection4.DisplayMember = "AMSR_PRED_NAME";
            }
        }

        // 4) on selection of ComboBox_Selection4
        private void ComboBox_Selection4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Visible = true;
        }

        // on selection of ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA
        private void ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            schema = ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Text;
            Console.WriteLine("[ComboBox_Selection4_SelectedIndexChanged] ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA: " + schema);
        }

        // fügt in alle notwendigen tabellen ein
        private void Submit_Click(object sender, EventArgs e)
        {
            // get table from dictionary
            string table = null;
            if (AGS_NAVSTEP_SCHEMA_dictionary.ContainsKey(agsNavstepSchema)) table = AGS_NAVSTEP_SCHEMA_dictionary[agsNavstepSchema];
            Console.WriteLine("[buttonSubmit_Click] Table: " + table);

            // open connection and begin transaction
            Console.WriteLine("[buttonSubmit_Click] open connection and begin transaction");
            NpgsqlConnection connection = DBContext.Service().GetConnection();
            connection.Open();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            // create a list of items
            LinkedList<Insert_item> list = new LinkedList<Insert_item>();

            // get ass_sid_target
            if (ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.SelectedValue.ToString().Equals("System.Data.DataRowView"))
            {
                Console.WriteLine("[buttonSubmit_Click] Error: AGS_ANALYSIS_SITUATION_SCHEMA not selected");
                return;
            }
            int ass_sid_target = Int32.Parse(ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.SelectedValue.ToString());

            // existing or new schema
            if (checkBox1.Checked)
            {
                schema = ComboBox_AGS_ANALYSIS_SITUATION_SCHEMA.Text;
            }

            // insert into AGS_NAVSTEP_SCHEMA
            list.AddLast(new Insert_item("AGS_SID", userInput.loaded_ags_sid));
            list.AddLast(new Insert_item("ASS_SID_SOURCE", userInput.loaded_ass_sid));
            list.AddLast(new Insert_item("ASS_SID_TARGET", ass_sid_target));
            list.AddLast(new Insert_item("NAVSS_OPNAME", agsNavstepSchema));
            list.AddLast(new Insert_item("NAVSS_GRD_EXPR", null));
            list.AddLast(new Insert_item("NAVSS_USED_IN_CMPNAV", 0));
            list.AddLast(new Insert_item("NAVSS_POS_X", 0));
            list.AddLast(new Insert_item("NAVSS_POS_Y", 0));
            list.AddLast(new Insert_item("NAVSS_POS_GRD_X", 0));
            list.AddLast(new Insert_item("NAVSS_POS_GRD_Y", 0));
            
            Console.WriteLine("[buttonSubmit_Click] calling function insinto(" + connection + ", " + transaction + ", AGS_NAVSTEP_SCHEMA, NAVSS_SID, " + list + ")");
            Console.WriteLine("[buttonSubmit_Click] values: {0}, {1}, {2}, {3}, null, 0, 0, 0, 0, 0", userInput.loaded_ags_sid, userInput.loaded_ass_sid, ass_sid_target, agsNavstepSchema);
            DBContext.Service().InsertInto(connection, transaction, "AGS_NAVSTEP_SCHEMA", "NAVSS_SID", list);

            // reset list
            list = new LinkedList<Insert_item>();

            // START insertion
            if (table == "AGS_NAVSS_DRILL_DOWN_TO_LEVEL" ||
                table == "AGS_NAVSS_ROLL_UP_TO_LEVEL")
            {
                // insert NAVSS_SID, DIM_SID, LVL_SID_GRANLVL
                list.AddLast(new Insert_item("DIM_SID", dim_sid));
                list.AddLast(new Insert_item("LVL_SID_GRANLVL", Int32.Parse(ComboBox_Selection2.SelectedValue.ToString())));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change UserInput
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection2: " + ComboBox_Selection2.Text);
                changed_ComboBox.SelectedIndex = changed_ComboBox.FindString(ComboBox_Selection2.Text);
            }
            else if (table == "AGS_NAVSS_MOVE_TO_NODE")
            {
                Console.WriteLine("[buttonSubmit_Click] textBox_DN.Text: " + textBox_DN.Text);

                //insert NAVSS_SID, DIM_SID, LVL_SID_DICELVL, NAVSS_DICE_NODE
                list.AddLast(new Insert_item("DIM_SID", dim_sid));
                list.AddLast(new Insert_item("LVL_SID_DICELVL", Int32.Parse(ComboBox_Selection2.SelectedValue.ToString())));
                list.AddLast(new Insert_item("NAVSS_DICE_NODE", textBox_DN.Text));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change UserInput
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection2: " + ComboBox_Selection2.Text);
                changed_ComboBox.SelectedIndex = changed_ComboBox.FindString(ComboBox_Selection2.Text);
                changed_TextBox.Text = textBox_DN.Text;
            }
            else if (table == "AGS_NAVSS_REFOCUS_SLICE_COND")
            {
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection.SelectedText: " + ComboBox_Selection.Text);

                //insert NAVSS_SID, DIM_SID
                list.AddLast(new Insert_item("DIM_SID", dim_sid));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change UserInput
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection2: " + ComboBox_Selection2.Text);
                Console.WriteLine("[buttonSubmit_Click] changed_ListBox.Items.Count: " + changed_ListBox.Items.Count);
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection.SelectedIndex: " + ComboBox_Selection2.SelectedIndex);
                for (int i = 0; i < changed_ListBox.Items.Count; i++)
                {
                    changed_ListBox.SetSelected(i, false);
                }
                changed_ListBox.SetSelected(ComboBox_Selection2.SelectedIndex, true);
            }
            else if (table == "AGS_NAVSS_REFOCUS_BMSR_COND_PARS")
            {
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection.SelectedText: " + ComboBox_Selection.SelectedValue.ToString());

                //insert NAVSS_SID, BMSR_PRED_SID
                list.AddLast(new Insert_item("BMSR_PRED_SID", Int32.Parse(ComboBox_Selection.SelectedValue.ToString())-1));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change UserInput
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection.SelectedIndex: " + ComboBox_Selection.SelectedIndex);
                Console.WriteLine("[buttonSubmit_Click] changed_ListBox.Items.Count: " + changed_ListBox.Items.Count);
                for (int i = 0; i < changed_ListBox.Items.Count; i++)
                {
                    changed_ListBox.SetSelected(i, false);
                }
                changed_ListBox.SetSelected(ComboBox_Selection.SelectedIndex, true);
            }
            else if (table == "AGS_NAVSS_REFOCUS_MEASURE_PARS")
            {
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection.SelectedText: " + ComboBox_Selection.SelectedValue.ToString());

                //insert NAVSS_SID, DAMSR_SID
                list.AddLast(new Insert_item("DAMSR_SID", Int32.Parse(ComboBox_Selection.SelectedValue.ToString())));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change UserInput
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection: " + ComboBox_Selection.Text);
                Console.WriteLine("[buttonSubmit_Click] changed_ListBox.Items.Count: " + changed_ListBox.Items.Count);
                for (int i = 0; i < changed_ListBox.Items.Count; i++)
                {
                    changed_ListBox.SetSelected(i, false);
                }
                changed_ListBox.SetSelected(ComboBox_Selection.SelectedIndex, true);
            }
            else if (table == "AGS_NAVSS_REFOCUS_AMSR_FILTER_PARS")
            {
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection.SelectedText: " + ComboBox_Selection.SelectedValue.ToString());

                //insert NAVSS_SID, AMSR_PRED_SID
                list.AddLast(new Insert_item("AMSR_PRED_SID", Int32.Parse(ComboBox_Selection.SelectedValue.ToString())));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change UserInput
                Console.WriteLine("[buttonSubmit_Click] ComboBox_Selection: " + ComboBox_Selection.Text);
                Console.WriteLine("[buttonSubmit_Click] changed_ListBox.Items.Count: " + changed_ListBox.Items.Count);
                for (int i = 0; i < changed_ListBox.Items.Count; i++)
                {
                    changed_ListBox.SetSelected(i, false);
                }
                changed_ListBox.SetSelected(ComboBox_Selection.SelectedIndex, true);
            }
            else if (table == "AGS_NAVSS_DRILL_ACROSS_TO_CUBE")
            {
                string stmt = "SELECT CUBE_SID FROM DW_CUBE WHERE CUBE_NAME = \'" + selection + "\'";
                Int32 cube_sid = Int32.Parse(DBContext.Service().GetStringFromStmt(stmt, 0, 0));
                Console.WriteLine("[buttonSubmit_Click] calling function insinto(" + connection + ", " + transaction + ", " + table + ", NAVSS_SID, " + list + ")");

                // insert NAVSS_SID, CUBE_SID
                list.AddLast(new Insert_item("CUBE_SID", cube_sid));
                DBContext.Service().InsertInto(connection, transaction, table, "NAVSS_SID", list);

                // change userInput
                Console.WriteLine("[buttonSubmit_Click] " + selection);
                userInput.ComboBoxCube.SelectedIndex = userInput.ComboBoxCube.FindString(selection);
                userInput.comboBoxCube_SelectedIndexChanged(userInput.ComboBoxCube, new EventArgs());
                userInput.ComboBoxCube.SelectedValue = selection;
            }
            else
            {
                Console.WriteLine("[buttonSubmit_Click] error");
                this.Close();
            }

            // transaction complete
            transaction.Commit();
            DBContext.Service().TransactionComplete();
            
            // disable fields -> user cannot do changes
            userInput.disable_fields();

            // close window
            Console.WriteLine("[buttonSubmit_Click] finished");
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "Select Navigation Operator", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void FillDictionary()
        {
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillDownToLevel", "AGS_NAVSS_DRILL_DOWN_TO_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("rollUpToLevel", "AGS_NAVSS_ROLL_UP_TO_LEVEL");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("moveToNode", "AGS_NAVSS_MOVE_TO_NODE");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusSliceCond", "AGS_NAVSS_REFOCUS_SLICE_COND");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusBMsrCond", "AGS_NAVSS_REFOCUS_BMSR_COND_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusMeasure", "AGS_NAVSS_REFOCUS_MEASURE_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("refocusAMsrFilter", "AGS_NAVSS_REFOCUS_AMSR_FILTER_PARS");
            AGS_NAVSTEP_SCHEMA_dictionary.Add("drillAcrossToCube", "AGS_NAVSS_DRILL_ACROSS_TO_CUBE");
        }
    }
}
