using System.Collections.Generic;

namespace WindowsFormsApp2.App.Dtos
{
    public class InvoiceProductDto
    {
        public string voen { get; set; }
        public List<Row> rows { get; set; }

        public class Row
        {
            public string tarix { get; set; }
            public string istifadeciAdi { get; set; }
            public string fakturaNo { get; set; }
            public string techizatciAdi { get; set; }
            public string mehsulAdi { get; set; }
            public string mehsulKodu { get; set; }
            public double miqdari { get; set; }
            public double alisQiymeti { get; set; }
            public int endirimFaiz { get; set; }
            public double endirimAzn { get; set; }
            public double endirimMeblegi { get; set; }
            public double odenilecekMebleg { get; set; }
        }
    }
}
