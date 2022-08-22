using LovelierJoais.Models;

namespace LovelierJoais.ViewModels
{
    public class PedidoProdutoViewModel
    {
        public Pedido Pedido { get; set; }

        public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }
    }
}
