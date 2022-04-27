using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Infrastructure.Controllers
{
    /// <summary>
    /// Controlador para CRUD de produtos
    /// </summary>
    [Route("api/Products")]
    [ApiController]
    [Authorize(Policy = "Administrator")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _service;
        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Inserir um novo produto
        /// </summary>
        /// <param name="sentProduct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] CreateProductsModel sentProduct)
        {
            return Ok(_service.Insert(sentProduct));
        }

        /// <summary>
        /// Atualizar um produto
        /// </summary>
        /// <param name="sentProduct"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] UpdateProductsModel sentProduct)
        {
            return Ok(_service.Update(sentProduct));
        }

        /// <summary>
        /// Buscar um produto por id
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
        /// Buscar todos os produtos
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
        /// Busca todos os produtos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody] PaginatedInputModel pagingParams)
        {
            return Ok(await _service.Get(pagingParams));
        }

        /// <summary>
        /// Excluir um produto por id
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
