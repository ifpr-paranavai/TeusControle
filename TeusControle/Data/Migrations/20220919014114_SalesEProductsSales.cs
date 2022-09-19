using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class SalesEProductsSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClosingDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CpfCnpj = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, defaultValue: 0m),
                    TotalOutPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, defaultValue: 0m),
                    TotalDiscount = table.Column<decimal>(type: "decimal(65,30)", nullable: false, defaultValue: 0m),
                    Active = table.Column<bool>(type: "bit(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastChange = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sales_users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "products_sale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Id2 = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    UnitOutPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, computedColumnSql: "UnitPrice - Discount", stored: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, computedColumnSql: "Amount * UnitPrice", stored: true),
                    TotalOutPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false, computedColumnSql: "Amount * UnitOutPrice", stored: true),
                    Discount = table.Column<decimal>(type: "decimal(65,30)", nullable: false, defaultValue: 0m),
                    Active = table.Column<bool>(type: "bit(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastChange = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_sale", x => new { x.Id, x.Id2 });
                    table.ForeignKey(
                        name: "FK_products_sale_products_Id2",
                        column: x => x.Id2,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_sale_sales_Id",
                        column: x => x.Id,
                        principalTable: "sales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_sale_users_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_products_sale_CreatedBy",
                table: "products_sale",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_products_sale_Id2",
                table: "products_sale",
                column: "Id2");

            migrationBuilder.CreateIndex(
                name: "IX_sales_CreatedBy",
                table: "sales",
                column: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_sale");

            migrationBuilder.DropTable(
                name: "sales");
        }
    }
}
