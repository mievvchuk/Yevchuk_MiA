namespace LW4_Task4_MiA.DTO
{
    public class RatingDto
    {
        public string? Id { get; set; }
        public string RecipeId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public int Value { get; set; }
        public string? Comment { get; set; }
        public string? UserName { get; set; }
        public string? RecipeTitle { get; set; }
    }
}
