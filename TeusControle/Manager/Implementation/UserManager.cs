using AutoMapper;
using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.User;
using Manager.Interfaces;
using Manager.Interfaces.Services;
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
        private readonly IJwtService jwtService;

        public UserManager(IUserRepository usersRepository, IMapper mapper, IJwtService jwtService)
        {
            this.userRepository = usersRepository;
            this.mapper = mapper;
            this.jwtService = jwtService;
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

        public async Task<string> ValidatePasswordGenerateTokenAsync(LoginRequest login)
        {
            var user = await userRepository.GetUserByEmailAsync(login.Email);
            if (user == null)
            {
                return null;
            }

            if (await ValidateAndUpdateHashAsync(user, login.Password))
            {
                return jwtService.GenerateToken(user);
            }
            return null;
        }

        private async Task<bool> ValidateAndUpdateHashAsync(User user, string loginPassword)
        {
            var passwordHasher = new PasswordHasher<User>();
            var status = passwordHasher.VerifyHashedPassword(user, user.Password, loginPassword);
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
