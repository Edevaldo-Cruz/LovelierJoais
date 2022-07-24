using LovelierJoais.Context;
using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;

namespace LovelierJoais.Repositories
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        private readonly AppDbContext _context;

        public SubcategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Subcategoria> Subcategorias => _context.Subcategorias;
    }
}
