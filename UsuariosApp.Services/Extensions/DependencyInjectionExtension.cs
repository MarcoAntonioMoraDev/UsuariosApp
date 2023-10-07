using UsuariosApp.Application.Interfaces;
using UsuariosApp.Application.Services;
using UsuariosApp.Domain.Interfaces.Messages;
using UsuariosApp.Domain.Interfaces.Repositories;
using UsuariosApp.Domain.Interfaces.Security;
using UsuariosApp.Domain.Interfaces.Services;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;
using UsuariosApp.Infra.Messages.Consumers;
using UsuariosApp.Infra.Messages.Producers;
using UsuariosApp.Infra.Security;

namespace UsuariosApp.Services.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioAppService, UsuarioAppService>();
            services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IHistoricoAtividadeRepository, HistoricoAtividadeRepository>();
            services.AddTransient<ITokenSecurity, TokenSecurity>();
            services.AddTransient<IEmailMessageProducer, EmailMessageProducer>();
            //services.AddHostedService<EmailMessageConsumer>();

            return services;
        }
    }
}



