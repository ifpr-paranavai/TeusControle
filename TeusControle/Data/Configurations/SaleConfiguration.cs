using Core.Domain;
using Core.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sales");

            builder.HasOne(p => p.CreatedByUser)
               .WithMany(p => p.Sales)
               .HasForeignKey(p => p.CreatedBy);

            builder.Property(p => p.Status)
                .HasConversion(
                    p => p.ToString(),
                    p => (SaleStatusEnum)Enum.Parse(typeof(SaleStatusEnum), p));

            builder.Property(p => p.TotalPrice)
                .HasDefaultValue(0);

            builder.Property(p => p.TotalDiscount)
                .HasDefaultValue(0);

            builder.Property(p => p.TotalOutPrice)
                .HasDefaultValue(0);
        }
    }
}
