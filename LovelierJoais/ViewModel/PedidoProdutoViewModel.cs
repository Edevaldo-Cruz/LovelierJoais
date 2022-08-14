using LovelierJoais.Models;

namespace LovelierJoais.ViewModel
{
    public class PedidoProdutoViewModel
    {
        public Pedido Pedido { get; set; }

        public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }

        public IEnumerable<Categoria> Categorias { get; set; }
    }
}
