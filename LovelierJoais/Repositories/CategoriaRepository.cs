using LovelierJoais.Context;
using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;

namespace LovelierJoais.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
