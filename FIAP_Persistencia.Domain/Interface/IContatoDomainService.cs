using FIAP_Persistencia.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Domain.Interface
{
    public interface IContatoDomainService
    {
        Task<string> CadastrarContato(Contato request);
        Task<string> AtualizarContato(Contato request);
        Task<IEnumerable<Contato>> ObterTodosContatos(string? ddd);
        Task<string> DeletarContato(int id);
    }
}
