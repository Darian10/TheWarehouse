using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheWarehouse.Repo.Migrations
{
    public partial class NewMigration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleList_Announcement_AnnouncementId",
                table: "SaleList");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleList_Sale_SaleId",
                table: "SaleList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleList",
                table: "SaleList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sale",
                table: "Sale");

            migrationBuilder.RenameTable(
                name: "SaleList",
                newName: "SalesList");

            migrationBuilder.RenameTable(
                name: "Sale",
                newName: "Sales");

            migrationBuilder.RenameIndex(
                name: "IX_SaleList_SaleId",
                table: "SalesList",
                newName: "IX_SalesList_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleList_AnnouncementId",
                table: "SalesList",
                newName: "IX_SalesList_AnnouncementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalesList",
                table: "SalesList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sales",
                table: "Sales",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesList_Announcement_AnnouncementId",
                table: "SalesList",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesList_Sales_SaleId",
                table: "SalesList",
                column: "SaleId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesList_Announcement_AnnouncementId",
                table: "SalesList");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesList_Sales_SaleId",
                table: "SalesList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalesList",
                table: "SalesList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sales",
                table: "Sales");

            migrationBuilder.RenameTable(
                name: "SalesList",
                newName: "SaleList");

            migrationBuilder.RenameTable(
                name: "Sales",
                newName: "Sale");

            migrationBuilder.RenameIndex(
                name: "IX_SalesList_SaleId",
                table: "SaleList",
                newName: "IX_SaleList_SaleId");

            migrationBuilder.RenameIndex(
                name: "IX_SalesList_AnnouncementId",
                table: "SaleList",
                newName: "IX_SaleList_AnnouncementId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleList",
                table: "SaleList",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sale",
                table: "Sale",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleList_Announcement_AnnouncementId",
                table: "SaleList",
                column: "AnnouncementId",
                principalTable: "Announcement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleList_Sale_SaleId",
                table: "SaleList",
                column: "SaleId",
                principalTable: "Sale",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
