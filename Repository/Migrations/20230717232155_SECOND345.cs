using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class SECOND345 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductsInShoppingCarts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductsInShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
