using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

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
                name: "ProjectDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDbs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TasksDbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TasksDbs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TasksDbs_ProjectDbs_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDbs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "ProjectDbs",
                columns: new[] { "Id", "CreatedDate", "Title" },
                values: new object[] { 4, new DateTime(2024, 10, 14, 14, 53, 25, 496, DateTimeKind.Local).AddTicks(231), "testlearn Aspnet" });

            migrationBuilder.InsertData(
                table: "TasksDbs",
                columns: new[] { "Id", "Description", "LastUpdated", "ProjectId" },
                values: new object[] { 2, "test test la la", new DateTime(2024, 10, 14, 14, 53, 25, 496, DateTimeKind.Local).AddTicks(590), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_TasksDbs_ProjectId",
                table: "TasksDbs",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TasksDbs");

            migrationBuilder.DropTable(
                name: "ProjectDbs");
        }
    }
}
