using LW4.Task1_MiA.Models;
namespace LW4.Task1_MiA.Endpoints { 
    public static class RatingEndpoints
    {
        public static void MapRatingEndpoints(this WebApplication app)
        {
            var ratings = new List<Rating>
            {
                new Rating { Id = 1, RecipeId = 1, UserId = 1, Score = 5, Review = "Delicious!" },
                new Rating { Id = 2, RecipeId = 2, UserId = 2, Score = 4, Review = "Tasty, but could use more dressing." },
                new Rating { Id = 3, RecipeId = 3, UserId = 3, Score = 3, Review = "Average." },
                new Rating { Id = 4, RecipeId = 1, UserId = 2, Score = 4, Review = "Pretty good!" },
                new Rating { Id = 5, RecipeId = 2, UserId = 3, Score = 5, Review = "Loved it!" },
                new Rating { Id = 6, RecipeId = 3, UserId = 1, Score = 2, Review = "Not my favorite." },
                new Rating { Id = 7, RecipeId = 1, UserId = 3, Score = 5, Review = "Absolutely fantastic!" },
            };
            app.MapGet("/ratings", () => Results.Ok(ratings));
            app.MapGet("/ratings/{id:int}", (int id) =>
            {
                var rating = ratings.FirstOrDefault(r => r.Id == id);
                return rating is not null ? Results.Ok(rating) : Results.NotFound();
            });
            app.MapPost("/ratings", (Rating newRating) =>
            {
                newRating.Id = ratings.Count + 1;
                ratings.Add(newRating);
                return Results.Created($"/ratings/{newRating.Id}", newRating);
            });
            app.MapPut("/ratings/{id:int}", (int id, Rating updatedRating) =>
            {
                var rating = ratings.FirstOrDefault(r => r.Id == id);
                if (rating is null) return Results.NotFound();
                rating.Score = updatedRating.Score;
                rating.Review = updatedRating.Review;
                return Results.Ok(rating);
            });
            app.MapDelete("/ratings/{id:int}", (int id) =>
            {
                var rating = ratings.FirstOrDefault(r => r.Id == id);
                if (rating is null) return Results.NotFound();
                ratings.Remove(rating);
                return Results.NoContent();
            });
        }
    }
}