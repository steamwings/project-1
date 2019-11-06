using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class AddCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomersToAccounts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomersToAccounts_CustomerId",
                table: "CustomersToAccounts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_CustomersToAccounts_CustomerId",
                table: "CustomersToAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomersToAccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
