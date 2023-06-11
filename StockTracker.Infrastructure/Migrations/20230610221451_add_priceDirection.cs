using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StockTracker.Infrastructure.Migrations
{
    public partial class add_priceDirection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "id_idx",
                table: "rsi",
                newName: "id_idx1");

            migrationBuilder.CreateTable(
                name: "price_direction",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ticker_id = table.Column<int>(type: "int", nullable: false),
                    direction = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    date_added = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_price_direction", x => x.id);
                    table.ForeignKey(
                        name: "FK_price_direction_tickers_ticker_id",
                        column: x => x.ticker_id,
                        principalTable: "tickers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "id_idx",
                table: "price_direction",
                column: "ticker_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "price_direction");

            migrationBuilder.RenameIndex(
                name: "id_idx1",
                table: "rsi",
                newName: "id_idx");
        }
    }
}
