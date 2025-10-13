namespace LW4_Task2_MiA.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; } 
        public string? Comment { get; set; }
    }
}
