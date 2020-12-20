using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.WebAPI.Migrations
{
    public partial class RenameContactUsToUserQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUsForms");

            migrationBuilder.CreateTable(
                name: "UserQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatorMessage = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    UserCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserResponderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ResponderMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuestions_AspNetUsers_UserCreatorId",
                        column: x => x.UserCreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserQuestions_AspNetUsers_UserResponderId",
                        column: x => x.UserResponderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_UserCreatorId",
                table: "UserQuestions",
                column: "UserCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuestions_UserResponderId",
                table: "UserQuestions",
                column: "UserResponderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserQuestions");

            migrationBuilder.CreateTable(
                name: "ContactUsForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorMessage = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    ResponderMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    UserCreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserResponderId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
    }
}
