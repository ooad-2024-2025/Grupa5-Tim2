using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class pomilujme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "korisnik_id",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "primalac_id",
                table: "Notifikacija",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "autor_id",
                table: "Komentar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Komentar",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ZahtjevZaRentanje_korisnik_id",
                table: "ZahtjevZaRentanje",
                column: "korisnik_id");

            migrationBuilder.CreateIndex(
                name: "IX_Placanje_zahtjevZaRentanjeId",
                table: "Placanje",
                column: "zahtjevZaRentanjeId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifikacija_primalac_id",
                table: "Notifikacija",
                column: "primalac_id");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_userId",
                table: "Komentar",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifikacija_AspNetUsers_primalac_id",
                table: "Notifikacija",
                column: "primalac_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Placanje_ZahtjevZaRentanje_zahtjevZaRentanjeId",
                table: "Placanje",
                column: "zahtjevZaRentanjeId",
                principalTable: "ZahtjevZaRentanje",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevZaRentanje_AspNetUsers_korisnik_id",
                table: "ZahtjevZaRentanje",
                column: "korisnik_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifikacija_AspNetUsers_primalac_id",
                table: "Notifikacija");

            migrationBuilder.DropForeignKey(
                name: "FK_Placanje_ZahtjevZaRentanje_zahtjevZaRentanjeId",
                table: "Placanje");

            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevZaRentanje_AspNetUsers_korisnik_id",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropIndex(
                name: "IX_ZahtjevZaRentanje_korisnik_id",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropIndex(
                name: "IX_Placanje_zahtjevZaRentanjeId",
                table: "Placanje");

            migrationBuilder.DropIndex(
                name: "IX_Notifikacija_primalac_id",
                table: "Notifikacija");

            migrationBuilder.DropIndex(
                name: "IX_Komentar_userId",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prezime",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "korisnik_id",
                table: "ZahtjevZaRentanje",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "primalac_id",
                table: "Notifikacija",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "autor_id",
                table: "Komentar",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
