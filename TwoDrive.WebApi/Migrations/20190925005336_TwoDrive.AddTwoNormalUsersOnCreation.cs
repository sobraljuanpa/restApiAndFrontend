using Microsoft.EntityFrameworkCore.Migrations;

namespace TwoDrive.WebApi.Migrations
{
    public partial class TwoDriveAddTwoNormalUsersOnCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastName",
                value: "Istrator");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FileId", "FirstName", "LastName", "Password", "Role", "RootFolderId", "Token", "UserId", "Username" },
                values: new object[] { 2L, "user1@user1.user1", null, "First", "TestUser", "user1", "User", null, null, null, "user1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FileId", "FirstName", "LastName", "Password", "Role", "RootFolderId", "Token", "UserId", "Username" },
                values: new object[] { 3L, "user2@user2.user2", null, "Second", "TestUser", "user2", "User", null, null, null, "user2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "LastName",
                value: "Istrador");
        }
    }
}
