using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App
{
    class ComboItem
    {
        public int ID { get; set; }
        public string Text { get; set; }

        public ComboItem(int id, string text)
        {
            ID = id;
            Text = text;
        }

        public String toString()
        {
            return this.ID + " " + this.Text;
        }

        // get data value and description from cube for certain table, 2 columns and a combobox
        public static void GetComboboxContent(ComboBox combobox, String table, String column1, String column2)
        {
            DataTable dt = DBContext.Service().GetData(table, column1, column2);
            DataTable dt2 = dt.Copy();

            combobox.DataSource = dt2;
            combobox.DisplayMember = column2;
            combobox.ValueMember = column1;
        }

        // get data value and description from cube for certain table, 2 columns and a combobox
        public static void getListBoxContent(ListBox listbox, String table, String column1, String column2)
        {
            DataTable dt = DBContext.Service().GetData(table, column1, column2);
            DataTable dt2 = dt.Copy();

            listbox.DataSource = dt2;
            listbox.DisplayMember = column2;
            listbox.ValueMember = column1;

            listbox.SelectionMode = SelectionMode.MultiExtended;
        }
    }
}
