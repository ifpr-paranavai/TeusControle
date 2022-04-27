using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusControle.Domain.Models;

namespace TeusControle.Infrastructure.Context
{
    /// <summary>
    /// Mapping entidade users - usuário
    /// </summary>
    internal class EntriesMapping : IEntityTypeConfiguration<Entries>
    {
        public void Configure(EntityTypeBuilder<Entries> builder)
        {
            builder.ToTable("entries");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Description)
                .HasColumnName("description")
                .HasColumnType("varchar(300)");

            builder.Property(e => e.ClosingDate)
                .HasColumnName("closing_date")
                .HasColumnType("datetime");

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'1'"); 

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.HasOne(e => e.CreatedByUser)
                .WithMany(i => i.Entries)
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