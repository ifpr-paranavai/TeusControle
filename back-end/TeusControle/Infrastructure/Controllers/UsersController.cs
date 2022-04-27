using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Infrastructure.Controllers
{
    /// <summary>
    /// Controlador para CRUD de usuário
    /// </summary>
    [Route("api/Users")]
    [ApiController]
    [Authorize(Policy = "Administrator")]
    public class UsersController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersService _service;
        public UsersController(
            IHttpContextAccessor httpContextAccessor,
            IUsersService service
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _service = service;
        }

        /// <summary>
        /// Inserir um novo usuário
        /// </summary>
        /// <param name="sentUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] CreateUserModel sentUser)
        {
            return Ok(_service.Insert(sentUser));
        }

        /// <summary>
        /// Atualizar um usuário
        /// </summary>
        /// <param name="sentUser"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] UpdateUserModel sentUser)
        {
            return Ok(_service.Update(sentUser));
        }

        /// <summary>
        /// Buscar usuário por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById([FromHeader] long id)
        {
            return Ok(_service.GetById(id));
        }

        /*/// <summary>
        /// Buscar todos os usuários
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IActionResult GetPaged(
             [FromHeader] int pageNumber,
             [FromHeader] int pageSize
        )
        {
            return Ok(_service.Get(
                pageNumber,
                pageSize
            ));
        }*/

        /// <summary>
        /// Busca todos os usuários
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] PaginatedInputModel pagingParams)
        {
            return Ok(await _service.Get(pagingParams));
        }

        /// <summary>
        /// Excluir um usuário por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromHeader] long id)
        {
            return Ok(_service.DeleteById(id));
        }
    }
}
