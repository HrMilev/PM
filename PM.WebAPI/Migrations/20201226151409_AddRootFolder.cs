using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.WebAPI.Migrations
{
    public partial class AddRootFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RootFolderId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RootFolderId",
                table: "AspNetUsers",
                column: "RootFolderId",
                unique: true,
                filter: "[RootFolderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Folders_RootFolderId",
                table: "AspNetUsers",
                column: "RootFolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Folders_RootFolderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RootFolderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RootFolderId",
                table: "AspNetUsers");
        }
    }
}
