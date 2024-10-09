using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Licorera_Corchos.web.Migrations
{
    /// <inheritdoc />
    public partial class CreateAUsuariosTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "A_Usuarios",
                columns: table => new
                {
                    Id_AU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_AU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_A = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_A_Usuarios", x => x.Id_AU);
                    table.ForeignKey(
                        name: "FK_A_Usuarios_Accounting_Id_A",
                        column: x => x.Id_A,
                        principalTable: "Accounting",
                        principalColumn: "Id_A",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_A_Usuarios_Id_A",
                table: "A_Usuarios",
                column: "Id_A");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "A_Usuarios");
        }
    }
}
