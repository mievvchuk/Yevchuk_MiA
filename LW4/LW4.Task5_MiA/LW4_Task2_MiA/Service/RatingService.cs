using AutoMapper;
using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;
using LW4_Task4_MiA.DTO;
using MongoDB.Driver;

namespace LW4_Task4_MiA.Service
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating> _repo;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Recipe> _recipes;
        private readonly IMapper _mapper;

        public RatingService(IMongoDatabase db, IRepository<Rating> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _users = db.GetCollection<User>("Users");
            _recipes = db.GetCollection<Recipe>("Recipes");
        }

        public async Task<IEnumerable<RatingDto>> GetAllAsync()
        {
            var ratings = await _repo.GetAllAsync();
            var result = new List<RatingDto>();

            foreach (var r in ratings)
            {
                var dto = _mapper.Map<RatingDto>(r);

                var user = await _users.Find(u => u.Id == r.UserId).FirstOrDefaultAsync();
                var recipe = await _recipes.Find(rec => rec.Id == r.RecipeId).FirstOrDefaultAsync();

                dto.UserName = user?.DisplayName ?? "(невідомий користувач)";
                dto.RecipeTitle = recipe?.Title ?? "(невідомий рецепт)";

                result.Add(dto);
            }

            return result;
        }

        public async Task<RatingDto?> GetByIdAsync(string id)
        {
            var rating = await _repo.GetByIdAsync(id);
            if (rating is null) return null;

            var dto = _mapper.Map<RatingDto>(rating);

            var user = await _users.Find(u => u.Id == rating.UserId).FirstOrDefaultAsync();
            var recipe = await _recipes.Find(r => r.Id == rating.RecipeId).FirstOrDefaultAsync();

            dto.UserName = user?.DisplayName ?? "(невідомий користувач)";
            dto.RecipeTitle = recipe?.Title ?? "(невідомий рецепт)";

            return dto;
        }

        public async Task<RatingDto> CreateAsync(RatingDto dto)
        {
            var rating = _mapper.Map<Rating>(dto);
            rating.Id = null;

            await _repo.CreateAsync(rating);
            return _mapper.Map<RatingDto>(rating);
        }

        public async Task<bool> UpdateAsync(string id, RatingDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            var updated = _mapper.Map<Rating>(dto);
            updated.Id = id;
            return await _repo.UpdateAsync(id, updated);
        }

        public async Task<bool> DeleteAsync(string id) =>
            await _repo.DeleteAsync(id);

        public async Task DeleteByRecipeIdAsync(string recipeId)
        {
            var filter = Builders<Rating>.Filter.Eq(r => r.RecipeId, recipeId);
            await _recipes.Database.GetCollection<Rating>("Ratings").DeleteManyAsync(filter);
        }
    }
}
