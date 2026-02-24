// Models/DTOs/ProjectOwnerDto.cs
namespace ConstructionManagementSystem.Models.DTOs
{
    public class ProjectOwnerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Contact { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
