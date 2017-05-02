using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class AddPropsToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TblBooking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "People",
                table: "TblBooking",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TblBooking");

            migrationBuilder.DropColumn(
                name: "People",
                table: "TblBooking");
        }
    }
}
