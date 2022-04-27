using TeusControle.Domain.Models.Dtos;
using TeusControle.Infrastructure.Dtos;

namespace TeusControle.Application.Interfaces.Services
{
    public interface ILoginService
    {
        /// <summary>
        /// Gera token de acesso
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        ResponseMessages<string> GenerateToken(TokenLogin credential);
    }
}
