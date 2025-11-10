using LW4_Task2_MiA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LW4_Task4_MiA.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(string id);
        Task<Category> CreateAsync(Category category);
        Task<bool> UpdateAsync(string id, Category category);
        Task<bool> DeleteAsync(string id);
    }
}
