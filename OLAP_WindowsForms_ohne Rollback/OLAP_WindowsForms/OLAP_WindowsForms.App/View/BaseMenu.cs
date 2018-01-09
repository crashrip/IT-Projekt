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
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Console.WriteLine(cell.Value.ToString());

            if (e.ColumnIndex == 0)
            {
                Console.WriteLine("bearbeiten");
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
    }
}
