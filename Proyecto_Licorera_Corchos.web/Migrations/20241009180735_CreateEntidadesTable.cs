using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Licorera_Corchos.web.Migrations
{
    /// <inheritdoc />
    public partial class CreateEntidadesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id_Cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id_Cliente);
                });

            migrationBuilder.CreateTable(
                name: "Modificaciones",
                columns: table => new
                {
                    Id_HModificaciones = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_H = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modificaciones", x => x.Id_HModificaciones);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id_Pedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contacto = table.Column<int>(type: "int", nullable: true),
                    Precio_Pedido = table.Column<decimal>(type: "money", nullable: true),
                    Fecha_Pedido = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Producto = table.Column<int>(type: "int", nullable: false),
                    Id_Cliente = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id_Pedido);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Permiso = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.Id_Rol);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id_Producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categoria = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Precio = table.Column<float>(type: "real", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id_Producto);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id_Ventas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha_Ventas = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_Ventas = table.Column<float>(type: "real", nullable: false),
                    Id_Pedido = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id_Ventas);
                });

            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    Id_A = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_A = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasena_A = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_HModificaciones_A = table.Column<int>(type: "int", nullable: false),
                    ModificacionesId_HModificaciones = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.Id_A);
                    table.ForeignKey(
                        name: "FK_Accounting_Modificaciones_ModificacionesId_HModificaciones",
                        column: x => x.ModificacionesId_HModificaciones,
                        principalTable: "Modificaciones",
                        principalColumn: "Id_HModificaciones",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id_Usuarios = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contrasenna = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_Rol = table.Column<int>(type: "int", nullable: false),
                    PermisosId_Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id_Usuarios);
                    table.ForeignKey(
                        name: "FK_Usuarios_Permisos_PermisosId_Rol",
                        column: x => x.PermisosId_Rol,
                        principalTable: "Permisos",
                        principalColumn: "Id_Rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ausuarios",
                columns: table => new
                {
                    Id_AU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_AU = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_A = table.Column<int>(type: "int", nullable: false),
                    AccountingId_A = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ausuarios", x => x.Id_AU);
                    table.ForeignKey(
                        name: "FK_Ausuarios_Accounting_AccountingId_A",
                        column: x => x.AccountingId_A,
                        principalTable: "Accounting",
                        principalColumn: "Id_A",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_ModificacionesId_HModificaciones",
                table: "Accounting",
                column: "ModificacionesId_HModificaciones");

            migrationBuilder.CreateIndex(
                name: "IX_Ausuarios_AccountingId_A",
                table: "Ausuarios",
                column: "AccountingId_A");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PermisosId_Rol",
                table: "Usuarios",
                column: "PermisosId_Rol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ausuarios");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "Accounting");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Modificaciones");
        }
    }
}
