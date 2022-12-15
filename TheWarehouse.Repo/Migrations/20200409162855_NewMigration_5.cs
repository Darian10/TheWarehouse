using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheWarehouse.Repo.Migrations
{
    public partial class NewMigration_5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesList_Announcement_AnnouncementId",
                table: "SalesList");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesList_Announcement_AnnouncementId",
                table: "SalesList",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesList_Announcement_AnnouncementId",
                table: "SalesList");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesList_Announcement_AnnouncementId",
                table: "SalesList",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
