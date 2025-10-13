using LW4_Task2_MiA.Data;
using LW4_Task2_MiA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW4_Task2_MiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly InMemoryDb _db;
        public CategoriesController(InMemoryDb db) => _db = db;

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll() => Ok(_db.Categories);

        [HttpGet("{id:int}")]
        public ActionResult<Category> GetById(int id)
        {
            var item = _db.Categories.FirstOrDefault(x => x.Id == id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public ActionResult<Category> Create([FromBody] Category model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            model.Id = _db.NextCategoryId();
            _db.Categories.Add(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public ActionResult<Category> Update(int id, [FromBody] Category model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.Id != 0 && model.Id != id)
                return BadRequest("Змінювати Id категорії заборонено.");

            var existing = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (existing is null) return NotFound();

            existing.Name = model.Name;
            existing.Type = model.Type;
            return Ok(existing);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var existing = _db.Categories.FirstOrDefault(x => x.Id == id);
            if (existing is null) return NotFound();
            _db.Categories.Remove(existing);
            return Ok();
        }
    }
}
