using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webblabb2distor.Migrations
{
    /// <inheritdoc />
    public partial class Auction : Migration
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
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    StartingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SellerUsername = table.Column<string>(type: "longtext", nullable: false)
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
                columns: new[] { "Id", "Description", "EndDateTime", "Name", "SellerUsername", "StartingPrice" },
                values: new object[,]
                {
                    { -2, "Old ferrari", new DateTime(2024, 11, 25, 0, 0, 0, 0, DateTimeKind.Local), "bartil", "bertil", 25000m },
                    { -1, "Auction description", new DateTime(2024, 11, 22, 0, 0, 0, 0, DateTimeKind.Local), "ragnar", "Seller", 45m }
                });

            migrationBuilder.InsertData(
                table: "BidsDbs",
                columns: new[] { "Id", "AuctionId", "Bidamount", "Biddername", "Bidtime" },
                values: new object[,]
                {
                    { -2, -2, 20000m, "Kalle", new DateTime(2024, 11, 23, 0, 0, 0, 0, DateTimeKind.Local) },
                    { -1, -1, 35m, "byuer", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 1, -1, 50m, "ferrari@gmail.com", new DateTime(2024, 11, 15, 12, 56, 51, 106, DateTimeKind.Local).AddTicks(3780) }
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
