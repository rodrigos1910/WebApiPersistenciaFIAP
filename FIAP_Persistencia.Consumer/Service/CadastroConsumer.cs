using FIAP_Persistencia.Application.Interface;
using FIAP_Persistencia.Application.Model;
using FIAP_Persistencia.Dominio.Entity;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Consumer.Service
{
    public class CadastroConsumer : BaseConsumer<ContatoModel>
    {
        public CadastroConsumer(IServiceProvider serviceProvider, IConfiguration configuration) : base("fila_cadastro", serviceProvider, configuration)
        {
        }

        protected override async Task ProcessMessage(ContatoModel contato, IServiceScope scope)
        {
            var service = scope.ServiceProvider.GetRequiredService<IContatoApplicationService>();
            await service.CadastrarContato(contato);
        }
    }
}
