using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;

namespace LovelierJoais.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICategoriaRepository _categoriaRepository;

        public AccountController(UserManager<IdentityUser> userManager, 
                                    SignInManager<IdentityUser> signInManager, 
                                        ICategoriaRepository categoriaRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _categoriaRepository = categoriaRepository;
        }



        //Método Get
        public IActionResult Login(string returnUrl)
        {
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }
        //Método Post
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
                return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                //Senha e login correto
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVM.ReturnUrl);
                }
            }
            //Usuario null ou usuario e senha incorreto
            ModelState.AddModelError("", "Falha ao realizar o login!!");
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return View(loginVM);
        }

        //Acionando a view Register
        public IActionResult Register()
        {
           
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return View();
        }

        //Método para o registro do usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = registroVM.UserName };
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                if (result.Succeeded)
                {
                    /*Codigo para inserir usuario no perfil member. 
                     * Implementação so dever ser incluida quando todas as 
                     * implemetação de perfil e usuario estiverem pronto */
                    await _userManager.AddToRoleAsync(user, "Member");


                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    this.ModelState.AddModelError("Registro", "Falha ao registrar o usuário");
                }
            }
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return View(registroVM);
        }

        //Método Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //Limpando a sessão
            HttpContext.Session.Clear();

            //Atribuindo null para o usuario
            HttpContext.User = null;

            //Logout
            await _signInManager.SignOutAsync();

            //Redirecionado a pagina para Home
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return RedirectToAction("Index", "Home");
        }

        // Método para retorna View acesso negado
        public IActionResult AccessDenied()
        {
            ViewBag.Categorias = _categoriaRepository.Categorias;
            return View();
        }
        
    }
}
