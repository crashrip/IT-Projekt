using OLAP_WindowsForms.App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLAP_WindowsForms.App
{
    class SchemaInitializer
    {
       
        public SchemaInitializer()
        {
            SchemaView view = new SchemaView();
            view.ShowDialog(view);
        }
    }
}
