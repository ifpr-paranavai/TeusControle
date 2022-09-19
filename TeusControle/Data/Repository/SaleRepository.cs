using Core.Domain;
using Data.Context;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(MyContext context) : base(context)
        {
        }
    }
}
