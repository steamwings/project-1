using Microsoft.EntityFrameworkCore.Migrations;

namespace YAB.Migrations
{
    public partial class DeleteCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomersToAccounts_Customers",
                table: "CustomersToAccounts");

            migrationBuilder.DropTable(
                name: "Customers");

            //migrationBuilder.DropIndex(
            //    name: "IX_CustomersToAccounts_CustomerId",
            //    table: "CustomersToAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "CustomersToAccounts",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                table: "CustomersToAccounts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FakeId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
