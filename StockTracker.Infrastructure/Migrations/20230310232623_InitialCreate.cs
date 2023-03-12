using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StockTracker.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "activity",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ticker_id = table.Column<int>(type: "int", nullable: false),
                    activity_date = table.Column<DateTime>(type: "date", nullable: false),
                    open = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    close = table.Column<decimal>(type: "decimal(9,4)", nullable: false),
                    volume = table.Column<int>(type: "int", nullable: false),
                    updown = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    high = table.Column<decimal>(type: "decimal(9,4)", nullable: true),
                    low = table.Column<decimal>(type: "decimal(9,4)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activity", x => x.id);
                },
                comment: "		")
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "averages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ticker_id = table.Column<int>(type: "int", nullable: false),
                    activity_date = table.Column<DateTime>(type: "date", nullable: false),
                    value = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    average_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_averages", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tickers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ticker = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    ticker_name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true),
                    trend = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    close = table.Column<float>(type: "float", nullable: true),
                    in_portfolio = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    industry = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    sector = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tickers", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rsi",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ticker_id = table.Column<int>(type: "int", nullable: false),
                    avg_loss = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    avg_gain = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    rs = table.Column<decimal>(type: "decimal(9,2)", nullable: false),
                    rsi = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rsi", x => x.id);
                    table.ForeignKey(
                        name: "id",
                        column: x => x.ticker_id,
                        principalTable: "tickers",
                        principalColumn: "id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "id_idx",
                table: "rsi",
                column: "ticker_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity");

            migrationBuilder.DropTable(
                name: "averages");

            migrationBuilder.DropTable(
                name: "rsi");

            migrationBuilder.DropTable(
                name: "tickers");
        }
    }
}
