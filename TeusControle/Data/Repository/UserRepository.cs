using Manager.Interfaces;
using Core.Domain;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext myContext;

        public UserRepository(MyContext context)
        {
            this.myContext = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await myContext.Users.AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await myContext.Users.FindAsync(id);
        }

        public async Task<User> InsertUserAsync(User user)
        {
            user.CreatedBy = 1; // TODO: buscar do usuario logado
            await myContext.Users.AddAsync(user);
            await myContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            var dbUser = await GetUserAsync(user.Id);
            if (dbUser == null)
            {
                return null;
            }

            myContext.Entry(dbUser).CurrentValues.SetValues(user);
            await myContext.SaveChangesAsync();

            return dbUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            var dbUser = await GetUserAsync(id);
            myContext.Users.Remove(dbUser);
            await myContext.SaveChangesAsync();
        }
    }
}
