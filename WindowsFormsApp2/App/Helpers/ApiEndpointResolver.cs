using System;
using static WindowsFormsApp2.App.Helpers.Enums;

namespace WindowsFormsApp2.App.Helpers
{
    public static class ApiEndpointResolver
    {
        public static string Resolve(ApiOperation operation)
        {
            switch (operation)
            {
                case ApiOperation.ProductInvoice:
                    return "productpurchase";
                case ApiOperation.WarehouseStock:
                    return "warehousestock";
                case ApiOperation.SaleDetail:
                    return "salesdetail";
                case ApiOperation.SaleRefund:
                    return "salerefund";
                case ApiOperation.Profit:
                    return "profit";
                case ApiOperation.PaymentTypes:
                    return "payment";
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }
    }
}
