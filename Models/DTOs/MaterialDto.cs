// Models/DTOs/MaterialDto.cs
namespace ConstructionManagementSystem.Models.DTOs
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = string.Empty;
        public decimal PricePerUnit { get; set; }
        public decimal LowStockThreshold { get; set; }
        public decimal TotalValue => Quantity * PricePerUnit;
        public bool IsLowStock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
