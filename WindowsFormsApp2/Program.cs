using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Design;
using WindowsFormsApp2.Helpers;
using static WindowsFormsApp2.Helpers.FormHelpers;

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
            DevExpress.XtraEditors.Controls.Localizer.Active = new CustomLocalizer();
            GridLocalizer.Active = new MyGridLocalizer();

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
