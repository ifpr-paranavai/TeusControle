using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class alteracoes_produtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrossWeight",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "products");

            migrationBuilder.DropColumn(
                name: "MaxPrice",
                table: "products");

            migrationBuilder.DropColumn(
                name: "NetWeight",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "GrossWeight",
                table: "products",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "products",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Length",
                table: "products",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaxPrice",
                table: "products",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "NetWeight",
                table: "products",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Width",
                table: "products",
                type: "decimal(65,30)",
                nullable: true);
        }
    }
}
