using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class ConnectBookingtoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyUserId",
                table: "TblBooking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TblBooking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblBooking_MyUserId",
                table: "TblBooking",
                column: "MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblBooking_AspNetUsers_MyUserId",
                table: "TblBooking",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblBooking_AspNetUsers_MyUserId",
                table: "TblBooking");

            migrationBuilder.DropIndex(
                name: "IX_TblBooking_MyUserId",
                table: "TblBooking");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "TblBooking");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblBooking");
        }
    }
}
