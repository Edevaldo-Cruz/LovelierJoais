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

        public IActionResult List()
        {
            //var produtos = _produtoRepository.Produtos;
            //return View(produtos);

            var produtosListViewModel = new ProdutoListViewModel();
            produtosListViewModel.Produtos = _produtoRepository.Produtos;
            produtosListViewModel.CategoriaAtual = "Categoria Atual";

            return View(produtosListViewModel);
        }
    }
}
