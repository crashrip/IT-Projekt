using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace OLAP_WindowsForms.App.View
{
    public partial class SelectTable : Form
    {
        public SelectTable()
        {
            InitializeComponent();
            ComboItem.SetComboboxContent(comboBox1, "DW_CUBE", "CUBE_SID", "CUBE_NAME");
        }

        //Cancel Button
        private void button2_Click(object sender, EventArgs e)
        {
            // Display a MsgBox asking the user to cancel or abort.
            if (MessageBox.Show("Are you sure you want to close the window?", "Select table",
               MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        /*
            if(!comboBox1.SelectedValue.Equals(null))
            {
                Console.WriteLine("hi");
            }
            */
        }

        //Start Button
        private void button1_Click(object sender, EventArgs e)
        {

            //Yee olden code
            /*
            String cube_sid = comboBox1.SelectedValue.ToString();
            string tableName = null;

            DataTable dt0 = DBContext.Service().GetData(
                "SELECT cube_name " + 
                "FROM dw_cube " +
                "WHERE " + cube_sid + " = cube_sid");

            if(dt0 == null)
            {
                Console.WriteLine("ERROR! dt0 is null!");
                return;
            }

            foreach(DataRow row in dt0.Rows)
            {
                tableName = row[0].ToString();
            }

            if(tableName.Equals(null) || tableName.Equals(""))
            {
                Console.WriteLine("ERROR! No tableName!");
                return;
            }
            Console.WriteLine(tableName);

            DataTable dt = DBContext.Service().GetData(
                "SELECT * " +
                "FROM " + tableName);
                //"ORDER " + "BY " + "LVL_SID " + "ASC");
            
            DataTable dt2 = dt.Copy();
            
            dataGridView1.Rows.Clear();
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Column_1";
            dataGridView1.Columns[1].Name = "Column_2";
            dataGridView1.Columns[2].Name = "Column_3";
            dataGridView1.Columns[3].Name = "Column_4";
            dataGridView1.Columns[4].Name = "Column_5";
            dataGridView1.Columns[5].Name = "Column_6";

            foreach (DataRow row in dt2.Rows)
            {

                string[] rowArray = new String[] {
                row[0].ToString(),
                row[1].ToString(),
                row[2].ToString(),
                row[3].ToString(),
                row[4].ToString(),
                row[5].ToString()
                };
                dataGridView1.Rows.Add(rowArray); 

            }
            */
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
