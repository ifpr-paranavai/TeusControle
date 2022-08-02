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

            builder.HasOne(p => p.CreatedByUser)
               .WithMany(p => p.ProductsEntry)
               .HasForeignKey(p => p.CreatedBy);

            builder.HasOne(p => p.Entry)
                .WithMany(p => p.ProductsEntry)
                .HasForeignKey(p => p.EntryId);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductsEntry)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
