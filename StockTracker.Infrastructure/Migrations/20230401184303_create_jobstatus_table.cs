using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StockTracker.Infrastructure.Migrations
{
    public partial class create_jobstatus_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "close",
                table: "tickers");

            migrationBuilder.DropColumn(
                name: "in_portfolio",
                table: "tickers");

            migrationBuilder.DropColumn(
                name: "trend",
                table: "tickers");

            migrationBuilder.CreateTable(
                name: "JobStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    JobName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    ActivityTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ActivityDescription = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobStatuses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobStatuses");

            migrationBuilder.AddColumn<float>(
                name: "close",
                table: "tickers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "in_portfolio",
                table: "tickers",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "trend",
                table: "tickers",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
