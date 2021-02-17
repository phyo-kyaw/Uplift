using Microsoft.EntityFrameworkCore.Migrations;

namespace Uplift.DataAccess.Migrations
{
    public partial class addOrderHeaderEmailToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "OrderHeader",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "OrderHeader");
        }
    }
}
