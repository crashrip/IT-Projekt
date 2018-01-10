using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OLAP_WindowsForms.App.View;

namespace OLAP_WindowsForms.App
{
    public partial class BaseMenu : Form
    {
        private int row;
        private int column;

        public BaseMenu()
        {
            // show login dialog
            Login login = new Login() { TopMost = true };
            login.ShowDialog(this);

            if (Login.loginSuccessful)
            {
                InitializeComponent();
                validateGraphSchema(); 
            }
            else
            {
                Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateNewAnalysis cna = new CreateNewAnalysis();
            cna.ShowDialog(this);
            //UserInput userinput = new UserInput() { TopMost = true };
            //userinput.ShowDialog(this);
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
                    DataTable dt = DBContext.Service().GetData("Select AGS_SID FROM AGS_ANALYSIS_GRAPH_SCHEMA WHERE AGS_NAME = '" + cell.Value.ToString() + "'");
                    DataTable dt2 = dt.Copy();
                    DataRow[] dr = dt2.Select();
                    String index = dr[0].ItemArray[0].ToString();
                    int ags_sid = Int32.Parse(index);
                    Console.WriteLine("bearbeiten " + cell.Value.ToString() + " AGS_SID: " + ags_sid);
                    LoadForm load = new LoadForm(ags_sid);
                    load.ShowDialog();
                }
            }

            
        }

        private void validateGraphSchema()
        {
            DataTable dt = DBContext.Service().GetData(
                "SELECT AGS_NAME, AGS_DESCRIPTION FROM AGS_ANALYSIS_GRAPH_SCHEMA ORDER BY AGS_SID DESC");

            DataTable dt2 = dt.Copy();

            dataGridView1.DataSource = dt2;
            //dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void reload_graph_schema(object sender, EventArgs e)
        {
            validateGraphSchema();
            Console.WriteLine("graph updated");
        }

        private void delete_selected_schema_Click(object sender, EventArgs e)
        {
            if( column == 0)
            {
                DataGridViewCell cell = dataGridView1.Rows[row].Cells[column];
                DBContext.Service().delete("AGS_ANALYSIS_GRAPH_SCHEMA", "AGS_NAME","'"+cell.Value.ToString()+"'");
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Console.WriteLine("enter cell mouse click");
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                row = e.RowIndex;
                column = e.ColumnIndex;
                Console.WriteLine("row: " + row + " column " + column);
            }

        }
    }
}
