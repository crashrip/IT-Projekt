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
        public int loaded_ags_sid;
        public int loaded_ass_sid;
        public Boolean overrideSchema = false; // true = override existing schema | false = create new schema based on selection
        public String name = "userinput";
        public String description = "";
        public Boolean dim_doctor = false;
        public Boolean dim_insurance = false;
        public Boolean dim_drug = false;
        public Boolean dim_medservice = false;
        public Boolean dim_hospital = false;
        public Boolean dim_time = false;
        public Boolean newForm; // true = new Form | false = load old form

        //bools for var Checkboxes
        public bool bmsr_var = false;
        public bool filter_var = false;

        public bool time_DL_var = false;
        public bool time_DN_var = false;
        public bool time_SC_var = false;
        public bool time_GL_var = false;

        public bool insurant_DL_var = false;
        public bool insurant_DN_var = false;
        public bool insurant_SC_var = false;
        public bool insurant_GL_var = false;
                                    
        public bool medService_DL_var = false;
        public bool medService_DN_var = false;
        public bool medService_SC_var = false;
        public bool medService_GL_var = false;

        public bool drug_DL_var = false;
        public bool drug_DN_var = false;
        public bool drug_SC_var = false;
        public bool drug_GL_var = false;

        public bool doctor_DL_var = false;
        public bool doctor_DN_var = false;
        public bool doctor_SC_var = false;
        public bool doctor_GL_var = false;

        public bool hospital_DL_var = false;
        public bool hospital_DN_var = false;
        public bool hospital_SC_var = false;
        public bool hospital_GL_var = false;

        private bool granLvlUsed = false;
        private bool diceLvlUsed = false;
        private bool refocusSliceCondUsed = false;
        private bool bmsrFilterUsed = false;
        private bool measureUsed = false;
        private bool amsrUsed = false;
        private bool drillAcross = false;

        public SelectTable selectTable;

        public UserInput(int ags_sid, Boolean newForm, int ass_sid = 0)
        {
            if (newForm)
            {
                this.loaded_ags_sid = ags_sid;
                InitializeComponent();
                // fill combobox with data preview from cube
                ComboItem.SetComboboxContent(ComboBoxCube, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
            } else
            {
                this.load(ags_sid,ass_sid);
            }
        }

        // start Form with Cube Dimension Selection
        public void comboBoxCube_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(ComboBoxCube.SelectedValue.ToString().Equals("System.Data.DataRowView")))
            {
                disable_dimensions();
                bmsr_Instantiate();
                measures_Instantiate();
                filter_Instantiate();
                dimension_enable_disable();
            }
        }

        // disables all dimension qualification elements
        public void disable_dimensions()
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

        // disable all fields except variables 
        public void disable_fields()
        {
            ComboBoxCube.Enabled = false;
            LDW_MEASURES.Enabled = false;
            if (bmsr_variable.Checked) LDW_BMSR.Enabled = true;
            if (filter_variable.Checked) LDW_FILTER.Enabled = true;
            if (doctor_DL.Checked) CDW_DOCTOR.Enabled = true;
            if (doctor_DN.Checked) TDW_DOCTOR.Enabled = true;
            if (doctor_SC.Checked) LDW_DOCTOR.Enabled = true;
            if (doctor_GL.Checked) CDW_DOCTOR_GL.Enabled = true;
            if (insurant_DL.Checked) CDW_INSURANT.Enabled = true;
            if (insurant_DN.Checked) TDW_INSURANT.Enabled = true;
            if (insurant_SC.Checked) LDW_INSURANT.Enabled = true;
            if (insurant_GL.Checked) CDW_INSURANT_GL.Enabled = true;
            if (drug_DL.Checked) CDW_DRUG.Enabled = true;
            if (drug_DN.Checked) TDW_DRUG.Enabled = true;
            if (drug_SC.Checked) LDW_DRUG.Enabled = true;
            if (drug_GL.Checked) CDW_DRUG_GL.Enabled = true;
            if (meds_DL.Checked) CDW_MEDSERVICE.Enabled = true;
            if (meds_DN.Checked) TDW_MEDSERVICE.Enabled = true;
            if (meds_SC.Checked) LDW_MEDSERVICE.Enabled = true;
            if (meds_GL.Checked) CDW_MEDSERVICE_GL.Enabled = true;
            if (hospital_DL.Checked) CDW_HOSPITAL.Enabled = true;
            if (hospital_DN.Checked) TDW_HOSPITAL.Enabled = true;
            if (hospital_SC.Checked) LDW_HOSPITAL.Enabled = true;
            if (hospital_GL.Checked) CDW_HOSPITAL_GL.Enabled = true;
            if (time_DL.Checked) CDW_TIME.Enabled = true;
            if (time_DN.Checked) TDW_TIME.Enabled = true;
            if (time_SC.Checked) LDW_TIME.Enabled = true;
            if (time_GL.Checked) CDW_TIME_GL.Enabled = true;

            if (!bmsr_variable.Checked) LDW_BMSR.Enabled = false;
            if (!filter_variable.Checked) LDW_FILTER.Enabled = false;
            if (!doctor_DL.Checked) CDW_DOCTOR.Enabled = false;
            if (!doctor_DN.Checked) TDW_DOCTOR.Enabled = false;
            if (!doctor_SC.Checked) LDW_DOCTOR.Enabled = false;
            if (!doctor_GL.Checked) CDW_DOCTOR_GL.Enabled = false;
            if (!insurant_DL.Checked) CDW_INSURANT.Enabled = false;
            if (!insurant_DN.Checked) TDW_INSURANT.Enabled = false;
            if (!insurant_SC.Checked) LDW_INSURANT.Enabled = false;
            if (!insurant_GL.Checked) CDW_INSURANT_GL.Enabled = false;
            if (!drug_DL.Checked) CDW_DRUG.Enabled = false;
            if (!drug_DN.Checked) TDW_DRUG.Enabled = false;
            if (!drug_SC.Checked) LDW_DRUG.Enabled = false;
            if (!drug_GL.Checked) CDW_DRUG_GL.Enabled = false;
            if (!meds_DL.Checked) CDW_MEDSERVICE.Enabled = false;
            if (!meds_DN.Checked) TDW_MEDSERVICE.Enabled = false;
            if (!meds_SC.Checked) LDW_MEDSERVICE.Enabled = false;
            if (!meds_GL.Checked) CDW_MEDSERVICE_GL.Enabled = false;
            if (!hospital_DL.Checked) CDW_HOSPITAL.Enabled = false;
            if (!hospital_DN.Checked) TDW_HOSPITAL.Enabled = false;
            if (!hospital_SC.Checked) LDW_HOSPITAL.Enabled = false;
            if (!hospital_GL.Checked) CDW_HOSPITAL_GL.Enabled = false;
            if (!time_DL.Checked) CDW_TIME.Enabled = false;
            if (!time_DN.Checked) TDW_TIME.Enabled = false;
            if (!time_SC.Checked) LDW_TIME.Enabled = false;
            if (!time_GL.Checked) CDW_TIME_GL.Enabled = false;
        }

        // enables allowed dimension qualification elements 
        public void dimension_enable_disable()
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
        public void bmsr_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            LDW_BMSR.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt = DBContext.Service().GetData(
               "select bmsr_pred_sid, bmsr_pred_name "+
               "from dw_bmsr_predicate "+
               "where bmsr_pred_sid > 0 AND cube_sid = "+ ComboBoxCube.SelectedValue.ToString());
            DataTable dt2 = dt.Copy();
            // set DataSource
            LDW_BMSR.DataSource = dt2;
            LDW_BMSR.DisplayMember = "bmsr_pred_name";
            LDW_BMSR.ValueMember = "bmsr_pred_sid";
            LDW_BMSR.SelectionMode = SelectionMode.MultiExtended;
        }

        //Fill Measures considering selected Cube
        public void measures_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            LDW_MEASURES.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt3 = DBContext.Service().GetData(
               "select c.damsr_sid, d.damsr_name "+
               "from dw_cube_derived_aggregate_measure c inner join "+
               "dw_derived_aggregate_measure d on c.damsr_sid = d.damsr_sid "+
               "where c.cube_sid = " + ComboBoxCube.SelectedValue.ToString()
                );
            DataTable dt4 = dt3.Copy();
            // set DataSource
            LDW_MEASURES.DataSource = dt4;
            LDW_MEASURES.DisplayMember = "damsr_name";
            LDW_MEASURES.ValueMember = "damsr_sid";
            LDW_MEASURES.SelectionMode = SelectionMode.MultiExtended;
        }

        // Fill DW_BMSR_PREDICATE considering selected Cube
        public void filter_Instantiate()
        {
            // Disable Selection Mode while instanciating DataSource
            LDW_FILTER.SelectionMode = SelectionMode.None;
            // SQL Query 
            DataTable dt = DBContext.Service().GetData(
               "select amsr_pred_sid, amsr_pred_name "+
               "from dw_amsr_predicate "+
               "where cube_sid = " + ComboBoxCube.SelectedValue.ToString()+ " AND AMSR_PRED_SID > 0"
                );
            DataTable dt2 = dt.Copy();
            // set DataSource
            LDW_FILTER.DataSource = dt2;
            LDW_FILTER.DisplayMember = "AMSR_PRED_NAME";
            LDW_FILTER.ValueMember = "AMSR_PRED_SID";
            LDW_FILTER.SelectionMode = SelectionMode.MultiExtended;
        }

        // START -------- general DimensionQualification fill operations ---------------------
        public void fillComboboxDimension(ComboBox cBox, int dim_sid) // DL
        {
            DataTable dt = DBContext.Service().GetData(
              "SELECT LVL_SID, LVL_NAME " +
              "FROM DW_LEVEL " +
              "WHERE DIM_SID = " + dim_sid + " AND LVL_SID > 0 " +
              "ORDER BY LVL_SID DESC"
               );
            DataTable dt2 = dt.Copy();

            cBox.DataSource = dt2;
            cBox.DisplayMember = "LVL_NAME";
            cBox.ValueMember = "LVL_SID";
        }

        public void fillListBoxDimension(ListBox lBox, int dim_sid, string lvl_sid)
        {
            try
            {
                lBox.SelectionMode = SelectionMode.None;
                DataTable dt = DBContext.Service().GetData(
                "Select DIM_PRED_NAME, DIM_PRED_SID " +
                "from dw_dim_predicate p inner join dw_level l on p.lvl_sid = l.lvl_sid where l.dim_sid = " + dim_sid + " and p.dim_pred_expr = 'TRUE' " +
                "union " +
                "Select DIM_PRED_NAME, DIM_PRED_SID " +
                "from dw_dim_predicate p inner " +
                "join dw_level l on p.lvl_sid = l.lvl_sid where l.dim_sid = " + dim_sid +
                " and l.lvl_sid > 0 and l.lvl_position <= (select lvl_position from dw_level where lvl_sid = " + lvl_sid + ")");
                DataTable dt2 = dt.Copy();

                lBox.DataSource = dt2;
                lBox.DisplayMember = "DIM_PRED_NAME";
                lBox.ValueMember = "DIM_PRED_SID";
                lBox.SelectionMode = SelectionMode.MultiExtended;
            } catch ( Exception e)
            {
                Console.WriteLine("ERROR: " + e.Message);
            } 
        }

        public void fillGL(ComboBox readFromCB, int dim_sid, ComboBox changeThisCB)
        {
            DataTable dt = DBContext.Service().GetData(
                "SELECT *" +
               "FROM DW_LEVEL " +
               "WHERE LVL_SID <= " + readFromCB.SelectedValue + " AND DIM_SID =" + dim_sid + " AND LVL_SID > 0 "+
               "ORDER BY LVL_SID DESC"
               );
            DataTable dt2 = dt.Copy();
            changeThisCB.DataSource = dt2;
            changeThisCB.DisplayMember = "LVL_NAME";
            changeThisCB.ValueMember = "LVL_SID";
        }
        // END -------- general DimensionQualification fill operations ---------------------

        // START --------------FILL DW GL AND SC-----------------------------------------
        public void CDW_TIME_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("time: " + CDW_TIME.SelectedIndex);
            if (CDW_TIME.SelectedIndex != -1)
            {
                CDW_TIME_GL.Enabled = true;
                // fill GL
                if (!(CDW_TIME.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillGL(CDW_TIME, 6, CDW_TIME_GL);
                }
                // changes Listbox SC only if something is selected
                if (!(CDW_TIME.SelectedValue.ToString().Equals("System.Data.DataRowView")))
                {
                    fillListBoxDimension(LDW_TIME,6, CDW_TIME.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_TIME_GL.SelectedIndex = -1;
                CDW_TIME_GL.Enabled = false;
            }
        }
        public void CDW_INSURANT_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("insurant: " + CDW_INSURANT.SelectedIndex);
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
                    fillListBoxDimension(LDW_INSURANT,2, CDW_INSURANT.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_INSURANT_GL.SelectedIndex = -1;
                CDW_INSURANT_GL.Enabled = false;
            }
        }
        public void CDW_MEDSERVICE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("medservice: " + CDW_MEDSERVICE.SelectedIndex);
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
                    fillListBoxDimension(LDW_MEDSERVICE,4, CDW_MEDSERVICE.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_MEDSERVICE_GL.SelectedIndex = -1;
                CDW_MEDSERVICE_GL.Enabled = false;
            }
        }
        public void CDW_DRUG_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("drug: " + CDW_DRUG.SelectedIndex);
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
                    fillListBoxDimension(LDW_DRUG,3, CDW_DRUG.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_DRUG_GL.SelectedIndex = -1;
                CDW_DRUG_GL.Enabled = false;
            }
        }
        public void CDW_DOCTOR_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Console.WriteLine("doctor: " + CDW_DOCTOR.SelectedIndex);
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
                    fillListBoxDimension(LDW_DOCTOR,1, CDW_DOCTOR.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_DOCTOR_GL.SelectedIndex = -1;
                CDW_DOCTOR_GL.Enabled = false;
            }
        }
        public void CDW_HOSPITAL_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Console.WriteLine("hospital: " + CDW_HOSPITAL.SelectedIndex);
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
                    fillListBoxDimension(LDW_HOSPITAL,5, CDW_HOSPITAL.SelectedValue.ToString());
                }
            }
            else
            {
                CDW_HOSPITAL_GL.SelectedIndex = -1;
                CDW_HOSPITAL_GL.Enabled = false;
            }
        }
        // END --------------FILL DW GL AND SC-----------------------------------------

        // check that input is numbers only
        public void TDW_TIME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }
        }

        public void deselect(object sender, KeyPressEventArgs e)
        {
            //Console.WriteLine(e.KeyChar);
            if (e.KeyChar == ' ')
            {
                if (sender.GetType().ToString().Equals("System.Windows.Forms.ListBox"))
                {
                    (sender as ListBox).ClearSelected();
                    (sender as ListBox).SelectedIndex = - 1;
                    //Console.WriteLine((sender as ListBox).ToString()+" -> "+(sender as ListBox).SelectedIndex);
                }
                if (sender.GetType().ToString().Equals("System.Windows.Forms.ComboBox"))
                {
                    (sender as ComboBox).SelectedIndex = 0;
                    (sender as ComboBox).SelectedIndex = -1;
                }
            }
        }

        // START ------------------- Buttons ------------------------------------------
        public void button_cancel_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "New Schema",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        public void button_select_navigation_operator_Click(object sender, EventArgs e)
        {
            // save schema
            SaveSchema save = new SaveSchema(this) { TopMost = true };
            save.ShowDialog(this);

            SelectNavigationOperator sno = new SelectNavigationOperator(this) { TopMost = true };
            sno.ShowDialog(this);
        }

        public void button_save_Click(object sender, EventArgs e)
        {
            SaveSchema save = new SaveSchema(this) { TopMost = true };
            save.ShowDialog(this);
        }
        // END ------------------- Buttons ------------------------------------------

        // load schema from cube MARIA
        public void load(int ags_sid, int ass_sid)
        {
            InitializeComponent();
            this.loaded_ags_sid = ags_sid;
            this.loaded_ass_sid = ass_sid;
            this.overrideSchema = true;
            try
            {

                Console.WriteLine("AGS_SID: " + loaded_ags_sid + " ASS_SID: " + loaded_ass_sid);
                // load cube_sid
                int cube_sid = DBContext.Service().GetKeyfromTable("AGS_NON_CMP_ASS", loaded_ass_sid, "ASS_SID_NASS", "CUBE_SID");
                String cube_name = DBContext.Service().GetSKeyfromTable("DW_CUBE", cube_sid, "CUBE_SID", "CUBE_NAME");
                // initialize combobox
                ComboItem.SetComboboxContent(ComboBoxCube, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
                if (cube_sid >= 0)
                {
                    ComboBoxCube.SelectedIndex = ComboBoxCube.FindStringExact(cube_name); ; // set to actual value
                    comboBoxCube_SelectedIndexChanged(ComboBoxCube, new EventArgs());
                    //if (ComboBoxCube.SelectedIndex == 0) dimension_enable_disable();// to start handlers

                    // set Dimension Qualifications & slice condition
                    DataTable dt_dim_qual = DBContext.Service().GetData(
                        "SELECT NASS_DQ_SID, DIM_SID, LVL_SID_DICELVL, NASS_DQ_DICE_NODE, LVL_SID_GRANLVL " +
                        "FROM AGS_NASS_DIM_QUAL " +
                        "WHERE ASS_SID_NASS = " + ass_sid);
                    DataTable dt2_dim_qual = dt_dim_qual.Copy();

                    foreach (DataRow row in dt2_dim_qual.Rows)
                    {
                        object dim = row["DIM_SID"];
                        object nass_dq = row["NASS_DQ_SID"];
                        object dl = row["LVL_SID_DICELVL"];
                        object dn = row["NASS_DQ_DICE_NODE"];
                        object gl = row["LVL_SID_GRANLVL"];

                        // SC
                        int nass_dq_sid = Convert.ToInt32(nass_dq);
                        LinkedList<int> sc_dim_pred = new LinkedList<int>(); //dim_pred
                                                                             //LinkedList<int> sc_lvl_sid = new LinkedList<int>(); // lvl_sid to figure out if variable or actual value
                        DataTable dt_dim_sc = DBContext.Service().GetData(
                        "SELECT DIM_PRED_SID " +
                        "FROM AGS_NASS_DIM_QUAL_SLICE_COND " +
                        "WHERE NASS_DQ_SID = " + nass_dq_sid);
                        DataTable dt2_dim_sc = dt_dim_sc.Copy();


                        int dim_i = Convert.ToInt32(dim);
                        int dl_i = -11;
                        String dn_s = "asdf";
                        int gl_i = -11;
                        String dl_s = "";
                        String gl_s = "";

                        Boolean dlNull = false;
                        Boolean dnNull = false;
                        Boolean glNull = false;

                        if (dl == DBNull.Value)
                        {
                            dlNull = true;
                        }
                        else
                        {
                            dl_i = Convert.ToInt32(dl);
                            dl_s = DBContext.Service().GetSKeyfromTable("DW_LEVEL", dl_i, "LVL_SID", "LVL_NAME");
                        }
                        if (dn == DBNull.Value)
                        {
                            dnNull = true;
                        }
                        else
                        {
                            dn_s = Convert.ToString(dn);
                        }
                        if (gl == DBNull.Value)
                        {
                            glNull = true;
                        }
                        else
                        {
                            gl_i = Convert.ToInt32(gl);
                            gl_s = DBContext.Service().GetSKeyfromTable("DW_LEVEL", gl_i, "LVL_SID", "LVL_NAME");
                        }
                        Console.WriteLine("dim: " + dim_i);
                        if (dlNull) { Console.Write(" DL: Null "); } else { Console.Write(" DL: " + dl_i); }
                        if (dnNull) { Console.Write(" DN: Null "); } else { Console.Write(" DN: " + dn_s); }
                        if (glNull) { Console.Write(" GL: Null \n"); } else { Console.Write(" GL: " + gl_i + "\n"); }

                        switch (dim_i)
                        {
                            case 1: // DOCTOR
                                if (!dlNull)
                                {
                                    if (dl_i < 0)
                                    {
                                        doctor_DL.Checked = true;
                                        doctor_DL_var = true;
                                    }
                                    if (dl_i > 0)
                                    {
                                        CDW_DOCTOR.SelectedIndex = CDW_DOCTOR.FindString(dl_s);
                                        CDW_DOCTOR_SelectedIndexChanged(CDW_DOCTOR, new EventArgs());
                                        //CDW_DOCTOR.Enabled = false;
                                        foreach (DataRow r in dt2_dim_sc.Rows)
                                        {
                                            int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                            if (dim_pred_sid < 0)
                                            {
                                                doctor_SC.Checked = true;
                                                doctor_SC_var = true;
                                            }
                                            if (dim_pred_sid > 0)
                                            {
                                                
                                                String key = DBContext.Service().GetSKeyfromTable("DW_DIM_PREDICATE", dim_pred_sid, "DIM_PRED_SID", "DIM_PRED_NAME");
                                                int index = LDW_DOCTOR.FindString(key);
                                                LDW_DOCTOR.SetSelected(index, true);
                                                //LDW_DOCTOR.Enabled = false;
                                            }
                                        }
                                    }
                                    // SC
                                    foreach (DataRow r in dt2_dim_sc.Rows)
                                    {
                                        int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                        if (dim_pred_sid < 0)
                                        {
                                            doctor_SC.Checked = true;
                                            doctor_SC_var = true;
                                        }
                                    }
                                }
                                if (!dnNull)
                                {
                                    if (dn_s.Equals(""))
                                    {
                                        doctor_DN.Checked = true;
                                        doctor_DN_var = true;
                                    }
                                    else
                                    {
                                        TDW_DOCTOR.Text = dn_s;
                                        //TDW_DOCTOR.Enabled = false;
                                    }
                                }
                                if (!glNull)
                                {
                                    if (gl_i < 0)
                                    {
                                        doctor_GL.Checked = true;
                                        doctor_GL_var = true;
                                    }
                                    if (gl_i > 0)
                                    {
                                        CDW_DOCTOR_GL.SelectedIndex = CDW_DOCTOR_GL.FindString(gl_s);
                                        //CDW_DOCTOR_GL.Enabled = false;
                                    }
                                }
                                break;
                            case 2: //INSURANT
                                if (!dlNull)
                                {
                                    if (dl_i < 0)
                                    {
                                        insurant_DL.Checked = true;
                                        insurant_DL_var = true;
                                    }
                                    if (dl_i > 0)
                                    {
                                        CDW_INSURANT.SelectedIndex = CDW_INSURANT.FindString(dl_s);
                                        CDW_INSURANT_SelectedIndexChanged(CDW_INSURANT, new EventArgs());

                                        // SC
                                        foreach (DataRow r in dt2_dim_sc.Rows)
                                        {

                                            int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                            Console.WriteLine("insurant entered ->" + dim_pred_sid);
                                            if (dim_pred_sid < 0)
                                            {
                                                Console.WriteLine("variable insurant");
                                                insurant_SC.Checked = true;
                                                insurant_SC_var = true;
                                            }

                                            if (dim_pred_sid > 0)
                                            {
                                                Console.WriteLine("insurant: ");
                                                String key = DBContext.Service().GetSKeyfromTable("DW_DIM_PREDICATE", dim_pred_sid, "DIM_PRED_SID", "DIM_PRED_NAME");
                                                int index = LDW_INSURANT.FindString(key);
                                                Console.WriteLine("insurant_: " + key + " index: " + index);
                                                LDW_INSURANT.SetSelected(index, true);
                                                //LDW_DOCTOR.Enabled = false;
                                            }
                                        }
                                    }
                                    // SC
                                    foreach (DataRow r in dt2_dim_sc.Rows)
                                    {

                                        int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                        Console.WriteLine("insurant entered ->"+dim_pred_sid);
                                        if (dim_pred_sid < 0)
                                        {
                                            Console.WriteLine("variable insurant");
                                            insurant_SC.Checked = true;
                                            insurant_SC_var = true;
                                        }
                                        
                                    }
                                }
                                if (!dnNull)
                                {
                                    if (dn_s.Equals(""))
                                    {
                                        insurant_DN.Checked = true;
                                        insurant_DN_var = true;
                                    }
                                    else
                                    {
                                        TDW_INSURANT.Text = dn_s;
                                    }
                                }
                                if (!glNull)
                                {
                                    if (gl_i < 0)
                                    {
                                        insurant_GL.Checked = true;
                                        insurant_GL_var = true;
                                    }
                                    if (gl_i > 0) CDW_INSURANT_GL.SelectedIndex = CDW_INSURANT_GL.FindString(gl_s);
                                }
                                break;
                            case 3: // DRUG
                                if (!dlNull)
                                {
                                    if (dl_i < 0)
                                    {
                                        drug_DL.Checked = true;
                                        drug_DL_var = true;
                                    }
                                    if (dl_i > 0)
                                    {
                                        CDW_DRUG.SelectedIndex = CDW_DRUG.FindString(dl_s);
                                        CDW_DRUG_SelectedIndexChanged(CDW_DRUG, new EventArgs());

                                        foreach (DataRow r in dt2_dim_sc.Rows)
                                        {
                                            
                                            int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                            Console.WriteLine("drug entered SC -> " +dim_pred_sid);
                                            if (dim_pred_sid < 0)
                                            {
                                                Console.WriteLine("variable drug");
                                                drug_SC.Checked = true;
                                                drug_SC_var = true;
                                            }
                                            
                                            if (dim_pred_sid > 0){
                                                Console.WriteLine("drug: ");
                                                String key = DBContext.Service().GetSKeyfromTable("DW_DIM_PREDICATE", dim_pred_sid, "DIM_PRED_SID", "DIM_PRED_NAME");
                                                int index = LDW_DRUG.FindString(key);
                                                Console.WriteLine("drug_: " + key + " index: " + index);
                                                LDW_DRUG.SetSelected(index, true);
                                                //LDW_DOCTOR.Enabled = false;
                                            }
                                        }
                                    }
                                    // SC
                                    foreach (DataRow r in dt2_dim_sc.Rows)
                                    {
                                        Console.WriteLine("drug entered SC");
                                        int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                        if (dim_pred_sid < 0)
                                        {
                                            Console.WriteLine("variable drug");
                                            drug_SC.Checked = true;
                                            drug_SC_var = true;
                                        }
                                    }
                                }
                                if (!dnNull)
                                {
                                    if (dn_s.Equals(""))
                                    {
                                        drug_DN.Checked = true;
                                        drug_DN_var = true;
                                    }
                                    else
                                    {
                                        TDW_DRUG.Text = dn_s;
                                    }
                                }
                                if (!glNull)
                                {
                                    if (gl_i < 0)
                                    {
                                        drug_GL.Checked = true;
                                        drug_GL_var = true;
                                    }
                                    if (gl_i > 0) CDW_DRUG_GL.SelectedIndex = CDW_DRUG_GL.FindString(gl_s);
                                }
                                break;
                            case 4: // MEDSERVICE
                                if (!dlNull)
                                {
                                    if (dl_i < 0)
                                    {
                                        meds_DL.Checked = true;
                                        medService_DL_var = true;
                                    }
                                    if (dl_i > 0)
                                    {
                                        CDW_MEDSERVICE.SelectedIndex = CDW_MEDSERVICE.FindString(dl_s);
                                        CDW_MEDSERVICE_SelectedIndexChanged(CDW_MEDSERVICE, new EventArgs());

                                        foreach (DataRow r in dt2_dim_sc.Rows)
                                        {
                                            int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                            if (dim_pred_sid < 0)
                                            {
                                                meds_SC.Checked = true;
                                                medService_SC_var = true;
                                            }

                                            if (dim_pred_sid > 0)
                                            {
                                                String key = DBContext.Service().GetSKeyfromTable("DW_DIM_PREDICATE", dim_pred_sid, "DIM_PRED_SID", "DIM_PRED_NAME");
                                                int index = LDW_MEDSERVICE.FindString(key);
                                                LDW_MEDSERVICE.SetSelected(index, true);
                                                //LDW_DOCTOR.Enabled = false;
                                            }
                                        }
                                    }
                                    // SC
                                    foreach (DataRow r in dt2_dim_sc.Rows)
                                    {
                                        int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                        if (dim_pred_sid < 0)
                                        {
                                            meds_SC.Checked = true;
                                            medService_SC_var = true;
                                        }
                                    }
                                }
                                if (!dnNull)
                                {
                                    if (dn_s.Equals(""))
                                    {
                                        meds_DN.Checked = true;
                                        medService_DN_var = true;
                                    }
                                    else
                                    {
                                        TDW_MEDSERVICE.Text = dn_s;
                                    }
                                }
                                if (!glNull)
                                {
                                    if (gl_i < 0)
                                    {
                                        meds_GL.Checked = true;
                                        medService_GL_var = true;
                                    }
                                    if (gl_i > 0) CDW_MEDSERVICE_GL.SelectedIndex = CDW_MEDSERVICE_GL.FindString(gl_s);
                                }
                                break;
                            case 5: // HOSPITAL
                                if (!dlNull)
                                {
                                    if (dl_i < 0)
                                    {
                                        hospital_DL.Checked = true;
                                        hospital_DL_var = true;
                                    }
                                    if (dl_i > 0)
                                    {
                                        CDW_HOSPITAL.SelectedIndex = CDW_HOSPITAL.FindString(dl_s);
                                        CDW_HOSPITAL_SelectedIndexChanged(CDW_HOSPITAL, new EventArgs());

                                        // SC
                                        foreach (DataRow r in dt2_dim_sc.Rows)
                                        {
                                            int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                            if (dim_pred_sid < 0)
                                            {
                                                hospital_SC.Checked = true;
                                                hospital_SC_var = true;
                                            }

                                            if (dim_pred_sid > 0)
                                            {
                                                String key = DBContext.Service().GetSKeyfromTable("DW_DIM_PREDICATE", dim_pred_sid, "DIM_PRED_SID", "DIM_PRED_NAME");
                                                int index = LDW_HOSPITAL.FindString(key);
                                                LDW_HOSPITAL.SetSelected(index, true);
                                                //LDW_DOCTOR.Enabled = false;
                                            }
                                        }
                                    }
                                    // SC
                                    foreach (DataRow r in dt2_dim_sc.Rows)
                                    {
                                        int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                        if (dim_pred_sid < 0)
                                        {
                                            hospital_SC.Checked = true;
                                            hospital_SC_var = true;
                                        }
                                    }
                                }
                                if (!dnNull)
                                {
                                    if (dn_s.Equals(""))
                                    {
                                        hospital_DN.Checked = true;
                                        hospital_DN_var = true;
                                    }
                                    else
                                    {
                                        TDW_HOSPITAL.Text = dn_s;
                                    }
                                }
                                if (!glNull)
                                {
                                    if (gl_i < 0)
                                    {
                                        hospital_GL.Checked = true;
                                        hospital_GL_var = true;
                                    }
                                    if (gl_i > 0) CDW_HOSPITAL_GL.SelectedIndex = CDW_HOSPITAL_GL.FindString(gl_s);
                                }
                                break;
                            case 6: //TIME
                                if (!dlNull)
                                {
                                    if (dl_i < 0)
                                    {
                                        time_DL.Checked = true;
                                        time_DL_var = true;
                                    }
                                    if (dl_i > 0)
                                    {
                                        CDW_TIME.SelectedIndex = CDW_TIME.FindString(dl_s);
                                        CDW_TIME_SelectedIndexChanged(CDW_TIME, new EventArgs());

                                        // SC
                                        foreach (DataRow r in dt2_dim_sc.Rows)
                                        {
                                            int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                            if (dim_pred_sid < 0)
                                            {
                                                time_SC.Checked = true;
                                                time_SC_var = true;
                                            }

                                            if (dim_pred_sid > 0)
                                            {
                                                String key = DBContext.Service().GetSKeyfromTable("DW_DIM_PREDICATE", dim_pred_sid, "DIM_PRED_SID", "DIM_PRED_NAME");
                                                int index = LDW_TIME.FindString(key);
                                                LDW_TIME.SetSelected(index, true);
                                                //LDW_DOCTOR.Enabled = false;
                                            }
                                        }
                                    }
                                    // SC
                                    foreach (DataRow r in dt2_dim_sc.Rows)
                                    {
                                        int dim_pred_sid = Convert.ToInt32(r["DIM_PRED_SID"]);
                                        if (dim_pred_sid < 0)
                                        {
                                            time_SC.Checked = true;
                                            time_SC_var = true;
                                        }
                                    }
                                }
                                if (!dnNull)
                                {
                                    if (dn_s.Equals(""))
                                    {
                                        time_DN.Checked = true;
                                        time_DN_var = true;
                                    }
                                    else
                                    {
                                        TDW_TIME.Text = dn_s;
                                    }
                                }
                                if (!glNull)
                                {
                                    if (gl_i < 0)
                                    {
                                        time_GL.Checked = true;
                                        time_GL_var = true;
                                    }
                                    if (gl_i > 0) CDW_TIME_GL.SelectedIndex = CDW_TIME_GL.FindString(gl_s);
                                }
                                break;
                        }

                    }
                    // load BMSR
                    LinkedList<int> bmsr = new LinkedList<int>(); //dim_pred
                    DataTable dt_bmsr = DBContext.Service().GetData(
                    "SELECT BMSR_PRED_SID " +
                    "FROM AGS_NASS_BMSR_FILTER " +
                    "WHERE ASS_SID_NASS = " + loaded_ass_sid);
                    DataTable dt2_bmsr = dt_bmsr.Copy();

                    foreach (DataRow row in dt2_bmsr.Rows)
                    {
                        object bmsr_o = row["BMSR_PRED_SID"];
                        int bmsr_int = Convert.ToInt32(bmsr_o);
                        Console.WriteLine("bmsr_int: " + bmsr_int);
                        if (bmsr_int < 0)
                        {
                            bmsr_variable.Checked = true;
                            bmsr_var = true;
                            LDW_BMSR.SelectedIndex = -1;
                        }
                        else
                        {
                            String key = DBContext.Service().GetSKeyfromTable("DW_BMSR_PREDICATE", bmsr_int, "BMSR_PRED_SID", "BMSR_PRED_NAME");
                            Console.WriteLine("key: " + key);
                            int index = LDW_BMSR.FindString(key);
                            Console.WriteLine("key_int: " + index);
                            LDW_BMSR.SetSelected(index, true);
                        }
                    }
                    // load Measures
                    LinkedList<int> measure = new LinkedList<int>(); //dim_pred
                    DataTable dt_measure = DBContext.Service().GetData(
                    "Select DAMSR_SID FROM ags_nass_aggregate_meassure WHERE ASS_SID_NASS = " + loaded_ass_sid);
                    DataTable dt2_measure = dt_measure.Copy();

                    foreach (DataRow row in dt2_measure.Rows)
                    {
                        object damsr_o = row["DAMSR_SID"];
                        int damsr_int = Convert.ToInt32(damsr_o);
                        Console.WriteLine("bmsr_int: " + damsr_int);

                        String key = DBContext.Service().GetSKeyfromTable("DW_DERIVED_AGGREGATE_MEASURE", damsr_int, "DAMSR_SID", "DAMSR_NAME");
                        Console.WriteLine("key: " + key);
                        int index = LDW_MEASURES.FindString(key);
                        Console.WriteLine("key_int: " + index);
                        LDW_MEASURES.SetSelected(index, true);

                    }
                    // load Filter
                    LinkedList<int> filter = new LinkedList<int>(); //dim_pred
                    DataTable dt_filter = DBContext.Service().GetData(
                    "Select AMSR_PRED_SID FROM ags_nass_amsr_filter WHERE ASS_SID_NASS = " + loaded_ass_sid);
                    DataTable dt2_filter = dt_filter.Copy();

                    foreach (DataRow row in dt2_filter.Rows)
                    {
                        object filter_o = row["AMSR_PRED_SID"];
                        int filter_int = Convert.ToInt32(filter_o);
                        Console.WriteLine("bmsr_int: " + filter_int);
                        if (filter_int < 0)
                        {
                            filter_variable.Checked = true;
                            filter_var = true;
                        }
                        else
                        {
                            String key = DBContext.Service().GetSKeyfromTable("dw_amsr_predicate", filter_int, "AMSR_PRED_SID", "AMSR_PRED_NAME");
                            Console.WriteLine("key: " + key);
                            int index = LDW_FILTER.FindString(key);
                            Console.WriteLine("key_int: " + index);
                            LDW_FILTER.SetSelected(index, true);
                        }
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // inser schema into cube
        public void insert(int ass_sid = -1)
        {
            //prepare Connection
            NpgsqlConnection connection = DBContext.Service().GetConnection();
            connection.Open();
            NpgsqlTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                Console.WriteLine("enter insert");

                LinkedList<Insert_item> list = new LinkedList<Insert_item>();

                int id = 0;
                // save ASS_SID 
                if (ass_sid == -1)
                {
                    DataTable dt = DBContext.Service().GetData(
                      "SELECT MAX(ASS_SID) FROM AGS_ANALYSIS_SITUATION_SCHEMA");
                    DataTable dt2 = dt.Copy();
                    DataRow[] dr = dt2.Select();
                    String index = dr[0].ItemArray[0].ToString();
                    if (index.Equals("") || index == null)
                    {
                        id = 1;
                    }
                    else
                    {
                        id = Int32.Parse(index) + 1; // -> +1 for current ass_sid
                        Console.WriteLine("id: "+id);
                    }
                }
                else
                {
                    id = ass_sid;
                }
                Console.WriteLine("final ass id: " + id);
                // AGS_ANALYSIS_SITUATION SCHEMA
                list.AddFirst(new Insert_item("ASS_SID", id));
                list.AddLast(new Insert_item("ASS_NAME", this.name));
                list.AddLast(new Insert_item("ASS_DESCRIPTION", this.description));
                list.AddLast(new Insert_item("AGS_SID", loaded_ags_sid)); //  Variable aus ags_analysis_graph_schema
                list.AddLast(new Insert_item("ASS_POS_X", 0));
                list.AddLast(new Insert_item("ASS_POS_Y", 0));
                DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_ANALYSIS_SITUATION_SCHEMA", list);

                
                list.Clear();
                // Cube -> AGS_NON_CMP_ASS 
                if (ComboBoxCube.SelectedIndex != -1 || (!(ComboBoxCube.SelectedValue.ToString().Equals("System.Data.DataRowView"))))
                {
                    Console.WriteLine("cube_sid: " + Int32.Parse(ComboBoxCube.SelectedValue.ToString()));
                    list.AddFirst(new Insert_item("ASS_SID_NASS", id));
                    list.AddLast(new Insert_item("ASS_USED_IN_CASS", 0)); // 1 used in comparative analysis , 0 used in non comparative analysis
                    list.AddLast(new Insert_item("CUBE_SID", Int32.Parse(ComboBoxCube.SelectedValue.ToString())));
                    DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NON_CMP_ASS", list);
                } 

                // DIM Qualification -> AGS_NASS_DIM_QUAL & AGS_NASS_DIM_QUAL_SLICE_COND
                if (dim_doctor) { DBContext.Service().InsertDimQual(connection, transaction, CDW_DOCTOR, CDW_DOCTOR_GL, TDW_DOCTOR, LDW_DOCTOR, id, 1, "Doctor", doctor_DL, doctor_DN, doctor_SC, doctor_GL); }
                if (dim_insurance) { DBContext.Service().InsertDimQual(connection, transaction, CDW_INSURANT, CDW_INSURANT_GL, TDW_INSURANT, LDW_INSURANT, id, 2, "Insurant", insurant_DL, insurant_DN, insurant_SC, insurant_GL); }
                if (dim_drug) { DBContext.Service().InsertDimQual(connection, transaction, CDW_DRUG, CDW_DRUG_GL, TDW_DRUG, LDW_DRUG, id, 3, "Drug", drug_DL, drug_DN, drug_SC, drug_GL); }
                if (dim_medservice) { DBContext.Service().InsertDimQual(connection, transaction, CDW_MEDSERVICE, CDW_MEDSERVICE_GL, TDW_MEDSERVICE, LDW_MEDSERVICE, id, 4, "MedService", meds_DL, meds_DN, meds_SC, meds_GL); }
                if (dim_hospital) { DBContext.Service().InsertDimQual(connection, transaction, CDW_HOSPITAL, CDW_HOSPITAL_GL, TDW_HOSPITAL, LDW_HOSPITAL, id, 5, "Hospital", hospital_DL, hospital_DN, hospital_SC, hospital_GL); }
                if (dim_time) { DBContext.Service().InsertDimQual(connection, transaction, CDW_TIME, CDW_TIME_GL, TDW_TIME, LDW_TIME, id, 6, "Time", time_DL, time_DN, time_SC, time_GL); }

                // BMSR ->
                list.Clear();
                Console.WriteLine("Listboxes: - BMSR: " + LDW_BMSR.SelectedIndex + " Measure: " + LDW_MEASURES.SelectedIndex + " Filter: " + LDW_FILTER.SelectedIndex);
                if (bmsr_variable.Checked)
                {
                    list.AddFirst(new Insert_item("NASS_BMSR_FLT_SID",DBContext.Service().GetLatestID("NASS_BMSR_FLT_SID","AGS_NASS_BMSR_FILTER")+1));
                    list.AddLast(new Insert_item("ASS_SID_NASS", id));
                    Console.WriteLine("variable: pk"+ (DBContext.Service().GetLatestID("NASS_BMSR_FLT_SID", "AGS_NASS_BMSR_FILTER") + 1)+" pred_sid " + (Int32.Parse((ComboBoxCube.SelectedValue.ToString())) * -1));
                    list.AddLast(new Insert_item("BMSR_PRED_SID", (Int32.Parse((ComboBoxCube.SelectedValue.ToString())) * -1)));
                    DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_BMSR_FILTER", list);
                } else if (LDW_BMSR.SelectedIndex != -1)
                { 
                    int nass_bmsr_flt_sid = DBContext.Service().GetLatestID("NASS_BMSR_FLT_SID", "AGS_NASS_BMSR_FILTER") + 1;
                    foreach (DataRowView dr in LDW_BMSR.SelectedItems)
                    {
                        list.AddFirst(new Insert_item("NASS_BMSR_FLT_SID",nass_bmsr_flt_sid));
                        list.AddLast(new Insert_item("ASS_SID_NASS", id));
                        list.AddLast(new Insert_item("BMSR_PRED_SID", Int32.Parse(dr.Row["bmsr_pred_sid"].ToString())));
                        Console.WriteLine("bmsr: " + dr.Row["bmsr_pred_sid"].ToString());
                        DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_BMSR_FILTER", list);
                        list.Clear();
                        nass_bmsr_flt_sid++;
                    }
                } else
                {
                    DataTable dt = DBContext.Service().GetData(
                        "Select bmsr_pred_sid "+
                        "from dw_bmsr_predicate where cube_sid = " + Int32.Parse(ComboBoxCube.SelectedValue.ToString()) + 
                        " and bmsr_pred_name like 'true%'");
                    DataTable dt2 = dt.Copy();
                    DataRow[] dr = dt2.Select();
                    int bmsr_pred_sid = Int32.Parse(dr[0].ItemArray[0].ToString());

                    list.AddFirst(new Insert_item("NASS_BMSR_FLT_SID", DBContext.Service().GetLatestID("NASS_BMSR_FLT_SID", "AGS_NASS_BMSR_FILTER") + 1));
                    list.AddLast(new Insert_item("ASS_SID_NASS", id));
                    list.AddLast(new Insert_item("BMSR_PRED_SID", bmsr_pred_sid));
                    DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_BMSR_FILTER", list);
                }

                // Measures ->
                list.Clear();

                if (LDW_MEASURES.SelectedIndex != -1)
                {
                    int nass_amsr_sid = DBContext.Service().GetLatestID("NASS_AMSR_SID", "AGS_NASS_AGGREGATE_MEASSURE") + 1;
                    foreach (DataRowView dr in LDW_MEASURES.SelectedItems)
                    {
                        list.AddFirst(new Insert_item("NASS_AMSR_SID", nass_amsr_sid));
                        list.AddLast(new Insert_item("ASS_SID_NASS", id));
                        list.AddLast(new Insert_item("DAMSR_SID", Int32.Parse(dr.Row["damsr_sid"].ToString())));
                        Console.WriteLine("bmsr: " + dr.Row["damsr_sid"].ToString());
                        DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_AGGREGATE_MEASSURE", list);
                        list.Clear();
                        nass_amsr_sid++;
                    }
                }
                // Filter -> 
                list.Clear();

                if (filter_variable.Checked)
                {
                    DataTable dt = DBContext.Service().GetData(
                        "Select amsr_pred_sid " +
                        "from dw_amsr_predicate where cube_sid = " + Int32.Parse(ComboBoxCube.SelectedValue.ToString()) +
                        " and amsr_pred_name like '%Var%'");
                    DataTable dt2 = dt.Copy();
                    DataRow[] dr = dt2.Select();
                    int amsr_pred_sid = Int32.Parse(dr[0].ItemArray[0].ToString());

                    list.AddFirst(new Insert_item("NASS_AMSR_FLT_SID", DBContext.Service().GetLatestID("NASS_AMSR_FLT_SID", "AGS_NASS_AMSR_FILTER") + 1));
                    list.AddLast(new Insert_item("ASS_SID_NASS", id));
                    Console.WriteLine("variable: pk" +amsr_pred_sid);
                    list.AddLast(new Insert_item("AMSR_PRED_SID", amsr_pred_sid));
                    DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_AMSR_FILTER", list);
                }
                else if (LDW_FILTER.SelectedIndex != -1)
                {
                    int nass_amsr_flt_sid = DBContext.Service().GetLatestID("NASS_AMSR_FLT_SID", "AGS_NASS_AMSR_FILTER") + 1;
                    foreach (DataRowView dr in LDW_FILTER.SelectedItems)
                    {
                        list.AddFirst(new Insert_item("NASS_AMSR_FLT_SID", nass_amsr_flt_sid));
                        list.AddLast(new Insert_item("ASS_SID_NASS", id));
                        list.AddLast(new Insert_item("AMSR_PRED_SID", Int32.Parse(dr.Row["amsr_pred_sid"].ToString())));
                        Console.WriteLine("amsr filter: " + dr.Row["amsr_pred_sid"].ToString());
                        DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_AMSR_FILTER", list);
                        list.Clear();
                        nass_amsr_flt_sid++;

                    }
                }
                else
                {
                    DataTable dt = DBContext.Service().GetData(
                        "Select amsr_pred_sid " +
                        "from dw_amsr_predicate where cube_sid = " + Int32.Parse(ComboBoxCube.SelectedValue.ToString()) +
                        " and amsr_pred_name like '%true%'");
                    DataTable dt2 = dt.Copy();
                    DataRow[] dr = dt2.Select();
                    int amsr_pred_sid = Int32.Parse(dr[0].ItemArray[0].ToString());

                    list.AddFirst(new Insert_item("NASS_AMSR_FLT_SID", DBContext.Service().GetLatestID("NASS_BMSR_FLT_SID", "AGS_NASS_BMSR_FILTER") + 1));
                    list.AddLast(new Insert_item("ASS_SID_NASS", id));
                    list.AddLast(new Insert_item("AMSR_PRED_SID", amsr_pred_sid));
                    DBContext.Service().InsertWithoutPK(connection, transaction, "AGS_NASS_AMSR_FILTER", list);
                }

                transaction.Commit();
                DBContext.Service().TransactionComplete();
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
                " was encountered while inserting the data."+e.Message);
                Console.WriteLine("Nothing was written to database.");
            }
        }

        // replaces old schema with new one
        public void overrideDB(int ass_sid)
        {
            this.deleteSchema(ass_sid);
            this.insert(ass_sid);
        }

        // deletes schema from cube
        public void deleteSchema(int ass_sid)
        {
            DBContext.Service().Delete("AGS_ANALYSIS_SITUATION_SCHEMA", "ASS_SID", ass_sid.ToString());
            Console.WriteLine("Schema deleted " + ass_sid);
        }

        // START-------------------- disable GL if DL is variable -----------------------------
        public void time_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (time_DL.CheckState == CheckState.Checked)
            {
                CDW_TIME_GL.SelectedIndex = -1;
                CDW_TIME_GL.Enabled = false;
                LDW_TIME.DataSource = null;
            }
            else
            {
                CDW_TIME_GL.Enabled = true;
                CDW_TIME_SelectedIndexChanged(CDW_TIME, new EventArgs());
            }

            Console.WriteLine("time_DL_variable set to: " + time_DL.Checked);
            time_DL_var = time_DL.Checked;
        }
        public void insurant_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (insurant_DL.CheckState == CheckState.Checked)
            {
                CDW_INSURANT_GL.SelectedIndex = -1;
                CDW_INSURANT_GL.Enabled = false;
                LDW_INSURANT.DataSource = null;
            }
            else
            {
                CDW_INSURANT_GL.Enabled = true;
                CDW_INSURANT_SelectedIndexChanged(CDW_INSURANT, new EventArgs());
            }

            Console.WriteLine("insurant_DL_variable set to: " + insurant_DL.Checked);
            insurant_DL_var = insurant_DL.Checked;
        }
        public void meds_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (meds_DL.CheckState == CheckState.Checked)
            {
                CDW_MEDSERVICE_GL.SelectedIndex = -1;
                CDW_MEDSERVICE_GL.Enabled = false;
                LDW_MEDSERVICE.DataSource = null;
            }
            else
            {
                CDW_MEDSERVICE_GL.Enabled = true;
                CDW_MEDSERVICE_SelectedIndexChanged(CDW_MEDSERVICE, new EventArgs());
            }

            Console.WriteLine("meds_DL_variable set to: " + meds_DL.Checked);
            medService_DL_var = meds_DL.Checked;
        }
        public void hospital_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (hospital_DL.CheckState == CheckState.Checked)
            {
                CDW_HOSPITAL_GL.SelectedIndex = -1;
                CDW_HOSPITAL_GL.Enabled = false;
                LDW_HOSPITAL.DataSource = null;
            }
            else
            {
                CDW_HOSPITAL_GL.Enabled = true;
                CDW_HOSPITAL_SelectedIndexChanged(CDW_HOSPITAL, new EventArgs());
            }

            Console.WriteLine("hospital_DL_variable set to: " + hospital_DL.Checked);
            hospital_DL_var = hospital_DL.Checked;
        }
        public void doctor_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (doctor_DL.CheckState == CheckState.Checked)
            {
                CDW_DOCTOR_GL.SelectedIndex = -1;
                CDW_DOCTOR_GL.Enabled = false;
                LDW_DOCTOR.DataSource = null;
            }
            else
            {
                CDW_DOCTOR_GL.Enabled = true;
                CDW_DOCTOR_SelectedIndexChanged(CDW_DOCTOR, new EventArgs());
            }

            Console.WriteLine("doctor_DL_variable set to: " + doctor_DL.Checked);
            doctor_DL_var = doctor_DL.Checked;
        }
        public void drug_DL_CheckedChanged(object sender, EventArgs e)
        {
            if (drug_DL.CheckState == CheckState.Checked)
            {
                CDW_DRUG_GL.SelectedIndex = -1;
                CDW_DRUG_GL.Enabled = false;
                LDW_DRUG.DataSource = null;
            }
            else
            {
                CDW_DRUG_GL.Enabled = true;
                CDW_DRUG_SelectedIndexChanged(CDW_DRUG, new EventArgs());
            }

            Console.WriteLine("drug_DL_variable set to: " + drug_DL.Checked);
            drug_DL_var = drug_DL.Checked;
        }
        // END---------------------- disable GL if DL is variable------------------------------

        public int getAssSid()
        {
            return this.loaded_ass_sid;
        }

        public void LDW_MEASURES_Click(object sender, EventArgs e)
        {
            Console.WriteLine("measure: " +LDW_MEASURES.SelectedIndex);
        }
        public void bmsr_variable_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("variable 1 " + (Int32.Parse((ComboBoxCube.SelectedValue.ToString())) * -1));
            //Console.WriteLine("variable 2 " + ((Int32.Parse(ComboBoxCube.SelectedIndex.ToString())) * -1));
            Console.WriteLine("bmsr_variable set to: " + bmsr_variable.Checked);
            bmsr_var = bmsr_variable.Checked;

        }

        private void filter_variable_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("filter_variable set to: " + filter_variable.Checked);
            filter_var = filter_variable.Checked;
        }

        private void time_DN_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("time_DN_variable set to: " + time_DN.Checked);
            time_DN_var = time_DN.Checked;
        }

        private void time_SC_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("time_SC_variable set to: " + time_SC.Checked);
            time_SC_var = time_SC.Checked;
        }

        private void time_GL_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("time_GL_variable set to: " + time_GL.Checked);
            time_GL_var = time_GL.Checked;
        }

        private void insurant_DN_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("insurant_DN_variable set to: " + insurant_DN.Checked);
            insurant_DN_var = insurant_DN.Checked;
        }

        private void insurant_SC_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("insurant_SC_variable set to: " + insurant_SC.Checked);
            insurant_SC_var = insurant_SC.Checked;
        }

        private void insurant_GL_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("insurant_GL_variable set to: " + insurant_GL.Checked);
            insurant_GL_var = insurant_GL.Checked;
        }

        private void meds_DN_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("meds_DN_variable set to: " + meds_DN.Checked);
            medService_DN_var = meds_DN.Checked;
        }

        private void meds_SC_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("meds_SC_variable set to: " + meds_SC.Checked);
            medService_SC_var = meds_SC.Checked;
        }

        private void meds_GL_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("meds_GL_variable set to: " + meds_GL.Checked);
            medService_GL_var = meds_GL.Checked;
        }

        private void drug_DN_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("drug_DN_variable set to: " + drug_DN.Checked);
            drug_DN_var = drug_DN.Checked;
        }

        private void drug_SC_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("drug_SC_variable set to: " + drug_SC.Checked);
            drug_SC_var = drug_SC.Checked;
        }

        private void drug_GL_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("drug_GL_variable set to: " + drug_GL.Checked);
            drug_GL_var = drug_GL.Checked;
        }

        private void doctor_DN_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("doctor_DN_variable set to: " + doctor_DN.Checked);
            doctor_DN_var = doctor_DN.Checked;
        }

        private void doctor_SC_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("doctor_SC_variable set to: " + doctor_SC.Checked);
            doctor_SC_var = doctor_SC.Checked;
        }

        private void doctor_GL_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("doctor_GL_variable set to: " + doctor_GL.Checked);
            doctor_GL_var = doctor_GL.Checked;
        }

        private void hospital_DN_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("hospital_DN_variable set to: " + hospital_DN.Checked);
            hospital_DN_var = hospital_DN.Checked;
        }

        private void hospital_SC_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("hospital_SC_variable set to: " + hospital_SC.Checked);
            hospital_SC_var = hospital_SC.Checked;
        }

        private void hospital_GL_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("hospital_GL_variable set to: " + hospital_GL.Checked);
            hospital_GL_var = hospital_GL.Checked;
        }

        public void DisableVars()
        {
            bmsr_variable.Enabled = false;
            filter_variable.Enabled = false;
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
        

        public void DisableNewOperators()
        {
            button_select_navigation_operator.Enabled = false;
        }

        public void CheckOperators()
        {
            operatorsChooser.Visible = true;
            operate.Visible = true;
            label4.Visible = true;
            drillDownToLevelButton.Visible = true;
            rollUpToLevelButton.Visible = true;
            moveToNodeButton.Visible = true;
            refocusSliceCondButton.Visible = true;
            refocusBMsrCondButton.Visible = true;
            refocusMeasureButton.Visible = true;
            refocusAMsrFilterButton.Visible = true;
            drillAcrossToCubeButton.Visible = true;

        }

        private void operate_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = (operatorsChooser.SelectedItem as DataRowView);

                int assSid = (int)selectedRow["ass_sid_target"];
                Console.WriteLine(assSid);

                UserInput initInput = new UserInput(loaded_ags_sid, false, assSid);
                initInput.startOperating.Visible = true;
                initInput.disable_fields();
                initInput.DisableVars();
                initInput.DisableNewOperators();
                initInput.ShowDialog();
                
                /*
                if (operatorsChooser.SelectedValue != null)
                {
                    if (granLvlUsed)
                    {


                        ;
                        /*
                        Console.WriteLine("Selected Item: " + operatorsChooser.SelectedItem.ToString());
                        Console.WriteLine("Selected Text: " + operatorsChooser.SelectedText.ToString());
                        Console.WriteLine("Selected Value: " + operatorsChooser.SelectedValue.ToString());
                        Console.WriteLine("DisplayMember: " + operatorsChooser.DisplayMember.ToString());
                        Console.WriteLine("ValueMember: " + operatorsChooser.ValueMember.ToString());
                        Console.WriteLine("comboBox: " + operatorsChooser.ToString());
                        Console.WriteLine("displayRectangle: " + operatorsChooser.DisplayRectangle.ToString());
                        Console.WriteLine("Text: " + operatorsChooser.Text.ToString());
                        *
                        var selectedRow = (operatorsChooser.SelectedItem as DataRowView);

                        int dimSid = (int)selectedRow["dim_sid"];
                        int granlvl = (int)selectedRow["lvl_sid_granlvl"];

                        int[] granLvls = new int[4];
                        int[] diceLvls = new int[4];
                        int countUsed = 1;


                        if (CDW_TIME_GL.SelectedValue != null)
                        {
                            Console.WriteLine("time there");
                            if (dimSid == 6)
                            {
                                granLvls[0] = granlvl;
                            }
                            else
                            {
                                granLvls[countUsed] = (int)CDW_TIME_GL.SelectedValue;
                                countUsed++;
                            }

                        }

                        if (CDW_DOCTOR_GL.SelectedValue != null)
                        {
                            Console.WriteLine("DOC there");
                            if (dimSid == 1)
                            {
                                granLvls[0] = granlvl;
                            }
                            else
                            {
                                granLvls[countUsed] = (int)CDW_DOCTOR_GL.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_DRUG_GL.SelectedValue != null)
                        {
                            Console.WriteLine("Drug there");
                            if (dimSid == 3)
                            {
                                granLvls[0] = granlvl;
                            }
                            else
                            {
                                granLvls[countUsed] = (int)CDW_DRUG_GL.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_HOSPITAL_GL.SelectedValue != null)
                        {
                            Console.WriteLine("Hospital there");
                            if (dimSid == 5)
                            {
                                granLvls[0] = granlvl;
                            }
                            else
                            {
                                granLvls[countUsed] = (int)CDW_HOSPITAL_GL.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_INSURANT_GL.SelectedValue != null)
                        {
                            Console.WriteLine("Insurant there");
                            if (dimSid == 2)
                            {
                                granLvls[0] = granlvl;
                            }
                            else
                            {
                                granLvls[countUsed] = (int)CDW_INSURANT_GL.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_MEDSERVICE_GL.SelectedValue != null)
                        {
                            Console.WriteLine("Medservice there");
                            if (dimSid == 4)
                            {
                                granLvls[0] = granlvl;
                            }
                            else
                            {
                                granLvls[countUsed] = (int)CDW_MEDSERVICE_GL.SelectedValue;
                                countUsed++;
                            }

                        }


                        String query = "select distinct a0.ass_sid_nass " +
                                        "FROM ags_nass_dim_qual a0, ags_nass_dim_qual a1, ags_nass_dim_qual a2, ags_nass_dim_qual a3, ags_nass_dim_qual a4, ags_non_cmp_ass an1, ags_non_cmp_ass an2 " +
                                        "WHERE an1.ass_sid_nass = " + this.loaded_ass_sid + " " +
                                        "AND an2.ass_sid_nass = a0.ass_sid_nass " +
                                        "AND an1.cube_sid = an2.cube_sid " +
                                        "AND a1.lvl_sid_granlvl = " + granLvls[0] + " " +
                                        "AND a2.lvl_sid_granlvl = " + granLvls[1] + " " +
                                        "AND a3.lvl_sid_granlvl = " + granLvls[2] + " " +
                                        "AND a4.lvl_sid_granlvl = " + granLvls[3] + " " +
                                        "AND a0.ass_sid_nass = a1.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a2.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a3.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a4.ass_sid_nass " +
                                        "ORDER BY a0.ass_sid_nass ";

                        Console.WriteLine(query);

                        DataTable dt = DBContext.Service().GetData(query);
                        DataTable dt2 = dt.Copy();
                        DataRow[] dr = dt2.Select();
                        int goalSid = Int32.Parse(dr[0].ItemArray[0].ToString());
                        Console.WriteLine(goalSid);

                        UserInput initInput = new UserInput(this.loaded_ags_sid, false, goalSid);
                        initInput.startOperating.Visible = true;
                        initInput.disable_fields();
                        initInput.DisableVars();
                        initInput.DisableNewOperators();

                        initInput.ShowDialog();
                        this.Close();
                        /*
                         * "SELECT ddtl.* " +
                           "FROM ags_navss_drill_down_to_level ddtl, ags_nass_dim_qual ndq " +
                           "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                           "AND ndq.dim_sid = ddtl.dim_sid " +
                           "AND ddtl.lvl_sid_granlvl < ndq.lvl_sid_granlvl"
                         *
                    }
                    else if (diceLvlUsed)
                    {
                        var selectedRow = (operatorsChooser.SelectedItem as DataRowView);
                        Console.WriteLine("selectedRow: " + selectedRow.ToString());

                        int dimSid = (int)selectedRow["dim_sid"];
                        int diceLvl = (int)selectedRow["lvl_sid_dicelvl"];
                        Console.WriteLine(dimSid);
                        Console.WriteLine(this.loaded_ags_sid);
                        Console.WriteLine(this.loaded_ass_sid);

                        int[] diceLvls = new int[4];
                        int countUsed = 1;


                        if (CDW_TIME.SelectedValue != null)
                        {
                            Console.WriteLine("time there");
                            Console.WriteLine(CDW_TIME.SelectedValue);
                            Console.WriteLine(CDW_TIME.SelectedItem);
                            if (dimSid == 6)
                            {
                                diceLvls[0] = diceLvl;
                            }
                            else
                            {
                                diceLvls[countUsed] = (int)CDW_TIME.SelectedValue;
                                countUsed++;
                            }

                        }

                        if (CDW_DOCTOR.SelectedValue != null)
                        {
                            Console.WriteLine("DOC there");
                            if (dimSid == 1)
                            {
                                diceLvls[0] = diceLvl;
                            }
                            else
                            {
                                diceLvls[countUsed] = (int)CDW_DOCTOR.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_DRUG.SelectedValue != null)
                        {
                            Console.WriteLine("Drug there");
                            if (dimSid == 3)
                            {
                                diceLvls[0] = diceLvl;
                            }
                            else
                            {
                                diceLvls[countUsed] = (int)CDW_DRUG.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_HOSPITAL.SelectedValue != null)
                        {
                            Console.WriteLine("Hospital there");
                            if (dimSid == 5)
                            {
                                diceLvls[0] = diceLvl;
                            }
                            else
                            {
                                diceLvls[countUsed] = (int)CDW_HOSPITAL.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_INSURANT.SelectedValue != null)
                        {
                            Console.WriteLine("Insurant there");
                            if (dimSid == 2)
                            {
                                diceLvls[0] = diceLvl;
                            }
                            else
                            {
                                diceLvls[countUsed] = (int)CDW_INSURANT.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (CDW_MEDSERVICE.SelectedValue != null)
                        {
                            Console.WriteLine("Medservice there");
                            if (dimSid == 4)
                            {
                                diceLvls[0] = diceLvl;
                            }
                            else
                            {
                                diceLvls[countUsed] = (int)CDW_MEDSERVICE.SelectedValue;
                                countUsed++;
                            }

                        }


                        String query = "select distinct a0.ass_sid_nass " +
                                        "FROM ags_nass_dim_qual a0, ags_nass_dim_qual a1, ags_nass_dim_qual a2, ags_nass_dim_qual a3, ags_nass_dim_qual a4, ags_non_cmp_ass an1, ags_non_cmp_ass an2 " +
                                        "WHERE an1.ass_sid_nass = " + this.loaded_ass_sid + " " +
                                        "AND an2.ass_sid_nass = a0.ass_sid_nass " +
                                        "AND an1.cube_sid = an2.cube_sid " +
                                        "AND a1.lvl_sid_dicelvl = " + diceLvls[0] + " " +
                                        "AND a2.lvl_sid_dicelvl = " + diceLvls[1] + " " +
                                        "AND a3.lvl_sid_dicelvl = " + diceLvls[2] + " " +
                                        "AND a4.lvl_sid_dicelvl = " + diceLvls[3] + " " +
                                        "AND a0.ass_sid_nass = a1.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a2.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a3.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a4.ass_sid_nass" +
                                        "ORDER BY a0.ass_sid_nass ";

                        Console.WriteLine(query);

                        DataTable dt = DBContext.Service().GetData(query);
                        DataTable dt2 = dt.Copy();
                        DataRow[] dr = dt2.Select();
                        int goalSid = Int32.Parse(dr[0].ItemArray[0].ToString());
                        Console.WriteLine(goalSid);

                        UserInput initInput = new UserInput(this.loaded_ags_sid, false, goalSid);
                        initInput.startOperating.Visible = true;
                        initInput.disable_fields();
                        initInput.DisableVars();
                        initInput.DisableNewOperators();

                        initInput.ShowDialog();
                        this.Close();
                    }
                    else if (refocusSliceCondUsed)
                    {
                        var selectedRow = (operatorsChooser.SelectedItem as DataRowView);
                        Console.WriteLine("selectedRow: " + selectedRow.ToString());

                        int dimSid = (int)selectedRow["dim_sid"];
                        Console.WriteLine(dimSid);
                        Console.WriteLine(this.loaded_ags_sid);
                        Console.WriteLine(this.loaded_ass_sid);

                        int[] lvl_sid = new int[4];
                        int countUsed = 1;

                        DataTable dt0 = DBContext.Service().GetData(
                        "SELECT lvl_sid " +
                        "FROM DW_DIM_PREDICATE " +
                        "WHERE dim_pred_sid = " + dimSid
                         ).Copy();
                        DataRow[] dr0 = dt0.Select();
                        int lvlSid = Int32.Parse(dr0[0].ItemArray[0].ToString());
                        Console.WriteLine(lvlSid);


                        if (LDW_TIME.SelectedValue != null)
                        {
                            Console.WriteLine("time there");
                            Console.WriteLine(LDW_TIME.SelectedValue);
                            Console.WriteLine(LDW_TIME.SelectedItem);
                            if (dimSid == 6)
                            {
                                lvl_sid[0] = lvlSid;

                            }
                            else
                            {
                                lvl_sid[countUsed] = (int)LDW_TIME.SelectedValue;
                                countUsed++;
                            }

                        }

                        if (LDW_DOCTOR.SelectedValue != null)
                        {
                            Console.WriteLine("DOC there");
                            if (dimSid == 1)
                            {
                                lvl_sid[0] = lvlSid;
                            }
                            else
                            {
                                lvl_sid[countUsed] = (int)LDW_DOCTOR.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (LDW_DRUG.SelectedValue != null)
                        {
                            Console.WriteLine("Drug there");
                            if (dimSid == 3)
                            {
                                lvl_sid[0] = lvlSid;
                            }
                            else
                            {
                                lvl_sid[countUsed] = (int)LDW_DRUG.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (LDW_HOSPITAL.SelectedValue != null)
                        {
                            Console.WriteLine("Hospital there");
                            if (dimSid == 5)
                            {
                                lvl_sid[0] = lvlSid;
                            }
                            else
                            {
                                lvl_sid[countUsed] = (int)LDW_HOSPITAL.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (LDW_INSURANT.SelectedValue != null)
                        {
                            Console.WriteLine("Insurant there");
                            if (dimSid == 2)
                            {
                                lvl_sid[0] = lvlSid;
                            }
                            else
                            {
                                lvl_sid[countUsed] = (int)LDW_INSURANT.SelectedValue;
                                countUsed++;
                            }

                        }
                        if (LDW_MEDSERVICE.SelectedValue != null)
                        {
                            Console.WriteLine("Medservice there");
                            if (dimSid == 4)
                            {
                                lvl_sid[0] = lvlSid;
                            }
                            else
                            {
                                lvl_sid[countUsed] = (int)LDW_MEDSERVICE.SelectedValue;
                                countUsed++;
                            }

                        }


                        String query = "select distinct a0.ass_sid_nass " +
                                        "FROM ags_nass_dim_qual a0, ags_nass_dim_qual a1, ags_nass_dim_qual a2, ags_nass_dim_qual a3, ags_nass_dim_qual a4, ags_non_cmp_ass an1, ags_non_cmp_ass an2 " +
                                        "WHERE an1.ass_sid_nass = " + this.loaded_ass_sid + " " +
                                        "AND an2.ass_sid_nass = a0.ass_sid_nass " +
                                        "AND an1.cube_sid = an2.cube_sid " +
                                        "AND a1.lvl_sid_dicelvl = " + lvl_sid[0] + " " +
                                        "AND a2.lvl_sid_dicelvl = " + lvl_sid[1] + " " +
                                        "AND a3.lvl_sid_dicelvl = " + lvl_sid[2] + " " +
                                        "AND a4.lvl_sid_dicelvl = " + lvl_sid[3] + " " +
                                        "AND a0.ass_sid_nass = a1.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a2.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a3.ass_sid_nass " +
                                        "AND a0.ass_sid_nass = a4.ass_sid_nass " +
                                        "ORDER BY a0.ass_sid_nass ";

                        Console.WriteLine(query);

                        DataTable dt = DBContext.Service().GetData(query);
                        DataTable dt2 = dt.Copy();
                        DataRow[] dr = dt2.Select();

                        int goalSid = Int32.Parse(dr[0].ItemArray[0].ToString());
                        Console.WriteLine(goalSid);

                        UserInput initInput = new UserInput(this.loaded_ags_sid, false, goalSid);
                        initInput.startOperating.Visible = true;
                        initInput.disable_fields();
                        initInput.DisableVars();
                        initInput.DisableNewOperators();

                        initInput.ShowDialog();
                        this.Close();
                    }
                }
                else if(bmsrFilterUsed)
                {

                }
                else if (measureUsed)
                {

                }
                else if(amsrUsed)
                {

                }
                else if(drillAcross)
                {

                }
   */
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }  
        }

        private void UserInput_Load(object sender, EventArgs e)
        {

        }

        //Used for testing initalisation
        public void startOperating_Click(object sender, EventArgs e)
        {
            CheckOperators();
            /*
            Console.WriteLine("The Test Commences!-> " + ComboBoxCube.SelectedValue.ToString());
            try
            {
               
                
                selectTable = new SelectTable();
                selectTable.cube_sid = ComboBoxCube.SelectedValue.ToString();
                selectTable.tableNameCB = "test";
                selectTable.setLabe2();
                selectTable.ShowDialog(this);
                
                UserInput initUserInput = new UserInput(loaded_ags_sid, false, loaded_ass_sid);

                initUserInput.ShowDialog();
            }
            catch (Exception exx)
            {
                Console.WriteLine(exx.Message);
            }*/
            }

            private void SQLQuery_Click(object sender, EventArgs e)
        {

            try
            {
                StringBuilder q = new StringBuilder();

                q.Append("SELECT DISTINCT * FROM ags_non_cmp_ass anca, ags_nass_dim_qual andq, dw_cube dc, dw_level dl, dw_dim_predicate ddp");

                int bmsrID = 0;
                int measuresID = 0;
                int filterID = 0;

                if (LDW_BMSR.SelectedValue != null)
                {
                    q.Append(", dw_bmsr_predicate dbp");
                }

                if(LDW_MEASURES.SelectedValue != null)
                {
                    q.Append(", dw_derived_aggregate_measure ddam");
                }

                if(LDW_FILTER.SelectedValue != null)
                {
                    q.Append(", dw_amsr_predicate dap");
                }

                q.Append(" WHERE andq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                    "AND  anca.ass_sid_nass = " + this.loaded_ass_sid + " " + "AND anca.cube_sid = dc.cube_sid " +
                    "AND dl.dim_sid = andq.dim_sid " +
                    "AND dl.lvl_sid = andq.lvl_sid_dicelvl " +
                    "AND ddp.dim_pred_sid = andq.dim_sid ");

                if (LDW_BMSR.SelectedValue != null)
                {
                    bmsrID = (int)LDW_BMSR.SelectedValue;
                    q.Append("AND dbp.bmsr_pred_sid = " + bmsrID + " ");
                }

                if (LDW_MEASURES.SelectedValue != null)
                {
                    measuresID = (int)LDW_MEASURES.SelectedValue;
                    q.Append("AND ddam.damsr_sid = " + measuresID + " ");
                }

                if (LDW_FILTER.SelectedValue != null)
                {
                    filterID = (int)LDW_FILTER.SelectedValue;
                    q.Append("AND dap.amsr_pred_sid = " + filterID + " ");
                } 

                q.Append("ORDER BY andq.nass_dq_sid");
                ShowSQL showSQL = new ShowSQL(q.ToString());
                showSQL.ShowDialog();
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void drillDownToLevelButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'drillDownToLevel' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            granLvlUsed = true;
            diceLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = false;
            measureUsed = false;
            amsrUsed = false;
            drillAcross = false;

            DataTable dtDrillDownToLevel = DBContext.Service().GetData(
                   "SELECT ddtl.* " +
                   "FROM ags_navss_drill_down_to_level ddtl, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = ddtl.dim_sid " +
                   "AND ddtl.lvl_sid_granlvl < ndq.lvl_sid_granlvl"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtDrillDownToLevel != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtDrillDownToLevel;
                operatorsChooser.DisplayMember = "lvl_sid_granlvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }

        private void rollUpToLevelButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'rollUpToLevel' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            granLvlUsed = true;
            diceLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = false;
            measureUsed = false;
            amsrUsed = false;
            drillAcross = false;

            DataTable rollUpToLevel = DBContext.Service().GetData(
                   "SELECT rutl.* " +
                   "FROM ags_navss_roll_up_to_level rutl, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = rutl.dim_sid " +
                   "AND rutl.lvl_sid_granlvl > ndq.lvl_sid_granlvl"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (rollUpToLevel != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = rollUpToLevel;
                operatorsChooser.DisplayMember = "lvl_sid_granlvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }

        private void moveToNodeButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'moveToNode' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            diceLvlUsed = true;
            granLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = false;
            measureUsed = false;
            amsrUsed = false;
            drillAcross = false;

            DataTable dtmoveToNode = DBContext.Service().GetData(
                   "SELECT mtn.* " +
                   "FROM ags_navss_move_to_node mtn, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = mtn.dim_sid " +
                   "AND (NOT mtn.lvl_sid_dicelvl = ndq.lvl_sid_dicelvl)"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtmoveToNode != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtmoveToNode;
                operatorsChooser.DisplayMember = "lvl_sid_dicelvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }
        //===========================
        private void refocusSliceCondButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'refocusSliceCond' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            refocusSliceCondUsed = true;
            granLvlUsed = false;
            diceLvlUsed = false;
            bmsrFilterUsed = false;
            measureUsed = false;
            amsrUsed = false;
            drillAcross = false;

            DataTable dtrefocusSliceCond = DBContext.Service().GetData(
                   "SELECT rsc.* " +
                   "FROM ags_navss_refocus_slice_cond rsc, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = rsc.dim_sid "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtrefocusSliceCond != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtrefocusSliceCond;
                operatorsChooser.DisplayMember = "dim_sid";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }
        //=========================
        private void refocusBMsrCondButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'refocusBMsrCond' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            granLvlUsed = false;
            diceLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = true;
            measureUsed = false;
            amsrUsed = false;
            drillAcross = false;

            DataTable dtrefocusBMsrCond = DBContext.Service().GetData(
                   "SELECT rbc.* " +
                   "FROM ags_navss_drill_down_to_level ddtl, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = ddtl.dim_sid " +
                   "AND ddtl.lvl_sid_granlvl < ndq.lvl_sid_granlvl"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtrefocusBMsrCond != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtrefocusBMsrCond;
                operatorsChooser.DisplayMember = "lvl_sid_granlvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }

        private void refocusMeasureButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'refocusMeasure' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            granLvlUsed = false;
            diceLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = false;
            measureUsed = true;
            amsrUsed = false;
            drillAcross = false;

            DataTable dtDrillDownToLevel = DBContext.Service().GetData(
                   "SELECT ddtl.* " +
                   "FROM ags_navss_drill_down_to_level ddtl, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = ddtl.dim_sid " +
                   "AND ddtl.lvl_sid_granlvl < ndq.lvl_sid_granlvl"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtDrillDownToLevel != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtDrillDownToLevel;
                operatorsChooser.DisplayMember = "lvl_sid_granlvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }

        private void refocusAMsrFilterButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'refocusAMsrFilter' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            granLvlUsed = false;
            diceLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = false;
            measureUsed = false;
            amsrUsed = true;
            drillAcross = false;

            DataTable dtDrillDownToLevel = DBContext.Service().GetData(
                   "SELECT ddtl.* " +
                   "FROM ags_navss_drill_down_to_level ddtl, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = ddtl.dim_sid " +
                   "AND ddtl.lvl_sid_granlvl < ndq.lvl_sid_granlvl"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtDrillDownToLevel != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtDrillDownToLevel;
                operatorsChooser.DisplayMember = "lvl_sid_granlvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }

        private void drillAcrossToCubeButton_Click(object sender, EventArgs e)
        {
            DataTable dtAns = DBContext.Service().GetData(
                   "SELECT ans.* " +
                   "FROM ags_navstep_schema ans " +
                   "WHERE ans.ass_sid_source = " + this.loaded_ass_sid + " " +
                   "AND ans.navss_opname = 'drillAcrossToCube' "
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtAns != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtAns;
                operatorsChooser.DisplayMember = "ass_sid_target";
            }
            /*
            granLvlUsed = false;
            diceLvlUsed = false;
            refocusSliceCondUsed = false;
            bmsrFilterUsed = false;
            measureUsed = false;
            amsrUsed = false;
            drillAcross = true;

            DataTable dtDrillDownToLevel = DBContext.Service().GetData(
                   "SELECT ddtl.* " +
                   "FROM ags_navss_drill_down_to_level ddtl, ags_nass_dim_qual ndq " +
                   "WHERE ndq.ass_sid_nass = " + this.loaded_ass_sid + " " +
                   "AND ndq.dim_sid = ddtl.dim_sid " +
                   "AND ddtl.lvl_sid_granlvl < ndq.lvl_sid_granlvl"
                ).Copy();

            Console.WriteLine("loaded_ass_sid: " + this.loaded_ass_sid);
            if (dtDrillDownToLevel != null)
            {
                Console.WriteLine("Worked");

                operatorsChooser.DataSource = dtDrillDownToLevel;
                operatorsChooser.DisplayMember = "lvl_sid_granlvl";
                //operatorsChooser.ValueMember = "dim_sid";
            }
            */
        }

        private void LDW_INSURANT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
