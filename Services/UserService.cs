// Services/UserService.cs - ✅ FULLY FIXED for snake_case entities
using Microsoft.EntityFrameworkCore;
using ConstructionManagementSystem.Data;
using ConstructionManagementSystem.Data.Entities;
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Select(u => new UserDto  // ✅ Projection - NULL SAFE
                {
                    Id = u.id,
                    FullName = u.full_name,
                    Email = u.email,
                    Contact = u.contact,
                    RoleName = _context.Roles.Where(r => r.id == u.role_id).Select(r => r.name).FirstOrDefault() ?? "No Role",
                    AssignedProjectId = u.assigned_project_id,
                    ProjectName = _context.Projects.Where(p => p.id == u.assigned_project_id).Select(p => p.name).FirstOrDefault() ?? "No Project",
                    Salary = u.salary,
                    IsActive = u.is_active,
                    CreatedAt = u.created_at
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.id == id);

            if (user == null) return null;

            return new UserDto
            {
                Id = user.id,
                FullName = user.full_name,
                Email = user.email,
                Contact = user.contact,
                RoleName = user.Role?.name ?? "No Role",
                AssignedProjectId = user.assigned_project_id,
                ProjectName = user.AssignedProject?.name ?? "No Project",
                Salary = user.salary,
                IsActive = user.is_active,
                CreatedAt = user.created_at
            };
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = new User
            {
                full_name = userDto.FullName,
                email = userDto.Email,
                password_hash = userDto.Email, // In production, use proper hashing
                contact = userDto.Contact,
                role_id = 2, // Default MANAGER role - update as needed
                assigned_project_id = userDto.AssignedProjectId,
                salary = userDto.Salary,
                is_active = userDto.IsActive
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            userDto.Id = user.id;
            userDto.CreatedAt = user.created_at;
            userDto.RoleName = "MANAGER";
            return userDto;
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("User not found");

            user.full_name = userDto.FullName;
            user.email = userDto.Email;
            user.contact = userDto.Contact;
            user.assigned_project_id = userDto.AssignedProjectId;
            user.salary = userDto.Salary;
            user.is_active = userDto.IsActive;

            await _context.SaveChangesAsync();
            userDto.Id = id;
            return userDto;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserDto>> GetUsersByProjectIdAsync(int projectId)
        {
            return await _context.Users
                .Where(u => u.assigned_project_id == projectId)
                .Select(u => new UserDto
                {
                    Id = u.id,
                    FullName = u.full_name,
                    RoleName = _context.Roles.Where(r => r.id == u.role_id).Select(r => r.name).FirstOrDefault() ?? "No Role",
                    Salary = u.salary,
                    IsActive = u.is_active
                })
                .ToListAsync();
        }
    }
}
