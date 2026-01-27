using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class InvoiceProductDto
    {
        public string Voen { get; set; }
        public List<Items> items { get; set; }

        public class Items
        {
            public string Date { get; set; }
            public string Username { get; set; }
            public string InvoiceNo { get; set; }
            public string SupplierName { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public double Quantity { get; set; }
            public double PurchasePrice { get; set; }
            public int DiscountPercantages { get; set; }
            public double DiscountAzn { get; set; }
            public double DiscountTotalAmount { get; set; }
            public double PayableAmount  { get; set; } //Ödəniləcək məbləğ
        }
    }
}
