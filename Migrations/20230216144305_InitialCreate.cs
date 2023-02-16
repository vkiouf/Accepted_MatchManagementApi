using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatchManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MatchDate = table.Column<DateTime>(type: "Date", nullable: false),
                    MatchTime = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    TeamA = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TeamB = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sport = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchID);
                });

            migrationBuilder.CreateTable(
                name: "MatchOdds",
                columns: table => new
                {
                    MatchOddsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchId = table.Column<int>(type: "int", nullable: false),
                    Specifier = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Odd = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchOdds", x => x.MatchOddsID);
                    table.ForeignKey(
                        name: "FK_MatchOdds_Match_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Match",
                        principalColumn: "MatchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchOdds_MatchId",
                table: "MatchOdds",
                column: "MatchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchOdds");

            migrationBuilder.DropTable(
                name: "Match");
        }
    }
}
