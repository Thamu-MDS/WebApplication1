// Services/RoleService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    Id = r.id,    // ✅ snake_case fixed
                    Name = r.name // ✅ snake_case fixed
                })
                .ToListAsync();
        }

        public async Task<RoleDto?> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return null;

            return new RoleDto
            {
                Id = role.id,   // ✅ snake_case fixed
                Name = role.name // ✅ snake_case fixed
            };
        }
    }
}
