using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class addedProductFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Condition",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductBrand",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductMaterial",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Condition",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductBrand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductMaterial",
                table: "Products");
        }
    }
}
