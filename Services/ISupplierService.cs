// Services/ISupplierService.cs
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetAllSuppliersAsync();
        Task<SupplierDto?> GetSupplierByIdAsync(int id);
        Task<SupplierDto> CreateSupplierAsync(SupplierDto supplierDto);
        Task<SupplierDto> UpdateSupplierAsync(int id, SupplierDto supplierDto);
        Task<bool> DeleteSupplierAsync(int id);
    }
}
