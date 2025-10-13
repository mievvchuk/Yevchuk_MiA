using System.ComponentModel.DataAnnotations;

namespace LW4_Task2_MiA.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(40, MinimumLength = 2)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CategoryType Type { get; set; } = CategoryType.Unknown;
    }
}
