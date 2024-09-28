using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddColourMatching : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadMatches_Uploads_UploadId",
                table: "UploadMatches");

            migrationBuilder.DropColumn(
                name: "UploadItemDescription",
                table: "UploadMatches");

            migrationBuilder.DropColumn(
                name: "UploadItemEmbedding",
                table: "UploadMatches");

            migrationBuilder.RenameColumn(
                name: "Similarity",
                table: "UploadMatches",
                newName: "EmbeddingSimilarity");

            migrationBuilder.RenameColumn(
                name: "UploadId",
                table: "UploadMatches",
                newName: "UploadItemId");

            migrationBuilder.AddColumn<double>(
                name: "ColourSimilarity",
                table: "UploadMatches",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Colours",
                table: "ClothingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UploadItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Embedding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HexColour = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadItems_Uploads_UploadId",
                        column: x => x.UploadId,
                        principalTable: "Uploads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadItems_UploadId",
                table: "UploadItems",
                column: "UploadId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadMatches_UploadItems_UploadItemId",
                table: "UploadMatches",
                column: "UploadItemId",
                principalTable: "UploadItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadMatches_UploadItems_UploadItemId",
                table: "UploadMatches");

            migrationBuilder.DropTable(
                name: "UploadItems");

            migrationBuilder.DropColumn(
                name: "ColourSimilarity",
                table: "UploadMatches");

            migrationBuilder.RenameColumn(
                name: "EmbeddingSimilarity",
                table: "UploadMatches",
                newName: "Similarity");

            migrationBuilder.RenameColumn(
                name: "UploadItemId",
                table: "UploadMatches",
                newName: "UploadId");

            migrationBuilder.AddColumn<string>(
                name: "UploadItemDescription",
                table: "UploadMatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UploadItemEmbedding",
                table: "UploadMatches",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Colours",
                table: "ClothingItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadMatches_Uploads_UploadId",
                table: "UploadMatches",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
