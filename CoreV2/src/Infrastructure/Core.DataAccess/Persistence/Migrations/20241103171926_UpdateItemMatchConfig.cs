using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemMatchConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadMatches_ClothingItems_ClothingItemId",
                table: "UploadMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadMatches_UploadItems_UploadItemId",
                table: "UploadMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadMatches",
                table: "UploadMatches");

            migrationBuilder.RenameTable(
                name: "UploadMatches",
                newName: "UploadItemMatches");

            migrationBuilder.RenameIndex(
                name: "IX_UploadMatches_ClothingItemId",
                table: "UploadItemMatches",
                newName: "IX_UploadItemMatches_ClothingItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadItemMatches",
                table: "UploadItemMatches",
                columns: new[] { "UploadItemId", "ClothingItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UploadItemMatches_ClothingItems_ClothingItemId",
                table: "UploadItemMatches",
                column: "ClothingItemId",
                principalTable: "ClothingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadItemMatches_UploadItems_UploadItemId",
                table: "UploadItemMatches",
                column: "UploadItemId",
                principalTable: "UploadItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadItemMatches_ClothingItems_ClothingItemId",
                table: "UploadItemMatches");

            migrationBuilder.DropForeignKey(
                name: "FK_UploadItemMatches_UploadItems_UploadItemId",
                table: "UploadItemMatches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadItemMatches",
                table: "UploadItemMatches");

            migrationBuilder.RenameTable(
                name: "UploadItemMatches",
                newName: "UploadMatches");

            migrationBuilder.RenameIndex(
                name: "IX_UploadItemMatches_ClothingItemId",
                table: "UploadMatches",
                newName: "IX_UploadMatches_ClothingItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadMatches",
                table: "UploadMatches",
                columns: new[] { "UploadItemId", "ClothingItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UploadMatches_ClothingItems_ClothingItemId",
                table: "UploadMatches",
                column: "ClothingItemId",
                principalTable: "ClothingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UploadMatches_UploadItems_UploadItemId",
                table: "UploadMatches",
                column: "UploadItemId",
                principalTable: "UploadItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
