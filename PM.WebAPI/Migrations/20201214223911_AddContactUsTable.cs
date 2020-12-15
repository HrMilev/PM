using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.WebAPI.Migrations
{
    public partial class AddContactUsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUsForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserResponderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResponderMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactUsForms_AspNetUsers_UserCreatorId",
                        column: x => x.UserCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactUsForms_AspNetUsers_UserResponderId",
                        column: x => x.UserResponderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUsForms_UserCreatorId",
                table: "ContactUsForms",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactUsForms_UserResponderId",
                table: "ContactUsForms",
                column: "UserResponderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUsForms");
        }
    }
}
