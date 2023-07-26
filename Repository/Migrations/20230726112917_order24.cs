using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class order24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "UserFavouritesId",
                table: "ShopApplicationUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopApplicationUsers_UserFavouritesId",
                table: "ShopApplicationUsers",
                column: "UserFavouritesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopApplicationUsers_Favourites_UserFavouritesId",
                table: "ShopApplicationUsers",
                column: "UserFavouritesId",
                principalTable: "Favourites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopApplicationUsers_Favourites_UserFavouritesId",
                table: "ShopApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ShopApplicationUsers_UserFavouritesId",
                table: "ShopApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UserFavouritesId",
                table: "ShopApplicationUsers");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingCartId",
                table: "ProductsInFavourites",
                type: "int",
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
    }
}
