using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLAP_WindowsForms.App.View
{
    public class Insert_item
    {
            private string column_name;
            private string s_value;
            private int i_value = -404;

            public Insert_item(string column_name, string s_value)
            {
                this.column_name = column_name;
                this.s_value = s_value;
            }

            public Insert_item(string column_name, int i_value)
            {
                this.column_name = column_name;
                this.i_value = i_value;
            }
            
            public bool IntValue() {
                if (i_value != -404)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            
            public bool IsNull()
            {
                if (s_value == null && i_value == -404)
                {
                    return true;
                } else { return false; }
            }

            public string GetColumnName() { return this.column_name; }
            public string GetSValue() { return this.s_value; }
            public int GetIValue() { return this.i_value; }
        }
    }

