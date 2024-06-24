using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureShoppingCartMvcUi.Data.Migrations
{
    /// <inheritdoc />
    public partial class MaterialSubscribe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "CatalogItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Material",
                table: "CatalogItem");
        }
    }
}
