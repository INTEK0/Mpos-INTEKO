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
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.Helpers.Messages;
using WindowsFormsApp2.NKA;
using static WindowsFormsApp2.Helpers.FormHelpers;

namespace WindowsFormsApp2.Forms
{
    public partial class fPrepaymentPay : DevExpress.XtraEditors.XtraForm
    {
        private readonly string _fiskalID = null;
        private readonly IpModel _IpModel = FormHelpers.GetIpModel();

        private static class PrepaymentPay
        {
            public static int PosMainId { get; set; }
            public static string FiskalId { get; set; }
            public static decimal Prepayment { get; set; }
            public static decimal Total { get; set; }
            public static string CustomerName { get; set; } = null;
        }

        public fPrepaymentPay(string fiscalId)
        {
            InitializeComponent();
            _fiskalID = fiscalId;
        }

        private void fPrepaymentPay_Load(object sender, EventArgs e)
        {
            string query = $@"SELECT psm.pos_satis_check_main_id,
                            psm.Prepayment,
                            psm.fiscal_id,
                            psm.UMUMI_MEBLEG,
                            m.AD + ' ' + m.SOYAD + ' ' + m.ATAADI as customerName
                            FROM [pos_satis_check_main] psm
                            LEFT join MUSTERILER m ON m.MUSTERILER_ID = psm.CustomerId
                            WHERE Prepayment>0
                            AND fiscalNum = '{_fiskalID}'";

            var data = DbProsedures.ConvertToDataTable(query);

            PrepaymentPay.PosMainId = data.Rows[0].Field<int>("pos_satis_check_main_id");
            PrepaymentPay.Prepayment = data.Rows[0].Field<decimal>("Prepayment");
            PrepaymentPay.FiskalId = data.Rows[0].Field<string>("fiscal_id");
            PrepaymentPay.Total = data.Rows[0].Field<decimal>("UMUMI_MEBLEG");
            PrepaymentPay.CustomerName = data.Rows[0].Field<string>("customerName");
        }

        private void bCash_Click(object sender, EventArgs e)
        {
            this.Text = "Ödəniş növü - NAĞD";
            this.Size = new System.Drawing.Size(460, 240);
            navigationFrame1.SelectedPage = pageCash;
            tCash_Total.EditValue = PrepaymentPay.Total - PrepaymentPay.Prepayment;
            tCash_Paid.EditValue = PrepaymentPay.Total - PrepaymentPay.Prepayment;
            tCash_Paid.Focus();
        }

        private void bCard_Click(object sender, EventArgs e)
        {
            this.Text = "Ödəniş növü - KART";

            if (PrepaymentPay.PosMainId > 0)
            {
                switch (_IpModel.Model)
                {
                    case "1":
                        bool isSuccess = Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = _IpModel.Ip,
                            Cashier = _IpModel.Cashier,
                            FiscalId = PrepaymentPay.FiskalId,
                            PrepaymentPay = PrepaymentPay.Prepayment,
                            Card = PrepaymentPay.Total - PrepaymentPay.Prepayment,
                            Total = PrepaymentPay.Total,
                            Cash = 0,
                            IncomingSum = 0,
                            CustomerNameManual = PrepaymentPay.CustomerName,
                            PayType = Enums.PayType.Card
                        }, PrepaymentPay.PosMainId);

                        if (isSuccess)
                        {
                            Close();
                        }
                        break; /*SUNMI*/
                    case "3":
                        Omnitech.PrepaymentSale(_IpModel.Ip, null, PrepaymentPay.PosMainId, _IpModel.Cashier, PrepaymentPay.FiskalId, PrepaymentPay.Prepayment, Enums.PayType.Card);
                        break; /*OMNITECH*/
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Fiskal id nömrəsi düzgün daxil edilmədi");
            }
        }

        private void bCashCard_Click(object sender, EventArgs e)
        {
            this.Text = "Ödəniş növü - NAĞD & KART";
            this.Size = new System.Drawing.Size(460, 290);
            navigationFrame1.SelectedPage = pageCashCard;
            tCashCard_Total.EditValue = PrepaymentPay.Total - PrepaymentPay.Prepayment;
        }

        private void bCash_Enter_Click(object sender, EventArgs e)
        {
            if (PrepaymentPay.PosMainId > 0)
            {
                switch (_IpModel.Model)
                {
                    case "1":
                       bool isSuccess = Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = _IpModel.Ip,
                            Cashier = _IpModel.Cashier,
                            FiscalId = PrepaymentPay.FiskalId,
                            PrepaymentPay = PrepaymentPay.Prepayment,
                            Card = 0,
                            Total = PrepaymentPay.Total,
                            Cash = Convert.ToDecimal(tCash_Paid.EditValue),
                            PayType = Enums.PayType.Cash,
                            CustomerNameManual = PrepaymentPay.CustomerName
                       }, PrepaymentPay.PosMainId);

                        if (isSuccess)
                        {
                            Close();
                        }
                        break; /*SUNMI*/
                    case "3":
                        Omnitech.PrepaymentSale(_IpModel.Ip, null, PrepaymentPay.PosMainId, _IpModel.Cashier, PrepaymentPay.FiskalId, PrepaymentPay.Prepayment, Enums.PayType.Cash);
                        break; /*OMNITECH*/
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Fiskal id nömrəsi düzgün daxil edilmədi");
                return;
            }
        }

        private void tCashCard_Enter_Click(object sender, EventArgs e)
        {
            if (PrepaymentPay.PosMainId > 0)
            {
                switch (_IpModel.Model)
                {
                    case "1":
                      bool isSuccess =  Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = _IpModel.Ip,
                            Cashier = _IpModel.Cashier,
                            FiscalId = PrepaymentPay.FiskalId,
                            PrepaymentPay = PrepaymentPay.Prepayment,
                            Cash = Convert.ToDecimal(tCashCard_Cash.EditValue),
                            Card = Convert.ToDecimal(tCashCard_Card.EditValue),
                            Total = PrepaymentPay.Total,
                            PayType = Enums.PayType.CashCard,
                          CustomerNameManual = PrepaymentPay.CustomerName
                      }, PrepaymentPay.PosMainId);

                        if (isSuccess)
                        {
                            Close();
                        }
                        break; /*SUNMI*/
                    case "3":
                        Omnitech.PrepaymentSale(_IpModel.Ip, null, PrepaymentPay.PosMainId, _IpModel.Cashier, PrepaymentPay.FiskalId, PrepaymentPay.Prepayment, Enums.PayType.CashCard);
                        break; /*OMNITECH*/
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Fiskal id nömrəsi düzgün daxil edilmədi");
            }
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
    }
}