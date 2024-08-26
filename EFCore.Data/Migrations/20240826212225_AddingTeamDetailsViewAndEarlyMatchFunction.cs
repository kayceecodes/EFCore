using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingTeamDetailsViewAndEarlyMatchFunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coaches_TeamId",
                table: "Coaches");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_TeamId",
                table: "Coaches",
                column: "TeamId",
                unique: true);

            migrationBuilder.Sql(@"CREATE FUNCTION [dbo].[GetEarliestMatch] (@teamId int) 
                                   RETURNS datetime
                                   BEGIN
                                        DECLARE @result datetime
                                        SELECT TOP 1 @result = date
                                        FROM [dbo].[Matches]
                                        order by Date
                                        return @result
                                    END");
            migrationBuilder.Sql(@"CREATE VIEW [dbo].[TeamsCoachesLeagues]
                                   AS
                                   SELECT t.Name, c.Name AS CoachName, l.Name AS LeagueName
                                   FROM dbo.Teams AS t LEFT OUTER JOIN
                                      dbo.Coaches AS c ON t.Id = c.TeamId INNER JOIN
                                      dbo.Leagues AS l ON t.LeagueId = l.Id"
                                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Coaches_TeamId",
                table: "Coaches");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_TeamId",
                table: "Coaches",
                column: "TeamId");

            migrationBuilder.Sql(@"DROP VIEW [dbo].[TeamsCoachesLeagues]");
            migrationBuilder.Sql(@"DROP Function [dbo].[GetEarliestMatch]");
        }
    }
}
