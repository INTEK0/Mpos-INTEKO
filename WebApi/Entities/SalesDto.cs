using System.Collections.Generic;

namespace WebApi.Entities
{
    public class SalesDto
    {
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public List<SaleItemDto> Items { get; set; }
        public string CashierName { get; set; } = "Admin";
    }
}