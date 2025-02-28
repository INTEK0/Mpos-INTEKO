using System;

namespace WindowsFormsApp2.Helpers.DB
{
    public class DatabaseClasses
    {
        public class Company
        {
            public string CompanyName { get; set; }
            public string Voen { get; set; }
            public string CompanyCode { get; set; }
            public string Address { get; set; }
            public string Phone { get; set; }
            public string WebSite { get; set; }
            public string Email { get; set; }
            public string User { get; set; }
            public DateTime DateRegister { get; set; }
            public string AccountNumber { get; set; }
            public string BankName { get; set; }
            public string BankVoen { get; set; }
            public string BankCode { get; set; }
            public string MH { get; set; }
            public string SWIFT { get; set; }
        }

        public class Customer
        {
            public int CustomerID { get; set; }
            public string ProccessNo { get; set; }
            public string CompanyName { get; set; }
            public string Voen { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string FatherName { get; set; }
            public DateTime DateBirth { get; set; }
            public string SvNo { get; set; }
            public string FinCode { get; set; }
            public string Address { get; set; }
            public string ResidentialAddress { get; set; }
            public DateTime SV_Start { get; set; }
            public DateTime SV_End { get; set; }
            public string Gender { get; set; }
            public string Nation { get; set; }
            public string Email { get; set; }
            public string MobPhone { get; set; }
            public string HomePhone { get; set; }
            public string Comment { get; set; }
            public string BankName { get; set; }
            public string BankVoen { get; set; }
            public string BankAccountNumber { get; set; }
            public string BankCode { get; set; }
            public string BankSwift { get; set; }
            public int IsDeleted { get; set; }
        }

        public class Guarantor
        {
            public int ID { get; set; }
            public string ProccessNo { get; set; }
            public string CompanyName { get; set; }
            public string Voen { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string FatherName { get; set; }
            public DateTime DateBirth { get; set; }
            public string SvNo { get; set; }
            public string FinCode { get; set; }
            public string Address { get; set; }
            public string ResidentialAddress { get; set; }
            public DateTime SV_Start { get; set; }
            public DateTime SV_End { get; set; }
            public string Gender { get; set; }
            public string Nation { get; set; }
            public string Email { get; set; }
            public string MobPhone { get; set; }
            public string HomePhone { get; set; }
            public string Comment { get; set; }
            public string BankName { get; set; }
            public string BankVoen { get; set; }
            public string BankAccountNumber { get; set; }
            public string BankCode { get; set; }
            public string BankSwift { get; set; }
            public int IsDeleted { get; set; }
        }

        public class Supplier
        {
            public int SupplierID { get; set; }
            public string ProccessNo { get; set; }
            public string SupplierName { get; set; }
            public string Voen { get; set; }
            public string Address { get; set; }
            public int BorcTeyinati { get; set; }
            public string ContractNo { get; set; }
            public decimal Debt { get; set; } //Borc
            public string MobPhone { get; set; }
            public string Email { get; set; }
            public string Comment { get; set; }
            public string BankName { get; set; }
            public string BankVoen { get; set; }
            public string BankAccountNumber { get; set; }
            public string BankCode { get; set; }
            public string BankSwift { get; set; }
            public int IsDeleted { get; set; }
        }

        public class Header
        {
            public decimal cash { get; set; }
            public decimal card { get; set; }
            public decimal bonus { get; set; } = 0;
            public decimal? paidPayment { get; set; } = null;
            public string CustomerName { get; set; }
            public Enums.PayType PayType { get; set; }
        }

        public class Item
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public decimal Quantity { get; set; }
            public decimal SalePrice { get; set; }
            public decimal PurchasePrice { get; set; }
            public int vatType { get; set; }
            public int QuantityType { get; set; }
            public int ProductId { get; set; }
        }

        public class Calculation
        {
            public string proccessNo { get; set; }
            public int ProductID { get; set; }
            public string Barcode { get; set; }
            public string ProductName { get; set; }
            public decimal SalePrice { get; set; }
            public decimal PurchasePrice { get; set; }
        }

        public class PosSales
        {
            public string posNomre { get; set; }
            public string longFiskalId { get; set; }
            public string proccessNo { get; set; }
            public decimal cash { get; set; }
            public decimal card { get; set; }
            public decimal total { get; set; }
            public string json { get; set; }
            public string shortFiskalId { get; set; }
            public string rrn { get; set; } = null;
            public Nullable<int> customerId { get; set; }
            public Nullable<int> doctorId { get; set; }
            public decimal Prepayment { get; set; } = 0; // Avans satışı
        }

        public class PosRefund
        {
            public string proccessNo { get; set; }
            public int pos_satis_check_main_id { get; set; }
            public int pos_satis_check_details_id { get; set; }
            public decimal quantity { get; set; }
            public string comment { get; set; }
        }

        public class Categories
        {
            public int KATEGORIYA_ID { get; set; }
            public string KATEGORIYA { get; set; }
        }

        public class ProductsMain
        {
            public string FakturaNo { get; set; }
            public string SupplierName { get; set; }
            public int SupplierId { get; set; }
            public DateTime? Date { get; set; }
            public string PaymentType { get; set; }
            public string ProccessNo { get; set; }
            public string Status { get; set; }
        }

        public class ProductsDetail
        {
            public int ProductId { get; set; }
            public int ProductMainId { get; set; }
            public string SupplierName { get; set; } = null; //Məhsul silinməsi üçün nəzərdə tutulub
            public string CategoryName { get; set; }
            public string Barocde { get; set; }
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string WarehouseName { get; set; }
            public decimal Quantity { get; set; } = 0;
            public string UnitName { get; set; }
            public string TaxName { get; set; }
            public string CurrencyName { get; set; }
            public decimal PurchasePrice { get; set; } = 0;
            public decimal SalePrice { get; set; } = 0;
            public string DiscountPercent { get; set; }
            public string DiscountAZN { get; set; }
            public string DiscountAmount { get; set; }
            public string TotalAmount { get; set; }
            public string IstehsalTarixi { get; set; } = null;
            public string BitisTarixi { get; set; } = null;
            public string XeberdarEt { get; set; }
            public int IsDeleted { get; set; }
        }

        public class Products
        {
            public int Id { get; set; }
            public int SupplierId { get; set; }
            public string WarehouseName { get; set; }
            public string CategoryName { get; set; }
            public decimal Quantity { get; set; }
            public int CurrencyId { get; set; }
            public string ProductName { get; set; }
            public string Barocde { get; set; }
            public string ProductCode { get; set; }
            public decimal PurchasePrice { get; set; }
            public decimal SalePrice { get; set; }
            public int UnitId { get; set; }
            public int TaxId { get; set; }
        }

        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool IsAdmin { get; set; }
            public string NameSurname { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string SvNo { get; set; }
            public string Address { get; set; }
            public DateTime DateBirth { get; set; }
            public string BloodType { get; set; }
        }

        public class Doctor
        {
            public int Id { get; set; }
            public string ProccessNo { get; set; }
            public string NameSurname { get; set; }
            public string Position { get; set; }
            public DateTime DateBirth { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Gender { get; set; }
            public int IsDeleted { get; set; }
        }

        public class OperationLogs
        {
            public Enums.OperationType OperationType { get; set; }
            public int OperationId { get; set; }
            public string Message { get; set; } = string.Empty;
            public string RequestCode { get; set; } = string.Empty;
            public string ResponseCode { get; set; } = string.Empty;

        }

        public class  GaimeMain
        {
            public string ProccessNo { get; set; }
            public string QaimeNomre { get; set; }
            public DateTime Date { get; set; }
            public string TotalPaid { get; set; }
            public string PaymentType { get; set; }
            public int CustomerId { get; set; }
            public string Customer { get; set; }
            public string Edvsiz { get; set; }
            public string Edvli { get; set; }
        }
    }
}