using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Parqueadero.AccesoDatos.Repository;
using Parqueadero.AccesoDatos.Repository.Data;
using Parqueadero.AccesoDatos.Repository.IRepository;
using Parqueadero.Negocio.Servicios;
using Parqueadero.Negocio.Servicios.IServicios;
using System;

namespace Parqueadero.APIs
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
            // Automapper - registro de mapeos
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddCors();
            services.AddControllers();
            
            services.AddScoped<IRepositoryRegistroVehiculo, RepositoryRegistroVehiculo>();
            services.AddScoped<IRepositoryTarifa, RepositoryTarifa>();
            services.AddScoped<IRepositoryTipoVehiculo, RepositoryTipoVehiculo>();
            services.AddScoped<IRepositoryVehiculo, RepositoryVehiculo>();

            services.AddScoped<IServiceRegistroVehiculo, ServiceRegistroVehiculo>();

            services.AddScoped<IContextDatabase, ContextDatabase>();
            AddSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:4200");
                options.AllowAnyMethod();
                options.AllowAnyHeader();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Parqueadero");
            });
        }


        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"Parqueadero {groupName}",
                    Version = groupName,
                    Description = "Parqueadero API",
                    Contact = new OpenApiContact
                    {
                        Name = "Parqueadero",
                        Email = string.Empty,
                        //Url = new Uri("https://foo.com/"),
                    }
                });
            });
        }
    }
}
