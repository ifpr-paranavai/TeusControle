using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models;
using TeusControle.Infrastructure.Context;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para registro de entrada de produtos
    /// </summary>
    public class EntriesRepository : BaseRepository<Entries>, IEntriesRepository
    {
        public EntriesRepository(ApiContext context) : base (context)
        {

        }
    }
}