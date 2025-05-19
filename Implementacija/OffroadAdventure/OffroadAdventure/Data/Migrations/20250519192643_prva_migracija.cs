using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class prva_migracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Komentar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    autor_id = table.Column<int>(type: "int", nullable: false),
                    ocjena = table.Column<int>(type: "int", nullable: false),
                    tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Komentar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifikacija",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    primalac_id = table.Column<int>(type: "int", nullable: false),
                    tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifikacija", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Placanje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    zahtjevZaRentanjeId = table.Column<int>(type: "int", nullable: false),
                    nacinPlacanja = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    datumPlacanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    popust = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placanje", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StavkaZahtjeva",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZahtjevZaRentanjeId = table.Column<int>(type: "int", nullable: false),
                    VoziloId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaZahtjeva", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Vozilo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cijenaPoDanu = table.Column<double>(type: "float", nullable: false),
                    dostupno = table.Column<bool>(type: "bit", nullable: false),
                    slikaURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vozilo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ZahtjevZaRentanje",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    korisnik_id = table.Column<int>(type: "int", nullable: false),
                    datumOd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datumDo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    brojVozila = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    popust = table.Column<double>(type: "float", nullable: false),
                    vrijemeTrajanja = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZahtjevZaRentanje", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Komentar");

            migrationBuilder.DropTable(
                name: "Notifikacija");

            migrationBuilder.DropTable(
                name: "Placanje");

            migrationBuilder.DropTable(
                name: "StavkaZahtjeva");

            migrationBuilder.DropTable(
                name: "Vozilo");

            migrationBuilder.DropTable(
                name: "ZahtjevZaRentanje");
        }
    }
}
