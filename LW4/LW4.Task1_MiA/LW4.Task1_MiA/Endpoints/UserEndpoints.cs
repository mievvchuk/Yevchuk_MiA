using LW4.Task1_MiA.Models;

namespace LW4.Task1_MiA.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "john_doe", Email = "john@example.com" },
                new User { Id = 2, Username = "jane_smith", Email = "jane@example.com" },
                new User { Id = 3, Username = "alice_wonder", Email = "alice@example.com" }            
            };
            app.MapGet("/users", () => Results.Ok(users));
            app.MapGet("/users/{id:int}", (int id) =>
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                return user is not null ? Results.Ok(user) : Results.NotFound();
            });

            app.MapPost("/users", (User newUser) =>
            {
                newUser.Id = users.Count + 1;
                users.Add(newUser);
                return Results.Created($"/users/{newUser.Id}", newUser);
            });

            app.MapPut("/users/{id:int}", (int id, User updatedUser) =>
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user is null) return Results.NotFound();
                user.Username = updatedUser.Username;
                user.Email = updatedUser.Email;
                return Results.Ok(user);
            });

            app.MapDelete("/users/{id:int}", (int id) =>
            {
                var user = users.FirstOrDefault(u => u.Id == id);
                if (user is null) return Results.NotFound();
                users.Remove(user);
                return Results.NoContent();
            });
        }
    }
}