using Microsoft.EntityFrameworkCore.Migrations;

namespace TwoDrive.WebApi.Migrations
{
    public partial class TwoDriveAuthorization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Administrator",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FileId", "FirstName", "LastName", "Password", "Role", "RootFolderId", "Token", "UserId", "Username" },
                values: new object[] { 1L, "admin@admin.admin", null, "Admin", "Istrador", "admin", "Admin", null, null, null, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Administrator",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }
    }
}
