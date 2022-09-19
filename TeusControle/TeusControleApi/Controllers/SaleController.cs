using Core.Shared.Models.Entry;
using Core.Shared.Models.Request;
using Core.Shared.Models.Responses;
using Core.Shared.Models.Sale;
using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    /// <summary>
    /// Controlador para venda de produtos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleManager saleManager;

        /// <summary>
        /// Controlador para venda de produtos
        /// </summary>
        /// <param name="saleManager"></param>
        public SaleController(ISaleManager saleManager)
        {
            this.saleManager = saleManager;
        }

        /// <summary>
        /// Retorna todas as vendas paginada.
        /// </summary>
        [HttpPost("paged")]
        [ProducesResponseType(typeof(PaginatedResponse<SalePagedModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult Get([FromBody] PaginatedRequest pagingParams)
        {
            var paged = saleManager.GetPaged(pagingParams);
            if (paged == null)
            {
                return NotFound("Não foi possivel buscar vendas.");
            }

            return Ok(paged);
        }

        /// <summary>
        /// Retorna uma venda buscada pelo id.
        /// </summary>
        /// <param name="id" example="123">Id da venda.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SaleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await saleManager.GetById(id);
            if (user == null)
            {
                return NotFound("Não foi possível buscar venda.");
            }
            return Ok(user);
        }

        /// <summary>
        /// Insere uma nova venda.
        /// </summary>
        /// <param name="newSale"></param>
        [HttpPost]
        [ProducesResponseType(typeof(SaleModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateSaleModel newSale)
        {
            var createdSale = await saleManager.Insert(newSale); 
            if (createdSale == null)
            {
                return Problem("Não foi possivel cadastrar venda.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdSale.Id }, createdSale);
        }

        /// <summary>
        /// Altera uma venda.
        /// </summary>
        /// <param name="sale"></param>
        [HttpPut]
        [ProducesResponseType(typeof(SaleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateSaleModel sale)
        {
            var updatedSale = await saleManager.Update(sale);
            if (updatedSale == null)
            {
                return Problem("Não foi possivel atualizar venda.");
            }
            return Ok(updatedSale);
        }

        /// <summary>
        /// Exclui uma venda por id.
        /// </summary>
        /// <param name="id" example="123">Id da venda.</param>
        [ProducesResponseType(typeof(SaleModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            var sale = await saleManager.DeleteById(id);
            if (sale == null)
            {
                return NotFound("Não foi possível excluir venda.");
            }
            return NoContent();
        }
    }
}
