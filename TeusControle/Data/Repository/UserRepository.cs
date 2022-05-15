using Core.Domain;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Manager.Interfaces.Repositories;
using Data.Repository.Base;

namespace Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository 
    {
        private readonly MyContext myContext;

        public UserRepository(MyContext context) : base(context)
        {
            this.myContext = context;
        }
    }
}
