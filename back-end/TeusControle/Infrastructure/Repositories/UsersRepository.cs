using System.Linq;
using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models;
using TeusControle.Infrastructure.Context;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para usuários
    /// </summary>
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(ApiContext context) : base (context)
        {

        }
    }
}