using LovelierJoais.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LovelierJoais.Components
{
    public class SubcategoriaMenu : ViewComponent
    {
        // Indico qual tipo é minha variavel
        private readonly ISubcategoriaRepository _SubcategoriaRepository;

        // Preencho minha varial com as informações
        public SubcategoriaMenu(ISubcategoriaRepository subcategoriaRepository)
        {
            _SubcategoriaRepository = subcategoriaRepository;
        }

        public IViewComponentResult Invoke()
        {
            var subcategorias = _SubcategoriaRepository.Subcategorias.OrderBy(c => c.SubcategoriaNome);
            return View(subcategorias);
        }
    }
}
