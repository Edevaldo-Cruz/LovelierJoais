using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LovelierJoais.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
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

            var lancheListViewModel = new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = categoriaAtual,
            };

            return View(lancheListViewModel);
        }

        public IActionResult Details(int produtoId)
        {
            var lanche = _produtoRepository.Produtos.FirstOrDefault(l => l.ProdutoId == produtoId);
            return View(lanche);
        }
    }
}
