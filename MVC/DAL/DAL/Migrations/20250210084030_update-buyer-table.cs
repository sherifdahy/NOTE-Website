using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class updatebuyertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommercialDiscountData_Item_ItemId",
                table: "CommercialDiscountData");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Receipts_ReceiptId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_ApplicationUserId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Buyer_BuyerId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_DocumentType_DocumentTypeId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Header_HeaderId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Seller_SellerId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Seller_BranchAddress_BranchAddressId",
                table: "Seller");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxableItem_Item_ItemId",
                table: "TaxableItem");

            migrationBuilder.DropForeignKey(
                name: "FK_TaxTotal_Receipts_ReceiptId",
                table: "TaxTotal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxTotal",
                table: "TaxTotal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaxableItem",
                table: "TaxableItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Seller",
                table: "Seller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Header",
                table: "Header");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommercialDiscountData",
                table: "CommercialDiscountData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Buyer",
                table: "Buyer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BranchAddress",
                table: "BranchAddress");

            migrationBuilder.RenameTable(
                name: "TaxTotal",
                newName: "taxTotal");

            migrationBuilder.RenameTable(
                name: "TaxableItem",
                newName: "taxableItem");

            migrationBuilder.RenameTable(
                name: "Seller",
                newName: "seller");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "item");

            migrationBuilder.RenameTable(
                name: "Header",
                newName: "header");

            migrationBuilder.RenameTable(
                name: "DocumentType",
                newName: "documentType");

            migrationBuilder.RenameTable(
                name: "CommercialDiscountData",
                newName: "commercialDiscountData");

            migrationBuilder.RenameTable(
                name: "Buyer",
                newName: "buyer");

            migrationBuilder.RenameTable(
                name: "BranchAddress",
                newName: "branchAddress");

            migrationBuilder.RenameColumn(
                name: "TaxType",
                table: "taxTotal",
                newName: "taxType");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "taxTotal",
                newName: "receiptId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "taxTotal",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "TaxTotalId",
                table: "taxTotal",
                newName: "taxTotalId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxTotal_ReceiptId",
                table: "taxTotal",
                newName: "IX_taxTotal_receiptId");

            migrationBuilder.RenameColumn(
                name: "TaxType",
                table: "taxableItem",
                newName: "taxType");

            migrationBuilder.RenameColumn(
                name: "SubType",
                table: "taxableItem",
                newName: "subType");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "taxableItem",
                newName: "rate");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "taxableItem",
                newName: "itemId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "taxableItem",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "TaxableItemId",
                table: "taxableItem",
                newName: "taxableItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TaxableItem_ItemId",
                table: "taxableItem",
                newName: "IX_taxableItem_itemId");

            migrationBuilder.RenameColumn(
                name: "Rin",
                table: "seller",
                newName: "rin");

            migrationBuilder.RenameColumn(
                name: "DeviceSerialNumber",
                table: "seller",
                newName: "deviceSerialNumber");

            migrationBuilder.RenameColumn(
                name: "CompanyTradeName",
                table: "seller",
                newName: "companyTradeName");

            migrationBuilder.RenameColumn(
                name: "BranchCode",
                table: "seller",
                newName: "branchCode");

            migrationBuilder.RenameColumn(
                name: "BranchAddressId",
                table: "seller",
                newName: "branchAddressId");

            migrationBuilder.RenameColumn(
                name: "ActivityCode",
                table: "seller",
                newName: "activityCode");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "seller",
                newName: "sellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Seller_BranchAddressId",
                table: "seller",
                newName: "IX_seller_branchAddressId");

            migrationBuilder.RenameColumn(
                name: "TotalSales",
                table: "Receipts",
                newName: "totalSales");

            migrationBuilder.RenameColumn(
                name: "TotalCommercialDiscount",
                table: "Receipts",
                newName: "totalCommercialDiscount");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Receipts",
                newName: "totalAmount");

            migrationBuilder.RenameColumn(
                name: "SellerId",
                table: "Receipts",
                newName: "sellerId");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Receipts",
                newName: "paymentMethod");

            migrationBuilder.RenameColumn(
                name: "NetAmount",
                table: "Receipts",
                newName: "netAmount");

            migrationBuilder.RenameColumn(
                name: "HeaderId",
                table: "Receipts",
                newName: "headerId");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                table: "Receipts",
                newName: "documentTypeId");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "Receipts",
                newName: "buyerId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Receipts",
                newName: "applicationUserId");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "Receipts",
                newName: "receiptId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_SellerId",
                table: "Receipts",
                newName: "IX_Receipts_sellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_HeaderId",
                table: "Receipts",
                newName: "IX_Receipts_headerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_DocumentTypeId",
                table: "Receipts",
                newName: "IX_Receipts_documentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_BuyerId",
                table: "Receipts",
                newName: "IX_Receipts_buyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_ApplicationUserId",
                table: "Receipts",
                newName: "IX_Receipts_applicationUserId");

            migrationBuilder.RenameColumn(
                name: "UnitType",
                table: "item",
                newName: "unitType");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "item",
                newName: "unitPrice");

            migrationBuilder.RenameColumn(
                name: "TotalSale",
                table: "item",
                newName: "totalSale");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "item",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "ReceiptId",
                table: "item",
                newName: "receiptId");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "item",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "NetSale",
                table: "item",
                newName: "netSale");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "item",
                newName: "itemType");

            migrationBuilder.RenameColumn(
                name: "ItemCode",
                table: "item",
                newName: "itemCode");

            migrationBuilder.RenameColumn(
                name: "InternalCode",
                table: "item",
                newName: "internalCode");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "item",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "item",
                newName: "itemId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_ReceiptId",
                table: "item",
                newName: "IX_item_receiptId");

            migrationBuilder.RenameColumn(
                name: "Uuid",
                table: "header",
                newName: "uuid");

            migrationBuilder.RenameColumn(
                name: "SOrderNameCode",
                table: "header",
                newName: "sOrderNameCode");

            migrationBuilder.RenameColumn(
                name: "ReferenceOldUUID",
                table: "header",
                newName: "referenceOldUUID");

            migrationBuilder.RenameColumn(
                name: "ReceiptNumber",
                table: "header",
                newName: "receiptNumber");

            migrationBuilder.RenameColumn(
                name: "PreviousUUID",
                table: "header",
                newName: "previousUUID");

            migrationBuilder.RenameColumn(
                name: "OrderdeliveryMode",
                table: "header",
                newName: "orderdeliveryMode");

            migrationBuilder.RenameColumn(
                name: "ExchangeRate",
                table: "header",
                newName: "exchangeRate");

            migrationBuilder.RenameColumn(
                name: "DateTimeIssued",
                table: "header",
                newName: "dateTimeIssued");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "header",
                newName: "currency");

            migrationBuilder.RenameColumn(
                name: "HeaderId",
                table: "header",
                newName: "headerId");

            migrationBuilder.RenameColumn(
                name: "TypeVersion",
                table: "documentType",
                newName: "typeVersion");

            migrationBuilder.RenameColumn(
                name: "ReceiptType",
                table: "documentType",
                newName: "receiptType");

            migrationBuilder.RenameColumn(
                name: "DocumentTypeId",
                table: "documentType",
                newName: "documentTypeId");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "commercialDiscountData",
                newName: "rate");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "commercialDiscountData",
                newName: "itemId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "commercialDiscountData",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "commercialDiscountData",
                newName: "amount");

            migrationBuilder.RenameColumn(
                name: "CommercialDiscountDataId",
                table: "commercialDiscountData",
                newName: "commercialDiscountDataId");

            migrationBuilder.RenameIndex(
                name: "IX_CommercialDiscountData_ItemId",
                table: "commercialDiscountData",
                newName: "IX_commercialDiscountData_itemId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "buyer",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "PaymentNumber",
                table: "buyer",
                newName: "paymentNumber");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "buyer",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "MobileNumber",
                table: "buyer",
                newName: "mobileNumber");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "buyer",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                table: "buyer",
                newName: "buyerId");

            migrationBuilder.RenameColumn(
                name: "Street",
                table: "branchAddress",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "RegionCity",
                table: "branchAddress",
                newName: "regionCity");

            migrationBuilder.RenameColumn(
                name: "Governate",
                table: "branchAddress",
                newName: "governate");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "branchAddress",
                newName: "country");

            migrationBuilder.RenameColumn(
                name: "BuildingNumber",
                table: "branchAddress",
                newName: "buildingNumber");

            migrationBuilder.RenameColumn(
                name: "BranchAddressId",
                table: "branchAddress",
                newName: "branchAddressId");

            migrationBuilder.AlterColumn<string>(
                name: "paymentNumber",
                table: "buyer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "mobileNumber",
                table: "buyer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "buyer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taxTotal",
                table: "taxTotal",
                column: "taxTotalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_taxableItem",
                table: "taxableItem",
                column: "taxableItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_seller",
                table: "seller",
                column: "sellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_item",
                table: "item",
                column: "itemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_header",
                table: "header",
                column: "headerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_documentType",
                table: "documentType",
                column: "documentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_commercialDiscountData",
                table: "commercialDiscountData",
                column: "commercialDiscountDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_buyer",
                table: "buyer",
                column: "buyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_branchAddress",
                table: "branchAddress",
                column: "branchAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_commercialDiscountData_item_itemId",
                table: "commercialDiscountData",
                column: "itemId",
                principalTable: "item",
                principalColumn: "itemId");

            migrationBuilder.AddForeignKey(
                name: "FK_item_Receipts_receiptId",
                table: "item",
                column: "receiptId",
                principalTable: "Receipts",
                principalColumn: "receiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_applicationUserId",
                table: "Receipts",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_buyer_buyerId",
                table: "Receipts",
                column: "buyerId",
                principalTable: "buyer",
                principalColumn: "buyerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_documentType_documentTypeId",
                table: "Receipts",
                column: "documentTypeId",
                principalTable: "documentType",
                principalColumn: "documentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_header_headerId",
                table: "Receipts",
                column: "headerId",
                principalTable: "header",
                principalColumn: "headerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_seller_sellerId",
                table: "Receipts",
                column: "sellerId",
                principalTable: "seller",
                principalColumn: "sellerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seller_branchAddress_branchAddressId",
                table: "seller",
                column: "branchAddressId",
                principalTable: "branchAddress",
                principalColumn: "branchAddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_taxableItem_item_itemId",
                table: "taxableItem",
                column: "itemId",
                principalTable: "item",
                principalColumn: "itemId");

            migrationBuilder.AddForeignKey(
                name: "FK_taxTotal_Receipts_receiptId",
                table: "taxTotal",
                column: "receiptId",
                principalTable: "Receipts",
                principalColumn: "receiptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_commercialDiscountData_item_itemId",
                table: "commercialDiscountData");

            migrationBuilder.DropForeignKey(
                name: "FK_item_Receipts_receiptId",
                table: "item");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_applicationUserId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_buyer_buyerId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_documentType_documentTypeId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_header_headerId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_seller_sellerId",
                table: "Receipts");

            migrationBuilder.DropForeignKey(
                name: "FK_seller_branchAddress_branchAddressId",
                table: "seller");

            migrationBuilder.DropForeignKey(
                name: "FK_taxableItem_item_itemId",
                table: "taxableItem");

            migrationBuilder.DropForeignKey(
                name: "FK_taxTotal_Receipts_receiptId",
                table: "taxTotal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taxTotal",
                table: "taxTotal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_taxableItem",
                table: "taxableItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_seller",
                table: "seller");

            migrationBuilder.DropPrimaryKey(
                name: "PK_item",
                table: "item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_header",
                table: "header");

            migrationBuilder.DropPrimaryKey(
                name: "PK_documentType",
                table: "documentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_commercialDiscountData",
                table: "commercialDiscountData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_buyer",
                table: "buyer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_branchAddress",
                table: "branchAddress");

            migrationBuilder.RenameTable(
                name: "taxTotal",
                newName: "TaxTotal");

            migrationBuilder.RenameTable(
                name: "taxableItem",
                newName: "TaxableItem");

            migrationBuilder.RenameTable(
                name: "seller",
                newName: "Seller");

            migrationBuilder.RenameTable(
                name: "item",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "header",
                newName: "Header");

            migrationBuilder.RenameTable(
                name: "documentType",
                newName: "DocumentType");

            migrationBuilder.RenameTable(
                name: "commercialDiscountData",
                newName: "CommercialDiscountData");

            migrationBuilder.RenameTable(
                name: "buyer",
                newName: "Buyer");

            migrationBuilder.RenameTable(
                name: "branchAddress",
                newName: "BranchAddress");

            migrationBuilder.RenameColumn(
                name: "taxType",
                table: "TaxTotal",
                newName: "TaxType");

            migrationBuilder.RenameColumn(
                name: "receiptId",
                table: "TaxTotal",
                newName: "ReceiptId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "TaxTotal",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "taxTotalId",
                table: "TaxTotal",
                newName: "TaxTotalId");

            migrationBuilder.RenameIndex(
                name: "IX_taxTotal_receiptId",
                table: "TaxTotal",
                newName: "IX_TaxTotal_ReceiptId");

            migrationBuilder.RenameColumn(
                name: "taxType",
                table: "TaxableItem",
                newName: "TaxType");

            migrationBuilder.RenameColumn(
                name: "subType",
                table: "TaxableItem",
                newName: "SubType");

            migrationBuilder.RenameColumn(
                name: "rate",
                table: "TaxableItem",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "TaxableItem",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "TaxableItem",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "taxableItemId",
                table: "TaxableItem",
                newName: "TaxableItemId");

            migrationBuilder.RenameIndex(
                name: "IX_taxableItem_itemId",
                table: "TaxableItem",
                newName: "IX_TaxableItem_ItemId");

            migrationBuilder.RenameColumn(
                name: "rin",
                table: "Seller",
                newName: "Rin");

            migrationBuilder.RenameColumn(
                name: "deviceSerialNumber",
                table: "Seller",
                newName: "DeviceSerialNumber");

            migrationBuilder.RenameColumn(
                name: "companyTradeName",
                table: "Seller",
                newName: "CompanyTradeName");

            migrationBuilder.RenameColumn(
                name: "branchCode",
                table: "Seller",
                newName: "BranchCode");

            migrationBuilder.RenameColumn(
                name: "branchAddressId",
                table: "Seller",
                newName: "BranchAddressId");

            migrationBuilder.RenameColumn(
                name: "activityCode",
                table: "Seller",
                newName: "ActivityCode");

            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Seller",
                newName: "SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_seller_branchAddressId",
                table: "Seller",
                newName: "IX_Seller_BranchAddressId");

            migrationBuilder.RenameColumn(
                name: "totalSales",
                table: "Receipts",
                newName: "TotalSales");

            migrationBuilder.RenameColumn(
                name: "totalCommercialDiscount",
                table: "Receipts",
                newName: "TotalCommercialDiscount");

            migrationBuilder.RenameColumn(
                name: "totalAmount",
                table: "Receipts",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "sellerId",
                table: "Receipts",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "paymentMethod",
                table: "Receipts",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "netAmount",
                table: "Receipts",
                newName: "NetAmount");

            migrationBuilder.RenameColumn(
                name: "headerId",
                table: "Receipts",
                newName: "HeaderId");

            migrationBuilder.RenameColumn(
                name: "documentTypeId",
                table: "Receipts",
                newName: "DocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "buyerId",
                table: "Receipts",
                newName: "BuyerId");

            migrationBuilder.RenameColumn(
                name: "applicationUserId",
                table: "Receipts",
                newName: "ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "receiptId",
                table: "Receipts",
                newName: "ReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_sellerId",
                table: "Receipts",
                newName: "IX_Receipts_SellerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_headerId",
                table: "Receipts",
                newName: "IX_Receipts_HeaderId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_documentTypeId",
                table: "Receipts",
                newName: "IX_Receipts_DocumentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_buyerId",
                table: "Receipts",
                newName: "IX_Receipts_BuyerId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_applicationUserId",
                table: "Receipts",
                newName: "IX_Receipts_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "unitType",
                table: "Item",
                newName: "UnitType");

            migrationBuilder.RenameColumn(
                name: "unitPrice",
                table: "Item",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "totalSale",
                table: "Item",
                newName: "TotalSale");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "Item",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "receiptId",
                table: "Item",
                newName: "ReceiptId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "Item",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "netSale",
                table: "Item",
                newName: "NetSale");

            migrationBuilder.RenameColumn(
                name: "itemType",
                table: "Item",
                newName: "ItemType");

            migrationBuilder.RenameColumn(
                name: "itemCode",
                table: "Item",
                newName: "ItemCode");

            migrationBuilder.RenameColumn(
                name: "internalCode",
                table: "Item",
                newName: "InternalCode");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Item",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "Item",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_item_receiptId",
                table: "Item",
                newName: "IX_Item_ReceiptId");

            migrationBuilder.RenameColumn(
                name: "uuid",
                table: "Header",
                newName: "Uuid");

            migrationBuilder.RenameColumn(
                name: "sOrderNameCode",
                table: "Header",
                newName: "SOrderNameCode");

            migrationBuilder.RenameColumn(
                name: "referenceOldUUID",
                table: "Header",
                newName: "ReferenceOldUUID");

            migrationBuilder.RenameColumn(
                name: "receiptNumber",
                table: "Header",
                newName: "ReceiptNumber");

            migrationBuilder.RenameColumn(
                name: "previousUUID",
                table: "Header",
                newName: "PreviousUUID");

            migrationBuilder.RenameColumn(
                name: "orderdeliveryMode",
                table: "Header",
                newName: "OrderdeliveryMode");

            migrationBuilder.RenameColumn(
                name: "exchangeRate",
                table: "Header",
                newName: "ExchangeRate");

            migrationBuilder.RenameColumn(
                name: "dateTimeIssued",
                table: "Header",
                newName: "DateTimeIssued");

            migrationBuilder.RenameColumn(
                name: "currency",
                table: "Header",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "headerId",
                table: "Header",
                newName: "HeaderId");

            migrationBuilder.RenameColumn(
                name: "typeVersion",
                table: "DocumentType",
                newName: "TypeVersion");

            migrationBuilder.RenameColumn(
                name: "receiptType",
                table: "DocumentType",
                newName: "ReceiptType");

            migrationBuilder.RenameColumn(
                name: "documentTypeId",
                table: "DocumentType",
                newName: "DocumentTypeId");

            migrationBuilder.RenameColumn(
                name: "rate",
                table: "CommercialDiscountData",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "itemId",
                table: "CommercialDiscountData",
                newName: "ItemId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "CommercialDiscountData",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "amount",
                table: "CommercialDiscountData",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "commercialDiscountDataId",
                table: "CommercialDiscountData",
                newName: "CommercialDiscountDataId");

            migrationBuilder.RenameIndex(
                name: "IX_commercialDiscountData_itemId",
                table: "CommercialDiscountData",
                newName: "IX_CommercialDiscountData_ItemId");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "Buyer",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "paymentNumber",
                table: "Buyer",
                newName: "PaymentNumber");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Buyer",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "mobileNumber",
                table: "Buyer",
                newName: "MobileNumber");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Buyer",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "buyerId",
                table: "Buyer",
                newName: "BuyerId");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "BranchAddress",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "regionCity",
                table: "BranchAddress",
                newName: "RegionCity");

            migrationBuilder.RenameColumn(
                name: "governate",
                table: "BranchAddress",
                newName: "Governate");

            migrationBuilder.RenameColumn(
                name: "country",
                table: "BranchAddress",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "buildingNumber",
                table: "BranchAddress",
                newName: "BuildingNumber");

            migrationBuilder.RenameColumn(
                name: "branchAddressId",
                table: "BranchAddress",
                newName: "BranchAddressId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentNumber",
                table: "Buyer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "Buyer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Buyer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxTotal",
                table: "TaxTotal",
                column: "TaxTotalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaxableItem",
                table: "TaxableItem",
                column: "TaxableItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Seller",
                table: "Seller",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Header",
                table: "Header",
                column: "HeaderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentType",
                table: "DocumentType",
                column: "DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommercialDiscountData",
                table: "CommercialDiscountData",
                column: "CommercialDiscountDataId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Buyer",
                table: "Buyer",
                column: "BuyerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BranchAddress",
                table: "BranchAddress",
                column: "BranchAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommercialDiscountData_Item_ItemId",
                table: "CommercialDiscountData",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Receipts_ReceiptId",
                table: "Item",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_ApplicationUserId",
                table: "Receipts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Buyer_BuyerId",
                table: "Receipts",
                column: "BuyerId",
                principalTable: "Buyer",
                principalColumn: "BuyerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_DocumentType_DocumentTypeId",
                table: "Receipts",
                column: "DocumentTypeId",
                principalTable: "DocumentType",
                principalColumn: "DocumentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Header_HeaderId",
                table: "Receipts",
                column: "HeaderId",
                principalTable: "Header",
                principalColumn: "HeaderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Seller_SellerId",
                table: "Receipts",
                column: "SellerId",
                principalTable: "Seller",
                principalColumn: "SellerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Seller_BranchAddress_BranchAddressId",
                table: "Seller",
                column: "BranchAddressId",
                principalTable: "BranchAddress",
                principalColumn: "BranchAddressId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TaxableItem_Item_ItemId",
                table: "TaxableItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaxTotal_Receipts_ReceiptId",
                table: "TaxTotal",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "ReceiptId");
        }
    }
}
