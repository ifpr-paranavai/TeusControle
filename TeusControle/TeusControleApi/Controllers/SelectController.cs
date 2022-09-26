using Manager.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TeusControleApi.Controllers
{
    /// <summary>
    /// Controlador para buscar opções de seleção
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SelectController : ControllerBase
    {
        private readonly ISelectManager selectManager;

        /// <summary>
        /// Controlador para buscar opções de seleção
        /// </summary>
        /// <param name="selectManager"></param>
        public SelectController(ISelectManager selectManager)
        {
            this.selectManager = selectManager;
        }
        
        /// <summary>
        /// Busca status para entradas
        /// </summary>
        /// <returns></returns>
        [HttpGet("entryStatus")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult GetEntryStatusSelect()
        {
            var select = selectManager.getEntryStatusSelect();
            if (select == null)
            {
                return NotFound("Não foi possível buscar itens.");
            }
            return Ok(select);
        }

        /// <summary>
        /// Busca tipos de usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet("userType")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserTypeSelect()
        {
            var select = selectManager.getUserTypeSelect();
            if (select == null)
            {
                return NotFound("Não foi possível buscar itens.");
            }
            return Ok(select);
        }

        /// <summary>
        /// Busca status para venda
        /// </summary>
        /// <returns></returns>
        [HttpGet("saleStatus")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public IActionResult GetSaleStatusSelect()
        {
            var select = selectManager.GetSaleStatusSelect();
            if (select == null)
            {
                return NotFound("Não foi possível buscar itens.");
            }
            return Ok(select);
        }
    }
}