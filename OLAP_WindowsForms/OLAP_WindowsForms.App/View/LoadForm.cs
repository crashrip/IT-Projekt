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
    public partial class LoadForm : Form
    {
        private int ags_sid;
        private int row;
        private int column;

        public LoadForm(int ags_sid)
        {
            this.ags_sid = ags_sid;
            InitializeComponent();
            validateGraphSchema();
        }

        private void validateGraphSchema()
        {
            DataTable dt = DBContext.Service().GetData(
                "SELECT ASS_SID, ASS_NAME, ASS_DESCRIPTION FROM AGS_ANALYSIS_SITUATION_SCHEMA "+
                "WHERE AGS_SID = "+ags_sid+" ORDER BY ASS_SID DESC");

            DataTable dt2 = dt.Copy();

            dataGridView1.DataSource = dt2;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadForm_Click(object sender, EventArgs e)
        {
            validateGraphSchema();
            Console.WriteLine("graph updated");
        }

        private void create_new_schema_Click(object sender, EventArgs e)
        {
            UserInput userinput = new UserInput(this.ags_sid, true);
            userinput.ShowDialog();
            this.Hide();
            this.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                row = e.RowIndex;
                column = e.ColumnIndex;
                if (e.RowIndex < (dataGridView1.RowCount - 1)) // Error if ColumIndex 0 and no data is in cell
                {
                    //Console.WriteLine("row: " + row + " column " + column);
                    if (e.ColumnIndex == 0 && e.Button.Equals(MouseButtons.Left)) // load shema with left click
                    {
                        DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        //Console.WriteLine("bearbeiten " + cell.Value);
                        int value = Int32.Parse(cell.Value.ToString());
                        UserInput userinput = new UserInput(ags_sid, false, value);
                        userinput.ShowDialog();
                        this.Hide();

                    }
                    if (column == 0 && e.Button.Equals(MouseButtons.Right)) // delete schema with right click
                    {
                        if (MessageBox.Show("Do you really want to delete this analysis situation?", "Delete Schema",
                           MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            DataGridViewCell cell = dataGridView1.Rows[row].Cells[column];
                            DBContext.Service().Delete("AGS_ANALYSIS_SITUATION_SCHEMA", "ASS_SID", cell.Value.ToString());
                            validateGraphSchema();
                        }
                    }
                }
            }
        }

        //Initiate button
        private void initiateButton_Click(object sender, EventArgs e)
        {
            DataGridViewCell cell = null;
            if (column == 0)
            {
                cell = dataGridView1.Rows[row].Cells[column];
                Console.WriteLine("column 0: " + cell.Value.ToString());
            }
            if (column == 1)
            {
                cell = dataGridView1.Rows[row].Cells[column-1];
                Console.WriteLine("column 1: " + cell.Value.ToString());
                // UserInput uInput = new UserInput();
            }
            if (column == 2)
            {
                cell = dataGridView1.Rows[row].Cells[column-2];
                Console.WriteLine("column 2: " + cell.Value.ToString());
            }
            if(cell != null)
            {
                int assSid = (int) cell.Value;

                UserInput initInput = new UserInput(ags_sid, false, assSid);
                initInput.startOperating.Visible = true;
                initInput.disable_fields();
                initInput.DisableVars();
                initInput.DisableNewOperators();

                initInput.ShowDialog();

            }
        }
    }
}
