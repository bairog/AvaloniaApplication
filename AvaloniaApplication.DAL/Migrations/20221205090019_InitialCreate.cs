using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AvaloniaApplication.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaneBoard",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlaneType = table.Column<string>(type: "TEXT", nullable: false),
                    PlaneBoardNo = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneBoard", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaneFlight",
                columns: table => new
                {
                    Id = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FlightSmena = table.Column<short>(type: "INTEGER", nullable: false),
                    FlightNo = table.Column<int>(type: "INTEGER", nullable: false),
                    PilotName = table.Column<string>(type: "TEXT", nullable: false),
                    PlaneBoardId = table.Column<ulong>(type: "INTEGER", nullable: false)
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
                values: new object[] { 1ul, "004", "SU-27" });

            migrationBuilder.InsertData(
                table: "PlaneBoard",
                columns: new[] { "Id", "PlaneBoardNo", "PlaneType" },
                values: new object[] { 2ul, "16", "MIG-29" });

            migrationBuilder.InsertData(
                table: "PlaneFlight",
                columns: new[] { "Id", "FlightDate", "FlightNo", "FlightSmena", "PilotName", "PlaneBoardId" },
                values: new object[] { 1ul, new DateTime(2010, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)1, "Иванов", 1ul });

            migrationBuilder.InsertData(
                table: "PlaneFlight",
                columns: new[] { "Id", "FlightDate", "FlightNo", "FlightSmena", "PilotName", "PlaneBoardId" },
                values: new object[] { 2ul, new DateTime(2010, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, (short)1, "Иванов", 1ul });

            migrationBuilder.InsertData(
                table: "PlaneFlight",
                columns: new[] { "Id", "FlightDate", "FlightNo", "FlightSmena", "PilotName", "PlaneBoardId" },
                values: new object[] { 3ul, new DateTime(2017, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, (short)1, "Петров", 1ul });

            migrationBuilder.InsertData(
                table: "PlaneFlight",
                columns: new[] { "Id", "FlightDate", "FlightNo", "FlightSmena", "PilotName", "PlaneBoardId" },
                values: new object[] { 4ul, new DateTime(2010, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, (short)1, "Сидоров", 2ul });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaneFlight");

            migrationBuilder.DropTable(
                name: "PlaneBoard");
        }
    }
}
