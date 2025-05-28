using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryMaster.Migrations
{
    /// <inheritdoc />
    public partial class AjusteProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Products_ProductId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCarrinho_Products_ProductId",
                table: "ItensCarrinho");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ItensCarrinho_ProductId",
                table: "ItensCarrinho");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProductId",
                table: "Estoques");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "ItensCarrinho",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Estoques",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SKU = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_ProdutoId",
                table: "ItensCarrinho",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCarrinho_Produtos_ProdutoId",
                table: "ItensCarrinho",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estoques_Produtos_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropForeignKey(
                name: "FK_ItensCarrinho_Produtos_ProdutoId",
                table: "ItensCarrinho");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_ItensCarrinho_ProdutoId",
                table: "ItensCarrinho");

            migrationBuilder.DropIndex(
                name: "IX_Estoques_ProdutoId",
                table: "Estoques");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "ItensCarrinho");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Estoques");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ItensCarrinho_ProductId",
                table: "ItensCarrinho",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Estoques_ProductId",
                table: "Estoques",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estoques_Products_ProductId",
                table: "Estoques",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItensCarrinho_Products_ProductId",
                table: "ItensCarrinho",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
