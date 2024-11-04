using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webblabb2distor.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuctionDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Enddate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Sellername = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionDbs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BidsDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Bidamount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Biddername = table.Column<string>(type: "longtext", nullable: false),
                    Bidtime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BidsDbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BidsDbs_AuctionDbs_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "AuctionDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "AuctionDbs",
                columns: new[] { "Id", "Enddate", "Sellername", "description", "name", "price" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), "Seller", "Auction description", "ragnar", 45m },
                    { 2, new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Local), "bertil", "Old ferrari", "bartil", 25000m }
                });

            migrationBuilder.InsertData(
                table: "BidsDbs",
                columns: new[] { "Id", "AuctionId", "Bidamount", "Biddername", "Bidtime" },
                values: new object[,]
                {
                    { 1, 1, 35m, "byuer", new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, 2, 20000m, "Kalle", new DateTime(2024, 11, 4, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BidsDbs_AuctionId",
                table: "BidsDbs",
                column: "AuctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BidsDbs");

            migrationBuilder.DropTable(
                name: "AuctionDbs");
        }
    }
}
