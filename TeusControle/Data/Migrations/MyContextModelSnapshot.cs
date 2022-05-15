﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.10");

            modelBuilder.Entity("Core.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit(1)");

                    b.Property<decimal?>("AvgPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("BrandName")
                        .HasColumnType("longtext");

                    b.Property<string>("BrandPicture")
                        .HasColumnType("longtext");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit(1)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("GpcCode")
                        .HasColumnType("longtext");

                    b.Property<string>("GpcDescription")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("GrossWeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Gtin")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("InStock")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("Lenght")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("MaxPrice")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("NcmCode")
                        .HasColumnType("longtext");

                    b.Property<string>("NcmDescription")
                        .HasColumnType("longtext");

                    b.Property<string>("NcmFullDescription")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("NetWeight")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Thumbnail")
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Width")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("products");
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit(1)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CpfCnpj")
                        .HasColumnType("longtext");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit(1)");

                    b.Property<int>("DocumentType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastChange")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("longtext");

                    b.Property<string>("ProfileType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatedBy");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Core.Domain.Product", b =>
                {
                    b.HasOne("Core.Domain.User", "CreatedByUser")
                        .WithMany("Products")
                        .HasForeignKey("CreatedBy");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.HasOne("Core.Domain.User", "CreatedByUser")
                        .WithMany("CreatedUsers")
                        .HasForeignKey("CreatedBy");

                    b.Navigation("CreatedByUser");
                });

            modelBuilder.Entity("Core.Domain.User", b =>
                {
                    b.Navigation("CreatedUsers");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
