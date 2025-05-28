using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusZ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "statusZahtjeva",
                table: "ZahtjevZaRentanje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "nacinPlacanjaEnum",
                table: "Placanje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "statusPlacanja",
                table: "Placanje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "statusNotifikacije",
                table: "Notifikacija",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statusZahtjeva",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropColumn(
                name: "nacinPlacanjaEnum",
                table: "Placanje");

            migrationBuilder.DropColumn(
                name: "statusPlacanja",
                table: "Placanje");

            migrationBuilder.DropColumn(
                name: "statusNotifikacije",
                table: "Notifikacija");
        }
    }
}
