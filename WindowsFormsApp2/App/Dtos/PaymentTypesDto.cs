using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class PaymentTypesDto
    {
        public string Voen { get; set; }
        public List<Items> items { get; set; }
        public class Items
        {
            public string Date { get; set; }
            public double Cash { get; set; }
            public double Card { get; set; }
            public double Bank { get; set; }
            public double Credit { get; set; }
        }
    }
}
