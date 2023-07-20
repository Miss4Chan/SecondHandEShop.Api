using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class productChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProductAvailablity",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ProductColor",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductMeasurements",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ProductPrice",
                table: "Products",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "ProductSize",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductSizeNumber",
                table: "Products",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductType",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductAvailablity",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductColor",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductMeasurements",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSize",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductSizeNumber",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductType",
                table: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
