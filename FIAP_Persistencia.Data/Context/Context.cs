 

using MySql.Data.MySqlClient;
using Dapper.Contrib.Extensions;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP_Persistencia.Dominio.Repository;
using FIAP_Persistencia.Dominio.Entity;

namespace FIAP_Persistencia.Data.Context
{

    public class Context<T> : IRepositoryBase<T> where T : EntityBase
    {
        protected IDbConnection _context;

        public Context(IDbConnection context) => _context = context;

        internal IDbConnection Connection
        {
            get
            {
                return new MySqlConnection(_context.ConnectionString);
            }
        }

        public async Task<int> CadastrarAsync(T entity)
        {
            using (var con = Connection)
                return await _context.InsertAsync<T>(entity);
        }

        public async Task<bool> AtualizarAsync(T entity)
        {
            using (var con = Connection)
                return await _context.UpdateAsync<T>(entity);
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            using (var con = Connection)
                return await _context.GetAllAsync<T>();
        }

        public async Task<bool> DeletarAsync(T entity)
        {
            using (var con = Connection)
                return await _context.DeleteAsync<T>(entity);
        }
    }
}
