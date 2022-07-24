using LovelierJoais.Models;

namespace LovelierJoais.Repositories.Interfaces
{
    public interface ISubcategoriaRepository
    {
        IEnumerable<Subcategoria> Subcategorias { get; }
    }
}
