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

        public HomeController(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
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
                Promocao = _produtoRepository.ProdutoPromocao,
                Destaque = _produtoRepository.ProdutoDestaque,
                //Categorias = _categoriaRepository.Categorias
            };
            ViewBag.Produtos = _produtoRepository.ProdutoDestaque.ToList();
            ViewBag.Categorias = _categoriaRepository.Categorias;
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