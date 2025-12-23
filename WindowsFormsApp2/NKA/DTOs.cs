using WindowsFormsApp2.Helpers;
using WindowsFormsApp2.Helpers.DB;
using static WindowsFormsApp2.Helpers.DB.DatabaseClasses;

public static class DTOs
{
    public class SalesDto
    {
        public string DocumentUUID { get; set; }
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
        public string CustomerNameManual { get; set; } = null;
        public int? CustomerId { get; set; } = null;
    }

    public class RefundDto
    {
        public string IpAddress { get; set; }
        public string DocumentUUID { get; set; }
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

    public class DepositDto
    {
        public string IpAddress { get; set; }
        public string AccessToken { get; set; }
        public string Cashier { get; set; }
        public decimal Sum { get; set; }
    }

    public class WithdrawDto
    {
        public string IpAddress { get; set; }
        public string AccessToken { get; set; }
        public string Cashier { get; set; }
        public decimal Sum { get; set; }
    }

    public class CreditSaleDto
    {
        public class Item
        {
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public decimal Quantity { get; set; }
            public decimal SalePrice { get; set; }

            public decimal Total
            {
                get
                {
                    return SalePrice * Quantity;
                }
            }

            public int VatType { get; set; }
            public int QuantityType { get; set; }
        }
        public Item item { get; set; }
        public decimal Total
        {
            get { return CashPayment + CardPayment; }
        }
        public string DocumentUUID { get; set; }
        public decimal CashPayment { get; set; } = 0;
        public decimal CardPayment { get; set; } = 0;
        public decimal creditPayment { get; set; }
        public decimal IncomingSum { get; set; }
        public string Cashier { get; set; }
        public string CreditContract { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public string Rrn { get; set; }
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
    }

    public class CreditPayDto
    {
        public int CreditMonthId { get; set; } //KREDIT_SATISI_AYLIK_ID
        public class Item
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public decimal Quantity { get; set; }
            public decimal SalePrice { get; set; }
            public decimal Total
            {
                get
                {
                    return SalePrice * Quantity;
                }
            }
            public int VatType { get; set; }
            public int quantityType { get; set; }
        };
        public Item item { get; set; }
        public string documentUUID { get; set; }
        public string ParenDocumentId { get; set; }
        public decimal Residue { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CardPayment { get; set; }
        public decimal Total
        {
            get { return CashPayment + CardPayment; }
        }
        public string CashierName { get; } = DbProsedures.GetUser().NameSurname;
        public string CreditContract { get; set; }
        public string CustomerName { get; set; }
        public int paymentNumber { get; set; } //Hansı ayın ödənişi olduğu göndərilir.
        public decimal IncomingSum { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
        public string Rrn { get; set; }
    }

    public class CreditSaleRefundDto
    {
        public class Item
        {
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public decimal Quantity { get; set; }
            public decimal SalePrice { get; set; }

            public decimal Total
            {
                get
                {
                    return SalePrice * Quantity;
                }
            }

            public int VatType { get; set; }
            public int QuantityType { get; set; }
        }
        public Item item { get; set; }
        public string DocumentUUID { get; set; }
        public string ParentLongFiscalId { get; set; }
        public string ParentShortFiscalId { get; set; }
        public string ParentDocumentNumber { get; set; }
        /// <summary>
        /// 0-Nisyə, 1-Nağd, 2-Kart, 3-Nağd-Kart
        /// </summary>
        public short PaymentTypeId { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CardPayment { get; set; }
        public decimal Total { get; set; }
        public decimal creditPayment { get; set; }
        public decimal IncomingSum { get; set; }
        public string Cashier { get; set; }
        public string CreditContract { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public string Rrn { get; set; }
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
    }

    public class CreditPayRefundDto
    {
        public class Item
        {
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public decimal Quantity { get; set; }
            public decimal SalePrice { get; set; }

            public decimal Total
            {
                get
                {
                    return SalePrice * Quantity;
                }
            }

            public int VatType { get; set; }
            public int QuantityType { get; set; }
        }
        public Item item { get; set; }
        public string DocumentUUID { get; set; }
        public string ParentLongFiscalId { get; set; }
        public string ParentShortFiscalId { get; set; }
        public string ParentDocumentNumber { get; set; }
        /// <summary>
        /// 0-Nisyə, 1-Nağd, 2-Kart, 3-Nağd-Kart
        /// </summary>
        public short PaymentTypeId { get; set; }
        public decimal CashPayment { get; set; }
        public decimal CardPayment { get; set; }
        public decimal Total { get; set; }
        public decimal IncomingSum { get; set; }
        public string Cashier { get; set; }
        public string CustomerName { get; set; }
        public string Note { get; set; }
        public string Url { get; set; }
        public string Rrn { get; set; }
        public string AccessToken { get; set; }
        public string MerchantId { get; set; }
    }
}