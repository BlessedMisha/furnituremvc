using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShoppingCartMvcUi.Data.Migrations
{
    /// <inheritdoc />
    public partial class plsworkorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatalogItemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ItemPrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "ItemName",
                table: "Order",
                newName: "OrderItemsJson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderItemsJson",
                table: "Order",
                newName: "ItemName");

            migrationBuilder.AddColumn<int>(
                name: "CatalogItemId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ItemPrice",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
