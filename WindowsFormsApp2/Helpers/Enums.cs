using System;
using System.ComponentModel;
using System.Reflection;

namespace WindowsFormsApp2.Helpers
{
    public static class Enums
    {
        public enum MessageType
        {
            Success,
            Warning,
            Error,
            Info
        }

        public enum PayType
        {
            Empty,
            Cash,
            Card,
            CashCard,
            Prepayment,
            Installment,
            OtherPay
        }

        public enum HeaderMessage
        {
            Mesaj,
            Xəta,
            Xəbərdarlıq,
            Bildiriş
        }

        public enum Operation
        {
            [Description("DAXİL ET")]
            Add,
            [Description("DÜZƏLİŞ ET")]
            Update,
            [Description("SİL")]
            Delete,
        }

        public enum PrintType
        {
            [Description("30x20")]
            minimum = 0,
            [Description("60x40")]
            medium,
            [Description("ÜFÜQİ (Geyim mağazası üçün)")]
            maximum
        }

        public enum BarcodeType
        {
            [Description("EAN13")]
            EAN13,
            [Description("128")]
            Code128
        }

        public enum SelectedDataType
        {
            [Description("MÜŞTƏRİ")]
            Customer,
            [Description("ZAMİN")]
            Guarantor,
            [Description("HƏKİM")]
            Doctor,
            [Description("GƏLİR")]
            Income,
            [Description("XƏRC")]
            Expense,
            [Description("TƏCHİZATÇI")]
            Supplier
        }

        public enum OperationType
        {
            [Description("Məhsul alışı")]
            AddProduct = 1,
            [Description("Məhsul alışı qaytarma")]
            RefundProduct,
            [Description("Pos satış")]
            PosSales,
            [Description("Pos satış qaytarma")]
            RefundPosSales,
            [Description("Qaimə satış")]
            QaimeSales,
            [Description("Qaimə satış qaytarma")]
            RefundQaimeSales,
            [Description("Exceli kontrollu əlavə et")]
            ExcelImport_Control,
            [Description("Exceli birbaşa əlavə et")]
            ExcelImport_Direct,
            [Description("Z-Hesabat (Gün sonu)")]
            ZReport,
            [Description("Nəzarət lenti")]
            ControlTape,
            [Description("Kredit satışı")]
            CreditSale,
            [Description("Kredit ödənişi")]
            CreditPay
        }

        public enum BankType
        {
            [Description("YOXDUR")]
            NONE,
            [Description("KAPİTAL BANK")]
            KAPITAL,
            [Description("PAŞA BANK")]
            PASHA,
            [Description("ABB BANK")]
            ABB,
        }

        public enum CustomerDebtType
        {
            AvansPay = 1,
            AvansSale,
            CreditSale,
            CreditPay
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
