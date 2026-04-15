using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trening_App.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoriaTreningow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPlanu = table.Column<int>(type: "INTEGER", nullable: false),
                    NazwaPlanu = table.Column<string>(type: "TEXT", nullable: false),
                    DataTreningu = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CzasTrwania = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriaTreningow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Poziom = table.Column<int>(type: "INTEGER", nullable: false),
                    Rodzaj = table.Column<string>(type: "TEXT", nullable: false),
                    IloscObwodow = table.Column<int>(type: "INTEGER", nullable: false),
                    PrzerwaMiedzyObwodami = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cwiczenia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlanId = table.Column<int>(type: "INTEGER", nullable: false),
                    NazwaCwiczenia = table.Column<string>(type: "TEXT", nullable: false),
                    LiczbaSerii = table.Column<int>(type: "INTEGER", nullable: false),
                    LiczbaPowtorzen = table.Column<int>(type: "INTEGER", nullable: false),
                    PrzerwaMiedzySeriami = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cwiczenia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cwiczenia_Plany_PlanId",
                        column: x => x.PlanId,
                        principalTable: "Plany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cwiczenia_PlanId",
                table: "Cwiczenia",
                column: "PlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cwiczenia");

            migrationBuilder.DropTable(
                name: "HistoriaTreningow");

            migrationBuilder.DropTable(
                name: "Plany");
        }
    }
}
