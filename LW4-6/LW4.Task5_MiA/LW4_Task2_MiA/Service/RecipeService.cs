using AutoMapper;
using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;
using LW4_Task4_MiA.DTO;
using MongoDB.Driver;

namespace LW4_Task4_MiA.Service
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _repo;
        private readonly IMongoCollection<Category> _categories;
        private readonly IMongoCollection<User> _users;
        private readonly IRatingService _ratings;
        private readonly IMapper _mapper;

        public RecipeService(IRepository<Recipe> repo, IRatingService ratings, IMongoDatabase db, IMapper mapper)
        {
            _repo = repo;
            _ratings = ratings;
            _mapper = mapper;
            _categories = db.GetCollection<Category>("Categories");
            _users = db.GetCollection<User>("Users");
        }

        // Всі рецепти як RecipeDto
        public async Task<IEnumerable<RecipeDto>> GetAllAsync()
        {
            var recipes = await _repo.GetAllAsync();
            var result = new List<RecipeDto>();

            foreach (var r in recipes)
            {
                var dto = _mapper.Map<RecipeDto>(r);

                var category = await _categories.Find(c => c.Id == r.CategoryId).FirstOrDefaultAsync();
                var author = await _users.Find(u => u.Id == r.AuthorUserId).FirstOrDefaultAsync();

                dto.CategoryId = category?.Name ?? "(невідома категорія)";
                dto.AuthorUserId = author?.DisplayName ?? "(невідомий автор)";

                result.Add(dto);
            }

            return result;
        }

        // Один рецепт як RecipeDto
        public async Task<RecipeDto?> GetByIdAsync(string id)
        {
            var recipe = await _repo.GetByIdAsync(id);
            if (recipe is null) return null;

            var dto = _mapper.Map<RecipeDto>(recipe);

            var category = await _categories.Find(c => c.Id == recipe.CategoryId).FirstOrDefaultAsync();
            var author = await _users.Find(u => u.Id == recipe.AuthorUserId).FirstOrDefaultAsync();

            dto.CategoryId = category?.Name ?? "(невідома категорія)";
            dto.AuthorUserId = author?.DisplayName ?? "(невідомий автор)";

            return dto;
        }

        public async Task<RecipeDto> CreateAsync(RecipeDto recipeDto)
        {
            var recipe = _mapper.Map<Recipe>(recipeDto);
            recipe.Id = null;

            await _repo.CreateAsync(recipe);
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task<bool> UpdateAsync(string id, RecipeDto recipeDto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            var updated = _mapper.Map<Recipe>(recipeDto);
            updated.Id = id;
            return await _repo.UpdateAsync(id, updated);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            var ok = await _repo.DeleteAsync(id);
            if (ok)
                await _ratings.DeleteByRecipeIdAsync(id);

            return ok;
        }
    }
}
