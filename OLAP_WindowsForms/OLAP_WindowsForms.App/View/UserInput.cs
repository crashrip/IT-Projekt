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


namespace OLAP_WindowsForms.App
{
    public partial class UserInput : Form
    {
        public UserInput()
        {
            InitializeComponent();

            // fill combobox with data preview from cube
            getComboboxContent(comboBox1, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
            //getListBoxContent(listBox1, "DW_DERIVED_BASE_MEASURE", "DBMSR_EXPR", "DBMSR_NAME"); 
            //getListBoxContent(listBox2, "DW_DERIVED_AGGREGATE_MEASURE", "DAMSR_EXPR", "DAMSR_NAME");

            // instantiate dimension qualifications
            // DL
            fillComboboxDimension(CDW_TIME, 6); //time

            // DN
            // SC
            // GL
            fillComboboxDimension(CDW_TIME_GL, 6); //time

        }

        // get data value and description from cube for certain table, 2 columns and a combobox
        public void getComboboxContent(ComboBox combobox,String table, String column1, String column2)
        {
            DataTable dt = DBContext.Service().GetData(table,column1,column2);
            DataTable dt2 = dt.Copy();

            combobox.DataSource = dt2;
            combobox.DisplayMember = column2;
            combobox.ValueMember = column1;
        }

        // get data value and description from cube for certain table, 2 columns and a combobox
        public void getListBoxContent(ListBox listbox, String table, String column1, String column2)
        {
            DataTable dt = DBContext.Service().GetData(table, column1, column2);
            DataTable dt2 = dt.Copy();
            
            listbox.DataSource = dt2;
            listbox.DisplayMember = column2;
            listbox.ValueMember = column1;
            
            listbox.SelectionMode = SelectionMode.MultiExtended;
        }



        // test labels
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = comboBox1.SelectedValue.ToString();

            

            if (!(comboBox1.SelectedValue.ToString().Equals("System.Data.DataRowView")))
            {
                listBox1_Instantiate();
                listBox2_Instantiate();
            }
        }
        // Fill Derived Base Measures considering selected Cube
        private void listBox1_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            listBox1.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt = DBContext.Service().GetData(
               "SELECT DBMSR_NAME, DBMSR_EXPR " +
               "FROM (DW_CUBE NATURAL JOIN DW_CUBE_DERIVED_BASE_MEASURE) NATURAL JOIN DW_DERIVED_BASE_MEASURE " +
               "WHERE DW_CUBE.CUBE_SID = " + comboBox1.SelectedValue.ToString()
                );
            DataTable dt2 = dt.Copy();
            // set DataSourc
            listBox1.DataSource = dt2;
            listBox1.DisplayMember = "DBMSR_NAME";
            listBox1.ValueMember = "DBMSR_EXPR";
            listBox1.SelectionMode = SelectionMode.MultiExtended;
        }

        //Fill Measures considering selected Cube
        private void listBox2_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            listBox2.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt3 = DBContext.Service().GetData(
               "SELECT DAMSR_NAME, DAMSR_EXPR " +
               "FROM (DW_CUBE NATURAL JOIN DW_CUBE_DERIVED_AGGREGATE_MEASURE) NATURAL JOIN DW_DERIVED_AGGREGATE_MEASURE " +
               "WHERE DW_CUBE.CUBE_SID = " + comboBox1.SelectedValue.ToString()
                );
            DataTable dt4 = dt3.Copy();
            // set DataSourc
            listBox2.DataSource = dt4;
            listBox2.DisplayMember = "DAMSR_NAME";
            listBox2.ValueMember = "DAMSR_EXPR";
            listBox2.SelectionMode = SelectionMode.MultiExtended;

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                label6.Text = "";
                foreach (Object sel in listBox1.SelectedItems)
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
            foreach (Object sel in listBox2.SelectedItems)
            {
                label7.Text += (sel as DataRowView)["DAMSR_EXPR"].ToString();
                label7.Text += " ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string table = "DW_LEVEL";
            DBContext._dataView = new DataView();
            DBContext.DataView().Text = table;
            DBContext.DataView().LoadData(DBContext.Service().GetData(
               "SELECT LVL_POSITION " +
               "FROM DW_LEVEL "+ 
               "WHERE LVL_SID = 4" 
));
            /*
            "SELECT " + targetColumn +
              "FROM " + table +
              "WHERE " + idColumn + " = " + id
            */
            DBContext.DataView().Show();

            
        }

        private void fillComboboxDimension(ComboBox cBox, int dim_sid)
        {
            DataTable dt = DBContext.Service().GetData(
              "SELECT LVL_SID, LVL_NAME " +
              "FROM DW_LEVEL " +
              "WHERE DIM_SID = "+dim_sid+
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
              "AND LVL_POSITION < "+lvl_pos+
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

        private String getID(String table, String idColumn, String id, String targetColumn)
        {
            DataTable dt = DBContext.Service().GetData(
              "SELECT " + targetColumn+
              "FROM " + table +
              "WHERE "+idColumn+" = " + id
               );

            return string.Join(", ",dt.Rows.OfType<DataRow>().Select(r => r[0].ToString()));

        }


        private void CDW_TIME_SelectedIndexChanged(object sender, EventArgs e)
        {
            // fill GL
            //fillComboboxDimensionGroupedBy(CDW_TIME_GL,6,CDW_TIME.SelectedValue.ToString());
            // changes Listbox SC only if something is selected
            if (!(CDW_TIME.SelectedValue.ToString().Equals("System.Data.DataRowView")))
            {
                fillListBoxDimension(LDW_TIME, CDW_TIME.SelectedValue.ToString());
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label13.Text = TDW_TIME.Text;
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

        private void label13_Click(object sender, EventArgs e)
        {
            if (!(CDW_TIME.SelectedValue.ToString().Equals("System.Data.DataRowView")))
            {
                label13.Text = getID("DW_LEVEL", "LVL_SID", "26", "LVL_POSITION");
            }
            
        }

        
    }
}
