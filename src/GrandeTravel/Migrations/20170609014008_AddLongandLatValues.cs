using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class AddLongandLatValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "TblTravelPackage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "TblTravelPackage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "TblTravelPackage");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "TblTravelPackage");
        }
    }
}
