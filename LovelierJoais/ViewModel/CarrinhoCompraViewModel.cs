using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;

namespace LovelierJoais.ViewModel
{
    public class CarrinhoCompraViewModel
    {
        public CarrinhoCompra CarrinhoCompra { get; set; }
        public decimal CarrinhoCompraTotal { get; set; }
        //public ICategoriaRepository Categorias { get; internal set; }

        //public IEnumerable<Categoria> Categorias { get; set; }

       // public IEnumerable<Produto> Produtos { get; set; }
    }
}
