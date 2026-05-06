using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraGrid.Localization;
using DevExpress.XtraReports.Design;
using Licence.Forms;
using Licence.Services;
using Serilog;
using Serilog.Context;
using WindowsFormsApp2.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.CacheData;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2
{
    static class Program
    {
        private static readonly string _licenceKey = LicenseService.Instance.GetLicenceKey();
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        const int SW_RESTORE = 9;
        [STAThread]
        static void Main()
        {
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .WriteTo.Async(a => a.File(
                    "logs\\app-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:dd-MM-yyyy HH:mm:ss} " +
                    "[{Level:u4}] " +
                    "[{UUID}] " +
                    "{Message:lj}{NewLine}{Exception}"
                ))
                .CreateLogger();


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.XtraEditors.Controls.Localizer.Active = new CustomLocalizer();
            GridLocalizer.Active = new MyGridLocalizer();

            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            bool createdNew;
            Licence.Services.AppService.mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                Process current = Process.GetCurrentProcess();
                Process[] processes = Process.GetProcessesByName(current.ProcessName);

                foreach (var process in processes)
                {
                    if (process.Id != current.Id)
                    {
                        IntPtr handle = process.MainWindowHandle;
                        if (handle != IntPtr.Zero)
                        {
                            ShowWindow(handle, SW_RESTORE);
                            SetForegroundWindow(handle);
                        }
                        break;
                    }
                }

                return;
            }


            #region [..Licence..]

            if (string.IsNullOrWhiteSpace(_licenceKey) || _licenceKey is "Yoxdur")
            {
                var result = new fRegister().ShowDialog();

                if (result == DialogResult.OK)
                {
                    Application.Restart();
                }
                return;
            }


            if (Licence.Helpers.FormHelpers.HasInternetConnection())
            {
                var user = LicenseService.Instance.RequestKeyControl(_licenceKey).Result;
                if (user == null || !user.IsActive || !LicenseService.Instance.LicenceExpireDateControl(user))
                {
                    Application.Run(new fDeactive(user));
                    return;
                }
            }
            else
            {
                FormHelpers.Alert("İnternet bağlantınız yoxdur", Enums.MessageType.Warning);
            }

            #endregion [..Licence..]


            FolderControl();
            CultureInfoData();



            try
            {
                LogContext.PushProperty("UUID", UUIDGenerateService.GetToken());
                Serilog.Log.Information("Application started");
                Application.Run(new avtorizasiya());
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Application crashed");
            }
            finally
            {
                Serilog.Log.Information("Application exit");
                Serilog.Log.CloseAndFlush();
            }
        }

        static void CultureInfoData()
        {
            var culture = new CultureInfo("az-AZ");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator = ",";
            CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ".";
            CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol = "₼"; //₼
        }
    }
}
