using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AjusteProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lenght",
                table: "products",
                newName: "Length");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                table: "products",
                newName: "Lenght");
        }
    }
}
