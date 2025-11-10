namespace LW4.Task1_MiA.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; } 
        public string Review { get; set; }
    }
}
