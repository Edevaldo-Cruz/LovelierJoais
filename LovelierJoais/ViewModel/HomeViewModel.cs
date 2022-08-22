using LovelierJoais.Models;

namespace LovelierJoais.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Produto> Promocao { get; set; }

        public IEnumerable<Produto> Destaque { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }        
    }
}
