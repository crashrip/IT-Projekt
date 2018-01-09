using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OLAP_WindowsForms.App
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DBContext.Initialize();

            try
            {
                Application.Run(new BaseMenu());
            }
            catch (Exception e)
            {
                // wrong login data
                Console.WriteLine("Wrong login data: " + e.Data);
            }
        }
    }
}
