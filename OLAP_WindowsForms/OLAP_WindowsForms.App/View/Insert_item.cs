using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLAP_WindowsForms.App.View
{
    public class Insert_item
    {
            private String column_name;
            private String s_value;
            private int i_value = -404;

            public Insert_item(String column_name, String s_value)
            {
                this.column_name = column_name;
                this.s_value = s_value;
            }

            public Insert_item(String column_name, int i_value)
            {
                this.column_name = column_name;
                this.i_value = i_value;
            }
            
            public Boolean intValue() {
            if (i_value != -404)
            {
                return true;
            } else
            {
                return false;
            }
            }
            
            public Boolean isNull()
        {
            if (s_value == null && i_value == -404)
            {
                return true;
            } else { return false; }
        }
            public String getColumnName() { return this.column_name; }
            public String getSValue() { return this.s_value; }
            public int getIValue() { return this.i_value; }
        }
    }

