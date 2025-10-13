namespace LW4_Task2_MiA.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty; 
        public string Description { get; set; } = string.Empty;
        public RecipeDifficulty Difficulty { get; set; } = RecipeDifficulty.Easy;
        public int CategoryId { get; set; }
        public int AuthorUserId { get; set; }
    }
}
