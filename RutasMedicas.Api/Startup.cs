using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RutasMedicas.Api.Filters;
using RutasMedicas.Business.Api.interfaces;
using RutasMedicas.Business.Api.services;
using RutasMedicas.Data.Api.connection;
using RutasMedicas.Data.Api.interfaces;
using RutasMedicas.Data.Api.repositories;
using System;

namespace RutasMedicas.Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string path = AppDomain.CurrentDomain.BaseDirectory;
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configuración app settings
            services.AddSingleton<IConfiguration>(configuration);

            // Inyección de dependencias -> Servicios
            services.AddTransient<IEpsService, EpsService>();
            services.AddTransient<IPersonService, PersonService>();

            // Inyección de dependencias -> Repositorio
            services.AddTransient<IConnection, Connection>(); 
            services.AddTransient<IEpsRepository, EpsRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();

            // Filters
            services.AddMvc(m =>
            {
                m.Filters.Add<ApiExceptionFilterAttribute>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Cors
            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
