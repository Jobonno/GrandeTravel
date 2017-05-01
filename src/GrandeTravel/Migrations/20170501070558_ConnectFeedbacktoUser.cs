using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GrandeTravel.Migrations
{
    public partial class ConnectFeedbacktoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyUserId",
                table: "TblFeedback",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "TblFeedback",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TblFeedback",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblFeedback_MyUserId",
                table: "TblFeedback",
                column: "MyUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TblFeedback_AspNetUsers_MyUserId",
                table: "TblFeedback",
                column: "MyUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblFeedback_AspNetUsers_MyUserId",
                table: "TblFeedback");

            migrationBuilder.DropIndex(
                name: "IX_TblFeedback_MyUserId",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "MyUserId",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TblFeedback");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TblFeedback");
        }
    }
}
