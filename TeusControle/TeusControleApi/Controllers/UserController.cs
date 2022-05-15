using Core.Shared.Models.Request;
using Core.Shared.Models.User;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        /// Retorna todos os usuários paginado.
        /// </summary>
        [HttpGet] // todo: verifica como trazer objetos em url query params
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] PaginatedRequest pagingParams)
        {
            var paged = await usersManager.GetPaged(pagingParams);
            if (paged == null)
            {
                return Problem("Não foi possivel buscar usuários.");
            }

            return Ok(paged);
        }
         
        /// <summary>
        /// Retorna um usuário buscado pelo id.
        /// </summary>
        /// <param name="id" example="123">Id do usuário.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await usersManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Insere um novo usuário.
        /// </summary>
        /// <param name="newUser"></param>
        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateUserModel newUser)
        {
            logger.LogInformation(" Objeto recebido {@newUser}", newUser);
            var createdUser = await usersManager.Insert(newUser); 
            if (createdUser == null)
            {
                return Problem("Não foi possivel cadastrar usuário.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdUser.Id }, createdUser);
        }

        /// <summary>
        /// Altera um usuário.
        /// </summary>
        /// <param name="user"></param>
        [HttpPut]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateUserModel user)
        {
            var updatedUser = await usersManager.Update(user);
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
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await usersManager.DeleteById(id);
            return NoContent();
        }
    }
}
