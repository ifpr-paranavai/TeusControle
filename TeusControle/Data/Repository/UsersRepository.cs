using Manager.Interfaces;
using Core.Domain;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyContext myContext;

        public UsersRepository(MyContext context)
        {
            this.myContext = context;
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await myContext.Users.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Users> GetUserAsync(int id)
        {
            return await myContext.Users.FindAsync(id);
        }

        public async Task<Users> InsertUserAsync(Users user)
        {
            await myContext.Users.AddAsync(user);
            await myContext.SaveChangesAsync();

            return user;
        }

        public async Task<Users> UpdateUserAsync(Users user)
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
