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
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUploads_AspNetUsers_AppUserId1",
                table: "ImageUploads");

            migrationBuilder.DropIndex(
                name: "IX_ImageUploads_AppUserId1",
                table: "ImageUploads");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cbce8c3a-cb69-4a1a-83ca-ee6d5fc4fd81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e2d29f82-21a5-4f1a-8940-51beb9981ab5");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "ImageUploads");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "ImageUploads",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43d7690f-02a7-437e-9989-365f7efd9b78", null, "Admin", "ADMIN" },
                    { "a0fb16f6-827b-4b4d-befb-ec4cc7d47087", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageUploads_AppUserId",
                table: "ImageUploads",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUploads_AspNetUsers_AppUserId",
                table: "ImageUploads",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageUploads_AspNetUsers_AppUserId",
                table: "ImageUploads");

            migrationBuilder.DropIndex(
                name: "IX_ImageUploads_AppUserId",
                table: "ImageUploads");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43d7690f-02a7-437e-9989-365f7efd9b78");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0fb16f6-827b-4b4d-befb-ec4cc7d47087");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "ImageUploads",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "ImageUploads",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cbce8c3a-cb69-4a1a-83ca-ee6d5fc4fd81", null, "Admin", "ADMIN" },
                    { "e2d29f82-21a5-4f1a-8940-51beb9981ab5", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageUploads_AppUserId1",
                table: "ImageUploads",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageUploads_AspNetUsers_AppUserId1",
                table: "ImageUploads",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
