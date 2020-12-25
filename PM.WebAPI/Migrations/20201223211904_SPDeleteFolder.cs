using Microsoft.EntityFrameworkCore.Migrations;

namespace PM.WebAPI.Migrations
{
    public partial class SPDeleteFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
            IF OBJECT_ID('DeleteFolder', 'P') IS NOT NULL
            DROP PROC DeleteFolder
            GO
 
            CREATE PROCEDURE DeleteFolder
				@FolderId INT,
				@CreatorId NVARCHAR(450)
			AS
				IF EXISTS (SELECT *
							FROM Folders
							WHERE Id = @FolderId AND CreatorId = @CreatorId)
				BEGIN
					WITH foldersToDelete(Id) 
					AS (
						SELECT Id
						FROM Folders
						WHERE Id = @FolderId
						UNION ALL
						SELECT c.Id 
						FROM foldersToDelete p, Folders c
						WHERE p.Id = c.ParentFolderId
					)

					DELETE FROM Folders
					WHERE Id IN (SELECT Id FROM foldersToDelete)
				END";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC DeleteFolder");
        }
    }
}
