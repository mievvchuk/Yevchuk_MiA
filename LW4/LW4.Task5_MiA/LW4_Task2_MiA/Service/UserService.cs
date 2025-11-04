using AutoMapper;
using LW4_Task2_MiA.Models;
using LW4_Task2_MiA.Repositories;
using LW4_Task4_MiA.DTO;
using MongoDB.Driver;

namespace LW4_Task4_MiA.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repo;
        private readonly IMongoCollection<User> _collection;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> repo, IMongoDatabase db, IMapper mapper)
        {
            _repo = repo;
            _collection = db.GetCollection<User>("Users");
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
            => _mapper.Map<IEnumerable<UserDto>>(await _repo.GetAllAsync());

        public async Task<UserDto?> GetByIdAsync(string id)
            => _mapper.Map<UserDto?>(await _repo.GetByIdAsync(id));

        public async Task<UserDto> CreateAsync(UserDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            entity.Id = null;
            await _repo.CreateAsync(entity);
            return _mapper.Map<UserDto>(entity);
        }

        public async Task<bool> UpdateAsync(string id, UserDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;
            _mapper.Map(dto, entity);
            return await _repo.UpdateAsync(id, entity);
        }

        public async Task<bool> DeleteAsync(string id)
            => await _repo.DeleteAsync(id);

        public async Task<UserDto?> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            var user = await _collection.Find(filter).FirstOrDefaultAsync();
            return _mapper.Map<UserDto?>(user);
        }
    }
}
