using LovelierJoais.Context;
using LovelierJoais.Models;
using LovelierJoais.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LovelierJoais.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminPedidosExController : Controller
    {
        private readonly AppDbContext _context;

        //Metódo para obter infomações do banco de dado
        public AdminPedidosExController(AppDbContext context)
        {
            _context = context;
        }

        //Metódo para exibir informações do pedido
        public IActionResult PedidoLanches(int ? id)
        {
            // Consulta
            var pedido = _context.Pedidos
                            .Include(pd => pd.PedidoItens)
                            .ThenInclude(l => l.Produto)
                            .FirstOrDefault(p => p.PedidoId == id);

            // Verifica se pedido existe
            if(pedido == null)
            {
                Response.StatusCode = 404;
                return View("PedidoNotFound", id.Value);
            }

            // Preenche dado da viewModel com informaçoes da consulta
            PedidoProdutoViewModel pedidoProdutos = new PedidoProdutoViewModel()
            {
                Pedido = pedido,
                PedidoDetalhes = pedido.PedidoItens
            };

            return View(pedidoProdutos);
        }

        // GET: Admin/AdminPedidos
        /* Index sem paginação
        public async Task<IActionResult> Index()
        {
              return View(await _context.Pedidos.ToListAsync());
        }*/

        // Index com paginação e filtro
        //public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        //{
        //    var resultado = _context.Pedidos.AsNoTracking()
        //                            .AsQueryable();

        //    //Filtro
        //    //Se a string filter não for null e não for espaços vazios
        //    if(!string.IsNullOrWhiteSpace(filter))
        //    {
        //        resultado = resultado.Where(p => p.Nome.Contains(filter));
        //    }

        //    //Atribuindo a variavel model o valor de resultado, tamanho da pagina pageindex que é 1 e a ordenação que o nome.
        //    var model = await PagingList.CreateAsync(resultado, 3, pageindex, sort, "Nome");


        //    //Rota para o filtro
        //    model.RouteValue = new RouteValueDictionary { { "filter", filter } };

        //    return View(model);
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "") //Nome
        {
            var resultado = _context.Pedidos.AsNoTracking()
                                      .AsQueryable();

            //if (!string.IsNullOrWhiteSpace(filter))
            //{
            //    resultado = resultado.Where(p => p.Nome.Contains(filter));               

            //}

            var model = await PagingList.CreateAsync(resultado, 3, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminPedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminPedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // POST: Admin/AdminPedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,Nome,Sobrenome,Endereco1,Endereco2,Cep,Estado,Cidade,Telefone,Email,PedidoEnviado,PedidoEntregueEm")] Pedido pedido)
        {
            if (id != pedido.PedidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.PedidoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // GET: Admin/AdminPedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.PedidoId == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Admin/AdminPedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AppDbContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(e => e.PedidoId == id);
        }
    }
}
