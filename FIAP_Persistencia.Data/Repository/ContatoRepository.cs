using Dapper;
using FIAP_Persistencia.Data.Context;
using FIAP_Persistencia.Dominio.Entity;
using FIAP_Persistencia.Dominio.Repository;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Data.Repository
{
    public class ContatoRepository(IDbConnection context) : Context<Contato>(context), IContatoRepository
    {
        public async Task<int> CadastrarScalarAsync(Contato contato)
        {

            var parametros = new DynamicParameters();
            parametros.Add("@Nome", contato.Nome);
            parametros.Add("@DDD", contato.DDD);
            parametros.Add("@Telefone", contato.Telefone);
            parametros.Add("@Email", contato.Email);

            var sql = @"INSERT INTO FIAPContato.Contato (Nome, DDD, Telefone, Email) 
                VALUES (@Nome, @DDD, @Telefone, @Email);
                SELECT LAST_INSERT_ID();";

            var id = await context.ExecuteScalarAsync<int>(sql, parametros);

            return id;

        }
    }
}
