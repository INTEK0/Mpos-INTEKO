using System.ComponentModel;

namespace WindowsFormsApp2.App.Helpers
{
    public class Enums
    {
        public enum ApiOperation
        {
            [Description("Məhsul alışı hesabatı")]
            ProductInvoice = 1,
            [Description("Anbar qalığı hesabatı")]
            WarehouseStock,
            [Description("Mənfəət hesabatı")]
            Profit,
            [Description("Satış hesabatı")]
            SaleDetail,
            [Description("Satış qaytarma hesabatı")]
            SaleRefund,
            [Description("Ödəniş növü hesabatı")]
            PaymentTypes
        }
    }
}