using FIAP_Persistencia.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Domain.Interface
{
    public interface IContatoRepository : IRepositoryBase<Contato>
    {
        Task<int> CadastrarScalarAsync(Contato contato);
    }
}
