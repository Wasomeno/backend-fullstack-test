using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend_fullstack_test.Migrations
{
    /// <inheritdoc />
    public partial class StockMovementSupplierRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "supplier_id",
                table: "stock_movement",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_stock_movement_supplier_id",
                table: "stock_movement",
                column: "supplier_id");

            migrationBuilder.AddForeignKey(
                name: "FK_stock_movement_supplier_supplier_id",
                table: "stock_movement",
                column: "supplier_id",
                principalTable: "supplier",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stock_movement_supplier_supplier_id",
                table: "stock_movement");

            migrationBuilder.DropIndex(
                name: "IX_stock_movement_supplier_id",
                table: "stock_movement");

            migrationBuilder.DropColumn(
                name: "supplier_id",
                table: "stock_movement");
        }
    }
}
