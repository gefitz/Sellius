using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaEstoque.API.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoTabelaUsuarioLicenca : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoUsuario",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorPorUsuario",
                table: "Licencas",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UsuairosIncluirFree",
                table: "Licencas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "UsuairosIncluirFree",
                table: "Licencas");

            migrationBuilder.AlterColumn<int>(
                name: "ValorPorUsuario",
                table: "Licencas",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");
        }
    }
}
