using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Licorera_Corchos.web.Migrations
{
    /// <inheritdoc />
    public partial class DetailingRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Renombrar tabla RoleSection a RoleSections
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleSection",
                table: "RoleSection");

            migrationBuilder.RenameTable(
                name: "RoleSection",
                newName: "RoleSections");

            // Validar existencia del índice antes de eliminarlo
            migrationBuilder.Sql(@"
        IF EXISTS (SELECT 1 
                   FROM sys.indexes 
                   WHERE name = 'IX_RoleSection_SectionId' AND object_id = OBJECT_ID('RoleSections'))
        BEGIN
            DROP INDEX [IX_RoleSection_SectionId] ON [RoleSections];
        END
    ");

            // Crear índice con el nuevo nombre
            migrationBuilder.CreateIndex(
                name: "IX_RoleSections_SectionId",
                table: "RoleSections",
                column: "SectionId");

            // Cambios en columnas existentes
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sections",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "LicoreraRoles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            // Añadir nueva columna
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            // Añadir clave primaria en tabla renombrada
            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleSections",
                table: "RoleSections",
                columns: new[] { "RoleId", "SectionId" });

            // Crear índices únicos
            migrationBuilder.CreateIndex(
                name: "IX_Sections_Name",
                table: "Sections",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true);

            // Añadir claves foráneas sin cascada
            migrationBuilder.AddForeignKey(
                name: "FK_RoleSections_LicoreraRoles_RoleId",
                table: "RoleSections",
                column: "RoleId",
                principalTable: "LicoreraRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict); // Restrict evita múltiples cascadas

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSections_Sections_SectionId",
                table: "RoleSections",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar claves foráneas
            migrationBuilder.DropForeignKey(
                name: "FK_RoleSections_LicoreraRoles_RoleId",
                table: "RoleSections");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleSections_Sections_SectionId",
                table: "RoleSections");

            // Eliminar índices únicos
            migrationBuilder.DropIndex(
                name: "IX_Sections_Name",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_Name",
                table: "Permissions");

            // Eliminar clave primaria
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleSections",
                table: "RoleSections");

            // Eliminar nueva columna
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

            // Restaurar índice con el nombre original
            migrationBuilder.DropIndex(
                name: "IX_RoleSections_SectionId",
                table: "RoleSections");

            migrationBuilder.CreateIndex(
                name: "IX_RoleSection_SectionId",
                table: "RoleSections",
                column: "SectionId");

            // Renombrar tabla RoleSections a RoleSection
            migrationBuilder.RenameTable(
                name: "RoleSections",
                newName: "RoleSection");

            // Restaurar clave primaria
            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleSection",
                table: "RoleSection",
                columns: new[] { "RoleId", "SectionId" });

            // Restaurar claves foráneas
            migrationBuilder.AddForeignKey(
                name: "FK_RoleSection_LicoreraRoles_RoleId",
                table: "RoleSection",
                column: "RoleId",
                principalTable: "LicoreraRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleSection_Sections_SectionId",
                table: "RoleSection",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

    }
}
