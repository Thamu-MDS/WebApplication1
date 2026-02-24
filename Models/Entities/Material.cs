// Data/Entities/Material.cs - ✅ FIXED to match SQL schema exactly
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionManagementSystem.Data.Entities
{
    public class Material
    {
        public int id { get; set; }                           // ✅ lowercase - matches SQL
        public string name { get; set; } = string.Empty;      // ✅ lowercase - matches SQL
        public int? supplier_id { get; set; }                 // ✅ snake_case - matches SQL
        public Supplier? Supplier { get; set; }               // ✅ Navigation property (PascalCase)
        public decimal quantity { get; set; }                 // ✅ lowercase - matches SQL
        public string unit { get; set; } = string.Empty;      // ✅ lowercase - matches SQL
        public decimal price_per_unit { get; set; }           // ✅ snake_case - matches SQL
        public decimal low_stock_threshold { get; set; } = 10; // ✅ snake_case - matches SQL
        public DateTime created_at { get; set; } = DateTime.UtcNow; // ✅ snake_case - matches SQL

        [NotMapped]  // ✅ Computed property for convenience
        public decimal TotalValue => quantity * price_per_unit;

        [NotMapped]  // ✅ Computed property for stock status
        public bool IsLowStock => quantity <= low_stock_threshold;
    }
}
