// Data/Entities/Supplier.cs - ✅ FIXED to match SQL schema exactly
namespace ConstructionManagementSystem.Data.Entities
{
    public class Supplier
    {
        public int id { get; set; }                           // ✅ lowercase - matches SQL
        public string name { get; set; } = string.Empty;      // ✅ lowercase - matches SQL
        public string? contact { get; set; }                  // ✅ lowercase - matches SQL
        public string? email { get; set; }                    // ✅ lowercase - matches SQL
        public DateTime created_at { get; set; } = DateTime.UtcNow;  // ✅ snake_case - matches SQL
    }
}
