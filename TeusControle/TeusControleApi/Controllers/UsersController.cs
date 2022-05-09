using Core.Domain;
using Core.Shared.Models.User;
using Manager.Interfaces;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Retorna todos os usuários.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await usersManager.GetUsersAsync());
        }

        /// <summary>
        /// Retorna um usuário buscado pelo id.
        /// </summary>
        /// <param name="id" example="123">Id do usuário.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await usersManager.GetUserAsync(id));
        }

        /// <summary>
        /// Insere um novo usuário.
        /// </summary>
        /// <param name="newUser"></param>
        [HttpPost]
        [ProducesResponseType(typeof(Users), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateUserModel newUser)
        {
            var createdUser = await usersManager.InsertUserAsync(newUser);
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Altera um usuário.
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [ProducesResponseType(typeof(Users), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateUserModel user)
        {
            var updatedUser = await usersManager.UpdateUserAsync(user);
            if (updatedUser == null)
            {
                return NotFound();
            }
            return Ok(updatedUser);
        }

        /// <summary>
        /// Exclui um usuário por id.
        /// </summary>
        /// <param name="id" example="123">Id do usuário.</param>
        [ProducesResponseType(typeof(Users), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await usersManager.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
