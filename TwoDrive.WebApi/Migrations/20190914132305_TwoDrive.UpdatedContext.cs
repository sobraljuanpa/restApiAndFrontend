using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TwoDrive.WebApi.Migrations
{
    public partial class TwoDriveUpdatedContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FolderId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FolderElement",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ParentId = table.Column<long>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    LastModifiedDate = table.Column<DateTime>(nullable: true),
                    OwnerId = table.Column<long>(nullable: true),
                    FolderId = table.Column<long>(nullable: true),
                    Folder_OwnerId = table.Column<long>(nullable: true),
                    FolderId1 = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FolderElement_FolderElement_FolderId",
                        column: x => x.FolderId,
                        principalTable: "FolderElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FolderElement_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FolderElement_FolderElement_FolderId1",
                        column: x => x.FolderId1,
                        principalTable: "FolderElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FolderElement_Users_Folder_OwnerId",
                        column: x => x.Folder_OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FolderElement_FolderElement_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FolderElement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_FileId",
                table: "Users",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_FolderId",
                table: "Users",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_FolderId",
                table: "FolderElement",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_OwnerId",
                table: "FolderElement",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_FolderId1",
                table: "FolderElement",
                column: "FolderId1");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_Folder_OwnerId",
                table: "FolderElement",
                column: "Folder_OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderElement_ParentId",
                table: "FolderElement",
                column: "ParentId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FileId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_FolderElement_FolderId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "FolderElement");

            migrationBuilder.DropIndex(
                name: "IX_Users_FileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FolderId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Users");
        }
    }
}
