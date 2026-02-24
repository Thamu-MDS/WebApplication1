// =========================================
// FIXED ApplicationDbContext.cs
// =========================================

using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data.Entities;
using WebApplication1.Models.Entities;

namespace ConstructionManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectOwner> ProjectOwners { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Roles - Match SQL: id, name
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.id);  // ✅ lowercase to match SQL
                entity.Property(e => e.name).IsRequired().HasMaxLength(50).HasColumnName("name");
                entity.ToTable("roles");
            });

            // Project Owners - Match SQL: id, name, contact, email, created_at
            modelBuilder.Entity<ProjectOwner>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired().HasMaxLength(150).HasColumnName("name");
                entity.Property(e => e.contact).HasMaxLength(50).HasColumnName("contact");
                entity.Property(e => e.email).HasMaxLength(120).HasColumnName("email");
                entity.Property(e => e.created_at).HasColumnName("created_at");
                entity.ToTable("project_owners");
            });

            // Projects - Match SQL exactly
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired().HasMaxLength(150).HasColumnName("name");
                entity.Property(e => e.location).HasMaxLength(150).HasColumnName("location");
                entity.Property(e => e.owner_id).IsRequired().HasColumnName("owner_id");
                entity.Property(e => e.start_date).HasColumnName("start_date");
                entity.Property(e => e.end_date).HasColumnName("end_date");
                entity.Property(e => e.budget).HasColumnType("decimal(15,2)").IsRequired().HasColumnName("budget");
                entity.Property(e => e.status).HasColumnName("status");
                entity.Property(e => e.created_at).HasColumnName("created_at");
                entity.ToTable("projects");
                
                entity.HasOne<ProjectOwner>()
                      .WithMany()
                      .HasForeignKey(p => p.owner_id)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("projects_ibfk_1");
            });

            // Users - Match SQL exactly
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.full_name).IsRequired().HasMaxLength(100).HasColumnName("full_name");
                entity.Property(e => e.email).HasMaxLength(120).HasColumnName("email");
                entity.Property(e => e.password_hash).HasMaxLength(255).HasColumnName("password_hash");
                entity.Property(e => e.contact).HasMaxLength(50).HasColumnName("contact");
                entity.Property(e => e.role_id).IsRequired().HasColumnName("role_id");
                entity.Property(e => e.assigned_project_id).HasColumnName("assigned_project_id");
                entity.Property(e => e.salary).HasColumnType("decimal(10,2)").HasColumnName("salary");
                entity.Property(e => e.is_active).HasColumnName("is_active");
                entity.Property(e => e.created_at).HasColumnName("created_at");
                entity.ToTable("users");
                
                entity.HasOne<Role>()
                      .WithMany()
                      .HasForeignKey(u => u.role_id)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("users_ibfk_1");
                      
                entity.HasOne<Project>()
                      .WithMany()
                      .HasForeignKey(u => u.assigned_project_id)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("users_ibfk_2");
            });

            // Suppliers
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired().HasMaxLength(150).HasColumnName("name");
                entity.Property(e => e.contact).HasMaxLength(50).HasColumnName("contact");
                entity.Property(e => e.email).HasMaxLength(120).HasColumnName("email");
                entity.Property(e => e.created_at).HasColumnName("created_at");
                entity.ToTable("suppliers");
            });

            // Materials
            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.name).IsRequired().HasMaxLength(150).HasColumnName("name");
                entity.Property(e => e.supplier_id).HasColumnName("supplier_id");
                entity.Property(e => e.quantity).HasColumnType("decimal(10,2)").IsRequired().HasColumnName("quantity");
                entity.Property(e => e.unit).HasMaxLength(50).HasColumnName("unit");
                entity.Property(e => e.price_per_unit).HasColumnType("decimal(10,2)").IsRequired().HasColumnName("price_per_unit");
                entity.Property(e => e.low_stock_threshold).HasColumnType("decimal(10,2)").HasDefaultValue(10).HasColumnName("low_stock_threshold");
                entity.Property(e => e.created_at).HasColumnName("created_at");
                entity.ToTable("materials");
                
                entity.HasOne<Supplier>()
                      .WithMany()
                      .HasForeignKey(m => m.supplier_id)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("materials_ibfk_1");
            });

            // Payments - ✅ FIXED: Exact SQL column mapping
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.Property(e => e.type).HasColumnName("type").HasConversion<string>();
                entity.Property(e => e.user_id).HasColumnName("user_id");
                entity.Property(e => e.material_id).HasColumnName("material_id");
                entity.Property(e => e.project_id).IsRequired().HasColumnName("project_id");
                entity.Property(e => e.payment_date).IsRequired().HasColumnName("payment_date");
                entity.Property(e => e.due_date).HasColumnName("due_date");
                entity.Property(e => e.amount).HasColumnType("decimal(15,2)").IsRequired().HasColumnName("amount");
                entity.Property(e => e.status).HasColumnName("status").HasConversion<string>();
                entity.Property(e => e.created_at).HasColumnName("created_at");
                entity.ToTable("payments");
                
                // Foreign Keys - Match exact SQL names
                entity.HasOne<User>()
                      .WithMany()
                      .HasForeignKey(p => p.user_id)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("payments_ibfk_1");
                      
                entity.HasOne<Material>()
                      .WithMany()
                      .HasForeignKey(p => p.material_id)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("payments_ibfk_2");
                      
                entity.HasOne<Project>()
                      .WithMany()
                      .HasForeignKey(p => p.project_id)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("payments_ibfk_3");

                // ✅ REMOVED CheckConstraint - MySQL EF Core Issue Fixed
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
