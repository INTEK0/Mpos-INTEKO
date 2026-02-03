using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class PaymentTypesDto
    {
        public string voen { get; set; }
        public List<Items> items { get; set; }
        public class Items
        {
            public string Date { get; set; }
            public decimal Cash { get; set; }
            public decimal Card { get; set; }
            public decimal Bank { get; set; }
            public decimal Credit { get; set; }
        }
    }
}
