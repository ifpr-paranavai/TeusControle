using Core.Domain;
using Core.Shared.Models.User;
using Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SerilogTimings;
using System;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager usersManager;
        private readonly ILogger<UserController> logger;

        public UserController(IUserManager usersManager, ILogger<UserController> logger)
        {
            this.usersManager = usersManager;
            this.logger = logger;
        }

        /// <summary>
        /// Retorna todos os usuários.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateUserModel newUser)
        {
            logger.LogInformation("Objeto recebido {@newUser}", newUser);
            User createdUser;
            using (Operation.Time("Tempo de adição de um novo usuário."))
            {
                logger.LogInformation("Foi requisitada a inserção de um novo usuário.");
                createdUser = await usersManager.InsertUserAsync(newUser);
            }
           
            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Altera um usuário.
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
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
        [ProducesResponseType(typeof(User), StatusCodes.Status204NoContent)]
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
