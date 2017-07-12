using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NbaRosManagementTool.Data.Migrations
{
    public partial class UserPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

           

           

            migrationBuilder.CreateTable(
                name: "UserPlayers",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false),
                    UserTeamsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPlayers", x => new { x.PlayerID, x.UserTeamsID });
                    table.ForeignKey(
                        name: "FK_UserPlayers_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPlayers_UserTeams_UserTeamsID",
                        column: x => x.UserTeamsID,
                        principalTable: "UserTeams",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPlayers_UserTeamsID",
                table: "UserPlayers",
                column: "UserTeamsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPlayers");

            migrationBuilder.AddColumn<int>(
                name: "UserTeamsID",
                table: "Players",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserTeamsID",
                table: "Players",
                column: "UserTeamsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_UserTeams_UserTeamsID",
                table: "Players",
                column: "UserTeamsID",
                principalTable: "UserTeams",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
