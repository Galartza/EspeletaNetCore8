using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EzpeletaNetCore8.Migrations
{
    /// <inheritdoc />
    public partial class MigracionPrueba21 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreTipoEjercicio",
                table: "EjerciciosFisicos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreTipoEjercicio",
                table: "EjerciciosFisicos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
