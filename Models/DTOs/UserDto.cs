// Models/DTOs/UserDto.cs
namespace ConstructionManagementSystem.Models.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public int? AssignedProjectId { get; set; }
        public string? ProjectName { get; set; }
        public decimal? Salary { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
