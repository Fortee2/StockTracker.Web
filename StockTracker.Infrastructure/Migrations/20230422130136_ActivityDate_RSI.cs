using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockTracker.Infrastructure.Migrations
{
    public partial class ActivityDate_RSI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "JobStatuses",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "activity_date",
                table: "rsi",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activity_date",
                table: "rsi");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "JobStatuses",
                newName: "id");
        }
    }
}
