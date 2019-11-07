using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class CascadeDeleteCTA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
