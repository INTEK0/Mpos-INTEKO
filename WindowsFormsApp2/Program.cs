using System;
using System.Globalization;
using System.Net;
using System.Windows.Forms;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Forms.PrintPages;
using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WebClient web = new WebClient();

            var culture = new CultureInfo("az-AZ");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
            CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ".";
            CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol = "₼"; //₼
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormHelpers.FolderControl();
            Application.Run(new avtorizasiya());
        }
    }
}
