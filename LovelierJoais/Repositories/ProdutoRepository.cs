using LovelierJoais.Context;
using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LovelierJoais.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Produto> Produtos => _context.Produtos.
                                    Include(c => c.Categoria);

        public IEnumerable<Produto> ProdutoPromocao => _context.Produtos.
                                    Where(p=> p.Promocao).
                                    Include(p => p.Categoria);

        public IEnumerable<Produto> ProdutoDestaque => _context.Produtos.
                                    Where(d => d.Destaque).
                                    Include(d => d.Categoria);

        public IEnumerable<Produto> ProdutoInfo => _context.Produtos.
                                    Where(i => i.Info).
                                    Include(i => i.Categoria);

        public Produto GetProdutosById(int produtoId)
        {
            return _context.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
        }
    }
}
