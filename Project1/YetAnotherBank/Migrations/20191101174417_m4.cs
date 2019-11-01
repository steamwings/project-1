using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YetAnotherBank.Migrations
{
    public partial class m4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InterestRateId",
                table: "Accounts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "NextPaymentDue",
                table: "Accounts",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "Accounts",
                type: "money",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentsBehind",
                table: "Accounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterestRates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestRates", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InterestRateId",
                table: "Accounts",
                column: "InterestRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_InterestRates_InterestRateId",
                table: "Accounts",
                column: "InterestRateId",
                principalTable: "InterestRates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_InterestRates_InterestRateId",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "InterestRates");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_InterestRateId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "InterestRateId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "NextPaymentDue",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PaymentsBehind",
                table: "Accounts");
        }
    }
}
