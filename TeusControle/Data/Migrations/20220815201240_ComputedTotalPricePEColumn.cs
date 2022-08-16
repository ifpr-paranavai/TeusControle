using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ComputedTotalPricePEColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "products_entry",
                type: "decimal(65,30)",
                nullable: false,
                computedColumnSql: "Amount * UnitPrice",
                stored: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "products_entry",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)",
                oldComputedColumnSql: "Amount * UnitPrice");
        }
    }
}
