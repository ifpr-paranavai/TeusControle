using Core.Domain;
using Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersManager usersManager;

        public UsersController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await usersManager.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await usersManager.GetUserAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users user)
        {
            var createdUser = await usersManager.InsertUserAsync(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, createdUser);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Users user)
        {
            var updatedUser = await usersManager.UpdateUserAsync(user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await usersManager.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
