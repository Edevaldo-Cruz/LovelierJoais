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

        public ProdutoController(IProdutoRepository produtoRepository,
                                ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Produto> produtos;
            string categoriaAtual = string.Empty;

            // Se a variavel categoria estiver null ou vazia
            if (string.IsNullOrEmpty(categoria))
            {
                // Retorna todos os lanches ordenado pelo id.
                produtos = (IEnumerable<Produto>)_produtoRepository.Produtos.OrderBy(l => l.ProdutoId);
                categoriaAtual = "Todos os Produtos";
            }
            else
            {
                //Se a variavel categoria for igual a Normal 
                if (string.Equals("Prata", categoria, StringComparison.OrdinalIgnoreCase))
                {
                    //Filtra e retorna lanches normais
                    produtos = _produtoRepository.Produtos
                    .Where(l => l.Categoria.CategoriaNome.Equals("Prata"))
                    .OrderBy(l => l.ProdutoId);
                }
                else
                {
                    //Filtra e retorna lanches naturais
                    produtos = _produtoRepository.Produtos
                    .Where(l => l.Categoria.CategoriaNome.Equals("Folheado"))
                    .OrderBy(l => l.ProdutoId);
                }
                categoriaAtual = categoria;
            }

            var ProdutoListViewModel = new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = categoriaAtual,
                Categorias = _categoriaRepository.Categorias
            };

            return View(ProdutoListViewModel);
        }

        public IActionResult Details(int produtoId)
        {
            var produto = _produtoRepository.Produtos.FirstOrDefault(l => l.ProdutoId == produtoId);
            return View(produto);
        }
    }
}
