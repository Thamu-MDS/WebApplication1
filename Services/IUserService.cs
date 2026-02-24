// Services/IUserService.cs
using ConstructionManagementSystem.Models.DTOs;

namespace ConstructionManagementSystem.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<UserDto> UpdateUserAsync(int id, UserDto userDto);
        Task<bool> DeleteUserAsync(int id);
        Task<List<UserDto>> GetUsersByProjectIdAsync(int projectId);
    }
}
