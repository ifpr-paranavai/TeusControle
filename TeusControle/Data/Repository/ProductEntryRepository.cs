using Core.Domain;
using Data.Context;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class ProductEntryRepository : BaseDoubleRepository<ProductEntry>, IProductEntryRepository
    {
        public ProductEntryRepository(MyContext context) : base(context)
        {
        }
    }
}
