using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models;
using TeusControle.Infrastructure.Context;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para registro de saídas de produtos
    /// </summary>
    public class DisposalsRepository : BaseRepository<Disposals>, IDisposalsRepository
    {
        public DisposalsRepository(ApiContext context) : base (context)
        {

        }
    }
}