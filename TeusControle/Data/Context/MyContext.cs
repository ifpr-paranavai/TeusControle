using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<Users> Users { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
    }
}
