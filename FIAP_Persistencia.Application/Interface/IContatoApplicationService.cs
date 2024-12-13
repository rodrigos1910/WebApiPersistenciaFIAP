using FIAP_Persistencia.Application.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Application.Interface
{
    public interface IContatoApplicationService
    {
        Task<string> CadastrarContato(ContatoModel request);
        Task<string> AtualizarContato(int id, ContatoModel request);
        Task<string> DeletarContato(int id);

        Task<IEnumerable<ContatoModelResponse>> ObterTodosContatos(string? ddd);
    }
}
