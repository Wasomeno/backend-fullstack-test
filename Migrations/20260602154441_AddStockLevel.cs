using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_test.Migrations
{
    /// <inheritdoc />
    public partial class AddStockLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stock_level",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    warehouse_location_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 256, nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 256, nullable: false),
                    qty = table.Column<int>(type: "int", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_level", x => x.id);
                    table.ForeignKey(
                        name: "FK_stock_level_Product_product_id",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stock_level_WarehouseLocation_warehouse_location_id",
                        column: x => x.warehouse_location_id,
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stock_level_product_id",
                table: "stock_level",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_level_warehouse_location_id",
                table: "stock_level",
                column: "warehouse_location_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_level");
        }
    }
}
