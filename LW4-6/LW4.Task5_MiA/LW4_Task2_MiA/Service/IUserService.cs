using LW4_Task4_MiA.DTO;

namespace LW4_Task4_MiA.Service
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(string id);
        Task<UserDto> CreateAsync(UserDto user);
        Task<bool> UpdateAsync(string id, UserDto user);
        Task<bool> DeleteAsync(string id);
        Task<UserDto?> GetByEmailAsync(string email);
    }
}
