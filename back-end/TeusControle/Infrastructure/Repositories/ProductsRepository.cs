using TeusControle.Application.Interfaces.Repository;
using TeusControle.Domain.Models;
using TeusControle.Infrastructure.Context;

namespace TeusControle.Infrastructure.Repository
{
    /// <summary>
    /// Repositório para produtos
    /// </summary>
    public class ProductsRepository : BaseRepository<Products>, IProductsRepository
    {
        public ProductsRepository(ApiContext context) : base (context)
        {

        }
    }
}