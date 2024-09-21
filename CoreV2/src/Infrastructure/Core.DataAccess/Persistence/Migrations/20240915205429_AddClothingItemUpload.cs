using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClothingItemUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadClothingItems_ClothingItems_ClothingItemsId",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_UploadClothingItems_Uploads_UploadsId",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadClothingItems",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropIndex(
                name: "IX_UploadClothingItems_UploadsId",
                table: "UploadClothingItems"
            );

            migrationBuilder.RenameColumn(
                name: "UploadsId",
                table: "UploadClothingItems",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "ClothingItemsId",
                table: "UploadClothingItems",
                newName: "ClothingItemId"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Uploads",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true
            );

            migrationBuilder.AddColumn<Guid>(
                name: "UploadId",
                table: "UploadClothingItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "UploadClothingItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "UploadClothingItems",
                type: "datetime2",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "UploadItemDescription",
                table: "UploadClothingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "UploadItemEmbedding",
                table: "UploadClothingItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadClothingItems",
                table: "UploadClothingItems",
                columns: new[] { "UploadId", "ClothingItemId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_UploadClothingItems_ClothingItemId",
                table: "UploadClothingItems",
                column: "ClothingItemId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_UploadClothingItems_ClothingItems_ClothingItemId",
                table: "UploadClothingItems",
                column: "ClothingItemId",
                principalTable: "ClothingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_UploadClothingItems_Uploads_UploadId",
                table: "UploadClothingItems",
                column: "UploadId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadClothingItems_ClothingItems_ClothingItemId",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_UploadClothingItems_Uploads_UploadId",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_UploadClothingItems",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropIndex(
                name: "IX_UploadClothingItems_ClothingItemId",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropColumn(name: "UploadId", table: "UploadClothingItems");

            migrationBuilder.DropColumn(name: "CreatedOn", table: "UploadClothingItems");

            migrationBuilder.DropColumn(name: "LastUpdatedOn", table: "UploadClothingItems");

            migrationBuilder.DropColumn(
                name: "UploadItemDescription",
                table: "UploadClothingItems"
            );

            migrationBuilder.DropColumn(name: "UploadItemEmbedding", table: "UploadClothingItems");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UploadClothingItems",
                newName: "UploadsId"
            );

            migrationBuilder.RenameColumn(
                name: "ClothingItemId",
                table: "UploadClothingItems",
                newName: "ClothingItemsId"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Uploads",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_UploadClothingItems",
                table: "UploadClothingItems",
                columns: new[] { "ClothingItemsId", "UploadsId" }
            );

            migrationBuilder.CreateIndex(
                name: "IX_UploadClothingItems_UploadsId",
                table: "UploadClothingItems",
                column: "UploadsId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_UploadClothingItems_ClothingItems_ClothingItemsId",
                table: "UploadClothingItems",
                column: "ClothingItemsId",
                principalTable: "ClothingItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_UploadClothingItems_Uploads_UploadsId",
                table: "UploadClothingItems",
                column: "UploadsId",
                principalTable: "Uploads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
