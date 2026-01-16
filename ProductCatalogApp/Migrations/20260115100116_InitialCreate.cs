using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CodeCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCategory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CodeCategory);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    CodeProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameProduct = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DescriptionProduct = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CodeCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.CodeProduct);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CodeCategory",
                        column: x => x.CodeCategory,
                        principalTable: "Categories",
                        principalColumn: "CodeCategory");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    CodeOrder = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodeProduct = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.CodeOrder);
                    table.ForeignKey(
                        name: "FK_Orders_Products_CodeProduct",
                        column: x => x.CodeProduct,
                        principalTable: "Products",
                        principalColumn: "CodeProduct");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CodeProduct",
                table: "Orders",
                column: "CodeProduct");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CodeCategory",
                table: "Products",
                column: "CodeCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
