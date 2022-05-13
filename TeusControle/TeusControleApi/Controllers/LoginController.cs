using Core.Domain;
using Core.Shared.Models.Request;
using Manager.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TeusControleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // TODO: mover regra de login para login manager
        private readonly IUserManager userManager;

        public LoginController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Realiza login
        /// </summary>
        /// <param name="login"></param>
        [HttpPost]
        [Route("Login")]
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
