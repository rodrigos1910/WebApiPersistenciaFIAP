using FIAP_Persistencia.Application.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP_Persistencia.Consumer.Model
{
    public class ContatoAtualizacaoDto
    {
        public int Id { get; set; }
        public ContatoModel Contato { get; set; }
    }
}
