using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class AddYearsToInterestRates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Years",
                table: "InterestRates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Years",
                table: "InterestRates");
        }
    }
}
