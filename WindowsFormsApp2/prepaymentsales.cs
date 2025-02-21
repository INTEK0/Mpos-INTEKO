using System;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2
{
    public partial class prepaymentsales : DevExpress.XtraEditors.XtraForm
    {
        private readonly POS_LAYOUT_NEW frm1;
        public prepaymentsales(POS_LAYOUT_NEW frm)
        {
            InitializeComponent();
            frm1 = frm;
        }

        private void IsSucces(Enums.PayType type)
        {
            frm1.prepaymentsalesfinish(textBox1.Text, type);
            DialogResult = DialogResult.OK;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            IsSucces(Enums.PayType.Cash);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            IsSucces(Enums.PayType.Card);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            IsSucces(Enums.PayType.CashCard);
        }
    }
}