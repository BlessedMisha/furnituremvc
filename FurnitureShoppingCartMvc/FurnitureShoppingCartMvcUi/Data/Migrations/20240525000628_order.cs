using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShoppingCartMvcUi.Data.Migrations
{
    /// <inheritdoc />
    public partial class order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderStatusId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Order",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "OrderStatusId",
                table: "Order",
                newName: "Quantity");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CatalogItemId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ItemPrice",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Order",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CatalogItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CatalogItemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ItemPrice",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Order",
                newName: "OrderStatusId");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Order",
                newName: "UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CatalogItem",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderStatusId",
                table: "Order",
                column: "OrderStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderStatus_OrderStatusId",
                table: "Order",
                column: "OrderStatusId",
                principalTable: "OrderStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
