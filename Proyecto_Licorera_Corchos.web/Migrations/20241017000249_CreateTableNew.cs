using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyecto_Licorera_Corchos.web.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id_Client = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id_Client);
                });

            migrationBuilder.CreateTable(
                name: "Modifications",
                columns: table => new
                {
                    Id_Modification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Modification_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifications", x => x.Id_Modification);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id_Rol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Permissions_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id_Rol);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id_Product);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id_Sales = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sale_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_Sales = table.Column<float>(type: "real", nullable: false),
                    Id_Orders = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id_Sales);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_Rol = table.Column<int>(type: "int", nullable: false),
                    PermissionsId_Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id_User);
                    table.ForeignKey(
                        name: "FK_Users_Permissions_PermissionsId_Rol",
                        column: x => x.PermissionsId_Rol,
                        principalTable: "Permissions",
                        principalColumn: "Id_Rol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounting",
                columns: table => new
                {
                    Id_Accounting = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accounting_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id_Modification = table.Column<int>(type: "int", nullable: false),
                    Id_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounting", x => x.Id_Accounting);
                    table.ForeignKey(
                        name: "FK_Accounting_Modifications_Id_Modification",
                        column: x => x.Id_Modification,
                        principalTable: "Modifications",
                        principalColumn: "Id_Modification",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounting_Users_Id_User",
                        column: x => x.Id_User,
                        principalTable: "Users",
                        principalColumn: "Id_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id_Order = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orders_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Contact = table.Column<int>(type: "int", nullable: true),
                    Total_Order = table.Column<decimal>(type: "money", nullable: true),
                    Orders_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id_Product = table.Column<int>(type: "int", nullable: false),
                    Id_Client = table.Column<int>(type: "int", nullable: false),
                    Id_Accounting = table.Column<int>(type: "int", nullable: false),
                    Id_Sales = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id_Order);
                    table.ForeignKey(
                        name: "FK_Orders_Accounting_Id_Accounting",
                        column: x => x.Id_Accounting,
                        principalTable: "Accounting",
                        principalColumn: "Id_Accounting",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_Id_Client",
                        column: x => x.Id_Client,
                        principalTable: "Clients",
                        principalColumn: "Id_Client",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Products_Id_Product",
                        column: x => x.Id_Product,
                        principalTable: "Products",
                        principalColumn: "Id_Product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Sales_Id_Sales",
                        column: x => x.Id_Sales,
                        principalTable: "Sales",
                        principalColumn: "Id_Sales",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersAudit",
                columns: table => new
                {
                    Id_UserAudit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAudit_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id_Accounting = table.Column<int>(type: "int", nullable: false),
                    AccountingId_Accounting = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAudit", x => x.Id_UserAudit);
                    table.ForeignKey(
                        name: "FK_UsersAudit_Accounting_AccountingId_Accounting",
                        column: x => x.AccountingId_Accounting,
                        principalTable: "Accounting",
                        principalColumn: "Id_Accounting",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Id_Modification",
                table: "Accounting",
                column: "Id_Modification");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Id_User",
                table: "Accounting",
                column: "Id_User");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id_Accounting",
                table: "Orders",
                column: "Id_Accounting");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id_Client",
                table: "Orders",
                column: "Id_Client");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id_Product",
                table: "Orders",
                column: "Id_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Id_Sales",
                table: "Orders",
                column: "Id_Sales");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PermissionsId_Rol",
                table: "Users",
                column: "PermissionsId_Rol");

            migrationBuilder.CreateIndex(
                name: "IX_UsersAudit_AccountingId_Accounting",
                table: "UsersAudit",
                column: "AccountingId_Accounting");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "UsersAudit");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Accounting");

            migrationBuilder.DropTable(
                name: "Modifications");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Permissions");
        }
    }
}
