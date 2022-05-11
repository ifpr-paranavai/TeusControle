using Core.Domain;
using Core.Shared.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUserManager
    {
        Task<User> GetUserAsync(int id);

        Task<IEnumerable<User>> GetUsersAsync();

        Task<User> InsertUserAsync(CreateUserModel newuUser);

        Task<User> UpdateUserAsync(UpdateUserModel updatedUser);

        Task DeleteUserAsync(int id);
    }
}
