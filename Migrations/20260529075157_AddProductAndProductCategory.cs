using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_test.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndProductCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocations_Warehouse_WarehouseId",
                table: "WarehouseLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseLocations",
                table: "WarehouseLocations");

            migrationBuilder.RenameTable(
                name: "WarehouseLocations",
                newName: "WarehouseLocation");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseLocations_WarehouseId_Code",
                table: "WarehouseLocation",
                newName: "IX_WarehouseLocation_WarehouseId_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseLocation",
                table: "WarehouseLocation",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory_ProductCategoryId",
                        column: x => x.ProductCategoryId,
                        principalTable: "ProductCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductCategoryId",
                table: "Product",
                column: "ProductCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                table: "WarehouseLocation",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WarehouseLocation_Warehouse_WarehouseId",
                table: "WarehouseLocation");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ProductCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WarehouseLocation",
                table: "WarehouseLocation");

            migrationBuilder.RenameTable(
                name: "WarehouseLocation",
                newName: "WarehouseLocations");

            migrationBuilder.RenameIndex(
                name: "IX_WarehouseLocation_WarehouseId_Code",
                table: "WarehouseLocations",
                newName: "IX_WarehouseLocations_WarehouseId_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WarehouseLocations",
                table: "WarehouseLocations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WarehouseLocations_Warehouse_WarehouseId",
                table: "WarehouseLocations",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
