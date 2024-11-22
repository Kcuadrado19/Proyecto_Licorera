using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Licorera_Corchos.web.Migrations
{
    /// <inheritdoc />
    public partial class FixSalesProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Eliminar la columna 'Name' de 'Sales' si ya no es necesaria
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sales");

            // 2. Agregar la columna 'ProductId' a 'Sales'
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Sales",
                type: "int",
                nullable: true); // Permitir nulos temporalmente para evitar conflictos iniciales

            // 3. Habilitar IDENTITY_INSERT y agregar un producto predeterminado
            migrationBuilder.Sql(@"
                SET IDENTITY_INSERT Product ON;

                IF NOT EXISTS (SELECT 1 FROM Product WHERE Id = 1)
                BEGIN
                    INSERT INTO Product (Id, Name, Description, Price) 
                    VALUES (1, 'Producto Default', 'Asignado automáticamente', 0.0)
                END;

                SET IDENTITY_INSERT Product OFF;
            ");

            // 4. Asignar 'ProductId = 1' a todas las ventas existentes
            migrationBuilder.Sql(@"
                UPDATE Sales 
                SET ProductId = 1 
                WHERE ProductId IS NULL
            ");

            // 5. Crear el índice para 'ProductId'
            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            // 6. Agregar la restricción de clave foránea
            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Product_ProductId",
                table: "Sales",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revertir los cambios realizados en el método Up

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Product_ProductId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProductId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

