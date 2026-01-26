using System;
using WindowsFormsApp2.App.Services;

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

            var syncService = new InvoiceService("");
            var success = await syncService.SendInvoicesAsync(invoiceData);
        }
    }
}