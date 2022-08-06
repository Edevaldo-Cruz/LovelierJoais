using LovelierJoais.Models;

namespace LovelierJoais.ViewModel
{
    public class ProdutoListViewModel : Produto
    {
        public IEnumerable<Produto> Produtos { get; set; }

        public string CategoriaAtual { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }

        public IEnumerable<Produto> produto {get; set;}

       
    }
}
