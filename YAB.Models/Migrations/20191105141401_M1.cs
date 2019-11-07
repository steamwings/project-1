using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class M1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_DebtAccounts_Accounts",
                table: "DebtAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TermAccounts_Accounts",
                table: "TermAccounts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(20, 6)", nullable: false),
                    InterestId = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    Business = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_InterestRates",
                        column: x => x.InterestId,
                        principalTable: "InterestRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InterestId",
                table: "Accounts",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_DebtAccounts_Accounts",
                table: "DebtAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_TermAccounts_Accounts",
                table: "TermAccounts");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Customers",
                type: "binary(64)",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Salt",
                table: "Customers",
                type: "binary(36)",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Customers",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(20, 6)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_InterestRates",
                        column: x => x.InterestId,
                        principalTable: "InterestRates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_InterestId",
                table: "Accounts",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Accounts",
                table: "CustomersToAccounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
