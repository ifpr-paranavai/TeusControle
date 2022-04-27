using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models;
using TeusControle.Infrastructure.Context;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para registro de entrada de produtos
    /// </summary>
    public class ProductEntriesRepository : BaseDoubleRepository<ProductEntries>, IProductEntriesRepository
    {
        public ProductEntriesRepository(ApiContext context) : base (context)
        {

        }
    }
}