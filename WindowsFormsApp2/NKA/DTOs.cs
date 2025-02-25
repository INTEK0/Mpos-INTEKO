using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;
using WindowsFormsApp2.Helpers;
using static WindowsFormsApp2.Helpers.Enums;

public static class DTOs
{
    public class SalesDto
    {
        public string IpAddress { get; set; }
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
        public string ProccessNo { get; set; }
        public decimal Cash { get; set; }
        public decimal Card { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public decimal IncomingSum { get; set; } //Ödənilən
        public decimal Balance { get; set; } //qalıq
        public string Cashier { get; set; }
        public Customer Customer { get; set; }
        public Doctor Doctor { get; set; }
        public string Rrn { get; set; } = string.Empty;
        public Enums.PayType PayType { get; set; }
        public string FiscalId { get; set; } = null;
        public decimal PrepaymentPay { get; set; } //Avans ödənişləri üçün
    }

    public class RefundDto
    {
        public string IpAddress { get; set; }
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
        public string ProccessNo { get; set; }
        public decimal Cash { get; set; }
        public decimal Card { get; set; }
        public decimal Total { get; set; }
        public string Cashier { get; set; }
        public string Rrn { get; set; } = string.Empty;
        public Enums.PayType PayType { get; set; }
    }
}