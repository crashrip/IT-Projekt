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
using System.Configuration;
using OLAP_WindowsForms.App.View;
using Npgsql;

namespace OLAP_WindowsForms.App
{
    public partial class UserInput : Form
    {
        private int ags_sid;
        public String name = "userinput";
        public String description = "";
        private Boolean dim_doctor = false;
        private Boolean dim_insurance = false;
        private Boolean dim_drug = false;
        private Boolean dim_medservice = false;
        private Boolean dim_hospital = false;
        private Boolean dim_time = false;

        public UserInput(int ags_sid)
        {
            this.ags_sid = ags_sid;
            InitializeComponent();

            // fill combobox with data preview from cube
            ComboItem.GetComboboxContent(ComboBoxCube, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
            //getListBoxContent(listBox1, "DW_DERIVED_BASE_MEASURE", "DBMSR_EXPR", "DBMSR_NAME"); 
            //getListBoxContent(listBox2, "DW_DERIVED_AGGREGATE_MEASURE", "DAMSR_EXPR", "DAMSR_NAME");

        }

        
        public void SelectComboBoxCube(string selection) // TODO not yet working
        {
            Console.WriteLine(selection);
            System.Threading.Thread.Sleep(5000);
            //ComboBoxCube.SelectedIndex = ComboBoxCube.Items.IndexOf(selection);
            ComboBoxCube.SelectedItem = selection;
        }
        
        // test labels
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = ComboBoxCube.SelectedValue.ToString();
            
            if (!(ComboBoxCube.SelectedValue.ToString().Equals("System.Data.DataRowView")))
            {
                Console.WriteLine(Int32.Parse(ComboBoxCube.SelectedValue.ToString()));
                disable_dimensions();

                bmsr_Instantiate();
                measures_Instantiate();
                filter_Instantiate();

                dimension_enable_disable();
            }
        }

        private void disable_dimensions()
        {
            // reset DL
            CDW_DOCTOR.SelectedIndex = -1;
            CDW_INSURANT.SelectedIndex = -1;
            CDW_DRUG.SelectedIndex = -1;
            CDW_MEDSERVICE.SelectedIndex = -1;
            CDW_HOSPITAL.SelectedIndex = -1;
            CDW_TIME.SelectedIndex = -1;
            // DL
            CDW_DOCTOR.Enabled = false;
            CDW_INSURANT.Enabled = false;
            CDW_DRUG.Enabled = false;
            CDW_MEDSERVICE.Enabled = false;
            CDW_HOSPITAL.Enabled = false;
            CDW_TIME.Enabled = false;
            // DN
            TDW_DOCTOR.Enabled = false;
            TDW_INSURANT.Enabled = false;
            TDW_DRUG.Enabled = false;
            TDW_MEDSERVICE.Enabled = false;
            TDW_HOSPITAL.Enabled = false;
            TDW_TIME.Enabled = false;
            // SC
            LDW_DOCTOR.Enabled = false;
            LDW_INSURANT.Enabled = false;
            LDW_DRUG.Enabled = false;
            LDW_MEDSERVICE.Enabled = false;
            LDW_HOSPITAL.Enabled = false;
            LDW_TIME.Enabled = false;
            // GL
            CDW_DOCTOR_GL.Enabled = false;
            CDW_INSURANT_GL.Enabled = false;
            CDW_DRUG_GL.Enabled = false;
            CDW_MEDSERVICE_GL.Enabled = false;
            CDW_HOSPITAL_GL.Enabled = false;
            CDW_TIME_GL.Enabled = false;
            // variables dimensions
            dim_doctor = false; 
            dim_insurance = false;
            dim_drug = false;
            dim_medservice = false;
            dim_hospital = false;
            dim_time = false;

            // variable checkboxes
            doctor_DL.Enabled = false;
            doctor_DN.Enabled = false;
            doctor_SC.Enabled = false;
            doctor_GL.Enabled = false;
            insurant_DL.Enabled = false;
            insurant_DN.Enabled = false;
            insurant_SC.Enabled = false;
            insurant_GL.Enabled = false;
            drug_DL.Enabled = false;
            drug_DN.Enabled = false;
            drug_SC.Enabled = false;
            drug_GL.Enabled = false;
            meds_DL.Enabled = false;
            meds_DN.Enabled = false;
            meds_SC.Enabled = false;
            meds_GL.Enabled = false;
            hospital_DL.Enabled = false;
            hospital_DN.Enabled = false;
            hospital_SC.Enabled = false;
            hospital_GL.Enabled = false;
            time_DL.Enabled = false;
            time_DN.Enabled = false;
            time_SC.Enabled = false;
            time_GL.Enabled = false;
        }

        private void dimension_enable_disable()
        {
            DataTable dt = DBContext.Service().GetData(
               "SELECT DIM_SID " +
               "FROM DW_CUBE_DIMENSION " +
               "WHERE CUBE_SID = " + ComboBoxCube.SelectedValue.ToString()
                );
            DataTable dt2 = dt.Copy();
            DataRow[] dr = dt2.Select();

            for (int i = 0; i < dr.Length; i++)
            {
                String dim_sid = dr[i].ItemArray[0].ToString();

                if (dim_sid.Equals("1"))
                {
                    CDW_DOCTOR.Enabled = true;
                    TDW_DOCTOR.Enabled = true;
                    LDW_DOCTOR.Enabled = true;
                    CDW_DOCTOR_GL.Enabled = true;
                    fillComboboxDimension(CDW_DOCTOR, 1); //doctor
                    dim_doctor = true;
                    doctor_DL.Enabled = true;
                    doctor_DN.Enabled = true;
                    doctor_SC.Enabled = true;
                    doctor_GL.Enabled = true;
                }
                if (dim_sid.Equals("2"))
                {
                    CDW_INSURANT.Enabled = true;
                    TDW_INSURANT.Enabled = true;
                    LDW_INSURANT.Enabled = true;
                    CDW_INSURANT_GL.Enabled = true;
                    fillComboboxDimension(CDW_INSURANT, 2); //insurant
                    dim_insurance = true;
                    insurant_DL.Enabled = true;
                    insurant_DN.Enabled = true;
                    insurant_SC.Enabled = true;
                    insurant_GL.Enabled = true;
                }
                if (dim_sid.Equals("3"))
                {
                    CDW_DRUG.Enabled = true;
                    TDW_DRUG.Enabled = true;
                    LDW_DRUG.Enabled = true;
                    CDW_DRUG_GL.Enabled = true;
                    fillComboboxDimension(CDW_DRUG, 3); //drug
                    dim_drug = true;
                    drug_DL.Enabled = true;
                    drug_DN.Enabled = true;
                    drug_SC.Enabled = true;
                    drug_GL.Enabled = true;
                }
                if (dim_sid.Equals("4"))
                {
                    CDW_MEDSERVICE.Enabled = true;
                    TDW_MEDSERVICE.Enabled = true;
                    LDW_MEDSERVICE.Enabled = true;
                    CDW_MEDSERVICE_GL.Enabled = true;
                    fillComboboxDimension(CDW_MEDSERVICE, 4); //medservice
                    dim_medservice = true;
                    meds_DL.Enabled = true;
                    meds_DN.Enabled = true;
                    meds_SC.Enabled = true;
                    meds_GL.Enabled = true;

                }
                if (dim_sid.Equals("5"))
                {
                    CDW_HOSPITAL.Enabled = true;
                    TDW_HOSPITAL.Enabled = true;
                    LDW_HOSPITAL.Enabled = true;
                    CDW_HOSPITAL_GL.Enabled = true;
                    fillComboboxDimension(CDW_HOSPITAL, 5); //hospital
                    dim_hospital = true;
                    hospital_DL.Enabled = true;
                    hospital_DN.Enabled = true;
                    hospital_SC.Enabled = true;
                    hospital_GL.Enabled = true;
                }
                if (dim_sid.Equals("6"))
                {
                    CDW_TIME.Enabled = true;
                    TDW_TIME.Enabled = true;
                    LDW_TIME.Enabled = true;
                    CDW_TIME_GL.Enabled = true;
                    fillComboboxDimension(CDW_TIME, 6); //time
                    dim_time = true;
                    time_DL.Enabled = true;
                    time_DN.Enabled = true;
                    time_SC.Enabled = true;
                    time_GL.Enabled = true;
                }
            }
        }

        // Fill Derived Base Measures considering selected Cube
        private void bmsr_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            LDW_BMSR.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt = DBContext.Service().GetData(
               "SELECT DBMSR_NAME, DBMSR_EXPR " +
               "FROM (DW_CUBE NATURAL JOIN DW_CUBE_DERIVED_BASE_MEASURE) NATURAL JOIN DW_DERIVED_BASE_MEASURE " +
               "WHERE DW_CUBE.CUBE_SID = " + ComboBoxCube.SelectedValue.ToString()    
                );
            DataTable dt2 = dt.Copy();
            // set DataSource
            LDW_BMSR.DataSource = dt2;
            LDW_BMSR.DisplayMember = "DBMSR_NAME";
            LDW_BMSR.ValueMember = "DBMSR_EXPR";
            LDW_BMSR.SelectionMode = SelectionMode.MultiExtended;
        }

        //Fill Measures considering selected Cube
        private void measures_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            LDW_MEASURES.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt3 = DBContext.Service().GetData(
               "SELECT DAMSR_NAME, DAMSR_EXPR " +
               "FROM (DW_CUBE NATURAL JOIN DW_CUBE_DERIVED_AGGREGATE_MEASURE) NATURAL JOIN DW_DERIVED_AGGREGATE_MEASURE " +
               "WHERE DW_CUBE.CUBE_SID = " + ComboBoxCube.SelectedValue.ToString()
                );
            DataTable dt4 = dt3.Copy();
            // set DataSource
            LDW_MEASURES.DataSource = dt4;
            LDW_MEASURES.DisplayMember = "DAMSR_NAME";
            LDW_MEASURES.ValueMember = "DAMSR_EXPR";
            LDW_MEASURES.SelectionMode = SelectionMode.MultiExtended;

        }

        // Fill DW_BMSR_PREDICATE considering selected Cube
        private void filter_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            LDW_FILTER.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt = DBContext.Service().GetData(
               "SELECT BMSR_PRED_SID, BMSR_PRED_NAME " +
               "FROM DW_BMSR_PREDICATE " +
               "WHERE CUBE_SID = " + ComboBoxCube.SelectedValue.ToString()
                );
            DataTable dt2 = dt.Copy();
            // set DataSource
            LDW_FILTER.DataSource = dt2;
            LDW_FILTER.DisplayMember = "BMSR_PRED_NAME";
            LDW_FILTER.ValueMember = "BMSR_PRED_SID";
            LDW_FILTER.SelectionMode = SelectionMode.MultiExtended;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label6.Text = "";
            Console.WriteLine(LDW_BMSR.SelectedItems.Count);
            foreach (Object sel in LDW_BMSR.SelectedItems)
            {
                label6.Text += (sel as DataRowView)["DBMSR_EXPR"];
                label6.Text += " ";
            }
        }

        private void UserInput_Load(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            label7.Text = "";
            foreach (Object sel in LDW_MEASURES.SelectedItems)
            {
                label7.Text += (sel as DataRowView)["DAMSR_EXPR"].ToString();
                label7.Text += " ";
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = DBContext.Service().GetData(
             "SELECT MAX(AGS_SID) FROM AGS_ANALYSIS_GRAPH_SCHEMA");

            DataTable dt2 = dt.Copy();
            DataRow[] dr = dt2.Select();

            String id = dr[0].ItemArray[0].ToString();

            DBContext.Service().delete("ags_analysis_graph_schema", "ags_sid", id);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            DataTable dt = DBContext.Service().GetData(
              "SELECT MAX(NASS_DQ_SID) FROM AGS_NASS_DIM_QUAL");

            DataTable dt2 = dt.Copy();
            DataRow[] dr = dt2.Select();

            String id = dr[0].ItemArray[0].ToString();
            int ID = Int32.Parse(id)+1;

            String[] column = { "AGS_NAME", "AGS_DESCRIPTION" };
            String[] input = new string[2];
            input[0] = "test";
            input[1] = "this is a test";


            DBContext.Service().insertInto("AGS_ANALYSIS_GRAPH_SCHEMA","AGS_SID",column,input);
            */
            Console.WriteLine("DL_Doc: " + Int32.Parse(CDW_DOCTOR.SelectedValue.ToString()));
            if (CDW_DOCTOR_GL.SelectedValue != null)
            {
                Console.WriteLine("not null -> ");
                Console.WriteLine("GL_Doc: " + Int32.Parse(CDW_DOCTOR_GL.SelectedValue.ToString()));
            } else
            {
                Console.WriteLine("it null");
            }
            /*
            string table = "DW_LEVEL";
            DBContext._dataView = new DataView();
            DBContext.DataView().Text = table;
            DBContext.DataView().LoadData(DBContext.Service().GetData(
               "SELECT LVL_POSITION " +
               "FROM DW_LEVEL " +
               "WHERE LVL_SID = 4"
));
            /*
            "SELECT " + targetColumn +
              "FROM " + table +
              "WHERE " + idColumn + " = " + id
            */
            //DBContext.DataView().Show();
        }

        private void fillComboboxDimension(ComboBox cBox, int dim_sid)
        {
            DataTable dt = DBContext.Service().GetData(
              "SELECT LVL_SID, LVL_NAME " +
              "FROM DW_LEVEL " +
              "WHERE DIM_SID = " + dim_sid +
              "ORDER BY LVL_SID DESC"
               );
            DataTable dt2 = dt.Copy();

            cBox.DataSource = dt2;
            cBox.DisplayMember = "LVL_NAME";
            cBox.ValueMember = "LVL_SID";
        }

        private void fillComboboxDimensionGroupedBy(ComboBox cBox, int dim_sid, string lvl_pos)
        {
            DataTable dt = DBContext.Service().GetData(
              "SELECT LVL_SID, LVL_NAME " +
              "FROM DW_LEVEL " +
              "WHERE DIM_SID = " + dim_sid +
              "AND LVL_POSITION < " + lvl_pos +
              "ORDER BY LVL_SID DESC"
               );
            DataTable dt2 = dt.Copy();

            cBox.DataSource = dt2;
            cBox.DisplayMember = "LVL_NAME";
            cBox.ValueMember = "LVL_SID";
        }

        private void fillListBoxDimension(ListBox lBox, string lvl_sid)
        {
            lBox.SelectionMode = SelectionMode.None;
            DataTable dt = DBContext.Service().GetData(
              "SELECT DIM_PRED_NAME, DIM_PRED_SID " +
              "FROM DW_DIM_PREDICATE " +
              "WHERE LVL_SID = " + lvl_sid
               );
            DataTable dt2 = dt.Copy();

            lBox.DataSource = dt2;
            lBox.DisplayMember = "DIM_PRED_NAME";
            lBox.ValueMember = "DIM_PRED_SID";
            lBox.SelectionMode = SelectionMode.MultiExtended;
        }

        // FILL DW GL AND SC
        private void CDW_TIME_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_TIME.SelectedIndex != -1)
            {
                CDW_TIME_GL.Enabled = true;
                // fill GL
                if (!(CDW_TIME.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    //fillComboboxDimensionGroupedBy(CDW_TIME_GL, 6, CDW_TIME.SelectedValue.ToString());
                    fillGL(CDW_TIME, 6, CDW_TIME_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_TIME.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_TIME, CDW_TIME.SelectedValue.ToString());
                }
            } else
            {
                CDW_TIME_GL.SelectedIndex = -1;
                CDW_TIME_GL.Enabled = false;
            }
        }
        private void CDW_INSURANT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_INSURANT.SelectedIndex != -1)
            {
                CDW_INSURANT_GL.Enabled = true;
                // fill GL
                if (!(CDW_INSURANT.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillGL(CDW_INSURANT, 2, CDW_INSURANT_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_INSURANT.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_INSURANT, CDW_INSURANT.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_INSURANT_GL.SelectedIndex = -1;
                CDW_INSURANT_GL.Enabled = false;
            }
        }

        private void CDW_MEDSERVICE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_MEDSERVICE.SelectedIndex != -1)
            {
                CDW_MEDSERVICE_GL.Enabled = true;
                // fill GL
                if (!(CDW_MEDSERVICE.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillGL(CDW_MEDSERVICE, 4, CDW_MEDSERVICE_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_MEDSERVICE.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_MEDSERVICE, CDW_MEDSERVICE.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_MEDSERVICE_GL.SelectedIndex = -1;
                CDW_MEDSERVICE_GL.Enabled = false;
            }
        }

        private void CDW_DRUG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_DRUG.SelectedIndex != -1)
            {
                CDW_DRUG_GL.Enabled = true;
                // fill GL
                if (!(CDW_DRUG.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillGL(CDW_DRUG, 3, CDW_DRUG_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_DRUG.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_DRUG, CDW_DRUG.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_DRUG_GL.SelectedIndex = -1;
                CDW_DRUG_GL.Enabled = false;
            }
        }
        private void CDW_DOCTOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_DOCTOR.SelectedIndex != -1)
            {
                CDW_DOCTOR_GL.Enabled = true;
                // fill GL
                if (!(CDW_DOCTOR.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillGL(CDW_DOCTOR, 1, CDW_DOCTOR_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_DOCTOR.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_DOCTOR, CDW_DOCTOR.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_DOCTOR_GL.SelectedIndex = -1;
                CDW_DOCTOR_GL.Enabled = false;
            }
        }

        private void CDW_HOSPITAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_HOSPITAL.SelectedIndex != -1)
            {
                CDW_HOSPITAL_GL.Enabled = true;
                // fill GL
                if (!(CDW_HOSPITAL.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillGL(CDW_HOSPITAL, 5, CDW_HOSPITAL_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_HOSPITAL.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_HOSPITAL, CDW_HOSPITAL.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_HOSPITAL_GL.SelectedIndex = -1;
                CDW_HOSPITAL_GL.Enabled = false;
            }
        }
        // END FILL GL AND SC

        // check that input is numbers only
        private void TDW_TIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void fillGL(ComboBox readFromCB, int dim_sid, ComboBox changeThisCB)
        {
            DataTable dt = DBContext.Service().GetData(
                "SELECT *" +
               "FROM DW_LEVEL " +
               "WHERE LVL_SID <=" + readFromCB.SelectedValue + " AND DIM_SID =" + dim_sid +
               "ORDER BY LVL_SID DESC"
               );
            DataTable dt2 = dt.Copy();
            changeThisCB.DataSource = dt2;
            changeThisCB.DisplayMember = "LVL_NAME";
            changeThisCB.ValueMember = "LVL_SID";

        }

        private void deselect(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar);
            if (e.KeyChar == ' ')
            {
                if (sender.GetType().ToString().Equals("System.Windows.Forms.ListBox"))
                {
                    (sender as ListBox).ClearSelected();
                }
                if (sender.GetType().ToString().Equals("System.Windows.Forms.ComboBox"))
                {
                    (sender as ComboBox).SelectedIndex = 0;
                    (sender as ComboBox).SelectedIndex = -1;
                }
            }
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "New Schema",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void button_select_navigation_operator_Click(object sender, EventArgs e)
        {
            SelectNavigationOperator sno = new SelectNavigationOperator(this) { TopMost = true };
            sno.ShowDialog(this);
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            SaveSchema save = new SaveSchema(this) { TopMost = true };
            save.ShowDialog(this);
        }

        public void insert()
        {
            //prepare Connection
            NpgsqlConnection connection = DBContext.Service().getConnection();
            connection.Open();
            NpgsqlCommand command = connection.CreateCommand();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            command.Connection = connection;
            command.Transaction = transaction;
            try
            {
                LinkedList<Insert_item> list = new LinkedList<Insert_item>();

                // AGS_ANALYSIS_SITUATION SCHEMA
                list.AddFirst(new Insert_item("ASS_NAME", this.name));
                list.AddLast(new Insert_item("ASS_DESCRIPTION", this.description));
                list.AddLast(new Insert_item("AGS_SID", ags_sid)); //  Variable aus ags_analysis_graph_schema
                list.AddLast(new Insert_item("ASS_POS_X", 0));
                list.AddLast(new Insert_item("ASS_POS_Y", 0));
                DBContext.Service().insinto(command, "AGS_ANALYSIS_SITUATION_SCHEMA", "ASS_SID", list);

                // save ASS_SID 
                DataTable dt = DBContext.Service().GetData(
                  "SELECT MAX(ASS_SID) FROM AGS_ANALYSIS_SITUATION_SCHEMA");

                DataTable dt2 = dt.Copy();
                DataRow[] dr = dt2.Select();
                String index = dr[0].ItemArray[0].ToString();
                int id = Int32.Parse(index);
                Console.WriteLine(id);

                list.Clear();
                // Cube -> AGS_NON_CMP_ASS 
                if (ComboBoxCube.SelectedIndex != -1 || (!(ComboBoxCube.SelectedValue.ToString().Equals("System.Data.DataRowView"))))
                {
                    Console.WriteLine("cube_sid: " + Int32.Parse(ComboBoxCube.SelectedValue.ToString()));
                    list.AddFirst(new Insert_item("ASS_SID_NASS", id));
                    list.AddLast(new Insert_item("ASS_USED_IN_CASS", 0)); // 1 used in comparative analysis , 0 used in non comparative analysis
                    list.AddLast(new Insert_item("CUBE_SID", Int32.Parse(ComboBoxCube.SelectedValue.ToString())));
                     DBContext.Service().insertWithoutPK(command, "AGS_NON_CMP_ASS", list);
                }

                // DIM Qualification -> AGS_NASS_DIM_QUAL & AGS_NASS_DIM_QUAL_SLICE_COND
                if (dim_doctor) { DBContext.Service().insertDimQual(command, CDW_DOCTOR, CDW_DOCTOR_GL, TDW_DOCTOR, LDW_DOCTOR, id, 1, "Doctor", doctor_DL, doctor_DN, doctor_SC, doctor_GL); }
                if (dim_insurance) { DBContext.Service().insertDimQual(command, CDW_INSURANT, CDW_INSURANT_GL, TDW_INSURANT, LDW_INSURANT, id, 2, "Insurant", insurant_DL, insurant_DN, insurant_SC, insurant_GL); }
                if (dim_drug) { DBContext.Service().insertDimQual(command, CDW_DRUG, CDW_DRUG_GL, TDW_DRUG, LDW_DRUG, id, 3, "Drug", drug_DL, drug_DN, drug_SC, drug_GL); }
                if (dim_medservice) { DBContext.Service().insertDimQual(command, CDW_MEDSERVICE, CDW_MEDSERVICE_GL, TDW_MEDSERVICE, LDW_MEDSERVICE, id, 4, "MedService", meds_DL, meds_DN, meds_SC, meds_GL); }
                if (dim_hospital) { DBContext.Service().insertDimQual(command, CDW_HOSPITAL, CDW_HOSPITAL_GL, TDW_HOSPITAL, LDW_HOSPITAL, id, 5, "Hospital", hospital_DL, hospital_DN, hospital_SC, hospital_GL); }
                if (dim_time) { DBContext.Service().insertDimQual(command, CDW_TIME, CDW_TIME_GL, TDW_TIME, LDW_TIME, id, 6, "Time", time_DL, time_DN, time_SC, time_GL); }



                // BMSR ->
                list.Clear();

                // Measures ->

                // Filter ->
                /*if (filter_variable.Checked)
                {
                    Console.WriteLine("entered_filter_variable -> Var");

                    DataTable sdt = DBContext.Service().GetData(
                    "select * from dw_dim_predicate " +
                    "where dim_pred_name = '" + defSC + "Var'");

                    //Console.WriteLine("query sucess");
                    DataTable sdt2 = sdt.Copy();
                    DataRow[] sdr = sdt2.Select();
                    String pred = sdr[0].ItemArray[0].ToString();
                    //Console.WriteLine("query sucess: -> " + pred);
                    int dim_pred = Int32.Parse(pred);

                    list.AddFirst(new Insert_item("NASS_DQ_SID", nass_dq_sid));
                    list.AddLast(new Insert_item("DIM_PRED_SID", dim_pred));
                    DBContext.Service().insinto("AGS_NASS_DIM_QUAL_SLICE_COND", "NASS_SC_SID", list);

                }
                else if (ldw.SelectedValue != null)
                {
                    Console.WriteLine("entered slice: ");
                    Console.WriteLine(ldw.SelectedValue.ToString());

                    foreach (Object sel in ldw.SelectedItems)
                    {
                        Console.WriteLine((sel as DataRowView)["DIM_PRED_SID"]);
                        list.AddFirst(new Insert_item("NASS_DQ_SID", nass_dq_sid));
                        list.AddLast(new Insert_item("DIM_PRED_SID", Int32.Parse((sel as DataRowView)["DIM_PRED_SID"].ToString())));
                        DBContext.Service().insinto("AGS_NASS_DIM_QUAL_SLICE_COND", "NASS_SC_SID", list);
                        list.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("entered slice empty --- def: " + defSC);

                    DataTable sdt = DBContext.Service().GetData(
                  "select dim_pred_sid from dw_dim_predicate where dim_pred_name like '%true" + defSC + "%'");

                    //Console.WriteLine("query sucess");
                    DataTable sdt2 = sdt.Copy();
                    DataRow[] sdr = sdt2.Select();
                    String pred = sdr[0].ItemArray[0].ToString();
                    //Console.WriteLine("query sucess: -> " + pred);
                    int dim_pred = Int32.Parse(pred);


                    //Console.WriteLine("dim_pred_empty: " + dim_pred);

                    // insert
                    list.AddFirst(new Insert_item("NASS_DQ_SID", nass_dq_sid));
                    list.AddLast(new Insert_item("DIM_PRED_SID", dim_pred));
                    DBContext.Service().insinto("AGS_NASS_DIM_QUAL_SLICE_COND", "NASS_SC_SID", list);

                }
                */
                transaction.Commit();
                Console.WriteLine("Transaction sucessful");
            }
            catch (Exception e)
            {
                try
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back");
                }
                catch (SqlException ex)
                {
                    if (transaction.Connection != null)
                    {
                        Console.WriteLine("An exception of type " + ex.GetType() +
                        " was encountered while attempting to roll back the transaction.");
                    }
                    else
                    {
                        Console.WriteLine("something unknown happened");
                    }
                }
                Console.WriteLine("An exception of type " + e.GetType() +
                " was encountered while inserting the data.");
                Console.WriteLine("Nothing was written to database.");
            }
        }

        // START: disable GL if DL is variable
        private void time_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (time_DL.CheckState == CheckState.Checked)
            {
                CDW_TIME_GL.SelectedIndex = -1;
                CDW_TIME_GL.Enabled = false;
            } else
            {
                CDW_TIME_GL.Enabled = true;
            }
        }

        private void insurant_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (insurant_DL.CheckState == CheckState.Checked)
            {
                CDW_INSURANT_GL.SelectedIndex = -1;
                CDW_INSURANT_GL.Enabled = false;
            }
            else
            {
                CDW_INSURANT_GL.Enabled = true;
            }
        }

        private void meds_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (meds_DL.CheckState == CheckState.Checked)
            {
                CDW_MEDSERVICE_GL.SelectedIndex = -1;
                CDW_MEDSERVICE_GL.Enabled = false;
            }
            else
            {
                CDW_MEDSERVICE_GL.Enabled = true;
            }
        }

        private void hospital_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (hospital_DL.CheckState == CheckState.Checked)
            {
                CDW_HOSPITAL_GL.SelectedIndex = -1;
                CDW_HOSPITAL_GL.Enabled = false;
            }
            else
            {
                CDW_HOSPITAL_GL.Enabled = true;
            }
        }

        private void doctor_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (doctor_DL.CheckState == CheckState.Checked)
            {
                CDW_DOCTOR_GL.SelectedIndex = -1;
                CDW_DOCTOR_GL.Enabled = false;
            }
            else
            {
                CDW_DOCTOR_GL.Enabled = true;
            }
        }

        private void drug_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (drug_DL.CheckState == CheckState.Checked)
            {
                CDW_DRUG_GL.SelectedIndex = -1;
                CDW_DRUG_GL.Enabled = false;
            }
            else
            {
                CDW_DRUG_GL.Enabled = true;
            }
        }
        // END: disable GL if DL is variable
    }
}
