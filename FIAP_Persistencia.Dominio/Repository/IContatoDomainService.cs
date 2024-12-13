using FIAP_Persistencia.Dominio.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Dominio.Repository
{
    public interface IContatoDomainService
    {
        Task<string> CadastrarContato(Contato request);
        Task<string> AtualizarContato(Contato request);
        Task<IEnumerable<Contato>> ObterTodosContatos(string? ddd);
        Task<string> DeletarContato(int id);
        Task<bool> VerificarContatoExistente(Contato contato);
        void TratarContato(Contato contato);
    }
}
