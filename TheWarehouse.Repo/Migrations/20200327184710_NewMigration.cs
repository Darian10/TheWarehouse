using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheWarehouse.Repo.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<double>(
                name: "MoneyCard",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Announcement",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiaVenta = table.Column<DateTime>(nullable: true),
                    Iva = table.Column<double>(nullable: true),
                    Subtotal = table.Column<double>(nullable: true),
                    Total = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnnouncementId = table.Column<int>(nullable: true),
                    Cant = table.Column<int>(nullable: true),
                    SaleId = table.Column<int>(nullable: true),
                    Total = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleList_Announcement_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleList_Sale_SaleId",
                        column: x => x.SaleId,
                        principalTable: "Sale",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleList_AnnouncementId",
                table: "SaleList",
                column: "AnnouncementId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleList_SaleId",
                table: "SaleList",
                column: "SaleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleList");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropColumn(
                name: "MoneyCard",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Announcement");

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }
    }
}
