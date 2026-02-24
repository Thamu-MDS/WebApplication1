// Data/Entities/User.cs - ✅ FIXED to match SQL schema exactly
using ConstructionManagementSystem.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Entities;

namespace ConstructionManagementSystem.Data.Entities
{
    public class User
    {
        public int id { get; set; }                           // ✅ lowercase - matches SQL
        public string full_name { get; set; } = string.Empty; // ✅ snake_case - matches SQL
        public string? email { get; set; }                    // ✅ lowercase - matches SQL
        public string? password_hash { get; set; }            // ✅ snake_case - matches SQL
        public string? contact { get; set; }                  // ✅ lowercase - matches SQL
        public int role_id { get; set; }                      // ✅ snake_case - matches SQL
        public Role? Role { get; set; }                       // ✅ Navigation property
        public int? assigned_project_id { get; set; }         // ✅ snake_case - matches SQL
        public Project? AssignedProject { get; set; }         // ✅ Navigation property
        public decimal? salary { get; set; }                  // ✅ lowercase - matches SQL
        public bool is_active { get; set; } = true;           // ✅ snake_case - matches SQL
        public DateTime created_at { get; set; } = DateTime.UtcNow; // ✅ snake_case - matches SQL

        [NotMapped]  // ✅ Computed property
        public string DisplayName => $"{full_name} ({Role?.name ?? "No Role"})";
    }
}
