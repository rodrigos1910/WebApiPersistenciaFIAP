using FIAP_Persistencia.Application.Interface;
using FIAP_Persistencia.Application.Mapper;
using FIAP_Persistencia.Application.Service;
using FIAP_Persistencia.Consumer.Service;
using FIAP_Persistencia.Data.Repository; 
using FIAP_Persistencia.Dominio.Repository;
using FIAP_Persistencia.Dominio.Service;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Contato.CrossCutting
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddSingleton(MapperConfiguration.RegisterMapping());
            services.AddScoped<IContatoDomainService, ContatoDomainService>();
            services.AddScoped<IContatoApplicationService, ContatoApplicationService>();
            services.AddScoped<IContatoRepository, ContatoRepository>();
            // services.AddScoped<IContatoConsumer, ContatoConsumer>();

            services.AddSingleton<CadastroConsumer>();
            services.AddSingleton<AtualizacaoConsumer>();
            services.AddSingleton<RemocaoConsumer>();

            return services;
        }
    }
}
