using LovelierJoais.Models;
using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LovelierJoais.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ISubcategoriaRepository _subcategoriaRepository;

        public HomeController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository, ISubcategoriaRepository subcategoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _subcategoriaRepository = subcategoriaRepository;
        }

        public IActionResult Index()
        {
            string[] banners = {
                "/images/banner1.png",
                "/images/banner2.png",
                "/images/banner3.png",
                "/images/banner4.png",               
            };
            var rnd = new Random();
            int index = rnd.Next(banners.Length);
            ViewBag.BannerAliatorio = banners[index];
            

            var homeViewModel = new HomeViewModel
            {
                ProdutoPromocao = _produtoRepository.ProdutoPromocao,
                ProdutoDestaque = _produtoRepository.ProdutoDestaque,
                ProdutoInfo = _produtoRepository.ProdutoInfo,
            };
            
            ViewBag.Categorias = _categoriaRepository.Categorias;
            ViewBag.Subcategorias = _subcategoriaRepository.Subcategorias;
            ViewBag.teste = _produtoRepository.ProdutoPromocao;
            ViewBag.Scroll = "on";
            return View(homeViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}