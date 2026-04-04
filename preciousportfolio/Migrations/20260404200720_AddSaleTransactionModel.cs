using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace preciousportfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleTransactionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_AspNetUsers_UserId",
                table: "Holdings");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Holdings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "SaleTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MetalType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    WeightOz = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateSold = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Proceeds = table.Column<decimal>(type: "TEXT", nullable: false),
                    CostBasis = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleTransactions_UserId",
                table: "SaleTransactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_AspNetUsers_UserId",
                table: "Holdings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_AspNetUsers_UserId",
                table: "Holdings");

            migrationBuilder.DropTable(
                name: "SaleTransactions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Holdings",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_AspNetUsers_UserId",
                table: "Holdings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
