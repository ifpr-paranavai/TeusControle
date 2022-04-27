using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusControle.Domain.Models;

namespace TeusControle.Infrastructure.Context
{
    /// <summary>
    /// Mapping entidade associativa de produtos para saída de produtos
    /// </summary>
    internal class ProductDisposalsMapping : IEntityTypeConfiguration<ProductDisposals>
    {
        public void Configure(EntityTypeBuilder<ProductDisposals> builder)
        {
            builder.ToTable("product_disposals");
            builder.HasKey(e => new { 
                e.Id, 
                e.Id2
            });

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.HasOne(e => e.Disposal)
                .WithMany(i => i.ProductDisposals)
                .HasForeignKey(e => e.Id);

            builder.Property(e => e.Id2)
                .HasColumnName("id2");

            builder.HasOne(e => e.Product)
                .WithMany(i => i.ProductDisposals)
                .HasForeignKey(e => e.Id2);

            builder.Property(e => e.Amount)
                .HasColumnName("amount")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.UnitPrice)
                .HasColumnName("unit_price")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.PercentualDiscount)
                .HasColumnName("percentual_discount")
                .HasColumnType("int")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.TotalPrice)
                .HasColumnName("total_price")
                .HasColumnType("decimal(10,2)")
                .HasComputedColumnSql("[unit_price] * [amount]");

            builder.Property(e => e.LiquidPrice)
                .HasColumnName("liquid_price")
                .HasColumnType("decimal(10,2)")
                .HasComputedColumnSql("[total_price] -([total_price] * ([percentual_discount]/100))"); ;

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.HasOne(e => e.CreatedByUser)
                .WithMany(i => i.ProductDisposals)
                .HasForeignKey(e => e.CreatedBy);

            builder.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

            builder.Property(e => e.Deleted)
                .HasColumnName("deleted")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.LastChange)
                .HasColumnName("last_change")
                .HasColumnType("datetime");
        }
    }
}