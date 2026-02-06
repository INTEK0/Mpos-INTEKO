using System;
using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class SaleDetailsDto
    {
        public string voen { get; set; }
        public List<Items> items { get; set; }
        public class Items
        {
            public DateTime Date { get; set; }
            public string CashierName { get; set; }
            public string SupplierName { get; set; }
            public string CategoryName { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string Barcode { get; set; }
            public string CustomerName { get; set; }
            public string DoctorName { get; set; }
            public double Quantity { get; set; }
            public string UnitName { get; set; }
            public double SalePrice { get; set; }
            public double DiscountAzn { get; set; }
            public string PaymentType { get; set; }
            public double TotalAmount { get; set; }
            public int TaxPercantages { get; set; } //string olmalıdır
            public string ProccessNo { get; set; }
            public string ReceiptNo { get; set; }
        }
    }
}
