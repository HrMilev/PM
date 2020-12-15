using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.WebAPI.Migrations
{
    public partial class AddSubjectInContactUsForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "ContactUsForms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ContactUsForms");
        }
    }
}
