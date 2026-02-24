// Services/MaterialService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly ApplicationDbContext _context;

        public MaterialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MaterialDto>> GetAllMaterialsAsync()
        {
            return await _context.Materials
                .Select(m => new MaterialDto  // ✅ Removed Include - use projection
                {
                    Id = m.id,
                    Name = m.name,
                    SupplierId = m.supplier_id,
                    SupplierName = _context.Suppliers  // ✅ NULL-SAFE projection
                        .Where(s => s.id == m.supplier_id)
                        .Select(s => s.name)
                        .FirstOrDefault() ?? "N/A",
                    Quantity = m.quantity,
                    Unit = m.unit,
                    PricePerUnit = m.price_per_unit,
                    LowStockThreshold = m.low_stock_threshold,
                    IsLowStock = m.quantity <= m.low_stock_threshold,
                    CreatedAt = m.created_at
                })
                .ToListAsync();
        }

        public async Task<MaterialDto?> GetMaterialByIdAsync(int id)
        {
            var material = await _context.Materials
                .FirstOrDefaultAsync(m => m.id == id);

            if (material == null) return null;

            return new MaterialDto
            {
                Id = material.id,
                Name = material.name,
                SupplierId = material.supplier_id,
                SupplierName = material.Supplier != null ? material.Supplier.name : "N/A",  // ✅ NULL-SAFE
                Quantity = material.quantity,
                Unit = material.unit,
                PricePerUnit = material.price_per_unit,
                LowStockThreshold = material.low_stock_threshold,
                IsLowStock = material.quantity <= material.low_stock_threshold,
                CreatedAt = material.created_at
            };
        }

        public async Task<MaterialDto> CreateMaterialAsync(MaterialDto materialDto)
        {
            var material = new Material
            {
                name = materialDto.Name,
                supplier_id = materialDto.SupplierId,
                quantity = materialDto.Quantity,
                unit = materialDto.Unit,
                price_per_unit = materialDto.PricePerUnit,
                low_stock_threshold = materialDto.LowStockThreshold
            };

            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            materialDto.Id = material.id;
            materialDto.CreatedAt = material.created_at;
            return materialDto;
        }

        public async Task<MaterialDto> UpdateMaterialAsync(int id, MaterialDto materialDto)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) throw new Exception("Material not found");

            material.name = materialDto.Name;
            material.supplier_id = materialDto.SupplierId;
            material.quantity = materialDto.Quantity;
            material.unit = materialDto.Unit;
            material.price_per_unit = materialDto.PricePerUnit;
            material.low_stock_threshold = materialDto.LowStockThreshold;

            await _context.SaveChangesAsync();
            materialDto.Id = id;
            return materialDto;
        }

        public async Task<bool> DeleteMaterialAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null) return false;

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<MaterialDto>> GetLowStockMaterialsAsync()
        {
            return await _context.Materials
                .Where(m => m.quantity <= m.low_stock_threshold)  // ✅ snake_case properties
                .Select(m => new MaterialDto
                {
                    Id = m.id,
                    Name = m.name,
                    SupplierId = m.supplier_id,
                    SupplierName = _context.Suppliers  // ✅ NULL-SAFE projection
                        .Where(s => s.id == m.supplier_id)
                        .Select(s => s.name)
                        .FirstOrDefault() ?? "N/A",
                    Quantity = m.quantity,
                    Unit = m.unit,
                    PricePerUnit = m.price_per_unit,
                    LowStockThreshold = m.low_stock_threshold,
                    IsLowStock = true  // ✅ Always true for low stock query
                })
                .ToListAsync();
        }
    }
}
