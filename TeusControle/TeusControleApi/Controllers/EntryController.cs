using Core.Shared.Models.Entry;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    /// <summary>
    /// Controlador para entrada de produtos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EntryController : ControllerBase
    {
        private readonly IEntryManager entryManager;

        /// <summary>
        /// Controlador para entrada de produtos
        /// </summary>
        /// <param name="entryManager"></param>
        public EntryController(IEntryManager entryManager)
        {
            this.entryManager = entryManager;
        }

        /// <summary>
        /// Retorna todas as entradas paginada.
        /// </summary>
        [HttpPost("paged")]
        [ProducesResponseType(typeof(PaginatedResponse<EntryPagedModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromBody] PaginatedRequest pagingParams)
        {
            var paged = entryManager.GetPaged(pagingParams);
            if (paged == null)
            {
                return NotFound("Não foi possivel buscar entradas.");
            }

            return Ok(paged);
        }

        /// <summary>
        /// Retorna uma entrada buscada pelo id.
        /// </summary>
        /// <param name="id" example="123">Id da entrada.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EntryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await entryManager.GetById(id);
            if (user == null)
            {
                return NotFound("Não foi possível buscar entrada.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Insere uma nova entrada.
        /// </summary>
        /// <param name="newEntry"></param>
        [HttpPost]
        [ProducesResponseType(typeof(EntryModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateEntryModel newEntry)
        {
            var createdEntry = await entryManager.Insert(newEntry); 
            if (createdEntry == null)
            {
                return Problem("Não foi possivel cadastrar entrada.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdEntry.Id }, createdEntry);
        }

        /// <summary>
        /// Altera uma entrada.
        /// </summary>
        /// <param name="entry"></param>
        [HttpPut]
        [ProducesResponseType(typeof(EntryModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateEntryModel entry)
        {
            var updatedEntry = await entryManager.Update(entry);
            if (updatedEntry == null)
            {
                return Problem("Não foi possivel atualizar entrada.");
            }
            return Ok(updatedEntry);
        }

        /// <summary>
        /// Exclui uma entrada por id.
        /// </summary>
        /// <param name="id" example="123">Id da entrada.</param>
        [ProducesResponseType(typeof(EntryModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var product = await entryManager.DeleteById(id);
            if (product == null)
            {
                return NotFound("Não foi possível excluir entrada.");
            }
            return NoContent();
        }
    }
}
