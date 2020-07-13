using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1.Model;

namespace Zadatak_1.Actions
{
    /// <summary>
    /// Class responsible for logging manager actions by using Delegate and Events.
    /// </summary>
    class LogActions
    {
        public delegate void LogAction();

        public static event LogAction Log;

        public static void LogDeleteProduct(Product p)
        {
            Log = () =>
            {
                File.AppendAllText(@"..\\..\Files\ManagerLogFile.txt", "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "]" + " Product: " + p.Name + ", Code: " + p.Code + ", has been deleted." + Environment.NewLine);
            };
            Log.Invoke();
        }

        public static void LogCreateProduct(Product p)
        {
            Log = () =>
            {
                File.AppendAllText(@"..\\..\Files\ManagerLogFile.txt", "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "]" + " Product: " + p.Name + ", Code: " + p.Code + ", has been created." + Environment.NewLine);
            };
            Log.Invoke();
        }

        public static void LogEditProduct(Product p)
        {
            Log = () =>
            {
                File.AppendAllText(@"..\\..\Files\ManagerLogFile.txt", "[" + DateTime.Now.ToString("dd.MM.yyyy HH:mm") + "]" + " Product: " + p.Name + ", Code: " + p.Code + ", has been edited." + Environment.NewLine);
            };
            Log.Invoke();
        }
    }
}
