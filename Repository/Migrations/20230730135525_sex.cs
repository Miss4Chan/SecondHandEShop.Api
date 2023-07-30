using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class sex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductSex",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductSex",
                table: "Products");
        }
    }
}
