using FIAP_Persistencia.Application.Interface;
using FIAP_Persistencia.Application.Model;
using FIAP_Persistencia.Consumer.Model;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Consumer.Service
{
    public class AtualizacaoConsumer : BaseConsumer<ContatoAtualizacaoDto>
    {
        public AtualizacaoConsumer(IServiceProvider serviceProvider, IConfiguration configuration) : base("fila_atualizacao", serviceProvider, configuration)
        {
        }

        protected override async Task ProcessMessage(ContatoAtualizacaoDto contato, IServiceScope scope)
        {
            var service = scope.ServiceProvider.GetRequiredService<IContatoApplicationService>();
            await service.AtualizarContato(contato.Id, contato.Contato);
        }
    }
}
