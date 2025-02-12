using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fAdminPassword : DevExpress.XtraEditors.XtraForm
    {
        public fAdminPassword()
        {
            InitializeComponent();
        }

        private void Submit()
        {
            if (tPassword.Text.Trim() is "inteko12348765")
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                tPassword.Text = null;
                FormHelpers.Alert("Şifrə yanlışdır", Enums.MessageType.Error);
                tPassword.Focus();
            }
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            Submit();
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