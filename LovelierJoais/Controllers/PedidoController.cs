using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LovelierJoais.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly ICategoriaRepository _categoriaRepository;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra, ICategoriaRepository categoriaRepository)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            List<CarrinhoCompraItem> Items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItems = Items;

            if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
            {
                ModelState.AddModelError("", "Sua sacola está vazia, que tal incluir um item???");
            }

            foreach (var item in Items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Produto.Preco * item.Quantidade);
            }

            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            if (ModelState.IsValid)
            {
                _pedidoRepository.CriarPedido(pedido);

                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                _carrinhoCompra.LimparCarrinho();

                return View("~/Views/Pedido/CheckoutCompleto.cshtml", new PedidoProdutoViewModel
                {
                    Pedido = pedido,
                    Categorias = _categoriaRepository.Categorias
                });
            }

            return View("~/Views/Pedido/CheckoutCompleto.cshtml", new PedidoProdutoViewModel
            {
                Pedido = pedido,
                Categorias = _categoriaRepository.Categorias
            });
        }
    }
}
