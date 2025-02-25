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
        }


        public fPrepaymentPay(string fiscalId)
        {
            InitializeComponent();
            _fiskalID = fiscalId;
        }

        private void fPrepaymentPay_Load(object sender, EventArgs e)
        {
            string query = $@"SELECT pos_satis_check_main_id,
                            Prepayment,
                            fiscal_id,
                            UMUMI_MEBLEG
                            FROM [pos_satis_check_main]
                            where Prepayment>0
                            and fiscalNum= '{_fiskalID}'";

            var data = DbProsedures.ConvertToDataTable(query);

            PrepaymentPay.PosMainId = data.Rows[0].Field<int>("pos_satis_check_main_id");
            PrepaymentPay.Prepayment = data.Rows[0].Field<decimal>("Prepayment");
            PrepaymentPay.FiskalId = data.Rows[0].Field<string>("fiscal_id");
            PrepaymentPay.Total = data.Rows[0].Field<decimal>("UMUMI_MEBLEG");
        }

        private void bCash_Click(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(460, 240);
            navigationFrame1.SelectedPage = pageCash;
            tCash_Total.EditValue = PrepaymentPay.Total - PrepaymentPay.Prepayment;
        }

        private void bCard_Click(object sender, EventArgs e)
        {
            if (PrepaymentPay.PosMainId > 0)
            {
                switch (_IpModel.Model)
                {
                    case "1":
                        Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = _IpModel.Ip,
                            Cashier = _IpModel.Cashier,
                            FiscalId = PrepaymentPay.FiskalId,
                            PrepaymentPay = PrepaymentPay.Prepayment,
                            Card = PrepaymentPay.Total - PrepaymentPay.Prepayment,
                            Total = PrepaymentPay.Total,
                            Cash = 0,
                            IncomingSum = 0,
                            PayType = Enums.PayType.Card
                        }, PrepaymentPay.PosMainId);
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
            this.Size = new System.Drawing.Size(460, 300);
            navigationFrame1.SelectedPage = pageCashCard;
        }

        private void bCash_Enter_Click(object sender, EventArgs e)
        {
            if (PrepaymentPay.PosMainId > 0)
            {
                switch (_IpModel.Model)
                {
                    case "1":
                        Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = _IpModel.Ip,
                            Cashier = _IpModel.Cashier,
                            FiscalId = PrepaymentPay.FiskalId,
                            PrepaymentPay = PrepaymentPay.Prepayment,
                            Card = 0,
                            Total = PrepaymentPay.Total,
                            Cash = Convert.ToDecimal(tCash_Paid.EditValue),
                            PayType = Enums.PayType.Cash
                        }, PrepaymentPay.PosMainId);
                        break; /*SUNMI*/
                    case "3":
                        Omnitech.PrepaymentSale(_IpModel.Ip, null, PrepaymentPay.PosMainId, _IpModel.Cashier, PrepaymentPay.FiskalId, PrepaymentPay.Prepayment, Enums.PayType.Cash);
                        break; /*OMNITECH*/
                }
            }
            else
            {
                ReadyMessages.ERROR_DEFAULT_MESSAGE("Fiskal id nömrəsi düzgün daxil edilmədi");
            }
        }

        private void tCashCard_Enter_Click(object sender, EventArgs e)
        {
            string query = $@"SELECT pos_satis_check_main_id,
                            Prepayment,
                            fiscal_id
                            FROM [pos_satis_check_main]
                            where Prepayment>0
                            and fiscalNum= '{_fiskalID}'";

            var data = DbProsedures.ConvertToDataTable(query);
            int number = data.Rows[0].Field<int>("pos_satis_check_main_id");
            decimal prepay = data.Rows[0].Field<decimal>("Prepayment");
            string fiskalid = data.Rows[0].Field<string>("fiscal_id");

            if (number > 0)
            {
                switch (_IpModel.Model)
                {
                    case "1":
                        Sunmi.PrepaymentSale(new DTOs.SalesDto
                        {
                            IpAddress = _IpModel.Ip,
                            Cashier = _IpModel.Cashier,
                            FiscalId = fiskalid,
                            PrepaymentPay = prepay,
                            PayType = Enums.PayType.Card
                        }, number);
                        break; /*SUNMI*/
                    case "3":
                        Omnitech.PrepaymentSale(_IpModel.Ip, null, number, _IpModel.Cashier, fiskalid, prepay, Enums.PayType.CashCard);
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
            }
            else
            {
                tCash_Balance.Text = 0.ToString("N2");
            }
        }
    }
}