namespace LW4_Task4_MiA.DTO
{
    public class RecipeDto
    {
        public string? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string CategoryId { get; set; } = string.Empty;
        public string AuthorUserId { get; set; } = string.Empty;
    }
}
