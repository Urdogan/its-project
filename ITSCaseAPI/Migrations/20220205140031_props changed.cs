using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSCaseAPI.Migrations
{
    public partial class propschanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "OrderCreationDate",
                table: "OrderItem",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCreationDate",
                table: "OrderItem");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
