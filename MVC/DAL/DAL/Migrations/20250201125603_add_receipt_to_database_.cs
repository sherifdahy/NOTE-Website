using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_receipt_to_database_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchAddress",
                columns: table => new
                {
                    BranchAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Governate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchAddress", x => x.BranchAddressId);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeVersion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.DocumentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Header",
                columns: table => new
                {
                    HeaderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeIssued = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreviousUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceOldUUID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeRate = table.Column<int>(type: "int", nullable: false),
                    SOrderNameCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderdeliveryMode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Header", x => x.HeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Seller",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyTradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchAddressId = table.Column<int>(type: "int", nullable: false),
                    DeviceSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seller", x => x.SellerId);
                    table.ForeignKey(
                        name: "FK_Seller_BranchAddress_BranchAddressId",
                        column: x => x.BranchAddressId,
                        principalTable: "BranchAddress",
                        principalColumn: "BranchAddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HeaderId = table.Column<int>(type: "int", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    TotalSales = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCommercialDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_Receipts_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalTable: "DocumentType",
                        principalColumn: "DocumentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Header_HeaderId",
                        column: x => x.HeaderId,
                        principalTable: "Header",
                        principalColumn: "HeaderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipts_Seller_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Seller",
                        principalColumn: "SellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalSale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptId");
                });

            migrationBuilder.CreateTable(
                name: "TaxTotal",
                columns: table => new
                {
                    TaxTotalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxTotal", x => x.TaxTotalId);
                    table.ForeignKey(
                        name: "FK_TaxTotal_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "ReceiptId");
                });

            migrationBuilder.CreateTable(
                name: "CommercialDiscountData",
                columns: table => new
                {
                    CommercialDiscountDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommercialDiscountData", x => x.CommercialDiscountDataId);
                    table.ForeignKey(
                        name: "FK_CommercialDiscountData_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "ItemId");
                });

            migrationBuilder.CreateTable(
                name: "TaxableItems",
                columns: table => new
                {
                    TaxableItemsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: true)
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
                name: "IX_CommercialDiscountData_ItemId",
                table: "CommercialDiscountData",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ReceiptId",
                table: "Item",
                column: "ReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_DocumentTypeId",
                table: "Receipts",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_HeaderId",
                table: "Receipts",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_SellerId",
                table: "Receipts",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Seller_BranchAddressId",
                table: "Seller",
                column: "BranchAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxableItems_ItemId",
                table: "TaxableItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxTotal_ReceiptId",
                table: "TaxTotal",
                column: "ReceiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommercialDiscountData");

            migrationBuilder.DropTable(
                name: "TaxableItems");

            migrationBuilder.DropTable(
                name: "TaxTotal");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropTable(
                name: "Header");

            migrationBuilder.DropTable(
                name: "Seller");

            migrationBuilder.DropTable(
                name: "BranchAddress");
        }
    }
}
