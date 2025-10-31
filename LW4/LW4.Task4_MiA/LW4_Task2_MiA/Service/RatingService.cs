using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;
using MongoDB.Driver;

namespace LW4_Task4_MiA.Service
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating> _repo;
        private readonly IMongoCollection<Rating> _collection;

        public RatingService(IMongoDatabase db, IRepository<Rating> repo)
        {
            _repo = repo;
            _collection = db.GetCollection<Rating>("Ratings");
        }

        public async Task<IEnumerable<Rating>> GetAllAsync()
            => await _repo.GetAllAsync();

        public async Task<Rating?> GetByIdAsync(string id)
            => await _repo.GetByIdAsync(id);

        public async Task<Rating> CreateAsync(Rating rating)
        {
            // не приймаємо id від клієнта
            rating.Id = null;

            await _repo.CreateAsync(rating);
            return rating;
        }

        public async Task<bool> UpdateAsync(string id, Rating rating)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            rating.Id = id;

            return await _repo.UpdateAsync(id, rating);
        }

        public async Task<bool> DeleteAsync(string id)
            => await _repo.DeleteAsync(id);

        public async Task DeleteByRecipeIdAsync(string recipeId)
        {
            var filter = Builders<Rating>.Filter.Eq(r => r.RecipeId, recipeId);
            await _collection.DeleteManyAsync(filter);
        }
    }
}
