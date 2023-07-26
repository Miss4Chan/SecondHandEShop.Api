using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class order2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "ProductsInFavourites",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsInFavourites_ShoppingCartId",
                table: "ProductsInFavourites",
                column: "ShoppingCartId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsInFavourites_ShoppingCarts_ShoppingCartId",
                table: "ProductsInFavourites",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsInFavourites_ShoppingCarts_ShoppingCartId",
                table: "ProductsInFavourites");

            migrationBuilder.DropIndex(
                name: "IX_ProductsInFavourites_ShoppingCartId",
                table: "ProductsInFavourites");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "ProductsInFavourites");
        }
    }
}
