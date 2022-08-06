using LovelierJoais.Models;
using Microsoft.EntityFrameworkCore;

namespace LovelierJoais.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItem { get; set; }

        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhe { get; set; }

    }
}
