using Core.Domain;
using Core.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Configurations
{
    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.ToTable("entries");

            builder.HasOne(p => p.CreatedByUser)
               .WithMany(p => p.Entries)
               .HasForeignKey(p => p.CreatedBy);

            builder.Property(p => p.Status)
                .HasConversion(
                    p => p.ToString(),
                    p => (EntryStatusEnum)Enum.Parse(typeof(EntryStatusEnum), p));

            builder.Property(p => p.TotalPrice)
                .HasDefaultValue(0);
        }
    }
}
