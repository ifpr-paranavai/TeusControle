using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductEntryConfiguration : IEntityTypeConfiguration<ProductEntry>
    {
        public void Configure(EntityTypeBuilder<ProductEntry> builder)
        {
            builder.ToTable("products_entry");

            builder.HasKey(p => new { 
                p.Id, 
                p.Id2 
            });

            builder.HasOne(p => p.CreatedByUser)
               .WithMany(p => p.ProductsEntry)
               .HasForeignKey(p => p.CreatedBy);

            builder.HasOne(p => p.Entry)
                .WithMany(p => p.ProductsEntry)
                .HasForeignKey(p => p.Id);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductsEntry)
                .HasForeignKey(p => p.Id2);

            builder.Property(p => p.TotalPrice)
                .HasComputedColumnSql("Amount * UnitPrice", stored: true);
        }
    }
}
