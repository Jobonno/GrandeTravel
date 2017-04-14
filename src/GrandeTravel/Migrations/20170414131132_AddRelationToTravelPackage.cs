using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using GrandeTravel.Models;

namespace GrandeTravel.Migrations
{
    public partial class AddRelationToTravelPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "TblFeedback",
                nullable: false,
                defaultValue: RatingEnum.ONESTAR);

            migrationBuilder.AddColumn<int>(
                name: "TravelPackageId",
                table: "TblFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_TravelPackageId",
                table: "TblFeedback",
                column: "TravelPackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblFeedback_TblTravelPackage_TravelPackageId",
                table: "TblFeedback",
                column: "TravelPackageId",
                principalTable: "TblTravelPackage",
                principalColumn: "TravelPackageId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblFeedback_TblTravelPackage_TravelPackageId",
                table: "TblFeedback");

            migrationBuilder.DropIndex(
                name: "IX_TblFeedback_TravelPackageId",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "TravelPackageId",
                table: "TblFeedback");
        }
    }
}
