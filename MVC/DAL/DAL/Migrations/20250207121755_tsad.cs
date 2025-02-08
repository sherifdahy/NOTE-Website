using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class tsad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxableItems");

            migrationBuilder.CreateTable(
                name: "TaxableItem",
                columns: table => new
                {
                    TaxableItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxableItem", x => x.TaxableItemId);
                    table.ForeignKey(
                        name: "FK_TaxableItem_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxableItem_ItemId",
                table: "TaxableItem",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxableItem");

            migrationBuilder.CreateTable(
                name: "TaxableItems",
                columns: table => new
                {
                    TaxableItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaxType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxableItems", x => x.TaxableItemsId);
                    table.ForeignKey(
                        name: "FK_TaxableItems_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxableItems_ItemId",
                table: "TaxableItems",
                column: "ItemId");
        }
    }
}
