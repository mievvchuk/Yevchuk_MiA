using LW4_Task4_MiA.DTO;

namespace LW4_Task4_MiA.Service
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingDto>> GetAllAsync();
        Task<RatingDto?> GetByIdAsync(string id);
        Task<RatingDto> CreateAsync(RatingDto rating);
        Task<bool> UpdateAsync(string id, RatingDto rating);
        Task<bool> DeleteAsync(string id);
        Task DeleteByRecipeIdAsync(string recipeId);
    }
}
