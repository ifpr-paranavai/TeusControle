using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeusControle.Domain.Models;

namespace TeusControle.Infrastructure.Context
{
    internal class ProductsMapping : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.ToTable("products");

            builder.Property(e => e.Id)
                .HasColumnName("id");

            builder.Property(e => e.Active)
                .HasColumnName("active")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'1'");

            builder.Property(e => e.AvgPrice)
                .HasColumnName("avg_price")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.BrandName)
                .HasColumnName("brand_name")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.BrandPicture)
                .HasColumnName("brand_picture")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by");

            builder.HasOne(e => e.CreatedByUser)
                .WithMany(i => i.Products)
                .HasForeignKey(e => e.CreatedBy);

            builder.Property(e => e.InStock)
                .HasColumnName("in_stock")
                .HasColumnType("decimal(10,2)"); ;

            builder.Property(e => e.CreatedDate)
                .HasColumnName("created_date")
                .HasColumnType("datetime")
                .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

            builder.Property(e => e.Deleted)
                .HasColumnName("deleted")
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasColumnName("description")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.GpcCode)
                .HasColumnName("gpc_code")
                .HasColumnType("varchar(30)");

            builder.Property(e => e.GpcDescription)
                .HasColumnName("gpc_description")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.GrossWeight)
                .HasColumnName("gross_weight")
                .HasColumnType("decimal(10,3)");

            builder.Property(e => e.Gtin)
                .HasColumnName("gtin")
                .HasColumnType("varchar(200)"); ;

            builder.Property(e => e.Height)
                .HasColumnName("height")
                .HasColumnType("decimal(10,3)");

            builder.Property(e => e.LastChange)
                .HasColumnName("last_change")
                .HasColumnType("datetime");

            builder.Property(e => e.Lenght)
                .HasColumnName("lenght")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.MaxPrice)
                .HasColumnName("max_price")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.NcmCode)
                .HasColumnName("ncm_code")
                .HasColumnType("varchar(20)");

            builder.Property(e => e.NcmDescription)
                .HasColumnName("ncm_description")
                .HasColumnType("varchar(100)");

            builder.Property(e => e.NcmFullDescription)
                .HasColumnName("ncm_full_description")
                .HasColumnType("varchar(200)");

            builder.Property(e => e.NetWeight)
                .HasColumnName("net_weight")
                .HasColumnType("decimal(10,3)");

            builder.Property(e => e.Price)
                .HasColumnName("price")
                .HasColumnType("decimal(10,2)");

            builder.Property(e => e.Thumbnail)
                .HasColumnName("thumbnail")
                .HasColumnType("varchar(300)");

            builder.Property(e => e.Width)
                .HasColumnName("width")
                .HasColumnType("decimal(10,3)");
        }
    }
}
