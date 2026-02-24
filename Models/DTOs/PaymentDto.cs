// Models/DTOs/PaymentDto.cs
namespace ConstructionManagementSystem.Models.DTOs
{
    public class PaymentDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public int? MaterialId { get; set; }
        public string? MaterialName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
