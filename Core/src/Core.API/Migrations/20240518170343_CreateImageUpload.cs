using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Core.API.Migrations
{
    /// <inheritdoc />
    public partial class CreateImageUpload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43d7690f-02a7-437e-9989-365f7efd9b78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0fb16f6-827b-4b4d-befb-ec4cc7d47087");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ImageUploads",
                newName: "ImagePath");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "ImageUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ImageUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ImageSize",
                table: "ImageUploads",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "973e9151-e1a5-4160-ad89-a34073e87f4f", null, "User", "USER" },
                    { "eca98bef-52ff-4820-b086-3bae6a078123", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "973e9151-e1a5-4160-ad89-a34073e87f4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eca98bef-52ff-4820-b086-3bae6a078123");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "ImageUploads");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ImageUploads");

            migrationBuilder.DropColumn(
                name: "ImageSize",
                table: "ImageUploads");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "ImageUploads",
                newName: "Image");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43d7690f-02a7-437e-9989-365f7efd9b78", null, "Admin", "ADMIN" },
                    { "a0fb16f6-827b-4b4d-befb-ec4cc7d47087", null, "User", "USER" }
                });
        }
    }
}
