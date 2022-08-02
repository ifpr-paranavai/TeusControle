using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.User;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager usersManager;

        public UserController(IUserManager usersManager)
        {
            this.usersManager = usersManager;
        }

        /// <summary>
        /// Retorna todos os usuários paginado.
        /// </summary>
        [HttpPost("paged")]
        [ProducesResponseType(typeof(PaginatedResponse<UserPagedModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromBody] PaginatedRequest pagingParams)
        {
            var paged = usersManager.GetPaged(pagingParams);
            if (paged == null)
            {
                return NotFound("Não foi possível buscar usuários.");
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
                return NotFound("Não foi possível buscar usuário.");
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await usersManager.DeleteById(id);
            if (user == null)
            {
                return NotFound("Não foi possível excluir usuário.");
            }
            return NoContent();
        }
    }
}
