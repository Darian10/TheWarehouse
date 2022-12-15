using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheWarehouse.Repo.Migrations
{
    public partial class NewMigration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Iva",
                table: "Sales",
                newName: "Tax");

            migrationBuilder.RenameColumn(
                name: "DiaVenta",
                table: "Sales",
                newName: "SaleDay");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Auction",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tax",
                table: "Sales",
                newName: "Iva");

            migrationBuilder.RenameColumn(
                name: "SaleDay",
                table: "Sales",
                newName: "DiaVenta");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Auction",
                newName: "Fecha");
        }
    }
}
