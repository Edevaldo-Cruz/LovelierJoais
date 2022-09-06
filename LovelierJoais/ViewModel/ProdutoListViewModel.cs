using LovelierJoais.Models;

namespace LovelierJoais.ViewModels
{
    public class ProdutoListViewModel : Produto
    {
        public IEnumerable<Produto> Produtos { get; set; }

        public string CategoriaAtual { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }

        public IEnumerable<Produto> Produto { get; set; }

        public IEnumerable<Produto> ProdutoDestaque { get; set; }

        public Produto produto { get; set; }


    }
}
