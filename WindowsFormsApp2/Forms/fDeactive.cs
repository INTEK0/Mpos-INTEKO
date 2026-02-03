using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Licence.Services;
using Microsoft.Win32;

namespace WindowsFormsApp2.Forms
{
    public partial class fDeactive : DevExpress.XtraEditors.XtraForm
    {
        private static readonly string _licenceKey = LicenseService.Instance.GetLicenceKey();
        private Licence.Entities.User _user = null;
        private int hiddenPanel = 1;
        public fDeactive(Licence.Entities.User user)
        {
            InitializeComponent();
            _user = user;
        }

        private async void bLicenceControl_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                _user = await LicenseService.Instance.RequestKeyControl(_licenceKey);
                if (_user != null)
                {

                    if (_user.IsActive && LicenseService.Instance.LicenceExpireDateControl(_user))
                    {
                        Application.Restart();
                    }
                    else
                    {
                        ErrorMesssage();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally {  Cursor.Current = Cursors.Default; }

        }

        private void bExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ErrorMesssage()
        {
            bool licenceDateResult = LicenseService.Instance.LicenceExpireDateControl(_user);
            if (!licenceDateResult)
            {
                lMessage.Text = $"Lisenziya müddəti {_user.LicenceExpireDate.ToString("dd.MM.yyyy")} tarixində bitmişdir.\nƏtraflı məlumat üçün <b>(055-206-23-66)</b> nömrəsi ilə əlaqə saxlayın.";
                return;
            }
            else
            {
                lMessage.Text = "Ətraflı məlumat üçün <b>(055-206-23-66)</b> nömrəsi ilə əlaqə saxlayın.";
                return;
            }
        }

        private void picLogo_DoubleClick(object sender, EventArgs e)
        {
            if (hiddenPanel is 3)
            {
                Licence.Forms.fAdmin admin = new Licence.Forms.fAdmin();
                if (admin.ShowDialog() is DialogResult.OK)
                {
                    this.Hide();
                    avtorizasiya f = new avtorizasiya();
                    f.Show();
                }
                else
                {
                    XtraMessageBox.Show("Şifrə səhvdir.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                hiddenPanel++;
            }
        }

        private void fDeactive_Load(object sender, EventArgs e)
        {
            Licence.Services.LicenseService.Instance.Stop();
            lProductID.Text = "ID: " + Registry.CurrentUser.OpenSubKey("Mpos").GetValue("ProductID").ToString();
            lVersion.Text = $"Version: {Application.ProductVersion}";
            ErrorMesssage();
        }
    }
}