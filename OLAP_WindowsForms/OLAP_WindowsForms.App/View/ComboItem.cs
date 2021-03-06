﻿using System;
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

        public string toString()
        {
            return this.ID + " " + this.Text;
        }

        // get data value and description from cube for certain table, 2 columns and a combobox
        public static void SetComboboxContent(ComboBox combobox, string table, string column)
        {
            DataTable dt = DBContext.Service().GetData(table, column).Copy();

            combobox.DataSource = dt;
            combobox.DisplayMember = column;
            combobox.ValueMember = column;
        }

        // get data value and description from cube for certain table, 2 columns and a combobox
        public static void SetComboboxContent(ComboBox combobox, string table, string column1, string column2)
        {
            DataTable dt = DBContext.Service().GetData(table, column1, column2).Copy();

            combobox.DataSource = dt;
            combobox.DisplayMember = column2;
            combobox.ValueMember = column1;
        }
    }
}
