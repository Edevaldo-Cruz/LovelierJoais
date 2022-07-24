using LovelierJoais.Models;

namespace LovelierJoais.Repositories.Interfaces
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> Produtos { get; }

        IEnumerable<Produto> ProdutoPromocao { get; }

        IEnumerable<Produto> ProdutoDestaque { get; }

        Produto GetProdutosById(int produtoId);
    }
}
