using LW4_Task2_MiA.Data;
using LW4_Task2_MiA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW4_Task2_MiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly InMemoryDb _db;
        public RecipesController(InMemoryDb db) => _db = db;

        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAll() => Ok(_db.Recipes);

        [HttpGet("{id:int}")]
        public ActionResult<Recipe> GetById(int id)
        {
            var r = _db.Recipes.FirstOrDefault(x => x.Id == id);
            return r is null ? NotFound() : Ok(r);
        }

        [HttpPost]
        public ActionResult<Recipe> Create([FromBody] Recipe model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_db.Categories.All(c => c.Id != model.CategoryId)) return BadRequest("CategoryId не існує.");
            if (_db.Users.All(u => u.Id != model.AuthorUserId)) return BadRequest("AuthorUserId не існує.");

            model.Id = _db.NextRecipeId();
            _db.Recipes.Add(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Recipe> Update(int id, [FromBody] Recipe model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.Id != 0 && model.Id != id)
                return BadRequest("Змінювати Id рецепта заборонено.");

            var r = _db.Recipes.FirstOrDefault(x => x.Id == id);
            if (r is null) return NotFound();

            if (_db.Categories.All(c => c.Id != model.CategoryId))
                return BadRequest("CategoryId не існує.");
            if (_db.Users.All(u => u.Id != model.AuthorUserId))
                return BadRequest("AuthorUserId не існує.");

            r.Title = model.Title;
            r.Slug = model.Slug;
            r.Description = model.Description;
            r.Difficulty = model.Difficulty;
            r.CategoryId = model.CategoryId;
            r.AuthorUserId = model.AuthorUserId;

            return Ok(r);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var r = _db.Recipes.FirstOrDefault(x => x.Id == id);
            if (r is null) return NotFound();
            _db.Recipes.Remove(r);
            _db.Ratings.RemoveAll(rt => rt.RecipeId == id); // каскадне очищення рейтингів
            return Ok();
        }
    }
}
