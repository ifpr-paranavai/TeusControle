using AutoMapper;
using Core.Domain;
using Core.Shared;
using Core.Shared.Models.User;
using Manager.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class UsersManager : IUsersManager
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;

        public UsersManager(IUsersRepository usersRepository, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await usersRepository.GetUsersAsync();
        }

        public async Task<Users> GetUserAsync(int id)
        {
            return await usersRepository.GetUserAsync(id);
        }

        public async Task<Users> InsertUserAsync(CreateUserModel newUser)
        {
            var user = mapper.Map<Users>(newUser);
            return await usersRepository.InsertUserAsync(user);
        }

        public async Task<Users> UpdateUserAsync(UpdateUserModel updatedUser)
        {
            var user = mapper.Map<Users>(updatedUser);
            return await usersRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await usersRepository.DeleteUserAsync(id);
        }
    }
}
