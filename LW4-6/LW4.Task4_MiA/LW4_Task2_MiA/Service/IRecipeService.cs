using LW4_Task2_MiA.Models;

namespace LW4_Task4_MiA.Service
{
    public interface IRecipeService
    {
        Task<IEnumerable<Recipe>> GetAllAsync();
        Task<Recipe?> GetByIdAsync(string id);
        Task<Recipe> CreateAsync(Recipe recipe);
        Task<bool> UpdateAsync(string id, Recipe recipe);
        Task<bool> DeleteAsync(string id);
    }
}
