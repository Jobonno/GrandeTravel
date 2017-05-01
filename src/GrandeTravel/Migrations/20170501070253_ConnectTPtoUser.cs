using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class ConnectTPtoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyUserId",
                table: "TblTravelPackage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TblTravelPackage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblTravelPackage_MyUserId",
                table: "TblTravelPackage",
                column: "MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblTravelPackage_AspNetUsers_MyUserId",
                table: "TblTravelPackage",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblTravelPackage_AspNetUsers_MyUserId",
                table: "TblTravelPackage");

            migrationBuilder.DropIndex(
                name: "IX_TblTravelPackage_MyUserId",
                table: "TblTravelPackage");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "TblTravelPackage");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblTravelPackage");
        }
    }
}
