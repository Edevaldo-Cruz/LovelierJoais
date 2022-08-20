using LovelierJoais.Context;
using LovelierJoais.Models;
using LovelierJoais.Repositories;
using LovelierJoais.Repositories.Interfaces;
using LovelierJoais.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace LovelierJoais
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //conecxao com banco de dados
            services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                                    .AddEntityFrameworkStores<AppDbContext>()
                                    .AddDefaultTokenProviders();

            // Serviço do repository
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ISubcategoriaRepository, SubcategoriaRepository>();
            services.AddTransient<IPedidoRepository, PedidoRepository>();

            //Habilitando memoria cache
            services.AddMemoryCache();

            //Habilitando Session
            services.AddSession();

            //Serviço para acessar recurso do HTTPContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Serviço perfil e usuario
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            //Registrando serviço class carrinho de compra
            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

            //Configuração das caracteristica das senha cadastrada pelo usuario (codigo não obrigatório)
            services.Configure<IdentityOptions>(options =>
            {
                //minimo um digito (true)
                options.Password.RequireDigit = false;

                //minimo um caracter minisculo (true)
                options.Password.RequireLowercase = false;

                //minimo um caracter alphaNumerico (true)
                options.Password.RequireNonAlphanumeric = false;

                //minimo um caracter mausculo (true)
                options.Password.RequireUppercase = false;

                //minimo de caracteres (6)
                options.Password.RequiredLength = 3;

                //Requer o número de caracteres distintos na senha. (1)
                options.Password.RequiredUniqueChars = 1;
            });

            // Politica de Autorização
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    politica =>
                    {
                        politica.RequireRole("Admin");
                    });
            });

            //Paginação
            services.AddPaging(options =>
            {
                options.ViewName = "Bootdtrap4";
                options.PageParameterName = "pageindex";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
        {
            //cria os perfis
            seedUserRoleInitial.SeedRoles();
            //cria os usuários e atribui ao perfil
            seedUserRoleInitial.SeedUsers();

            //Habilitando o Session
            app.UseSession();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                );
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}