using Core.Domain;
using Core.Shared.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(p => p.Name).HasMaxLength(200).IsRequired();

            builder.HasMany(p => p.CreatedUsers)
                .WithOne(p => p.CreatedByUser)
                .HasForeignKey(p => p.CreatedBy);

            builder.Property(p => p.ProfileType)
                .HasConversion(
                    p => p.ToString(),
                    p => (ProfileTypesEnum)Enum.Parse(typeof(ProfileTypesEnum), p));
        }
    }
}
