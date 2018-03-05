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
    public partial class ShowSQL : Form
    {
        public ShowSQL()
        {
            InitializeComponent();
        }

        public ShowSQL(String sql)
        {
            InitializeComponent();
            this.richTextBox1.Text = sql;
            
        }

    }
}
