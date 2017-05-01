using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class addmigrationchangeIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblTravelPackage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblBooking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TblTravelPackage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TblFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TblBooking",
                nullable: false,
                defaultValue: 0);
        }
    }
}
