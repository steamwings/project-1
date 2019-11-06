using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class AddTypeToInterestRate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "InterestRates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountTypes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InterestRates_TypeId",
                table: "InterestRates",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestRates_AccountTypes_TypeId",
                table: "InterestRates",
                column: "TypeId",
                principalTable: "AccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestRates_AccountTypes_TypeId",
                table: "InterestRates");

            migrationBuilder.DropIndex(
                name: "IX_InterestRates_TypeId",
                table: "InterestRates");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "InterestRates");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AccountTypes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
