using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_test.Migrations
{
    /// <inheritdoc />
    public partial class AddStockMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stock_movement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    movement_code = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    qty = table.Column<int>(type: "int", maxLength: 256, nullable: false),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    created_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    completed_by_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    completed_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    warehouse_location_from_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    warehouse_location_to_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stock_movement", x => x.id);
                    table.ForeignKey(
                        name: "FK_stock_movement_Product_product_id",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stock_movement_WarehouseLocation_warehouse_location_from_id",
                        column: x => x.warehouse_location_from_id,
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stock_movement_WarehouseLocation_warehouse_location_to_id",
                        column: x => x.warehouse_location_to_id,
                        principalTable: "WarehouseLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stock_movement_user_completed_by_id",
                        column: x => x.completed_by_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_stock_movement_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_stock_movement_completed_by_id",
                table: "stock_movement",
                column: "completed_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_movement_created_by_id",
                table: "stock_movement",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_movement_product_id",
                table: "stock_movement",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_movement_warehouse_location_from_id",
                table: "stock_movement",
                column: "warehouse_location_from_id");

            migrationBuilder.CreateIndex(
                name: "IX_stock_movement_warehouse_location_to_id",
                table: "stock_movement",
                column: "warehouse_location_to_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "stock_movement");
        }
    }
}
