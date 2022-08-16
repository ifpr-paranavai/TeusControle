using Core.Domain;
using Data.Context;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class EntryRepository : BaseRepository<Entry>, IEntryRepository
    {
        public EntryRepository(MyContext context) : base(context)
        {
        }
    }
}
