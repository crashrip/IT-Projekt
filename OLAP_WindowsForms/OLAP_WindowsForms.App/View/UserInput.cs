﻿using System;
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

namespace OLAP_WindowsForms.App
{
    public partial class UserInput : Form
    {
        public UserInput()
        {
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
                disable_dimensions();

                bmsr_Instantiate();
                measures_Instantiate();
                filter_Instantiate();

                dimension_enable_disable();
            }
        }

        private void disable_dimensions()
        {
            CDW_DOCTOR.Enabled = false;
            CDW_INSURANT.Enabled = false;
            CDW_DRUG.Enabled = false;
            CDW_MEDSERVICE.Enabled = false;
            CDW_HOSPITAL.Enabled = false;
            CDW_TIME.Enabled = false;
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
                    fillComboboxDimension(CDW_DOCTOR, 1); //doctor
                }
                if (dim_sid.Equals("2"))
                {
                    CDW_INSURANT.Enabled = true;
                    fillComboboxDimension(CDW_INSURANT, 2); //insurant
                }
                if (dim_sid.Equals("3"))
                {
                    CDW_DRUG.Enabled = true;
                    fillComboboxDimension(CDW_DRUG, 3); //drug
                }
                if (dim_sid.Equals("4"))
                {
                    CDW_MEDSERVICE.Enabled = true;
                    fillComboboxDimension(CDW_MEDSERVICE, 4); //medservice
                }
                if (dim_sid.Equals("5"))
                {
                    CDW_HOSPITAL.Enabled = true;
                    fillComboboxDimension(CDW_HOSPITAL, 5); //hospital
                }
                if (dim_sid.Equals("6"))
                {
                    CDW_TIME.Enabled = true;
                    fillComboboxDimension(CDW_TIME, 6); //time
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
            foreach (Object sel in LDW_BMSR.SelectedItems)
            {
                label6.Text += (sel as DataRowView)["DBMSR_EXPR"].ToString();
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
              "SELECT DIM_PRED_NAME, DIM_PRED_EXPR " +
              "FROM DW_DIM_PREDICATE " +
              "WHERE LVL_SID = " + lvl_sid
               );
            DataTable dt2 = dt.Copy();

            lBox.DataSource = dt2;
            lBox.DisplayMember = "DIM_PRED_NAME";
            lBox.ValueMember = "DIM_PRED_EXPR";
            lBox.SelectionMode = SelectionMode.MultiExtended;
        }

        // FILL DW GL AND SC
        private void CDW_TIME_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_TIME.SelectedIndex != -1)
            {
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
            }
        }
        private void CDW_INSURANT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_INSURANT.SelectedIndex != -1)
            {
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
        }

        private void CDW_MEDSERVICE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_MEDSERVICE.SelectedIndex != -1)
            {
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
        }

        private void CDW_DRUG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_DRUG.SelectedIndex != -1)
            {
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
        }
        private void CDW_DOCTOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_DOCTOR.SelectedIndex != -1)
            {
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
        }

        private void CDW_HOSPITAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CDW_HOSPITAL.SelectedIndex != -1)
            {
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
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        // check that input is numbers only
        private void TDW_TIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        private void LDW_TIME_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CDW_TIME_GL_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            Console.WriteLine(e.KeyChar);
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

        
    }
}
