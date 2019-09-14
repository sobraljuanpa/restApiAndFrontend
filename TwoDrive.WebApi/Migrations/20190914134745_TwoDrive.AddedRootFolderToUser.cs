using Microsoft.EntityFrameworkCore.Migrations;

namespace TwoDrive.WebApi.Migrations
{
    public partial class TwoDriveAddedRootFolderToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderElement_Users_OwnerId",
                table: "FolderElement");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FolderElementId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_FolderElement_OwnerId",
                table: "FolderElement");

            migrationBuilder.RenameColumn(
                name: "FolderElementId",
                table: "Users",
                newName: "RootFolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FolderElementId",
                table: "Users",
                newName: "IX_Users_RootFolderId");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Users",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "FolderElement",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FileId",
                table: "Users",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FolderElement_FileId",
                table: "Users",
                column: "FileId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FolderElement_RootFolderId",
                table: "Users",
                column: "RootFolderId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FileId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_RootFolderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "RootFolderId",
                table: "Users",
                newName: "FolderElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RootFolderId",
                table: "Users",
                newName: "IX_Users_FolderElementId");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "FolderElement",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_OwnerId",
                table: "FolderElement",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderElement_Users_OwnerId",
                table: "FolderElement",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FolderElement_FolderElementId",
                table: "Users",
                column: "FolderElementId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
