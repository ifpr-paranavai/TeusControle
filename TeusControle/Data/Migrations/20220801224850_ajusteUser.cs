using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ajusteUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
