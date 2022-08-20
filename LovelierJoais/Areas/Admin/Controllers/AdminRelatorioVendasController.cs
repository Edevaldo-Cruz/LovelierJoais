using LovelierJoais.Areas.Admin.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace LovelierJoais.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminRelatorioVendasController : Controller
    {
        private readonly RelatorioVendasService relatorioVendasService;

        public AdminRelatorioVendasController(RelatorioVendasService _relatorioVendasService)
        {
            this.relatorioVendasService = _relatorioVendasService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate, DateTime? maxDate)
        {
            // Se não for informada a data minima, vai ser considerado o ano atual e o dia 01 de janeiro(mes 01)
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }

            //Se não for informada a data maxima, vai ser considerada a data atual.
            if(!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            // Passando informação com as views data
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var result = await relatorioVendasService.FindByDateAsync(minDate.Value, maxDate.Value);
            return View(result);
        }
    }
}
