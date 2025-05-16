using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Licence.Forms
{
    public partial class fAdmin : DevExpress.XtraEditors.XtraForm
    {
        public fAdmin()
        {
            InitializeComponent();
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            Submit();
        }

        private void Submit()
        {
            string key = Services.LicenseService.Instance.GetLicenceKey();
            var result = DialogResult.Cancel;
            if (key is "Yoxdur")
            {
                string currentDate = DateTime.Now.ToString("HH:mm");
                if (tPassword.Text.Trim() == $"inteko{currentDate}")
                {
                    result = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Şifrə səhvdir.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (tPassword.Text.Trim() == $"inteko{key.Substring(key.Length - 4)}")
                {
                    result = DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("Şifrə səhvdir.", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            DialogResult = result;
            this.Close();
        }

        private void tPassword_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    Submit();
                    break;
                case Keys.Escape:
                    Close();
                    break;
            }
        }
    }
}