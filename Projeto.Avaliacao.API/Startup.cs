using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Projeto.Avaliacao.API.Dominio.Interfaces;
using Projeto.Avaliacao.API.Dominio.Servicos;
using Projeto.Avaliacao.API.Infra.Comum;
using Projeto.Avaliacao.API.Infra.Repositorios;
using System;

namespace Projeto.Avaliacao.API
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Projeto.Avaliacao.API", Version = "v1" });
            });

            services.AddDbContext<Contexto>(options =>
                 options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
            //services.AddDbContext<Contexto>(options => options.UseInMemoryDatabase(databaseName: "db_avaliacao"));
            services.AddTransient(typeof(IBaseRepositorio<>), typeof(BaseRepositorio<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IProdutoEntregaRepositorio, ProdutoEntregaRepositorio>();
            services.AddScoped<IImportacaoRepositorio, ImportacaoRepositorio>();
            services.AddScoped<IImportacaoDeArquivo, ImportacaoDeArquivo>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto.Avaliacao.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
