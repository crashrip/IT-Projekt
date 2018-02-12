using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App.View
{
    public partial class Comparator : Form
    {
        private int schemaOriginal_sid;
        private string schemaOriginal_name;

        private int schemaComparable_sid;
        private string schemaComparable_name;

        public Comparator()
        {
            InitializeComponent();
        }

        public void SetSchemaOriginal(string schema)
        {
            DataRow[] dr = DBContext.Service().GetData(
                    "SELECT * " +
                    "FROM AGS_ANALYSIS_SITUATION_SCHEMA " +
                    "WHERE ASS_NAME = " + schema
                ).Copy().Select();

            schemaOriginal_sid = Int32.Parse(dr[0].ItemArray[0].ToString());
            schemaOriginal_name = dr[0].ItemArray[1].ToString();

            Console.WriteLine("[SetSchemaOriginal] schema_id: {0}, schema: {1}", schemaOriginal_sid, schemaOriginal_name);
        }

        public void SetSchemaOriginal(int ass_sid)
        {
            DataRow[] dr = DBContext.Service().GetData(
                    "SELECT * " +
                    "FROM AGS_ANALYSIS_SITUATION_SCHEMA " +
                    "WHERE ASS_SID = " + ass_sid
                ).Copy().Select();

            schemaOriginal_sid = Int32.Parse(dr[0].ItemArray[0].ToString());
            schemaOriginal_name = dr[0].ItemArray[1].ToString();

            Console.WriteLine("[SetSchemaOriginal] schema_id: {0}, schema: {1}", schemaOriginal_sid, schemaOriginal_name);
        }

        public void SetSchemaComparable(string schema)
        {
            Console.WriteLine("[SetSchemaComparable(string)]");

            DataRow[] dr = DBContext.Service().GetData(
                    "SELECT * " +
                    "FROM AGS_ANALYSIS_SITUATION_SCHEMA " +
                    "WHERE ASS_NAME = '" + schema + "'"
                ).Copy().Select();

            schemaComparable_sid = Int32.Parse(dr[0].ItemArray[0].ToString());
            schemaComparable_name = dr[0].ItemArray[1].ToString();

            Console.WriteLine("[SetSchemaComparable] schema_id: {0}, schema: {1}", schemaComparable_sid, schemaComparable_name);
        }

        public void SetSchemaComparable(int ass_sid)
        {
            Console.WriteLine("[SetSchemaComparable(int)]");

            DataRow[] dr = DBContext.Service().GetData(
                    "SELECT * " +
                    "FROM AGS_ANALYSIS_SITUATION_SCHEMA " +
                    "WHERE ASS_SID = " + ass_sid
                ).Copy().Select();

            schemaComparable_sid = Int32.Parse(dr[0].ItemArray[0].ToString());
            schemaComparable_name = dr[0].ItemArray[1].ToString();

            Console.WriteLine("[SetSchemaComparable] schema_id: {0}, schema: {1}", schemaComparable_sid, schemaComparable_name);
        }

        public void CompareSchemas()
        {
            if (schemaOriginal_sid <= 0 || schemaComparable_sid <= 0) return;


        }
    }
}
