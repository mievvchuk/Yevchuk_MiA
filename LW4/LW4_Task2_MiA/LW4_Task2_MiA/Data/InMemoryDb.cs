using LW4_Task2_MiA.Models;

namespace LW4_Task2_MiA.Data
{
    public class InMemoryDb
    {
        public List<Category> Categories { get; } = new();
        public List<User> Users { get; } = new();
        public List<Recipe> Recipes { get; } = new();
        public List<Rating> Ratings { get; } = new();

        private int _catId = 1, _userId = 1, _recipeId = 1, _ratingId = 1;

        public InMemoryDb()
        {
            // Початкові дані
            Categories.AddRange(new[]
            {
                new Category{ Id = _catId++, Name="Десерти", Type=CategoryType.Dessert },
                new Category{ Id = _catId++, Name="Сніданки", Type=CategoryType.Breakfast },
                new Category{ Id = _catId++, Name="Обіди", Type=CategoryType.Lunch },
                new Category{ Id = _catId++, Name="Вечері", Type=CategoryType.Dinner },
                new Category{ Id = _catId++, Name="Інше", Type=CategoryType.Unknown },
            });
            Users.Add(new User { Id = _userId++, DisplayName = "Alice", Email = "alice@example.com", Role = UserRole.Regular });
            Users.Add(new User { Id = _userId++, DisplayName = "Bob", Email = "bob@gmail.com", Role = UserRole.Admin });
            Users.Add(new User { Id = _userId++, DisplayName = "Charlie", Email = "charlie@yahoo.com", Role = UserRole.Regular });
            Users.Add(new User { Id = _userId++, DisplayName = "Diana", Email = "diana@duckduckgo.com", Role = UserRole.Regular });
            Recipes.Add(new Recipe
            {
                Id = _recipeId++,
                Title = "Яблучний пиріг",
                Slug = "yabluchnyi-pyrig",
                Description = "Класичний рецепт.",
                Difficulty = RecipeDifficulty.Medium,
                CategoryId = 1,
                AuthorUserId = 1
            });
            Recipes.Add(new Recipe
            {
                Id = _recipeId++,
                Title = "Омлет",
                Slug = "omlet",
                Description = "Швидкий сніданок.",
                Difficulty = RecipeDifficulty.Easy,
                CategoryId = 2,
                AuthorUserId = 1
            });
            Recipes.Add(new Recipe
            {
                Id = _recipeId++,
                Title = "Млинці",
                Slug = "mlynci",
                Description = "Тонкі млинці з начинкою.",
                Difficulty = RecipeDifficulty.Easy,
                CategoryId = 2,
                AuthorUserId = 1
            });
            Recipes.Add(new Recipe
            {
                Id = _recipeId++,
                Title = "Тірамісу",
                Slug = "tiramisu",
                Description = "Італійський десерт.",
                Difficulty = RecipeDifficulty.Hard,
                CategoryId = 1,
                AuthorUserId = 1
            });
            Ratings.Add(new Rating { Id = _ratingId++, RecipeId = 1, UserId = 1, Value = 5, Comment = "Смачно!" });
            Ratings.Add(new Rating { Id = _ratingId++, RecipeId = 1, UserId = 2, Value = 4, Comment = "Добре, але можна краще." });
            Ratings.Add(new Rating { Id = _ratingId++, RecipeId = 2, UserId = 3, Value = 5, Comment = "Просто і смачно." });
            Ratings.Add(new Rating { Id = _ratingId++, RecipeId = 3, UserId = 4, Value = 3, Comment = "Млинці вийшли трохи товсті." });
            Ratings.Add(new Rating { RecipeId = _ratingId++, UserId = 2, Value = 5, Comment = "Обожнюю тірамісу!" });
            Ratings.Add(new Rating { Id = _ratingId++, RecipeId = 4, UserId = 3, Value = 4, Comment = "Дуже смачно, але складно готувати." });
            Ratings.Add(new Rating { Id = _ratingId++, RecipeId = 2, UserId = 4, Value = 4, Comment = "Хороший рецепт для швидкого сніданку." });
        }

        public int NextCategoryId() => _catId++;
        public int NextUserId() => _userId++;
        public int NextRecipeId() => _recipeId++;
        public int NextRatingId() => _ratingId++;
    }
}
