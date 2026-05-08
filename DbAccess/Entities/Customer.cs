using System;

namespace DbAccess.Entities
{
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
        public decimal Debt { get; set; }
        public int IsDeleted { get; set; }
    }
}
