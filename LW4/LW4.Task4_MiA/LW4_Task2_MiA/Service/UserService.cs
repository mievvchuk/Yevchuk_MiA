using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;
using MongoDB.Driver;

namespace LW4_Task4_MiA.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly IMongoCollection<User> _collection;

        public UserService(IRepository<User> repo, IMongoDatabase db)
        {
            _repo = repo;
            _collection = db.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<User?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
        {
            // не беремо id від клієнта
            user.Id = null;

            // (опціонально) перевірка емейлу
            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                var filter = Builders<User>.Filter.Eq(u => u.Email, user.Email);
                var exists = await _collection.Find(filter).FirstOrDefaultAsync();
                if (exists is not null)
                {
                    // можеш або кинути помилку, або повернути існуючого
                    return exists;
                }
            }

            await _repo.CreateAsync(user);
            return user;
        }

        public async Task<bool> UpdateAsync(string id, User user)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            user.Id = id;

            return await _repo.UpdateAsync(id, user);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            return await _repo.DeleteAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
