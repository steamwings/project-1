using Microsoft.EntityFrameworkCore.Migrations;

namespace YetAnotherBank.Migrations
{
    public partial class m3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "TransferResult",
                table: "Transactions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "AccountFromId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccountToId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountFromId",
                table: "Transactions",
                column: "AccountFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountToId",
                table: "Transactions",
                column: "AccountToId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountFromId",
                table: "Transactions",
                column: "AccountFromId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountToId",
                table: "Transactions",
                column: "AccountToId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Customers_CustomerId",
                table: "Transactions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountFromId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountToId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Customers_CustomerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountFromId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountToId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CustomerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountFromId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountToId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "TransferResult",
                table: "Transactions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                table: "Transactions",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountId",
                table: "Transactions",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
