using LW4_Task4_MiA.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LW4_Task4_MiA.Service
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(string id);
        Task<CategoryDto> CreateAsync(CategoryDto category);
        Task<bool> UpdateAsync(string id, CategoryDto category);
        Task<bool> DeleteAsync(string id);
    }
}
