// Models/DTOs/SupplierDto.cs
namespace ConstructionManagementSystem.Models.DTOs
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
