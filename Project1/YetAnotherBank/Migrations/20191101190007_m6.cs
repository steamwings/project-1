using Microsoft.EntityFrameworkCore.Migrations;

namespace YetAnotherBank.Migrations
{
    public partial class m6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountFromId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "AccountFromId",
                table: "Transactions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountFromId",
                table: "Transactions",
                column: "AccountFromId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Accounts_AccountFromId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "AccountFromId",
                table: "Transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Accounts_AccountFromId",
                table: "Transactions",
                column: "AccountFromId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
