using LW4_Task2_MiA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LW4_Task6_MiA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet("protected")]
        [Authorize] 
        public IActionResult GetProtectedData()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            var email = User.FindFirstValue(ClaimTypes.Email);
            var role = User.FindFirstValue(ClaimTypes.Role);

            return Ok(new
            {
                Message = "Це захищені дані.",
                UserId = userId,
                Email = email,
                Role = role
            });
        }
    }
}
