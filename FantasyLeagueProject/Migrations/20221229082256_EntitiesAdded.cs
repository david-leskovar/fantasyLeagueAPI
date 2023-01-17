using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FantasyLeagueProject.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    SubTitle = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    ImageURL = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NumberOfRounds = table.Column<int>(type: "INTEGER", nullable: false),
                    Budget = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentRound = table.Column<int>(type: "INTEGER", nullable: false),
                    ChangeTeamEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    RoundPerfomancesAdded = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Cost = table.Column<double>(type: "REAL", nullable: false),
                    TournamentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    TournamentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Kills = table.Column<int>(type: "INTEGER", nullable: false),
                    Assists = table.Column<int>(type: "INTEGER", nullable: false),
                    Deaths = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Performances_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTeams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoundNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Points = table.Column<int>(type: "INTEGER", nullable: false),
                    TournamentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryTeams_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoryTeams_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayerUser",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerUser", x => new { x.PlayersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PlayerUser_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostUser",
                columns: table => new
                {
                    LikedByUsersId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LikedPostsId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUser", x => new { x.LikedByUsersId, x.LikedPostsId });
                    table.ForeignKey(
                        name: "FK_PostUser_Posts_LikedPostsId",
                        column: x => x.LikedPostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUser_Users_LikedByUsersId",
                        column: x => x.LikedByUsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryTeamPlayer",
                columns: table => new
                {
                    HistoryTeamsId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PlayersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryTeamPlayer", x => new { x.HistoryTeamsId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_HistoryTeamPlayer_HistoryTeams_HistoryTeamsId",
                        column: x => x.HistoryTeamsId,
                        principalTable: "HistoryTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryTeamPlayer_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTeamPlayer_PlayersId",
                table: "HistoryTeamPlayer",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTeams_TournamentId",
                table: "HistoryTeams",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryTeams_UserId",
                table: "HistoryTeams",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_PlayerId",
                table: "Performances",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TournamentId",
                table: "Players",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerUser_UsersId",
                table: "PlayerUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_LikedPostsId",
                table: "PostUser",
                column: "LikedPostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_TournamentId",
                table: "Users",
                column: "TournamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryTeamPlayer");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "PlayerUser");

            migrationBuilder.DropTable(
                name: "PostUser");

            migrationBuilder.DropTable(
                name: "HistoryTeams");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
