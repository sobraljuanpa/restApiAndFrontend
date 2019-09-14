using Microsoft.EntityFrameworkCore.Migrations;

namespace TwoDrive.WebApi.Migrations
{
    public partial class TwoDriveRefactoredDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderElement_Users_Folder_OwnerId",
                table: "FolderElement");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FileId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FolderId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_FolderElement_Folder_OwnerId",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Folder_OwnerId",
                table: "FolderElement");

            migrationBuilder.RenameColumn(
                name: "FolderId",
                table: "Users",
                newName: "FolderElementId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FolderId",
                table: "Users",
                newName: "IX_Users_FolderElementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FolderElement_FolderElementId",
                table: "Users",
                column: "FolderElementId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FolderElementId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "FolderElementId",
                table: "Users",
                newName: "FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_FolderElementId",
                table: "Users",
                newName: "IX_Users_FolderId");

            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Folder_OwnerId",
                table: "FolderElement",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FileId",
                table: "Users",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_Folder_OwnerId",
                table: "FolderElement",
                column: "Folder_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderElement_Users_Folder_OwnerId",
                table: "FolderElement",
                column: "Folder_OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FolderElement_FileId",
                table: "Users",
                column: "FileId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_FolderElement_FolderId",
                table: "Users",
                column: "FolderId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
