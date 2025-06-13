using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffroadAdventure.Data.Migrations
{
    /// <inheritdoc />
    public partial class komentarispravka : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "komentarId",
                table: "Komentar",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "komentarId",
                table: "Komentar");
        }
    }
}
