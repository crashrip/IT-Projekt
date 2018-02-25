using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OLAP_WindowsForms.App.View;

namespace OLAP_WindowsForms.App
{
    public static class DBContext
    {
        private static DBService _service;
        //public static DataView _dataView;

        public static  void Initialize()
        {
            if (_service == null)
            {
                _service = new DBService();
            }
        }

        public static DBService Service()
        {
            return _service ?? throw new Exception("DBContext not initialized"); 
        }

        /*public static DataView DataView()
        {
            return _dataView ?? throw new Exception("DBContext not initialized");
        }*/
    }
}
