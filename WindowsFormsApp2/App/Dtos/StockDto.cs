using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class StockDto
    {
        public string voen { get; set; }
        public List<Items> items { get; set; }

        public class Items
        {
            public string SupplierName { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string ProductBarcode { get; set; }

            public decimal PurchasePrice { get; set; }
            public decimal SalePrice { get; set; }
            public decimal StockQuantity { get; set; }
            public string UnitName { get; set; }
            public string TaxName { get; set; }
        }
    }
}
