using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FinalnaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Placanje_zahtjevZaRentanjeId",
                table: "Placanje");

            migrationBuilder.DropColumn(
                name: "statusNotifikacije",
                table: "Notifikacija");

            migrationBuilder.AlterColumn<string>(
                name: "dodatniZahtjev",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_zahtjevZaRentanjeId",
                table: "Placanje",
                column: "zahtjevZaRentanjeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Placanje_zahtjevZaRentanjeId",
                table: "Placanje");

            migrationBuilder.AlterColumn<string>(
                name: "dodatniZahtjev",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "statusNotifikacije",
                table: "Notifikacija",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_zahtjevZaRentanjeId",
                table: "Placanje",
                column: "zahtjevZaRentanjeId");
        }
    }
}
