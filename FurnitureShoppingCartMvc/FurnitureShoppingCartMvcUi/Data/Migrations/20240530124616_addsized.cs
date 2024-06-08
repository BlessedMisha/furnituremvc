using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShoppingCartMvcUi.Data.Migrations
{
    /// <inheritdoc />
    public partial class addsized : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CatalogItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "CatalogItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "CatalogItem");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "CatalogItem");
        }
    }
}
