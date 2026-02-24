// Services/ProjectService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectDto>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Select(p => new ProjectDto  // ✅ Projection - NULL SAFE, no Include needed
                {
                    Id = p.id,
                    Name = p.name,
                    Location = p.location,
                    OwnerId = p.owner_id,
                    OwnerName = _context.ProjectOwners
                        .Where(po => po.id == p.owner_id)
                        .Select(po => po.name)
                        .FirstOrDefault() ?? "N/A",
                    StartDate = p.start_date,
                    EndDate = p.end_date,
                    Budget = p.budget,
                    Status = p.status.ToString(),
                    CreatedAt = p.created_at
                })
                .ToListAsync();
        }

        public async Task<ProjectDto?> GetProjectByIdAsync(int id)
        {
            var project = await _context.Projects
                .FirstOrDefaultAsync(p => p.id == id);

            if (project == null) return null;

            return new ProjectDto
            {
                Id = project.id,
                Name = project.name,
                Location = project.location,
                OwnerId = project.owner_id,
                OwnerName = project.Owner != null ? project.Owner.name : "N/A",
                StartDate = project.start_date,
                EndDate = project.end_date,
                Budget = project.budget,
                Status = project.status.ToString(),
                CreatedAt = project.created_at
            };
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto)
        {
            var project = new Project
            {
                name = projectDto.Name,
                location = projectDto.Location,
                owner_id = projectDto.OwnerId,
                start_date = projectDto.StartDate,
                end_date = projectDto.EndDate,
                budget = projectDto.Budget,
                status = Enum.Parse<ProjectStatus>(projectDto.Status)
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            projectDto.Id = project.id;
            projectDto.CreatedAt = project.created_at;
            return projectDto;
        }

        public async Task<ProjectDto> UpdateProjectAsync(int id, ProjectDto projectDto)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) throw new Exception("Project not found");

            project.name = projectDto.Name;
            project.location = projectDto.Location;
            project.owner_id = projectDto.OwnerId;
            project.start_date = projectDto.StartDate;
            project.end_date = projectDto.EndDate;
            project.budget = projectDto.Budget;
            project.status = Enum.Parse<ProjectStatus>(projectDto.Status);

            await _context.SaveChangesAsync();
            projectDto.Id = id;
            return projectDto;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
