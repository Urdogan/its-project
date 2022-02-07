using Microsoft.EntityFrameworkCore.Migrations;

namespace ITSCaseAPI.Migrations
{
    public partial class propschanged2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerOrderAddress",
                table: "CustomerOrder",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerOrderAddress",
                table: "CustomerOrder");
        }
    }
}
