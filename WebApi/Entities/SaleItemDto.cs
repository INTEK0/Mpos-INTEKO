namespace WebApi.Entities
{
    public class SaleItemDto
    {
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public int Quantity { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int UnitType { get; set; }
        public int TaxRate { get; set; }
    }
}