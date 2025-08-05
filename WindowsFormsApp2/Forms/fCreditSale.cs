using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.ExpressionEditor;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit.Import.Html;
using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using WindowsFormsApp2.NKA;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using WindowsFormsApp2.Validations;
using static WindowsFormsApp2.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fCreditSale : DevExpress.XtraEditors.XtraForm
    {
        private readonly DatabaseClasses.User _user = DbProsedures.GetUser();
        private readonly FormHelpers.IpModel _terminal = FormHelpers.GetIpModel();
        public fCreditSale()
        {
            InitializeComponent();
        }

        private void fCreditSale_Load(object sender, EventArgs e)
        {
            tProccessNo.Text = DbProsedures.GET_CreditSaleProccessNo();
            tCashier.Text = _user.NameSurname;
        }

        private void tCustomerName_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void tZamin_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void tProductName_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }

        private void bPay_Click(object sender, EventArgs e)
        {
            decimal payment = Convert.ToDecimal(tInitialAmount.Text);
            if (payment > 0)
            {
                fPay pay = new fPay(payment);
                if (pay.ShowDialog() is DialogResult.OK)
                {
                    Payment(pay.Result.Total, pay.Result.Cash, pay.Result.Card, pay.Result.IncomingSum);
                }
            }
            else
            {
                Payment(0, 0, 0, 0);
            }
        }

        private void Payment(decimal Total, decimal Cash, decimal Card, decimal IncomingSum)
        {
            string uuid = Guid.NewGuid().ToString();
            string vahid = null;// get_vahid();

            var data = Validation();
            if (data is null) return;

            try
            {
                decimal quantity = Convert.ToDecimal(tQuantity.Text);

                //int vatType = Convert.ToInt32(label9.Text);
                int quantityType = Convert.ToInt32(vahid);

                decimal creditPayment = Convert.ToDecimal(tTotalAmount.Text);

                DTOs.CreditSaleDto creditDto = new DTOs.CreditSaleDto()
                {
                    Url = _terminal.Ip,
                    MerchantId = _terminal.MerchantId,
                    CustomerName = tCustomerName.Text,
                    Cashier = _user.NameSurname,
                    DocumentUUID = uuid,
                    CreditContract = tContractNo.Text.Trim(),
                    CashPayment = Cash,
                    CardPayment = Card,
                    IncomingSum = IncomingSum,
                    creditPayment = creditPayment,
                    Note = tComment.Text.Trim(),
                    item = new DTOs.CreditSaleDto.Item()
                    {
                        ProductName = tProductName.Text,
                        ProductCode = "0", //Barcode
                        Quantity = quantity,
                        QuantityType = quantityType,
                        SalePrice = Convert.ToDecimal(tSalePrice.Text),
                        VatType = 0 //VatType
                    }
                };

                switch (_terminal.Model)
                {
                    case "1":
                        var SunmiResult = Sunmi.CreditSale(creditDto);

                        if (SunmiResult.Item1 == true)
                        {
                            CreditSaleMain(data, SunmiResult.Item2, SunmiResult.Item3);
                        }
                        break;
                    case "2":
                        var AzSmartResult = AzSmart.CreditSale(creditDto);

                        if (AzSmartResult.Item1)
                        {
                            CreditSaleMain(data, AzSmartResult.Item2, AzSmartResult.Item3);
                        }
                        break;
                    case "3":
                        var OmnitechResult = Omnitech.CreditSale(creditDto);

                        if (OmnitechResult.Item1)
                        {
                            CreditSaleMain(data, OmnitechResult.Item2, OmnitechResult.Item3);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("XƏTA \n\n" + ex.Message);
            }
        }

        private async void CreditSaleMain(DatabaseClasses.CreditMain data, string longFiscalId, string shortFiscalId)
        {
            data.LonfFiskalId = longFiscalId;
            data.ShortFiskalId = shortFiscalId;
            int id = await DbProsedures.Insert_CreditMain(data);

            DbProsedures.InsertCustomerDebt(CustomerDebtType.CreditSale,
                DateTime.Now,
                Convert.ToInt32(data.CustomerId),
                Convert.ToDecimal(data.OdenilenMebleg));

            await CreditMonthAdd(id, longFiscalId);

            FormHelpers.Log($"{tProductName.Text} məhsulu {tContractNo.Text} müqavilə nömrəsinə əsasən kredit satışı ilə satıldı.");
            //krediprint();
        }

        private async Task CreditMonthAdd(int creditMainId, string fiscalId)
        {
            int month = Convert.ToInt32(cmbMonth.Text);
            for (int i = 1; i <= month; i++)
            {
                int j = 30 * i;
                DatabaseClasses.CreditSaleMonth item = new DatabaseClasses.CreditSaleMonth()
                {
                    CreditSaleId = creditMainId,
                    Month = i,
                    PaymentDay = DateTime.Today.AddDays(j),
                    Amount = Convert.ToDecimal(tCreditPeriodAmount.Text),
                    CreditSaleFiscalId = fiscalId
                };

                await DbProsedures.Insert_CreditMonth(item);
            }
        }

        private DatabaseClasses.CreditMain Validation()
        {
            //DatabaseClasses.CreditMain credit = new DatabaseClasses.CreditMain();
            //credit.ProcessNo = tProccessNo.Text;
            //credit.ContractNo = tContractNo.Text.Trim();
            //credit.OdenilenMebleg = Decimal.Parse(tTotal.Text);
            //credit.CustomerName = tCustomerName.Text.Trim();
            //credit.CustomerId = string.IsNullOrWhiteSpace(customerId) ? 0 : Convert.ToInt32(customerId);
            //credit.ZaminName = string.IsNullOrWhiteSpace(tZamin.Text) ? "YOXDUR" : tZamin.Text.Trim();
            //credit.ZaminId = string.IsNullOrWhiteSpace(zaminId) ? 1 : Convert.ToInt32(zaminId);
            //credit.SupplierName = tSupplier.Text;
            //credit.ProductId = string.IsNullOrWhiteSpace(mal_alisi_details_id) ? 0 : Convert.ToInt32(mal_alisi_details_id);
            //credit.ProductName = tProductName.Text;
            //credit.Quantity = Decimal.Parse(tQuantity.Text);
            //credit.SalePrice = Decimal.Parse(tSalePrice.Text);
            //credit.DiscountPercent = Decimal.Parse(tDiscountPercent.Text);
            //credit.DiscountAmount = Decimal.Parse(tDiscountAmount.Text);
            //credit.Taksit = string.IsNullOrWhiteSpace(cmbMonth.Text) ? 0 : Convert.ToInt32(cmbMonth.Text);
            //credit.Total = Decimal.Parse(tTotal.Text);
            //credit.IlkinOdenis = Decimal.Parse(tIlkinOdenis.Text);
            //credit.Comment = memoEdit1.Text.Trim();
            //credit.MonthAmount = Decimal.Parse(tAyliqOdenis.Text);


            //var validator = new CreditValidation();
            //var validateResult = validator.Validate(credit);

            //if (!validateResult.IsValid)
            //{
            //    foreach (var error in validateResult.Errors)
            //    {
            //        FormHelpers.Alert(error.ErrorMessage, Enums.MessageType.Warning);
            //        return null;
            //    }
            //}

            return null;
        }
    }
}