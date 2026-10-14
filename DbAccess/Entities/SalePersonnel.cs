using System;

namespace DbAccess.Entities
{
    public class SalePersonnel
    {
        public string FullName { get; set; }
        public DateTime? DateBirth { get; set; }
        public decimal BonusPercantage { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
