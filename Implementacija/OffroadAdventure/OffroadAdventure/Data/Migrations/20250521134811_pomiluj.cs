using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class pomiluj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_StavkaZahtjeva_VoziloId",
                table: "StavkaZahtjeva",
                column: "VoziloId");

            migrationBuilder.CreateIndex(
                name: "IX_StavkaZahtjeva_ZahtjevZaRentanjeId",
                table: "StavkaZahtjeva",
                column: "ZahtjevZaRentanjeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StavkaZahtjeva_Vozilo_VoziloId",
                table: "StavkaZahtjeva",
                column: "VoziloId",
                principalTable: "Vozilo",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StavkaZahtjeva_ZahtjevZaRentanje_ZahtjevZaRentanjeId",
                table: "StavkaZahtjeva",
                column: "ZahtjevZaRentanjeId",
                principalTable: "ZahtjevZaRentanje",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StavkaZahtjeva_Vozilo_VoziloId",
                table: "StavkaZahtjeva");

            migrationBuilder.DropForeignKey(
                name: "FK_StavkaZahtjeva_ZahtjevZaRentanje_ZahtjevZaRentanjeId",
                table: "StavkaZahtjeva");

            migrationBuilder.DropIndex(
                name: "IX_StavkaZahtjeva_VoziloId",
                table: "StavkaZahtjeva");

            migrationBuilder.DropIndex(
                name: "IX_StavkaZahtjeva_ZahtjevZaRentanjeId",
                table: "StavkaZahtjeva");
        }
    }
}
