using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NbaRosManagementTool.Data.Migrations
{
    public partial class newup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapHold",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PPG",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Restricted",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayerRating",
                table: "Players",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerRating",
                table: "Players");

            migrationBuilder.AddColumn<decimal>(
                name: "CapHold",
                table: "Players",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<double>(
                name: "PPG",
                table: "Players",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Restricted",
                table: "Players",
                nullable: false,
                defaultValue: false);
        }
    }
}
