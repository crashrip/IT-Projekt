using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App
{
    public partial class DataView : Form
    {
        public DataView()
        {
            InitializeComponent();
        }

        public void LoadData(DataTable dt)
        {
            dataGridView1.DataSource = dt;
        }
    }
}
