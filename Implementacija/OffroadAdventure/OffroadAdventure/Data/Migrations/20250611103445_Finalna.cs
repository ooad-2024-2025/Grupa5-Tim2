using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Finalna : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar");

            migrationBuilder.DropIndex(
                name: "IX_Komentar_userId",
                table: "Komentar");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Komentar");

            migrationBuilder.AlterColumn<string>(
                name: "tekst",
                table: "Komentar",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "autor_id",
                table: "Komentar",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_autor_id",
                table: "Komentar",
                column: "autor_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_AspNetUsers_autor_id",
                table: "Komentar",
                column: "autor_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Komentar_AspNetUsers_autor_id",
                table: "Komentar");

            migrationBuilder.DropIndex(
                name: "IX_Komentar_autor_id",
                table: "Komentar");

            migrationBuilder.AlterColumn<string>(
                name: "tekst",
                table: "Komentar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "autor_id",
                table: "Komentar",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "Komentar",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Komentar_userId",
                table: "Komentar",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Komentar_AspNetUsers_userId",
                table: "Komentar",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
