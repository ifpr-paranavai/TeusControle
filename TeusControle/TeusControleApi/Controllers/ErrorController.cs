using Core.Shared.Models.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace TeusControleApi.Controllers
{
    /// <summary>
    /// Controlador para erro
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : Controller
    {
        /// <summary>
        /// Endpoint de erro
        /// </summary>
        /// <returns></returns>
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; // busca exception lançada

            var errorId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            Response.StatusCode = 500;
            return new ErrorResponse(errorId);
        }
    }
}
