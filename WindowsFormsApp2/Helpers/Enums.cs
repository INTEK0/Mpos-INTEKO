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
            PreCashCard,
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
            [Description("KİÇİK")]
            minimum = 0,
            [Description("ORTA")]
            medium,
            [Description("ÜFÜQİ (Geyim mağazası üçün)")]
            maximum
        }

        public enum SelectedDataType
        {
            [Description("MÜŞTƏRİ")]
            Customer,
            [Description("ZAMİN")]
            Guarantor,
            [Description("HƏKİM")]
            Doctor
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
            ExcelImport_Direct
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}
