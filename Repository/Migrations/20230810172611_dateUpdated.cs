using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class dateUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CommentDate",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "FormattedDate",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormattedTime",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormattedDate",
                table: "Comments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FormattedTime",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormattedDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FormattedTime",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "FormattedDate",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "FormattedTime",
                table: "Comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentDate",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
