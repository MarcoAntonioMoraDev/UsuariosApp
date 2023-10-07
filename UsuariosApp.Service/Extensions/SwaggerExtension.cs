using Microsoft.OpenApi.Models;
using System.Reflection;

namespace UsuariosApp.Services.Extensions
{
    /// <summary>
    /// Classe de extensão para configurarmos a documentação
    /// da API gerada pela biblioteca Swagger
    /// </summary>
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "UsuariosApp - API para controle de usuários.",
                    Description = "API desenvolvida em .NET 7 com EntityFramework, XUnit, JWT e RabbitMQ",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "COTI Informática",
                        Email = "contato@cotiinformatica.com.br",
                        Url = new Uri("http://www.cotiinformatica.com.br")
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuariosApp");
            });

            return app;
        }
    }
}


