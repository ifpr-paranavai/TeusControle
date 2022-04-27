using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Infrastructure.Controllers
{
    [Route("api/DisposalsController")]
    [ApiController]
    [Authorize]
    public class DisposalsController : Controller
    {
        private readonly IDisposalsService _service;
        public DisposalsController(IDisposalsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Criar uma saída de produto
        /// </summary>
        /// <param name="disposalModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Insert")]
        public IActionResult CreateDisposal(CreateDisposalModel disposalModel)
        {
            return Ok(_service.Insert(disposalModel));
        }

        /// <summary>
        /// Atualiza uma saída de produto
        /// </summary>
        /// <param name="disposalModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateEntry(UpdateDisposalModel disposalModel)
        {
            return Ok(_service.Update(disposalModel));
        }

        /// <summary>
        /// Deleta uma saída de produto
        /// </summary>
        /// <param name="disposalId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteDisposal(long disposalId)
        {
            return Ok(_service.Delete(disposalId));
        }

        /// <summary>
        /// Insere um novo item de produto a uma respectiva saída de produtos
        /// </summary>
        /// <param name="productDisposal"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertProductDisposalItem")]
        public IActionResult InsertProductEntryItem(CreateProductDisposalsModel productDisposal)
        {
            return Ok(_service.InsertProductDisposalItem(productDisposal));
        }

        /// <summary>
        /// Remove um item de produto de uma respectiva saída de produtos
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteProductItem")]
        public IActionResult DeleteProductItem(
            long entityId,
            long productId
        )
        {
            return Ok(_service.DeleteProductDisposalItem(
                entityId,
                productId
            ));
        }

        /// <summary>
        /// Finaliza saída de produtos
        /// </summary>
        /// <param name="disposalId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CloseDisposal")]
        public IActionResult CloseDisposal(long disposalId)
        {
            return Ok(_service.CloseDisposal(disposalId));
        }

        /*/// <summary>
        /// Busca todas as saídas
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
        /// Busca todas as saídas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        public async Task<IActionResult> GetPaged([FromBody]PaginatedInputModel pagingParams) 
        {
            return Ok(await _service.Get(pagingParams));
        }

        /// <summary>
        /// Busca informações de uma saída
        /// </summary>
        /// <param name="disposalId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(long disposalId)
        {
            return Ok(_service.GetById(disposalId));
        }
    }
}
