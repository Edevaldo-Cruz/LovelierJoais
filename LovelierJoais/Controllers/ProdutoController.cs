using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LovelierJoais.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ISubcategoriaRepository _subcategoriaRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, ISubcategoriaRepository subcategoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public IActionResult List(string subcategoria, string categoria, int produtoId)
        {
            IEnumerable<Produto> produtos;
            string subcategoriaAtual = string.Empty;
            string categoriaAtual = string.Empty;

            // Se a variavel categoria estiver null ou vazia
            if (string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(categoria))
            {
                produtos = (IEnumerable<Produto>)_produtoRepository.Produtos.OrderBy(l => l.ProdutoId);
                subcategoriaAtual = "Todos os Produtos";
            }
            else if (!string.IsNullOrEmpty(subcategoria) && string.IsNullOrEmpty(categoria))
            {
                produtos = _produtoRepository.Produtos
                    .Where(p => p.SubcategoriaId == Convert.ToInt32(subcategoria))
                    .OrderBy(l => l.ProdutoId);
                subcategoriaAtual = subcategoria;
            }
            else
            {

                produtos = _produtoRepository.Produtos
                   .Where(p => p.CategoriaId == Convert.ToInt32(categoria))
                   .OrderBy(l => l.ProdutoId);
                categoriaAtual = categoria;
            }

            var ProdutoListViewModel = new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = subcategoriaAtual,
                Categorias = _categoriaRepository.Categorias

            };

            ViewBag.Destaque = _produtoRepository.ProdutoDestaque.ToList();
            ViewBag.Categorias = _categoriaRepository.Categorias;
            ViewBag.Subcategorias = _subcategoriaRepository.Subcategorias;
            //ViewBag.Selecionado = _produtoRepository.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
            return View(ProdutoListViewModel);
        }

        public IActionResult Details(int produtoId)
        {
            var ProdutoListViewModel = new ProdutoListViewModel
            {
                //Categorias = _categoriaRepository.Categorias
                produto = _produtoRepository.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId)
            };
            ViewBag.Selecionado = _produtoRepository.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
            ViewBag.Categorias = _categoriaRepository.Categorias;
            ViewBag.Destaque = _produtoRepository.ProdutoDestaque.ToList();
            ViewBag.Subcategorias = _subcategoriaRepository.Subcategorias;
            return View(ProdutoListViewModel);
        }

        public ViewResult Search(string searchString)
        {
            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                produtos = _produtoRepository.Produtos.OrderBy(p => p.ProdutoId);
                categoriaAtual = "Todos os produtos";
            }
            else
            {
                produtos = _produtoRepository.Produtos
                            .Where(p => p.Nome.ToLower().Contains(searchString.ToLower()));

                if (produtos.Any())
                    categoriaAtual = "Produtos";
                else
                    categoriaAtual = "Nenhum produto foi encontrado";

            }

            ViewBag.Subcategorias = _subcategoriaRepository.Subcategorias;
            return View("~/Views/Produto/List.cshtml", new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = categoriaAtual,
                Categorias = _categoriaRepository.Categorias
            });
        }

    }
}

