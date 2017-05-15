using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class removeEmailFromProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "TblTravelProviderProfile");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TblCustomerProfile");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TblTravelProviderProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TblCustomerProfile",
                nullable: true);
        }
    }
}
