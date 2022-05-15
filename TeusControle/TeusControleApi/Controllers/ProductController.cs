using Core.Shared.Models.Product;
using Core.Shared.Models.Request;
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
    [Authorize(Policy = "Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager productManager;

        public ProductController(IProductManager usersManager)
        {
            this.productManager = usersManager;
        }

        /// <summary>
        /// Retorna todos os produtos paginado.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] PaginatedRequest pagingParams)
        {
            var paged = await productManager.GetPaged(pagingParams);
            if (paged == null)
            {
                return Problem("Não foi possivel buscar produtos.");
            }

            return Ok(paged);
        }

        /// <summary>
        /// Retorna um produto buscado pelo id.
        /// </summary>
        /// <param name="id" example="123">Id do produto.</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            var user = await productManager.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Insere um novo produto.
        /// </summary>
        /// <param name="newProduct"></param>
        [HttpPost]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateProductModel newProduct)
        {
            var createdProduct = await productManager.Insert(newProduct); 
            if (createdProduct == null)
            {
                return Problem("Não foi possivel cadastrar produto.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Altera um produto.
        /// </summary>
        /// <param name="product"></param>
        [HttpPut]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UpdateProductModel product)
        {
            var updatedProduct = await productManager.Update(product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }

        /// <summary>
        /// Exclui um produto por id.
        /// </summary>
        /// <param name="id" example="123">Id do usuário.</param>
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await productManager.DeleteById(id);
            return NoContent();
        }
    }
}
