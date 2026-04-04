using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace preciousportfolio.Migrations
{
    /// <inheritdoc />
    public partial class LinkSaleTransactionToHolding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HoldingId",
                table: "SaleTransactions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleTransactions_HoldingId",
                table: "SaleTransactions",
                column: "HoldingId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleTransactions_Holdings_HoldingId",
                table: "SaleTransactions",
                column: "HoldingId",
                principalTable: "Holdings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleTransactions_Holdings_HoldingId",
                table: "SaleTransactions");

            migrationBuilder.DropIndex(
                name: "IX_SaleTransactions_HoldingId",
                table: "SaleTransactions");

            migrationBuilder.DropColumn(
                name: "HoldingId",
                table: "SaleTransactions");
        }
    }
}
