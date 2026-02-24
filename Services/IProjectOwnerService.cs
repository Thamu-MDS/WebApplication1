// Services/IProjectOwnerService.cs
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public interface IProjectOwnerService
    {
        Task<List<ProjectOwnerDto>> GetAllProjectOwnersAsync();
        Task<ProjectOwnerDto?> GetProjectOwnerByIdAsync(int id);
        Task<ProjectOwnerDto> CreateProjectOwnerAsync(ProjectOwnerDto projectOwnerDto);
        Task<ProjectOwnerDto> UpdateProjectOwnerAsync(int id, ProjectOwnerDto projectOwnerDto);
        Task<bool> DeleteProjectOwnerAsync(int id);
    }
}
