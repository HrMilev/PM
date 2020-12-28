using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.WebAPI.Migrations
{
    public partial class AddFileExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "UploadedFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "UploadedFiles");
        }
    }
}
