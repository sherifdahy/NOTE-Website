using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_receipt_to_database__ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_ApplicationUserId",
                table: "Receipts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_ApplicationUserId",
                table: "Receipts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_ApplicationUserId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_ApplicationUserId",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Receipts");
        }
    }
}
