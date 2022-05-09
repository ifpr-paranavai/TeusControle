using Core.Domain;
using Core.Shared.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUsersManager
    {
        Task<Users> GetUserAsync(int id);

        Task<IEnumerable<Users>> GetUsersAsync();

        Task<Users> InsertUserAsync(CreateUserModel newuUser);

        Task<Users> UpdateUserAsync(UpdateUserModel updatedUser);

        Task DeleteUserAsync(int id);
    }
}
