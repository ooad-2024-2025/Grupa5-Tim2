using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Uklanjanjeduplogstatusa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevZaRentanje_AspNetUsers_korisnik_id",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropColumn(
                name: "status",
                table: "ZahtjevZaRentanje");

            migrationBuilder.AlterColumn<string>(
                name: "korisnik_id",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevZaRentanje_AspNetUsers_korisnik_id",
                table: "ZahtjevZaRentanje",
                column: "korisnik_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZahtjevZaRentanje_AspNetUsers_korisnik_id",
                table: "ZahtjevZaRentanje");

            migrationBuilder.AlterColumn<string>(
                name: "korisnik_id",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "ZahtjevZaRentanje",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ZahtjevZaRentanje_AspNetUsers_korisnik_id",
                table: "ZahtjevZaRentanje",
                column: "korisnik_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
