using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class zahtjevzarentanje : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar");

            migrationBuilder.RenameColumn(
                name: "vrijemeTrajanja",
                table: "ZahtjevZaRentanje",
                newName: "cijena");

            migrationBuilder.AddColumn<string>(
                name: "brojTelefona",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "dodatniZahtjev",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ime",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "prezime",
                table: "ZahtjevZaRentanje",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Komentar",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "brojTelefona",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropColumn(
                name: "dodatniZahtjev",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropColumn(
                name: "email",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropColumn(
                name: "ime",
                table: "ZahtjevZaRentanje");

            migrationBuilder.DropColumn(
                name: "prezime",
                table: "ZahtjevZaRentanje");

            migrationBuilder.RenameColumn(
                name: "cijena",
                table: "ZahtjevZaRentanje",
                newName: "vrijemeTrajanja");

            migrationBuilder.AlterColumn<string>(
                name: "userId",
                table: "Komentar",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Prezime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Ime",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
