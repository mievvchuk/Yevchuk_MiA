using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;

namespace LW4_Task4_MiA.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;

        public CategoryService(IRepository<Category> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Category?> GetByIdAsync(string id) => await _repo.GetByIdAsync(id);

        public async Task<Category> CreateAsync(Category category)
        {
            category.Id = null;

            await _repo.CreateAsync(category);
            return category;
        }

        public async Task<bool> UpdateAsync(string id, Category category)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            //фіксуємо id
            category.Id = id;

            return await _repo.UpdateAsync(id, category);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;

            return await _repo.DeleteAsync(id);
        }
    }
}
