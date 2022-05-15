using Core.Domain;
using Core.Shared.Models.Request;
using Core.Shared.Models.User;
using Manager.Implementation.Base;
using Manager.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Manager.Implementation
{
    public partial class UserManager : BaseManager<User>, IUserManager
    {
        public async Task<string> ValidatePasswordGenerateTokenAsync(LoginRequest login)
        {

            var user = userRepository.Query(q => q.Email == login.Email && !q.Deleted)
                .FirstOrDefault();
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

        private static void ConvertPasswordToHash(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
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
                    await RehashUserPassword(user);
                    return true;
                default:
                    throw new InvalidOperationException();
            }
        }

        private async Task RehashUserPassword(User user)
        {
            ConvertPasswordToHash(user);
            await UpdateAsync<UserModel>(user);
        }
    }
}
