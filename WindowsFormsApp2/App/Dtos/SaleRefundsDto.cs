using System;
using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class SaleRefundsDto
    {
        public string Voen { get; set; }
        public List<Items> items { get; set; }

        public class Items
        {
            public DateTime Date { get; set; }
            public string SaleProccessNo { get; set; }
            public string RefundProccessNo { get; set; }
            public string Username { get; set; }
            public string Supplier { get; set; }
            public string ProductName { get; set; }
            public decimal Quantity { get; set; }
            public string UnitName { get; set; }
            public decimal SalePrice { get; set; }
            public decimal DiscountAzn { get; set; }
            public string PaymentType { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal RefundQuantity { get; set; }
            public decimal RefundAmount { get; set; }
        }
    }
}
