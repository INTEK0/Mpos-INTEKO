using DevExpress.XtraEditors;
using System;
using WindowsFormsApp2.Helpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fWithdraw : DevExpress.XtraEditors.XtraForm
    {
        public decimal depositAmount = 0;
        private readonly POS_LAYOUT_NEW frm1;
        decimal _total = default;
        public static decimal asd;
        public fWithdraw(POS_LAYOUT_NEW frm)
        {
            InitializeComponent();
            frm1 = frm;
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            var button = (SimpleButton)sender;
            if (button.Text == ",")
            {
                if (tPaid.Text.Contains(","))
                {
                    int index = tPaid.Text.IndexOf(",");
                    tPaid.SelectionStart = index + 1;
                }
            }
            else
            {
                int cursorPosition = tPaid.SelectionStart;

                if (tPaid.Text.Contains(",") && cursorPosition > tPaid.Text.IndexOf(","))
                {
                    int decimalPartLength = tPaid.Text.Length - tPaid.Text.IndexOf(",") - 1;

                    tPaid.EditValue = tPaid.Text.Insert(cursorPosition, button.Text);
                    tPaid.SelectionStart = cursorPosition + 1;

                }
                else
                {
                    if (tPaid.Text == "0,00")
                    {
                        tPaid.Text = null;
                    }
                    tPaid.EditValue = tPaid.Text.Insert(cursorPosition, button.Text);
                    tPaid.SelectionStart = cursorPosition + 1;
                }
            }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            tPaid.EditValue = null;
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tPaid.Text))
            {

                decimal amount = Convert.ToDecimal(tPaid.EditValue.ToString());
                asd = amount;
                if (amount > 0)
                {
                    depositAmount = amount;
                    _total = amount;
                    DialogResult = System.Windows.Forms.DialogResult.OK;


                    Deposits();
                    this.Close();
                }
            }
            else
            {
                FormHelpers.Alert("Məbləğ daxil edilmədi", Enums.MessageType.Warning);
            }
        }


        private void Deposits()
        {


            frm1.withdraw(asd);

        }
    }
}