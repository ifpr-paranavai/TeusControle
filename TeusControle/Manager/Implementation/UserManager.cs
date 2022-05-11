using AutoMapper;
using Core.Domain;
using Core.Shared.Models.User;
using Manager.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserManager(IUserRepository usersRepository, IMapper mapper)
        {
            this.userRepository = usersRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await userRepository.GetUserAsync(id);
        }

        public async Task<User> InsertUserAsync(CreateUserModel newUser)
        {
            var user = mapper.Map<User>(newUser);
            ConvertPasswordToHash(user);

            return await userRepository.InsertUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(UpdateUserModel updatedUser)
        {
            var user = mapper.Map<User>(updatedUser);
            ConvertPasswordToHash(user);

            return await userRepository.UpdateUserAsync(user);
        }

        public async Task<User> UpdateUserAsync(User updatedUser)
        {
            var user = mapper.Map<User>(updatedUser);
            ConvertPasswordToHash(user);

            return await userRepository.UpdateUserAsync(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteUserAsync(id);
        }

        private static void ConvertPasswordToHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }

        public async Task<bool> ValidatePassWordAsync(User user)
        {
            var userDb = await userRepository.GetUserAsync(user.Id);
            if (userDb == null)
            {
                return false;
            }

            return await ValidateAndUpdateHashAsync(user, userDb.Password);
        }

        private async Task<bool> ValidateAndUpdateHashAsync(User user, string hash)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(user, hash, user.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed:
                    return false;
                case PasswordVerificationResult.Success:
                    return true;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    await UpdateUserAsync(user);
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
