﻿using LovelierJoais.Context;
using LovelierJoais.Models;
using LovelierJoais.Repositories;
using LovelierJoais.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                options.UseSqlServer(Configuration
                .GetConnectionString("DefaultConnection")));

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

            //Registrando serviço class carrinho de compra
            services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}