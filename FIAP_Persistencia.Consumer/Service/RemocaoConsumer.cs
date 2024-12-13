using FIAP_Persistencia.Application.Interface;
using FIAP_Persistencia.Application.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Consumer.Service
{
    public class RemocaoConsumer : BaseConsumer<int>
    {
        public RemocaoConsumer(IServiceProvider serviceProvider, IConfiguration configuration) : base("fila_exclusao", serviceProvider, configuration)
        {
        }

        protected override async Task ProcessMessage(int id, IServiceScope scope)
        {
            var service = scope.ServiceProvider.GetRequiredService<IContatoApplicationService>();
            await service.DeletarContato(id);
        }
    }
}
