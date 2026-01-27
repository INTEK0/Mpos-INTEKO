using System;
using DevExpress.XtraEditors;
using WindowsFormsApp2.App.Application;
using WindowsFormsApp2.App.Services;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.Forms
{
    public partial class fApiTest : DevExpress.XtraEditors.XtraForm
    {
        public fApiTest()
        {
            InitializeComponent();
        }

        private async void fApiTest_Load(object sender, EventArgs e)
        {
            var invoiceData = App.Helpers.DbHelpers.GetInvoiceData();
            var saleData = App.Helpers.DbHelpers.GetSaleDetailsData();
            var paymentTypesData = App.Helpers.DbHelpers.PaymentTypesData();


            var facade = new SyncFacade();
            var result = await facade.SendInvoicesAsync();
            //var result1 = await facade.SendPaymentsAsync();
            var result2 = await facade.SendSalesAsync();

            if (result.Ok)
                XtraMessageBox.Show($"Uğurlu: {result.Inserted} sətir göndərildi");
            else
                XtraMessageBox.Show($"Xəta: {result.Error}");

            //ApiService service = new ApiService();
            //var result = await service.SendAsync(paymentTypesData, ApiOperation.Odenis);

            //if (result.Ok)
            //{
            //    XtraMessageBox.Show($"Uğurlu: {result.Inserted} sətir data əlavə edildi");
            //}
            //else
            //{
            //    XtraMessageBox.Show($"Xəta: {result.Error}");
            //}
        }
    }
}