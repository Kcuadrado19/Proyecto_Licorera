using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Licorera_Corchos.web.Migrations
{
    /// <inheritdoc />
    public partial class CreateAccountingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    Id_A = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_A = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasena_A = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_HModificaciones_A = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.Id_A);
                    table.ForeignKey(
                        name: "FK_Accounting_Modificaciones_Id_HModificaciones_A",
                        column: x => x.Id_HModificaciones_A,
                        principalTable: "Modificaciones",
                        principalColumn: "Id_HModificaciones",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Id_HModificaciones_A",
                table: "Accounting",
                column: "Id_HModificaciones_A");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounting");
        }
    }
}
