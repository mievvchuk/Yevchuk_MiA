using LW4_Task4_MiA.DTO;

namespace LW4_Task4_MiA.Service
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllAsync();
        Task<RecipeDto?> GetByIdAsync(string id);
        Task<RecipeDto> CreateAsync(RecipeDto recipe);
        Task<bool> UpdateAsync(string id, RecipeDto recipe);
        Task<bool> DeleteAsync(string id);
    }
}
