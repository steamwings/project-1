using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YetAnotherBankWeb.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Customers",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Username = table.Column<string>(fixedLength: true, maxLength: 10, nullable: false),
            //        PasswordHash = table.Column<byte[]>(fixedLength: true, maxLength: 64, nullable: false),
            //        Salt = table.Column<byte[]>(fixedLength: true, maxLength: 36, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Customers", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "InterestRates",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 50, nullable: false),
            //        Rate = table.Column<decimal>(type: "decimal(20, 6)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_InterestRates", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Accounts",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(maxLength: 50, nullable: false),
            //        Balance = table.Column<decimal>(type: "decimal(20, 6)", nullable: false),
            //        InterestId = table.Column<int>(nullable: false, defaultValueSql: "((1))"),
            //        Created = table.Column<DateTime>(type: "datetime", nullable: false),
            //        LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
            //        Active = table.Column<bool>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Accounts", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Accounts_InterestRates",
            //            column: x => x.InterestId,
            //            principalTable: "InterestRates",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "CustomersToAccounts",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AccountId = table.Column<long>(nullable: false),
            //        CustomerId = table.Column<long>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CustomersToAccounts", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_CustomersToAccounts_Accounts",
            //            column: x => x.AccountId,
            //            principalTable: "Accounts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_CustomersToAccounts_Customers",
            //            column: x => x.CustomerId,
            //            principalTable: "Customers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "DebtAccounts",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AccountId = table.Column<long>(nullable: false),
            //        PaymentAmount = table.Column<decimal>(type: "decimal(20, 6)", nullable: false),
            //        PaymentsBehind = table.Column<int>(nullable: false),
            //        NextPaymentDue = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_DebtAccounts", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_DebtAccounts_Accounts",
            //            column: x => x.AccountId,
            //            principalTable: "Accounts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "TermAccounts",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        AccountId = table.Column<long>(nullable: false),
            //        MaturationDate = table.Column<DateTime>(type: "datetime", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_TermAccounts", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_TermAccounts_Accounts",
            //            column: x => x.AccountId,
            //            principalTable: "Accounts",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Accounts_InterestId",
            //    table: "Accounts",
            //    column: "InterestId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CustomersToAccounts_AccountId",
            //    table: "CustomersToAccounts",
            //    column: "AccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_CustomersToAccounts_CustomerId",
            //    table: "CustomersToAccounts",
            //    column: "CustomerId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_DebtAccounts_AccountId",
            //    table: "DebtAccounts",
            //    column: "AccountId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_TermAccounts_AccountId",
            //    table: "TermAccounts",
            //    column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "CustomersToAccounts");

            //migrationBuilder.DropTable(
            //    name: "DebtAccounts");

            //migrationBuilder.DropTable(
            //    name: "TermAccounts");

            //migrationBuilder.DropTable(
            //    name: "Customers");

            //migrationBuilder.DropTable(
            //    name: "Accounts");

            //migrationBuilder.DropTable(
            //    name: "InterestRates");
        }
    }
}
