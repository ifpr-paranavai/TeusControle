using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models;
using TeusControle.Infrastructure.Context;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para registro de saída de produtos
    /// </summary>
    public class ProductDisposalsRepository : BaseDoubleRepository<ProductDisposals>, IProductDisposalsRepository
    {
        public ProductDisposalsRepository(ApiContext context) : base (context)
        {

        }
    }
}