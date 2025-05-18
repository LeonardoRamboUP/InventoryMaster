namespace InventoryMaster.Models
{
    // Modelo que representa um produto no sistema
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}