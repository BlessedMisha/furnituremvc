using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShoppingCartMvcUi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addpresition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogItems",
                table: "CatalogItems");

            migrationBuilder.RenameTable(
                name: "CatalogItems",
                newName: "CatalogItem");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CatalogItem",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogItem",
                table: "CatalogItem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CatalogItem",
                table: "CatalogItem");

            migrationBuilder.RenameTable(
                name: "CatalogItem",
                newName: "CatalogItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "CatalogItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)",
                oldPrecision: 9,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CatalogItems",
                table: "CatalogItems",
                column: "Id");
        }
    }
}
