using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class userUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ShopApplicationUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "ShopApplicationUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ShopApplicationUsers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "ShopApplicationUsers");
        }
    }
}
