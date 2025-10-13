namespace LW4.Task1_MiA.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public List<Rating> Ratings { get; set; } = new List<Rating>();

    }
}
