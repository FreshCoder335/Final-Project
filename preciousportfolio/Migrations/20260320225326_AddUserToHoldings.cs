using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace preciousportfolio.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToHoldings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Holdings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Holdings_UserId",
                table: "Holdings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holdings_AspNetUsers_UserId",
                table: "Holdings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holdings_AspNetUsers_UserId",
                table: "Holdings");

            migrationBuilder.DropIndex(
                name: "IX_Holdings_UserId",
                table: "Holdings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Holdings");
        }
    }
}
