using Core.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(int id);

        Task<IEnumerable<User>> GetUsersAsync();
        
        Task<User> InsertUserAsync(User user);
        
        Task<User> UpdateUserAsync(User user);

        Task DeleteUserAsync(int id);
    }
}
