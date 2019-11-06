using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class CustomerTemp1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FakeId",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FakeId",
                table: "Customers");
        }
    }
}
