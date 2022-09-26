using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class campoDescontoTotalCalculado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalDiscount",
                table: "products_sale",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m,
                computedColumnSql: "Amount * Discount", 
                stored: true
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalDiscount",
                table: "products_sale");
        }
    }
}
