using Core.Domain;
using Manager.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository usersRepository;
        
        public UsersManager(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await usersRepository.GetUsersAsync();
        }

        public async Task<Users> GetUserAsync(int id)
        {
            return await usersRepository.GetUserAsync(id);
        }

        public async Task<Users> InsertUserAsync(Users user)
        {
            return await usersRepository.InsertUserAsync(user);
        }

        public async Task<Users> UpdateUserAsync(Users user)
        {
            return await usersRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await usersRepository.DeleteUserAsync(id);
        }
    }
}
