using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.ViewModel;
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

        public IActionResult List(string subcategoria)
        {
            IEnumerable<Produto> produtos;
            string subcategoriaAtual = string.Empty;

            // Se a variavel categoria estiver null ou vazia
            if (string.IsNullOrEmpty(subcategoria))
            {
                
                produtos = (IEnumerable<Produto>)_produtoRepository.Produtos.OrderBy(l => l.ProdutoId);
                subcategoriaAtual = "Todos os Produtos";
            }
            else
            {
                //Se a variavel categoria for igual a Normal 
                if (string.Equals("1", subcategoria, StringComparison.OrdinalIgnoreCase))
                {
                    produtos = _produtoRepository.Produtos
                    .Where(p => p.SubcategoriaId == 1)
                    .OrderBy(l => l.ProdutoId);
                }
                else
                {                  
                    produtos = _produtoRepository.Produtos
                    .Where(p => p.SubcategoriaId == 2)
                    .OrderBy(l => l.ProdutoId);
                }
                subcategoriaAtual = subcategoria;
            }

            var ProdutoListViewModel = new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = subcategoriaAtual,
                Categorias = _categoriaRepository.Categorias
            };

            return View(ProdutoListViewModel);
        }

        public IActionResult Details(int produtoId)
        {
            var selecionado = _produtoRepository.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
            var ProdutoListViewModel = new ProdutoListViewModel
            {               
                Categorias = _categoriaRepository.Categorias
            };            
            ViewBag.Selecionado = selecionado;
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

            return View("~/Views/Produto/List.cshtml", new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = categoriaAtual,
                Categorias = _categoriaRepository.Categorias
            });
        }

    }
}

