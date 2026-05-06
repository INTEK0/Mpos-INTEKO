using System;

namespace WebApi.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Voen { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public DateTime DateBirth { get; set; }
        public string Email { get; set; }
        public string MobPhone { get; set; }
        public int IsDeleted { get; set; }
    }
}