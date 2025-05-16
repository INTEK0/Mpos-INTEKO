using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Licence.Services;
using Microsoft.Win32;

namespace Licence.Forms
{
    public partial class fDeactive : DevExpress.XtraEditors.XtraForm
    {
        public event EventHandler LoginRequested;
        private static readonly string _licenceKey = LicenceKey();
        private int hiddenPanel = 1;
        public fDeactive()
        {
            InitializeComponent();
        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bLicenceControl_Click(object sender, EventArgs e)
        {
            LicenseService.Instance.Start(_licenceKey);
        }

        private void fDeactive_Load(object sender, EventArgs e)
        {
            lProductID.Text = "ID: " + Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ProductID").ToString();
            lVersion.Text = $"Version: {Application.ProductVersion}";
        }

        private static string LicenceKey()
        {
            string key = "Yoxdur";
            if (Registry.GetValue(@"HKEY_CURRENT_USER\Mpos\", "ProductID", null) == null)
            {
                Registry.CurrentUser.CreateSubKey("Mpos").SetValue("ProductID", "Yoxdur");
            }
            else
            {
                key = Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ProductID").ToString();
            }
            return key;
        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            fAdmin admin = new fAdmin();
            if (admin.ShowDialog() is DialogResult.OK)
            {
                LoginRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                XtraMessageBox.Show("Şifrə səhvdir.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}