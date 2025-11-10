using AutoMapper;
using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;
using LW4_Task4_MiA.DTO;

namespace LW4_Task4_MiA.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repo;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<CategoryDto>>(await _repo.GetAllAsync());

        public async Task<CategoryDto?> GetByIdAsync(string id)
            => _mapper.Map<CategoryDto?>(await _repo.GetByIdAsync(id));

        public async Task<CategoryDto> CreateAsync(CategoryDto dto)
        {
            var entity = _mapper.Map<Category>(dto);
            await _repo.CreateAsync(entity);
            return _mapper.Map<CategoryDto>(entity);
        }

        public async Task<bool> UpdateAsync(string id, CategoryDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;
            _mapper.Map(dto, entity);
            return await _repo.UpdateAsync(id, entity);
        }

        public async Task<bool> DeleteAsync(string id)
            => await _repo.DeleteAsync(id);
    }
}
