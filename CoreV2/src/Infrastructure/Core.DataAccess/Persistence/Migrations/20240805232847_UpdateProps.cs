using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.DataAccess.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Uploads_Path", table: "Uploads");

            migrationBuilder.DropColumn(name: "Path", table: "Uploads");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Uploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ImageUrl", table: "Uploads");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Uploads",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.CreateIndex(
                name: "IX_Uploads_Path",
                table: "Uploads",
                column: "Path",
                unique: true
            );
        }
    }
}
