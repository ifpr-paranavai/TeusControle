using Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUsersManager
    {
        Task<Users> GetUserAsync(int id);

        Task<IEnumerable<Users>> GetUsersAsync();

        Task<Users> InsertUserAsync(Users user);

        Task<Users> UpdateUserAsync(Users user);

        Task DeleteUserAsync(int id);
    }
}
