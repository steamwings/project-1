using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountTypes_TypeId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentsBehind",
                table: "DebtAccounts",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Accounts",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Accounts",
                type: "datetime",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(20, 6)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(20, 6)");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTypes_TypeId",
                table: "Accounts",
                column: "TypeId",
                principalTable: "AccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AccountTypes_TypeId",
                table: "Accounts");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentsBehind",
                table: "DebtAccounts",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdated",
                table: "Accounts",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "Accounts",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Accounts",
                type: "decimal(20, 6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20, 6)",
                oldDefaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AccountTypes_TypeId",
                table: "Accounts",
                column: "TypeId",
                principalTable: "AccountTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
