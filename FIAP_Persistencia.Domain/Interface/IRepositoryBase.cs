using FIAP_Persistencia.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Domain.Interface
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<int> CadastrarAsync(T entity);
        Task<bool> AtualizarAsync(T entity);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<bool> DeletarAsync(T entity);
    }
}
