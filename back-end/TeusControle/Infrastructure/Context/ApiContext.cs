using Microsoft.EntityFrameworkCore;
using TeusControle.Domain.Models;

namespace TeusControle.Infrastructure.Context
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Entries> Entries { get; set; }
        public virtual DbSet<ProductEntries> ProductsEntries { get; set; }
        public virtual DbSet<Disposals> Disposals { get; set; }
        public virtual DbSet<ProductDisposals> ProductDisposals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductsMapping());
            modelBuilder.ApplyConfiguration(new UsersMapping());
            modelBuilder.ApplyConfiguration(new EntriesMapping());
            modelBuilder.ApplyConfiguration(new ProductEntriesMapping());
            modelBuilder.ApplyConfiguration(new DisposalsMapping());
            modelBuilder.ApplyConfiguration(new ProductDisposalsMapping());
        }
    }
}
