using DevExpress.XtraEditors;
using System;
using System.Windows.Forms;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.NKA;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fDeposit : DevExpress.XtraEditors.XtraForm
    {
        public decimal depositAmount = 0;
        private POS_LAYOUT_NEW _frm;
        private readonly IpModel _IpModel = FormHelpers.GetIpModel();
        public fDeposit(POS_LAYOUT_NEW frm)
        {
            InitializeComponent();
            _frm = frm;
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
            tPaid.EditValue = 0;
        }

        private void bEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tPaid.Text))
            {
                decimal amount = Convert.ToDecimal(tPaid.EditValue.ToString());
                if (amount > 0)
                {
                    switch (_IpModel.Model)
                    {
                        case "1": Sunmi.Deposit(new DTOs.DepositDto
                        {
                            Sum = amount,
                            Cashier = _IpModel.Cashier,
                            IpAddress = _IpModel.Ip
                        });
                            break;
                        case "2": 
                            break;
                        case "3": 
                            break;
                        case "4": 
                            break;
                        case "5":
                            break;
                        case "6":
                            depositAmount = amount;
                            _frm.deposit(amount);
                            break;
                        case "7": 
                            break;
                    }

                    //DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                FormHelpers.Alert("Məbləğ daxil edilmədi", Enums.MessageType.Warning);
            }
        }

        private void tPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode is Keys.Enter)
            {
                bEnter_Click(null, null);
            }
        }
    }
}