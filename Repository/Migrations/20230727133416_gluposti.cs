using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class gluposti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserRatingTotal",
                table: "ShopApplicationUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRatingTotal",
                table: "ShopApplicationUsers");
        }
    }
}
