// Services/SupplierService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SupplierDto>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                .Select(s => new SupplierDto
                {
                    Id = s.id,           // ✅ snake_case fixed
                    Name = s.name,       // ✅ snake_case fixed
                    Contact = s.contact, // ✅ snake_case fixed
                    Email = s.email,      // ✅ snake_case fixed
                    CreatedAt = s.created_at  // ✅ snake_case fixed
                })
                .ToListAsync();
        }

        public async Task<SupplierDto?> GetSupplierByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return null;

            return new SupplierDto
            {
                Id = supplier.id,           // ✅ snake_case fixed
                Name = supplier.name,       // ✅ snake_case fixed
                Contact = supplier.contact, // ✅ snake_case fixed
                Email = supplier.email,      // ✅ snake_case fixed
                CreatedAt = supplier.created_at  // ✅ snake_case fixed
            };
        }

        public async Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto)
        {
            var supplier = new Supplier
            {
                name = supplierDto.Name,     // ✅ snake_case fixed
                contact = supplierDto.Contact, // ✅ snake_case fixed
                email = supplierDto.Email    // ✅ snake_case fixed
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            supplierDto.Id = supplier.id;           // ✅ snake_case fixed
            supplierDto.CreatedAt = supplier.created_at;  // ✅ snake_case fixed
            return supplierDto;
        }

        public async Task<SupplierDto> UpdateSupplierAsync(int id, SupplierDto supplierDto)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) throw new Exception("Supplier not found");

            supplier.name = supplierDto.Name;       // ✅ snake_case fixed
            supplier.contact = supplierDto.Contact; // ✅ snake_case fixed
            supplier.email = supplierDto.Email;     // ✅ snake_case fixed

            await _context.SaveChangesAsync();
            supplierDto.Id = id;
            return supplierDto;
        }

        public async Task<bool> DeleteSupplierAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null) return false;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
