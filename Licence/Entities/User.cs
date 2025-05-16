using System;

namespace Licence.Entities
{
    public class User
    {
        public bool IsActive { get; set; }
        public string LicenceKey { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Voen { get; set; }
        public string CompanyCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string ContractNo { get; set; }
        public DateTime RegisterDate { get; set; }
        public string TerminalModel { get; set; }
        public string TerminalSerialNumber { get; set; }
        public string LicenceVersion { get; set; }
        public string MposVersion { get; set; }
        public DateTime LicenceExpireDate { get; set; }
        public string Message { get; set; } = null;
    }
}