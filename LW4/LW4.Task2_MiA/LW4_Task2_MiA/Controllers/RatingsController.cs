using LW4_Task2_MiA.Data;
using LW4_Task2_MiA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW4_Task2_MiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingsController : ControllerBase
    {
        private readonly InMemoryDb _db;
        public RatingsController(InMemoryDb db) => _db = db;

        [HttpGet]
        public ActionResult<IEnumerable<Rating>> GetAll() => Ok(_db.Ratings);

        [HttpGet("{id:int}")]
        public ActionResult<Rating> GetById(int id)
        {
            var r = _db.Ratings.FirstOrDefault(x => x.Id == id);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpPost("/api/recipes/{recipeId:int}/ratings")]
        public ActionResult<Rating> CreateForRecipe(int recipeId, [FromBody] Rating model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var recipe = _db.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipe is null) return NotFound("Recipe не знайдено.");

            // Перевіряємо існування користувача
            var user = _db.Users.FirstOrDefault(u => u.Id == model.UserId);
            if (user is null) return BadRequest("UserId не існує.");

            // Форсуємо правильні зв'язки
            var rating = new Rating
            {
                Id = _db.NextRatingId(),
                RecipeId = recipeId,           // ігноруємо model.RecipeId
                UserId = user.Id,
                Value = model.Value,
                Comment = model.Comment
            };

            _db.Ratings.Add(rating);
            return CreatedAtAction(nameof(GetById), new { id = rating.Id }, rating);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Rating> Update(int id, [FromBody] Rating model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.Id != 0 && model.Id != id)
                return BadRequest("Змінювати Id рейтингу заборонено.");

            var r = _db.Ratings.FirstOrDefault(x => x.Id == id);
            if (r is null) return NotFound();

            if (_db.Recipes.All(rc => rc.Id != model.RecipeId))
                return BadRequest("RecipeId не існує.");
            if (_db.Users.All(u => u.Id != model.UserId))
                return BadRequest("UserId не існує.");

            r.RecipeId = model.RecipeId;
            r.UserId = model.UserId;
            r.Value = model.Value;
            r.Comment = model.Comment;
            return Ok(r);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var r = _db.Ratings.FirstOrDefault(x => x.Id == id);
            if (r is null) return NotFound();
            _db.Ratings.Remove(r);
            return Ok();
        }
    }
}
