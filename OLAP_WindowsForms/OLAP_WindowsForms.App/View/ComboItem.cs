using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
