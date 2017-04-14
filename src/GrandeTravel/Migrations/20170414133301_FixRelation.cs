using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class FixRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblFeedback_TblBooking_BookingId1",
                table: "TblFeedback");

            migrationBuilder.DropIndex(
                name: "IX_TblFeedback_BookingId1",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "TblBooking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "TblFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BookingId1",
                table: "TblFeedback",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "TblBooking",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_BookingId1",
                table: "TblFeedback",
                column: "BookingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TblFeedback_TblBooking_BookingId1",
                table: "TblFeedback",
                column: "BookingId1",
                principalTable: "TblBooking",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
