using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeusControle.Application.Interfaces.Services;
using TeusControle.Domain.Models.Dtos;

namespace TeusControle.Infrastructure.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService Configuration)
        {
            _service = Configuration;
        }

        /// <summary>
        /// Gera token de acesso
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        public IActionResult Token([FromBody] TokenLogin login)
        {
            return Ok(_service.GenerateToken(login));
        }
    }
}
