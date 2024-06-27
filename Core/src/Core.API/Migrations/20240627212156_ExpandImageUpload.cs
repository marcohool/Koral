using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.API.Migrations
{
    /// <inheritdoc />
    public partial class ExpandImageUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e10f5c3-54a6-4dc4-b968-3f2002304da5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd91e2d5-8e11-4649-92b5-25715dfb402c");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "ImageUploads",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "ImageUploads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ClothingItemsMatched",
                table: "ImageUploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ImageUploads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFavourited",
                table: "ImageUploads",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "ImageUploads",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClothingItemsMatched",
                table: "ImageUploads");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ImageUploads");

            migrationBuilder.DropColumn(
                name: "IsFavourited",
                table: "ImageUploads");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "ImageUploads");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ImageUploads",
                newName: "UploadedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "ImageUploads",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e10f5c3-54a6-4dc4-b968-3f2002304da5", null, "User", "USER" },
                    { "bd91e2d5-8e11-4649-92b5-25715dfb402c", null, "Admin", "ADMIN" }
                });
        }
    }
}
