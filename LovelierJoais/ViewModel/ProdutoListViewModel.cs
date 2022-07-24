using LovelierJoais.Models;

namespace LovelierJoais.ViewModel
{
    public class ProdutoListViewModel
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public string CategoriaAtual { get; set; }
    }
}
