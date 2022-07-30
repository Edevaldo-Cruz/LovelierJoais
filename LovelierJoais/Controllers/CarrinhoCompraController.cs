﻿using LovelierJoais.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LovelierJoais.Models;
using LovelierJoais.ViewModel;

namespace LovelierJoais.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly IProdutoRepository _produtoRepository; 
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(IProdutoRepository produtoRepository, 
                                        ICategoriaRepository categoriaRepository, 
                                        CarrinhoCompra carrinhoCompra)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVM = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                Categorias = _categoriaRepository.Categorias,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()                
            };

            return View(carrinhoCompraVM);
        }

        public IActionResult AdicionarItemNoCarrinho(int produtoId)
        {
            var produtoSelecionado = _produtoRepository.Produtos
                                .FirstOrDefault(p => p.ProdutoId == produtoId);

            if (produtoSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(produtoSelecionado);
            }

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
