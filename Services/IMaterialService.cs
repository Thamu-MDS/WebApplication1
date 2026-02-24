// Services/IMaterialService.cs
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public interface IMaterialService
    {
        Task<List<MaterialDto>> GetAllMaterialsAsync();
        Task<MaterialDto?> GetMaterialByIdAsync(int id);
        Task<MaterialDto> CreateMaterialAsync(MaterialDto materialDto);
        Task<MaterialDto> UpdateMaterialAsync(int id, MaterialDto materialDto);
        Task<bool> DeleteMaterialAsync(int id);
        Task<List<MaterialDto>> GetLowStockMaterialsAsync();
    }
}
