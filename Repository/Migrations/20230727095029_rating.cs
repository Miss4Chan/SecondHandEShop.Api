using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class rating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "UserRating",
                table: "ShopApplicationUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UserRatingCount",
                table: "ShopApplicationUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRating",
                table: "ShopApplicationUsers");

            migrationBuilder.DropColumn(
                name: "UserRatingCount",
                table: "ShopApplicationUsers");
        }
    }
}
