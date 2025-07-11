using System;
using System.Windows.Forms;

namespace WindowsFormsApp2.Forms
{
    public partial class fPay : DevExpress.XtraEditors.XtraForm
    {
        private readonly decimal _total;
        public ResultData Result { get; private set; }
        public class ResultData
        {
            public decimal Total { get; set; }
            public decimal Cash { get; set; }
            public decimal Card { get; set; }
            public decimal IncomingSum { get; set; }
        }
        public fPay(decimal total = 0)
        {
            InitializeComponent();
            _total = total;
        }

        private void fPay_Load(object sender, EventArgs e)
        {
            if (_total is 0)
            {
                tCash_Total.ReadOnly = false;
                tCashCard_Total.ReadOnly = false;
            }
        }

        private void bCash_Click(object sender, EventArgs e)
        {
            this.Text = "Ödəniş növü - NAĞD";
            this.Size = new System.Drawing.Size(460, 240);
            navigationFrame1.SelectedPage = pageCash;
            tCash_Total.EditValue = _total;
            if (_total is 0)
                tCash_Total.Focus();
            else
                tCash_Paid.Focus();

        }

        private void bCard_Click(object sender, EventArgs e)
        {
            Result = new ResultData()
            {
                Total = _total,
                Card = _total
            };
            DialogResult = DialogResult.OK;
        }

        private void bCashCard_Click(object sender, EventArgs e)
        {
            this.Text = "Ödəniş növü - NAĞD & KART";
            this.Size = new System.Drawing.Size(460, 290);
            navigationFrame1.SelectedPage = pageCashCard;
            tCashCard_Total.EditValue = _total;
            if (_total is 0)
                tCashCard_Total.Focus();
            else
                tCashCard_Card.Focus();
        }

        private void tCash_Paid_EditValueChanged(object sender, EventArgs e)
        {
            decimal total = Convert.ToDecimal(tCash_Total.Text);
            decimal paid = Convert.ToDecimal(tCash_Paid.Text);
            decimal balance = paid - total;
            if (paid >= total)
            {
                tCash_Balance.Text = balance.ToString();
                lCash_Message.Visible = false;
                this.Size = new System.Drawing.Size(460, 240);
            }
            else
            {
                tCash_Balance.Text = 0.ToString("N2");
                this.Size = new System.Drawing.Size(460, 255);
                lCash_Message.Text = $" ÖDƏNİŞ MƏBLƏĞİ AZ DAXİL EDİLMİŞDİR. MİNİMUM NAĞD ÖDƏNİŞ : {total}";
                lCash_Message.Visible = true;
            }
        }

        private void tCashCard_Card_EditValueChanged(object sender, EventArgs e)
        {
            decimal total = Convert.ToDecimal(tCashCard_Total.Text);
            decimal card = Convert.ToDecimal(tCashCard_Card.Text);
            if (card < total)
            {
                tCashCard_Cash.EditValue = total - card;
            }
        }

        private void tCashCard_Cash_EditValueChanged(object sender, EventArgs e)
        {
            decimal total = Convert.ToDecimal(tCashCard_Total.Text);
            decimal card = Convert.ToDecimal(tCashCard_Card.Text);
            decimal cash = Convert.ToDecimal(tCashCard_Cash.Text);

            decimal cashTotal = total - card;

            if (cash >= cashTotal)
            {
                tCashCard_Balance.EditValue = cash - cashTotal;
                lCashCard_Message.Visible = false;
                this.Size = new System.Drawing.Size(460, 290);
            }
            else
            {
                tCashCard_Balance.EditValue = 0.ToString();
                this.Size = new System.Drawing.Size(460, 310);
                lCashCard_Message.Text = $"NAĞD ÖDƏNİŞ MƏBLƏĞİ AZ DAXİL EDİLMİŞDİR. MİNİMUM NAĞD ÖDƏNİŞ : {cashTotal}";
                lCashCard_Message.Visible = true;
            }
        }

        private void bCash_Enter_Click(object sender, EventArgs e)
        {
            Result = new ResultData()
            {
                Total = Convert.ToDecimal(tCash_Total.EditValue),
                Cash = Convert.ToDecimal(tCash_Total.EditValue),
                IncomingSum = Convert.ToDecimal(tCash_Paid.EditValue),
            };
            DialogResult = DialogResult.OK;
        }

        private void tCashCard_Enter_Click(object sender, EventArgs e)
        {
            Result = new ResultData()
            {
                Total = Convert.ToDecimal(tCashCard_Total.EditValue),
                Card = Convert.ToDecimal(tCashCard_Card.EditValue),
                Cash = Convert.ToDecimal(tCashCard_Total.EditValue) - Convert.ToDecimal(tCashCard_Card.EditValue),
                IncomingSum = Convert.ToDecimal(tCashCard_Cash.EditValue),
            };
            DialogResult = DialogResult.OK;
        }
    }
}