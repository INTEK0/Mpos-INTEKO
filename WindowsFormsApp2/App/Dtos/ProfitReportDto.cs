using System;
using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class ProfitReportDto
    {
        public string Voen { get; set; }
        public List<Items> items { get; set; }
        public class Items
        {
            public int Count { get; set; }
            public DateTime Date { get; set; }
            public string Username { get; set; }
            public string ProccessNo { get; set; }
            public string SupplierName { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public decimal Quantity { get; set; }
            public string UnitName { get; set; }
            public decimal SaleQuantity { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal SalePrice { get; set; }
            public decimal TotalPurchaseAmount { get; set; } //Toplam alış qiyməti
            public decimal TotalSaleAmount { get; set; }  //Toplam satış qiyməti
            public decimal ProfitAmount { get; set; } //Mənfəət
        }
    }
}
