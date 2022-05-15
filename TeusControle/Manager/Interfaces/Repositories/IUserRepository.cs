using Core.Domain;
using Manager.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
