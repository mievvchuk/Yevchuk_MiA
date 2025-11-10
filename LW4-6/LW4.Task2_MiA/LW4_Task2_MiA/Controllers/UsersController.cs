using LW4_Task2_MiA.Data;
using LW4_Task2_MiA.Models;
using Microsoft.AspNetCore.Mvc;

namespace LW4_Task2_MiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly InMemoryDb _db;
        public UsersController(InMemoryDb db) => _db = db;

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll() => Ok(_db.Users);

        [HttpGet("{id:int}")]
        public ActionResult<User> GetById(int id)
        {
            var u = _db.Users.FirstOrDefault(x => x.Id == id);
            return u is null ? NotFound() : Ok(u);
        }

        [HttpPost]
        public ActionResult<User> Create([FromBody] User model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            model.Id = _db.NextUserId();
            _db.Users.Add(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public ActionResult<User> Update(int id, [FromBody] User model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (model.Id != 0 && model.Id != id)
                return BadRequest("Змінювати Id користувача заборонено.");

            var u = _db.Users.FirstOrDefault(x => x.Id == id);
            if (u is null) return NotFound();

            u.DisplayName = model.DisplayName;
            u.Email = model.Email;
            u.Role = model.Role;
            return Ok(u);
        }


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var u = _db.Users.FirstOrDefault(x => x.Id == id);
            if (u is null) return NotFound();
            _db.Users.Remove(u);
            return Ok();
        }
    }
}
