using System;
using System.Windows.Forms;

namespace WindowsFormsApp2.Forms
{
    public partial class fCardAndOtherPay : DevExpress.XtraEditors.XtraForm
    {
        public fCardAndOtherPay()
        {
            InitializeComponent();
        }

        private void fCardAndOtherPay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void bCard_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

        private void bOtherPay_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;

        }
    }
}