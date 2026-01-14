using System;

namespace WindowsFormsApp2.App.Dtos
{
    public class PosSaleDto
    {
        public string Voen { get; set; }
        public int PosSaleId { get; set; }
        public string ReceiptNo { get; set; }
        public string ShortFiscalId { get; set; }
        public DateTime SaleDate { get; set; }
        public int UserId { get; set; }
        public string ProccessNo { get; set; }
        public decimal Cash { get; set; }
        public decimal Card { get; set; }
        public decimal TotalAmount { get; set; }
        public string BankRRN { get; set; }
        public string BankTransactionId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string DoctorName { get; set; }
        public int DoctorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
