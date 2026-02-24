// Data/Entities/Payment.cs - ✅ FIXED to match SQL schema exactly
using System.ComponentModel.DataAnnotations.Schema;

namespace ConstructionManagementSystem.Data.Entities
{
    public class Payment
    {
        public int id { get; set; }                           // ✅ lowercase - matches SQL
        public PaymentType type { get; set; }                 // ✅ lowercase - matches SQL ENUM
        public int? user_id { get; set; }                     // ✅ snake_case - matches SQL
        public User? User { get; set; }                       // ✅ Navigation property
        public int? material_id { get; set; }                 // ✅ snake_case - matches SQL
        public Material? Material { get; set; }               // ✅ Navigation property
        public int project_id { get; set; }                   // ✅ snake_case - required in SQL
        public Project? Project { get; set; }                 // ✅ Navigation property
        public DateTime payment_date { get; set; }            // ✅ snake_case - matches SQL
        public DateTime? due_date { get; set; }               // ✅ snake_case - matches SQL
        public decimal amount { get; set; }                   // ✅ lowercase - matches SQL
        public PaymentStatus status { get; set; } = PaymentStatus.Unpaid;  // ✅ lowercase
        public DateTime created_at { get; set; } = DateTime.UtcNow;       // ✅ snake_case

        [NotMapped]  // ✅ Computed - for business logic
        public bool IsOverdue => status == PaymentStatus.Unpaid && due_date.HasValue && due_date.Value < DateTime.Today;
    }

    public enum PaymentType
    {
        [System.ComponentModel.Description("Worker")]  // ✅ Matches SQL ENUM exactly
        Worker,
        [System.ComponentModel.Description("Material")] // ✅ Matches SQL ENUM exactly
        Material
    }

    public enum PaymentStatus
    {
        [System.ComponentModel.Description("Paid")]    // ✅ Matches SQL ENUM exactly
        Paid,
        [System.ComponentModel.Description("Unpaid")]  // ✅ Matches SQL ENUM exactly
        Unpaid,
        [System.ComponentModel.Description("Overdue")] // ✅ Matches SQL ENUM exactly
        Overdue
    }
}
