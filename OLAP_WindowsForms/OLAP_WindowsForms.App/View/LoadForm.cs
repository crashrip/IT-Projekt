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
            DataGridViewCell cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            Console.WriteLine(cell.Value.ToString());

            if (e.ColumnIndex == 0)
            {

                Console.WriteLine("bearbeiten " + cell.Value);
                int value = Int32.Parse(cell.Value.ToString());
                UserInput userinput = new UserInput(ags_sid, false, value);
                userinput.ShowDialog();
                this.Hide();
                this.Close();
            }
        }

        private void LoadForm_Click(object sender, EventArgs e)
        {
            validateGraphSchema();
            Console.WriteLine("graph updated");
        }
    }
}
