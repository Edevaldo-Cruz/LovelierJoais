using LovelierJoais.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LovelierJoais.Models;
using LovelierJoais.ViewModels;
using Newtonsoft.Json;
using LovelierJoais.Repositories;

namespace LovelierJoais.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly ISubcategoriaRepository _subcategoriaRepository;

        public CarrinhoCompraController(IProdutoRepository produtoRepository, 
                                        ICategoriaRepository categoriaRepository, 
                                        CarrinhoCompra carrinhoCompra, 
                                        ISubcategoriaRepository subcategoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _carrinhoCompra = carrinhoCompra;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
            };
            ViewBag.Categorias = _categoriaRepository.Categorias;
            ViewBag.Subcategorias = _subcategoriaRepository.Subcategorias;
            //var list = TempData["Selecionado"] ;
            //ViewBag.selecionado = list;

            return View(carrinhoCompraVM);
        }

        public IActionResult AdicionarItemNoCarrinhoCompra(int produtoId)
        {
            var produtoSelecionado = _produtoRepository.Produtos
                                     .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produtoSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(produtoSelecionado);
            }

            //TempData["Selecionado"] = JsonConvert.SerializeObject(produtoSelecionado, Formatting.None,
            //            new JsonSerializerSettings()
            //            {
            //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //            });
            ViewBag.Subcategorias = _subcategoriaRepository.Subcategorias;
            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemDoCarrinhoCompra(int produtoId)
        {
            var produtoSelecionado = _produtoRepository.Produtos
                                    .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produtoSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(produtoSelecionado);
            }
            return RedirectToAction("Index");
        }
    }
}
