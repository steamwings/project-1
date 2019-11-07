using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class CascadeDeleteTermDebt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebtAccounts_Accounts",
                table: "DebtAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TermAccounts_Accounts",
                table: "TermAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_DebtAccounts_Accounts",
                table: "DebtAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TermAccounts_Accounts",
                table: "TermAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DebtAccounts_Accounts",
                table: "DebtAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TermAccounts_Accounts",
                table: "TermAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_DebtAccounts_Accounts",
                table: "DebtAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TermAccounts_Accounts",
                table: "TermAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
