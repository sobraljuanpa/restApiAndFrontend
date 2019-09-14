using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TwoDrive.WebApi.Migrations
{
    public partial class TwoDriveChangedParentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderElement_FolderElement_FolderId",
                table: "FolderElement");

            migrationBuilder.DropForeignKey(
                name: "FK_FolderElement_FolderElement_FolderId1",
                table: "FolderElement");

            migrationBuilder.DropForeignKey(
                name: "FK_FolderElement_FolderElement_ParentId",
                table: "FolderElement");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FileId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_RootFolderId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FolderElement",
                table: "FolderElement");

            migrationBuilder.DropIndex(
                name: "IX_FolderElement_FolderId",
                table: "FolderElement");

            migrationBuilder.DropIndex(
                name: "IX_FolderElement_FolderId1",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "FolderId1",
                table: "FolderElement");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "FolderElement");

            migrationBuilder.RenameTable(
                name: "FolderElement",
                newName: "Folders");

            migrationBuilder.RenameIndex(
                name: "IX_FolderElement_ParentId",
                table: "Folders",
                newName: "IX_Folders_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Folders",
                table: "Folders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OwnerId = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Folders_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ParentId",
                table: "Files",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders",
                column: "ParentId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Files_FileId",
                table: "Users",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Folders_RootFolderId",
                table: "Users",
                column: "RootFolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentId",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Files_FileId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Folders_RootFolderId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Folders",
                table: "Folders");

            migrationBuilder.RenameTable(
                name: "Folders",
                newName: "FolderElement");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_ParentId",
                table: "FolderElement",
                newName: "IX_FolderElement_ParentId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "FolderElement",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "FolderElement",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FolderId",
                table: "FolderElement",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "FolderElement",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FolderId1",
                table: "FolderElement",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "FolderElement",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FolderElement",
                table: "FolderElement",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_FolderId",
                table: "FolderElement",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_FolderId1",
                table: "FolderElement",
                column: "FolderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderElement_FolderElement_FolderId",
                table: "FolderElement",
                column: "FolderId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FolderElement_FolderElement_FolderId1",
                table: "FolderElement",
                column: "FolderId1",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FolderElement_FolderElement_ParentId",
                table: "FolderElement",
                column: "ParentId",
                principalTable: "FolderElement",
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
                name: "FK_Users_FolderElement_RootFolderId",
                table: "Users",
                column: "RootFolderId",
                principalTable: "FolderElement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
