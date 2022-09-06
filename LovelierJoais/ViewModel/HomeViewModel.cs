using LovelierJoais.Models;

namespace LovelierJoais.ViewModels
{
    public class HomeViewModel 
    {
        public IEnumerable<Produto> ProdutoPromocao { get; set; }

        public IEnumerable<Produto> ProdutoDestaque { get; set; }

        public IEnumerable<Produto> ProdutoInfo { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }   

        public IEnumerable<Produto> Produtos { get; set; }
        

    }
}
