using LW4_Task2_MiA.Models;

namespace LW4_Task4_MiA.Service
{
    public interface IRatingService
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating?> GetByIdAsync(string id);
        Task<Rating> CreateAsync(Rating rating);
        Task<bool> DeleteAsync(string id);
        Task<bool> UpdateAsync(string id, Rating rating);

        //щоб при видаленні рецепта прибрати рейтинги
        Task DeleteByRecipeIdAsync(string recipeId);
    }
}
