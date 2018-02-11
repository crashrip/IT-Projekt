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
                Console.WriteLine("There has been an error: " + e.Message);
            }
        }
    }
}
