using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheWarehouse.Repo.Migrations
{
    public partial class NewMigration_6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_AspNetUsers_ApplicationUserId",
                table: "Announcement");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_AspNetUsers_ApplicationUserId",
                table: "Announcement",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcement_AspNetUsers_ApplicationUserId",
                table: "Announcement");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcement_AspNetUsers_ApplicationUserId",
                table: "Announcement",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
