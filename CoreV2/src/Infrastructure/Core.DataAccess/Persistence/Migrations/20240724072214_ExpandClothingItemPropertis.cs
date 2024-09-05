using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpandClothingItemPropertis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "ClothingItems",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "ClothingItems",
                type: "int",
                maxLength: 10,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SourceRegion",
                table: "ClothingItems",
                type: "int",
                maxLength: 3,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "ClothingItems");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "ClothingItems");

            migrationBuilder.DropColumn(
                name: "SourceRegion",
                table: "ClothingItems");
        }
    }
}
