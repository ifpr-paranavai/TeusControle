using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("products");

            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();

            builder.HasOne(p => p.CreatedByUser)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.CreatedBy);
        }
    }
}
