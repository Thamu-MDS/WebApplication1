// Services/IProjectService.cs
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto);
        Task<ProjectDto> UpdateProjectAsync(int id, ProjectDto projectDto);
        Task<bool> DeleteProjectAsync(int id);
    }
}
