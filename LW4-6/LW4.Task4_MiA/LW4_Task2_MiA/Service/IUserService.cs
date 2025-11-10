using LW4_Task2_MiA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LW4_Task4_MiA.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(string id);
        Task<User> CreateAsync(User user);
        Task<bool> UpdateAsync(string id, User user);
        Task<bool> DeleteAsync(string id);

        Task<User?> GetByEmailAsync(string email);
    }
}
