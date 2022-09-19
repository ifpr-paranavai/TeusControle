using Core.Domain;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public new DbSet<Entry> Entry { get; set; }
        public DbSet<ProductEntry> ProductEntry { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<ProductSale> ProductSale { get; set; }

        public MyContext()
        {

        }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new EntryConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntryConfiguration());
            modelBuilder.ApplyConfiguration(new SaleConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSaleConfiguration());
        }
    }
}
