// Data/Entities/Project.cs - ✅ FIXED to match SQL schema exactly
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionManagementSystem.Data.Entities
{
    public class Project
    {
        public int id { get; set; }                    // ✅ lowercase - matches SQL
        public string name { get; set; } = string.Empty;     // ✅ lowercase - matches SQL
        public string? location { get; set; }          // ✅ lowercase - matches SQL
        public int owner_id { get; set; }              // ✅ snake_case - matches SQL
        public ProjectOwner? Owner { get; set; }       // ✅ Navigation property (PascalCase)
        public DateTime? start_date { get; set; }      // ✅ snake_case - matches SQL
        public DateTime? end_date { get; set; }        // ✅ snake_case - matches SQL
        public decimal budget { get; set; }            // ✅ lowercase - matches SQL
        public ProjectStatus status { get; set; } = ProjectStatus.Planning;  // ✅ lowercase
        public DateTime created_at { get; set; } = DateTime.UtcNow;  // ✅ snake_case

        [NotMapped]  // ✅ Not in SQL table - only for enum conversion
        public string StatusString => status.ToString();
    }

    public enum ProjectStatus
    {
        Planning,
        [System.ComponentModel.Description("In Progress")]  // ✅ Matches SQL enum value
        InProgress,
        Completed
    }
}
