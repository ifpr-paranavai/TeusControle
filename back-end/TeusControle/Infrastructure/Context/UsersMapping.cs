using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusControle.Domain.Models;

namespace TeusControle.Infrastructure.Context
{
    /// <summary>
    /// Mapping entidade users - usuário
    /// </summary>
    internal class UsersMapping : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("users");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.BirthDate)
                .HasColumnName("birth_date")
                .HasColumnType("date");

            builder.Property(e => e.CpfCnpj)
                .IsRequired()
                .HasColumnName("cpf_cnpj")
                .HasColumnType("varchar(14)");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.HasOne(e => e.CreatedByUser)
                .WithMany(i => i.CreatorUsers)
                .HasForeignKey(e => e.CreatedBy);

            builder.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

            builder.Property(e => e.Deleted)
                .HasColumnName("deleted")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.DocumentType)
                .HasColumnName("document_type");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("varchar(100)");

            builder.Property(e => e.LastChange)
                .HasColumnName("last_change")
                .HasColumnType("datetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("name")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.Password)
                .IsRequired()
                .HasColumnName("password")
                .HasColumnType("varchar(50)");

            builder.Property(e => e.ProfileImage)
                .HasColumnName("profile_image")
                .HasColumnType("varchar(300)");

            builder.Property(e => e.ProfileType)
                .HasColumnName("profile_type");

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasColumnName("user_name")
                .HasColumnType("varchar(15)");
        }
    }
}