using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class ProductSaleConfiguration : IEntityTypeConfiguration<ProductSale>
    {
        public void Configure(EntityTypeBuilder<ProductSale> builder)
        {
            builder.ToTable("products_sale");

            builder.HasKey(p => new { 
                p.Id, 
                p.Id2 
            });

            builder.HasOne(p => p.CreatedByUser)
               .WithMany(p => p.ProductsSale)
               .HasForeignKey(p => p.CreatedBy);

            builder.HasOne(p => p.Sale)
                .WithMany(p => p.ProductsSale)
                .HasForeignKey(p => p.Id);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.ProductsSale)
                .HasForeignKey(p => p.Id2);

            builder.Property(p => p.TotalPrice)
                .HasComputedColumnSql("Amount * UnitPrice", stored: true);

            builder.Property(p => p.UnitOutPrice)
                .HasComputedColumnSql("UnitPrice - Discount", stored: true);

            builder.Property(p => p.TotalOutPrice)
                .HasComputedColumnSql("Amount * UnitOutPrice", stored: true);

            builder.Property(p => p.Discount)
                .HasDefaultValue(0);
        }
    }
}
