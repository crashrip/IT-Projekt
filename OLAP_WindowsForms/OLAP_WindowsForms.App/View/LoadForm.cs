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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //Console.WriteLine("enter cell content click");
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                row = e.RowIndex;
                column = e.ColumnIndex;
                Console.WriteLine("row: " + row + " column " + column);
                if (e.ColumnIndex == 0)
                {
                    DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    Console.WriteLine("bearbeiten " + cell.Value);
                    int value = Int32.Parse(cell.Value.ToString());
                    UserInput userinput = new UserInput(ags_sid, false, value);
                    userinput.ShowDialog();
                    this.Hide();
                    this.Close();
                }
            }  
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

        private void delete_selected_schema_Click(object sender, EventArgs e)
        {
            if (column == 1)
            {
                if (MessageBox.Show("Are you sure that you want to delete the Analysis-Schema ?", "Delete Schema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) ==
    DialogResult.Yes)
                {
                    DataGridViewCell cell = dataGridView1.Rows[row].Cells[column];
                    Console.WriteLine("column 1 - delete: " + cell.Value.ToString());
                    DBContext.Service().delete("AGS_ANALYSIS_SITUATION_SCHEMA", "ASS_NAME", "'" + cell.Value.ToString() + "'");
                } 
            }
            if (column == 0)
            {
                if (MessageBox.Show("Are you sure that you want to delete the Analysis-Schema?", "Delete Schema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) ==
    DialogResult.Yes)
                {
                    DataGridViewCell cell = dataGridView1.Rows[row].Cells[column];
                    Console.WriteLine("column 0 - delete: " + cell.Value.ToString());
                    DBContext.Service().delete("AGS_ANALYSIS_SITUATION_SCHEMA", "ASS_SID", cell.Value.ToString());
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                row = e.RowIndex;
                column = e.ColumnIndex;
                Console.WriteLine("row: " + row + " column " + column);
            }
        }

        //Initiate button
        private void button1_Click(object sender, EventArgs e)
        {
            if (column == 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[row].Cells[column];
                Console.WriteLine("column 0: " + cell.Value.ToString());
            }
            if (column == 1)
            {
                DataGridViewCell cell = dataGridView1.Rows[row].Cells[column-1];
                Console.WriteLine("column 1: " + cell.Value.ToString());
                // UserInput uInput = new UserInput();
            }
            if (column == 2)
            {
                DataGridViewCell cell = dataGridView1.Rows[row].Cells[column-2];
                Console.WriteLine("column 2: " + cell.Value.ToString());
            }
        }
    }
}
