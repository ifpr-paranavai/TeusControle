using Core.Shared.Models.Request;
using Manager.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    /// <summary>
    /// Controlador de login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserManager userManager;

        /// <summary>
        /// Controlador de login
        /// </summary>
        /// <param name="userManager"></param>
        public AuthController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Realiza login
        /// </summary>
        /// <param name="login"></param>
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            var token = await userManager.ValidatePasswordGenerateTokenAsync(login);
            if (token != null)
            {
                return Ok(token);
            }
            return Unauthorized();
        }
    }
}
