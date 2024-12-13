using FIAP_Persistencia.Dominio.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Dominio.Repository
{
    public interface IContatoRepository : IRepositoryBase<Contato>
    {
        Task<int> CadastrarScalarAsync(Contato contato);
    }

}
