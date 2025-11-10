using LW4.Task1_MiA.Models;
namespace LW4.Task1_MiA.Endpoints {
    public static class RecipeEndpoints
    {
        public static void MapRecipeEndpoints(this WebApplication app)
        {
            var recipes = new List<Recipe>
            {
                new Recipe { Id = 1, Name = "Pasta", Instructions = "Boil water, add pasta" },
                new Recipe { Id = 2, Name = "Salad", Instructions = "Mix vegetables" },
                new Recipe { Id = 3, Name = "Sandwich", Instructions = "Put ingredients between bread slices" },
                new Recipe { Id = 4, Name = "Omelette", Instructions = "Beat eggs, cook in a pan" },
                new Recipe { Id = 5, Name = "Soup", Instructions = "Boil broth, add ingredients" },
                new Recipe { Id = 6, Name = "Steak", Instructions = "Season meat, cook to desired doneness" },
                new Recipe { Id = 7, Name = "Pancakes", Instructions = "Mix batter, cook on griddle" },
                new Recipe { Id = 8, Name = "Tacos", Instructions = "Fill tortillas with ingredients" },
                new Recipe { Id = 9, Name = "Pizza", Instructions = "Prepare dough, add toppings, bake" },
                new Recipe { Id = 10, Name = "Curry", Instructions = "Cook spices, add meat/vegetables, simmer" },
                new Recipe { Id = 11, Name = "Grilled Cheese", Instructions = "Butter bread, add cheese, grill" },
                new Recipe { Id = 12, Name = "Fruit Smoothie", Instructions = "Blend fruits with yogurt or milk" },
                new Recipe { Id = 13, Name = "Roast Chicken", Instructions = "Season chicken, roast in oven" },
            };
            app.MapGet("/recipes", () => Results.Ok(recipes));
            app.MapGet("/recipes/{id:int}", (int id) =>
            {
                var recipe = recipes.FirstOrDefault(r => r.Id == id);
                return recipe is not null ? Results.Ok(recipe) : Results.NotFound();
            });
            app.MapPost("/recipes", (Recipe newRecipe) =>
            {
                // Перевірка: чи заповнене поле Name
                if (string.IsNullOrWhiteSpace(newRecipe.Name))
                    return Results.BadRequest("Recipe name cannot be empty.");
                // Додавання нового рецепту
                newRecipe.Id = recipes.Count + 1;
                recipes.Add(newRecipe);
                return Results.Created($"/recipes/{newRecipe.Id}", newRecipe);
            });
            app.MapPut("/recipes/{id:int}", (int id, Recipe updatedRecipe) =>
            {
                var recipe = recipes.FirstOrDefault(r => r.Id == id);
                if (recipe is null) return Results.NotFound();
                recipe.Name = updatedRecipe.Name;
                recipe.Instructions = updatedRecipe.Instructions;
                return Results.Ok(recipe);
            });
            app.MapDelete("/recipes/{id:int}", (int id) =>
            {
                var recipe = recipes.FirstOrDefault(r => r.Id == id);
                if (recipe is null) return Results.NotFound();
                recipes.Remove(recipe);
                return Results.NoContent();
            });
        }
    }
}