using Core.Domain;
using Data.Context;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(MyContext context) : base(context)
        {
        }
    }
}
