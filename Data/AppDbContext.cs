using Microsoft.EntityFrameworkCore;
using InventoryMaster.Models;

namespace InventoryMaster.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinho { get; set; }
        public DbSet<ContaPagar> ContasPagar { get; set; }
        public DbSet<ContaReceber> ContasReceber { get; set; }
    }
}