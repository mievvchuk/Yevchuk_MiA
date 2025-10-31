using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;

namespace LW4_Task4_MiA.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _recipes;
        private readonly IRatingService _ratings;

        public RecipeService(IRepository<Recipe> recipes, IRatingService ratings)
        {
            _recipes = recipes;
            _ratings = ratings;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
            => await _recipes.GetAllAsync();

        public async Task<Recipe?> GetByIdAsync(string id)
            => await _recipes.GetByIdAsync(id);

        public async Task<Recipe> CreateAsync(Recipe recipe)
        {
            recipe.Id = null;

            await _recipes.CreateAsync(recipe);
            return recipe;
        }

        public async Task<bool> UpdateAsync(string id, Recipe recipe)
        {
            var existing = await _recipes.GetByIdAsync(id);
            if (existing is null) return false;

            // фіксуємо id
            recipe.Id = id;

            return await _recipes.UpdateAsync(id, recipe);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _recipes.GetByIdAsync(id);
            if (existing is null) return false;

            var ok = await _recipes.DeleteAsync(id);
            if (ok)
            {
                // прибираємо рейтинги цього рецепта
                await _ratings.DeleteByRecipeIdAsync(id);
            }
            return ok;
        }
    }
}
