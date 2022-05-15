using Core.Domain;
using Data.Context;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository 
    {
        public UserRepository(MyContext context) : base(context)
        {
        }
    }
}
