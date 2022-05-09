using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();

            builder.HasOne(p => p.CreatedByUser)
                .WithMany(p => p.CreatorUsers)
                .HasForeignKey(p => p.CreatedBy)
                .IsRequired(false);
        }
    }
}
