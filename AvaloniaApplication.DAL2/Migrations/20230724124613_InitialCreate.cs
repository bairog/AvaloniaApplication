using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AvaloniaApplication.DAL2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaneBoard",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    PlaneType = table.Column<string>(type: "text", nullable: false),
                    PlaneBoardNo = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneBoard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaneFlight",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    FlightDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FlightSmena = table.Column<short>(type: "smallint", nullable: false),
                    FlightNo = table.Column<int>(type: "integer", nullable: false),
                    PilotName = table.Column<string>(type: "text", nullable: false),
                    PlaneBoardId = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneFlight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlaneFlight_PlaneBoard_PlaneBoardId",
                        column: x => x.PlaneBoardId,
                        principalTable: "PlaneBoard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PlaneBoard",
                columns: new[] { "Id", "PlaneBoardNo", "PlaneType" },
                values: new object[,]
                {
                    { 1m, "004", "SU-27" },
                    { 2m, "16", "MIG-29" }
                });

            migrationBuilder.InsertData(
                table: "PlaneFlight",
                columns: new[] { "Id", "FlightDate", "FlightNo", "FlightSmena", "PilotName", "PlaneBoardId" },
                values: new object[,]
                {
                    { 1m, new DateTime(2010, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 1, (short)1, "Иванов", 1m },
                    { 2m, new DateTime(2010, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 2, (short)1, "Иванов", 1m },
                    { 3m, new DateTime(2017, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), 6, (short)1, "Петров", 1m },
                    { 4m, new DateTime(2010, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), 1, (short)1, "Сидоров", 2m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaneBoard_PlaneType_PlaneBoardNo",
                table: "PlaneBoard",
                columns: new[] { "PlaneType", "PlaneBoardNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlaneFlight_PlaneBoardId_FlightDate_FlightSmena_FlightNo",
                table: "PlaneFlight",
                columns: new[] { "PlaneBoardId", "FlightDate", "FlightSmena", "FlightNo" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaneFlight");

            migrationBuilder.DropTable(
                name: "PlaneBoard");
        }
    }
}
