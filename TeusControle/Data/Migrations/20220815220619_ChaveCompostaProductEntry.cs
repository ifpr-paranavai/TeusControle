using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChaveCompostaProductEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_entry_entries_Id",
                table: "products_entry");

            migrationBuilder.DropPrimaryKey(
                name: "PK_products_entry",
                table: "products_entry");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products_entry",
                table: "products_entry",
                columns: new[] { "Id", "Id2" });

            migrationBuilder.AddForeignKey(
                name: "FK_products_entry_entries_Id",
                table: "products_entry",
                column: "Id",
                principalTable: "entries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_entry_entries_Id",
                table: "products_entry"); 

            migrationBuilder.DropPrimaryKey(
                name: "PK_products_entry",
                table: "products_entry");

            migrationBuilder.AddPrimaryKey(
                name: "PK_products_entry",
                table: "products_entry",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_products_entry_entries_Id",
                table: "products_entry",
                column: "Id",
                principalTable: "entries",
                principalColumn: "Id");
        }
    }
}
