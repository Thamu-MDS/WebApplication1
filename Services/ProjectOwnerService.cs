// Services/ProjectOwnerService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public class ProjectOwnerService : IProjectOwnerService
    {
        private readonly ApplicationDbContext _context;

        public ProjectOwnerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectOwnerDto>> GetAllProjectOwnersAsync()
        {
            return await _context.ProjectOwners
                .Select(po => new ProjectOwnerDto
                {
                    Id = po.id,           // ✅ snake_case fixed
                    Name = po.name,       // ✅ snake_case fixed
                    Contact = po.contact, // ✅ snake_case fixed
                    Email = po.email,      // ✅ snake_case fixed
                    CreatedAt = po.created_at  // ✅ snake_case fixed
                })
                .ToListAsync();
        }

        public async Task<ProjectOwnerDto?> GetProjectOwnerByIdAsync(int id)
        {
            var owner = await _context.ProjectOwners.FindAsync(id);
            if (owner == null) return null;

            return new ProjectOwnerDto
            {
                Id = owner.id,           // ✅ snake_case fixed
                Name = owner.name,       // ✅ snake_case fixed
                Contact = owner.contact, // ✅ snake_case fixed
                Email = owner.email,      // ✅ snake_case fixed
                CreatedAt = owner.created_at  // ✅ snake_case fixed
            };
        }

        public async Task<ProjectOwnerDto> CreateProjectOwnerAsync(ProjectOwnerDto projectOwnerDto)
        {
            var owner = new ProjectOwner
            {
                name = projectOwnerDto.Name,     // ✅ snake_case fixed
                contact = projectOwnerDto.Contact, // ✅ snake_case fixed
                email = projectOwnerDto.Email    // ✅ snake_case fixed
            };

            _context.ProjectOwners.Add(owner);
            await _context.SaveChangesAsync();

            projectOwnerDto.Id = owner.id;           // ✅ snake_case fixed
            projectOwnerDto.CreatedAt = owner.created_at;  // ✅ snake_case fixed
            return projectOwnerDto;
        }

        public async Task<ProjectOwnerDto> UpdateProjectOwnerAsync(int id, ProjectOwnerDto projectOwnerDto)
        {
            var owner = await _context.ProjectOwners.FindAsync(id);
            if (owner == null) throw new Exception("Project Owner not found");

            owner.name = projectOwnerDto.Name;       // ✅ snake_case fixed
            owner.contact = projectOwnerDto.Contact; // ✅ snake_case fixed
            owner.email = projectOwnerDto.Email;     // ✅ snake_case fixed

            await _context.SaveChangesAsync();
            projectOwnerDto.Id = id;
            return projectOwnerDto;
        }

        public async Task<bool> DeleteProjectOwnerAsync(int id)
        {
            var owner = await _context.ProjectOwners.FindAsync(id);
            if (owner == null) return false;

            _context.ProjectOwners.Remove(owner);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
